using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easom.Support.App_Start;
using System.Data;
using Easom.Core;
using Easom.Core.Support;
using CHCMS.Utility;
using Easom.Support.ActionFilters;

namespace Easom.Support.Controllers
{
    public class StatisticalTableController : SysAdminBaseController
    {
        #region 统计图

        #region 客服明细报表

        /// <summary>
        /// 角色明细报表
        /// </summary>
        /// <returns></returns>
        public ActionResult TJDepartmentFormsData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_MINGXI))
            {
                LoginManager.GoUnAccessPage();
            }
            ViewData["role"] = CurrentUser.UserRole.ManagerRoles; //Role.Actor.GetAllData();
            return View();
        }


        /// <summary>
        /// 角色明细报表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTJDepartmentFormsData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_MINGXI))
            {
                LoginManager.GoUnAccessPage();
            }
            string sectionStr = "";
            foreach (Section i in CurrentUser.UserSectionList)
            {
                sectionStr += "" + i.ID + ",";
            }
            sectionStr += "-1";
            int month = RequestUtility.GetQueryInt("month", 0);
            string role = RequestUtility.GetQueryString("roleStr");
            if (role.Length <= 0)
            {
                role = "-1";
            }
            else
            {
                role += "-1";
            }
            #region 网络统计客服明细
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            DateTime endTime = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1 - month).ToString("yyyy-MM-dd 23:59:59"));
            DataSet departmentKefu = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Web, beginTime, endTime);
            StringBuilder departmentHenadOne = new StringBuilder();
            StringBuilder departmentHenadTwo = new StringBuilder();
            DataTable departmentTable = departmentKefu.Tables[0];
            int cols = departmentTable.Columns.Count;
            int rows = departmentTable.Rows.Count;
            /*今天*/
            beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            DataSet dayDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Web, beginTime, endTime);
            DataTable dayDepartmentTA = dayDepartment.Tables[0];
            int dayTaRow = dayDepartmentTA.Rows.Count;
            /*第一个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1 - month).ToString("yyyy-MM-dd 23:59:59"));
            DataSet oneDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Web, beginTime, endTime);
            DataTable oneDepartmentTA = oneDepartment.Tables[0];
            int oneTaRow = oneDepartmentTA.Rows.Count;
            /*第二个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 1).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet twoDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Web, beginTime, endTime);
            DataTable twoDepartmentTA = twoDepartment.Tables[0];
            int twoTaRow = twoDepartmentTA.Rows.Count;
            /*第三个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 2).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet threeDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Web, beginTime, endTime);
            DataTable threeDepartmentTA = threeDepartment.Tables[0];
            int threeTaRow = threeDepartmentTA.Rows.Count;
            string dayDepartmentString = string.Empty;
            string oneDepartmentString = string.Empty;
            string twoDepartmentString = string.Empty;
            string threeDiseaString = string.Empty;
            for (int j = 0; j < oneTaRow; j++)
            {
                dayDepartmentString += "," + dayDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < oneTaRow; j++)
            {
                oneDepartmentString += "," + oneDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < twoTaRow; j++)
            {
                twoDepartmentString += "," + twoDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < threeTaRow; j++)
            {
                threeDiseaString += "," + threeDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < rows; j++)
            {
                int badds = 0;
                int barri = 0;
                int tradds = 0;
                int trarri = 0;
                int tadds = 0;
                int tarri = 0;
                departmentHenadOne.Append("<tr><td>" + departmentTable.Rows[j][1] + "</td>");
                if (dayDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < oneTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(dayDepartmentTA.Rows[k][0]))
                        {
                            departmentHenadOne.Append("<td>" + dayDepartmentTA.Rows[k][2] + "</td><td>" + dayDepartmentTA.Rows[k][3] + "</td><td>" + (Convert.ToDecimal(dayDepartmentTA.Rows[k][3]) / Convert.ToDecimal((int)dayDepartmentTA.Rows[k][2] == 0 ? 1 : (int)dayDepartmentTA.Rows[k][2])).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    departmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (oneDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < oneTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(oneDepartmentTA.Rows[k][0]))
                        {
                            badds = (int)oneDepartmentTA.Rows[k][2];
                            barri = (int)oneDepartmentTA.Rows[k][3];
                            departmentHenadOne.Append("<td>" + badds + "</td><td>" + barri + "</td><td>" + (Convert.ToDecimal(barri) / Convert.ToDecimal(badds == 0 ? 1 : badds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    departmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (twoDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < twoTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(twoDepartmentTA.Rows[k][0]))
                        {
                            tadds = (int)twoDepartmentTA.Rows[k][2];
                            tarri = (int)twoDepartmentTA.Rows[k][3];
                            departmentHenadOne.Append("<td>" + tadds + "</td><td>" + tarri + "</td><td>" + (Convert.ToDecimal(tarri) / Convert.ToDecimal(tadds == 0 ? 1 : tadds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    departmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (threeDiseaString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < threeTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(threeDepartmentTA.Rows[k][0]))
                        {
                            tradds = (int)threeDepartmentTA.Rows[k][2];
                            trarri = (int)threeDepartmentTA.Rows[k][3];
                            departmentHenadOne.Append("<td>" + tradds + "</td><td>" + trarri + "</td><td>" + (Convert.ToDecimal(trarri) / Convert.ToDecimal(tradds == 0 ? 1 : tradds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    departmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }


                int[] add = { tadds, tradds };
                int result = add.Max();
                result = (badds - result);
                int[] arr = { trarri, tarri };
                int result2 = arr.Max();
                result2 = (barri - result2);
                if (result > 0)
                {
                    departmentHenadOne.Append("<td><b style='color:red'>↑↑&nbsp;" + result + "</b></td>");
                }
                else if (result == 0)
                {
                    departmentHenadOne.Append("<td><b style='color:gray'>＝&nbsp;" + result + "</b></td>");
                }
                else
                {
                    departmentHenadOne.Append("<td><b style='color:green'>↓↓&nbsp;" + Math.Abs(result) + "</b></td>");
                }
                if (result2 > 0)
                {
                    departmentHenadOne.Append("<td><b style='color:red'>↑↑&nbsp;" + result2 + "</b></td>");
                }
                else if (result2 == 0)
                {
                    departmentHenadOne.Append("<td><b style='color:gray'>＝&nbsp;" + result2 + "</b></td>");
                }
                else
                {
                    departmentHenadOne.Append("<td><b style='color:green'>↓↓&nbsp;" + Math.Abs(result2) + "</b></td>");
                }

                departmentHenadOne.Append("</tr>");
            }
            #endregion
            //ViewData["dt1"] = departmentHenadOne;
            #region 电话统计
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1 - month).ToString("yyyy-MM-dd 23:59:59"));
            departmentKefu = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Telephone, beginTime, endTime);
            StringBuilder TdepartmentHenadOne = new StringBuilder();
            departmentTable = departmentKefu.Tables[0];
            cols = departmentTable.Columns.Count;
            rows = departmentTable.Rows.Count;
            /*今天*/
            beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            dayDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Telephone, beginTime, endTime);
            dayDepartmentTA = dayDepartment.Tables[0];
            dayTaRow = dayDepartmentTA.Rows.Count;
            /*第一个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1 - month).ToString("yyyy-MM-dd 23:59:59"));
            oneDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Telephone, beginTime, endTime);
            oneDepartmentTA = oneDepartment.Tables[0];
            oneTaRow = oneDepartmentTA.Rows.Count;
            /*第二个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 1).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            twoDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Telephone, beginTime, endTime);
            twoDepartmentTA = twoDepartment.Tables[0];
            twoTaRow = twoDepartmentTA.Rows.Count;
            /*第三个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 2).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            threeDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Telephone, beginTime, endTime);
            threeDepartmentTA = threeDepartment.Tables[0];
            threeTaRow = threeDepartmentTA.Rows.Count;
            dayDepartmentString = string.Empty;
            oneDepartmentString = string.Empty;
            twoDepartmentString = string.Empty;
            threeDiseaString = string.Empty;
            for (int j = 0; j < oneTaRow; j++)
            {
                dayDepartmentString += "," + dayDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < oneTaRow; j++)
            {
                oneDepartmentString += "," + oneDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < twoTaRow; j++)
            {
                twoDepartmentString += "," + twoDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < threeTaRow; j++)
            {
                threeDiseaString += "," + threeDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < rows; j++)
            {
                int badds = 0;
                int barri = 0;

                int tradds = 0;
                int trarri = 0;
                int tadds = 0;
                int tarri = 0;
                TdepartmentHenadOne.Append("<tr><td>" + departmentTable.Rows[j][1] + "</td>");
                if (dayDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < oneTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(dayDepartmentTA.Rows[k][0]))
                        {
                            TdepartmentHenadOne.Append("<td>" + dayDepartmentTA.Rows[k][2] + "</td><td>" + dayDepartmentTA.Rows[k][3] + "</td><td>" + (Convert.ToDecimal(dayDepartmentTA.Rows[k][3]) / Convert.ToDecimal((int)dayDepartmentTA.Rows[k][2] == 0 ? 1 : (int)dayDepartmentTA.Rows[k][2])).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    TdepartmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (oneDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < oneTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(oneDepartmentTA.Rows[k][0]))
                        {
                            badds = (int)oneDepartmentTA.Rows[k][2];
                            barri = (int)oneDepartmentTA.Rows[k][3];
                            TdepartmentHenadOne.Append("<td>" + badds + "</td><td>" + barri + "</td><td>" + (Convert.ToDecimal(barri) / Convert.ToDecimal(badds == 0 ? 1 : badds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    TdepartmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (twoDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < twoTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(twoDepartmentTA.Rows[k][0]))
                        {
                            tadds = (int)twoDepartmentTA.Rows[k][2];
                            tarri = (int)twoDepartmentTA.Rows[k][3];
                            TdepartmentHenadOne.Append("<td>" + tadds + "</td><td>" + tarri + "</td><td>" + (Convert.ToDecimal(tarri) / Convert.ToDecimal(tadds == 0 ? 1 : tadds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    TdepartmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (threeDiseaString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < threeTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(threeDepartmentTA.Rows[k][0]))
                        {
                            tradds = (int)threeDepartmentTA.Rows[k][2];
                            trarri = (int)threeDepartmentTA.Rows[k][3];
                            TdepartmentHenadOne.Append("<td>" + tradds + "</td><td>" + trarri + "</td><td>" + (Convert.ToDecimal(trarri) / Convert.ToDecimal(tradds == 0 ? 1 : tradds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    TdepartmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }


                int[] add = { tadds, tradds };
                int result = add.Max();
                result = (badds - result);
                int[] arr = { trarri, tarri };
                int result2 = arr.Max();
                result2 = (barri - result2);
                if (result > 0)
                {
                    TdepartmentHenadOne.Append("<td><b style='color:red'>↑↑&nbsp;" + result + "</b></td>");
                }
                else if (result == 0)
                {
                    TdepartmentHenadOne.Append("<td><b style='color:gray'>＝&nbsp;" + result + "</b></td>");
                }
                else
                {
                    TdepartmentHenadOne.Append("<td><b style='color:green'>↓↓&nbsp;" + Math.Abs(result) + "</b></td>");
                }
                if (result2 > 0)
                {
                    TdepartmentHenadOne.Append("<td><b style='color:red'>↑↑&nbsp;" + result2 + "</b></td>");
                }
                else if (result2 == 0)
                {
                    TdepartmentHenadOne.Append("<td><b style='color:gray'>＝&nbsp;" + result2 + "</b></td>");
                }
                else
                {
                    TdepartmentHenadOne.Append("<td><b style='color:green'>↓↓&nbsp;" + Math.Abs(result2) + "</b></td>");
                }

                TdepartmentHenadOne.Append("</tr>");
            }
            #endregion
            //ViewData["dt2"] = TdepartmentHenadOne;
            #region 其他统计
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1 - month).ToString("yyyy-MM-dd 23:59:59"));
            departmentKefu = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Others, beginTime, endTime);
            StringBuilder AdepartmentHenadOne = new StringBuilder();
            departmentTable = departmentKefu.Tables[0];
            cols = departmentTable.Columns.Count;
            rows = departmentTable.Rows.Count;
            /*今天*/
            beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            dayDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Others, beginTime, endTime);
            dayDepartmentTA = dayDepartment.Tables[0];
            dayTaRow = dayDepartmentTA.Rows.Count;
            /*第一个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1 - month).ToString("yyyy-MM-dd 23:59:59"));
            oneDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Others, beginTime, endTime);
            oneDepartmentTA = oneDepartment.Tables[0];
            oneTaRow = oneDepartmentTA.Rows.Count;
            /*第二个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 1).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            twoDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Others, beginTime, endTime);
            twoDepartmentTA = twoDepartment.Tables[0];
            twoTaRow = twoDepartmentTA.Rows.Count;
            /*第三个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 2).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            threeDepartment = Orders.Actor.GetDepartmentData(role, sectionStr, (int)CountTypeEnum.Others, beginTime, endTime);
            threeDepartmentTA = threeDepartment.Tables[0];
            threeTaRow = threeDepartmentTA.Rows.Count;
            dayDepartmentString = string.Empty;
            oneDepartmentString = string.Empty;
            twoDepartmentString = string.Empty;
            threeDiseaString = string.Empty;
            for (int j = 0; j < oneTaRow; j++)
            {
                dayDepartmentString += "," + dayDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < oneTaRow; j++)
            {
                oneDepartmentString += "," + oneDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < twoTaRow; j++)
            {
                twoDepartmentString += "," + twoDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < threeTaRow; j++)
            {
                threeDiseaString += "," + threeDepartmentTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < rows; j++)
            {
                int badds = 0;
                int barri = 0;

                int tradds = 0;
                int trarri = 0;
                int tadds = 0;
                int tarri = 0;
                AdepartmentHenadOne.Append("<tr><td>" + departmentTable.Rows[j][1] + "</td>");
                if (dayDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < oneTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(dayDepartmentTA.Rows[k][0]))
                        {
                            AdepartmentHenadOne.Append("<td>" + dayDepartmentTA.Rows[k][2] + "</td><td>" + dayDepartmentTA.Rows[k][3] + "</td><td>" + (Convert.ToDecimal(dayDepartmentTA.Rows[k][3]) / Convert.ToDecimal((int)dayDepartmentTA.Rows[k][2] == 0 ? 1 : (int)dayDepartmentTA.Rows[k][2])).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    AdepartmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (oneDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < oneTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(oneDepartmentTA.Rows[k][0]))
                        {
                            badds = (int)oneDepartmentTA.Rows[k][2];
                            barri = (int)oneDepartmentTA.Rows[k][3];
                            AdepartmentHenadOne.Append("<td>" + badds + "</td><td>" + barri + "</td><td>" + (Convert.ToDecimal(barri) / Convert.ToDecimal(badds == 0 ? 1 : badds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    AdepartmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (twoDepartmentString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < twoTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(twoDepartmentTA.Rows[k][0]))
                        {
                            tadds = (int)twoDepartmentTA.Rows[k][2];
                            tarri = (int)twoDepartmentTA.Rows[k][3];
                            AdepartmentHenadOne.Append("<td>" + tadds + "</td><td>" + tarri + "</td><td>" + (Convert.ToDecimal(tarri) / Convert.ToDecimal(tadds == 0 ? 1 : tadds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    AdepartmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (threeDiseaString.IndexOf("," + departmentTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < threeTaRow; k++)
                    {
                        if (Convert.ToInt32(departmentTable.Rows[j][0]) == Convert.ToInt32(threeDepartmentTA.Rows[k][0]))
                        {
                            tradds = (int)threeDepartmentTA.Rows[k][2];
                            trarri = (int)threeDepartmentTA.Rows[k][3];
                            AdepartmentHenadOne.Append("<td>" + tradds + "</td><td>" + trarri + "</td><td>" + (Convert.ToDecimal(trarri) / Convert.ToDecimal(tradds == 0 ? 1 : tradds)).ToString("p2") + "</td>");
                        }
                    }
                }
                else
                {
                    AdepartmentHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                int[] add = { tadds, tradds };
                int result = add.Max();
                result = (badds - result);
                int[] arr = { trarri, tarri };
                int result2 = arr.Max();
                result2 = (barri - result2);
                if (result > 0)
                {
                    AdepartmentHenadOne.Append("<td><b style='color:red'>↑↑&nbsp;" + result + "</b></td>");
                }
                else if (result == 0)
                {
                    AdepartmentHenadOne.Append("<td><b style='color:gray'>＝&nbsp;" + result2 + "</b></td>");
                }
                else
                {
                    AdepartmentHenadOne.Append("<td><b style='color:green'>↓↓&nbsp;" + Math.Abs(result) + "</b></td>");
                }
                if (result2 > 0)
                {
                    AdepartmentHenadOne.Append("<td><b style='color:red'>↑↑&nbsp;" + result2 + "</b></td>");
                }
                else if (result2 == 0)
                {
                    AdepartmentHenadOne.Append("<td><b style='color:gray'>＝&nbsp;" + result2 + "</b></td>");
                }
                else
                {
                    AdepartmentHenadOne.Append("<td><b style='color:green'>↓↓&nbsp;" + Math.Abs(result2) + "</b></td>");
                }

                AdepartmentHenadOne.Append("</tr>");
            }
            #endregion
            //ViewData["dt3"] = AdepartmentHenadOne;
            return Json(new { flag = "success", net = departmentHenadOne.ToString(), tel = TdepartmentHenadOne.ToString(), oth = AdepartmentHenadOne.ToString() });
        }

        #endregion

        #region 月趋势统计图

        public ActionResult MothTrendForms()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_ZHOUYUE))
            {
                LoginManager.GoUnAccessPage();
            }
            int moth = RequestUtility.GetQueryInt("month", 0);
            int year = Convert.ToInt32(DateTime.Now.AddMonths(-moth).ToString("yyyy"));
            int month = Convert.ToInt32(DateTime.Now.AddMonths(-moth).ToString("MM"));
            ViewData["MothTitle"] = month;
            int days = DateTime.DaysInMonth(year, month);
            int hospital = CurrentUser.HospitalID;
            string sectionStr = "";
            foreach (Section i in CurrentUser.UserSectionList)
            {
                sectionStr += "" + i.ID + ",";
            }
            sectionStr += "-1";
            StringBuilder MonthWebData = new StringBuilder();
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-moth).ToString("yyyy-MM-01 00:00:00"));
            DateTime endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - moth).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet addSet = Orders.Actor.GetMonthDataByDay(sectionStr, beginTime, endTime, (int)CountTypeEnum.Web, 0);
            DataSet arrSet = Orders.Actor.GetMonthDataByDay(sectionStr, beginTime, endTime, (int)CountTypeEnum.Web, 1);
            DataTable addSetTable = addSet.Tables[0];
            DataTable arrSetTable = arrSet.Tables[0];
            for (int i = 0; i < days; i++)
            {
                string adds = "0";
                string arrs = "0";
                var add = from row in addSetTable.AsEnumerable()
                          where (row.Field<int>("dayss") == Convert.ToInt32(beginTime.AddDays(i).ToString("dd")) && row.Field<int>("Months") == Convert.ToInt32(beginTime.AddDays(i).ToString("MM")))
                          select row;
                var arr = from row in arrSetTable.AsEnumerable()
                          where (row.Field<int>("dayss") == Convert.ToInt32(beginTime.AddDays(i).ToString("dd")) && row.Field<int>("Months") == Convert.ToInt32(beginTime.AddDays(i).ToString("MM")))
                          select row;
                foreach (var item in add)
                {
                    if (item["adds"].ToString() != null && item["adds"].ToString().Length > 0)
                    {
                        adds = item["adds"].ToString();
                    }
                    else
                    {
                        adds = "0";
                    }
                }
                foreach (var item in arr)
                {
                    if (item["adds"].ToString() != null && item["adds"].ToString().Length > 0)
                    {
                        arrs = item["adds"].ToString();
                    }
                    else
                    {
                        arrs = "0";
                    }
                }
                MonthWebData.Append("<tr><td>" + beginTime.AddDays(i).ToString("dd天") + "</td><td>" + adds + "</td><td>" + arrs + "</td></tr>");
            }
            ViewData["MonthWebData"] = MonthWebData.ToString();



            StringBuilder MonthTelData = new StringBuilder();
            addSet = Orders.Actor.GetMonthDataByDay(sectionStr, beginTime, endTime, (int)CountTypeEnum.Telephone, 0);
            arrSet = Orders.Actor.GetMonthDataByDay(sectionStr, beginTime, endTime, (int)CountTypeEnum.Telephone, 1);
            addSetTable = addSet.Tables[0];
            arrSetTable = arrSet.Tables[0];
            for (int i = 0; i < days; i++)
            {
                string adds = "0";
                string arrs = "0";
                var add = from row in addSetTable.AsEnumerable()
                          where (row.Field<int>("dayss") == Convert.ToInt32(beginTime.AddDays(i).ToString("dd")) && row.Field<int>("Months") == Convert.ToInt32(beginTime.AddDays(i).ToString("MM")))
                          select row;
                var arr = from row in arrSetTable.AsEnumerable()
                          where (row.Field<int>("dayss") == Convert.ToInt32(beginTime.AddDays(i).ToString("dd")) && row.Field<int>("Months") == Convert.ToInt32(beginTime.AddDays(i).ToString("MM")))
                          select row;
                foreach (var item in add)
                {
                    if (item["adds"].ToString() != null && item["adds"].ToString().Length > 0)
                    {
                        adds = item["adds"].ToString();
                    }
                    else
                    {
                        adds = "0";
                    }
                }
                foreach (var item in arr)
                {
                    if (item["adds"].ToString() != null && item["adds"].ToString().Length > 0)
                    {
                        arrs = item["adds"].ToString();
                    }
                    else
                    {
                        arrs = "0";
                    }
                }
                MonthTelData.Append("<tr><td>" + beginTime.AddDays(i).ToString("dd天") + "</td><td>" + adds + "</td><td>" + arrs + "</td></tr>");
            }
            ViewData["MonthTelData"] = MonthTelData.ToString();


            StringBuilder MonthAntData = new StringBuilder();
            addSet = Orders.Actor.GetMonthDataByDay(sectionStr, beginTime, endTime, (int)CountTypeEnum.Others, 0);
            arrSet = Orders.Actor.GetMonthDataByDay(sectionStr, beginTime, endTime, (int)CountTypeEnum.Others, 1);
            addSetTable = addSet.Tables[0];
            arrSetTable = arrSet.Tables[0];
            for (int i = 0; i < days; i++)
            {
                string adds = "0";
                string arrs = "0";
                var add = from row in addSetTable.AsEnumerable()
                          where (row.Field<int>("dayss") == Convert.ToInt32(beginTime.AddDays(i).ToString("dd")) && row.Field<int>("Months") == Convert.ToInt32(beginTime.AddDays(i).ToString("MM")))
                          select row;
                var arr = from row in arrSetTable.AsEnumerable()
                          where (row.Field<int>("dayss") == Convert.ToInt32(beginTime.AddDays(i).ToString("dd")) && row.Field<int>("Months") == Convert.ToInt32(beginTime.AddDays(i).ToString("MM")))
                          select row;
                foreach (var item in add)
                {
                    if (item["adds"].ToString() != null && item["adds"].ToString().Length > 0)
                    {
                        adds = item["adds"].ToString();
                    }
                    else
                    {
                        adds = "0";
                    }
                }
                foreach (var item in arr)
                {
                    if (item["adds"].ToString() != null && item["adds"].ToString().Length > 0)
                    {
                        arrs = item["adds"].ToString();
                    }
                    else
                    {
                        arrs = "0";
                    }
                }
                MonthAntData.Append("<tr><td>" + beginTime.AddDays(i).ToString("dd天") + "</td><td>" + adds + "</td><td>" + arrs + "</td></tr>");
            }
            ViewData["MonthAntData"] = MonthAntData.ToString();


            StringBuilder MonthCountData = new StringBuilder();
            addSet = Orders.Actor.GetMonthDataByDay(sectionStr, beginTime, endTime, -1, 0);
            arrSet = Orders.Actor.GetMonthDataByDay(sectionStr, beginTime, endTime, -1, 1);
            addSetTable = addSet.Tables[0];
            arrSetTable = arrSet.Tables[0];
            for (int i = 0; i < days; i++)
            {
                string adds = "0";
                string arrs = "0";
                var add = from row in addSetTable.AsEnumerable()
                          where (row.Field<int>("dayss") == Convert.ToInt32(beginTime.AddDays(i).ToString("dd")) && row.Field<int>("Months") == Convert.ToInt32(beginTime.AddDays(i).ToString("MM")))
                          select row;
                var arr = from row in arrSetTable.AsEnumerable()
                          where (row.Field<int>("dayss") == Convert.ToInt32(beginTime.AddDays(i).ToString("dd")) && row.Field<int>("Months") == Convert.ToInt32(beginTime.AddDays(i).ToString("MM")))
                          select row;
                foreach (var item in add)
                {
                    if (item["adds"].ToString() != null && item["adds"].ToString().Length > 0)
                    {
                        adds = item["adds"].ToString();
                    }
                    else
                    {
                        adds = "0";
                    }
                }
                foreach (var item in arr)
                {
                    if (item["adds"].ToString() != null && item["adds"].ToString().Length > 0)
                    {
                        arrs = item["adds"].ToString();
                    }
                    else
                    {
                        arrs = "0";
                    }
                }
                MonthCountData.Append("<tr><td>" + beginTime.AddDays(i).ToString("dd天") + "</td><td>" + adds + "</td><td>" + arrs + "</td></tr>");
            }
            ViewData["MonthCountData"] = MonthCountData.ToString();

            return View();
        }

        #endregion

        #region 到院对比报表
        /// <summary>
        /// 到院对比
        /// </summary>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult CustomMothTrendforms()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_DAOYUAN))
            {
                LoginManager.GoUnAccessPage();
            }
            string sectionStr = "";
            foreach (Section i in CurrentUser.UserSectionList)
            {
                sectionStr += "" + i.ID + ",";
            }
            sectionStr += "-1";

            DateTime beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
            DateTime endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            int countAdds = 0;
            int countOrders = 0;
            int countArrive = 0;
            int adds = 0;
            int orders = 0;
            int arrive = 0;
            StringBuilder todayData = new StringBuilder();
            Orders.Actor.GetDuiBiData(out adds, out orders, out arrive, beginTime, endTime, sectionStr, (Int32)CountTypeEnum.Web);
            countAdds = countAdds + adds;
            countOrders = countOrders + orders;
            countArrive = countArrive + arrive;
            todayData.Append("<tr style='background: #F0F0F0;' onmouseover=\"this.style.background='#FFFF99'\" onmouseout=\"this.style.background='#F0F0F0'\">");
            todayData.Append("<td style='border-right: 1px solid #ccc;'>网络：</td><td style='border-right: 1px solid #ccc;'>");
            todayData.Append("预约&nbsp;<a href=\"javascript:add('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',0)\" onmouseover=\"this.style.color='#FF0000'\" onmouseout=\"this.style.color='#3BA0D2'\">" + adds + "</a>");
            todayData.Append("</td><td style='border-right: 1px solid #ccc;'>预到 <a href=\"javascript:order('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',0)\" onmouseover=\"this.style.color='#FF0000'\" onmouseout=\"this.style.color='#3BA0D2'\">" + orders + "</a>");
            todayData.Append("</td><td style='border-right: 1px solid #ccc;'>实到  <a href=\"javascript:arrive('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',0)\" onmouseover=\"this.style.color='#FF0000'\"");
            todayData.Append("onmouseout=\"this.style.color='#3BA0D2'\">" + arrive + "</a></td></tr>");
            adds = 0;
            orders = 0;
            arrive = 0;
            Orders.Actor.GetDuiBiData(out adds, out orders, out arrive, beginTime, endTime, sectionStr, (Int32)CountTypeEnum.Telephone);
            countAdds = countAdds + adds;
            countOrders = countOrders + orders;
            countArrive = countArrive + arrive;
            todayData.Append("<tr style='color:gray;background: #fff;' onmouseover=\"this.style.background='#FFFF99'\" onmouseout=\"this.style.background='#fff'\">");
            todayData.Append("<td style='border-right: 1px solid #ccc;'>电话：</td><td style='border-right: 1px solid #ccc;'>");
            todayData.Append("预约&nbsp;<a href=\"javascript:add('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',1)\" onmouseover=\"this.style.color='#FF0000'\" onmouseout=\"this.style.color='#3BA0D2'\">" + adds + "</a>");
            todayData.Append("</td><td style='border-right: 1px solid #ccc;'>预到 <a href=\"javascript:order('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',1)\" onmouseover=\"this.style.color='#FF0000'\" onmouseout=\"this.style.color='#3BA0D2'\">" + orders + "</a>");
            todayData.Append("</td><td style='border-right: 1px solid #ccc;'>实到  <a href=\"javascript:arrive('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',1)\" onmouseover=\"this.style.color='#FF0000'\"");
            todayData.Append("onmouseout=\"this.style.color='#3BA0D2'\">" + arrive + "</a></td></tr>");
            adds = 0;
            orders = 0;
            arrive = 0;
            Orders.Actor.GetDuiBiData(out adds, out orders, out arrive, beginTime, endTime, sectionStr, (Int32)CountTypeEnum.Others);
            countAdds = countAdds + adds;
            countOrders = countOrders + orders;
            countArrive = countArrive + arrive;
            todayData.Append("<tr style='background: #F0F0F0;' onmouseover=\"this.style.background='#FFFF99'\" onmouseout=\"this.style.background='#F0F0F0'\">");
            todayData.Append("<td style='border-right: 1px solid #ccc;'>其他：</td><td style='border-right: 1px solid #ccc;'>");
            todayData.Append("预约&nbsp;<a href=\"javascript:add('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',2)\" onmouseover='this.style.color='#FF0000'' onmouseout='this.style.color='#3BA0D2''>" + adds + "</a>");
            todayData.Append("</td><td style='border-right: 1px solid #ccc;'>预到 <a href=\"javascript:order('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',2)\" onmouseover=\"this.style.color='#FF0000'\" onmouseout=\"this.style.color='#3BA0D2'\">" + orders + "</a>");
            todayData.Append("</td><td style='border-right: 1px solid #ccc;'>实到  <a href=\"javascript:arrive('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',2)\" onmouseover=\"this.style.color='#FF0000'\"");
            todayData.Append("onmouseout=\"this.style.color='#3BA0D2'\">" + arrive + "</a></td></tr>");

            todayData.Append("<tr style='color:gray;background: #fff;' onmouseover=\"this.style.background='#FFFF99'\" onmouseout=\"this.style.background='#fff'\">");
            todayData.Append("<td style='border-right: 1px solid #ccc;'>总计：</td><td style='border-right: 1px solid #ccc;'>");
            todayData.Append("预约&nbsp;<a href=\"javascript:add('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',-1)\" onmouseover='this.style.color='#FF0000'' onmouseout='this.style.color='#3BA0D2''>" + countAdds + "</a>");
            todayData.Append("</td><td style='border-right: 1px solid #ccc;'>预到 <a href=\"javascript:order('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',-1)\" onmouseover=\"this.style.color='#FF0000'\" onmouseout=\"this.style.color='#3BA0D2'\">" + countOrders + "</a>");
            todayData.Append("</td><td style='border-right: 1px solid #ccc;'>实到  <a href=\"javascript:arrive('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',-1)\" onmouseover=\"this.style.color='#FF0000'\"");
            todayData.Append("onmouseout=\"this.style.color='#3BA0D2'\">" + countArrive + "</a></td></tr>");
            ViewData["today"] = todayData;
            return View();
        }

        [AuthorityActionFilter]
        public ActionResult Addforms()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_DAOYUAN))
            {
                LoginManager.GoUnAccessPage();
            }

            string[] classCss = { "table-primary", "table-success", "table-warning", "table-danger", "table-info", "table-dark" };
            Random ran = new Random();
            int indexCss = ran.Next(0, 5);

            string sectionStr = "";
            foreach (Section i in CurrentUser.UserSectionList)
            {
                sectionStr += "" + i.ID + ",";
            }
            sectionStr += "-1";
            DateTime beginTimes = RequestUtility.GetQueryDateTime("starttime");
            DateTime endTimes = RequestUtility.GetQueryDateTime("endtime");
            DateTime beginTime = Convert.ToDateTime(beginTimes.ToString("yyyy-MM-dd"));
            DateTime endTime = Convert.ToDateTime(endTimes.ToString("yyyy-MM-dd 23:59:59.99"));
            int countAdds = 0;
            int countOrders = 0;
            int countArrive = 0;
            int adds = 0;
            int orders = 0;
            int arrive = 0;
            StringBuilder todayData = new StringBuilder();
            Orders.Actor.GetDuiBiData(out adds, out orders, out arrive, beginTime, endTime, sectionStr, (Int32)CountTypeEnum.Web);
            countAdds = countAdds + adds;
            countOrders = countOrders + orders;
            countArrive = countArrive + arrive;

            todayData.Append("<div class=\"col-sm-6\"><table class=\"table table-bordered " + classCss[indexCss] + "\">");
            todayData.Append("<thead><tr><th colspan=\"4\">" + beginTime.ToString("yyyy-MM-dd") + "到" + endTime.ToString("yyyy-MM-dd") + "</tr></th></thead><tbody>");
            todayData.Append("<tr>");
            todayData.Append("<td>网络：</td><td>");
            todayData.Append("预约&nbsp;<a href=\"javascript:add('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',0)\">" + adds + "</a>");
            todayData.Append("</td><td>预到 <a href=\"javascript:order('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',0)\">" + orders + "</a>");
            todayData.Append("</td><td>实到  <a href=\"javascript:arrive('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',0)\"");
            todayData.Append("'>" + arrive + "</a></td></tr>");
            adds = 0;
            orders = 0;
            arrive = 0;
            Orders.Actor.GetDuiBiData(out adds, out orders, out arrive, beginTime, endTime, sectionStr, (Int32)CountTypeEnum.Telephone);
            countAdds = countAdds + adds;
            countOrders = countOrders + orders;
            countArrive = countArrive + arrive;
            todayData.Append("<tr>");
            todayData.Append("<td>电话：</td><td>");
            todayData.Append("预约&nbsp;<a href=\"javascript:add('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',1)\">" + adds + "</a>");
            todayData.Append("</td><td>预到 <a href=\"javascript:order('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',1)\">" + orders + "</a>");
            todayData.Append("</td><td>实到  <a href=\"javascript:arrive('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',1)\"");
            todayData.Append(">" + arrive + "</a></td></tr>");
            adds = 0;
            orders = 0;
            arrive = 0;
            Orders.Actor.GetDuiBiData(out adds, out orders, out arrive, beginTime, endTime, sectionStr, (Int32)CountTypeEnum.Others);
            countAdds = countAdds + adds;
            countOrders = countOrders + orders;
            countArrive = countArrive + arrive;
            todayData.Append("<tr>");
            todayData.Append("<td>其他：</td><td>");
            todayData.Append("预约&nbsp;<a href=\"javascript:add('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',2)\">" + adds + "</a>");
            todayData.Append("</td><td>预到 <a href=\"javascript:order('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',2)\">" + orders + "</a>");
            todayData.Append("</td><td>实到  <a href=\"javascript:arrive('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',2)\"");
            todayData.Append(">" + arrive + "</a></td></tr>");

            todayData.Append("<tr>");
            todayData.Append("<td>总计：</td><td>");
            todayData.Append("预约&nbsp;<a href=\"javascript:add('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',-1)\"'>" + countAdds + "</a>");
            todayData.Append("</td><td>预到 <a href=\"javascript:order('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',-1)\">" + countOrders + "</a>");
            todayData.Append("</td><td>实到  <a href=\"javascript:arrive('" + beginTime.ToString("yyyy-MM-dd") + "','" + endTime.ToString("yyyy-MM-dd") + "',-1)\"");
            todayData.Append(">" + countArrive + "</a></td></tr>");
            todayData.Append(" </tbody></table></div>");
            return Json(new { flag = "success", Data = todayData.ToString() }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 疾病统计图

        public ActionResult DiseaseStatisticsFrom()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_DISEASE))
            {
                LoginManager.GoUnAccessPage();
            }
            string sectionStr = "";
            foreach (Section i in CurrentUser.UserSectionList)
            {
                sectionStr += "" + i.ID + ",";
            }
            sectionStr += "-1";
            int month = RequestUtility.GetQueryInt("month", 0);
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            DateTime endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet adddisease = Orders.Actor.GetDiseaseByHospital(sectionStr, beginTime, endTime);
            StringBuilder diseaseHenadOne = new StringBuilder();
            StringBuilder diseaseHenadTwo = new StringBuilder();
            DataTable diseaseTable = adddisease.Tables[0];
            int cols = diseaseTable.Columns.Count;
            int rows = diseaseTable.Rows.Count;
            /*第一个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet oneDisease = Orders.Actor.GetDiseaseByHospital(sectionStr, beginTime, endTime);
            DataTable oneDiseaseTA = oneDisease.Tables[0];
            int oneTaRow = oneDiseaseTA.Rows.Count;
            /*第二个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 1).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet twoDisease = Orders.Actor.GetDiseaseByHospital(sectionStr, beginTime, endTime);
            DataTable twoDiseaseTA = twoDisease.Tables[0];
            int twoTaRow = twoDiseaseTA.Rows.Count;
            /*第三个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 2).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet threeDisease = Orders.Actor.GetDiseaseByHospital(sectionStr, beginTime, endTime);
            DataTable threeDiseaseTA = threeDisease.Tables[0];
            int threeTaRow = threeDiseaseTA.Rows.Count;
            string oneDiseaString = string.Empty;
            string twoDiseaString = string.Empty;
            string threeDiseaString = string.Empty;
            for (int j = 0; j < oneTaRow; j++)
            {
                oneDiseaString += "," + oneDiseaseTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < twoTaRow; j++)
            {
                twoDiseaString += "," + twoDiseaseTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < threeTaRow; j++)
            {
                threeDiseaString += "," + threeDiseaseTA.Rows[j][1] + ",";
            }
            for (int j = 0; j < rows; j++)
            {
                diseaseHenadOne.Append("<tr><td>" + diseaseTable.Rows[j][1] + "</td>");
                if (oneDiseaString.IndexOf("," + diseaseTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < oneTaRow; k++)
                    {
                        if (Convert.ToInt32(diseaseTable.Rows[j][0]) == Convert.ToInt32(oneDiseaseTA.Rows[k][0]))
                        {
                            diseaseHenadOne.Append("<td>" + oneDiseaseTA.Rows[k][2] + "</td><td>" + oneDiseaseTA.Rows[k][3] + "</td>");
                        }
                    }
                }
                else
                {
                    diseaseHenadOne.Append("<td>0</td><td>0</td>");
                }
                if (twoDiseaString.IndexOf("," + diseaseTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < twoTaRow; k++)
                    {
                        if (Convert.ToInt32(diseaseTable.Rows[j][0]) == Convert.ToInt32(twoDiseaseTA.Rows[k][0]))
                        {
                            diseaseHenadOne.Append("<td>" + twoDiseaseTA.Rows[k][2] + "</td><td>" + twoDiseaseTA.Rows[k][3] + "</td>");
                        }
                    }
                }
                else
                {
                    diseaseHenadOne.Append("<td>0</td><td>0</td>");
                }
                if (threeDiseaString.IndexOf("," + diseaseTable.Rows[j][1] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < threeTaRow; k++)
                    {
                        if (Convert.ToInt32(diseaseTable.Rows[j][0]) == Convert.ToInt32(threeDiseaseTA.Rows[k][0]))
                        {
                            diseaseHenadOne.Append("<td>" + threeDiseaseTA.Rows[k][2] + "</td><td>" + threeDiseaseTA.Rows[k][3] + "</td>");
                        }
                    }
                }
                else
                {
                    diseaseHenadOne.Append("<td>0</td><td>0</td>");
                }
                diseaseHenadOne.Append("</tr>");
            }
            int oneAdd = 0;
            int oneArrive = 0;
            int twoAdd = 0;
            int twoArrive = 0;
            int threeAdd = 0;
            int threeArrive = 0;
            for (int j = 0; j < oneTaRow; j++)
            {
                oneAdd = oneAdd + Convert.ToInt32(oneDiseaseTA.Rows[j][2]);
                oneArrive = oneArrive + Convert.ToInt32(oneDiseaseTA.Rows[j][3]);
            }
            for (int j = 0; j < twoTaRow; j++)
            {
                twoAdd = twoAdd + Convert.ToInt32(twoDiseaseTA.Rows[j][2]);
                twoArrive = twoArrive + Convert.ToInt32(twoDiseaseTA.Rows[j][3]);
            }
            for (int j = 0; j < threeTaRow; j++)
            {
                threeAdd = threeAdd + Convert.ToInt32(threeDiseaseTA.Rows[j][2]);
                threeArrive = threeArrive + Convert.ToInt32(threeDiseaseTA.Rows[j][3]);
            }
            diseaseHenadOne.Append("<tr>");
            diseaseHenadOne.Append("<td>合计</td>");
            diseaseHenadOne.Append("<td>" + oneAdd + "</td>");
            diseaseHenadOne.Append("<td>" + oneArrive + "</td>");
            diseaseHenadOne.Append("<td>" + twoAdd + "</td>");
            diseaseHenadOne.Append("<td>" + twoArrive + "</td>");
            diseaseHenadOne.Append("<td>" + threeAdd + "</td>");
            diseaseHenadOne.Append("<td>" + threeArrive + "</td>");
            diseaseHenadOne.Append("</tr>");
            diseaseHenadTwo.Append("</tr><th></th>");
            diseaseHenadTwo.Append("<th colspan='2'>" + DateTime.Now.AddMonths(-month).ToString("MM") + "月</th>");
            diseaseHenadTwo.Append("<th colspan='2'>" + DateTime.Now.AddMonths(-month - 1).ToString("MM") + "月</th>");
            diseaseHenadTwo.Append("<th colspan='2'>" + DateTime.Now.AddMonths(-month - 2).ToString("MM") + "月</th></tr>");

            ViewData["Heand"] = diseaseHenadOne;
            ViewData["HeandTwo"] = diseaseHenadTwo;
            return View();
        }

        #endregion

        #region 地区统计报表
        public ActionResult AreaForms()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_AREA))
            {
                LoginManager.GoUnAccessPage();
            }
            string sectionStr = "";
            foreach (Section i in CurrentUser.UserSectionList)
            {
                sectionStr += "" + i.ID + ",";
            }
            sectionStr += "-1";

            int oneAdd = 0;
            int oneArrive = 0;
            int twoAdd = 0;
            int twoArrive = 0;
            int threeAdd = 0;
            int threeArrive = 0;
            int BenDiAdds = 0;
            int BenDiArrive = 0;
            int month = RequestUtility.GetQueryInt("month", 0);
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            DateTime endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet addarea = Orders.Actor.GetAreaHospital(sectionStr, beginTime, endTime);
            StringBuilder areaHenadOne = new StringBuilder();
            StringBuilder areaHenadTwo = new StringBuilder();
            StringBuilder areaFootTwo = new StringBuilder();
            DataTable areaTable = addarea.Tables[0];
            int cols = areaTable.Columns.Count;
            int rows = areaTable.Rows.Count;
            /*第一个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet onearea = Orders.Actor.GetAreaHospital(sectionStr, beginTime, endTime);
            DataTable oneareaTA = onearea.Tables[0];
            int oneTaRow = oneareaTA.Rows.Count;
            Orders.Actor.GetBenDiAreaDate(out BenDiAdds, out BenDiArrive, sectionStr, beginTime, endTime);
            areaHenadOne.Append("<tr><td>本地</td>");
            areaHenadOne.Append("<td>" + BenDiAdds + "</td>");
            areaHenadOne.Append("<td>" + BenDiArrive + "</td>");
            areaHenadOne.Append("<td>" + (Convert.ToDouble(BenDiArrive) / Convert.ToDouble(BenDiAdds == 0 ? 1 : BenDiAdds)).ToString("p0") + "</td>");
            int benAddOne = BenDiAdds;
            int benArriveOne = BenDiArrive;
            /*第二个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 1).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet twoarea = Orders.Actor.GetAreaHospital(sectionStr, beginTime, endTime);
            DataTable twoareaTA = twoarea.Tables[0];
            Orders.Actor.GetBenDiAreaDate(out BenDiAdds, out BenDiArrive, sectionStr, beginTime, endTime);
            areaHenadOne.Append("<td>" + BenDiAdds + "</td>");
            areaHenadOne.Append("<td>" + BenDiArrive + "</td>");
            areaHenadOne.Append("<td>" + (Convert.ToDouble(BenDiArrive) / Convert.ToDouble(BenDiAdds == 0 ? 1 : BenDiAdds)).ToString("p0") + "</td>");
            int benAddTwo = BenDiAdds;
            int benArriveTwo = BenDiArrive;

            int twoTaRow = twoareaTA.Rows.Count;
            /*第三个月*/
            beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
            endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 2).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            DataSet threearea = Orders.Actor.GetAreaHospital(sectionStr, beginTime, endTime);
            Orders.Actor.GetBenDiAreaDate(out BenDiAdds, out BenDiArrive, sectionStr, beginTime, endTime);
            areaHenadOne.Append("<td>" + BenDiAdds + "</td>");
            areaHenadOne.Append("<td>" + BenDiArrive + "</td>");
            areaHenadOne.Append("<td>" + (Convert.ToDouble(BenDiArrive) / Convert.ToDouble(BenDiAdds == 0 ? 1 : BenDiAdds)).ToString("p0") + "</td></tr>");
            int benAddThree = BenDiAdds;
            int benArriveThree = BenDiArrive;
            DataTable threeareaTA = threearea.Tables[0];
            int threeTaRow = threeareaTA.Rows.Count;
            string oneDiseaString = string.Empty;
            string twoDiseaString = string.Empty;
            string threeDiseaString = string.Empty;


            for (int j = 0; j < oneTaRow; j++)
            {
                oneDiseaString += "," + oneareaTA.Rows[j][0] + ",";
            }
            for (int j = 0; j < twoTaRow; j++)
            {
                twoDiseaString += "," + twoareaTA.Rows[j][0] + ",";
            }
            for (int j = 0; j < threeTaRow; j++)
            {
                threeDiseaString += "," + threeareaTA.Rows[j][0] + ",";
            }




            for (int j = 0; j < rows; j++)
            {
                areaHenadOne.Append("<tr><td>" + areaTable.Rows[j][0] + "</td>");
                if (oneDiseaString.IndexOf("," + areaTable.Rows[j][0] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < oneTaRow; k++)
                    {
                        if (areaTable.Rows[j][0].ToString() == oneareaTA.Rows[k][0].ToString())
                        {
                            areaHenadOne.Append("<td>" + oneareaTA.Rows[k][1] + "</td><td>" + oneareaTA.Rows[k][2] + "</td><td>" + (Convert.ToDouble(oneareaTA.Rows[k][2]) / Convert.ToDouble(Convert.ToInt32(oneareaTA.Rows[k][1]) == 0 ? 1 : oneareaTA.Rows[k][1])).ToString("p0") + "</td>");
                        }
                    }
                }
                else
                {
                    areaHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (twoDiseaString.IndexOf("," + areaTable.Rows[j][0] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < twoTaRow; k++)
                    {
                        if (areaTable.Rows[j][0].ToString() == twoareaTA.Rows[k][0].ToString())
                        {
                            areaHenadOne.Append("<td>" + twoareaTA.Rows[k][1] + "</td><td>" + twoareaTA.Rows[k][2] + "</td><td>" + (Convert.ToDouble(twoareaTA.Rows[k][2]) / Convert.ToDouble(Convert.ToInt32(twoareaTA.Rows[k][1]) == 0 ? 1 : twoareaTA.Rows[k][1])).ToString("p0") + "</td>");
                        }
                    }
                }
                else
                {
                    areaHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                if (threeDiseaString.IndexOf("," + areaTable.Rows[j][0] + ','.ToString()) > -1)
                {
                    for (int k = 0; k < threeTaRow; k++)
                    {
                        if (areaTable.Rows[j][0].ToString() == threeareaTA.Rows[k][0].ToString())
                        {
                            areaHenadOne.Append("<td>" + threeareaTA.Rows[k][1] + "</td><td>" + threeareaTA.Rows[k][2] + "</td><td>" + (Convert.ToDouble(threeareaTA.Rows[k][2]) / Convert.ToDouble(Convert.ToInt32(threeareaTA.Rows[k][1]) == 0 ? 1 : threeareaTA.Rows[k][1])).ToString("p0") + "</td>");
                        }
                    }
                }
                else
                {
                    areaHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                }
                areaHenadOne.Append("</tr>");
            }

            for (int j = 0; j < oneTaRow; j++)
            {
                oneAdd = oneAdd + Convert.ToInt32(oneareaTA.Rows[j][1]);
                oneArrive = oneArrive + Convert.ToInt32(oneareaTA.Rows[j][2]);
            }
            for (int j = 0; j < twoTaRow; j++)
            {
                twoAdd = twoAdd + Convert.ToInt32(twoareaTA.Rows[j][1]);
                twoArrive = twoArrive + Convert.ToInt32(twoareaTA.Rows[j][2]);
            }
            for (int j = 0; j < threeTaRow; j++)
            {
                threeAdd = threeAdd + Convert.ToInt32(threeareaTA.Rows[j][1]);
                threeArrive = threeArrive + Convert.ToInt32(threeareaTA.Rows[j][2]);
            }
            areaFootTwo.Append("<tr>");
            areaFootTwo.Append("<td>合计</td>");
            areaFootTwo.Append("<td>" + (oneAdd + benAddOne) + "</td>");
            areaFootTwo.Append("<td>" + (oneArrive + benArriveOne) + "</td>");
            areaFootTwo.Append("<td>" + (Convert.ToDouble((oneArrive + benArriveOne)) / Convert.ToDouble((oneAdd + benAddOne) == 0 ? 1 : (oneAdd + benAddOne))).ToString("p0") + "</td>");
            areaFootTwo.Append("<td>" + (twoAdd + benAddTwo) + "</td>");
            areaFootTwo.Append("<td>" + (twoArrive + +benArriveTwo) + "</td>");
            areaFootTwo.Append("<td>" + (Convert.ToDouble((twoArrive + benArriveTwo)) / Convert.ToDouble((twoAdd + benAddTwo) == 0 ? 1 : (twoAdd + benAddTwo))).ToString("p0") + "</td>");
            areaFootTwo.Append("<td>" + (threeAdd + benAddThree) + "</td>");
            areaFootTwo.Append("<td>" + (threeArrive + +benArriveThree) + "</td>");
            areaFootTwo.Append("<td>" + (Convert.ToDouble((threeArrive + benArriveThree)) / Convert.ToDouble((threeAdd + benAddThree) == 0 ? 1 : (threeAdd + benAddThree))).ToString("p0") + "</td>");
            areaFootTwo.Append("</tr>");
            areaHenadTwo.Append("</tr><th></th>");
            areaHenadTwo.Append("<th colspan='3'>" + DateTime.Now.AddMonths(-month).ToString("MM") + "月</th>");
            areaHenadTwo.Append("<th colspan='3'>" + DateTime.Now.AddMonths(-month - 1).ToString("MM") + "月</th>");
            areaHenadTwo.Append("<th colspan='3'>" + DateTime.Now.AddMonths(-month - 2).ToString("MM") + "月</th></tr>");

            ViewData["Foot"] = areaFootTwo;
            ViewData["Heand"] = areaHenadOne;
            ViewData["HeandTwo"] = areaHenadTwo;
            return View();
        }
        #endregion

        #region 媒体来源报表

        public ActionResult MediaForms()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_MEDIA))
            {
                LoginManager.GoUnAccessPage();
            }
            string sectionStr = "";
            foreach (Section i in CurrentUser.UserSectionList)
            {
                sectionStr += "" + i.ID + ",";
            }
            sectionStr += "-1";
            IList<MediaSource> media = MediaSource.Actor.GetAllMediaSource(CurrentUser.HospitalID, 0);
            foreach (MediaSource med in media)
            {
                int month = RequestUtility.GetQueryInt("month", 0);
                DateTime beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
                DateTime endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                DataSet areaData = null;
                areaData = Orders.Actor.GetMediaHospital(sectionStr, beginTime, endTime, CurrentUser.HospitalID, med.ID);
                StringBuilder netHenadOne = new StringBuilder();
                netHenadOne.Append(@"<tbody><tr><td></td><td>预约</td> <td>到院</td><td>到院率</td><td>预约</td><td>到院</td><td>到院率</td><td>预约</td> <td>到院</td><td>到院率</td></tr>");
                StringBuilder netHenadTwo = new StringBuilder();
                DataTable netTable = areaData.Tables[0];
                int cols = netTable.Columns.Count;
                int rows = netTable.Rows.Count;
                /*第一个月*/
                beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month).ToString("yyyy-MM-01 00:00:00"));
                endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                areaData = null;
                areaData = Orders.Actor.GetMediaHospital(sectionStr, beginTime, endTime, CurrentUser.HospitalID, med.ID);

                DataTable onenetTA = areaData.Tables[0];
                int oneTaRow = onenetTA.Rows.Count;
                /*第二个月*/
                beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 1).ToString("yyyy-MM-01 00:00:00"));
                endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                areaData = null;
                areaData = Orders.Actor.GetMediaHospital(sectionStr, beginTime, endTime, CurrentUser.HospitalID, med.ID);

                DataTable twonetTA = areaData.Tables[0];
                int twoTaRow = twonetTA.Rows.Count;
                /*第三个月*/
                beginTime = Convert.ToDateTime(DateTime.Now.AddMonths(-month - 2).ToString("yyyy-MM-01 00:00:00"));
                endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1 - month - 2).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                areaData = null;
                areaData = Orders.Actor.GetMediaHospital(sectionStr, beginTime, endTime, CurrentUser.HospitalID, med.ID);

                DataTable threenetTA = areaData.Tables[0];
                int threeTaRow = threenetTA.Rows.Count;
                string oneNetString = string.Empty;
                string twoNetString = string.Empty;
                string threeNetString = string.Empty;

                for (int j = 0; j < oneTaRow; j++)
                {
                    oneNetString += "," + onenetTA.Rows[j][1] + ",";
                }
                for (int j = 0; j < twoTaRow; j++)
                {
                    twoNetString += "," + twonetTA.Rows[j][1] + ",";
                }
                for (int j = 0; j < threeTaRow; j++)
                {
                    threeNetString += "," + threenetTA.Rows[j][1] + ",";
                }
                for (int j = 0; j < rows; j++)
                {
                    netHenadOne.Append("<tr><td>" + netTable.Rows[j][1] + "</td>");
                    if (oneNetString.IndexOf("," + netTable.Rows[j][1] + ','.ToString()) > -1)
                    {
                        for (int k = 0; k < oneTaRow; k++)
                        {
                            if (Convert.ToInt32(netTable.Rows[j][0]) == Convert.ToInt32(onenetTA.Rows[k][0]))
                            {
                                netHenadOne.Append("<td>" + onenetTA.Rows[k][2] + "</td><td>" + onenetTA.Rows[k][3] + "</td><td>" + (Convert.ToDouble(onenetTA.Rows[k][3]) / Convert.ToDouble(Convert.ToInt32(onenetTA.Rows[k][2]) == 0 ? 1 : onenetTA.Rows[k][2])).ToString("p0") + "</td>");
                            }
                        }
                    }
                    else
                    {
                        netHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                    }
                    if (twoNetString.IndexOf("," + netTable.Rows[j][1] + ','.ToString()) > -1)
                    {
                        for (int k = 0; k < twoTaRow; k++)
                        {
                            if (Convert.ToInt32(netTable.Rows[j][0]) == Convert.ToInt32(twonetTA.Rows[k][0]))
                            {
                                netHenadOne.Append("<td>" + twonetTA.Rows[k][2] + "</td><td>" + twonetTA.Rows[k][3] + "</td><td>" + (Convert.ToDouble(twonetTA.Rows[k][3]) / Convert.ToDouble(Convert.ToInt32(twonetTA.Rows[k][2]) == 0 ? 1 : twonetTA.Rows[k][2])).ToString("p0") + "</td>");
                            }
                        }
                    }
                    else
                    {
                        netHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                    }
                    if (threeNetString.IndexOf("," + netTable.Rows[j][1] + ','.ToString()) > -1)
                    {
                        for (int k = 0; k < threeTaRow; k++)
                        {
                            if (Convert.ToInt32(netTable.Rows[j][0]) == Convert.ToInt32(threenetTA.Rows[k][0]))
                            {
                                netHenadOne.Append("<td>" + threenetTA.Rows[k][2] + "</td><td>" + threenetTA.Rows[k][3] + "</td><td>" + (Convert.ToDouble(threenetTA.Rows[k][3]) / Convert.ToDouble(Convert.ToInt32(threenetTA.Rows[k][2]) == 0 ? 1 : threenetTA.Rows[k][2])).ToString("p0") + "</td>");
                            }
                        }
                    }
                    else
                    {
                        netHenadOne.Append("<td>0</td><td>0</td><td>0%</td>");
                    }
                    netHenadOne.Append("</tr>");
                }
                int oneAdd = 0;
                int oneArrive = 0;
                int twoAdd = 0;
                int twoArrive = 0;
                int threeAdd = 0;
                int threeArrive = 0;
                for (int j = 0; j < oneTaRow; j++)
                {
                    oneAdd = oneAdd + Convert.ToInt32(onenetTA.Rows[j][2]);
                    oneArrive = oneArrive + Convert.ToInt32(onenetTA.Rows[j][3]);
                }
                for (int j = 0; j < twoTaRow; j++)
                {
                    twoAdd = twoAdd + Convert.ToInt32(twonetTA.Rows[j][2]);
                    twoArrive = twoArrive + Convert.ToInt32(twonetTA.Rows[j][3]);
                }
                for (int j = 0; j < threeTaRow; j++)
                {
                    threeAdd = threeAdd + Convert.ToInt32(threenetTA.Rows[j][2]);
                    threeArrive = threeArrive + Convert.ToInt32(threenetTA.Rows[j][3]);
                }

                netHenadOne.Append("<tr>");
                netHenadOne.Append("<td>合计</td>");
                netHenadOne.Append("<td>" + oneAdd + "</td>");
                netHenadOne.Append("<td>" + oneArrive + "</td>");
                netHenadOne.Append("<td>" + ((Convert.ToDouble(oneArrive) / Convert.ToDouble((oneAdd) == 0 ? 1 : oneAdd))).ToString("p0") + "</td>");
                netHenadOne.Append("<td>" + twoAdd + "</td>");
                netHenadOne.Append("<td>" + twoArrive + "</td>");
                netHenadOne.Append("<td>" + ((Convert.ToDouble(twoArrive) / Convert.ToDouble((twoAdd) == 0 ? 1 : twoAdd))).ToString("p0") + "</td>");
                netHenadOne.Append("<td>" + threeAdd + "</td>");
                netHenadOne.Append("<td>" + threeArrive + "</td>");
                netHenadOne.Append("<td>" + ((Convert.ToDouble(threeArrive) / Convert.ToDouble((threeAdd) == 0 ? 1 : threeAdd))).ToString("p0") + "</td>");
                netHenadOne.Append("</tr></tbody></table></div></div></div>");
                netHenadTwo.Append("<div class=\"form-group\"> <div class=\"row\"><div class=\"col-sm-12\"><h4>" + med.Name + "</h4></div><div class=\"table-responsive\"><table  class=\"table table-primary table-bordered\" style=\"width: 100%\">");
                netHenadTwo.Append("<thead></tr><th></th>");
                netHenadTwo.Append("<th colspan='3'>" + DateTime.Now.AddMonths(-month).ToString("MM") + "月</th>");
                netHenadTwo.Append("<th colspan='3'>" + DateTime.Now.AddMonths(-month - 1).ToString("MM") + "月</th>");
                netHenadTwo.Append("<th colspan='3'>" + DateTime.Now.AddMonths(-month - 2).ToString("MM") + "月</th></tr></thead>");
                ViewData["Heand"] += netHenadTwo.ToString() + netHenadOne.ToString();
            }
            return View();
        }
        #endregion

        #endregion
    }
}
