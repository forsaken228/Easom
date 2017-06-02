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
using System.Text.RegularExpressions;
using Easom.Message;

namespace Easom.Support.Controllers
{

    public class MessageController : SysAdminBaseController
    {
        string desCode = WebConfigUtility.GetAppSetting("encrypt");
        public ActionResult TemplateIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_TEMPLATE))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public ActionResult TemplateAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.EE_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            return View();
        }

        /// <summary>
        /// 添加短信模板
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult TemplateAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.EE_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            MessageTemplate entity = new MessageTemplate();
            entity.Content = GetFormString(form, "content");
            entity.Name = GetFormString(form, "name");
            entity.CreateUserID = CurrentUser.ID;
            int row = MessageTemplate.Actor.Insert(entity);
            if (row > 0)
            {
                return Json(new { flag = "success" });
            }
            return Json(new { flag = "error" });
        }

        public ActionResult TemplateEdit()
        {
            if (!LoginManager.HasAutority(AuthorityConst.EE_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            int id = RequestUtility.GetQueryInt("id", -1);
            if (id > 0)
            {
                MessageTemplate messageTemplate = MessageTemplate.Actor.GetByID(id);
                return View(messageTemplate);
            }
            return View();
        }

        /// <summary>
        /// 编辑短信模板
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult TemplateEdit(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.EE_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            int id = RequestUtility.GetQueryInt("id", -1);
            MessageTemplate entity = MessageTemplate.Actor.GetByID(id);
            if (entity != null)
            {
                entity.Content = GetFormString(form, "content");
                entity.Name = GetFormString(form, "name");
                MessageTemplate.Actor.Update(entity);
                return Json(new { flag = "success" });
            }
            return Json(new { flag = "error" });
        }

        [AuthorityActionFilter]
        public JsonResult TemplateDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.EE_DELETE))
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
                        MessageTemplate messageTemplate = MessageTemplate.Actor.GetByID(id);
                        if (messageTemplate.State == 0)
                        {
                            MessageTemplate.Actor.Delete(id);
                        }
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTemplatelListData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_TEMPLATE))
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
            string where = "CreateUserID=" + CurrentUser.ID;
            IList<MessageTemplate> list = MessageTemplate.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                StringBuilder todo = new StringBuilder();
                string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                todo.AppendFormat("<span><a title='编辑'  class='fa fa-edit' href='javascript:edit({0})'></a></span>&nbsp;&nbsp;", item.ID);
                sourceData.Add(new dynamic[] { checkeds, item.Name, item.Content, todo.ToString() });
            }

            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData,
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SingleSend()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_SEND))
            {
                LoginManager.GoUnAccessPagePop();
            }
            int pageCount = 0;
            string type = RequestUtility.GetQueryString("type");
            int id = RequestUtility.GetQueryInt("id", -1);
            if (id > 0)
            {
                Orders entity = Orders.Actor.GetByID(id);
                string tel = entity.Telephone;
                ViewData["tel"] = tel;

                //state=1 代表是系统默认模板，
                string where = "CreateUserID=" + CurrentUser.ID;
                IList<MessageTemplate> list = MessageTemplate.Actor.Search(out pageCount, 1, -1, where, "ID", true);
                ViewData["List"] = list;
                ViewData["templates"] = type;
                ViewData["id"] = id;
                if (CurrentUser.Hospital != null)
                {
                    ViewData["HospitalName"] = CurrentUser.Hospital.Name;
                }
                else
                {
                    ViewData["HospitalName"] = "错误！没有读取到任何医院";
                }
                return View(entity);
            }
            else
            {
                return Redirect("/error/UnAccessPop");
            }
        }

        public ActionResult MessageSend()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_SEND))
            {
                LoginManager.GoUnAccessPagePop();
            }
            int pageCount = 0;
            //state=1 代表是系统默认模板，
            string where = "CreateUserID=" + CurrentUser.ID + "or state=1";
            IList<MessageTemplate> list = MessageTemplate.Actor.Search(out pageCount, 1, -1, where, "ID", false);
            ViewData["List"] = list;
            return View();
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult MessageSend(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_SEND))
            {
                LoginManager.GoUnAccessPage();
            }
            HospitalToServe hospitalToServe = HospitalToServe.Actor.GetByHospitalID(CurrentUser.Hospital.ID, "KEY_NOTE");
            if (hospitalToServe != null && hospitalToServe.Num > 0)
            {
                string contentStr = GetFormString(form, "content");
                string mobiles = GetFormString(form, "mobile");
                mobiles = SecurityUtility.DESDecrypt(mobiles, desCode);
                StringBuilder messageContent = new StringBuilder(contentStr);
                int id = RequestUtility.GetQueryInt("id", -1);
                Orders orders = Orders.Actor.GetByID(id);
                if (orders != null)
                {
                    //对短信模板进行替换
                    messageContent.Replace("#姓名#", orders.Name);
                    messageContent.Replace("#医院名称#", CurrentUser.Hospital.Name);
                    messageContent.Replace("#预约时间#", orders.OrderTime.ToString("yyyy-mm-dd"));
                    messageContent.Replace("#专家号#", orders.ExpertIdentifier);

                }
                messageContent.Append("【" + CurrentUser.Hospital.Name + "】");
                string content = messageContent.ToString();
                MessageLog entity = new MessageLog
                {
                    SendDate = DateTime.Now,
                    SendContent = content,
                    UserID = CurrentUser.ID,
                    SectionID = orders != null ? orders.SectionID : 0,
                    HospitalID = CurrentUser.HospitalID,
                    ToNumber = mobiles,
                    SendState = (int)MessageLastRunStateEnum.Error
                };

                //批量发送
                mobiles = mobiles.Replace("，", ",");
                if (mobiles.IndexOf(",") != -1)
                {
                    string[] mobileArr = mobiles.Split(',');
                    if (mobileArr != null && mobileArr.Length > 0)
                    {
                        foreach (var mobile in mobileArr)
                        {
                            if (!string.IsNullOrEmpty(mobile))
                            {
                                string pattern = @"^1\d{10}$";
                                if (Regex.IsMatch(mobile, pattern))
                                {
                                    //todo添加发送信息操作
                                    int returndata = Notify.Actor.HttpNotifyBatchSend(mobile, content, null, null);
                                    //int returndata = 1;
                                    if (returndata >= 0)
                                    {

                                        entity.SendState = (int)MessageLastRunStateEnum.Success;
                                        MessageLog.Actor.Insert(entity);
                                    }
                                }
                                else
                                {
                                    entity.SendState = (int)MessageLastRunStateEnum.Error;
                                    MessageLog.Actor.Insert(entity);
                                }
                            }
                        }
                    }

                    return Json(new { flag = "success" });
                }
                else
                {
                    string pattern = @"^1\d{10}$";
                    //todo添加发送信息操作
                    if (Regex.IsMatch(mobiles, pattern))
                    {
                        int returndata = Notify.Actor.HttpNotifyBatchSend(mobiles, content, null, null);
                        //int returndata = 1;
                        if (returndata >= 0)
                        {

                            hospitalToServe.Num = hospitalToServe.Num - 1;
                            HospitalToServe.Actor.Update(hospitalToServe);
                            entity.SendState = (int)MessageLastRunStateEnum.Success;
                            int i = MessageLog.Actor.Insert(entity);
                            if (i > 0)
                            {
                                return Json(new { flag = "success" });
                            }
                            else
                            {
                                entity.SendState = (int)MessageLastRunStateEnum.Error;
                                MessageLog.Actor.Insert(entity);
                                return Json(new { flag = "error" });
                            }
                        }
                    }
                    else
                    {
                        return Json(new { flag = "error2" });
                    }
                    return Json(new { flag = "error" });
                }
            }
            else
            {
                return Json(new { flag = "error4" });
            }
        }


        public ActionResult Messagelog()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_MESSAGELOG))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public ActionResult GetMessageLoglListData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_MESSAGELOG))
            {
                LoginManager.GoUnAccessPage();
            }

            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);

            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "SendDate";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;

            string where = "1=1";
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            string sectionStr = "(";
            foreach (var item in sectionlists)
            {
                sectionStr += item.ID + ",";
            }
            sectionStr += "-1500)";
            string usersql = string.Format("SectionID in {0}", sectionStr);
            where += string.Format("AND {0}", usersql);
            IList<MessageLog> messagelogs = new List<MessageLog>();
            IList<MessageLog> list = MessageLog.Actor.Search(out pageCount, pageIndex, pageSize, where, "SendDate", true).ToList();
            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                string para4 = (MessageLastRunStateEnum)item.SendState == MessageLastRunStateEnum.Success ? "<span class=\"label label-success\">成功</span>" : "<span class=\"label label-danger\">错误</span>";
                string para5 = "<div class=\"mediaImg\"><img src=\"/files/upload/photo/" + item.UserInfo.PictureURL + "\" /><br/><span>" + item.UserInfo.TrueName+ "</span></div>";
                sourceData.Add(new dynamic[] { para5,item.ToDESNumber, item.SendContent, item.SendDate.ToString("yyyy-MM-dd HH:mm"), para4, item.ID });
            }

            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData,
            }, JsonRequestBehavior.AllowGet);
        }

        [AuthorityActionFilter]
        public JsonResult GetTemplatesByID()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_SEND))
            {
                LoginManager.GoUnAccessPage();
            }

            int id = RequestUtility.GetQueryInt("aid", -1);
            MessageTemplate messageTemplate = MessageTemplate.Actor.GetByID(id);
            if (messageTemplate != null)
            {
                return Json(new { flag = "success", content = messageTemplate.Content }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { flag = "error" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderToday()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_ADDMESSAGE))
            {
                LoginManager.GoUnAccessPage();
            }
            string globalKeyword = RequestUtility.GetQueryString("globalKeyword");
            string type = RequestUtility.GetQueryString("type");
            //0表示 预约，新增（包括预约等待，预约已到）
            string arriveState = "0";
            //本日新增预约
            string startTime = DateTime.Now.ToString("yyyy-MM-dd") + "  00:00:00.000";
            string endTime = DateTime.Now.ToString("yyyy-MM-dd") + "  23:59:59.999";
            string timeKinds = "0";

            string section = RequestUtility.GetQueryString("section");
            string doctor = RequestUtility.GetQueryString("doctor");
            string diseaseKind = RequestUtility.GetQueryString("diseasekind");
            string mediaSource = RequestUtility.GetQueryString("mediasource");
            //创建人
            string keys = RequestUtility.GetQueryString("keys");
            string customer = RequestUtility.GetQueryString("userInfo");
            string department = RequestUtility.GetQueryString("department");

            string repeatStartTime = RequestUtility.GetQueryString("repeatstarttime");
            string repeatEndTime = RequestUtility.GetQueryString("repeatendtime");
            string repeatSearchData = RequestUtility.GetQueryString("repeatsearchdata");

            Dictionary<string, string> searchDatas = new Dictionary<string, string>();
            searchDatas.Add("keys", keys);
            searchDatas.Add("globalKeyword", globalKeyword);
            searchDatas.Add("timekinds", timeKinds);
            if (!string.IsNullOrEmpty(timeKinds))
            {
                if (Convert.ToInt32(timeKinds) != -1)
                {
                    searchDatas.Add("starttime", startTime);
                    searchDatas.Add("endtime", endTime);
                }

            }
            searchDatas.Add("customer", customer.ToString());
            searchDatas.Add("doctor", doctor);
            searchDatas.Add("arrivestate", arriveState);
            searchDatas.Add("diseasekind", diseaseKind);
            searchDatas.Add("department", department);
            searchDatas.Add("mediasource", mediaSource);
            searchDatas.Add("section", section);
            searchDatas.Add("repeatstarttime", repeatStartTime);
            searchDatas.Add("repeatendtime", repeatEndTime);
            searchDatas.Add("repeatsearchdata", repeatSearchData);
            ViewBag.SearchDatas = GetSearchDataString(searchDatas);
            ViewBag.type = "today";
            ViewBag.Title = "本日新增预约";
            return View();
        }

        public ActionResult OrderTomarrow()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_TOMORRW))
            {
                LoginManager.GoUnAccessPage();
            }
            string globalKeyword = RequestUtility.GetQueryString("globalKeyword");
            string type = RequestUtility.GetQueryString("type");
            //0表示 预约，新增（包括预约等待，预约已到）
            string arriveState = "0";

            //明日预到患者
            string startTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "  00:00:00.000";
            string endTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "  23:59:59.999";
            string timeKinds = "2";

            string section = RequestUtility.GetQueryString("section");
            string doctor = RequestUtility.GetQueryString("doctor");
            string diseaseKind = RequestUtility.GetQueryString("diseasekind");
            string mediaSource = RequestUtility.GetQueryString("mediasource");
            //创建人
            string keys = RequestUtility.GetQueryString("keys");
            string customer = RequestUtility.GetQueryString("userInfo");
            string department = RequestUtility.GetQueryString("department");

            string repeatStartTime = RequestUtility.GetQueryString("repeatstarttime");
            string repeatEndTime = RequestUtility.GetQueryString("repeatendtime");
            string repeatSearchData = RequestUtility.GetQueryString("repeatsearchdata");

            Dictionary<string, string> searchDatas = new Dictionary<string, string>();
            searchDatas.Add("keys", keys);
            searchDatas.Add("globalKeyword", globalKeyword);
            searchDatas.Add("timekinds", timeKinds);
            if (!string.IsNullOrEmpty(timeKinds))
            {
                if (Convert.ToInt32(timeKinds) != -1)
                {
                    searchDatas.Add("starttime", startTime);
                    searchDatas.Add("endtime", endTime);
                }

            }
            searchDatas.Add("customer", customer.ToString());
            searchDatas.Add("doctor", doctor);
            searchDatas.Add("arrivestate", arriveState);
            searchDatas.Add("diseasekind", diseaseKind);
            searchDatas.Add("department", department);
            searchDatas.Add("mediasource", mediaSource);
            searchDatas.Add("section", section);
            searchDatas.Add("repeatstarttime", repeatStartTime);
            searchDatas.Add("repeatendtime", repeatEndTime);
            searchDatas.Add("repeatsearchdata", repeatSearchData);
            ViewBag.SearchDatas = GetSearchDataString(searchDatas);
            ViewBag.type = "tomarrow";
            ViewBag.Title = "明日预到患者";
            return View();
        }

        public ActionResult GetlistOrderData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.E_TOMORRW) || !LoginManager.HasAutority(AuthorityConst.E_ADDMESSAGE))
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
            string where = " IsDelete=0 ";
            string repeat = string.Empty;
            string keys = RequestUtility.GetQueryString("keys");
            string globalKeyword = RequestUtility.GetQueryString("globalKeyword");

            if (string.IsNullOrEmpty(keywords))
            {
                keywords = globalKeyword;
            }


            string timeKinds = RequestUtility.GetQueryString("timekinds");
            string startTime = RequestUtility.GetQueryString("starttime");
            string endTime = RequestUtility.GetQueryString("endtime");
            string section = RequestUtility.GetQueryString("section");
            string doctor = RequestUtility.GetQueryString("doctor");
            string diseaseKind = RequestUtility.GetQueryString("diseasekind");
            string arriveState = RequestUtility.GetQueryString("arrivestate");
            string mediaSource = RequestUtility.GetQueryString("mediasource");
            string customer = RequestUtility.GetQueryString("customer");//创建人
            string department = RequestUtility.GetQueryString("department");

            string repeatStartTime = RequestUtility.GetQueryString("repeatstarttime");
            string repeatEndTime = RequestUtility.GetQueryString("repeatendtime");

            //string reciveDoctor = RequestUtility.GetQueryString("recivedoctor");
            //string selectStartTime = RequestUtility.GetQueryString("selectstarttime");
            //string selectEndTime = RequestUtility.GetQueryString("selectendtime");

            int repeatSearchData = RequestUtility.GetQueryInt("repeatsearchdata", -1);
            if (CurrentUser != null)
            {
                where += " AND HospitalID=" + CurrentUser.HospitalID + " AND OrderState=" + (int)OrderStateEnum.OrdersData + " ";
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                keywords = keywords.Trim();
                where += " AND (Name LIKE '" + keywords + "%' OR Telephone LIKE '%" + keywords + "'" + " OR charindex('" + keywords + "',ExpertIdentifier)>0)";
            }
            if (!string.IsNullOrEmpty(timeKinds))
            {
                if (Convert.ToInt32(timeKinds) == 0)
                {
                    if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                    {
                        where += " AND AddTime >= '" + startTime + "' AND AddTime < '" + endTime + "' ";
                    }
                    if (!string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                    {
                        where += " AND AddTime >= '" + startTime + "'";
                    }
                    if (string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                    {
                        where += " AND AddTime < '" + endTime + "'";
                    }

                }
                if (Convert.ToInt32(timeKinds) == 1)
                {
                    if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                    {
                        where += " AND RecordTime >= '" + startTime + "'  AND  RecordTime < '" + endTime + "' ";
                    }
                    if (!string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                    {
                        where += " AND RecordTime > '" + startTime + "'";
                    }
                    if (string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                    {
                        where += " AND RecordTime < '" + endTime + "'";
                    }
                }
                if (Convert.ToInt32(timeKinds) == 2)
                {
                    if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                    {
                        where += " AND OrderTime >= '" + startTime + "'  AND  OrderTime < '" + endTime + "' ";
                    }
                    if (!string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                    {
                        where += " AND OrderTime > '" + startTime + "'";
                    }
                    if (string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                    {
                        where += " AND OrderTime < '" + endTime + "'";
                    }

                }
            }
            if (!string.IsNullOrEmpty(keys))
            {
                where += " AND KeyWords like '%" + keys + "%'";
            }

            if (!string.IsNullOrEmpty(customer))
            {
                if (ConvertUtility.ToInt32(customer, -1) > -1)
                {
                    where += " AND CreateUserID=" + customer;
                }
            }
            if (!string.IsNullOrEmpty(doctor))
            {
                if (ConvertUtility.ToInt32(doctor, -1) > -1)
                {
                    where += " AND DoctorID=" + doctor;
                }
            }

            if (!string.IsNullOrEmpty(arriveState))
            {
                if (ConvertUtility.ToInt32(arriveState, -1) > -1)
                {
                    where += " AND ArriveStateType=" + arriveState;
                }
            }

            if (!string.IsNullOrEmpty(diseaseKind))
            {
                if (ConvertUtility.ToInt32(diseaseKind, -1) > -1)
                {
                    where += " AND DiseaseID=" + diseaseKind;
                }
            }

            if (!string.IsNullOrEmpty(department))
            {
                if (ConvertUtility.ToInt32(department, -1) > -1)
                {
                    where += " AND CountType=" + department;
                }
            }

            if (!string.IsNullOrEmpty(mediaSource))
            {
                if (ConvertUtility.ToInt32(mediaSource, -1) > -1)
                {
                    where += " AND MediaSourceID=" + mediaSource;
                }
            }

            if (!string.IsNullOrEmpty(repeatStartTime) && !string.IsNullOrEmpty(repeatEndTime))
            {
                repeat = "OrderTime >= '" + repeatStartTime + " 00:00:00.000" + "' AND OrderTime < '" + repeatEndTime + " 23:59:56.999" + "'";
            }

            if (!string.IsNullOrEmpty(repeatStartTime) && string.IsNullOrEmpty(repeatEndTime))
            {
                repeat = "OrderTime > '" + repeatStartTime + " 00:00:00.000" + "'";
            }

            if (string.IsNullOrEmpty(repeatStartTime) && !string.IsNullOrEmpty(repeatEndTime))
            {
                repeat = "OrderTime < '" + repeatEndTime + " 23:59:56.999" + "'";
            }

            if (repeatSearchData == 1)
            {
                where += " AND Name in ( select Name from Orders where " + repeat + " group by Name having COUNT(Name)>1) AND " + repeat;
            }

            if (repeatSearchData == 0)
            {
                where += " AND Telephone in ( select Telephone from Orders where " + repeat + " group by Telephone having COUNT(Telephone)>1) AND " + repeat;
            }
            if (string.IsNullOrEmpty(section))
            {
                where += " AND (";
                IList<Section> sectionLst = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
                foreach (Section sItem in sectionLst)
                {
                    where += " SectionID=" + sItem.ID + " or ";
                }
                where += " SectionID=-100 )";
            }
            else
            {
                string[] sectionLst = StringUtility.Split(section, ',');
                where += " AND (";
                foreach (string sItem in sectionLst)
                {
                    where += " SectionID=" + sItem + " or ";
                }
                where += " SectionID=-100 )";
            }
            IList<dynamic> orderList = null;
            orderList = OrderToShow(out pageCount, pageIndex, pageSize, where, orderFields, isDesc, CurrentUser);

            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = orderList
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 预约页面显示
        /// </summary>
        /// <param name="pageCount">总条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">一页显示数据条数</param>
        /// <param name="where">查询语句</param>
        /// <param name="orderFields">排序字段</param>
        /// <param name="isDesc">是否逆序</param>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public static IList<dynamic> OrderToShow(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc, UserInfo user)
        {
            pageCount = 0;
            IList<dynamic> sourceData = new List<dynamic>();
            if (user != null)
            {
                IList<Orders> entityList = Orders.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
                //var entityList = orders.ToList().Where(i => i.IsSendMessage ==false).ToList();
                //pageCount = entityList.Count;
                if (entityList != null && entityList.Count > 0)
                {
                    foreach (var item in entityList)
                    {
                        string baseInfo = string.Format("<a style='color:{0};' href='javascript:chatcontent({1})'>姓名: {2}-{3}岁-{4}</a><br />",
                            item.Sex ? "#008000" : "#ff5400",
                            item.ID,
                            item.Name,
                            item.Age,
                            item.Sex ? "男" : "女");

                        if (!string.IsNullOrEmpty(item.Telephone))
                        {
                            baseInfo += "手机:&nbsp;" + item.Telephone + "<br />";
                        }
                        if (!string.IsNullOrEmpty(item.QQ))
                        {
                            baseInfo += "Q Q:&nbsp;" + item.QQ + "<br />";
                        }
                        if (item.AreaSourceType >= 0)
                        {
                            if (item.AreaSourceType == AreaTypeEnum.native)
                            {
                                baseInfo += "地区:&nbsp;本地" + "<br />";
                            }
                            if (item.AreaSourceType == AreaTypeEnum.nonlocal)
                            {
                                if (item.Area.Length < 2)
                                {
                                    baseInfo += "地区:&nbsp;外地" + "<br />";
                                }
                                else
                                {
                                    baseInfo += "地区:&nbsp;外地-" + item.Area + "<br />";
                                }
                            }
                        }
                        string reservationMsg = string.Format("<b>专家号：{0}</b><br />疾病：{1}{2}",
                                item.ExpertIdentifier,
                                item.Disease != null ? item.Disease.Name : "没有疾病",
                                item.DoctorID <= 0 ? string.Empty : "<br/>" + ("接待：" + item.Doctor.Name));

                        string remarkMsg = string.IsNullOrEmpty(item.Question) ? "" : (@"咨&nbsp;&nbsp;&nbsp;询：" + item.Question + "<br />");
                        remarkMsg += string.IsNullOrEmpty(item.Remark) ? "" : ("备&nbsp;&nbsp;&nbsp;注：" + item.Remark + "<br />");
                        remarkMsg += item.LastCallOnDada == null ? "" : ("回&nbsp;&nbsp;&nbsp;访：" + item.LastCallOnDada.Remark + "<span style='color:blue'>(" + item.LastCallOnDada.CallOnDateTime.ToString("yyyy-MM-dd HH:mm") + "&nbsp;by:&nbsp;" + item.LastCallOnDada.CallUserName + ")</span><br />");
                        if (item.MediaEntity != null)
                        {
                            remarkMsg += string.IsNullOrEmpty(item.MediaEntity.Name) ? "" : ("来&nbsp;&nbsp;&nbsp;源：" + item.MediaEntity.Name + "");
                            if (item.MediaSourceExtendID > 0)
                            {
                                MediaSource entitys = MediaSource.Actor.GetByID(item.MediaSourceExtendID);
                                if (entitys != null)
                                {
                                    remarkMsg += "-" + entitys.Name + "<br/>";
                                }
                            }
                        }
                        remarkMsg += string.IsNullOrEmpty(item.KeyWords) ? "" : ("关键字：" + item.KeyWords);
                        string para2 = item.IsSendMessage != null ? "<br><span class='label l-success'><a style='color:white' title='" + item.IsSendMessage.SendDate + "'>已通知</a></span>" : "";
                        string para3 = item.OrderTime.ToString("yyyy-MM-dd HH:mm") + (item.ArriveStateType == ArriveStateEnum.Arrived ? "<br/><font title='到院时间' style='color:#008000'>" + item.RecordTime.ToString("yyyy-MM-dd HH:mm") + "</font>" : "");
                        string para4 = Convert.ToInt32(item.ArriveStateType) == 0 ? "<span class='label l-pending'>等待</span" : Convert.ToInt32(item.ArriveStateType) == 2 ? "<span class='label l-suspend'>未到</span>" : "  <span class='label l-success'>已到</span>";
                        string para5 = "部&nbsp;&nbsp;&nbsp;门：" + (item.CountType == CountTypeEnum.Telephone ? "电话" : (item.CountType == CountTypeEnum.Web ? "网络" : "其他")) + "<br />";
                        para5 += "科&nbsp;&nbsp;&nbsp;室：" + item.SectionName.Name + "<br/>";
                        para5 += "创建人：" + item.CreateUserName;
                        sourceData.Add(new dynamic[] { baseInfo,
                                                       reservationMsg,
                                                       remarkMsg,
                                                       para3,
                                                       para4,
                                                       para5,
                                                       item.AddTime.ToString("yyyy-MM-dd HH:mm"),
                                                       item.ID,para2 });
                    }
                }
            }

            return sourceData;
        }

        /// <summary>
        /// 获取当前医院的短信余额
        /// </summary>
        /// <returns></returns>
        public int getCurrentHospitalMessageNum()
        {
            HospitalToServe hospitalToServe = HospitalToServe.Actor.GetByHospitalID(CurrentUser.Hospital.ID, "KEY_NOTE");
            return hospitalToServe.Num;
        }
    }
}
