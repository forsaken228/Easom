using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easom.Support.App_Start;
using System.Web.Mvc;
using Easom.Support.ActionFilters;
using Easom.Core;
using CHCMS.Utility;
using Easom.Core.Support;
using Easom.Message;

namespace Easom.Support.Controllers
{
    public class ServeManagerController : SysAdminBaseController
    {
        #region 系统服务

        public ActionResult MyServeIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            //int num = Notify.Actor.HttpOverage();
            //ViewBag.num = num;
            return View();
        }

        public ActionResult GetMyServeDataList()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
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
            if (keywords != "")
            {
                where += " and  ServeName  LIKE '%" + keywords + "%'";
            }

            IList<MyServe> list = MyServe.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                string action = "<a class=\"fa fa-edit\" href=\"javascript:edit('" + item.ID + "')\" title=\"编辑\"></a>&nbsp;";
                sourceData.Add(new dynamic[] { checkeds, item.ServeName, item.ServeKey, item.RepertoryNum, item.Price, item.Remark, action });
            }
            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData,
            }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        public ActionResult MyServeAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [HttpPost]
        public JsonResult MyServeAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            bool insertData = false;
            MyServe entity = new MyServe();
            entity.ServeName = GetFormString(form, "serveName");
            entity.RepertoryNum = GetFormInt(form, "repertoryNum", -1);
            entity.Remark = GetFormString(form, "Remark");
            entity.Price = GetFormDecimal(form, "Price", -1);
            entity.ServeKey = GetFormString(form, "serveKey");
            int sectionID = MyServe.Actor.Insert(entity);
            if (sectionID > 0)
            {
                insertData = true;
            }
            return insertData == true ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }

        /// <summary>
        /// 编辑服务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult MyServeEdit(int id)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            MyServe entity = MyServe.Actor.GetByID(id);
            return View(entity);
        }

        /// <summary>
        /// 编辑服务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MyServeEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                MyServe entity = MyServe.Actor.GetByID(id);
                if (entity != null)
                {
                    entity.ServeName = GetFormString(form, "serveName");
                    entity.RepertoryNum = GetFormInt(form, "repertoryNum", -1);
                    entity.Remark = GetFormString(form, "Remark");
                    entity.Price = GetFormDecimal(form, "Price", -1);
                    entity.ServeKey = GetFormString(form, "serveKey");
                    MyServe.Actor.Update(entity);
                    return Json(new { flag = "success" });
                }
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult MyServeDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
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
                        MyServe.Actor.Delete(id);
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 医院服务

        public ActionResult HospitalServeIndex()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            return View();
        }

        public ActionResult GetHospitalServeDataList()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
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
            if (keywords != "")
            {
                where += " and  HospitalID  LIKE '%" + keywords + "%'";
            }
            IList<HospitalToServe> list = HospitalToServe.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);

            IList<dynamic> sourceData = new List<dynamic>();

            foreach (var item in list)
            {
                if (item.Hospital != null)
                {
                    string checkeds = "<input type=\"checkbox\" value=\"" + item.ID + "\" />";
                    string action = "<a class=\"fa fa-edit\" href=\"javascript:view(" + item.HospitalID + ",'" + item.Sever.ServeKey + "')\" title=\"编辑\"></a>&nbsp;";
                    sourceData.Add(new dynamic[] { checkeds, item.Hospital.Name, item.Sever == null ? "该服务不存在" : item.Sever.ServeName, item.Num, item.DateType == ServeDateTypeEnum.None ? "永久有效" : item.ExpirationDate.ToString("yyyy-MM-dd HH:mm"), item.CreateTime.ToString("yyyy-MM-dd HH:mm"), action });
                }
            }

            return Json(new
            {
                sEcho = jQueryDataTablesRequestData.SEcho,
                iTotalRecords = pageCount,
                iTotalDisplayRecords = pageCount,
                aaData = sourceData,
            }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 添加科室
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        public ActionResult HospitalServeAdd()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            IList<Hospital> hospital = Hospital.Actor.GetByUserID(CurrentUser.ID, 0);
            IList<MyServe> myServe = MyServe.Actor.GetByAllServe();
            ViewData["Hospital"] = hospital;
            ViewData["MyServe"] = myServe;
            return View();
        }

        /// <summary>
        /// 根据hospitalID和serveID获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetHospitalServe()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            int hospitalID = RequestUtility.GetQueryInt("hospitalID", -1);
            string serveKey = RequestUtility.GetQueryString("serveKey");
            if (serveKey != null)
            {
                IList<Hospital> hospital = Hospital.Actor.GetByUserID(CurrentUser.ID, 0);
                HospitalToServe hospitalToServe = HospitalToServe.Actor.GetByHospitalID(hospitalID, serveKey);
                if (hospitalToServe != null)
                {
                    return Json(new { flag = "success", DataNum = hospitalToServe.Num, DateType = (int)hospitalToServe.DateType, DateTime = hospitalToServe.ExpirationDate.ToString("yyyy-MM-dd"), DateTimeHour = hospitalToServe.ExpirationDate.ToString("HH"), ServeData = "UPDATE", hopitalServeID = hospitalToServe.ID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { flag = "success", DataNum = 0, DateType = -1, ServeData = "INSERT" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { flag = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>       
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult HospitalServeAdd(FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            bool insertData = false;
            HospitalToServe entity = new HospitalToServe();
            entity.HospitalID = GetFormInt(form, "hospital", -1);
            int severID = GetFormInt(form, "serveName", -1);
            MyServe myserve = MyServe.Actor.GetByID(severID);
            if (myserve != null)
            {
                entity.ServeKey = myserve.ServeKey;
                entity.Num = GetFormInt(form, "RepertoryNum", -1);
                entity.ExpirationDate = GetFormDate(form, "ExpirationDate").AddHours(GetFormInt(form, "selecthours", 0));
                entity.DateType = (ServeDateTypeEnum)GetFormInt(form, "IsSetExpirationDate", 0);
                entity.CreateTime = DateTime.Now;
                HospitalToServe hospitalToServe = HospitalToServe.Actor.GetByHospitalID(entity.HospitalID, myserve.ServeKey);
                int zengNum = 0;//这次增加的数量
                if (hospitalToServe != null)
                {
                    zengNum = (entity.Num - hospitalToServe.Num);
                    entity.ID = hospitalToServe.ID;
                    HospitalToServe.Actor.Update(entity);
                    insertData = true;
                }
                else
                {
                    zengNum = entity.Num;
                    int sectionID = HospitalToServe.Actor.Insert(entity);
                    if (sectionID > 0)
                    {
                        insertData = true;
                    }
                }
                if (insertData == true)
                {
                    myserve.RepertoryNum = myserve.RepertoryNum - zengNum;/*从总数中减少*/
                    MyServe.Actor.Update(myserve);
                }
            }
            else
            {
                insertData = false;
            }
            return insertData == true ? Json(new { flag = "success" }) : Json(new { flag = "error" });
        }

        /// <summary>
        /// 编辑服务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public ActionResult HospitalServeEdit()
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }

            int hospitalID = RequestUtility.GetQueryInt("hospitalID", -1);
            string ServeKey = RequestUtility.GetQueryString("ServeKey");
            IList<Hospital> hospital = Hospital.Actor.GetByUserID(CurrentUser.ID, 0);
            IList<MyServe> myServe = MyServe.Actor.GetByAllServe();
            ViewData["Hospital"] = hospital;
            ViewData["MyServe"] = myServe;
            ViewBag.HospitalID = hospitalID;
            ViewBag.ServeKey = ServeKey;
            HospitalToServe entity = HospitalToServe.Actor.GetByHospitalID(hospitalID, ServeKey);
            return View(entity);
        }

        /// <summary>
        /// 编辑服务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        [HttpPost]
        public JsonResult HospitalServeEdit(int id, FormCollection form)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
            {
                LoginManager.GoUnAccessPage();
            }
            if (id > 0)
            {
                int hospitalID = GetFormInt(form, "hospital", -1);
                string serverKey = GetFormString(form, "serveName");
                if (hospitalID > 0 && !string.IsNullOrEmpty(serverKey))
                {
                    HospitalToServe entity = HospitalToServe.Actor.GetByHospitalID(hospitalID, serverKey);
                    if (entity != null)
                    {
                        entity.HospitalID = GetFormInt(form, "hospital", -1);
                        entity.Num = GetFormInt(form, "RepertoryNum", -1);
                        entity.ServeKey = GetFormString(form, "serveName");


                        string date = GetFormString(form, "ExpirationDate");
                        string time = GetFormString(form, "selecthours");

                        entity.ExpirationDate = Convert.ToDateTime(date + " " + time);// GetFormDate(form, "ExpirationDate").AddHours(GetFormInt(form, "selecthours", -1));
                        entity.DateType = (ServeDateTypeEnum)GetFormInt(form, "IsSetExpirationDate", 0);
                        HospitalToServe.Actor.Update(entity);
                        return Json(new { flag = "success" });
                    }
                }
            }
            return Json(new { flag = "error" });
        }

        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AuthorityActionFilter]
        public JsonResult HospitalServeDelete(string ids)
        {
            if (!LoginManager.HasAutority(AuthorityConst.D_SERVE))
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
                        HospitalToServe.Actor.Delete(id);
                    }
                }
            }
            return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
