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

namespace Easom.Web.Support.Controllers
{
    public class WapCustomerServiceController : SysAdminBaseController
    {
        #region 预约表操作

        public ActionResult OrderIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_YYGL))
            {
                LoginManager.GoUnAccessPage();
            }
            int pageIndex = RequestUtility.GetQueryInt("pageIndex", -1);
            string searchName = RequestUtility.GetQueryString("searchName");
            string globalKeywords = RequestUtility.GetQueryString("globalKeyword");
            string timeKinds = RequestUtility.GetQueryString("timekinds");
            string startTime = RequestUtility.GetQueryString("starttime");
            string endTime = RequestUtility.GetQueryString("endtime");
            string section = RequestUtility.GetQueryString("section");
            string doctor = RequestUtility.GetQueryString("doctor");
            string diseaseKind = RequestUtility.GetQueryString("diseasekind");
            string arriveState = RequestUtility.GetQueryString("arrivestate");
            string mediaSource = RequestUtility.GetQueryString("mediasource");
            //创建人
            string keys = RequestUtility.GetQueryString("keys");
            string customer = RequestUtility.GetQueryString("userInfo");
            string department = RequestUtility.GetQueryString("department");
            int repeatSearchData = RequestUtility.GetQueryInt("repeatsearchdata", -1);
            string repeatStartTime = RequestUtility.GetQueryString("repeatstarttime");
            string repeatEndTime = RequestUtility.GetQueryString("repeatendtime");
            string callOn = RequestUtility.GetQueryString("uncallOnData");
            string where = " IsDelete=0 ";
            string repeat = string.Empty;
            if (CurrentUser != null)
            {
                where += " AND HospitalID=" + CurrentUser.HospitalID + " AND OrderState=" + (int)OrderStateEnum.OrdersData + " ";
            }
            if (!string.IsNullOrEmpty(globalKeywords))
            {
                globalKeywords = globalKeywords.Trim();
                where += " AND (Name LIKE '%" + globalKeywords + "%' OR Telephone LIKE '%" + globalKeywords + "%'" + " OR charindex('" + globalKeywords + "',ExpertIdentifier)>0)";
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

            if (callOn != null && callOn != "")
            {
                string sectionStr = "";
                foreach (Section sItem in CurrentUser.UserSectionList)
                {
                    sectionStr += "" + sItem.ID + ",";
                }
                sectionStr += " -100 ";

                IList<CallOnData> callOnData = CallOnData.Actor.GetByUserIDAndState(CurrentUser.ID, 0, 0, sectionStr);
                where += " AND (";
                foreach (CallOnData cal in callOnData)
                {
                    where += " ID=" + cal.OrdersID + " or ";
                }
                where += " ID=-100 )";
            }
            if (CurrentUser.UserRole.ManagerRoles != null)
            {
                where += " AND (";
                foreach (Role roleID in CurrentUser.UserRole.ManagerRoles)
                {
                    where += " RoleID=" + roleID.ID + " or ";
                }
                where += " RoleID=-100 )";
            }
            int pageCount = 0;
            int pageSize = 5;
            IList<Orders> entityList = Orders.Actor.SearchView(out pageCount, pageIndex, pageSize, where, "UpdateTime", true);
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageSize"] = pageSize;
            ViewData["pageCount"] = pageCount;
            ViewData["searchName"] = searchName;
            ViewData["pbPreFix"] = "/wapcustomerservice/orderindex?globalKeyword=" + globalKeywords + "&searchName=" + searchName + "&starttime=" + startTime + "" + "&endtime=" + endTime + "" + "&arrivestate=" + arriveState + "&timekinds=" + timeKinds + "&pageIndex=";
            return View(entityList);
        }
        #endregion

        #region 查看聊天内容

        public ActionResult ChatContent()
        {
            int orderID = RequestUtility.GetQueryInt("id", -1);
            Orders orders = Orders.Actor.GetByID(orderID);
            if (orderID > -1)
            {
                string section = ",";
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
            return RedirectToAction("Index", "Error");
        }
        #endregion

    }
}
