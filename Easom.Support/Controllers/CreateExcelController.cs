using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easom.Support.App_Start;
using Easom.Core.Support;
using CHCMS.Utility;
using System.IO;
using Easom.Core;
using System.Data;

namespace Easom.Support.Controllers
{
    public class CreateExcelController : SysAdminBaseController
    {
        #region 导出EXCEL

        public ActionResult ReportForms()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_EXECL))
            {
                LoginManager.GoUnAccessPage();
            }
            ViewData["role"] = CurrentUser.UserRole.ManagerRoles;//Role.Actor.GetAllData();
            return View();
        }
        public ActionResult SubmitExcelForms()
        {
            if (!LoginManager.HasAutority(AuthorityConst.B_EXECL))
            {
                LoginManager.GoUnAccessPage();
            }
            string sectionStr = "";
            foreach (Section i in CurrentUser.UserSectionList)
            {
                sectionStr += "" + i.ID + ",";
            }
            sectionStr += "-1";
            string result = string.Empty;
            string savePath = WebSitePath.GetPath("excel");
            string saveURL = WebSitePath.GetURL("excel") + "/" + WebSitePath.GetAutoURL();
            string filename = FileUtility.CreateFileName(".xls");
            string fullFilePath = Path.Combine(savePath, filename);
            if (!Directory.Exists(savePath))
            {
                FileUtility.CreateDirectory(savePath);
            }
            DateTime startTime = RequestUtility.GetQueryDateTime("startTime");
            DateTime endTime = RequestUtility.GetQueryDateTime("endTime");
            string roleStr = RequestUtility.GetQueryString("roleStr");
            int formType = RequestUtility.GetQueryInt("hidFormType", 1);
            if (startTime.ToString("yyyy/MM/dd hh:mm:ss") == "0001/1/1 00:00:00")
            {
                startTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
            }
            if (endTime.ToString("yyyy/MM/dd hh:mm:ss") == "0001/1/1 0:00:00")
            {
                endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:55.999"));
            }
            int hospitalID = CurrentUser.HospitalID;
            

            bool isSuccessExported = false;
            if (formType == 1)
            {
                string title = CurrentUser.Hospital.Name + "【预约信息统计报表】" +"(" + startTime.ToString("yyyy年MM月dd日") + "—" + endTime.ToString("yyyy年MM月dd日") + ")";
                int pageCount = 0;
                string where = " IsDelete=0 AND OrderState=0 AND SectionID in (" + sectionStr + ") AND  (AddTime between '" + startTime.ToString("yyyy-MM-dd 00:00:00.000") + "' AND '" + endTime.ToString("yyyy-MM-dd 23:59:55.999") + "' )";
                IList<Orders> entities = Orders.Actor.Search(out pageCount, 1, 100000, where, "ID", true);
                isSuccessExported = Orders.ExportOrders(entities, title, fullFilePath);
            }
            if (formType == 2)
            {
                string title = CurrentUser.Hospital.Name+"【到院信息统计报表】"+ "(" + startTime.ToString("yyyy年MM月dd日") + "—" + endTime.ToString("yyyy年MM月dd日") + ")";
                int pageCount = 0;
                string where = " IsDelete=0 AND OrderState=0 AND SectionID in (" + sectionStr + ") AND  (RecordTime between '" + startTime.ToString("yyyy-MM-dd 00:00:00.000") + "' AND '" + endTime.ToString("yyyy-MM-dd 23:59:55.999") + "' ) AND ArriveStateType=" + (int)ArriveStateEnum.Arrived + " ";
                IList<Orders> entities = Orders.Actor.Search(out pageCount, 1, 100000, where, "ID", true);
                isSuccessExported = Orders.ExportOrders(entities, title, fullFilePath);
            }
            if (formType == 3)//角色统计报表
            {
                string title = CurrentUser.Hospital.Name+"【角色预约数据统计报表】" + "(" + startTime.ToString("yyyy年MM月dd日") + "—" + endTime.ToString("yyyy年MM月dd日") + ")";
                if (roleStr != null)
                {
                    string role = roleStr;
                    isSuccessExported = Orders.ExportOrders(role, sectionStr, Convert.ToDateTime(startTime.ToString("yyyy-MM-dd 00:00:00.000")), Convert.ToDateTime(endTime.ToString("yyyy-MM-dd 23:59:55.999")), title, fullFilePath);
                }
                else
                {
                    isSuccessExported = false;
                }
            }
            if (formType == 4)
            {
                string title = CurrentUser.Hospital.Name +"【医生接诊信息统计报表】"+ "(" + startTime.ToString("yyyy年MM月dd日") + "—" + endTime.ToString("yyyy年MM月dd日") + ")";
                isSuccessExported = Orders.ExportDoctorOrders(CurrentUser.ID, CurrentUser.HospitalID, Convert.ToDateTime(startTime.ToString("yyyy-MM-dd 00:00:00.000")), Convert.ToDateTime(endTime.ToString("yyyy-MM-dd 23:59:55.999")), sectionStr, title, fullFilePath);
            }
            if (formType == 5)
            {
                string title = CurrentUser.Hospital.Name + "【疾病信息统计报表】" + "(" + startTime.ToString("yyyy年MM月dd日") + "—" + endTime.ToString("yyyy年MM月dd日") + ")";
                isSuccessExported = Orders.ExportDiseaseOrders(Convert.ToDateTime(startTime.ToString("yyyy-MM-dd 00:00:00.000")), Convert.ToDateTime(endTime.ToString("yyyy-MM-dd 23:59:55.999")), sectionStr, title, fullFilePath);
            }
            if (isSuccessExported)
            {
                return Json(new { flag = "success", viewURL = saveURL + "/" + filename }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { flag = "error" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
