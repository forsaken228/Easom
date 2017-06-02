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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
namespace Easom.Support.Controllers
{
    public class JJ_BaiduHourController:  SysAdminBaseController
    {
        protected IWorkbook _workBook;

     

        public ActionResult BaiduDayIndex()
        {
            int accountID = 1;
            int day = RequestUtility.GetQueryInt("day", 0); 
            StringBuilder areaHenadOne = new StringBuilder();
            StringBuilder areaHenadTwo = new StringBuilder();

            /*第一天*/
            DateTime begintime1 = Convert.ToDateTime(DateTime.Now.AddDays(-day).ToShortDateString());
            DateTime begintime2 = Convert.ToDateTime(begintime1.AddDays(-1).ToShortDateString());
            DateTime begintime3 = Convert.ToDateTime(begintime1.AddDays(-2).ToShortDateString());
            IList<BaiDuDataModel> hoursList1 = GetHoursSumByDay(accountID, begintime1);
            IList<BaiDuDataModel> hoursList2 = GetHoursSumByDay(accountID, begintime2);
            IList<BaiDuDataModel> hoursList3 = GetHoursSumByDay(accountID, begintime3);
            for (int j = 1; j <= hoursList1.Count; j++)
             {
                areaHenadOne.Append("<tr>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + j + "</td>");
                var item1 = hoursList1[j - 1];
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item1.ShowNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item1.ClickNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item1.PayNum + "</td>");
                var item2 = hoursList2[j - 1];
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item2.ShowNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item2.ClickNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item2.PayNum + "</td>");
                var item3 = hoursList3[j - 1];
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item3.ShowNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item3.ClickNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item3.PayNum + "</td>");

                areaHenadOne.Append("</tr>");
            }
        
            areaHenadTwo.Append("</tr><th style='text-align: center; border-right: 1px solid #ccc;'></th>");
            areaHenadTwo.Append("<th colspan='3' style='text-align: center; border-right: 1px solid #ccc;'>" + begintime1.ToString("yyyy-MM-dd") + "日</th>");
            areaHenadTwo.Append("<th colspan='3' style='text-align: center; border-right: 1px solid #ccc;'>" + begintime2.ToString("yyyy-MM-dd") + "日</th>");
            areaHenadTwo.Append("<th colspan='3' style='text-align: center; border-right: 1px solid #ccc;'>" + begintime3.ToString("yyyy-MM-dd") + "日</th></tr>"); 

