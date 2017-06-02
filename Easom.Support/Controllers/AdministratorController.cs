using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easom.Support.ActionFilters;
using System.Web.Mvc;
using Easom.Support.App_Start;
using Easom.Support;
using CHCMS.Utility;
using Easom.Core;
using Easom.Core.Support;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Easom.Support.Controllers
{

    public class AdministratorController : SysAdminBaseController
    {
        #region User

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult UserIndex(string kv, int pageIndex)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_USER))
            {
                LoginManager.GoUnAccessPage();
            }
            string roleID = RequestUtility.GetQueryString("roleid");
            string state = RequestUtility.GetQueryString("state");
            string type = RequestUtility.GetQueryString("type");
            Dictionary<string, string> searchDatas = new Dictionary<string, string>();
            searchDatas.Add("roleid", roleID);
            searchDatas.Add("state", state);
            searchDatas.Add("type", type);
            ViewBag.searchDatas = Easom.Core.Orders.GetSearchDataString(searchDatas);
            return View();
        }

        public ActionResult GetListUserData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_USER))
            {
                LoginManager.GoUnAccessPage();
            }
            try
            {
                Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);

                int pageSize = jQueryDataTablesRequestData.PageSize;
                int pageIndex = jQueryDataTablesRequestData.PageIndex;
                string keywords = jQueryDataTablesRequestData.Search;
                string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
                bool isDesc = jQueryDataTablesRequestData.IsDesc;
                int pageCount = 0;
                string where = "1=1";
                IList<dynamic> sourceData = new List<dynamic>();
                int roleID = RequestUtility.GetQueryInt("roleid", 0);
                int state = RequestUtility.GetQueryInt("state", -2);
                int type = RequestUtility.GetQueryInt("type", 0);

                IList<Hospital> hospital = null;
                //医院管理员和超级获取所对应用户的管理权限(判断的逻辑写到SQL中)
                if (CurrentUser.Name.ToUpper() == "ADMIN")
                {
                    hospital = Hospital.Actor.GetByALLHospital();
                }
                else
                {
                    hospital = UserInfo.GetHospitalByUserID(CurrentUser.ID, -1);
                }
                where += " AND ( CHARINDEX(',-1,',HospitalIDStrs)>0 ";
                foreach (Hospital item in hospital)
                {
                    where += " or CHARINDEX('," + item.ID + ",',HospitalIDStrs)>0 ";
                }
                where += ")";
                //string sql = string.Format("HospitalID in {0}", hospitalstr);
                //IList<UserToHospital> userHList = UserToHospital.Actor.Search(out pageCount, 1, -1, sql, "UserID", isDesc);
                //List<string> users = new List<string>();
                //foreach (var item in userHList)
                //{
                //    users.Add(item.UserID.ToString());

                //}
                //对查询的userid进行去重
                //List<string> usersID = users.Distinct().ToList();
                //string userstr = ",";
                //foreach (var item in usersID)
                //{
                //    userstr += item + ",";
                //}
                if (!string.IsNullOrEmpty(keywords))
                {
                    where += " AND (Name  LIKE '" + keywords + "%' OR TrueName LIKE '" + keywords + "%')";
                }

                if (state == -1 || state == 0)
                {
                    if (!string.IsNullOrEmpty(where))
                    {
                        where += " AND State=" + state;
                    }
                }

                if (roleID > 0)
                {
                    where += " AND RoleID=" + roleID;
                }
                else
                {
                    string rolesql = String.Empty;
                    if (CurrentUser.UserRole != null && !String.IsNullOrEmpty(CurrentUser.UserRole.ToRole))
                    {
                        string[] paras = StringUtility.Split(CurrentUser.UserRole.ToRole, ',');
                        //读取当前用户能查看的用户
                        rolesql += "(";
                        foreach (var item in paras)
                        {
                            rolesql += item + ",";
                        }
                        rolesql += "-1500)";
                        where += string.Format(" AND RoleID  in {0}", rolesql);
                    }
                }

                IList<UserInfo> entityList = UserInfo.Actor.ViewSearch(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
                if (entityList != null && entityList.Count > 0)
                {
                    foreach (var item in entityList)
                    {
                        //if (userstr.Contains("," + item.ID + ","))
                        //{
                        string hospitalStr = "";
                        int numHospital = 0;
                        foreach (Hospital hos in hospital)
                        {
                            UserToHospital userToHospital = UserToHospital.Actor.GetByUserIDAndHospitalID(item.ID, hos.ID);
                            if (userToHospital != null)
                            {
                                Hospital hospitalName = Hospital.Actor.GetByID(userToHospital.HospitalID);
                                hospitalStr += hospitalName.Name + "，";
                                numHospital++;
                            }
                        }
                        hospitalStr = hospitalStr.Trim('，');
                        if (hospitalStr.Length > 25)
                        {
                            hospitalStr = StringUtility.CutString(hospitalStr, 20, true) + "(等" + numHospital + "家医院)<a class=\"fa fa-hospital-o\" href=\"javascript:detail('" + item.ID + "')\" title=\"查看更多医院\"></a>";
                        }

                        string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                        string action = "<a class=\"fa fa-edit\" href=\"javascript:edit('" + item.ID + "')\" title=\"编辑\"></a>&nbsp;<a class=\"fa fa-h-square\" href=\"javascript:sethospital('" + item.ID + "')\" title=\"设置医院﹑科室\"></a>";
                        string userEdit = "<a href=\"javascript:detail('" + item.ID + "')\"/>" + item.TrueName + "</a>";
                        sourceData.Add(new dynamic[] { checkeds, item.ID, item.Name, userEdit, item.RoleTypeName, hospitalStr, item.CreateTime.ToString("yyyy-MM-dd"), action });
                        //}
                    }
                }
                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = pageCount,
                    iTotalDisplayRecords = pageCount,
                    aaData = sourceData,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log4NetUtility.WriterErrorLog(ex.ToString());
                throw;
            }
        }

        public ActionResult UserSearch()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_USER))
            {
                LoginManager.GoUnAccessPage();
            }
            string where = string.Empty;
            IList<Role> entityList = null;
            if (CurrentUser != null)
            {
                entityList = CurrentUser.UserRole.ManagerRoles;
            }
            ViewBag.roleList = entityList;
            return View();
        }

        public ActionResult SetHospital(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DU_SETHOSIPITAL))
            {
                LoginManager.GoUnAccessPage();
            }
            UserInfo user = UserInfo.Actor.GetByID(id);
            ViewBag.UserName = user.TrueName;
            ViewData["UserID"] = user.ID;
            IList<Hospital> hospital = null;
            if (CurrentUser.Name.ToUpper() == "ADMIN")
            {
                hospital = Hospital.Actor.GetByALLHospital();
            }
            else
            {
                hospital = UserInfo.GetHospitalByUserID(CurrentUser.ID, -1);
            }
            StringBuilder treeData = new StringBuilder();
            treeData.Append("rightTree.add(0, -1, '点击添加操作权限');");
            foreach (var item in hospital)
            {
                treeData.Append("rightTree.add(" + item.ID + ", 0, '" + item.Name + "');");
                IList<Section> sectionList = null;
                if (CurrentUser.Name.ToUpper() == "ADMIN")
                {
                    sectionList = Section.Actor.GetByHospitalID(item.ID);
                }
                else
                {
                    sectionList = Section.Actor.GetByUserID(CurrentUser.ID, -1, item.ID);
                }
                if (sectionList != null)
                {
                    foreach (var item1 in sectionList)
                    {
                        treeData.Append("rightTree.add(" + (item1.ID + 100000) + ", " + item.ID + ", '" + item1.Name + "');");
                    }
                }
            }

            string authids = string.Empty;
            IList<Hospital> hospitaltoId = UserInfo.GetHospitalByUserID(id, -1);
            foreach (var i in hospitaltoId)
            {
                authids += i.ID + ",";
                IList<Section> sectiontoId = Section.Actor.GetByUserID(id, -1, i.ID);
                foreach (var s in sectiontoId)
                {
                    authids += "" + (s.ID + 100000) + ",";
                }
            }

            foreach (var i in hospital)
            {
                authids += i + ",";
            }
            ViewBag.authids = authids;
            ViewBag.text = treeData;
            return View();
        }

        [HttpPost]
        public JsonResult SetHospital(FormCollection form, int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DU_SETHOSIPITAL))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id != CurrentUser.ID || CurrentUser.Name.ToUpper() == "ADMIN")
            {
                string ids = form["ids"];
                bool insertData = true;
                int defaluteHospitalID = 0;
                if (string.IsNullOrEmpty(ids))
                {
                    return Json(new { flag = "error" });
                }
                string[] paras = StringUtility.Split(ids, ',');
                IList<UserToSection> userToSection = UserToSection.Actor.GetByUserID(CurrentUser.ID);
                foreach (UserToSection userAndSection in userToSection)
                {
                    UserToSection.Actor.DeleteBySectionIDAndUserID(id, userAndSection.SectionID);
                }
                if (paras != null && paras.Length > 0)
                {
                    foreach (var item in paras)
                    {
                        if (int.Parse(item) > 100000)
                        {
                            UserToSection usertosection = new UserToSection();
                            usertosection.UserID = id;
                            usertosection.SectionID = int.Parse(item) - 100000;
                            usertosection.IsDefault = true;
                            UserToSection.Actor.Insert(usertosection);
                        }
                    }
                }
                /***********************医院************************************/
                UserInfo entity = UserInfo.Actor.GetByID(id);

                IList<Hospital> userToHospital = UserInfo.GetHospitalByUserID(CurrentUser.ID, -1);
                foreach (Hospital userAndHospital in userToHospital)
                {
                    UserToHospital.Actor.Delete(id, userAndHospital.ID);
                }
                if (entity != null)
                {
                    defaluteHospitalID = entity.HospitalID;
                }
                string hospitalStr = "";
                foreach (var items in paras)
                {
                    hospitalStr += items + ",";
                }
                hospitalStr = "," + hospitalStr;
                if (paras != null && paras.Length > 0)
                {
                    foreach (var item in paras)
                    {
                        if (int.Parse(item) < 100000)
                        {
                            if (defaluteHospitalID != int.Parse(item))
                            {
                                UserToHospital.Actor.Delete(id, int.Parse(item));
                                Hospital.Actor.InsertUserToHospital(id, int.Parse(item), false);
                            }
                            else
                            {
                                UserToHospital.Actor.Delete(id, int.Parse(item));
                                Hospital.Actor.InsertUserToHospital(id, int.Parse(item), true);
                            }
                        }
                    }
                }
                return insertData == true ? Json(new { flag = "success" }) : Json(new { flag = "error" });
            }
            return Json(new { flag = "error2" });
        }

        /// <summary>
        /// 获取科室内所有疾病
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSectionDataByHosital()
        {
            IList<Section> sectionList = null;
            string ids = RequestUtility.GetQueryString("ids");
            int userID = RequestUtility.GetQueryInt("userID", 0);
            IList<UserToSection> userToSectionList = UserToSection.Actor.GetByUserID(userID);
            string sectionStr = "";
            foreach (UserToSection userSec in userToSectionList)
            {
                sectionStr += "," + userSec.SectionID;
            }
            sectionStr += ",";

            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(ids, ',');
            StringBuilder diseaseData = new StringBuilder();
            foreach (var item in paras)
            {
                int hospitalID = Convert.ToInt32(item);
                if (hospitalID > 0)
                {
                    string hosipitalName = Hospital.Actor.GetByID(hospitalID).Name;
                    if (CurrentUser.Name.ToUpper() == "ADMIN")
                    {
                        sectionList = Section.Actor.GetByHospitalID(hospitalID);
                    }
                    else
                    {
                        sectionList = Section.Actor.GetByUserID(CurrentUser.ID, -1, hospitalID);
                    }

                    diseaseData.Append(string.Format(" <fieldset><legend class=\"choiceFld\">{0}</legend> <div class=\"fieldwrap input-uniform\">", hosipitalName));

                    foreach (Section sec in sectionList)
                    {
                        if (sectionStr.IndexOf("," + sec.ID + ",") >= 0)
                        {
                            diseaseData.Append(string.Format("<input name=\"checkbox\"  checked=\"checked\" class=\"checkbox\" type=\"checkbox\" style=\" margin-right:10px;\" value=\"{0}\" /><label class=\"choice\">{1}</label>", sec.ID, sec.Name));
                        }
                        else
                        {
                            diseaseData.Append(string.Format("<input name=\"checkbox\" class=\"checkbox\" type=\"checkbox\" style=\" margin-right:10px;\" value=\"{0}\" /><label class=\"choice\">{1}</label>", sec.ID, sec.Name));
                        }
                    }
                    diseaseData.Append("</div></fieldset>");
                }
            }
            return Json(new { flag = "success", diseaseData = diseaseData.ToString() }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UserAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.DU_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            if (CurrentUser.Name.ToUpper() == "ADMIN")
            {
                IList<Role> entityList = Role.Actor.GetAllData();
                ViewData["allrole"] = entityList;
            }
            else
            {
                IList<Role> entityList = CurrentUser.UserRole.ManagerRoles;
                ViewData["allrole"] = entityList;
            }

            return View();
        }

        [HttpPost]
        public JsonResult UserAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DU_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            bool insertData = false;
            Easom.Core.UserInfo userInfo = new UserInfo();
            Easom.Core.UserInfo existsUser = Easom.Core.UserInfo.Actor.GetByName(GetFormString(form, "username"));
            if (existsUser != null)
            {
                return Json(new { flag = Easom.Core.Support.UserInformationEum.HaveName.ToString() });
            }
            userInfo.Name = GetFormString(form, "username");
            userInfo.TrueName = GetFormString(form, "usertruename");
            userInfo.Email = GetFormString(form, "email");
            userInfo.Telephone = GetFormString(form, "mobile");
            userInfo.RoleID = GetFormInt(form, "roleid", -1);
            //RoleType role= (RoleType)ConvertUtility.EnumParse(typeof(RoleType), GetFormString(form, "roleid"), RoleType.TelCallBack);
            //userInfo.RoleID = (int)role;
            userInfo.LastLoginIP = CurrentUser == null ? "127.0.0.1" : CurrentUser.LastLoginIP;
            userInfo.CreateTime = DateTime.Now;
            userInfo.LastLoginTime = DateTime.Now;
            userInfo.PassWord = SecurityUtility.DESEncrypt(GetFormString(form, "Password"));
            userInfo.State = GetFormBool(form, "state") == true ? Easom.Core.Support.UserStateEnum.Normal : Easom.Core.Support.UserStateEnum.Disable;

            userInfo.PictureURL = "/default/photoMan.png";
            int returnData = Easom.Core.UserInfo.Actor.Insert(userInfo);
            if (returnData > 0)
            {
                insertData = Hospital.Actor.InsertUserToHospital(returnData, CurrentUser.HospitalID, true);
                foreach (Section sec in CurrentUser.UserSectionList)
                {
                    UserToSection usertosection = new UserToSection();
                    usertosection.UserID = returnData;
                    usertosection.SectionID = sec.ID;
                    usertosection.IsDefault = true;
                    UserToSection.Actor.Insert(usertosection);
                }
                MessageTemplate todayTemplate = new MessageTemplate
                {
                    Name = "新增预约患者提示模板（系统默认模板只能修改）",
                    Content = "尊敬的#姓名#，您已成功预约#预约时间#的专家号#专家号#，请您合理安排时间到我院就诊。",
                    CreateUserID = returnData,
                    State = TemplateTypeEnum.today,
                };
                MessageTemplate tomarrowTemplate = new MessageTemplate
                {
                    Name = "明日预到患者提醒模板（系统默认模板只能修改）",
                    Content = "尊敬的#姓名#，#医院名称#提醒您于#预约时间#来我院就诊，请在导医台报#专家号#和姓名有专人引导就医。",
                    CreateUserID = returnData,
                    State = TemplateTypeEnum.tomarrow,
                };
                MessageTemplate.Actor.Insert(todayTemplate);
                MessageTemplate.Actor.Insert(tomarrowTemplate);
            }

            return insertData == true ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }


        public ActionResult UserEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DU_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            UserInfo entity = Easom.Core.UserInfo.Actor.GetByID(id);
            if (CurrentUser.Name.ToUpper() == "ADMIN")
            {
                IList<Role> entityList = Role.Actor.GetAllData();
                ViewData["allrole"] = entityList;
            }
            else
            {
                IList<Role> entityList = CurrentUser.UserRole.ManagerRoles;
                ViewData["allrole"] = entityList;
            }
            if (entity != null)
            {
                return View(entity);
            }
            return View();
        }

        [HttpPost]
        public JsonResult UserEdit(FormCollection form, int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DU_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            UserInfo userInfo = Easom.Core.UserInfo.Actor.GetByID(id);
            UserInfo existsUser = Easom.Core.UserInfo.Actor.GetByName(GetFormString(form, "username"));
            if (existsUser != null && existsUser.ID != id)
            {
                return Json(new { flag = Easom.Core.Support.UserInformationEum.HaveName.ToString() });
            }
            userInfo.Name = GetFormString(form, "username");
            userInfo.TrueName = GetFormString(form, "usertruename");
            userInfo.ID = id;
            userInfo.Email = GetFormString(form, "email");
            userInfo.Telephone = GetFormString(form, "mobile");
            userInfo.RoleID = GetFormInt(form, "roleid", -1);
            if (GetFormString(form, "password") != userInfo.PassWord)
            {
                userInfo.PassWord = SecurityUtility.DESEncrypt(GetFormString(form, "password"));
            }
            userInfo.State = GetFormBool(form, "state") == true ? Easom.Core.Support.UserStateEnum.Normal : Easom.Core.Support.UserStateEnum.Disable;
            Easom.Core.UserInfo.Actor.Update(userInfo);
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult UserDelete(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DU_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            if (string.IsNullOrEmpty(form["ids"]))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(form["ids"], ',');
            foreach (var item in paras)
            {
                int id = ConvertUtility.ToInt32(item, -1);
                if (id > 0 && CurrentUser.ID != id)
                {
                    Easom.Core.UserInfo.Actor.Delete(id);
                }
            }
            return Json(new { flag = "success" });

        }

        public ActionResult UserDetailInformation(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_USER))
            {
                LoginManager.GoUnAccessPage();
            }
            string hospitalName = string.Empty;
            if (id > 0)
            {
                UserInfo user = UserInfo.Actor.GetByID(id);
                if (user != null)
                {
                    IList<Hospital> entityList = Hospital.Actor.GetByUserID(user.ID, -1);
                    if (entityList != null && entityList.Count > 0)
                    {
                        foreach (var item in entityList)
                        {
                            Hospital entity = item as Hospital;
                            if (entity != null)
                            {
                                hospitalName += entity.Name + " ";
                            }
                        }
                        ViewData["hospitalName"] = hospitalName;
                    }
                    return View(user);
                }
            }
            return View();
        }
        #endregion

        #region Role
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RoleIndex(string kv, int pageIndex)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_ROLE))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }


        public ActionResult GetListRoleData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_ROLE))
            {
                LoginManager.GoUnAccessPage();
            }
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;
            string where = string.Empty;
            if (!string.IsNullOrEmpty(keywords))
            {
                if (!string.IsNullOrEmpty(where))
                {
                    where += " AND Name like '%" + keywords + "%'";
                }
                else
                {
                    where = "Name like '%" + keywords + "%'";
                }
            }
            IList<dynamic> entityList = Easom.Core.Role.ConvertToArray(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = entityList
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult RoleAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        [HttpPost]
        public JsonResult RoleAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            Role entity = new Role();
            Role existsRole = Role.Actor.GetByName(GetFormString(form, "name"));
            if (existsRole != null)
            {
                return Json(new { flag = Easom.Core.Support.UserInformationEum.HaveName.ToString() });
            }
            entity.Name = GetFormString(form, "name");
            entity.Remark = GetFormString(form, "description");
            int returnData = Role.Actor.Insert(entity);
            return returnData > 0 ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }


        public ActionResult RoleEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            Role entity = Role.Actor.GetByID(id);
            if (entity != null)
            {
                return View(entity);
            }
            return View();
        }

        [HttpPost]
        public JsonResult RoleEdit(FormCollection form, int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            Role entity = Role.Actor.GetByID(id);
            Role existsRole = Role.Actor.GetByName(GetFormString(form, "name"));
            if (existsRole != null && existsRole.ID != id)
            {
                return Json(new { flag = Easom.Core.Support.UserInformationEum.HaveName.ToString() });
            }
            entity.Name = GetFormString(form, "name");
            entity.Remark = GetFormString(form, "description");
            Role.Actor.Update(entity);
            return Json(new { flag = "success" });
        }

        [HttpPost]
        public JsonResult RoleDelete(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            if (string.IsNullOrEmpty(form["ids"]))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(form["ids"], ',');
            foreach (var item in paras)
            {
                int id = ConvertUtility.ToInt32(item, -1);
                //&& CurrentUser.ID != id
                if (id > 0)
                {
                    Role.Actor.Delete(id);
                }
            }
            return Json(new { flag = "success" });

        }

        public ActionResult RoleSetAuthor(int roleID)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_SETAUTHOR))
            {
                LoginManager.GoUnAccessPage();
            }

            int pageCount = 1;
            IList<Authority> list = Easom.Core.Authority.Actor.Search(out pageCount, 1, -1, " ParentID=0", "ID", false);
            StringBuilder treeData = new StringBuilder();
            treeData.Append("rightTree.add(0, -1, '点击添加操作权限');");
            foreach (var item in list)
            {
                treeData.Append("rightTree.add(" + item.ID + ", " + item.ParentID + ", '" + item.Name + "');");
                if (item.ChannelList != null)
                {
                    foreach (var item1 in item.ChannelList)
                    {
                        treeData.Append("rightTree.add(" + item1.ID + ", " + item1.ParentID + ", '" + item1.Name + "');");
                        if (item1.ChannelList != null)
                        {
                            foreach (var item2 in item1.ChannelList)
                            {
                                treeData.Append("rightTree.add(" + item2.ID + ", " + item2.ParentID + ", '" + item2.Name + "');");
                            }
                        }
                    }
                }


            }

            IList<int> roleAuthroList = Role.Actor.GetByRoleID(roleID);
            string authids = string.Empty;
            foreach (var i in roleAuthroList)
            {
                authids += i + ",";
            }
            ViewBag.authids = authids;
            ViewData["roleid"] = roleID;
            Role roleEntity = Role.Actor.GetByID(roleID);
            if (roleEntity != null)
            {
                ViewBag.roleName = roleEntity.Name;
            }
            ViewBag.text = treeData;
            return View();
        }

        [HttpPost]
        public JsonResult RoleSetAuthor(FormCollection form, int roleID)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_SETAUTHOR))
            {
                LoginManager.GoUnAccessPage();
            }
            bool returnData = true;
            if (string.IsNullOrEmpty(form["ids"]) || roleID <= 0)
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(form["ids"], ',');
            if (paras != null && paras.Length > 0)
            {
                Role.Actor.DeleteByRoleID(roleID);
                foreach (var item in paras)
                {
                    int id = ConvertUtility.ToInt32(item, -1);
                    if (id > 0)
                    {
                        Authority.Actor.AddAuthors(roleID, id);
                    }
                }
            }
            return returnData == true ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }

        public ActionResult RoleManger(int roleID)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_SETTOROLE))
            {
                LoginManager.GoUnAccessPage();
            }
            IList<Role> list = Role.Actor.GetAllData();
            IList<int> roleAuthroList = Role.Actor.GetByRoleID(roleID);
            ViewData["roleid"] = roleID;
            Role roleEntity = Role.Actor.GetByID(roleID);
            if (roleEntity != null)
            {
                ViewBag.roleName = roleEntity.Name;
            }
            if (!string.IsNullOrEmpty(roleEntity.ToRole))
            {
                string[] paras = StringUtility.Split(roleEntity.ToRole, ',');
                int[] roleList = Array.ConvertAll(paras, id => Convert.ToInt32(id));
                if (roleList != null && roleList.Count() > 0)
                {
                    ViewBag.roleList = roleList;
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult RoleManger(FormCollection form, int roleID)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DR_SETTOROLE))
            {
                LoginManager.GoUnAccessPage();
            }
            string torole = form["ids"];
            Role roleEntity = Role.Actor.GetByID(roleID);
            roleEntity.ToRole = torole;
            Role.Actor.Update(roleEntity);
            return Json(new { flag = "success" });
        }


        #endregion Author

        #region Authority
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult AuthorIndex(string kv, int pageIndex)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_AUTHRITY))
            {
                LoginManager.GoUnAccessPage();
            }
            int pageCount = 1;
            IList<Authority> list = Easom.Core.Authority.Actor.Search(out pageCount, 1, -1, " ParentID=0", "ID", false);
            return View(list);
        }

        public ActionResult GetListAuthorData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_AUTHRITY))
            {
                LoginManager.GoUnAccessPage();
            }
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);

            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;
            string where = string.Format("Name like '%{0}%'", keywords);
            IList<dynamic> entityList = Easom.Core.Authority.ConvertToArray(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = entityList
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AuthorAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.DA_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            int parentID = RequestUtility.GetQueryInt("ids", -1);
            ViewData["parentID"] = parentID;
            return View();
        }

        [HttpPost]
        public JsonResult AuthorAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DA_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            Authority entity = new Authority();
            Authority existsRole = Authority.Actor.GetByAuthorityKey(GetFormString(form, "authorkey"));
            if (existsRole != null)
            {
                return Json(new { flag = Easom.Core.Support.UserInformationEum.HaveAuthorKey.ToString() });
            }
            entity.Name = GetFormString(form, "name");
            entity.Remark = GetFormString(form, "description");
            entity.AuthorityKey = GetFormString(form, "authorkey");
            entity.ParentID = GetFormInt(form, "parentID", -1);
            entity.CroupNumber = GetFormInt(form, "groupnumber", -1);
            int returnData = Authority.Actor.Insert(entity);
            return returnData > 0 ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }

        public ActionResult AuthorEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DA_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            Authority entity = Authority.Actor.GetByID(id);
            if (entity != null)
            {
                return View(entity);
            }
            return View();
        }

        [HttpPost]
        public JsonResult AuthorEdit(FormCollection form, int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DA_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            Authority entity = Authority.Actor.GetByID(id);
            Authority existsRole = Authority.Actor.GetByAuthorityKey(GetFormString(form, "authorkey"));
            if (existsRole != null && existsRole.ID != id)
            {
                return Json(new { flag = Easom.Core.Support.UserInformationEum.HaveAuthorKey.ToString() });
            }
            entity.Name = GetFormString(form, "name");
            entity.Remark = GetFormString(form, "description");
            entity.AuthorityKey = GetFormString(form, "authorkey");
            entity.CroupNumber = GetFormInt(form, "groupnumber", -1);
            Authority.Actor.Update(entity);
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AuthorDelete(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DA_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            if (string.IsNullOrEmpty(form["ids"]))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(form["ids"], ',');
            foreach (var item in paras)
            {
                int id = ConvertUtility.ToInt32(item, -1);
                Authority auhorigy = Authority.Actor.GetByID(id);
                {
                    if (auhorigy.ChannelList != null)
                    {
                        foreach (var item1 in auhorigy.ChannelList)
                        {
                            Authority.Actor.DeleteByParentID(item1.ID);
                        }
                    }

                }
                Authority.Actor.Delete(id);
                Authority.Actor.DeleteByParentID(id);
            }
            return Json(new { flag = "success" });
        }

        #endregion

        #region Notifier

        /// <summary>
        /// 通知中心设置
        /// </summary>
        /// <returns></returns>
        public ActionResult NotifierIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        /// <summary>
        /// 通知中心数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNotifierListData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;

            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;

            string where = " 1=1 ";

            if (keywords != "")
            {
                where += " and  TrueName  LIKE '%" + keywords + "%' or Name LIKE '%" + keywords + "%'";
            }

            IList<Notifier> list = Notifier.Actor.NotifierNotifyLogSearch(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                string action = "<a class=\"fa fa-edit\" href=\"javascript:edit('" + item.ID + "')\" title=\"编辑\"></a>&nbsp;";
                sourceData.Add(new dynamic[] {
                    checkeds,
                    item.Name,
                    "<a href='javascript:logindex(" + item.UserID + ")'>"+item.TrueName+"</a>",
                    Convert.ToInt32(item.NotifyType)==(int)NotifyTypeEnum.Mobile?"手机":Convert.ToInt32(item.NotifyType)==(int)NotifyTypeEnum.Email?"Email":Convert.ToInt32(item.NotifyType)==(int)NotifyTypeEnum.MobileEmail?"手机和Email":"未设置",
                    item.NotifyCycle==(int)NotifyCycleEnum.Day?"天":"未定义",
                    item.SendDate.ToString("yyyy-MM-dd HH:mm") !="0001-01-01 12:00" ? "" + item.SendDate.ToString("yyyy-MM-dd HH:mm") + "" : "<span style='color:gray'>无记录</span>",
                    item.SendState==SendState.MobileSuccess?"<span class='label l-success'>手机成功</span>":item.SendState==SendState.EmailSuccess?"<span class='label l-success'>邮件成功</span>":item.SendState==SendState.BothSuccess?"<span class='label l-success'>全部成功</span>":item.SendState==SendState.MobileFail?"<span class='label l-suspend'>手机失败</span>":item.SendState==SendState.EmailFail?"<span class='label l-suspend'>邮件失败</span>":item.SendState==SendState.BothFail?"<span class='label l-suspend'>全部失败</span>":"<span style='color:gray'>无记录</span>",
                    item.Disable ? "<span class='label l-success'>启动</span>" : "<span class='label l-suspend'>未启动</span>",
                    item.CreateDate.ToString("yyyy-MM-dd HH:mm"),
                    action
                });
            }

            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData,
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加通知
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult NotifierAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }


        public ActionResult GetAllUserData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);

            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(keywords))
            {
                where += " and  TrueName  LIKE '%" + keywords + "%' or Name LIKE '%" + keywords + "%' or Telephone LIKE '%" + keywords + "%' or Email LIKE '%" + keywords + "%'";
            }
            IList<UserInfo> entity = Easom.Core.UserInfo.Actor.UserInfoNotInNotifierSearch(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            IList<dynamic> sourceData = new List<dynamic>();
            foreach (var item in entity)
            {
                string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                sourceData.Add(new dynamic[] { checkeds, item.Name, item.TrueName, item.Telephone, item.Email });
            }
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加通知
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult NotifierAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            int returnData = -1;
            if (string.IsNullOrEmpty(form["ids"]))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(form["ids"], ',');
            Notifier notifier = new Notifier();

            foreach (var item in paras)
            {
                int id = ConvertUtility.ToInt32(item, -1);
                if (id > 0)
                {
                    notifier.UserID = id;
                    notifier.NotifyType = 0;
                    notifier.NotifyCycle = 0;
                    notifier.Disable = true;
                    notifier.CreateDate = DateTime.Now;
                    returnData = Notifier.Actor.Insert(notifier);
                }
            }
            return returnData > 0 ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult NotifierDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            bool result = false;
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { flag = "error" });
            }
            string[] idArr = StringUtility.Split(ids, ',');
            if (idArr != null && idArr.Length > 0)
            {
                foreach (var item in idArr)
                {
                    int id = ConvertUtility.ToInt32(item, -1);
                    if (id > 0)
                    {
                        result = Notifier.Actor.Delete(id);
                    }
                }
            }
            return result == true ? Json(new { flag = "success" }, JsonRequestBehavior.AllowGet) : Json(new { flag = "error" });
        }

        /// <summary>
        /// 编辑状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult NotifierEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            Notifier entity = Notifier.Actor.GetByID(id);
            if (entity != null)
            {
                return View(entity);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 编辑状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult NotifierEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            Notifier entity = Notifier.Actor.GetByID(id);

            entity.NotifyType = (NotifyTypeEnum)GetFormInt(form, "notifytype", 0);
            entity.NotifyCycle = GetFormInt(form, "notifycycle", 0);
            entity.Disable = GetFormBool(form, "disable");
            bool returnData = Notifier.Actor.Update(entity);
            return returnData == true ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }

        #endregion

        #region NotifyLog

        [AuthorityActionFilter]
        public ActionResult NotifyLogIndex(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_NOTIF))
            {
                LoginManager.GoUnAccessPage();
            }
            IList<NotifyLog> entity = NotifyLog.Actor.GetByID(id);
            if (entity != null)
            {
                return View(entity);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region OprationLog

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult OperationLogIndex(string kv, int pageIndex)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_OPERATION_LOG))
            {
                LoginManager.GoUnAccessPage();
            }
            string linkID = RequestUtility.GetQueryString("linkID");
            Dictionary<string, string> searchDatas = new Dictionary<string, string>();
            searchDatas.Add("linkID", linkID);
            ViewBag.SearchDatas = GetNewSearchDataString(searchDatas);
            return View();
        }


        public ActionResult GetListOperationLogData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_OPERATION_LOG))
            {
                LoginManager.GoUnAccessPage();
            }
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;
           
            string where =" 1=1 ";
            if (!string.IsNullOrEmpty(keywords))
            {
                    where += " AND tableName like '%" + keywords + "%'";
            }
            string linkID = RequestUtility.GetQueryString("linkID");
            if (!string.IsNullOrEmpty(linkID))
            {
                where += " AND LinkID="+ linkID+" " ;
            }
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            string sectionStr = "(";
            foreach (var item in sectionlists)
            {
                sectionStr += item.ID + ",";
            }
            sectionStr += "-1500)";
            string usersql = string.Format("SectionID in {0}", sectionStr);
            where += string.Format("AND {0}", usersql);
            IList<OperationLog> entityList = Easom.Core.OperationLog.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            IList<dynamic> sourceData = new List<dynamic>();
            if (entityList != null && entityList.Count > 0)
            {
                foreach (var item in entityList)
                {
                   // JArray ja = (JArray)JsonConvert.DeserializeObject(item.OperationInfo);
                    sourceData.Add(new dynamic[] {
                       "",
                      "<a  href='javascript:chatcontent("+item.LinkID+")'>"+ item.TableName+"</a>",
                       item.OperationTypeName,
                       item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                       item.UserName,
                       item.OperationInfo
                    });
                }
            }
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData
            }, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}
