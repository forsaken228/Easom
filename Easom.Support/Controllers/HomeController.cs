using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easom.Support.App_Start;
using Easom.Core;
using Easom.Core.Support;

namespace Easom.Support.Controllers
{
    public class HomeController : SysAdminBaseController
    {
        public ActionResult Index()
        {
            string sectionStr = "-100";

            if (CurrentUser.UserSectionList != null)
            {
               foreach (Section sItem in CurrentUser.UserSectionList)
               {
                   sectionStr += "," + sItem.ID + "";
               }
            }
                #region 首页数据显示
                //公共变量
                int adddate = 0;
                int orderdata = 0;
                int arrivedata = 0;

                #region 网络统计
                StringBuilder indexDatanet = new StringBuilder();
                DateTime begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                DateTime endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                indexDatanet.Append("<tr><td style=''>今日</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',0)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',0)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',0)\">" + arrivedata + "</a></td></tr>");
                //border-right: 1px solid #ccc; width:50px

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                indexDatanet.Append("<tr style='color:gray;'><td>月比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                indexDatanet.Append("<tr style='color:gray;'><td>周比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                indexDatanet.Append("<tr><td >昨天</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',0)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',0)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',0)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(begintimes.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                indexDatanet.Append("<tr><td>本月</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',0)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',0)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',0)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                indexDatanet.Append("<tr style='color:gray;'><td >同比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(begintimes.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                indexDatanet.Append("<tr><td >上月</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',0)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',0)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',0)\">" + arrivedata + "</a></td></tr>");
                ViewBag.Datanet = indexDatanet;
                #endregion

                #region 电话统计

                StringBuilder indexDatanettel = new StringBuilder();
                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                indexDatanettel.Append("<tr><td >今日</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',1)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',1)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',1)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                indexDatanettel.Append("<tr style='color:gray;'><td >月比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                indexDatanettel.Append("<tr style='color:gray;'><td >周比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                indexDatanettel.Append("<tr><td >昨天</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',1)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',1)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',1)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(begintimes.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                indexDatanettel.Append("<tr><td >本月</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',1)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',1)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',1)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                indexDatanettel.Append("<tr style='color:gray;'><td >同比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(begintimes.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                indexDatanettel.Append("<tr><td >上月</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',1)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',1)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',1)\">" + arrivedata + "</a></td></tr>");
                ViewBag.Datatel = indexDatanettel;
                #endregion

                #region 其他

                StringBuilder indexDatanetAnt = new StringBuilder();
                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                indexDatanetAnt.Append("<tr><td >今日</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',2)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',2)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',2)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                indexDatanetAnt.Append("<tr style='color:gray;'><td >月比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                indexDatanetAnt.Append("<tr style='color:gray;'><td >周比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                indexDatanetAnt.Append("<tr><td >昨天</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',2)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',2)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',2)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(begintimes.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                indexDatanetAnt.Append("<tr><td>本月</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',2)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',2)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',2)\">" + arrivedata + "</a></td></tr>");


                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                indexDatanetAnt.Append("<tr><td>同比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");


                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(begintimes.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                indexDatanetAnt.Append("<tr><td>上月</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',2)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',2)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',2)\">" + arrivedata + "</a></td></tr>");
                ViewBag.DataAnt = indexDatanetAnt;
                #endregion

                #region 预约统计数据汇总

                StringBuilder indexDatanetSta = new StringBuilder();
                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);
                indexDatanetSta.Append("<tr><td>今日</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',-1)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',-1)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',-1)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);
                indexDatanetSta.Append("<tr><td >月比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);
                indexDatanetSta.Append("<tr><td>周比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);
                indexDatanetSta.Append("<tr><td>昨天</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',-1)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',-1)\" >" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',-1)\">" + arrivedata + "</a></td></tr>");

                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(begintimes.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);
                indexDatanetSta.Append("<tr><td>本月</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',-1)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',-1)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',-1)\">" + arrivedata + "</a></td></tr>");


                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);
                indexDatanetSta.Append("<tr><td>同比</td><td>预约 <span style='color:#FF8040'>" + adddate + "</span></td><td>预到 <span style='color:#FF8040'>" + orderdata + "</span></td><td>实到 <span style='color:#FF8040'>" + arrivedata + "</span></td></tr>");


                adddate = 0;
                orderdata = 0;
                arrivedata = 0;
                begintimes = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00.000"));
                endtimes = Convert.ToDateTime(begintimes.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:56.999"));
                Orders.Actor.GetIndexData(out adddate, out orderdata, out arrivedata, begintimes, endtimes, sectionStr, -1);
                indexDatanetSta.Append("<tr><td>上月</td><td>预约&nbsp;<a href=\"javascript:add('" + begintimes + "','" + endtimes + "',-1)\">" + adddate + "</a></td><td>预到 <a href=\"javascript:order('" + begintimes + "','" + endtimes + "',-1)\">" + orderdata + "</a></td><td>实到 <a href=\"javascript:arrive('" + begintimes + "','" + endtimes + "',-1)\">" + arrivedata + "</a></td></tr>");
                ViewBag.DataASta = indexDatanetSta;
                #endregion

                #endregion
            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

    }
}
