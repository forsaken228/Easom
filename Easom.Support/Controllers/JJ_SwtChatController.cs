using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CHCMS.Utility;
using Easom.Core.Support;
using Easom.Core;
using Easom.Support.App_Start;
using System.Data;

namespace Easom.Support.Controllers
{
    public class JJ_SwtChatController : SysAdminBaseController
    {
        public ActionResult SwtChatIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public ActionResult SwtChatTableView()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT))
            {
                LoginManager.GoUnAccessPage();
            }
            DateTime beginTime = RequestUtility.GetQueryDateTime("begintime");
            DateTime endTime = RequestUtility.GetQueryDateTime("endtime");
            int chatState = RequestUtility.GetQueryInt("chatState", 0);
            int TableState = RequestUtility.GetQueryInt("tableState", 0);

            Dictionary<string, string> DicChatState = ConvertUtility.GetEnumDic(typeof(ChatStateEnum));
            Dictionary<string, string> DicTableState = ConvertUtility.GetEnumDic(typeof(SwtTableSortEnum));
            ViewData["ChatStateName"] = DicChatState[((ChatStateEnum)chatState).ToString()];
            string TableStateName = DicTableState[((SwtTableSortEnum)TableState).ToString()];
            ViewData["TableStateName"] = TableStateName;

            ViewData["BeginTimeString"] = beginTime.ToString("yyyy年MM月dd日");
            ViewData["EndTimeString"] = endTime.ToString("yyyy年MM月dd日");


            ViewData["chatState"] = chatState;
            ViewData["tableState"] = TableState;
            ViewData["beginTime"] = beginTime.ToString("yyyy-MM-dd 00:00:00.000");
            ViewData["endTime"] = endTime.ToString("yyyy-MM-dd 23:57:00.000");

            IList<JJ_Account> account = JJ_Account.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
            ViewData["JJ_Account"] = account;

            return View();
        }

        public JsonResult SwtReport()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT))
            {
                LoginManager.GoUnAccessPage();
            }
            DateTime beginTime = RequestUtility.GetQueryDateTime("begintime");
            DateTime endTime = RequestUtility.GetQueryDateTime("endtime");
            int chatState = RequestUtility.GetQueryInt("chatState", 0);
            int TableState = RequestUtility.GetQueryInt("tableState", 0);
            string account = RequestUtility.GetQueryString("account");
            Dictionary<string, string> DicTableState = ConvertUtility.GetEnumDic(typeof(SwtTableSortEnum));
            string TableStateName = DicTableState[((SwtTableSortEnum)TableState).ToString()];

            StringBuilder swtChatTable = new StringBuilder();

            string sectionStr = "0,";
            if (CurrentUser.UserSectionList != null)
            {
                foreach (Section sItem in CurrentUser.UserSectionList)
                {
                    sectionStr += "" + sItem.ID + ",";
                }
            }
            sectionStr += "-100";
            if (TableState == (int)SwtTableSortEnum.BaiduPlan || TableState == (int)SwtTableSortEnum.BaiduKeywords)
            {
                //IList<JJ_Account> account = JJ_Account.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
                //ViewData["JJ_Account"] = account;
                swtChatTable = SwtReportBaiduPlanORKeywords(beginTime, endTime, chatState, TableState, TableStateName, sectionStr, swtChatTable, TableState, account.TrimEnd(','));
            }
            else
            {
                swtChatTable = SwtReport(beginTime, endTime, chatState, TableState, TableStateName, sectionStr, swtChatTable);
            }
            return Json(new { flag = "success", tabledata = swtChatTable.ToString() });
        }

        private StringBuilder SwtReport(DateTime beginTime, DateTime endTime, int chatState, int TableState, string TableStateName, string sectionStr, StringBuilder swtChatTable)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT))
            {
                LoginManager.GoUnAccessPage();
            }
            DataSet SwtTableSortStatistics = JJ_Swt_Report.Actor.GetSwtTableSortStatistics(beginTime, endTime, CurrentUser.HospitalID, sectionStr, TableState, chatState);
            DataTable areaTable = SwtTableSortStatistics.Tables[0];
            int cols = areaTable.Columns.Count;
            int rows = areaTable.Rows.Count;
            swtChatTable.Append(@"<thead><tr><th>" + TableStateName + "</th><th>对话数</th></tr></thead><tbody>");

            for (int i = 0; i < rows; i++)
            {
                swtChatTable.Append("<tr>");
                for (int j = 0; j < cols; j++)
                {
                    string num = StringUtility.CutString(areaTable.Rows[i][j].ToString(), 50,false);
                  if (num.Contains("http"))
                  {
                      swtChatTable.Append("<td><span title=" + areaTable.Rows[i][j].ToString().TrimEnd('/') + ">" + num + "&nbsp;</span><a target='_blank' href=" + areaTable.Rows[i][j].ToString().TrimEnd('/') + ">点击访问</a></td>");
                  }
                  else
                  {
                      swtChatTable.Append("<td><span title=" + areaTable.Rows[i][j].ToString() + ">" +(num == "" ? "无数据显示" : num) + "</span></td>");
                  }
                }
                swtChatTable.Append("</tr>");
            }
            swtChatTable.Append(@"</tbody><tfoot><tr></tr></tfoot>");
            return swtChatTable;
        }

        private StringBuilder SwtReportBaiduPlanORKeywords(DateTime beginTime, DateTime endTime, int chatState, int TableState, string TableStateName, string sectionStr, StringBuilder swtChatTable, int planOrkeywords,string accountStr)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT))
            {
                LoginManager.GoUnAccessPage();
            }
            DataSet SwtTableSortStatistics = null;
            string name = "";
            if (planOrkeywords == (int)SwtTableSortEnum.BaiduPlan)
            {
                name = "计划";
                SwtTableSortStatistics = JJ_Swt_Report.Actor.GetBaiduPlanTableSortStatistics(beginTime, endTime, CurrentUser.HospitalID, sectionStr, accountStr.TrimEnd(','), chatState.ToString());
            }
            else
            {
                name = "关键词";
                SwtTableSortStatistics = JJ_Swt_Report.Actor.GetBaiduKeyWordsTableSortStatistics(beginTime, endTime, CurrentUser.HospitalID, sectionStr, accountStr.TrimEnd(','), chatState.ToString());
            }
            DataTable areaTable = SwtTableSortStatistics.Tables[0];
            int cols = areaTable.Columns.Count;
            int rows = areaTable.Rows.Count;
            swtChatTable.Append(@"<thead><tr><th>" + name + "</th><th>展现</th><th>点击</th><th>消费</th><th>对话</th><th>预约</th><th>到院</th><th>对话率</th><th>预约率</th><th>到院率</th><th>点击率</th></tr></thead><tbody>");

            for (int i = 0; i < rows; i++)
            {
                swtChatTable.Append("<tr>");
                for (int j = 0; j < cols; j++)
                {
                    swtChatTable.Append("<td><span title=" + areaTable.Rows[i][j].ToString() + ">" + StringUtility.CutString(areaTable.Rows[i][j].ToString() == "" ? "无数据显示" : areaTable.Rows[i][j].ToString(), 40, true) + "</span></td>");
                }
                string chatNum = "0";//对话率
                string yuyueNum = "0";//预约率
                string arriveNum = "0";//到院率
                string clickNum = "0";//点击率
                if(areaTable.Rows[i][2].ToString()!="0")
                {
                   chatNum = (Convert.ToDecimal(areaTable.Rows[i][4].ToString()) / Convert.ToDecimal(areaTable.Rows[i][2].ToString())).ToString("P2");
                }
                if (areaTable.Rows[i][4].ToString() != "0")
                {
                    yuyueNum = (Convert.ToDecimal(areaTable.Rows[i][5].ToString()) / Convert.ToDecimal(areaTable.Rows[i][4].ToString())).ToString("P2");
                }
                if (areaTable.Rows[i][5].ToString() != "0")
                {
                    arriveNum = (Convert.ToDecimal(areaTable.Rows[i][6].ToString()) / Convert.ToDecimal(areaTable.Rows[i][5].ToString())).ToString("P2");
                }
                if (areaTable.Rows[i][1].ToString() != "0")
                {
                    clickNum = (Convert.ToDecimal(areaTable.Rows[i][2].ToString()) / Convert.ToDecimal(areaTable.Rows[i][1].ToString())).ToString("P2");
                }
                swtChatTable.Append("<td><span>" + chatNum + "</span></td>");
                swtChatTable.Append("<td><span>" + yuyueNum + "</span></td>");
                swtChatTable.Append("<td><span>" + arriveNum + "</span></td>");
                swtChatTable.Append("<td><span>" + clickNum + "</span></td>");
                swtChatTable.Append("</tr>");
            }
            swtChatTable.Append(@"</tbody><tfoot><tr></tr></tfoot>");
            return swtChatTable;
        }
    }
}