            ViewData["Heand"] = areaHenadOne;
            ViewData["HeandTwo"] = areaHenadTwo;
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }         
        public ActionResult CurrentDayData()
        {

            string startDate = RequestUtility.GetQueryString("startDate");

            ViewBag.startDate = startDate;

            if (!String.IsNullOrEmpty(startDate))
            {
                ViewBag.startDate = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.startDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd");
            }
            return View();
        }
        public ActionResult GetbaiduDayData()
        {
            int accountID = 1;
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;
            IList<dynamic> sourceData = new List<dynamic>();
            string startDate = RequestUtility.GetQueryString("startDate");
            DateTime begintime;
            if (!String.IsNullOrEmpty(startDate))
            {
                begintime = Convert.ToDateTime(startDate);
            }
            else
            {
                begintime = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            }
            IList<BaiDuDataModel> hoursList = GetHoursSumByDay(accountID, begintime);
            foreach (var item in hoursList)
            {
                sourceData.Add(new dynamic[] { item.TimeState + "-" + (Convert.ToInt32(item.TimeState) + 1) + "时", item.ShowNum, item.ClickNum, item.PayNum, item.TimeState });

            }
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData
            }, JsonRequestBehavior.AllowGet);

        }


        #region JJ_KeyWords_Report的相关操作

        public ActionResult CurrentAccountData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_HURO_MANAGER))
            {
                LoginManager.GoUnAccessPage();
            }
            string where = "1=1 ";
            int pageCount = 0;
            int accountID = RequestUtility.GetQueryInt("accountID", 0);
            StringBuilder areaHenadOne = new StringBuilder();
            StringBuilder areaHenadTwo = new StringBuilder();
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            string sectionStr = "(";
            foreach (var item in sectionlists)
            {
                sectionStr += item.ID + ",";
            }
            sectionStr += "-1500)";
            string usersql = string.Format("SectionID in {0}", sectionStr);
            where += string.Format("AND {0}", usersql);
            IList<JJ_Account> list = JJ_Account.Actor.Search(out pageCount, 1, -1, where, "ID", true);
            bool result = JJ_Account.GetJJ_AccountAuthority(list, accountID);
            if (result)
            {
                JJ_Account account = JJ_Account.Actor.GetByID(accountID);
                if (account != null)
                {
                    ViewBag.accountName = account.AccountName;
                    ViewBag.accountID = account.ID;
                }

            }
            else if(list.Count>0)
            {
                    ViewBag.accountName = list[0].AccountName;
                    ViewBag.accountID = list[0].ID;
                    accountID = list[0].ID;
            }
            string begintime1;
            string datetime = RequestUtility.GetQueryString("begintime");
            if (!String.IsNullOrEmpty(datetime))
            {
                begintime1 = Convert.ToDateTime(datetime).ToString("yyyy-MM-dd");
            }
            else
            {
                begintime1 = DateTime.Now.ToString("yyyy-MM-dd");
            }
            DateTime begintime = Convert.ToDateTime(begintime1);
            IList<BaiDuDataModel> hoursList1 = GetHoursSumByDay(accountID, begintime);

            areaHenadTwo.Append(" <th style='text-align: center; border-right: 1px solid #ccc;'></td>");
            areaHenadTwo.Append(" <th style='text-align: center; border-right: 1px solid #ccc;'>展现量</td>");
            areaHenadTwo.Append(" <th style='text-align: center; border-right: 1px solid #ccc;'>点击量</td>");
            areaHenadTwo.Append(" <th style='text-align: center; border-right: 1px solid #ccc;'>消费</td>");
            areaHenadTwo.Append(" <th style='text-align: center; border-right: 1px solid #ccc;'>操作</td>");
          
            for (int j = 0; j < hoursList1.Count; j++)
            {
                areaHenadOne.Append("<tr>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + j + "-" + (Convert.ToInt32(j) + 1) + "时" + "</td>");
                var item1 = hoursList1[j];
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item1.ShowNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item1.ClickNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + item1.PayNum + "</td>");
                areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'><span>");
                 if (LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_HOUR_UPLOAD))
                   {
                       areaHenadOne.Append("<a title='导入'  href='javascript:importFilter(" + item1.TimeState + ")'>导入&nbsp&nbsp&nbsp</a>");
                   }
                //areaHenadOne.Append("<a class='action-icons c-Delete' href='javascript:delHourData(" + item1.TimeState + ")' title='清空'></a></span></td>");
                //areaHenadOne.Append("<a class='action-icons c-suspend' href='javascript:showimportdata(" + item1.TimeState + ")' title='查看'></a>");
                 areaHenadOne.Append("<a  href='javascript:delHourData(" + item1.TimeState + ")' title='清空'>清空&nbsp&nbsp&nbsp</a>");
                 areaHenadOne.Append("<a  href='javascript:showimportdata(" + item1.TimeState + ")' title='查看'>查看</a></span></td>");
                areaHenadOne.Append("</tr>");
            }
            int sumShowNum = 0; 
            int sumClick = 0;
            decimal sumPagNum = 0;
            foreach (var item in hoursList1)
            {
                sumShowNum = sumShowNum + Convert.ToInt32(item.ShowNum);
                sumClick = sumClick + Convert.ToInt32(item.ClickNum);
                sumPagNum = sumPagNum + Convert.ToDecimal(item.PayNum);
            }
            areaHenadOne.Append("<tr>");
            areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>合计</td>");
            areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + sumShowNum + "</td>");
            areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + sumClick + "</td>");
            areaHenadOne.Append("<td style='text-align: center; border-right: 1px solid #ccc;'>" + sumPagNum + "</td>");
            areaHenadOne.Append("</tr>");
            
            ViewBag.startDate = begintime1;
            ViewData["Heand"] = areaHenadOne;
            ViewData["HeandTwo"] = areaHenadTwo;
            ViewBag.Account = list;
            return View();

        }

        public ActionResult BaiduHourIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_HURO_MANAGER))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            int timeState = RequestUtility.GetQueryInt("TimeState", -1);
            string datetime = RequestUtility.GetQueryString("datetime");
            var JJ_Accountlist = JJ_Account.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
            bool result = JJ_Account.GetJJ_AccountAuthority(JJ_Accountlist, accountID);
            if (result)
            {
                ViewBag.TimeState = timeState;
                ViewBag.accountID = accountID;
                ViewBag.datetime = datetime;
                ViewBag.tip = datetime + "  " + timeState + "-" + (timeState + 1) + "时段";
            }
            else
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public ActionResult GetbaiduHourData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_HURO_MANAGER))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            int timeState = RequestUtility.GetQueryInt("TimeState", -1);
            string datetime = RequestUtility.GetQueryString("datetime");
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            IList<dynamic> entity = null;
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "PayNum";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;
            string where = "JJ_AccountID=" + accountID;

            if (!String.IsNullOrEmpty(datetime))
            {
                datetime = Convert.ToDateTime(datetime).ToShortDateString();
            }
            else
            {
                datetime = DateTime.Now.ToShortDateString();
            }
            DateTime begintime = Convert.ToDateTime(datetime);
            DateTime endtime = Convert.ToDateTime(begintime.ToString("yyyy-MM-dd 23:59:59.99"));
            where += " AND DataTime between '" + begintime + "' And '" + endtime + "'";
            if (timeState > -1)
            {
                where += " AND TimeState=" + timeState;
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                where += " AND JJ_KeyWordsName LIKE '%" + keywords + "%'";
            }

            entity = ConvertFilterToArray(out pageCount, pageIndex, pageSize, where, orderFields, true);
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = entity
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Delete(FormCollection form)
        {
            string ids = GetFormString(form, "ids");
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(ids, ',');
            if (paras != null && paras.Length > 0)
            {
                foreach (var item in paras)
                {
                    int id = ConvertUtility.ToInt32(item, -1);
                    if (id > 0)
                    {
                        JJ_KeyWords_Report.Actor.Delete(id);
                    }
                }

                return Json(new { flag = "success" });
            }

            return Json(new { flag = "error" });
        }

        public JsonResult DeleteHourData(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_HOUR_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            int timeState = RequestUtility.GetQueryInt("TimeState", -1);
            if (timeState > 0 && timeState == DateTime.Now.Hour)
            {
                DateTime begintime = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                DateTime endtime = Convert.ToDateTime(begintime.ToString("yyyy-MM-dd 23:59:59.99"));
                JJ_KeyWords_Report.Actor.DeleteDataByHour(accountID, begintime, endtime, timeState);
                return Json(new { flag = "success", JsonRequestBehavior.AllowGet });
            }


            return Json(new { flag = "error", JsonRequestBehavior.AllowGet });
        }

        public ActionResult Import()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_HOUR_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            ViewBag.accountID = accountID;
            return View();
        }
        [HttpPost]
        public JsonResult Import(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_HOUR_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            DateTime begintime = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime endtime = Convert.ToDateTime(begintime.ToString("yyyy-MM-dd 23:59:59.99"));
            JJ_KeyWords_Report.Actor.DeleteDataByHour(accountID, begintime, endtime, DateTime.Now.Hour);
            if (SiteUtility.CurRequest.Files != null && SiteUtility.CurRequest.Files.Count > 0)
            {
                using (Stream currentStream = SiteUtility.CurRequest.Files[0].InputStream)
                {
                    _workBook = new HSSFWorkbook(currentStream);

                    DataTable tb = GetData(currentStream);
                    for (int i = 1; i < tb.Rows.Count; i++)
                    {
                        JJ_KeyWords_Report jjkeyword = new JJ_KeyWords_Report();
                        jjkeyword.DataTime = DateTime.Now;
                        jjkeyword.JJ_AccountID = accountID;
                        jjkeyword.JJ_Plan = tb.Rows[i][2].ToString();
                        jjkeyword.JJ_Cell = tb.Rows[i][3].ToString();
                        jjkeyword.JJ_KeyWordsName = tb.Rows[i][4].ToString();
                        jjkeyword.ShowNum = Convert.ToInt32(tb.Rows[i][5]);
                        jjkeyword.ClickNum = Convert.ToInt32(tb.Rows[i][6]);
                        jjkeyword.PayNum = Convert.ToDecimal(tb.Rows[i][7]);
                        jjkeyword.TimeState = DateTime.Now.Hour;
                        // JJ_KeyWords_Report.Actor.Insert(jjkeyword);
                        if (!jjkeyword.JJ_Cell.Contains("已删除"))
                        {
                            JJ_KeyWords_Report model = JJ_KeyWords_Report.Actor.GetbaiduHourSumByKeywordName(accountID, begintime, endtime, jjkeyword.JJ_Plan, jjkeyword.JJ_Cell, jjkeyword.JJ_KeyWordsName);
                            if (model != null)
                            {
                                //JJ_KeyWords_Report entity = JJ_KeyWords_Report.Actor.GetByKeywordName(accountID, begintime, endtime, jjkeyword.JJ_Plan, jjkeyword.JJ_Cell, jjkeyword.JJ_KeyWordsName, jjkeyword.TimeState);
                                //jjkeyword.ShowNum = jjkeyword.ShowNum - model.ShowNum;
                                //jjkeyword.ClickNum = jjkeyword.ClickNum - model.ClickNum;
                                //jjkeyword.PayNum = jjkeyword.PayNum - model.PayNum;
                                //if (jjkeyword.ShowNum > 0 && jjkeyword.ClickNum > 0 && jjkeyword.PayNum > 0 || jjkeyword.ShowNum > 0 && jjkeyword.ClickNum == 0)
                                //{
                                //    //JJ_KeyWords_Report entity = JJ_KeyWords_Report.Actor.GetByKeywordName(accountID, begintime, endtime, jjkeyword.JJ_Plan, jjkeyword.JJ_Cell, jjkeyword.JJ_KeyWordsName, jjkeyword.TimeState);
                                //    if (entity != null)
                                //    {
                                //        entity.ShowNum = jjkeyword.ShowNum + entity.ShowNum;
                                //        entity.ClickNum = jjkeyword.ClickNum + entity.ClickNum;
                                //        entity.PayNum = jjkeyword.PayNum + entity.PayNum;
                                //        JJ_KeyWords_Report.Actor.Update(entity);
                                //    }
                                //    else
                                //    {
                                //        JJ_KeyWords_Report.Actor.Insert(jjkeyword);
                                //    }
                                //}
                                jjkeyword.ShowNum = jjkeyword.ShowNum - model.ShowNum;
                                jjkeyword.ClickNum = jjkeyword.ClickNum - model.ClickNum;
                                jjkeyword.PayNum = jjkeyword.PayNum - model.PayNum;
                                if (jjkeyword.ShowNum > 0 && jjkeyword.ClickNum > 0 && jjkeyword.PayNum > 0 || jjkeyword.ShowNum > 0 && jjkeyword.ClickNum == 0)
                                {
                                    JJ_KeyWords_Report.Actor.Insert(jjkeyword);
                                }
                                else if (jjkeyword.ShowNum >= 0 && jjkeyword.ClickNum < 0 && jjkeyword.PayNum < 0)
                                {
                                    JJ_KeyWords_Report.Actor.Insert(jjkeyword);
                                }
                                else if (jjkeyword.ShowNum == 0 && jjkeyword.ClickNum > 0 && jjkeyword.PayNum > 0)
                                {
                                    JJ_KeyWords_Report entity = JJ_KeyWords_Report.Actor.GetByKeywordName(accountID, begintime, endtime, jjkeyword.JJ_Plan, jjkeyword.JJ_Cell, jjkeyword.JJ_KeyWordsName, -1);
                                    if (entity != null)
                                    {
                                        entity.ShowNum = entity.ShowNum;
                                        entity.ClickNum = jjkeyword.ClickNum + entity.ClickNum;
                                        entity.PayNum = jjkeyword.PayNum + entity.PayNum;
                                        entity.TimeState = DateTime.Now.Hour;
                                        entity.DataTime = DateTime.Now;
                                        JJ_KeyWords_Report.Actor.Update(entity);
                                        //jjkeyword.ShowNum = entity.ShowNum;
                                        //JJ_KeyWords_Report.Actor.Insert(jjkeyword);
                                    }
                                }


                            }
                            else
                            {
                                JJ_KeyWords_Report.Actor.Insert(jjkeyword);
                            }
                        }
                    }
                }
            }

            return Json("success");
        } 
        #endregion

        #region keyword的相关操作


        public ActionResult KeyWordStore()
        {

            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_KEYWORDS))
            {
                LoginManager.GoUnAccessPage();
            }
            string where = "1=1 ";
            int pageCount = 0;
            int accountID = RequestUtility.GetQueryInt("accountID", 0);
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            string sectionStr = "(";
            foreach (var item in sectionlists)
            {
                sectionStr += item.ID + ",";
            }
            sectionStr += "-1500)";
            string usersql = string.Format("SectionID in {0}", sectionStr);
            where += string.Format("AND {0}", usersql);
            IList<JJ_Account> list = JJ_Account.Actor.Search(out pageCount, 1, -1, where, "ID", true);
            bool result = JJ_Account.GetJJ_AccountAuthority(list, accountID);
            if (result)
            {
                JJ_Account account = JJ_Account.Actor.GetByID(accountID);
                if (account != null)
                {
                    ViewBag.accountName = account.AccountName;
                    ViewBag.accountID = account.ID;
                }
            }
            else if (list.Count > 0)
            {
                ViewBag.accountName = list[0].AccountName;
                ViewBag.accountID = list[0].ID;

            }
            ViewBag.Account = list;
            return View();
        }

        public ActionResult KeyWordStoreImport()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_KEYWORDS_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            var JJ_Accountlist = JJ_Account.Actor.GetBySections(CurrentUser.ID, CurrentUser.HospitalID);
            bool result = JJ_Account.GetJJ_AccountAuthority(JJ_Accountlist, accountID);
            if (result)
            {
                ViewBag.accountID = accountID;
            }
            else
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
     
            
        }

        public JsonResult KeyWordStoredelete(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_KEYWORDS_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            string ids = GetFormString(form, "ids");
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(ids, ',');
            if (paras != null && paras.Length > 0)
            {
                foreach (var item in paras)
                {
                    int id = ConvertUtility.ToInt32(item, -1);
                    if (id > 0)
                    {
                        JJ_KeyWords.Actor.Delete(id);
                    }
                }

                return Json(new { flag = "success" });
            }

            return Json(new { flag = "error" });
        }


        public ActionResult GetKeyWordStoreData()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_KEYWORDS))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            IList<dynamic> entity = null;
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;
            string where = "JJ_AccountID=" + accountID;


            if (!string.IsNullOrEmpty(keywords))
            {
                where += " AND Name LIKE '%" + keywords + "%'";

            }

            entity = ConvertKeyWordStoreToArray(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = entity
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult KeyWordStoreImport(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_KEYWORDS_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            if (SiteUtility.CurRequest.Files != null && SiteUtility.CurRequest.Files.Count > 0)
            {
                using (Stream currentStream = SiteUtility.CurRequest.Files[0].InputStream)
                {
                    _workBook = new HSSFWorkbook(currentStream);

                    DataTable tb = GetData(currentStream);
                    int returnData = JJ_KeyWords.Actor.DeleteByAccountID(accountID);
                    if (returnData >= 0)
                    {
                        for (int i = 1; i < tb.Rows.Count; i++)
                        {

                            JJ_KeyWords jjkeyword = new JJ_KeyWords();
                            jjkeyword.JJ_AccountID = accountID;
                            jjkeyword.JJ_Plan = tb.Rows[i][0].ToString();
                            jjkeyword.JJ_Cell = tb.Rows[i][1].ToString();
                            jjkeyword.Name = tb.Rows[i][2].ToString();
                            jjkeyword.PCUrl = tb.Rows[i][3].ToString();
                            jjkeyword.WAPUrl = tb.Rows[i][4].ToString();
                            JJ_KeyWords.Actor.Insert(jjkeyword);

                        }
                    }
                }
            }

            return Json("success");
        }  
        #endregion

        #region 日关键词的相关操作
        public ActionResult DayReport()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_DAY_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            string where = "1=1 ";
            int pageCount = 0;
            int accountID = RequestUtility.GetQueryInt("accountID", 0);
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            string sectionStr = "(";
            foreach (var item in sectionlists)
            {
                sectionStr += item.ID + ",";
            }
            sectionStr += "-1500)";
            string usersql = string.Format("SectionID in {0}", sectionStr);
            where += string.Format("AND {0}", usersql);
            IList<JJ_Account> list = JJ_Account.Actor.Search(out pageCount, 1, -1, where, "ID", true);
            bool result = JJ_Account.GetJJ_AccountAuthority(list, accountID);
            if (result)
            {
                JJ_Account account = JJ_Account.Actor.GetByID(accountID);
                if (account != null)
                {
                    ViewBag.accountName = account.AccountName;
                    ViewBag.accountID = account.ID;
                }

            } else if (list.Count > 0)
            {
                ViewBag.accountName = list[0].AccountName;
                ViewBag.accountID = list[0].ID;

            }
            ViewBag.Account = list;
            string begintime1;
            string datetime = RequestUtility.GetQueryString("begintime");
            if (!String.IsNullOrEmpty(datetime))
            {
                begintime1 = Convert.ToDateTime(datetime).ToString("yyyy-MM-dd");
            }
            else
            {
                begintime1 = DateTime.Now.ToString("yyyy-MM-dd");
            }
            ViewBag.startDate = begintime1;
            return View();
        }

        public ActionResult GetDayReportData()
        {
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            string datetime = RequestUtility.GetQueryString("datetime");
            Easom.Support.Helper.DataTablesRequest jQueryDataTablesRequestData = new Easom.Support.Helper.DataTablesRequest(this.Request);
            IList<dynamic> entity = null;
            int pageSize = jQueryDataTablesRequestData.PageSize;
            int pageIndex = jQueryDataTablesRequestData.PageIndex;
            string keywords = jQueryDataTablesRequestData.Search;
            string orderFields = jQueryDataTablesRequestData.OrderFiled ?? "ID";
            bool isDesc = jQueryDataTablesRequestData.IsDesc;
            int pageCount = 0;
            string where = "JJ_AccountID=" + accountID;
            if (!String.IsNullOrEmpty(datetime))
            {
                datetime = Convert.ToDateTime(datetime).ToShortDateString();
            }
            else
            {
                datetime = DateTime.Now.ToShortDateString();
            }
            DateTime begintime = Convert.ToDateTime(datetime);
            DateTime endtime = Convert.ToDateTime(begintime.ToString("yyyy-MM-dd 23:59:59.99"));
            where += " AND DataTime between '" + begintime + "' And '" + endtime + "'";

            if (!string.IsNullOrEmpty(keywords))
            {
                where += " AND JJ_KeyWordsName LIKE '%" + keywords + "%'";

            }

            entity = ConvertDayReportToArray(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = entity
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DayImport()
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_DAY_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            string datetime = RequestUtility.GetQueryString("datetime");
            ViewBag.datetime = datetime;
            ViewBag.accountID = accountID;
            return View();
        }
        [HttpPost]
        public JsonResult DayImport(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.KEY_TONGJI_JJ_BAIDU_DAY_UPLOAD))
            {
                LoginManager.GoUnAccessPage();
            }
            int accountID = RequestUtility.GetQueryInt("accountID", -1);
            string datetime = RequestUtility.GetQueryString("datetime");
            DateTime begintime = Convert.ToDateTime(datetime);
            DateTime endtime = Convert.ToDateTime(begintime.ToString("yyyy-MM-dd 23:59:59.99"));
            int returnData = JJ_DayKeyWords_Report.Actor.DeleteDataByDay(accountID, begintime, endtime);
            if (SiteUtility.CurRequest.Files != null && SiteUtility.CurRequest.Files.Count > 0)
            {
                using (Stream currentStream = SiteUtility.CurRequest.Files[0].InputStream)
                {
                    _workBook = new HSSFWorkbook(currentStream);

                    DataTable tb = GetData(currentStream);
                    if (returnData >= 0)
                    {
                        for (int i = 1; i < tb.Rows.Count; i++)
                        {
                            JJ_DayKeyWords_Report jjkeyword = new JJ_DayKeyWords_Report();
                            jjkeyword.DataTime = begintime;
                            jjkeyword.JJ_AccountID = accountID;
                            jjkeyword.JJ_Plan = tb.Rows[i][2].ToString();
                            jjkeyword.JJ_Cell = tb.Rows[i][3].ToString();
                            jjkeyword.JJ_KeyWordsName = tb.Rows[i][4].ToString();
                            jjkeyword.ShowNum = Convert.ToInt32(tb.Rows[i][5]);
                            jjkeyword.ClickNum = Convert.ToInt32(tb.Rows[i][6]);
                            jjkeyword.PayNum = Convert.ToDecimal(tb.Rows[i][7]);
                            jjkeyword.TimeState = DateTime.Now.Hour;
                            JJ_DayKeyWords_Report.Actor.Insert(jjkeyword);

                        }
                    }
                }
            }

            return Json("success");
        }  
        #endregion


       private IList<dynamic> ConvertFilterToArray(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc)
        {
            pageCount = 0;
            IList<dynamic> sourceData = new List<dynamic>();
            IList<JJ_KeyWords_Report> entityList = JJ_KeyWords_Report.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

            if (entityList != null && entityList.Count > 0)
            {
                foreach (var item in entityList)
                {
                    sourceData.Add(new dynamic[] { item.ID, item.JJ_Plan, item.JJ_Cell, item.JJ_KeyWordsName, item.ShowNum, item.ClickNum, item.PayNum, item.TimeState, item.ID });
                }
            }

            return sourceData;
        }

       private IList<dynamic> ConvertKeyWordStoreToArray(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc)
       {
           pageCount = 0;
           IList<dynamic> sourceData = new List<dynamic>();
           IList<JJ_KeyWords> entityList = JJ_KeyWords.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

           if (entityList != null && entityList.Count > 0)
           {
               foreach (var item in entityList)
               {
                   sourceData.Add(new dynamic[] { item.ID, item.JJ_Plan, item.JJ_Cell, item.Name, item.PCUrl, item.WAPUrl });
               }
           }

           return sourceData;
       }
       private IList<dynamic> ConvertDayReportToArray(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc)
       {
           pageCount = 0; 
           IList<dynamic> sourceData = new List<dynamic>();
           IList<JJ_DayKeyWords_Report> entityList = JJ_DayKeyWords_Report.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

           if (entityList != null && entityList.Count > 0)
           {
               foreach (var item in entityList)
               {
                   sourceData.Add(new dynamic[] { item.JJ_Plan, item.JJ_Cell, item.JJ_KeyWordsName, item.ShowNum, item.ClickNum, item.PayNum, item.TimeState, item.ID });
               }
           }

           return sourceData;
       }

       private IList<BaiDuDataModel> GetHoursSumByDay(int accountID, DateTime begintime)  
        {
            int hours = 24;
            IList<dynamic> sourceData = new List<dynamic>();
            IList<BaiDuDataModel> hoursList = new List<BaiDuDataModel>();
           // begintime = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime endtime = Convert.ToDateTime(begintime.ToString("yyyy-MM-dd 23:59:59.99"));
            DataSet addSet = JJ_KeyWords_Report.Actor.GetHourData(accountID, begintime, endtime);
            DataTable addSetTable = addSet.Tables[0];
            for (int i = 0; i < hours; i++)
            {
                var add = from row in addSetTable.AsEnumerable()
                          where (row.Field<int>("TimeState") == i)
                          select row;
                DataRow item = add.SingleOrDefault();
                if (item != null)
                {
                    hoursList.Add(new BaiDuDataModel { TimeState=i.ToString(), ShowNum = item["ShowNum"].ToString(), ClickNum = item["clickNum"].ToString(), PayNum = item["PayNum"].ToString() });
                }
                else
                {
                    hoursList.Add(new BaiDuDataModel { TimeState = i.ToString(), ShowNum = "0", ClickNum = "0", PayNum = "0" });
                }
            }

            return hoursList;
        }

       public virtual DataTable GetData(Stream stream)
        {
            using (stream)
            {
                var sheet = _workBook.GetSheetAt(0);
                if (sheet != null)
                {
                    var headerRow = sheet.GetRow(0);
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



    }

   
}
