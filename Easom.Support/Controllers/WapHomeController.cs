using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easom.Support.App_Start;
using Easom.Core;
using Easom.Core.Support;
using Easom.Support;
using CHCMS.Utility;

namespace Easom.Wap.Support.Controllers
{
    public class WapHomeController : SysAdminBaseController
    {
        public ActionResult Index()
        {
            if (!LoginManager.HasAutority(AuthorityConst.H_HOME))
            {
                Response.Redirect("/wapcustomerservice/orderindex");
            }
            if (LoginManager.HasAutority(AuthorityConst.H_HOME_TONGJI))
            {
                string sectionStr = "";
                foreach (Section sItem in CurrentUser.UserSectionList)
                {
                    sectionStr += "" + sItem.ID + ",";
                }
                sectionStr += " -100 ";
                //公共变量
                int adddate = 0;
                int orderdata = 0;
                int arrivedata = 0;

                #region 今日

                //网络
                StringBuilder todayData = new StringBuilder();
                DateTime begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                DateTime endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);

                todayData.Append("<li class=\"menu-item menu-item-top\"><a class=\"link-text link-inblock\" style=\"color: #0f3059\" name=\"atoday\">今日：</a><a class=\"link-text\" href=\"javascript:add('今日网络预约','" + begintimes + "','" + endtimes + "',0)\">网约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('今日网络预到','" + begintimes + "','" + endtimes + "',0)\">网预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('今日网络已到','" + begintimes + "','" + endtimes + "',0)\">网到&nbsp;" + arrivedata + "</a></li>");

                //电话
                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                todayData.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059; visibility: hidden;\" name=\"atoday\">今日：</a><a class=\"link-text\" href=\"javascript:add('今日电话预约','" + begintimes + "','" + endtimes + "',1)\">电约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('今日电话预约','" + begintimes + "','" + endtimes + "',1)\">电预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('今日电话预约','" + begintimes + "','" + endtimes + "',1)\">电到&nbsp;" + arrivedata + "</a></li>");

                //其他
                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                todayData.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059; visibility: hidden;\" name=\"atoday\">今日：</a><a class=\"link-text\" href=\"javascript:add('今日其他预约','" + begintimes + "','" + endtimes + "',2)\">自约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:add('今日其他预约','" + begintimes + "','" + endtimes + "',2)\">自预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:add('今日其他预约','" + begintimes + "','" + endtimes + "',2)\">自到&nbsp;" + arrivedata + "</a></li>");

                ViewBag.TodayData = todayData;


                #endregion

                #region 昨日

