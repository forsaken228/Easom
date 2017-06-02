using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easom.Support.App_Start;
using Easom.Core;
using Easom.Core.Support;
using CHCMS.Utility;
using System.IO;
using System.Web;
using System.Data;
using Easom.Support.ActionFilters;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Text.RegularExpressions;

namespace Easom.Support.Controllers
{
    public class JJ_SwtChatManagerController : SysAdminBaseController
    {
        protected IWorkbook _workBook;



        #region 对话统计报表首页

        public ActionResult SwtChatManagerIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT_MANAGER))
            {
                LoginManager.GoUnAccessPage();
            }

            string StrChatState = RequestUtility.GetQueryString("ChatState");
            string StrTitle = RequestUtility.GetQueryString("Title");
            string StrEquipment = RequestUtility.GetQueryString("Equipment");
            string StrDisease = RequestUtility.GetQueryString("Disease");
            string StrBeginUrl = RequestUtility.GetQueryString("BeginUrl");
            string StrFromUrl = RequestUtility.GetQueryString("FromUrl");
            string StrKeyWords = RequestUtility.GetQueryString("KeyWords");
            Dictionary<string, string> searchDatas = new Dictionary<string, string>();

            searchDatas.Add("ChatState", StrChatState);
            searchDatas.Add("Title", StrTitle);
            searchDatas.Add("Equipment", StrEquipment);
            searchDatas.Add("Disease", StrDisease);
            searchDatas.Add("BeginUrl", StrBeginUrl);
            searchDatas.Add("FromUrl", StrFromUrl);
            searchDatas.Add("KeyWords", StrKeyWords);
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
            ViewBag.SearchDatas = GetSearchDataString(searchDatas);
            return View();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSwtReportData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT_MANAGER))
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
            string where = " HospitalID="+CurrentUser.HospitalID+" AND ";
            //string globalKeyword = RequestUtility.GetQueryString("globalKeyword");
            //if (string.IsNullOrEmpty(keywords))
            //{
            //    keywords = globalKeyword;
            //}

            DateTime beginTime = RequestUtility.GetQueryDateTime("starttime");
            DateTime endTime = RequestUtility.GetQueryDateTime("endtime");

            if (beginTime<Convert.ToDateTime("1990-1-1") || endTime<Convert.ToDateTime("1990-1-1"))
            {
                beginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:01"));
                endTime = Convert.ToDateTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            }
            DataSet areaData = JJ_Swt_Report.Actor.GetJJ_Swt_ReportHospital(beginTime, endTime, CurrentUser.HospitalID);
           
            where += "  BeginTime>='" + beginTime + "' and BeginTime<='" + endTime + "'";
            if (!string.IsNullOrEmpty(keywords))
            {
                keywords = keywords.Trim();
                if (keywords=="预约对话"){ keywords = "0"; }
                if (keywords == "有效对话") { keywords = "1"; }
                if (keywords == "一般对话") { keywords = "2"; }
                if (keywords == "无对话") { keywords = "3"; }
                where += " AND (ChatState =" + keywords;
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                keywords = keywords.Trim();
                where += " OR Title LIKE '%" + keywords + "%'" + " OR Equipment LIKE '%" + keywords
                    + "%' OR Disease LIKE '%" + keywords + "%'" + " OR BeginUrl LIKE '%" + keywords
                    + "%' OR FromUrl LIKE '%" + keywords + "%'" + " OR KeyWords LIKE '%" + keywords  + "%')";
            }
            IList<dynamic> orderList = null;
            try
            {
                orderList = OrderToShow(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            }
            catch (Exception e)
            {

                Log4NetUtility.WriterErrorLog(where + e.ToString());
            }
            return Json(new { 
                sEcho = jQueryDataTablesRequestData.SEcho, 
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = orderList
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 页面显示
        /// </summary>
        /// <param name="pageCount">总条数</param> 
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">一页显示数据条数</param>
        /// <param name="where">查询语句</param>
        /// <param name="orderFields">排序字段</param>
        /// <param name="isDesc">是否逆序</param>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public static IList<dynamic> OrderToShow(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT_MANAGER))
            {
                LoginManager.GoUnAccessPage();
            }
            pageCount = 0;
            IList<dynamic> sourceData = new List<dynamic>();
            IList<JJ_Swt_Report> entityList = JJ_Swt_Report.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            if (entityList != null && entityList.Count > 0)
            {
                foreach (var item in entityList)
                {
                    string StrChatState = null, StrTitle = null, StrEquipment = null, StrDisease = null, StrBeginUrl = null, StrFromUrl = null, StrKeyWords = null;
                    if (!string.IsNullOrEmpty(item.ChatState.ToString()))
                    {
                        Dictionary<string, string> dicE = ConvertUtility.GetEnumDic(typeof(Easom.Core.Support.ChatStateEnum));
                        StrChatState = "<span title=" + dicE[((ChatStateEnum)item.ChatState).ToString()] + ">" + dicE[((ChatStateEnum)item.ChatState).ToString()] + "</span>";
                    }
                    else
                    { 
                        StrChatState = "<span title='无'>无</span>";
                    }
                    if (!string.IsNullOrEmpty(item.Title.ToString()))
                    {
                        StrTitle = "<span title='" + item.Title + "'>" + StringUtility.CutString(item.Title, 18, true) + "</span>";
                    }
                    else
                    {
                        StrTitle = "<span title='无'>无</span>";
                    }
                    if (!string.IsNullOrEmpty(item.Equipment))
                    {
                        StrEquipment = "<span title=" + item.Equipment + ">" + item.Equipment + "</span>";
                    }
                    else
                    {
                        StrEquipment = "<span title='无'>无</span>";
                    }
                    if (!string.IsNullOrEmpty(item.Disease))
                    {
                        StrDisease = "<span title=" + item.Disease + ">" + item.Disease + "</span>";
                    }
                    else
                    {
                        StrDisease = "<span title='无'>无</span>";
                    }
                    if (!string.IsNullOrEmpty(item.BeginUrl))
                    {
                        if (item.BeginUrl.Contains("http"))
                        {
                            StrBeginUrl = "<a target='_blank' href='" + item.BeginUrl.TrimEnd('/') + "'><span title=" + item.BeginUrl.TrimEnd('/') + ">" + StringUtility.CutString(item.BeginUrl, 18, true) + "</span></a>";
                        }
                        else
                        {
                            StrBeginUrl = "<span title=" + item.BeginUrl + ">" + item.BeginUrl + "</span>";
                        }
                    }
                    else
                    {
                        StrBeginUrl = "<span title='无'>无</span>";
                    }
                    if (!string.IsNullOrEmpty(item.FromUrl))
                    {
                        if (item.FromUrl.Contains("http"))
                        {
                            StrFromUrl = "<a target='_blank' href='" + item.FromUrl.TrimEnd('/') + "'><span title=" + item.FromUrl.TrimEnd('/') + ">" + StringUtility.CutString(item.FromUrl, 18, true) + "</span></a>";
                        }
                        else
                        {
                            StrFromUrl = "<span title=" + item.FromUrl + ">" + item.FromUrl + "</span>";
                        }
                    }
                    else
                    {
                        StrFromUrl = "<span title='无'>无</span>";
                    }
                    if (!string.IsNullOrEmpty(item.KeyWords))
                    {
                        StrKeyWords = "<span title=" + item.KeyWords + ">" + StringUtility.CutString(item.KeyWords, 18, true) + "</span>";
                    }
                    else
                    {
                        StrKeyWords = "<span title='无'>无</span>";
                    }
                    sourceData.Add(new dynamic[] { StrChatState, 
                                                       StrTitle,
                                                       StrEquipment,
                                                       StrDisease,
                                                       StrBeginUrl,
                                                       StrFromUrl,
                                                       StrKeyWords
                        });
                }
            }
            return sourceData;
        }
        #endregion


        #region  导入商务通信息

        public ActionResult InExcel()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT_MANAGER_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            int timeState = RequestUtility.GetQueryInt("TimeState", -1);
            ViewBag.TimeState = timeState;
            //ViewBag.HospitalID = hospitalID;
            return View();
        }

        [HttpPost]
        public JsonResult InExcel(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_SWTCHAT_MANAGER_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            try
            {
                if (SiteUtility.CurRequest.Files != null && SiteUtility.CurRequest.Files.Count > 0)
                {
                    using (Stream currentStream = SiteUtility.CurRequest.Files[0].InputStream)
                    {
                        _workBook = new HSSFWorkbook(currentStream);
                        DataTable tb = GetData(currentStream);
                        JJ_Swt_Report jjswt = new JJ_Swt_Report();

                        int[] RowsSum = RowsSumff(tb);

                        for (int i = 3; i < tb.Rows.Count; i++)
                        {
                            int IDReamrk1 = 0;
                            int pageCount = 1;

                            IDReamrk1 = Convert.ToInt32(tb.Rows[i][RowsSum[0]].ToString());
                            IList<JJ_Swt_Report> jjswtList = JJ_Swt_Report.Actor.Search(out pageCount, 1, -1, Convert.ToString("KeyWords='" + tb.Rows[i][RowsSum[9]] + "' and BeginTime='" + tb.Rows[i][RowsSum[3]] + "' and HospitalID=" + CurrentUser.HospitalID), "ID", false);
                            
                            if (jjswtList.Count == 0)
                            {
                                //ID
                                jjswt.Reamrk1 = IDReamrk1.ToString();
                                //对话类型
                                jjswt.ChatState = ChatState(tb.Rows[i][RowsSum[2]].ToString(), tb.Rows[i][RowsSum[1]].ToString());
                                //Title
                                jjswt.Title = TitleChangeStr(tb.Rows[i][RowsSum[2]].ToString());
                                //初始对话时间
                                jjswt.BeginTime = Convert.ToDateTime(tb.Rows[i][RowsSum[3]]);
                                //设备
                                jjswt.Equipment = tb.Rows[i][RowsSum[4]].ToString();
                                //病种
                                jjswt.Disease = tb.Rows[i][RowsSum[5]].ToString();
                                //着陆页
                                jjswt.BeginUrl = tb.Rows[i][RowsSum[6]].ToString();
                                //聊天链接
                                jjswt.ChatUrl = tb.Rows[i][RowsSum[7]].ToString();
                                //访问来源
                                jjswt.FromUrl = tb.Rows[i][RowsSum[8]].ToString();
                                //关键词
                                jjswt.KeyWords = tb.Rows[i][RowsSum[9]].ToString();
                                //医院ID
                                jjswt.HospitalID = CurrentUser.HospitalID;
                                //部门ID
                                int SID = TitleChangeInt(tb.Rows[i][RowsSum[2]].ToString());
                                if (Orders.Actor.GetByID(SID) != null)
                                {
                                   jjswt.SectionID = Convert.ToInt32(Orders.Actor.GetByID(SID).SectionID);
                                } 
                                JJ_Swt_Report.Actor.Insert(jjswt);
                            }
                            else
                            {
                                //自增ID
                                jjswt.ID = jjswtList[0].ID;
                                //ID-Reamrk1
                                jjswt.Reamrk1 = jjswtList[0].Reamrk1;
                                //对话类型
                                jjswt.ChatState = ChatState(tb.Rows[i][RowsSum[2]].ToString(), tb.Rows[i][RowsSum[1]].ToString());
                                //Title
                                jjswt.Title = TitleChangeStr(tb.Rows[i][RowsSum[2]].ToString());
                                //初始对话时间
                                jjswt.BeginTime = Convert.ToDateTime(tb.Rows[i][RowsSum[3]]);
                                //设备
                                jjswt.Equipment = tb.Rows[i][RowsSum[4]].ToString();
                                //病种
                                jjswt.Disease = tb.Rows[i][RowsSum[5]].ToString();
                                //着陆页
                                jjswt.BeginUrl = tb.Rows[i][RowsSum[6]].ToString();
                                //聊天链接
                                jjswt.ChatUrl = tb.Rows[i][RowsSum[7]].ToString();
                                //访问来源
                                jjswt.FromUrl = tb.Rows[i][RowsSum[8]].ToString();
                                //关键词
                                jjswt.KeyWords = tb.Rows[i][RowsSum[9]].ToString();
                                //医院ID
                                jjswt.HospitalID = CurrentUser.HospitalID;
                                //部门ID
                                int SID = TitleChangeInt(tb.Rows[i][RowsSum[2]].ToString());
                                if (Orders.Actor.GetByID(SID) != null)
                                {
                                    jjswt.SectionID = Convert.ToInt32(Orders.Actor.GetByID(SID).SectionID);
                                }
                                JJ_Swt_Report.Actor.Update(jjswt);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('" + ex.Message + "');</script>");
            }
            return Json("success");
        }

        /// <summary>
        /// Excel列
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public int[] RowsSumff(DataTable tb)
        {
            int[] RowsSum = new int[10];
            string[] Names = new string[] { "编号" ,"对话类型","名称","开始访问时间", "操作系统","客人类别",
                                            "初次访问网址","对话来源","访问来源","关键词"};
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    for (int m = 0; m < Names.Length; m++)
                    {
                        if (tb.Rows[i][j].ToString() == Names[m])
                        {
                            RowsSum[m] = j;
                            break;
                        }
                    }
                }
            }
            return RowsSum;
        }

        /// <summary>
        /// 判断标题是否在预约号中存在特色符号，替换后，返回新的标题
        /// </summary>
        /// <param name="UseStr"></param>
        /// <returns></returns>
        public string TitleChangeStr(string str)
        {
            string TChange="";
            int d;
            int Sindex = str.IndexOf("#") + 1;
            int Sof = str.LastIndexOf("#");
            int Scount = Sof - Sindex;
           
            try
            {
                if ((str.Length - str.Replace("#", "").Length) == 2 && Scount > 0)
                {
                    //TChange = Regex.Replace(TChange, @"[^0-9\u4e00-\u9fa5\s]", "");
                    TChange = Regex.Replace(str.Substring(Sindex, Scount), @"[^\d]*", "");
                    if (int.TryParse(TChange, out d))
                    {
                        TChange = "#" + TChange + "#" + str.Substring(0, Sindex - 1) + str.Substring(Sof + 1);
                    }
                }
                if (TChange=="")
                {
                    TChange = str;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return TChange;
        }

        /// <summary>
        /// 判断标题是否存在预约号,返回预约号
        /// </summary>
        /// <param name="UseStr"></param>
        /// <returns></returns>
        public int TitleChangeInt(string str)
        {
            string TChange;
            int d;
            int Sindex = str.IndexOf("#") + 1;
            int Sof = str.LastIndexOf("#");
            int Scount = Sof - Sindex;
            int Intreturn = 0;
            try
            {
                if ((str.Length - str.Replace("#", "").Length) == 2 && Scount>0)
                {
                    TChange = str.Substring(Sindex, Scount);
                    //TChange = Regex.Replace(TChange, @"[^0-9\u4e00-\u9fa5\s]", "");
                    TChange = Regex.Replace(TChange, @"[^\d]*", "");
                    if (int.TryParse(TChange, out d))
                    {
                        Intreturn = Convert.ToInt32(TChange);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Intreturn;
        }

        /// <summary>
        /// 对话类型转换
        /// </summary>
        /// <param name="UseStr"></param>
        /// <returns></returns>
        public int ChatState(string UseStr, string ChatState)
        {
            int ChatStateInt;
            if (TitleChangeInt(UseStr)>0)
                ChatStateInt = 0;
            else if (ChatState == "较好对话" || ChatState == "极佳对话")
                ChatStateInt = 1;
            else if (ChatState == "一般对话")
                ChatStateInt = 2;
            else
                ChatStateInt = 3;

            return ChatStateInt;
        }

        public virtual DataTable GetData(Stream stream)
        {
            using (stream)
            {
                var sheet = _workBook.GetSheetAt(0);
                if (sheet != null)
                {
                    //Excel文件 列名
                    var headerRow = sheet.GetRow(3);
                    DataTable dt = new DataTable();
                    int columnCount = headerRow.Cells.Count;
                    for (int i = 0; i < columnCount; i++)
                    {
                        dt.Columns.Add("col_" + i.ToString());
                    }
                    var row = sheet.GetRowEnumerator();
                    while (row.MoveNext())
                    {
                        var dtRow = dt.NewRow();
                        var excelRow = row.Current as IRow;
                        for (int i = 0; i < columnCount; i++)
                        {
                            var cell = excelRow.GetCell(i);
                            if (cell != null)
                            {
                                dtRow[i] = GetValue(cell);
                            }
                        }
                        dt.Rows.Add(dtRow);
                    }
                    return dt;
                }
            }
            return null;
        }

        private object GetValue(ICell cell)
        {
            object value = null;
            switch (cell.CellType)
            {
                case CellType.BLANK:
                    break;
                case CellType.BOOLEAN:
                    value = cell.BooleanCellValue ? "1" : "0"; break;
                case CellType.ERROR:
                    value = cell.ErrorCellValue; break;
                case CellType.FORMULA:
                    value = "=" + cell.CellFormula; break;
                case CellType.NUMERIC:
                    value = cell.NumericCellValue.ToString(); break;
                case CellType.STRING:
                    value = cell.StringCellValue; break;
                case CellType.Unknown:
                    break;
            }
            return value;
        }
        #endregion

    }
}
