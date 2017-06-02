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

namespace Easom.Support.Controllers
{

    public class OutReservationController : SysAdminBaseController
    {
        #region  跨站预约列表
        public ActionResult OutOrderIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.A_GUOHAO))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public ActionResult GetOutOrderDataList()
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
            //获取当前用户切换的科室
            IList<Section> sectionlists = CurrentUser.UserSectionList;
            string sectionStr = "(";
            if (sectionlists != null)
            {
                foreach (var item in sectionlists)
                {
                    if (item != null)
                    {
                        sectionStr += item.ID + ",";
                    }
                }
            }
            sectionStr += "-1500)";
            string usersql = string.Format("SectionID in {0}", sectionStr);
            where += string.Format("AND {0}", usersql);

            IList<OutOrder> list = OutOrder.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                string isRead = "";
                if (item.State)
                {
                    isRead = "<span>已读</span>";
                }
                else {
                    isRead = string.Format("<a title=\"未读\" style=\"color:#ff5400;\" href=\"javascript:sigleCheck({0})\">未读</a>", item.ID);
                }
                sourceData.Add(new dynamic[] {
                    "<input type=\"checkbox\" value=\"" + item.ID + "\"/>",
                    item.UserName, item.Sex ? "男" : "女",
                    item.Age, item.Telephone,
                    item.OrderTime.ToString("yyyy-MM-dd"),
                    item.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                    item.CurrentOutSiteToHospital == null ? "没有来源" : item.CurrentOutSiteToHospital.URL,
                    string.Format("<span title=\"{0}\">{1}</span>",item.Description,StringUtility.CutString(item.Description,16,true)),
                    isRead,
                    item.State });
            }

            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetAttribute(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AG_READED))
            {
                LoginManager.GoUnAccessPage();
            }
            string ids = RequestUtility.GetFormString("ids");
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(ids, ',');
            if (paras != null && paras.Length > 0)
            {
                foreach (var item in paras)
                {
                    int id = Convert.ToInt32(item);
                    if (id > 0)
                    {
                        OutOrder currentEntity = OutOrder.Actor.GetByID(id);
                        if (currentEntity != null)
                        {
                            currentEntity.State = true;
                            OutOrder.Actor.Update(currentEntity);
                        }
                    }
                }
                return Json(new { flag = "success" });
            }
            return Json(new { flag = "error" });
        }


        [HttpPost]
        public JsonResult OutOrderDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.AG_DELETE))
            {
                LoginManager.GoUnAccessPage();
            }
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { flag = "error" });
            }
            string[] paras = StringUtility.Split(ids, ',');
            if (paras != null && paras.Length > 0)
            {
                foreach (var item in paras)
                {
                    int id = Convert.ToInt32(item);
                    if (id > 0)
                    {

                        OutOrder.Actor.Delete(id);
                    }
                }
                return Json(new { flag = "success" });
            }
            return Json(new { flag = "error" });
        }
        #endregion

    }
}