                StringBuilder yesterday = new StringBuilder();
                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                yesterday.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059\" name=\"atoday\">昨日：</a><a class=\"link-text\" href=\"javascript:add('昨日网络预约','" + begintimes + "','" + endtimes + "',0)\">网约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('昨日网络预到','" + begintimes + "','" + endtimes + "',0)\">网预&nbsp;" + orderdata + "</a><a  class=\"link-text\" href=\"javascript:arrive('昨日网络已到','" + begintimes + "','" + endtimes + "',0)\">网到&nbsp;" + arrivedata + "</a></li>");


                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                yesterday.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059; visibility: hidden;\" name=\"atoday\">昨日：</a><a class=\"link-text\" href=\"javascript:add('昨日电话预约','" + begintimes + "','" + endtimes + "',0)\">电约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('昨日电话预到','" + begintimes + "','" + endtimes + "',0)\">电预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('昨日电话已到','" + begintimes + "','" + endtimes + "',0)\">电到&nbsp;" + arrivedata + "</a></li>");


                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                yesterday.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059; visibility: hidden;\" name=\"atoday\">昨日：</a><a class=\"link-text\" href=\"javascript:add('昨日其他预约','" + begintimes + "','" + endtimes + "',2)\">自约&nbsp;" + adddate + "</a><a class=\"link-text\"href=\"javascript:order('昨日其他预到','" + begintimes + "','" + endtimes + "',2)\">自预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('昨日其他已到','" + begintimes + "','" + endtimes + "',2)\">自到&nbsp;" + arrivedata + "</a></li>");
                #endregion

                //明日
                orderdata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);

                yesterday.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059\" name=\"atoday\">明日：</a><a class=\"link-text\" href=\"javascript:order('明日网络预到','" + begintimes + "','" + endtimes + "',0)\">网预&nbsp;" + orderdata + "</a>");
                orderdata = 0;
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                yesterday.Append("<a class=\"link-text\" href=\"javascript:order('明日电话预到','" + begintimes + "','" + endtimes + "',1)\">电预&nbsp;" + orderdata + "</a>");
                orderdata = 0;
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                yesterday.Append("<a class=\"link-text\" href=\"javascript:order('明日其他预到','" + begintimes + "','" + endtimes + "',2)\">自预&nbsp;" + orderdata + "</a>");
                yesterday.Append("</li>");
                ViewBag.yesterdayData = yesterday;

                #region 预约统计数据汇总

                StringBuilder allCount = new StringBuilder();

                //本月网络
                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                allCount.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059\" name=\"atoday\">本月：</a><a class=\"link-text\" href=\"javascript:add('本月网络预约','" + begintimes + "','" + endtimes + "',0)\">网约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('本月网络预到','" + begintimes + "','" + endtimes + "',0)\">网预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('本月网络已到','" + begintimes + "','" + endtimes + "',0)\">网到&nbsp;" + arrivedata + "</a></li>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                allCount.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059; visibility: hidden;\" name=\"atoday\">本月：</a><a class=\"link-text\" href=\"javascript:add('本月电话预约','" + begintimes + "','" + endtimes + "',1)\">电约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('本月电话预到','" + begintimes + "','" + endtimes + "',1)\">电预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('本月电话已到','" + begintimes + "','" + endtimes + "',1)\">电到&nbsp;" + arrivedata + "</a></li>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                allCount.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059; visibility: hidden;\" name=\"atoday\">本月：</a><a class=\"link-text\" href=\"javascript:add('本月其他预约','" + begintimes + "','" + endtimes + "',2)\">自约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('本月其他预到','" + begintimes + "','" + endtimes + "',2)\">自预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('本月其他已到','" + begintimes + "','" + endtimes + "',2)\">自到&nbsp;" + arrivedata + "</a></li>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);
                allCount.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059;\" name=\"atoday\">本月总计：</a><a class=\"link-text\" href=\"javascript:add('本月预约','" + begintimes + "','" + endtimes + "',-1)\">预约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('本月预到','" + begintimes + "','" + endtimes + "',-1)\">预到&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('本月已到','" + begintimes + "','" + endtimes + "',-1)\">已到&nbsp;" + arrivedata + "</a></li>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);

                allCount.Append("<li class=\"menu-item \"><a class=\"link-text link-inblock\" style=\"color: #0f3059\" name=\"atoday\">上月：</a><a class=\"link-text\" href=\"javascript:add('上月网络预约','" + begintimes + "','" + endtimes + "',0)\">网约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('上月网络预到','" + begintimes + "','" + endtimes + "',0)\">网预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('上月网络已到','" + begintimes + "','" + endtimes + "',0)\">网到&nbsp;" + arrivedata + "</a></li>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);

                allCount.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059; visibility: hidden;\" name=\"atoday\">上月：</a><a class=\"link-text\" href=\"javascript:add('上月电话预约','" + begintimes + "','" + endtimes + "',1)\">电约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('上月电话预到','" + begintimes + "','" + endtimes + "',1)\">电预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('上月电话已到','" + begintimes + "','" + endtimes + "',1)\">电到&nbsp;" + arrivedata + "</a></li>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);

                allCount.Append("<li class=\"menu-item\"><a class=\"link-text link-inblock\" style=\"color: #0f3059; visibility: hidden;\" name=\"atoday\">上月：</a><a class=\"link-text\" href=\"javascript:add('上月其他预约','" + begintimes + "','" + endtimes + "',2)\">自约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('上月其他预到','" + begintimes + "','" + endtimes + "',2)\">自预&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('上月其他已到','" + begintimes + "','" + endtimes + "',2)\">自到&nbsp;" + arrivedata + "</a></li>");
                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);

                allCount.Append("<li class=\"menu-item menu-item-bottom\"><a class=\"link-text link-inblock\" style=\"color: #0f3059;\" name=\"atoday\">上月总计：</a><a class=\"link-text\" href=\"javascript:add('上月预约','" + begintimes + "','" + endtimes + "',-1)\">预约&nbsp;" + adddate + "</a><a class=\"link-text\" href=\"javascript:order('上月预到','" + begintimes + "','" + endtimes + "',-1)\">预到&nbsp;" + orderdata + "</a><a class=\"link-text\" href=\"javascript:arrive('上月已到','" + begintimes + "','" + endtimes + "',-1)\">已到&nbsp;" + arrivedata + "</a></li>");
                ViewBag.DataASta = allCount;
                #endregion
            }
            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

        public ActionResult NetIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.H_HOME_TONGJI))
            {
                Response.Redirect("/waphome/index");
            }
            IList<Hospital> userHospital = Hospital.Actor.GetByUserID(CurrentUser.ID, -1);
            StringBuilder allCount = new StringBuilder();
            int i = 0;
            int SumDayData = 0;
            int SumMothData = 0;
            int SHANGMothDate = 0;
            foreach (Hospital itme in userHospital)
            {
                if (i == 0)
                {
                    allCount.Append("<li class=\"menu-item menu-item-top\">");
                }
                //else if (i == userHospital.Count - 1)
                //{
                //    allCount.Append("<li class=\"menu-item menu-item-bottom\">");
                //}
                else
                {
                    allCount.Append("<li class=\"menu-item\">");
                }
                string sectionStr = null;
                IList<Section> sectionlst = Section.Actor.GetByHospitalID(itme.ID);
                foreach (Section sItem in sectionlst)
                {
                    sectionStr += "" + sItem.ID + ",";
                }
                sectionStr += " -100 ";
                int adddate = 0;
                int orderdata = 0;
                int arrivedata = 0;


                decimal todayNet = 0;
                decimal mothNet = 0;
                decimal yuNet = 0;
                DateTime begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                DateTime endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                SumDayData = SumDayData + arrivedata;
                todayNet = arrivedata;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                SumMothData = SumMothData + arrivedata;
                mothNet = arrivedata;
                yuNet = Convert.ToDecimal(mothNet) / Convert.ToDecimal(DateTime.Now.Day) * Convert.ToDecimal(endtimes.Day);
                allCount.Append("<span class=\"link-text link-inblock\" style=\"color: #0f3059;\" name=\"atoday\">" + StringUtility.CutString(itme.Name, 4, false) + "：</span><span class=\"link-text\">今网&nbsp;" + todayNet + "</span><span class=\"link-text\">总网&nbsp;" + mothNet + "</span><span class=\"link-text\">预网&nbsp;" + Convert.ToInt32(yuNet) + "</span></li>");

                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                SHANGMothDate = SHANGMothDate + arrivedata;
                i++;
            }
            DateTime endtime = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1).ToString("yyyy-MM-dd 23:59:56.999"));
            allCount.Append("<li class=\"menu-item\"><span class=\"link-text link-inblock\" style=\"color: #0f3059;\" name=\"atoday\">合&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;计：</span><span class=\"link-text\">今网&nbsp;" + SumDayData + "</span><span class=\"link-text\">总网&nbsp;" + SumMothData + "</span><span class=\"link-text\" style=\"width:70px; \">预网&nbsp;" + Convert.ToInt32(Convert.ToDecimal(SumMothData) / Convert.ToDecimal(DateTime.Now.Day) * Convert.ToDecimal(endtime.Day)) + "</span></li>");

            allCount.Append("<li class=\"menu-item menu-item-bottom\"><span class=\"link-text link-inblock\" style=\"color: #0f3059;\" name=\"atoday\">上月合计：</span><span class=\"link-text\">总月&nbsp;" + SHANGMothDate + "</span></li>");

            ViewBag.DataASta = allCount;
            return View();
        }
    }
}
