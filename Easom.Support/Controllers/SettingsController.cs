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
using Newtonsoft.Json.Linq;

namespace Easom.Support.Controllers
{

    public class SettingsController : SysAdminBaseController
    {



        #region JJ_Account 竞价账户

        /// <summary>
        /// 账户设置
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult JJ_AccountIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_ACCOUNT))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        /// <summary>
        /// 取账户列表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetJJ_AccountListData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_ACCOUNT))
            {
                LoginManager.GoUnAccessPage();
            }
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;

            if (CurrentUser.HospitalID <= 0)
            {
                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string>(),
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string keywords = jQueryDataTablesRequestData.Search;
                string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
                bool isDesc = jQueryDataTablesRequestData.IsDesc;
                int pageCount = 0;
                string where = "1=1 ";
                if (keywords != "")
                {
                    where += " and  (AccountName  LIKE '%" + keywords + "%')";
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
                IList<JJ_Account> list = JJ_Account.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
                IList<dynamic> sourceData = new List<dynamic>();
                foreach (var item in list)
                {
                    sourceData.Add(new dynamic[] { item.ID, item.AccountName, item.SectionID, item.ID, item.SectionName });
                }
                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = pageCount,
                    iTotalDisplayRecords = pageCount,
                    aaData = sourceData,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        public ActionResult JJ_AccountAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_ACCOUNT_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            return View(sectionlists);
        }

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult JJ_AccountAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_ACCOUNT_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            JJ_Account entity = new JJ_Account();
            entity.AccountName = GetFormString(form, "AccountName");
            entity.HospitalID = CurrentUser.HospitalID;
            entity.SectionID = GetFormInt(form, "sectionid", -1);
            entity.Remark1 = GetFormString(form, "remark1");
            entity.Remark2 = GetFormString(form, "remark2");

            if (JJ_AccountName(entity.AccountName))
            {
                int row = JJ_Account.Actor.Insert(entity);
                if (row > 0)
                {
                    return Json(new { flag = "success" });
                }
                return Json(new { flag = "error" });
            }
            else
            {
                return Json(new { flag = "error2" });
            }
        }

        /// <summary>
        /// 编辑账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult JJ_AccountEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_ACCOUNT_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }

            var JJ_Accountlist = JJ_Account.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
            bool result = JJ_Account.GetJJ_AccountAuthority(JJ_Accountlist, id);


            ViewData["allsection"] = CurrentUser.UserSectionList;
            if (result == true)
            {
                JJ_Account entity = JJ_Account.Actor.GetByID(id);
                return View(entity);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 编辑账户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult JJ_AccountEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_ACCOUNT_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                JJ_Account entity = JJ_Account.Actor.GetByID(id);
                if (entity != null)
                {
                    string ident = GetFormString(form, "AccountName");
                    if (entity.AccountName == ident)
                    {
                        entity.AccountName = GetFormString(form, "AccountName");
                        entity.HospitalID = CurrentUser.HospitalID;
                        entity.SectionID = GetFormInt(form, "sectionid", -1);

                        JJ_Account.Actor.Update(entity);
                        return Json(new { flag = "success" });
                    }
                    else
                    {
                        bool result = DoctorIdentifier(ident);
                        if (result == true)
                        {
                            entity.AccountName = GetFormString(form, "AccountName");
                            entity.HospitalID = CurrentUser.HospitalID;
                            entity.SectionID = GetFormInt(form, "sectionid", -1);
                            entity.Remark1 = GetFormString(form, "remark1");
                            entity.Remark2 = GetFormString(form, "remark2");
                            JJ_Account.Actor.Update(entity);
                            return Json(new { flag = "success" });
                        }
                        else
                        {
                            return Json(new { flag = "error2" });
                        }
                    }
                }
                else
                {
                    return Json(new { flag = "error" });
                }
            }
            else
            {
                return Json(new { flag = "error" });
            }
        }

        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult JJ_AccountDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_ACCOUNT_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }

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
                        var JJ_Accountlist = JJ_Account.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
                        bool result = JJ_Account.GetJJ_AccountAuthority(JJ_Accountlist, id);
                        if (result == true)
                        {
                            JJ_Account.Actor.Delete(id);

                            //JJ_Account doctor = JJ_Account.Actor.GetByID(id);
                            ////对账户进行软删除0正常，1回收站
                            //doctor.IsDelete = 1;
                            //JJ_Account.Actor.Update(doctor);
                        }
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 帐号编号是否重复
        /// </summary>
        /// <returns></returns>
        public bool JJ_AccountName(string AccountName)
        {

            int pageCount = 0;
            string where = " hospitalID=" + CurrentUser.HospitalID + " and AccountName='" + AccountName + "'";
            IList<JJ_Account> ist = JJ_Account.Actor.Search(out pageCount, 1, 10, where, "ID", true);
            return ist.Count > 0 ? false : true;
        }

        #endregion


        #region Doctor

        /// <summary>
        /// 医生设置
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult DoctorIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.C_DOCTOR))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        /// <summary>
        /// 取医生列表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDoctorListData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.C_DOCTOR))
            {
                LoginManager.GoUnAccessPage();
            }
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;

            if (CurrentUser.HospitalID <= 0)
            {
                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string>(),
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string keywords = jQueryDataTablesRequestData.Search;
                string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
                bool isDesc = jQueryDataTablesRequestData.IsDesc;
                int pageCount = 0;
                string where = "IsDelete=0 ";
                if (keywords != "")
                {
                    where += " and  (Name  LIKE '%" + keywords + "%' or Identifier LIKE '%" + keywords + "%')";
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
                IList<Doctor> list = Doctor.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

                IList<dynamic> sourceData = new List<dynamic>();

                foreach (var item in list)
                {
                    string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                    string userEdit = "<a class=\"fa fa-edit\" href=\"javascript:view('" + item.ID + "')\"/></a>";
                    sourceData.Add(new dynamic[] { checkeds, item.Identifier, item.Name, item.SectionName, userEdit });
                }

                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = pageCount,
                    iTotalDisplayRecords = pageCount,
                    aaData = sourceData,
                }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 添加医生
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        public ActionResult DoctorAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.CD_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            return View(sectionlists);
        }

        /// <summary>
        /// 添加医生
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult DoctorAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CD_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            Doctor entity = new Doctor();
            entity.Name = GetFormString(form, "name");
            entity.HospitalID = CurrentUser.HospitalID;
            entity.SectionID = GetFormInt(form, "sectionid", -1);
            entity.Intro = GetFormString(form, "intro");
            entity.Identifier = GetFormString(form, "identifier");
            if (entity.HospitalID == -1)
            {
                return Json(new { flag = "error3" });
            }
            else
            {
                if (DoctorIdentifier(entity.Identifier))
                {
                    int row = Doctor.Actor.Insert(entity);
                    if (row > 0)
                    {
                        return Json(new { flag = "success" });
                    }
                    return Json(new { flag = "error" });
                }
                else
                {
                    return Json(new { flag = "error2" });
                }
            }
        }

        /// <summary>
        /// 编辑医生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult DoctorEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CD_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            var doctorlist = Doctor.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
            bool result = Doctor.GetDoctorAuthority(doctorlist, id);
            ViewData["allsection"] = CurrentUser.UserSectionList;
            if (result == true)
            {
                Doctor entity = Doctor.Actor.GetByID(id);
                return View(entity);
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 编辑医生
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult DoctorEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CD_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                Doctor entity = Doctor.Actor.GetByID(id);
                if (entity != null)
                {
                    string ident = GetFormString(form, "identifier");
                    if (entity.Identifier == ident)
                    {
                        entity.Name = GetFormString(form, "name");
                        entity.HospitalID = CurrentUser.HospitalID;
                        entity.SectionID = GetFormInt(form, "sectionid", -1);
                        entity.Intro = GetFormString(form, "intro");
                        entity.Identifier = ident;
                        Doctor.Actor.Update(entity);
                        return Json(new { flag = "success" });
                    }
                    else
                    {
                        bool result = DoctorIdentifier(ident);
                        if (result == true)
                        {
                            entity.Name = GetFormString(form, "name");
                            entity.HospitalID = CurrentUser.HospitalID;
                            entity.SectionID = GetFormInt(form, "sectionid", -1);
                            entity.Intro = GetFormString(form, "intro");
                            entity.Identifier = ident;
                            Doctor.Actor.Update(entity);
                            return Json(new { flag = "success" });
                        }
                        else
                        {
                            return Json(new { flag = "error2" });
                        }
                    }
                }
                else
                {
                    return Json(new { flag = "error" });
                }
            }
            else
            {
                return Json(new { flag = "error" });
            }
        }

        /// <summary>
        /// 删除医生
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult DoctorDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CD_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }

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
                        var doctorlist = Doctor.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
                        bool result = Doctor.GetDoctorAuthority(doctorlist, id);
                        if (result == true)
                        {
                            Doctor doctor = Doctor.Actor.GetByID(id);
                            //对医生进行软删除0正常，1回收站
                            doctor.IsDelete = 1;
                            Doctor.Actor.Update(doctor);
                        }
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 医生编号是否重复
        /// </summary>
        /// <returns></returns>
        public bool DoctorIdentifier(string identifier)
        {

            int pageCount = 0;
            string where = " hospitalID=" + CurrentUser.HospitalID + " and identifier='" + identifier + "'" + " And IsDelete=0";
            IList<Doctor> ist = Doctor.Actor.Search(out pageCount, 1, 10, where, "ID", true);
            return ist.Count > 0 ? false : true;
        }

        #endregion

        #region Disease

        /// <summary>
        /// 疾病列表
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult DiseaseIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.C_DIEASE))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        /// <summary>
        /// 取疾病列表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDiseaseListData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.C_DIEASE))
            {
                LoginManager.GoUnAccessPage();
            }

            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;

            if (CurrentUser.HospitalID <= 0)
            {
                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string>(),
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string keywords = jQueryDataTablesRequestData.Search;
                string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "Priority";
                bool isDesc = jQueryDataTablesRequestData.IsDesc;
                int pageCount = 0;
                string where = " IsDelete=0 ";
                if (keywords != "")
                {
                    where += " and  Name  LIKE '%" + keywords + "%'";
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

                IList<Disease> list = Disease.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

                IList<dynamic> sourceData = new List<dynamic>();

                foreach (var item in list)
                {
                    string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                    string userEdit = "<a class=\"fa fa-edit\" href=\"javascript:view('" + item.ID + "')\"/></a>";
                    sourceData.Add(new dynamic[] { checkeds, item.Name, item.IsMain == true ? "是" : "否", item.SectionName, item.Cure, userEdit });
                }

                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = pageCount,
                    iTotalDisplayRecords = pageCount,
                    aaData = sourceData,
                }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 添加疾病
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult DiseaseAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.CC_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            return View(sectionlists);
        }

        /// <summary>
        /// 疾病添加
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult DiseaseAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CC_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            Disease entity = new Disease();
            entity.Priority = GetFormInt(form, "priority", 0);
            entity.HospitalID = CurrentUser.HospitalID;
            entity.IsMain = GetFormString(form, "isMain") == "1" ? true : false;
            entity.SectionID = GetFormInt(form, "sectionid", -1);
            entity.CreatTime = DateTime.Now;
            entity.Cure = GetFormString(form, "cure");
            entity.Name = GetFormString(form, "name");
            entity.Intro = GetFormString(form, "intro");
            if (DiseaseIdentifier(entity.Name))
            {
                int row = Disease.Actor.Insert(entity);
                if (row > 0)
                {
                    return Json(new { flag = "success" });
                }
                else
                {
                    return Json(new { flag = "error" });
                }
            }
            else
            {
                return Json(new { flag = "error2" });
            }
        }
        /// <summary>
        /// 编辑疾病
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult DiseaseEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CC_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            ViewData["allsection"] = CurrentUser.UserSectionList;
            IList<Disease> list = Disease.Actor.GetAllDisease(CurrentUser.HospitalID);
            bool result = Disease.GetDiseaseAuthority(list, id);
            if (result == true)
            {
                Disease entity = Disease.Actor.GetByID(id);
                return View(entity);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 编辑疾病
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult DiseaseEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CC_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                Disease entity = Disease.Actor.GetByID(id);
                if (entity != null)
                {
                    entity.Priority = GetFormInt(form, "priority", 0);
                    entity.HospitalID = CurrentUser.HospitalID;
                    entity.IsMain = GetFormString(form, "ismain") == "0" ? false : true;
                    entity.SectionID = GetFormInt(form, "sectionid", -1);
                    entity.Cure = GetFormString(form, "cure");
                    entity.Name = GetFormString(form, "name");
                    entity.Intro = GetFormString(form, "intro");
                    Disease.Actor.Update(entity);
                    return Json(new { flag = "success" });
                }
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 删除疾病
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult DiseaseDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CC_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
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
                        IList<Disease> list = Disease.Actor.GetAllDisease(CurrentUser.HospitalID);
                        bool result = Disease.GetDiseaseAuthority(list, id);
                        if (result == true)
                        {
                            Disease disease = Disease.Actor.GetByID(id);
                            //对疾病进行软删除0正常，1回收站
                            disease.IsDelete = 1;
                            Disease.Actor.Update(disease);
                        }
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 疾病名称是否重复
        /// </summary>
        /// <returns></returns>
        public bool DiseaseIdentifier(string name)
        {

            int pageCount = 0;
            string where = " name='" + name + "' And  HospitalID=" + CurrentUser.HospitalID + " And IsDelete=0";
            IList<Disease> ist = Disease.Actor.Search(out pageCount, 1, 10, where, "ID", true);
            if (ist.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 返回整张疾病表集合
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult GetAllDisease()
        {
            //if (!LoginManager.HasAutority(AuthorityConst.S_DISEASE))
            //{
            //    LoginManager.GoUnAccessPage();
            //}
            IList<Disease> list = Disease.Actor.GetAllDisease(CurrentUser.HospitalID);
            return Json(list);
        }

        #endregion

        #region Hospital

        /// <summary>
        /// 医院列表
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult HospitalIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_HOSIPITAL))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public ActionResult GetHospitalListData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_HOSIPITAL))
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
                where += " and  Name  LIKE '%" + keywords + "%' or Intro LIKE '%" + keywords + "%'";
            }

            IList<Hospital> list = Hospital.Actor.Search(out pageCount, pageIndex, pageSize, where, "ID", true);

            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                string userEdit = "<a class=\"fa fa-edit\" href=\"javascript:view('" + item.ID + "')\"/></a>";
                sourceData.Add(new dynamic[] { checkeds, item.Name, item.Intro, userEdit });
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
        /// 添加医院
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        public ActionResult HospitalAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.DH_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            return View();
        }


        /// <summary>
        /// 添加医院
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult HospitalAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DH_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            Hospital entity = new Hospital();
            entity.AreaID = GetFormInt(form, "areaid", 0);
            entity.Name = GetFormString(form, "name");
            entity.Intro = GetFormString(form, "intro");
            if (entity.AreaID == -1)
            {
                return Json(new { flag = "error2" });
            }
            else
            {
                int row = Hospital.Actor.Insert(entity);
                OperationLog logs = new OperationLog();
                JArray jlogs = new JArray();
                if (!string.IsNullOrEmpty(entity.Name))
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "医院名称"),
                        new JProperty("Value", entity.Name)
                        ));
                }
                if (!string.IsNullOrEmpty(entity.Intro))
                {
                    jlogs.Add(new JObject(
                  new JProperty("Name", "医院简介"),
                  new JProperty("Value", entity.Intro)
                  ));
                }
                logs.OperationType = OperationTypeEnum.Add;
                logs.TableName = "医院信息";
                logs.OperationInfo = jlogs.ToString();
                logs.LinkID = row;
                if (jlogs.Count > 0)
                {
                    CreateOperationLogs(logs);
                }

                //添加完医院给医院添加默认科室
                if (row > 0)
                {
                    Section section = new Section();
                    section.HospitalID = row;
                    section.Name = "默认科室";
                    section.Intro = "默认科室";
                    int mediaRow = Section.Actor.Insert(section);
                   MediaSource.Actor.InsertAllMediaNew(row);

                    OperationLog slogs = new OperationLog();
                    JArray sjlogs = new JArray();
                    if (!string.IsNullOrEmpty(section.Name))
                    {
                        sjlogs.Add(new JObject(
                            new JProperty("Name", "科室名称"),
                            new JProperty("Value", section.Name)
                            ));
                    }
                    if (!string.IsNullOrEmpty(section.Intro))
                    {
                        sjlogs.Add(new JObject(
                      new JProperty("Name", "科室简介"),
                      new JProperty("Value", section.Intro)
                      ));
                    }
                    slogs.OperationType = OperationTypeEnum.Add;
                    slogs.TableName = "科室信息";
                    slogs.OperationInfo = sjlogs.ToString();
                    slogs.LinkID = mediaRow;
                    if (jlogs.Count > 0)
                    {
                        CreateOperationLogs(slogs);
                    }
                    return Json(new { flag = "success" });
                }
                return Json(new { flag = "error" });
            }
        }

        /// <summary>
        /// 编辑医院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult HospitalEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DH_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            Hospital entity = Hospital.Actor.GetByID(id);
            return View(entity);
        }

        /// <summary>
        /// 编辑医院
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult HospitalEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DH_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                Hospital entity = Hospital.Actor.GetByID(id);
                if (entity != null)
                {
                    entity.AreaID = GetFormInt(form, "areaid", 0);
                    entity.Name = GetFormString(form, "name");
                    entity.Intro = GetFormString(form, "intro");
                    Hospital.Actor.Update(entity);
                    return Json(new { flag = "success" });
                }
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 删除医院
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult HospitalDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DH_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
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
                        Hospital.Actor.Delete(id);
                        // Hospital.Actor.HospitalIdDelete(id);
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取当前角色的所有医院
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult ChangeHospital()
        {

            IList<Hospital> allhospital = UserInfo.GetHospitalByUserID(CurrentUser.ID, -1);
            if (allhospital != null)
            {
                ViewData["Hospitalid"] = CurrentUser.HospitalID;
                return View(allhospital);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 切换当前医院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult CurrentHospitalEdit(FormCollection form)
        {

            int id = GetFormInt(form, "hospitalid", 0);
            int defult = UserInfo.UpdateUserTOHospital(CurrentUser.ID, id);
            if (defult > 0)
            {
                return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { flag = "error" });
            }
        }


        /// <summary>
        /// 获取当前角色的所有医院科室
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult ChangeHospitalSection()
        {

            //获取当前用户所的科室
            IList<Section> allSection = Section.Actor.GetByUserID(CurrentUser.ID, -1, CurrentUser.HospitalID);
            //获取当前用户所选择的科室
            IList<Section> entitySection = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
            if (allSection != null)
            {
                ViewData["allSection"] = allSection;
                ViewData["userSection"] = entitySection;
                return View();
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public JsonResult ChangeHospitalSection(FormCollection form)
        {

            string ids = form["ids"];
            bool insertData = false;
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(ids, ',');
            if (paras != null && paras.Length > 0)
            {
                IList<Section> entitySection = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
                var s = from item in entitySection
                        select item.ID;
                foreach (var item in s)
                {
                    UserToSection.Actor.UpdateUserToSection(CurrentUser.ID, item, false);
                }
                foreach (var item in paras)
                {
                    int sectionID = Convert.ToInt32(item);
                    UserToSection.Actor.UpdateUserToSection(CurrentUser.ID, sectionID, true);
                }
                insertData = true;
            }
            return insertData == true ? Json(new { flag = "success" }) : Json(new { flag = "error" });

        }


        /// <summary>
        /// 获取当前角色的所有医院
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult SetHospital()
        {
            //if (!LoginManager.HasAutority(AuthorityConst.G_HOSPITAL))
            //{
            //    LoginManager.GoUnAccessPage();
            //}
            IList<Hospital> allhospital = UserInfo.GetHospitalByUserID(CurrentUser.ID, -1);
            if (allhospital != null)
            {
                ViewData["Hospitalid"] = CurrentUser.HospitalID;
                return View(allhospital);
            }
            else
            {
                return null;
            }
        }

        public JsonResult CurrentHospitalID()
        {
            int id = RequestUtility.GetQueryInt("hospitalid", 0);
            int defult = UserInfo.UpdateUserTOHospital(CurrentUser.ID, id);
            if (defult > 0)
            {
                return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { flag = "error" });
            }
        }
        #endregion

        #region HospitalWebsite

        /// <summary>
        /// 医院网站
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult HospitalWebsiteIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.C_SITE))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public ActionResult GetHospitalWebsitelistData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.C_SITE))
            {
                LoginManager.GoUnAccessPage();
            }
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;

            if (CurrentUser.HospitalID <= 0)
            {
                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string>(),
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string keywords = jQueryDataTablesRequestData.Search;
                string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
                bool isDesc = jQueryDataTablesRequestData.IsDesc;
                int pageCount = 0;

                string where = " 1=1 ";
                if (keywords != "")
                {
                    where += " and  URL  LIKE '%" + keywords + "%'";
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

                IList<HospitalWebsite> list = HospitalWebsite.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

                IList<dynamic> sourceData = new List<dynamic>();

                foreach (var item in list)
                {
                    string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                    string userEdit = "<a class=\"fa fa-edit\" href=\"javascript:view('" + item.ID + "')\"/></a>";
                    sourceData.Add(new dynamic[] { checkeds, item.URL, item.SiteType == (int)SorceTypeEnum.Site ? "网站" : "微信", item.SectionName, userEdit });
                }

                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = pageCount,
                    iTotalDisplayRecords = pageCount,
                    aaData = sourceData,
                }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 添加医院网站
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        public ActionResult HospitalWebsiteAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.CS_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            return View(sectionlists);
        }
        /// <summary>
        /// 添加医院网站
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult HospitalWebsiteAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CS_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            HospitalWebsite entity = new HospitalWebsite();
            string url = GetFormString(form, "url");
            entity.HospitalID = CurrentUser.HospitalID;
            entity.Title = url;
            entity.SiteType = GetFormInt(form, "source", -1);
            entity.SectionID = GetFormInt(form, "sectionid", -1);
            HospitalWebsite existEntity = HospitalWebsite.Actor.GetByURL(url.ToLower());
            if (existEntity != null)
            {
                return Json(new { flag = "HasUrl" });
            }
            entity.URL = url;

            int row = HospitalWebsite.Actor.Insert(entity);
            if (row > 0)
            {
                return Json(new { flag = "success" });
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 编辑医院网站
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult HospitalWebsiteEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CS_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            ViewData["allsection"] = CurrentUser.UserSectionList;
            var Websitelist = HospitalWebsite.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
            bool result = HospitalWebsite.GetHospitalWebsiteAuthority(Websitelist, id);
            if (result == true)
            {
                HospitalWebsite entity = HospitalWebsite.Actor.GetByID(id);
                return View(entity);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 编辑医院网站
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult HospitalWebsiteEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CS_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                HospitalWebsite entity = HospitalWebsite.Actor.GetByID(id);
                if (entity != null)
                {
                    string url = GetFormString(form, "URL");
                    HospitalWebsite existEntity = HospitalWebsite.Actor.GetByURL(url.ToLower());
                    if (existEntity != null && existEntity.ID != id)
                    {
                        return Json(new { flag = "HasUrl" });
                    }
                    entity.Title = url;
                    entity.URL = url;
                    entity.SiteType = GetFormInt(form, "source", -1);
                    entity.SectionID = GetFormInt(form, "sectionid", -1);
                    HospitalWebsite.Actor.Update(entity);
                    return Json(new { flag = "success" });
                }
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 删除医院网站
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult HospitalWebsiteDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CS_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
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
                        var Websitelist = HospitalWebsite.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
                        bool result = HospitalWebsite.GetHospitalWebsiteAuthority(Websitelist, id);
                        if (result == true)
                        {
                            HospitalWebsite.Actor.Delete(id);
                        }
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回所有网站网址
        /// </summary>
        /// <param name="hospitalid"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult GetAllHospitalWebsiteData()
        {
            string sectionID = RequestUtility.GetQueryString("sectionID");
            string[] sectionidArr = StringUtility.Split(sectionID, ',');
            IList<HospitalWebsite> newIst = new List<HospitalWebsite>();
            foreach (string id in sectionidArr)
            {
                IList<HospitalWebsite> ist = HospitalWebsite.Actor.GetAllHospitalWebsite(int.Parse(id), 0);
                foreach (HospitalWebsite hospital in ist)
                {
                    newIst.Add(hospital);
                }
            }
            return Json(newIst);
        }



        #endregion

        #region Section

        /// <summary>
        /// 科室添加
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult SectionIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.C_SECTION))
            {
                LoginManager.GoUnAccessPage();
            }
            IList<Section> ListSection = Section.Actor.GetByHospitalID(CurrentUser.HospitalID);
            HospitalToServe hospitalSection = HospitalToServe.Actor.GetByHospitalID(CurrentUser.HospitalID, ServeConst.KEY_SECTION);
            int num = 0;
            if (hospitalSection != null)
            {
                num = hospitalSection.Num - ListSection.Count;
            }
            ViewBag.SectionNum = num;
            return View();
        }

        public ActionResult GetSectionListData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.C_SECTION))
            {
                LoginManager.GoUnAccessPage();
            }

            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;

            if (CurrentUser.HospitalID <= 0)
            {
                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string>(),
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string keywords = jQueryDataTablesRequestData.Search;
                string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
                bool isDesc = jQueryDataTablesRequestData.IsDesc;
                int pageCount = 0;
                string where = "IsDelete=0 AND HospitalID=" + CurrentUser.HospitalID + " ";
                if (keywords != "")
                {
                    where += " and  Name  LIKE '%" + keywords + "%'";
                }

                IList<Section> list = Section.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

                IList<dynamic> sourceData = new List<dynamic>();

                foreach (var item in list)
                {
                    string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                    string userEdit = "<a class=\"fa fa-edit\" href=\"javascript:view('" + item.ID + "')\"/></a>";
                    sourceData.Add(new dynamic[] { checkeds, item.Name, item.Intro, userEdit });
                }
                return Json(new
                {
                    sEcho = jQueryDataTablesRequestData.SEcho,
                    iTotalRecords = pageCount,
                    iTotalDisplayRecords = pageCount,
                    aaData = sourceData,
                }, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 添加科室
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        public ActionResult SectionAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.CY_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            return View();
        }

        /// <summary>
        /// 添加科室
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult SectionAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CY_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            IList<Section> ListSection = Section.Actor.GetByHospitalID(CurrentUser.HospitalID);
            HospitalToServe hospitalSection = HospitalToServe.Actor.GetByHospitalID(CurrentUser.HospitalID, ServeConst.KEY_SECTION);
            int num = 0;
            if (hospitalSection != null)
            {
                num = hospitalSection.Num - ListSection.Count;
            }
            ViewBag.SectionNum = num;

            bool insertData = false;
            Section entity = new Section();
            entity.HospitalID = CurrentUser.HospitalID;
            entity.Name = GetFormString(form, "name");
            entity.Intro = GetFormString(form, "Intro");
            int sectionID = Section.Actor.Insert(entity);
            if (sectionID > 0)
            {
                insertData = Section.Actor.InsertUserToSection(CurrentUser.ID, sectionID, false);

            }
            return insertData == true ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }

        /// <summary>
        /// 编辑科室
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult SectionEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CY_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            IList<Section> list = Section.Actor.GetByUserID(CurrentUser.ID, -1, CurrentUser.HospitalID);
            bool result = Section.GetSectionAuthority(list, id);
            if (result == true)
            {
                Section entity = Section.Actor.GetByID(id);
                return View(entity);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 编辑科室
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult SectionEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CY_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                Section entity = Section.Actor.GetByID(id);
                if (entity != null)
                {
                    entity.HospitalID = CurrentUser.HospitalID;
                    entity.Name = GetFormString(form, "name");
                    entity.Intro = GetFormString(form, "intro");
                    Section.Actor.Update(entity);
                    return Json(new { flag = "success" });
                }
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult SectionDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.CY_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
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
                        IList<Section> list = Section.Actor.GetByUserID(CurrentUser.ID, -1, CurrentUser.HospitalID);
                        bool result = Section.GetSectionAuthority(list, id);
                        if (result == true)
                        {
                            Section section = Section.Actor.GetByID(id);
                            //对医生进行软删除0正常，1回收站
                            section.IsDelete = 1;
                            Section.Actor.Update(section);
                        }
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SectionMove()
        {
            LoginManager.GoUnAccessPage();
            ViewBag.IDS = RequestUtility.GetQueryString("ids");
            ViewBag.TYPE = RequestUtility.GetQueryString("type");
            IList<Section> allSection = Section.Actor.GetByUserID(CurrentUser.ID, -1, CurrentUser.HospitalID);
            ViewData["allsection"] = allSection;
            return View();
        }

        [HttpPost]
        public JsonResult SectionMove(FormCollection form)
        {
            LoginManager.GoUnAccessPage();
            int sectionID = GetFormInt(form, "sectionID", 0);
            int type = RequestUtility.GetQueryInt("type", -1);
            string ids = RequestUtility.GetQueryString("ids");
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { flag = "error" });
            }

            string[] paras = ids.Split(',');
            if (paras != null && paras.Length > 0)
            {
                foreach (var item in paras)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (type == 3)
                        {
                            HospitalWebsite entity = HospitalWebsite.Actor.GetByID(int.Parse(item));
                            if (entity != null)
                            {
                                entity.SectionID = sectionID;
                                HospitalWebsite.Actor.Update(entity);
                            }
                        }
                        if (type == 2)
                        {
                            Disease entity = Disease.Actor.GetByID(int.Parse(item));
                            if (entity != null)
                            {
                                entity.SectionID = sectionID;
                                Disease.Actor.Update(entity);
                            }
                        }
                        if (type == 1)
                        {
                            Doctor entity = Doctor.Actor.GetByID(int.Parse(item));
                            if (entity != null)
                            {
                                entity.SectionID = sectionID;
                                Doctor.Actor.Update(entity);
                            }
                        }
                    }
                }

                return Json(new { flag = "success" });
            }

            return Json(new { flag = "error" });
        }
        #endregion

        #region MediaSource
        /// <summary>
        /// 媒体列表
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult MediaSourceIndex(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_MEDIA))
            {
                LoginManager.GoUnAccessPage();
            }
            IList<MediaSource> entitylist = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, 0);
            if (entitylist.Count > 0)
            {
                ViewData["mediaList"] = entitylist;
            }
            else
            {
                MediaSource.Actor.InsertAllMedia(CurrentUser.HospitalID);
                IList<MediaSource> entity = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, 0);
                Orders.Actor.UpdateMediaID(CurrentUser.HospitalID);
                ViewData["mediaList"] = entity;
            }
            return View();
        }

        /// <summary>
        /// 获取媒体数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetMediaSourceListData(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_MEDIA))
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
            string where = " ParentID=" + id + " AND HospitalID=" + CurrentUser.HospitalID + "";

            if (keywords != "")
            {
                where += " and  Name  LIKE '%" + keywords + "%'";
            }

            IList<MediaSource> list = MediaSource.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                sourceData.Add(new dynamic[] { item.ID, item.ID, item.Name, item.ID });
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
        /// 添加媒体
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        public ActionResult MediaSourceAdd(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DM_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            ViewData["parentid"] = id;
            return View();
        }

        /// <summary>
        /// 添加媒体
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult MediaSourceAdd(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DM_ADD))
            {
                LoginManager.GoUnAccessPage();
            }
            MediaSource entity = new MediaSource();
            entity.ParentID = id;
            entity.Name = GetFormString(form, "name");
            entity.HospitalID = CurrentUser.HospitalID;
            int row = MediaSource.Actor.Insert(entity);
            if (row > 0)
            {
                return Json(new { flag = "success" });
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 返回整张媒体表集合
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult GetAllMediaSourceData()
        {
            IList<MediaSource> list = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID);
            return Json(list);
        }

        /// <summary>
        /// 编辑媒体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult MediaSourceEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DM_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            MediaSource entity = MediaSource.Actor.GetByID(id);
            return View(entity);
        }

        /// <summary>
        /// 编辑媒体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult MediaSourceEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DM_EDIT))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                MediaSource entity = MediaSource.Actor.GetByID(id);
                if (entity != null)
                {
                    entity.Name = GetFormString(form, "name");
                    MediaSource.Actor.Update(entity);
                    return Json(new { flag = "success" });
                }
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 删除媒体
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult MediaSourceDelete(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.DM_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id < 0)
            {
                return Json(new { flag = "error" });
            }
            else
            {
                MediaSource.Actor.Delete(id);

                MediaSource.Actor.DeleteParentID(id);
                return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);


            }

        }
        /// <summary>
        /// 通过parentid返回整张媒体表集合
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult GetAllMediaSourceDataByParentid(int id)
        {
            IList<MediaSource> list = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, id);
            return Json(list);
        }

        #endregion
        #region MediaSource
        public ActionResult SetHospitalPassWord()
        {
            if (!LoginManager.HasAutority(AuthorityConst.MiMaBaoHu))
            {
                LoginManager.GoUnAccessPage();
            }

            Hospital hospital = CurrentUser.Hospital;
            return View(hospital);
        }
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult SetHospitalPassWord(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.MiMaBaoHu))
            {
                LoginManager.GoUnAccessPage();
            }
            try
            {
                Hospital hospital = CurrentUser.Hospital;
                if (!string.IsNullOrEmpty(GetFormString(form, "password")))
                {
                    hospital.PassWord = SecurityUtility.DESEncrypt(GetFormString(form, "password"));
                }
                hospital.IsOpenPassWord = GetFormString(form, "openSafe") == "true" ? 1 : 0;
                Easom.Core.Hospital.Actor.Update(hospital);
                return Json(new { flag = "success" });
            }
            catch (Exception)
            {
                return Json(new { flag = "error" });
            }
        }
        #endregion
    }
}
