using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Web;
using CHCMS.Utility;
using Easom.Core;
using Easom.Support.App_Start;
using Easom.Core.Support;
using Easom.Support;
using Newtonsoft.Json.Linq;

namespace Easom.Support.Controllers
{
    public class CustomerServiceController : SysAdminBaseController
    {
        #region 预约表操作
        //string desCode = WebConfigUtility.GetAppSetting("encrypt");
        public ActionResult OrderIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_YYGL))
            {
                LoginManager.GoUnAccessPage();
            }
            string globalKeyword = RequestUtility.GetQueryString("globalKeyword");
            string timeKinds = RequestUtility.GetQueryString("timekinds");
            string startTime = RequestUtility.GetQueryString("starttime");
            string endTime = RequestUtility.GetQueryString("endtime");
            string section = RequestUtility.GetQueryString("section");
            string doctor = RequestUtility.GetQueryString("doctor");
            string diseaseKind = RequestUtility.GetQueryString("diseasekind");
            string arriveState = RequestUtility.GetQueryString("arrivestate");
            string mediaSource = RequestUtility.GetQueryString("mediasource");
            string mediasourceextend = RequestUtility.GetQueryString("mediasourceextend");
            string hospitalwebsite = RequestUtility.GetQueryString("hospitalwebsite");
            //创建人
            string keys = RequestUtility.GetQueryString("keys");
            string customer = RequestUtility.GetQueryString("userInfo");
            string department = RequestUtility.GetQueryString("department");
            string repeatStartTime = RequestUtility.GetQueryString("repeatstarttime");
            string repeatEndTime = RequestUtility.GetQueryString("repeatendtime");
            string repeatSearchData = RequestUtility.GetQueryString("repeatsearchdata");

            string callOn = RequestUtility.GetQueryString("uncallOnData");

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
            searchDatas.Add("mediasourceextend", mediasourceextend.ToString());
            searchDatas.Add("hospitalwebsite", hospitalwebsite.ToString());

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
            searchDatas.Add("uncallOnData", callOn);
            string sectionStr = "-100";
            if (CurrentUser.UserSectionList != null)
            {
                foreach (Section sItem in CurrentUser.UserSectionList)
                {
                    sectionStr += "," + sItem.ID + "";
                }
            }
            int adddate = 0;
            int orderdata = 0;
            int arrivedata = 0;
            Orders.Actor.GetIndexDataByUserID(out adddate, out orderdata, out arrivedata, CurrentUser.ID, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000")), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999")), sectionStr, -1);
            ViewBag.add = adddate;
            ViewBag.arrive = arrivedata;
            ViewBag.order = orderdata;
            ViewBag.SearchDatas = GetNewSearchDataString(searchDatas);
            return View();
        }
        public ActionResult GetlistOrderData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_YYGL))
            {
                LoginManager.GoUnAccessPage();
            }
            Helper.DataTablesRequest jQueryDataTablesRequestData;
            int pageSize, pageIndex, pageCount;
            string orderFields, where;
            bool isDesc;
            IList<dynamic> orderList;
            GetParameter(out jQueryDataTablesRequestData, out pageSize, out pageIndex, out orderFields, out isDesc, out pageCount, out where, out orderList);
            orderList = OrderToShow(out pageCount, pageIndex, pageSize, where, orderFields, isDesc, CurrentUser);
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = orderList
            }, JsonRequestBehavior.AllowGet);
        }
        private void GetParameter(out Helper.DataTablesRequest jQueryDataTablesRequestData, out int pageSize, out int pageIndex, out string orderFields, out bool isDesc, out int pageCount, out string where, out IList<dynamic> orderList)
        {
            #region
            jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            pageSize = jQueryDataTablesRequestData.PageSize;
            pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            isDesc = jQueryDataTablesRequestData.IsDesc;
            pageCount = 0;
            where = " IsDelete=0 ";
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
            string callOn = RequestUtility.GetQueryString("uncallOnData");

            string mediasourceextend = RequestUtility.GetQueryString("mediasourceextend");
            string hospitalwebsite = RequestUtility.GetQueryString("hospitalwebsite");

            int repeatSearchData = RequestUtility.GetQueryInt("repeatsearchdata", -1);
            try
            {
                if (CurrentUser != null)
                {
                    where += " AND HospitalID=" + CurrentUser.HospitalID + " AND OrderState=" + (int)OrderStateEnum.OrdersData + " ";
                }
                if (!string.IsNullOrEmpty(keywords))
                {
                    keywords = keywords.Trim();
                    where += " AND (Name LIKE '%" + keywords + "%' OR Telephone LIKE '%" + keywords + "%'" + " OR ExpertIdentifier LIKE '%" + keywords + "%')";
                }
                if (!string.IsNullOrEmpty(timeKinds))
                {
                    if (Convert.ToInt32(timeKinds) == 0)
                    {
                        if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                        {
                            where += " AND AddTime >= '" + Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00.000") + "' AND AddTime < '" + Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:56.000") + "' ";
                        }
                        if (!string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                        {
                            where += " AND AddTime >= '" + Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00.000") + "'";
                        }
                        if (string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                        {
                            where += " AND AddTime < '" + Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:56.000") + "'";
                        }
                    }
                    if (Convert.ToInt32(timeKinds) == 1)
                    {
                        if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                        {
                            where += " AND RecordTime >= '" + Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00.000") + "'  AND  RecordTime < '" + Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:56.000") + "' ";
                        }
                        if (!string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                        {
                            where += " AND RecordTime > '" + Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00.000") + "'";
                        }
                        if (string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                        {
                            where += " AND RecordTime < '" + Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:56.000") + "'";
                        }
                    }
                    if (Convert.ToInt32(timeKinds) == 2)
                    {
                        if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                        {
                            where += " AND OrderTime >= '" + Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00.000") + "'  AND  OrderTime < '" + Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:56.000") + "' ";
                        }
                        if (!string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
                        {
                            where += " AND OrderTime > '" + Convert.ToDateTime(startTime).ToString("yyyy-MM-dd 00:00:00.000") + "'";
                        }
                        if (string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                        {
                            where += " AND OrderTime < '" + Convert.ToDateTime(endTime).ToString("yyyy-MM-dd 23:59:56.000") + "'";
                        }
                    }
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
                        if (!string.IsNullOrEmpty(mediasourceextend))
                        {
                            if (ConvertUtility.ToInt32(mediasourceextend, -1) > -1)
                            {
                                where += " AND MediaSourceExtendID=" + mediasourceextend;
                            }
                        }

                        if (!string.IsNullOrEmpty(keys) && ConvertUtility.ToInt32(mediaSource, -1) == MediaSource.Actor.GetByName(CurrentUser.HospitalID, "网络").ID)
                        {
                            where += " AND KeyWords like '%" + keys + "%'";

                        }
                        if (!string.IsNullOrEmpty(hospitalwebsite) && ConvertUtility.ToInt32(hospitalwebsite, -1) > -1 && int.Parse(mediaSource) == MediaSource.Actor.GetByName(CurrentUser.HospitalID, "网络").ID)
                        {
                            where += " AND HospitalWebsiteID =" + hospitalwebsite;
                        }
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
                    where += " AND SectionID in (";
                    IList<Section> sectionLst = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
                    foreach (Section sItem in sectionLst)
                    {
                        where += "" + sItem.ID + ",";
                    }
                    where += "-100 )";
                }
                else
                {
                    string[] sectionLst = StringUtility.Split(section, ',');
                    where += " AND SectionID in (";
                    foreach (string sItem in sectionLst)
                    {
                        where += "" + sItem + ",";
                    }
                    where += "-100 )";
                }

                if (callOn != null && callOn != "")
                {
                    string sectionStr = "";
                    if (CurrentUser.UserSectionList != null)
                    {
                        foreach (Section sItem in CurrentUser.UserSectionList)
                        {
                            sectionStr += "" + sItem.ID + ",";
                        }
                    }
                    sectionStr += "-100";
                    IList<CallOnData> callOnData = CallOnData.Actor.GetByUserIDAndState(CurrentUser.ID, 0, 0, sectionStr);
                    where += " AND ArriveStateType=" + (int)ArriveStateEnum.Wait;
                    where += " AND ID in (";
                    foreach (CallOnData cal in callOnData)
                    {
                        where += "" + cal.OrdersID + " , ";
                    }
                    where += "-100 )";
                }
                if (CurrentUser.UserRole.ManagerRoles != null)
                {
                    where += " AND RoleID in (";
                    foreach (Role roleID in CurrentUser.UserRole.ManagerRoles)
                    {
                        if (roleID != null)
                        {
                            where += "" + roleID.ID + ",";
                        }
                    }
                    where += "-100 )";
                }

            }
            catch (Exception e)
            {
                Log4NetUtility.WriterErrorLog(where + e.ToString());
            }
            orderList = null;
            #endregion
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
        public IList<dynamic> OrderToShow(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc, UserInfo user)
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_YYGL))
            {
                LoginManager.GoUnAccessPage();
            }
            pageCount = 0;
            IList<dynamic> sourceData = new List<dynamic>();
            if (user != null)
            {
                IList<Orders> entityList = Orders.Actor.SearchView(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
                if (entityList != null && entityList.Count > 0)
                {
                    foreach (var item in entityList)
                    {
                        string para2 = item.LastMessage != null ? "<span class='fa fa-smile-o' title='已通知'></span>&nbsp;" : "";
                        string function = "chatcontent";
                        string canshu = "" + item.ID + ",\"" + CurrentUser.Hospital.HSafeCode + "\"";
                        if (CurrentUser.Hospital.IsOpenPassWord == 1)//表示未开启该功能
                        {
                            function = "password";
                            canshu = item.ID.ToString();
                        }
                        string baseInfo = "";
                        if (LoginManager.HasAutority(AuthorityConst.KEY_ORDER_PERSON_MESSAG))
                        {
                            baseInfo = string.Format("<a style='color:{0};' href='javascript:{5}({1})'>{2}-{3}岁-{4}</a>&nbsp;{6}<br />",
                            item.Sex ? "#1CAF9A" : "#ff5400",
                            canshu,
                            item.Name,
                            item.Age,
                            item.Sex ? "男" : "女",
                            function,
                            para2
                            );
                        }
                        else
                        {
                            baseInfo = string.Format("<span style='color:{0};'>{1}-{2}岁-{3}</span><br />",
                         item.Sex ? "#1CAF9A" : "#ff5400",
                         item.Name,
                         item.Age,
                         item.Sex ? "男" : "女");
                        }

                        if (!string.IsNullOrEmpty(item.Telephone))
                        {
                            baseInfo += "<i class=\"fa  fa-mobile-phone\"></i>&nbsp;" + item.DESTelephone + "<br />";
                        }
                        //if (!string.IsNullOrEmpty(item.QQ))
                        //{
                        //    baseInfo += "Q Q:&nbsp;" + item.QQ + "<br />";
                        //}
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
                        string reservationMsg = string.Format("{0}疾病：{1}{2}",
                    item.ExpertIdentifier != null && item.ExpertIdentifier.Trim() != "" ? "专家号：" + item.ExpertIdentifier + "<br />" : "",
                    item.Disease != null ? item.Disease.Name : "没有疾病",
                     item.DoctorID <= 0 ? string.Empty : "<br/>" + ("医生：" + item.Doctor.Name));
                        string remarkMsg = string.Empty;
                        if (item.MediaEntity != null)
                        {
                            remarkMsg += string.IsNullOrEmpty(item.MediaEntity.Name) ? "" : (item.MediaEntity.Name);
                            if (item.MediaSourceExtendID > 0)
                            {
                                MediaSource entitys = MediaSource.Actor.GetByID(item.MediaSourceExtendID);
                                if (entitys != null)
                                {
                                    remarkMsg += "-" + entitys.Name + "";
                                }
                            }
                        }
                        if (item.HospitalWebsite != null)
                        {
                            remarkMsg += string.IsNullOrEmpty(item.HospitalWebsite.URL) ? "" : ("（" + item.HospitalWebsite.URL + "）");
                        }
                        remarkMsg += string.IsNullOrEmpty(item.KeyWords) ? "" : ("<br/>关键字：<span title=" + StringUtility.CutHtmlFormat(item.KeyWords) + ">" + StringUtility.CutString(item.KeyWords, 30, true) + "</span>");
                        var remarkMsg2 = string.IsNullOrEmpty(item.Question) ? "" : (@"咨&nbsp;&nbsp;&nbsp;询：" + StringUtility.CutString(item.Question, 150, true) + "");
                        var remarkMsg3 = string.IsNullOrEmpty(item.Remark) ? "" : ("<span title=" + item.Remark.Replace(" ", "") + ">备&nbsp;&nbsp;&nbsp;注：" + StringUtility.CutString(item.Remark, 150, true) + "</span>");
                        var remarkMsg4 = item.LastCallOnDada == null ? "" : ("<span title=" + StringUtility.CutHtmlFormat(item.Remark) + ">回&nbsp;&nbsp;&nbsp;访：" + StringUtility.CutString(item.LastCallOnDada.Remark, 150, true) + "<span style='color:blue'>(" + item.LastCallOnDada.CallOnDateTime.ToString("yy-MM-dd HH:mm") + "&nbsp;by:&nbsp;" + item.LastCallOnDada.CallUserName + ")</span></span>");

                        string para3 = item.OrderTime.ToString("yyyy-MM-dd HH:mm") + (item.ArriveStateType == ArriveStateEnum.Arrived ? "<br/><font title='到院时间' style='color:#008000'>" + item.RecordTime.ToString("yyyy-MM-dd HH:mm") + "</font>" : "");
                        string para4 = Convert.ToInt32(item.ArriveStateType) == 0 ? "<span class=\"label label-warning\">等待</span>" : Convert.ToInt32(item.ArriveStateType) == 2 ? "<span class=\"label label-danger\">未到</span>" : "  <span class=\"label label-primary\">已到</span>";
                        //string para6 = "" + (item.CountType == CountTypeEnum.Telephone ? "电话" : (item.CountType == CountTypeEnum.Web ? "网络" : "其他")) + "";
                        //para5 += "科&nbsp;&nbsp;&nbsp;室：" + item.SectionName.Name + "<br/>";
                        //para5 += "创建人：" + item.CreateUserName;
                        string para5 = "<div class=\"mediaImg\"><img src=\"/files/upload/photo/" + item.CreateUserPictureURL + "\" /><br/><span>" + item.CreateUserName + "</span></div>";

                        StringBuilder todo = new StringBuilder();

                        if (LoginManager.HasAutority(AuthorityConst.AY_EDIT))
                        {
                            if (LoginManager.HasAutority(AuthorityConst.AY_EDITOTHER))
                            {
                                todo.AppendFormat("<span><a title='编辑'  class='fa fa-edit' href='javascript:edit({0})'></a></span>&nbsp;&nbsp;", item.ID);
                            }
                            else
                            {
                                if (item.CreateUserID == LoginManager.GetUserId())
                                {
                                    todo.AppendFormat("<span><a title='编辑'  class='fa fa-edit' href='javascript:edit({0})'></a></span>&nbsp;&nbsp;", item.ID);
                                }
                            }
                        }

                        if (LoginManager.HasAutority(AuthorityConst.AY_DELETE))
                        {
                            todo.AppendFormat("<span><a title='删除'  class='fa fa-trash-o' href='javascript:delCheck({0})'></a></span>&nbsp;&nbsp;", item.ID);
                        }
                        if (LoginManager.HasAutority(AuthorityConst.AY_STATE))
                        {
                            todo.AppendFormat("<span><a title='来院状态' class='fa fa-ambulance' href='javascript:arrivestate({0})'></a></span>&nbsp;&nbsp;", item.ID);
                        }
                        if (LoginManager.HasAutority(AuthorityConst.AY_CALLBACK))
                        {
                            if ((int)item.ArriveStateType == 0 || (int)item.ArriveStateType == 2)
                            {
                                todo.AppendFormat("<span><a title='回访管理' class='fa fa-phone'  href='javascript:callbackManager({0},0)'> </a></span>&nbsp;&nbsp;", item.ID);
                            }
                        }
                        if (LoginManager.HasAutority(AuthorityConst.E_MESSAGE))
                        {
                            todo.AppendFormat("<span><a title='短信通知' class='glyphicon glyphicon-phone'  href='javascript:sendMessage({0})'></a></span>&nbsp;&nbsp;", item.ID);
                        }
                        if (LoginManager.HasAutority(AuthorityConst.KEY_OPERATION_LOG))
                        {
                            todo.AppendFormat("<span><a title='操作日志' class='fa fa-clipboard'  href='javascript:viewLogs({0})'></a></span>", item.ID);
                        }
                        sourceData.Add(new dynamic[] {
                            "",
                            baseInfo,
                            reservationMsg,
                            remarkMsg,
                            para3,
                            para4,
                            para5,
                            item.AddTime.ToString("yyyy-MM-dd HH:mm"),
                            todo.ToString(),
                            para2,
                            item.CreateUserID,
                            Convert.ToInt32(item.ArriveStateType),
                            remarkMsg2,
                            remarkMsg3,
                            remarkMsg4
                        });
                    }
                }
            }

            return sourceData;
        }

        public ActionResult OrderAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            IList<Section> list = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
            ViewData["sectionList"] = list;
            ViewData["mediaList"] = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, 0);
            return View();
        }

        /// <summary>
        /// 获取科室内所有医生
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDoctorDataBySection()
        {
            //if (!LoginManager.HasAutority(AuthorityConst.AY_ADD) || !LoginManager.HasAutority(AuthorityConst.AY_EDIT))
            //{
            //    LoginManager.GoUnAccessPage();
            //}
            string sectionStr = RequestUtility.GetQueryString("sectionStr");
            int doctorID = RequestUtility.GetQueryInt("doctorID", -1);
            StringBuilder doctorData = new StringBuilder();
            if (!string.IsNullOrEmpty(sectionStr))
            {
                string[] str = StringUtility.Split(sectionStr, ',');
                foreach (string s in str)
                {
                    if (int.Parse(s) > 0)
                    {
                        IList<Doctor> docList = Doctor.Actor.GetBySectionID(int.Parse(s));
                        foreach (Doctor doc in docList)
                        {
                            if (doctorID == doc.ID)
                            {
                                doctorData.Append(string.Format("<span title=\"" + doc.Name + "\" class=\"rdio rdioPosition\"><input name=\"doctorName\"   id=\"{2}\" checked=\"checked\" type=\"radio\" value=\"{0}\" /><label for=\"{2}\">{1}</label><input id=\"{2}\" value=\"{3}\" type=\"hidden\" /></span>&nbsp;", doc.ID, doc.Name, "doctorName" + doc.ID, doc.Identifier));
                            }
                            else
                            {
                                doctorData.Append(string.Format("<span title=\"" + doc.Name + "\" class=\"rdio rdioPosition\"><input name=\"doctorName\"  id=\"{2}\"  type=\"radio\" value=\"{0}\" /><label for=\"{2}\">{1}</label><input id=\"{2}\" value=\"{3}\" type=\"hidden\" /></span>&nbsp;", doc.ID, doc.Name, "doctorName" + doc.ID, doc.Identifier));
                            }
                        }
                    }
                }
                return Json(new { flag = "success", doctorData = doctorData.ToString() });
            }
            return Json(new { flag = "error" });
        }
        /// <summary>
        /// 获取科室内所有疾病
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDiseaseDataBySection()
        {
            //if (!LoginManager.HasAutority(AuthorityConst.AY_ADD) || !LoginManager.HasAutority(AuthorityConst.AY_EDIT))
            //{
            //    LoginManager.GoUnAccessPage();
            //}
            string sectionStr = RequestUtility.GetQueryString("sectionStr");
            int diseaseID = RequestUtility.GetQueryInt("diseaseID", -1);
            StringBuilder diseaseData = new StringBuilder();
            if (!string.IsNullOrEmpty(sectionStr))
            {
                IList<Disease> diseaseList = Disease.Actor.GetDiseaseBySection(sectionStr);
                foreach (Disease dis in diseaseList)
                {
                    if (diseaseID == dis.ID)
                    {
                        diseaseData.Append(string.Format("<span title=\"" + dis.Name + "\" class=\"rdio rdioPosition\"><input name=\"diseaseName\" checked=\"checked\" id=\"{0}\"  type=\"radio\" value=\"{0}\" /><label for=\"{0}\">{1}</label></span>", dis.ID, StringUtility.CutString(dis.Name, 7, true)));
                    }
                    else
                    {
                        diseaseData.Append(string.Format("<span title=\"" + dis.Name + "\" class=\"rdio rdioPosition\"><input name=\"diseaseName\" id=\"{0}\"  type=\"radio\" value=\"{0}\" /><label for=\"{0}\">{1}</label></span>", dis.ID, StringUtility.CutString(dis.Name, 7, true)));
                    }
                }
                return Json(new { flag = "success", diseaseData = diseaseData.ToString() });
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 通过角色和科室获取用户
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserInfoBySectionAndRole()
        {
            string sectionStr = RequestUtility.GetQueryString("sectionStr");
            int roleID = RequestUtility.GetQueryInt("roleID", -1);
            if (!string.IsNullOrEmpty(sectionStr) && roleID > 0)
            {
                StringBuilder userListBuild = new StringBuilder();
                userListBuild.Append(string.Format("<option value=\"{0}\" selected=\"selected\">{1}</option>", -1, "请选择用户"));
                string[] str = StringUtility.Split(sectionStr, ',');
                IList<UserInfo> NewuserList = new List<UserInfo>();
                foreach (string s in str)
                {
                    if (int.Parse(s) > 0)
                    {
                        IList<UserInfo> userList = UserInfo.Actor.GetByRoleAndSection(roleID, int.Parse(s));
                        foreach (UserInfo user in userList)
                        {
                            //foreach (UserInfo users in NewuserList)
                            //{
                            //    if (user.ID != users.ID)
                            //    {
                            NewuserList.Add(user);
                            //    }
                            //}
                        }
                    }
                }
                var userListvalue = NewuserList.GroupBy(p => new { p.ID, p.TrueName }).Select(g => g.First()).ToList();
                foreach (UserInfo user in userListvalue as IList<UserInfo>)
                {
                    userListBuild.Append(string.Format("<option value=\"{0}\">{1}</option>", user.ID, user.TrueName));
                }
                return Json(new { flag = "success", data = userListBuild.ToString() });
            }
            return Json(new { flag = "error" });
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult OrderAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            // string desCode = WebConfigUtility.GetAppSetting("encrypt");
            bool IsInsert = false;
            string tel = GetFormString(form, "tel");
            if (tel != null && tel.Length > 0)
            {
                string sectionStr = "";
                if (CurrentUser.UserSectionList != null)
                {
                    foreach (Section sItem in CurrentUser.UserSectionList)
                    {
                        sectionStr += "" + sItem.ID + ",";
                    }
                }
                sectionStr += "-100";

                Orders orders = Orders.Actor.GetCountByExistsTel(sectionStr, tel, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00.000"), DateTime.Now.ToString("yyyy-MM-dd 23:59:56"));
                if (orders != null)
                {
                    IsInsert = false;
                }
                else
                {
                    IsInsert = true;
                }
            }
            else
            {
                IsInsert = true;
            }
            if (IsInsert == true)
            {
                Orders entity = new Orders();
                entity.Age = GetFormInt(form, "age", 0);
                entity.Name = GetFormString(form, "name");
                entity.AddTime = System.DateTime.Now;
                entity.Area = GetFormString(form, "area");
                entity.AreaSourceType = (AreaTypeEnum)GetFormInt(form, "areasourcetype", -1);
                entity.Question = "";
                entity.QQ = "";
                entity.Telephone = "";
                entity.ChatRecord = "";
                string oldString = GetFormString(form, "chatcontent");
                if (!string.IsNullOrEmpty(oldString))
                {
                    string[] newStrings = oldString.Split('\n');
                    StringBuilder newString = new StringBuilder();
                    foreach (var item in newStrings)
                    {
                        newString.Append(item + "&");
                    }
                    entity.ChatRecord = newString.ToString();//聊天记录加密
                }
                entity.CountType = (CountTypeEnum)GetFormInt(form, "countnumber", 3);
                entity.CreateUserID = CurrentUser.ID;
                entity.DiseaseID = GetFormInt(form, "disease", -1);
                entity.ExpertIdentifier = GetFormString(form, "expertidentifier").ToUpper();
                entity.UpdateTime = System.DateTime.Now;
                entity.Sex = GetFormString(form, "sex") == "0" ? true : false;
                entity.Remark = GetFormString(form, "remark");
                if (!string.IsNullOrEmpty(GetFormString(form, "tel")))
                {
                    try
                    {
                        entity.Telephone = GetFormString(form, "tel");
                    }
                    catch (Exception)
                    {

                        throw;
                    }//电话加密
                }

                if (!string.IsNullOrEmpty(GetFormString(form, "question")))
                {
                    entity.Question = GetFormString(form, "question");//问题加密
                }
                if (!string.IsNullOrEmpty(GetFormString(form, "qq")))
                {
                    entity.QQ = GetFormString(form, "qq");//QQ加密
                }

                string date = GetFormString(form, "ordertime");
                string time = GetFormString(form, "orderhour");
                entity.OrderTime = Convert.ToDateTime(date + " " + time);
                entity.MediaSourceID = GetFormInt(form, "mediasource", -1);
                if (entity.MediaSourceID == MediaSource.Actor.GetByName(CurrentUser.HospitalID, "网络").ID)//只有选择网络以下两个字段才有效
                {
                    entity.HospitalWebsiteID = GetFormInt(form, "hospitalwebsite", -1);
                    entity.KeyWords = GetFormString(form, "keywords");
                }
                else
                {
                    entity.HospitalWebsiteID = -1;
                    entity.KeyWords = "";
                }
                entity.MediaSourceExtendID = GetFormInt(form, "mediaSourceExtend", -1);
                entity.RecordTime = System.DateTime.Now;
                entity.HospitalID = CurrentUser.Hospital.ID;
                entity.SectionID = GetFormInt(form, "section", -1);
                Section sec = Section.Actor.GetByID(entity.SectionID);
                if (sec != null)
                {
                    if (sec.HospitalID != entity.HospitalID)
                    {
                        return Json(new { flag = "error" });//当前科室不是当前医院
                    }
                }
                entity.DoctorID = GetFormInt(form, "doctor", -1);
                entity.OrderState = 0;
                int isSuccess = Orders.Actor.Insert(entity);
                if (isSuccess > 0)
                {
                    entity.ID = isSuccess;
                    CreateOprentionAddOrDeleteLog(entity, OperationTypeEnum.Add);
                }
                return Json(new { flag = "success" });
            }
            else//不等于空说明有重复的，不能提交
            {
                return Json(new { flag = "error2" });
            }
        }

        private static void CreateOprentionAddOrDeleteLog(Orders entity, OperationTypeEnum ote)
        {
            //记录日志
            OperationLog logs = new OperationLog();
            JArray jlogs = new JArray();
            if (!string.IsNullOrEmpty(entity.ExpertIdentifier))
            {
                jlogs.Add(new JObject(
                    new JProperty("Name", "专家号"),
                   new JProperty("Value", entity.ExpertIdentifier)
                   ));
            }
            jlogs.Add(new JObject(
                new JProperty("Name", "姓名"),
                new JProperty("Value", entity.Name)
                ));
            jlogs.Add(new JObject(
                new JProperty("Name", "年龄"),
                new JProperty("Value", entity.Age)
                ));
            jlogs.Add(new JObject(
                new JProperty("Name", "性别"),
                new JProperty("Value", entity.Sex ? "男" : "女")
                ));
            if (!string.IsNullOrEmpty(entity.Telephone))
            {
                jlogs.Add(new JObject(
                new JProperty("Name", "电话"),
                new JProperty("Value", entity.Telephone)
                ));
            }
            if (!string.IsNullOrEmpty(entity.QQ))
            {
                jlogs.Add(new JObject(
                new JProperty("Name", "QQ"),
                new JProperty("Value", entity.QQ)
                ));
            }
            if (!string.IsNullOrEmpty(entity.Question))
            {
                jlogs.Add(new JObject(
                new JProperty("Name", "咨询内容"),
                new JProperty("Value", entity.Question)
                ));
            }
            if (!string.IsNullOrEmpty(entity.ChatRecord))
            {
                jlogs.Add(new JObject(
                    new JProperty("Name", "聊天记录"),
                    new JProperty("Value", "详细信息请查看源头数据")// entity.ChatRecord)
                    ));
            }
            if (!string.IsNullOrEmpty(entity.Remark))
            {
                jlogs.Add(new JObject(
                new JProperty("Name", "备注"),
                new JProperty("Value", entity.Remark)
                ));
            }
            jlogs.Add(new JObject(
                new JProperty("Name", "科室"),
                new JProperty("Value", entity.SectionName.Name)
                ));
            if (entity.Doctor != null)
            {
                jlogs.Add(new JObject(
                new JProperty("Name", "医生"),
                new JProperty("Value", entity.Doctor.Name)
                ));
            }
            if (entity.Disease != null)
            {
                jlogs.Add(new JObject(
                new JProperty("Name", "疾病"),
                new JProperty("Value", entity.Disease.Name)
                ));
            }
            jlogs.Add(new JObject(
              new JProperty("Name", "预约时间"),
              new JProperty("Value", entity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss"))
              ));
            //媒体来源
            if (entity.MediaEntity != null)
            {
                string remarkMsg = string.IsNullOrEmpty(entity.MediaEntity.Name) ? "" : (entity.MediaEntity.Name);
                if (entity.MediaSourceExtendID > 0)
                {
                    MediaSource entitys = MediaSource.Actor.GetByID(entity.MediaSourceExtendID);
                    if (entitys != null)
                    {
                        remarkMsg += "-" + entitys.Name + "";
                    }
                }
                jlogs.Add(new JObject(
            new JProperty("Name", "媒体来源"),
            new JProperty("Value", remarkMsg)
            ));
            }
            if (entity.HospitalWebsite != null)
            {
                jlogs.Add(new JObject(
            new JProperty("Name", "网站"),
            new JProperty("Value", entity.HospitalWebsite.URL)
            ));
            }
            jlogs.Add(new JObject(
          new JProperty("Name", "地区来源"),
          new JProperty("Value", entity.AreaSourceType == AreaTypeEnum.native ? "本地" : "外地:" + entity.Area)
          ));
            string CountMsg = "无";
            if (entity.CountType == CountTypeEnum.Others)
            {
                CountMsg = "其他";
            }
            if (entity.CountType == CountTypeEnum.Telephone)
            {
                CountMsg = "电话";
            }
            if (entity.CountType == CountTypeEnum.Web)
            {
                CountMsg = "网络";
            }
            jlogs.Add(new JObject(
        new JProperty("Name", "统计账户"),
        new JProperty("Value", CountMsg)
        ));
            logs.OperationType = ote;// OperationTypeEnum.Add;
            logs.TableName = "预约信息";
            logs.OperationInfo = jlogs.ToString();
            logs.HospitalID = entity.HospitalID;
            logs.SectionID = entity.SectionID;
            logs.LinkID = entity.ID;
            if (jlogs.Count > 0)
            {
                CreateOperationLogs(logs);
            }
        }

        public ActionResult OrderEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            Orders orders = Orders.Actor.GetByID(id);
            IList<Section> list = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
            ViewData["sectionList"] = list;
            ViewData["mediaList"] = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, 0);
            ViewData["encrypt"] = WebConfigUtility.GetAppSetting("encrypt"); ;
            return View(orders);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult OrderEdit(FormCollection form, int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            if (id > 0)
            {
                string desCode = WebConfigUtility.GetAppSetting("encrypt");
                bool IsUpdate = false;
                Orders entity = Orders.Actor.GetByID(id);
                Orders oldentity = entity.Clone();

                if (entity != null)
                {
                    if (!string.IsNullOrEmpty(GetFormString(form, "tel")) && !GetFormString(form, "tel").ToString().Contains("*"))
                    {
                        if (GetFormString(form, "tel").ToString() != entity.DESTelephone)//如果不相等说明已经修改
                        {
                            string tel = GetFormString(form, "tel");
                            Orders orders = Orders.Actor.GetCountByExistsTel(entity.SectionID.ToString(), tel, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00.000"), DateTime.Now.ToString("yyyy-MM-dd 23:59:56"));
                            if (orders != null)
                            {
                                if (orders.ID == entity.ID)
                                {
                                    IsUpdate = true;
                                }
                                else
                                {
                                    IsUpdate = false;
                                }
                            }
                            else
                            {
                                IsUpdate = true;
                            }
                        }
                        else
                        {
                            IsUpdate = true;
                        }
                    }
                    else
                    {
                        IsUpdate = true;
                    }

                    if (IsUpdate == true)
                    {
                        entity.Age = GetFormInt(form, "age", 0);
                        entity.Name = GetFormString(form, "name");
                        if (entity.OrderState == 1)
                        {
                            entity.AddTime = System.DateTime.Now;
                        }
                        entity.Area = GetFormString(form, "area");
                        entity.AreaSourceType = (AreaTypeEnum)GetFormInt(form, "areasourcetype", -1);

                        entity.CountType = (CountTypeEnum)GetFormInt(form, "countnumber", 3);
                        //entity.CreateUserID = CurrentUser.ID;
                        entity.DiseaseID = GetFormInt(form, "disease", -1);
                        entity.ExpertIdentifier = GetFormString(form, "expertidentifier").ToUpper();
                        entity.UpdateTime = System.DateTime.Now;

                        entity.Sex = GetFormString(form, "sex") == "0" ? true : false;
                        entity.Remark = GetFormString(form, "remark");

                        string oldString = GetFormString(form, "chatcontent");//聊天记录加密
                        if (!string.IsNullOrEmpty(oldString) && !oldString.Contains("*"))
                        {
                            if (oldString != entity.DESChatRecord)//如果不相等说明已经修改
                            {
                                string[] newStrings = oldString.Split('\n');
                                StringBuilder newString = new StringBuilder();
                                foreach (var item in newStrings)
                                {
                                    newString.Append(item + "&");
                                }
                                entity.ChatRecord = newString.ToString();//聊天记录加密
                            }
                        }
                        if (!string.IsNullOrEmpty(GetFormString(form, "tel")) && !GetFormString(form, "tel").Contains("*"))
                        {
                            if (GetFormString(form, "tel").ToString() != entity.DESTelephone)//如果不相等说明已经修改
                            {
                                entity.Telephone = GetFormString(form, "tel");//电话加密
                            }
                        }
                        if (!string.IsNullOrEmpty(GetFormString(form, "question")) && !GetFormString(form, "question").Contains("*"))
                        {
                            if (GetFormString(form, "question").ToString() != entity.Question)//如果不相等说明已经修改
                            {
                                entity.Question = GetFormString(form, "question");//问题加密
                            }
                        }
                        if (!string.IsNullOrEmpty(GetFormString(form, "qq")) && !GetFormString(form, "qq").ToString().Contains("*"))
                        {
                            if (GetFormString(form, "qq").ToString() != entity.QQ)//如果不相等说明已经修改
                            {
                                entity.QQ = GetFormString(form, "qq");//QQ加密
                            }
                        }

                        if (entity.OrderState != (int)Easom.Core.Support.OrderStateEnum.OrdersData)
                        {
                            string date = GetFormString(form, "ordertime");
                            string time = GetFormString(form, "orderhour");
                            entity.OrderTime = Convert.ToDateTime(date + " " + time);
                        }
                        entity.MediaSourceID = GetFormInt(form, "mediasource", -1);
                        if (entity.MediaSourceID == MediaSource.Actor.GetByName(CurrentUser.HospitalID, "网络").ID)//只有选择网络以下两个字段才有效
                        {
                            entity.HospitalWebsiteID = GetFormInt(form, "hospitalwebsite", -1);
                            entity.KeyWords = GetFormString(form, "keywords");
                        }
                        else
                        {
                            entity.HospitalWebsiteID = -1;
                            entity.KeyWords = "";
                        }
                        entity.MediaSourceExtendID = GetFormInt(form, "mediasourceextend", -1);
                        //entity.RecordTime = System.DateTime.Now;
                        //entity.HospitalID = CurrentUser.Hospital.ID;
                        entity.SectionID = GetFormInt(form, "section", -1);
                        Section sec = Section.Actor.GetByID(entity.SectionID);
                        if (sec != null)
                        {
                            if (sec.HospitalID != entity.HospitalID)
                            {
                                return Json(new { flag = "error" });
                            }
                        }
                        entity.DoctorID = GetFormInt(form, "doctor", -1);
                        entity.OrderState = 0;
                        Orders.Actor.Update(entity);

                        /////////////////////////
                        //记录日志
                        OperationLog logs = new OperationLog();
                        JArray jlogs = new JArray();
                        if (entity.ExpertIdentifier != null && oldentity.ExpertIdentifier != entity.ExpertIdentifier)
                        {
                            jlogs.Add(new JObject(
                                new JProperty("Name", "专家号"),
                                new JProperty("Value", oldentity.ExpertIdentifier + "------>" + entity.ExpertIdentifier)
                               ));
                        }
                        if (oldentity.Name != entity.Name)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "姓名"),
                            new JProperty("Value", oldentity.Name + "------>" + entity.Name)
                            ));
                        }
                        if (oldentity.Age != entity.Age)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "年龄"),
                            new JProperty("Value", oldentity.Age + "------>" + entity.Age)
                            ));
                        }
                        if (oldentity.Sex != entity.Sex)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "性别"),
                            new JProperty("Value", (oldentity.Sex ? "男" : "女") + "------>" + (entity.Sex ? "男" : "女"))
                            ));
                        }
                        if (!string.IsNullOrEmpty(entity.Telephone) && oldentity.Telephone != entity.Telephone)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "电话"),
                            new JProperty("Value", oldentity.Telephone + "------>" + entity.Telephone)
                            ));
                        }
                        if (!string.IsNullOrEmpty(entity.QQ) && oldentity.QQ != entity.QQ)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "QQ"),
                            new JProperty("Value", oldentity.QQ + "------>" + entity.QQ)
                            ));
                        }
                        if (!string.IsNullOrEmpty(entity.Question) && oldentity.Question != entity.Question)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "咨询内容"),
                            new JProperty("Value", "咨询内容有修改,详细内容暂不显示")//oldentity.Question + "------>" + entity.Question)
                            ));
                        }
                        if (!string.IsNullOrEmpty(entity.ChatRecord) && oldentity.ChatRecord != entity.ChatRecord)
                        {
                            jlogs.Add(new JObject(
                                new JProperty("Name", "聊天记录"),
                                new JProperty("Value", "聊天记录有修改,详细内容暂不显示")// oldentity.ChatRecord + "------>" + entity.ChatRecord)
                                ));
                        }
                        if (!string.IsNullOrEmpty(entity.Remark) && oldentity.Remark != entity.Remark)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "备注"),
                            new JProperty("Value", oldentity.Remark + "------>" + entity.Remark)
                            ));
                        }
                        if (entity.SectionName.Name != entity.SectionName.Name)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "科室"),
                            new JProperty("Value", oldentity.SectionName.Name + "------>" + entity.SectionName.Name)
                            ));
                        }
                        if (entity.Doctor != null && oldentity.Doctor.Name != entity.Doctor.Name)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "医生"),
                            new JProperty("Value", oldentity.Doctor.Name + "------>" + entity.Doctor.Name)
                            ));
                        }
                        if (entity.Disease != null && oldentity.Disease.Name != entity.Disease.Name)
                        {
                            jlogs.Add(new JObject(
                            new JProperty("Name", "疾病"),
                            new JProperty("Value", oldentity.Disease.Name + "------>" + entity.Disease.Name)
                            ));
                        }
                        if (entity.OrderTime != null && oldentity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss") != entity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss"))
                        {
                            jlogs.Add(new JObject(
                          new JProperty("Name", "预约时间"),
                          new JProperty("Value", oldentity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss") + "------>" + entity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss"))
                          ));
                        }
                        //媒体来源

                        string oldremarkMsg = "";
                        if (oldentity.MediaEntity != null)
                        {
                            oldremarkMsg = string.IsNullOrEmpty(oldentity.MediaEntity.Name) ? "" : (oldentity.MediaEntity.Name);
                            if (oldentity.MediaSourceExtendID > 0)
                            {
                                MediaSource oldentitys = MediaSource.Actor.GetByID(oldentity.MediaSourceExtendID);
                                if (oldentitys != null)
                                {
                                    oldremarkMsg += "-" + oldentitys.Name + "";
                                }
                            }
                        }
                        string remarkMsg = "";
                        if (entity.MediaEntity != null)
                        {
                            remarkMsg = string.IsNullOrEmpty(entity.MediaEntity.Name) ? "" : (entity.MediaEntity.Name);
                            if (entity.MediaSourceExtendID > 0)
                            {
                                MediaSource entitys = MediaSource.Actor.GetByID(entity.MediaSourceExtendID);
                                if (entitys != null)
                                {
                                    remarkMsg += "-" + entitys.Name + "";
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(remarkMsg) && oldremarkMsg != remarkMsg)
                        {
                            jlogs.Add(new JObject(
                        new JProperty("Name", "媒体来源"),
                        new JProperty("Value", oldremarkMsg + "------->" + remarkMsg)
                        ));
                        }
                        if (entity.HospitalWebsite != null && oldentity.HospitalWebsite.URL == entity.HospitalWebsite.URL)
                        {
                            jlogs.Add(new JObject(
                        new JProperty("Name", "网站"),
                        new JProperty("Value", oldentity.HospitalWebsite.URL + "------->" + entity.HospitalWebsite.URL)
                        ));
                        }
                        if (entity.AreaSourceType != oldentity.AreaSourceType || entity.Area != oldentity.Area)
                        {
                            jlogs.Add(new JObject(
                      new JProperty("Name", "地区来源"),
                      new JProperty("Value", (oldentity.AreaSourceType == AreaTypeEnum.native ? "本地" : "外地:" + oldentity.Area) + "------->" + (entity.AreaSourceType == AreaTypeEnum.native ? "本地" : "外地:" + entity.Area))
                      ));
                        }
                        string CountMsg = "无";
                        CountMsg = GetCountText(entity);
                        string OldCountMsg = "无";
                        OldCountMsg = GetCountText(oldentity);
                        if (oldentity.CountType != entity.CountType)
                        {
                            jlogs.Add(new JObject(
                        new JProperty("Name", "统计账户"),
                        new JProperty("Value", OldCountMsg + "------->" + CountMsg)
                        ));
                        }
                        logs.OperationType = OperationTypeEnum.Update;
                        logs.TableName = "预约信息";
                        logs.OperationInfo = jlogs.ToString();
                        logs.LinkID = oldentity.ID;
                        logs.HospitalID = entity.HospitalID;
                        logs.SectionID = entity.SectionID;
                        if (jlogs.Count > 0)
                        {
                            CreateOperationLogs(logs);
                        }
                        return Json(new { flag = "success" });
                    }
                    else
                    {
                        return Json(new { flag = "error2" });
                    }
                }
            }
            return Json(new { flag = "error" });
        }

        private static string GetCountText(Orders entity)
        {
            string CountMsg = "无";
            if (entity.CountType == CountTypeEnum.Others)
            {
                CountMsg = "其他";
            }
            if (entity.CountType == CountTypeEnum.Telephone)
            {
                CountMsg = "电话";
            }
            if (entity.CountType == CountTypeEnum.Web)
            {
                CountMsg = "网络";
            }

            return CountMsg;
        }

        [HttpPost]
        public JsonResult OrderDelete(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            string ids = GetFormString(form, "ids");
            if (ids != "")
            {
                string[] idstr = StringUtility.Split(ids, ',');
                foreach (string item in idstr)
                {
                    //是否有此科室管理权限，是否有删除权限，否则不让删。
                    Orders entity = Orders.Actor.GetByID(int.Parse(item));
                    if (entity != null)
                    {
                        entity.IsDelete = 1;
                        Orders.Actor.Update(entity);
                        CreateOprentionAddOrDeleteLog(entity, OperationTypeEnum.Delete);
                        return Json(new { flag = "success" });
                    }
                }
            }
            return Json(new { flag = "error" });
        }

        #region 查看是否有该病人
        /// <summary>
        /// 查看是否有该病人
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult GetCountByExistsName()
        {
            int sectionID = RequestUtility.GetQueryInt("sectionID", 0);
            string name = RequestUtility.GetQueryString("name");
            if (sectionID > 0 && !string.IsNullOrEmpty(name))
            {
                string sectionStr = "";
                if (CurrentUser.UserSectionList != null)
                {
                    foreach (Section sItem in CurrentUser.UserSectionList)
                    {
                        sectionStr += "" + sItem.ID + ",";
                    }
                }
                sectionStr += "-100";
                Orders entity = new Orders();
                entity = Orders.Actor.GetCountByExistsName(sectionStr, name, DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd 00:00:00.000"), DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                if (entity != null)
                {
                    return Json(new { flag = "success", patientName = entity.Name.ToString(), tel = entity.DESTelephone.ToString(), createUserName = entity.CreateUserName.ToString(), addTime = entity.AddTime.ToString("MM月dd日hh时"), sectionName = entity.SectionName.Name }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { flag = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { flag = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 查看是否有该病人
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult GetCountByExistsTel()
        {
            int sectionID = RequestUtility.GetQueryInt("sectionID", 0);
            string telephone = RequestUtility.GetQueryString("telephone");
            if (sectionID > 0 && !string.IsNullOrEmpty(telephone))
            {
                string sectionStr = "";
                if (CurrentUser.UserSectionList != null)
                {
                    foreach (Section sItem in CurrentUser.UserSectionList)
                    {
                        sectionStr += "" + sItem.ID + ",";
                    }
                }
                sectionStr += "-100";
                Orders entity = new Orders();
                entity.Telephone = telephone;
                entity = Orders.Actor.GetCountByExistsTel(sectionStr, telephone, DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd 00:00:00.000"), DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                if (entity != null)
                {
                    return Json(new { flag = "success", patientName = entity.Name, tel = entity.DESTelephone, createUserName = entity.CreateUserName, addTime = entity.AddTime.ToString("MM月dd日hh时"), sectionName = entity.SectionName.Name }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { flag = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { flag = "error" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region 回访管理

        public ActionResult CallBack()
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_CALLBACK))
            {
                LoginManager.GoUnAccessPagePop();
            }
            int id = RequestUtility.GetQueryInt("id", -1);
            Orders order = Orders.Actor.GetByID(id);
            return View(order);
        }

        [HttpPost]
        public JsonResult CallBack(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_CALLBACK))
            {
                LoginManager.GoUnAccessPagePop();
            }
            string sectionStr = "-1";
            if (CurrentUser.UserSectionList != null)
            {
                foreach (Section sItem in CurrentUser.UserSectionList)
                {
                    sectionStr += "," + sItem.ID + "";
                }
            }
            CallOnData entity = new CallOnData();
            CallOnData oldentity = new CallOnData();
            int orderID = GetFormInt(form, "orderID", -1);
            int isSetNextCallBackTime = GetFormInt(form, "isSetCallBackTime", -1);
            entity.CallOnDateTime = DateTime.Now;
            entity.CallUserID = CurrentUser.ID;
            entity.OrdersID = orderID;
            entity.IsCallOn = true;
            entity.Remark = GetFormString(form, "callback");
            oldentity = entity.Clone();
            Orders order = Orders.Actor.GetByID(orderID);
            Orders oldorder = order.Clone();
            if (order != null)//预约数据不为空的话修改状态
            {
                int arriveType = GetFormInt(form, "arrivetype", -1);

                if ((ArriveStateEnum)arriveType == ArriveStateEnum.Arrived)
                {
                    order.ArriveStateType = ArriveStateEnum.Arrived;
                }
                if ((ArriveStateEnum)arriveType == ArriveStateEnum.NoArrive)
                {
                    order.ArriveStateType = ArriveStateEnum.NoArrive;
                }
                if ((ArriveStateEnum)arriveType == ArriveStateEnum.Wait)
                {
                    order.ArriveStateType = ArriveStateEnum.Wait;
                }
                string date = GetFormString(form, "ordertime");
                string time = GetFormString(form, "orderhour");
                DateTime orderTime = Convert.ToDateTime(date + " " + time);
                order.OrderTime = orderTime;// GetFormDate(form, "ordertime").AddHours(GetFormInt(form, "orderhour", 0));
                order.UpdateTime = DateTime.Now;

                Orders.Actor.Update(order);
            }
            //查找今日以前的日期有没有没有回访的记录提醒，全部删除.
            CallOnData.Actor.DeleteRamind(order.ID, entity.CallUserID, entity.CallOnDateTime);
            //IEnumerable<int> intLst=(from per in callOnData where per.IsCallOn == false select per.ID); 
            int returnData = CallOnData.Actor.Insert(entity);
            if (isSetNextCallBackTime == 1)
            {
                IList<CallOnData> callOnData = CallOnData.Actor.GetByUserIDAllCallOnData(entity.CallUserID, 0, 0, sectionStr);
                if (callOnData.Count < 20)//回访提醒不能超过20个
                {
                    CallOnData entitys = new CallOnData();

                    string date1 = GetFormString(form, "nextCallBackTime");
                    string time2 = GetFormString(form, "callOnTimeSelecthours");
                    DateTime callOnDateTime = Convert.ToDateTime(date1 + " " + time2);

                    entitys.CallOnDateTime = callOnDateTime;// GetFormDate(form, "nextCallBackTime").AddHours(GetFormInt(form, "callOnTimeSelecthours", 0));
                    entitys.CallUserID = CurrentUser.ID;
                    entitys.IsCallOn = false;
                    entitys.OrdersID = orderID;
                    entitys.IsValid = 0;
                    CallOnData.Actor.Insert(entitys);
                }
                else
                {
                    return Json(new { flag = "error1" });
                }
            }
            ///////////////////////////////////////////////////////////////
            OperationLog logs = new OperationLog();
            JArray jlogs = new JArray();
            if (order.OrderTime != null && oldorder.OrderTime != order.OrderTime)
            {
                jlogs.Add(new JObject(
                    new JProperty("Name", "预约时间"),
                    new JProperty("Value", oldorder.OrderTime.ToString("yyyy-MM-dd HH:mm:ss") + "------>" + order.OrderTime.ToString("yyyy-MM-dd HH:mm:ss"))
                   ));
            }
            if (oldorder.ArriveStateType != order.ArriveStateType)
            {
                jlogs.Add(new JObject(
                    new JProperty("Name", "到院状态"),
                    new JProperty("Value", (Convert.ToInt32(oldorder.ArriveStateType) == 0 ? "等待" : Convert.ToInt32(oldorder.ArriveStateType) == 2 ? "未到" : " 已到") + "------>" + (Convert.ToInt32(order.ArriveStateType) == 0 ? "等待" : Convert.ToInt32(order.ArriveStateType) == 2 ? "未到" : " 已到"))
                   ));
            }
            if (!string.IsNullOrEmpty(entity.Remark))
            {
                jlogs.Add(new JObject(
                    new JProperty("Name", "回访内容"),
                    new JProperty("Value", entity.Remark)
                   ));
            }
            if (isSetNextCallBackTime == 1)//设置了下次提醒时间
            {
                string date1 = GetFormString(form, "nextCallBackTime");
                string time2 = GetFormString(form, "callOnTimeSelecthours");
                DateTime callOnDateTime = Convert.ToDateTime(date1 + " " + time2);
                jlogs.Add(new JObject(
                    new JProperty("Name", "提醒时间"),
                    new JProperty("Value", callOnDateTime.ToString("yyyy-MM-dd HH:mm:ss"))
                   ));
            }

            logs.OperationType = OperationTypeEnum.Add;
            logs.TableName = "回访";
            logs.OperationInfo = jlogs.ToString();
            logs.LinkID = oldorder.ID;
            logs.HospitalID = order.HospitalID;
            logs.SectionID = order.SectionID;
            if (jlogs.Count > 0)
            {
                CreateOperationLogs(logs);
            }
            //////////////////////////////////////////////////////////////

            return returnData > 0 ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }
        #endregion

        #region 到院状态

        public ActionResult ArriveState()
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_STATE))
            {
                LoginManager.GoUnAccessPagePop();
            }
            int orderID = RequestUtility.GetQueryInt("orderid", -1);
            if (orderID > -1)
            {
                Orders orders = Orders.Actor.GetByID(orderID);
                if (orders != null)
                {
                    IList<Doctor> listDoctor = Doctor.Actor.GetBySectionID(orders.SectionID);
                    ViewBag.doctorList = listDoctor;
                    return View(orders);
                }
            }
            return RedirectToAction("Index", "Error");
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ArriveState(FormCollection form, int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_STATE))
            {
                LoginManager.GoUnAccessPagePop();
            }
            string remark = GetFormString(form, "dremark");
            int callBack = GetFormInt(form, "arrivetype", -1);
            int doctorID = GetFormInt(form, "doctorid", -1);

            string date = GetFormString(form, "ordertime");
            string time = GetFormString(form, "ordertimeHour");
            DateTime ordertime = Convert.ToDateTime(date + " " + time);

            string redate = GetFormString(form, "recordtime");
            string retime = GetFormString(form, "recordtimeHour");
            DateTime recordtime = Convert.ToDateTime(redate + " " + retime);
            bool isConsume = GetFormBool(form, "isConsume");
            decimal consumeNum = GetFormDecimal(form, "consumeNum",0);
            
           
            Orders entity = Orders.Actor.GetByID(id);
            Orders oldentity = entity.Clone();

            if (entity != null)
            {
                if ((ArriveStateEnum)callBack == ArriveStateEnum.Arrived)//如果状态为已到
                {
                    entity.RecordTime = recordtime;
                    CallOnData.Actor.DeleteRamind(entity.ID);
                }
                entity.ArriveStateType = (ArriveStateEnum)callBack;
                entity.UpdateTime = System.DateTime.Now;
                entity.DoctorID = doctorID;
                entity.DRemark = remark;
                entity.OrderTime = ordertime;
                entity.RecordUserID = CurrentUser.ID;
                entity.IsConsume = isConsume;
                if (isConsume)//为true才存数据
                {
                    entity.ConsumeNum = consumeNum;
                }
                Orders.Actor.Update(entity);
                OperationLog logs = new OperationLog();
                JArray jlogs = new JArray();
                if (oldentity.ArriveStateType != entity.ArriveStateType)
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "到院状态"),
                        new JProperty("Value", (Convert.ToInt32(oldentity.ArriveStateType) == 0 ? "等待" : Convert.ToInt32(oldentity.ArriveStateType) == 2 ? "未到" : " 已到") + "------>" + (Convert.ToInt32(entity.ArriveStateType) == 0 ? "等待" : Convert.ToInt32(entity.ArriveStateType) == 2 ? "未到" : " 已到"))
                       ));
                }
                if (oldentity.Doctor != null && oldentity.DoctorID != entity.DoctorID)
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "医生"),
                        new JProperty("Value", oldentity.Doctor.Name + "------>" + entity.Doctor.Name)
                       ));
                }
                if (!string.IsNullOrEmpty(entity.DRemark) && oldentity.DRemark != entity.DRemark)
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "备注"),
                        new JProperty("Value", oldentity.DRemark + "------>" + entity.DRemark)
                       ));
                }
                //if (oldentity.RecordUserID != entity.RecordUserID)
                //{
                //    jlogs.Add(new JObject(
                //        new JProperty("Name", "修改人"),
                //        new JProperty("Value", oldentity.RecordUserName + "------>" + entity.RecordUserName)
                //       ));
                //}
                if (oldentity.OrderTime != entity.OrderTime)
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "预约时间"),
                        new JProperty("Value", oldentity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss") + "------>" + entity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss"))
                       ));
                }
                if (oldentity.IsConsume != entity.IsConsume)
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "是否消费"),
                        new JProperty("Value", (oldentity.IsConsume?"是":"否")+ "------>" + (entity.IsConsume ? "是" : "否"))
                       ));
                }
                if (oldentity.ConsumeNum != entity.ConsumeNum)
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "消费金额"),
                        new JProperty("Value", oldentity.ConsumeNum+ "------>" + entity.ConsumeNum)
                       ));
                }
                if (oldentity.OrderTime != entity.OrderTime)
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "预约时间"),
                        new JProperty("Value", oldentity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss") + "------>" + entity.OrderTime.ToString("yyyy-MM-dd HH:mm:ss"))
                       ));
                }

                if (entity.ArriveStateType == ArriveStateEnum.Arrived)
                {
                    jlogs.Add(new JObject(
                        new JProperty("Name", "到院时间"),
                        new JProperty("Value", entity.RecordTime.ToString("yyyy-MM-dd HH:mm:ss"))
                       ));
                }
                //if (oldentity.UpdateTime != entity.UpdateTime)
                //{
                //    jlogs.Add(new JObject(
                //        new JProperty("Name", "修改时间"),
                //        new JProperty("Value", oldentity.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") + "------>" + entity.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"))
                //       ));
                //}
                logs.OperationType = OperationTypeEnum.Add;//添加回访记录
                logs.TableName = "到院状态";
                logs.OperationInfo = jlogs.ToString();
                logs.LinkID = entity.ID;
                logs.HospitalID = entity.HospitalID;
                logs.SectionID = entity.SectionID;
                if (jlogs.Count > 0)
                {
                    CreateOperationLogs(logs);
                }
                //////////////////////////////////////
                return Json(new { flag = "success" });
            }
            return Json(new { flag = "error" });
        }
        #endregion

        #region 查看聊天内容

        public ActionResult ChatContent()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_ORDER_PERSON_MESSAG))
            {
                LoginManager.GoUnAccessPagePop();
            }
            int orderID = RequestUtility.GetQueryInt("id", -1);
            //string code = RequestUtility.GetQueryString("code");
            try
            {
                //if (CurrentUser.Hospital.HSafeCode == code)
                //{
                Orders orders = Orders.Actor.GetByID(orderID);
                try
                {
                    if (orderID > -1)
                    {
                        if (CurrentUser.Hospital.IsOpenPassWord == 1)//表示未开启该功能
                        {
                            Hospital hospital = CurrentUser.Hospital;
                            hospital.HSafeCode = GenerateRandom(12);
                            Hospital.Actor.Update(hospital);
                        }
                        string section = ",";
                        if (CurrentUser.UserSectionList != null)
                        {
                            foreach (Section sItem in CurrentUser.UserSectionList)
                            {
                                section += sItem.ID + ",";
                            }
                            if (section.Contains("," + orders.SectionID + ","))
                            {

                                IList<CallOnData> callondata = CallOnData.Actor.GetByOrderID(orderID, 1);
                                ViewData["CallOnData"] = callondata;
                                ViewData["CallOnDataCount"] = callondata.Count;
                                return View(orders);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log4NetUtility.WriterErrorLog(ex.ToString());
                    throw;
                }
                //}
            }
            catch (Exception ex)
            {
                Log4NetUtility.WriterErrorLog(ex.ToString());
                // return RedirectToAction("Index", "Error");
                throw;
            }
            return RedirectToAction("Index", "Error");
        }


        public ActionResult MyselfChatContent()
        {
            int orderID = RequestUtility.GetQueryInt("id", -1);
            //string code = RequestUtility.GetQueryString("code");
            try
            {
                //  if (CurrentUser.Hospital.HSafeCode == code)
                //  {
                Orders orders = Orders.Actor.GetByID(orderID);
                try
                {
                    if (orderID > -1 && orders.OrderState == (int)OrderStateEnum.MySelfOrdersData)
                    {
                        //if (CurrentUser.Hospital.IsOpenPassWord == 1)//表示未开启该功能
                        //{
                        //    Hospital hospital = CurrentUser.Hospital;
                        //    hospital.HSafeCode = GenerateRandom(12);
                        //    Hospital.Actor.Update(hospital);
                        //}
                        string section = ",";
                        if (CurrentUser.UserSectionList != null)
                        {
                            foreach (Section sItem in CurrentUser.UserSectionList)
                            {
                                section += sItem.ID + ",";
                            }
                            if (section.Contains("," + orders.SectionID + ","))
                            {

                                IList<CallOnData> callondata = CallOnData.Actor.GetByOrderID(orderID, 1);
                                ViewData["CallOnData"] = callondata;
                                ViewData["CallOnDataCount"] = callondata.Count;
                                return View(orders);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log4NetUtility.WriterErrorLog(ex.ToString());
                    throw;
                }
                // }
            }
            catch (Exception ex)
            {
                Log4NetUtility.WriterErrorLog(ex.ToString());
                // return RedirectToAction("Index", "Error");
                throw;
            }
            return RedirectToAction("Index", "Error");
        }
        #endregion

        #region 高级搜索

        public ActionResult OrderAdvancedSearch()
        {
            IList<Section> list = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
            ViewData["sectionList"] = list;
            ViewData["mediaList"] = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, 0);
            ViewData["role"] = CurrentUser.UserRole.ManagerRoles; //Role.Actor.GetAllData();
            return View();
        }

        #endregion

        #region 重复病人搜索

        public ActionResult RepeatSearch()
        {
            return View();
        }

        #endregion

        #region 自助挂号预约表

        /*public ActionResult OutOrderIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_GUOHAO))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public JsonResult GetOutOrderDataList()
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_GUOHAO))
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
                where += " AND  UserName  LIKE '%" + keywords + "%'";
            }
            Hospital currentHospital = LoginManager.GetCurrentHospital();
            if (currentHospital != null)
            {
                where += "  AND HospitalID=" + currentHospital.ID;
            }

            IList<OutOrder> list = OutOrder.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                sourceData.Add(new dynamic[] { 
                    item.ID, item.UserName, item.Sex ? "男" : "女",
                    item.Age, item.Telephone,
                    item.OrderTime.ToString("yyyy-MM-dd"),
                    item.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                    item.HospitalWebsite == null ? "没有来源" : item.HospitalWebsite.URL,
                    item.Description, item.ID, item.State 
                });
            }

            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData,
            }, JsonRequestBehavior.AllowGet);
        }*/

        #endregion

        #region 资源库操作

        public ActionResult MySelfOrderIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_PERSON))
            {
                LoginManager.GoUnAccessPage();
            }
            string globalKeyword = RequestUtility.GetQueryString("globalKeyword");
            string timeKinds = RequestUtility.GetQueryString("timekinds");
            string startTime = RequestUtility.GetQueryString("starttime");
            string endTime = RequestUtility.GetQueryString("endtime");
            string section = RequestUtility.GetQueryString("section");
            string doctor = RequestUtility.GetQueryString("doctor");
            string diseaseKind = RequestUtility.GetQueryString("diseasekind");
            string arriveState = RequestUtility.GetQueryString("arrivestate");
            string mediaSource = RequestUtility.GetQueryString("mediasource");
            string mediasourceextend = RequestUtility.GetQueryString("mediasourceextend");
            string hospitalwebsite = RequestUtility.GetQueryString("hospitalwebsite");
            //创建人
            string keys = RequestUtility.GetQueryString("keys");
            string customer = RequestUtility.GetQueryString("userInfo");
            string department = RequestUtility.GetQueryString("department");
            string repeatStartTime = RequestUtility.GetQueryString("repeatstarttime");
            string repeatEndTime = RequestUtility.GetQueryString("repeatendtime");
            string repeatSearchData = RequestUtility.GetQueryString("repeatsearchdata");
            string callOn = RequestUtility.GetQueryString("uncallOnData");
            if (!string.IsNullOrEmpty(callOn))
            {
                ViewBag.callOn = callOn;
            }
            else
            {
                ViewBag.callOn = 0;
            }

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
            searchDatas.Add("mediasourceextend", mediasourceextend.ToString());
            searchDatas.Add("hospitalwebsite", hospitalwebsite.ToString());

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
            searchDatas.Add("uncallOnData", callOn);
            //ViewBag.SearchDatas = GetNewSearchDataString();
            return View();
        }

        public ActionResult GetlistMyData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_PERSON))
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
            string where = " IsDelete=0 AND OrderState=" + (int)OrderStateEnum.MySelfOrdersData + " ";
            int repeatSearchData = RequestUtility.GetQueryInt("repeatsearchdata", -1);
            string callOn = RequestUtility.GetQueryString("uncallOnData");
            if (LoginManager.HasAutority(AuthorityConst.AP_SELECTORTER))
            {
                    if (!string.IsNullOrEmpty(CurrentUser.UserRole?.ManagerRoles.ToString()))
                    {
                        where += " AND (";
                        @where = CurrentUser.UserRole.ManagerRoles.Where(roleId => roleId != null).Aggregate(@where, (current, roleId) => current + (" RoleID=" + roleId.ID + " or "));
                        where += " RoleID=-100 )";
                    }
            }
            else
            {
                where += " AND CreateUserID=" + CurrentUser.ID + "";
            }
            if (CurrentUser != null)
            {
                where += " AND HospitalID=" + CurrentUser.HospitalID;
            }
            /*是否有查看别人资源的权限，如果有显示所有的，没有只显示自己的*/
            if (!string.IsNullOrEmpty(keywords))
            {
                keywords = keywords.Trim();
                where += " AND (Name LIKE '" + keywords + "%' OR Telephone LIKE '%" + keywords + "'" + " OR charindex('" + keywords + "',ExpertIdentifier)>0)";
            }
            where += " AND SectionID in (";
            IList<Section> sectionLst = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
            @where = sectionLst.Aggregate(@where, (current, sItem) => current + ("" + sItem.ID + ","));
            where += "-100 )";
            if (!string.IsNullOrEmpty(callOn))
            {
                string sectionStr = "-100";
                if (CurrentUser.UserSectionList != null)
                {
                    sectionStr = CurrentUser.UserSectionList.Aggregate(sectionStr, (current, sItem) => current + ("," + sItem.ID + ""));
                }
                IList<CallOnData> callOnData = CallOnData.Actor.GetByUserIDAndState(CurrentUser.ID, 0, 1, sectionStr);
                where += " AND ID in (";
                @where = callOnData.Aggregate(@where, (current, cal) => current + ("" + cal.OrdersID + " , "));
                where += " -100 )";
            }
            //RequestUtility.GetQueryString("userInfo");
            IList<dynamic> orderList = null;
            orderList = MyselfOrderToShow(out pageCount, pageIndex, pageSize, where, orderFields, isDesc, CurrentUser);

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
        public IList<dynamic> MyselfOrderToShow(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc, UserInfo user)
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_PERSON))
            {
                LoginManager.GoUnAccessPage();
            }
            pageCount = 0;
            IList<dynamic> sourceData = new List<dynamic>();
            if (user != null)
            {
                IList<Orders> entityList = Orders.Actor.SearchView(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
                if (entityList != null && entityList.Count > 0)
                {
                    foreach (var item in entityList)
                    {
                        string function = "chatcontent";
                        string canshu = "" + item.ID + ",\"" + CurrentUser.Hospital.HSafeCode + "\"";
                        //if (CurrentUser.Hospital.IsOpenPassWord == 1)//表示未开启该功能
                        //{
                        //    function = "chatcontent";
                        //    canshu = item.ID.ToString();
                        //}
                        string baseInfo = string.Format("<a style='color:{0};' href='javascript:{5}({1})'>{2}-{3}岁-{4}</a><br />",
                            item.Sex ? "#1CAF9A" : "#ff5400",
                            canshu,
                            item.Name,
                            item.Age,
                            item.Sex ? "男" : "女",
                            function);

                        if (!string.IsNullOrEmpty(item.Telephone))
                        {
                            baseInfo += "手机:&nbsp;" + item.DESTelephone + "<br />";
                        }
                        //if (!string.IsNullOrEmpty(item.QQ))
                        //{
                        //    baseInfo += "Q Q:&nbsp;" + item.QQ + "<br />";
                        //}
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
                        string reservationMsg =
                            $"{(item.ExpertIdentifier != null && item.ExpertIdentifier.Trim() != "" ? "专家号：" + item.ExpertIdentifier + "<br />" : "")}疾病：{(item.Disease != null ? item.Disease.Name : "没有疾病")}{(item.DoctorID <= 0 ? string.Empty : "<br/>" + ("医生：" + item.Doctor.Name))}";

                        string remarkMsg = string.IsNullOrEmpty(item.Question) ? "" : @"咨询：<span title=" + StringUtility.CutHtmlFormat(item.Question) + ">" + StringUtility.CutString(item.Question, 30, true) + "</span><br />";
                        remarkMsg += string.IsNullOrEmpty(item.Remark) ? "" : "备注：<span title=" + StringUtility.CutHtmlFormat(item.Remark) + ">" + StringUtility.CutString(item.Remark, 30, true) + "</span><br />";
                        remarkMsg += item.LastCallOnDada == null ? "" : "回访：" + item.LastCallOnDada.Remark + "<span style='color:blue'>(" + item.LastCallOnDada.CallOnDateTime.ToString("yyyy-MM-dd HH:mm") + "&nbsp;by:&nbsp;" + item.LastCallOnDada.CallUserName + ")</span><br />";
                        if (item.MediaEntity != null)
                        {
                            remarkMsg += string.IsNullOrEmpty(item.MediaEntity.Name) ? "" : "来源：" + item.MediaEntity.Name + "";
                            if (item.MediaSourceExtendID > 0)
                            {
                                var entitys = MediaSource.Actor.GetByID(item.MediaSourceExtendID);
                                if (entitys != null)
                                {
                                    remarkMsg += "-" + entitys.Name + "<br/>";
                                }
                            }
                        }

                        var callOnData = CallOnData.Actor.GetLastDataByOrderID(item.ID, 0);

                        remarkMsg += string.IsNullOrEmpty(item.KeyWords) ? "" : ("<br/>关键字：<span title=" + StringUtility.CutHtmlFormat(item.KeyWords) + ">" + StringUtility.CutString(item.KeyWords, 30, true) + "</span>");
                        //string para5 = "部&nbsp;&nbsp;&nbsp;门：" + (item.CountType == CountTypeEnum.Telephone ? "电话" : (item.CountType == CountTypeEnum.Web ? "网络" : "其他")) + "<br />";
                        //para5 += "科&nbsp;&nbsp;&nbsp;室：" + item.SectionName.Name + "<br/>";
                        //para5 += "创建人：" + item.CreateUserName;
                        string para5 = "<div class=\"mediaImg\"><img src=\"/files/upload/photo/" + item.CreateUserPictureURL + "\" /><p>" + item.CreateUserName + "</p></div>";
                        StringBuilder todo = new StringBuilder();
                        if (item.CreateUserID == CurrentUser.ID)
                        {
                            todo.AppendFormat("<span><a title='编辑'  class='fa fa-edit' href='javascript:edit(\"{0}\")'></a></span>&nbsp;&nbsp;", item.ID);
                        }
                        todo.AppendFormat("<span><a title='删除'  class='fa fa-trash-o' href='javascript:delCheck(\"{0}\")'></a></span>&nbsp;&nbsp;", item.ID);
                        todo.AppendFormat("<span><a title='回访管理' class='fa fa-phone'  href='javascript:callbackManager(\"{0}\",1)'></a></span>&nbsp;&nbsp;", item.ID);
                        todo.AppendFormat("<span><a title='转化为预约信息' class='glyphicon glyphicon-plus-sign' href='javascript:toOrder(\"{0}\")'></a></span>", item.ID);
                        sourceData.Add(new dynamic[] {
                            "",
                            baseInfo,
                            reservationMsg,
                             remarkMsg,
                            callOnData?.CallOnDateTime.ToString("yyyy-MM-dd HH:mm") ?? "暂无安排",
                            para5,
                            item.AddTime.ToString("yyyy-MM-dd HH:mm"),
                            todo.ToString(),
                            item.CreateUserID
                        });
                    }
                }
            }

            return sourceData;
        }

        public ActionResult MyselfOrderAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.AP_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            IList<Section> list = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
            ViewData["sectionList"] = list;
            ViewData["mediaList"] = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, 0);
            return View();
        }

        [HttpPost]
        public JsonResult MyselfOrderAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AP_ADD))
            {
                LoginManager.GoUnAccessPagePop();
            }
            // string desCode = WebConfigUtility.GetAppSetting("encrypt");
            Orders entity = new Orders();
            entity.Age = GetFormInt(form, "age", 0);
            entity.Name = GetFormString(form, "name");
            entity.AddTime = System.DateTime.Now;
            entity.Area = GetFormString(form, "area");
            entity.AreaSourceType = (AreaTypeEnum)GetFormInt(form, "areasourcetype", -1);
            //string oldString = GetFormString(form, "chatcontent");
            //string[] newStrings = oldString.Split('\n');
            //StringBuilder newString = new StringBuilder();
            //foreach (var item in newStrings)
            //{
            //    newString.Append(item + "&");
            //}
            //entity.ChatRecord = newString.ToString();
            entity.Question = "";
            entity.QQ = "";
            entity.Telephone = "";
            entity.ChatRecord = "";
            string oldString = GetFormString(form, "chatcontent");
            if (!string.IsNullOrEmpty(oldString))
            {
                string[] newStrings = oldString.Split('\n');
                StringBuilder newString = new StringBuilder();
                foreach (var item in newStrings)
                {
                    newString.Append(item + "&");
                }
                entity.ChatRecord = newString.ToString();//聊天记录加密
            }
            if (!string.IsNullOrEmpty(GetFormString(form, "tel")))
            {
                try
                {
                    entity.Telephone = GetFormString(form, "tel");
                }
                catch (Exception)
                {
                    throw;
                }//电话加密
            }
            if (!string.IsNullOrEmpty(GetFormString(form, "question")))
            {
                entity.Question = GetFormString(form, "question");//问题加密
            }
            if (!string.IsNullOrEmpty(GetFormString(form, "qq")))
            {
                entity.QQ = GetFormString(form, "qq");//QQ加密
            }


            entity.CountType = (CountTypeEnum)GetFormInt(form, "countnumber", 3);
            entity.CreateUserID = CurrentUser.ID;
            entity.DiseaseID = GetFormInt(form, "disease", -1);
            entity.ExpertIdentifier = GetFormString(form, "expertidentifier");
            entity.UpdateTime = System.DateTime.Now;
            entity.Sex = GetFormString(form, "sex") == "0" ? true : false;
            entity.Remark = GetFormString(form, "remark");
            entity.OrderTime = System.DateTime.Now;
            entity.MediaSourceID = GetFormInt(form, "mediasource", -1);
            if (entity.MediaSourceID == MediaSource.Actor.GetByName(CurrentUser.HospitalID, "网络").ID)//只有选择网络以下两个字段才有效
            {
                entity.HospitalWebsiteID = GetFormInt(form, "hospitalwebsite", -1);
                entity.KeyWords = GetFormString(form, "keywords");
            }
            else
            {
                entity.HospitalWebsiteID = -1;
                entity.KeyWords = "";
            }
            entity.MediaSourceExtendID = GetFormInt(form, "mediaSourceExtend", -1);
            entity.RecordTime = System.DateTime.Now;
            entity.HospitalID = CurrentUser.Hospital.ID;
            entity.SectionID = GetFormInt(form, "section", -1);
            entity.DoctorID = GetFormInt(form, "doctor", -1);
            entity.OrderState = 1;
            int returnData = -1;
            try
            {
                returnData = Orders.Actor.Insert(entity);
            }
            catch (Exception ex)
            {
                returnData = -1;
            }
            return returnData > 0 ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }

        public ActionResult MyselfOrderEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AP_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            Orders orders = Orders.Actor.GetByID(id);
            IList<Section> list = Section.Actor.GetByUserID(CurrentUser.ID, 1, CurrentUser.HospitalID);
            ViewData["sectionList"] = list;
            ViewData["mediaList"] = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, 0);
            return View(orders);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult MyselfOrderEdit(FormCollection form, int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AP_EDIT))
            {
                LoginManager.GoUnAccessPagePop();
            }
            if (id > 0)
            {
                // string desCode = WebConfigUtility.GetAppSetting("encrypt");
                Orders entity = Orders.Actor.GetByID(id);
                if (entity != null)
                {
                    entity.Age = GetFormInt(form, "age", 0);
                    entity.Name = GetFormString(form, "name");
                    //entity.AddTime = System.DateTime.Now;
                    entity.Area = GetFormString(form, "area");
                    entity.AreaSourceType = (AreaTypeEnum)GetFormInt(form, "areasourcetype", -1);
                    string oldString = GetFormString(form, "chatcontent");//聊天记录加密
                    if (!string.IsNullOrEmpty(oldString) && !oldString.Contains("*"))
                    {
                        if (oldString != entity.DESChatRecord)//如果不相等说明已经修改
                        {
                            string[] newStrings = oldString.Split('\n');
                            StringBuilder newString = new StringBuilder();
                            foreach (var item in newStrings)
                            {
                                newString.Append(item + "&");
                            }
                            entity.ChatRecord = newString.ToString();//聊天记录加密
                        }
                    }
                    if (!string.IsNullOrEmpty(GetFormString(form, "tel")) && !GetFormString(form, "tel").ToString().Contains("*"))
                    {
                        if (GetFormString(form, "tel").ToString() != entity.DESTelephone)//如果不相等说明已经修改
                        {
                            entity.Telephone = GetFormString(form, "tel");//电话加密
                        }
                    }
                    if (!string.IsNullOrEmpty(GetFormString(form, "question")) && !GetFormString(form, "question").ToString().Contains("*"))
                    {
                        if (GetFormString(form, "question").ToString() != entity.Question)//如果不相等说明已经修改
                        {
                            entity.Question = GetFormString(form, "question");//问题加密
                        }
                    }
                    if (!string.IsNullOrEmpty(GetFormString(form, "qq")) && !GetFormString(form, "qq").ToString().Contains("*"))
                    {
                        if (GetFormString(form, "qq").ToString() != entity.QQ)//如果不相等说明已经修改
                        {
                            entity.QQ = GetFormString(form, "qq");//QQ加密
                        }
                    }

                    entity.CountType = (CountTypeEnum)GetFormInt(form, "countnumber", 3);
                    //entity.CreateUserID = CurrentUser.ID;
                    entity.DiseaseID = GetFormInt(form, "disease", -1);
                    entity.ExpertIdentifier = GetFormString(form, "expertidentifier");
                    entity.UpdateTime = System.DateTime.Now;
                    //entity.Telephone = GetFormString(form, "tel");
                    entity.Sex = GetFormString(form, "sex") == "0" ? true : false;
                    entity.Remark = GetFormString(form, "remark");
                    //entity.Question = GetFormString(form, "question");
                    //entity.QQ = GetFormString(form, "qq");
                    //entity.OrderTime = GetFormDate(form, "ordertime").AddHours(GetFormInt(form, "orderhour", 0));
                    entity.MediaSourceID = GetFormInt(form, "mediasource", -1);
                    if (entity.MediaSourceID == MediaSource.Actor.GetByName(CurrentUser.HospitalID, "网络").ID)//只有选择网络以下两个字段才有效
                    {
                        entity.HospitalWebsiteID = GetFormInt(form, "hospitalwebsite", -1);
                        entity.KeyWords = GetFormString(form, "keywords");
                    }
                    else
                    {
                        entity.HospitalWebsiteID = -1;
                        entity.KeyWords = "";
                    }
                    entity.MediaSourceExtendID = GetFormInt(form, "mediasourceextend", -1);
                    //entity.RecordTime = System.DateTime.Now;
                    // entity.HospitalID = CurrentUser.Hospital.ID;
                    entity.SectionID = GetFormInt(form, "section", -1);
                    entity.DoctorID = GetFormInt(form, "doctor", -1);
                    entity.OrderState = 1;
                    //entity.OrderTime = DateTime.Now;
                    Orders.Actor.Update(entity);
                    return Json(new { flag = "success" });
                }
            }
            return Json(new { flag = "error" });
        }

        #region 回访管理

        public ActionResult MyselfCallBack()
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_CALLBACK))
            {
                LoginManager.GoUnAccessPagePop();
            }
            int id = RequestUtility.GetQueryInt("id", -1);
            Orders order = Orders.Actor.GetByID(id);
            return View(order);
        }

        [HttpPost]
        public JsonResult MyselfCallBack(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AY_CALLBACK))
            {
                LoginManager.GoUnAccessPagePop();
            }
            string sectionStr = "-1";
            if (CurrentUser.UserSectionList != null)
            {
                foreach (Section sItem in CurrentUser.UserSectionList)
                {
                    sectionStr += "," + sItem.ID + "";
                }
            }
            CallOnData entity = new CallOnData();
            int orderID = GetFormInt(form, "orderID", -1);
            int isSetNextCallBackTime = GetFormInt(form, "isSetCallBackTime", -1);
            entity.CallOnDateTime = DateTime.Now;
            entity.CallUserID = CurrentUser.ID;
            entity.OrdersID = orderID;
            entity.IsCallOn = true;
            entity.Remark = GetFormString(form, "callback");
            Orders order = Orders.Actor.GetByID(orderID);
            if (order != null)
            {
                Orders.Actor.Update(order);
            }
            //查找今日以前的日期有没有没有回访的记录提醒，全部删除.
            CallOnData.Actor.DeleteRamind(order.ID, entity.CallUserID, entity.CallOnDateTime);
            //IEnumerable<int> intLst=(from per in callOnData where per.IsCallOn == false select per.ID); 
            int returnData = CallOnData.Actor.Insert(entity);
            if (isSetNextCallBackTime == 1)
            {
                IList<CallOnData> callOnData = CallOnData.Actor.GetByUserIDAllCallOnData(entity.CallUserID, 0, 0, sectionStr);
                if (callOnData.Count < 20)
                {
                    CallOnData entitys = new CallOnData();

                    string date = GetFormString(form, "nextCallBackTime");
                    string time = GetFormString(form, "callOnTimeSelecthours");
                    DateTime callOnDateTime = Convert.ToDateTime(date + " " + time);

                    entitys.CallOnDateTime = callOnDateTime;// GetFormDate(form, "nextCallBackTime").AddHours(GetFormInt(form, "callOnTimeSelecthours", 0));
                    entitys.CallUserID = CurrentUser.ID;
                    entitys.IsCallOn = false;
                    entitys.OrdersID = orderID;
                    entitys.IsValid = 1;
                    CallOnData.Actor.Insert(entitys);
                }
            }
            return returnData > 0 ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }
        #endregion

        [HttpPost]
        public JsonResult MyselfOrderDelete(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AP_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            string ids = GetFormString(form, "ids");
            if (ids != "")
            {
                string[] idstr = StringUtility.Split(ids, ',');
                foreach (string item in idstr)
                {
                    //是否有此科室管理权限，是否有删除权限，否则不让删。
                    Orders entity = Orders.Actor.GetByID(int.Parse(item));
                    if (entity != null)
                    {
                        entity.IsDelete = 1;
                        Orders.Actor.Update(entity);
                        return Json(new { flag = "success" });
                    }
                }
            }
            return Json(new { flag = "error" });
        }

        #endregion

        #region 密码验证
        public ActionResult Password()
        {
            //if (!LoginManager.HasAutority(AuthorityConst.G_USERS))
            //{
            //    LoginManager.GoUnAccessPage();
            //}
            int orderID = RequestUtility.GetQueryInt("id", -1);
            ViewData["OrderID"] = orderID;
            //UserInfo userEntity = Easom.Core.UserInfo.Actor.GetByID(CurrentUser.ID);
            //return View(userEntity);
            return View();
        }
        [HttpPost]
        public JsonResult Password(FormCollection form)
        {
            //oldpassword

            Hospital entity = CurrentUser.Hospital;
            if (entity != null)
            {
                string aa = SecurityUtility.DESDecrypt(entity.PassWord.ToString());
                string safeurl = "";
                if (GetFormString(form, "oldpassword") != aa)
                {
                    return Json(new { flag = "oldpwderror" });
                }
                else
                {
                    safeurl = GenerateRandom(12);
                    entity.HSafeCode = safeurl;
                }
                Easom.Core.Hospital.Actor.Update(entity);
                return Json(new { flag = "success", code = safeurl });
            }
            return Json(new { flag = "error" });
        }
        private static char[] constant =
      {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
      };
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
        #endregion

        #region 设置密码验证
        public ActionResult SetPassword()
        {
            //if (!LoginManager.HasAutority(AuthorityConst.G_USERS))
            //{
            //    LoginManager.GoUnAccessPage();
            //}
            int orderID = RequestUtility.GetQueryInt("id", -1);
            ViewData["OrderID"] = orderID;
            //UserInfo userEntity = Easom.Core.UserInfo.Actor.GetByID(CurrentUser.ID);
            //return View(userEntity);
            return View();
        }
        [HttpPost]
        public JsonResult SetPassword(FormCollection form)
        {
            //oldpassword
            string aa = SecurityUtility.DESDecrypt(CurrentUser.Hospital.PassWord.ToString());
            string safeurl = "";
            if (GetFormString(form, "oldpassword") != aa)
            {
                return Json(new { flag = "oldpwderror" });
            }
            else
            {
                safeurl = GenerateRandom(12);
                CurrentUser.Hospital.HSafeCode = safeurl;
            }
            Easom.Core.Hospital.Actor.Update(CurrentUser.Hospital);
            return Json(new { flag = "success", code = safeurl });
        }
        #endregion
    }
}
