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
using Easom.Core.Support;

namespace Easom.Support.Controllers
{

    public class WapSettingsController : SysAdminBaseController
    {


        #region Hospital

        /// <summary>
        /// 获取当前角色的所有医院科室
        /// </summary>
        /// <returns></returns>
        public JsonResult ShowHospitalSection()
        {
            StringBuilder changeHospitalStr = new StringBuilder();

            //获取当前用户所选择的科室
            // IList<Section> allSection = Section.Actor.GetByUserID(CurrentUser.ID, -1, CurrentUser.HospitalID);
            //获取当前用户所的科室
            IList<Hospital> allhospital = UserInfo.GetHospitalByUserID(CurrentUser.ID, -1);
            IList<Section> allSections = Section.Actor.GetByUserID(CurrentUser.ID, -1, CurrentUser.HospitalID);
            IList<Section> entitySection = CurrentUser.UserSectionList;/*当前用户科室*/
            string sectionStr = ",";
            foreach (Section sItem in CurrentUser.UserSectionList)
            {
                sectionStr += "" + sItem.ID + ",";
            }
            sectionStr += " -100,";

            if (entitySection.Count > 1 || allSections.Count == 1)
            {
                changeHospitalStr.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"show_menu();\"class=\"hdr-navdowm com-btn1 ui-btn\"><span class=\"com-btn1-inner ui-btn-inner\">" + CurrentUser.Hospital.Name + "</span></a>");//当前选择的医院科室
            }
            else
            {
                foreach (Section section in entitySection)
                {
                    changeHospitalStr.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"show_menu();\"class=\"hdr-navdowm com-btn1 ui-btn\"><span class=\"com-btn1-inner ui-btn-inner\">" + CurrentUser.Hospital.Name + "(" + section.Name + ")</span></a>");//当前选择的医院科室
                }
            }
            changeHospitalStr.AppendFormat("<div id=\"hdr-nav-menu\" class=\"hdr-nav-menu\"  ><ul class=\"menu\">");
            foreach (Hospital hospital in allhospital)
            {
                IList<Section> allSection = Section.Actor.GetByUserID(CurrentUser.ID, -1, hospital.ID);
                if (entitySection.Count > 1 || allSection.Count == 1)
                {
                    if (hospital.ID == CurrentUser.HospitalID)
                    {
                        changeHospitalStr.AppendFormat("<li class=\"menu-item\"><a class=\"link-text-Hospital\" style=\"color: #ff5400\"  href=\"javascript:ChangeHospitalSection(0," + hospital.ID + ",-1)\">" + hospital.Name + "</a></li>");
                    }
                    else
                    {
                        changeHospitalStr.AppendFormat("<li class=\"menu-item\"><a class=\"link-text-Hospital\" href=\"javascript:ChangeHospitalSection(0," + hospital.ID + ",-1)\">" + hospital.Name + "</a></li>");
                    }
                }
                else
                {
                    changeHospitalStr.AppendFormat("<li class=\"menu-item\"><a class=\"link-text-Hospital\" href=\"javascript:ChangeHospitalSection(0," + hospital.ID + ",-1)\">" + hospital.Name + "</a></li>");
                }

                if (allSection.Count > 1)
                {
                    foreach (Section section in allSection)
                    {
                        if (sectionStr.Contains(","+section.ID+","))
                        {
                            changeHospitalStr.AppendFormat("<li class=\"menu-item\"><a style=\"color: #ff5400\" class=\"link-text-Hospital\" href=\"javascript:ChangeHospitalSection(1," + hospital.ID + "," + section.ID + ")\">" + hospital.Name + "(" + section.Name + ")</a></li>");
                        }
                        else
                        {
                            changeHospitalStr.AppendFormat("<li class=\"menu-item\"><a class=\"link-text-Hospital\" href=\"javascript:ChangeHospitalSection(1," + hospital.ID + "," + section.ID + ")\">" + hospital.Name + "(" + section.Name + ")</a></li>");
                        }
                    }

                }
            }
            changeHospitalStr.AppendFormat("</ul></div>");
            return Json(new { flag = "success", SelectHospitalStr = changeHospitalStr.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChangeHospitalSection()
        {
            int type = RequestUtility.GetQueryInt("type", 0);
            int hid = RequestUtility.GetQueryInt("hospitalID", 0);
            int sid = RequestUtility.GetQueryInt("sectionID", 0);
            IList<Section> allSection = Section.Actor.GetByUserID(CurrentUser.ID, -1, hid);
            if (type == 0)
            {
                int defult = UserInfo.UpdateUserTOHospital(CurrentUser.ID, hid);
                foreach (Section section in allSection)
                {
                    UserToSection.Actor.UpdateUserToSection(CurrentUser.ID, section.ID, true);
                }
                if (defult > 0)
                {
                    return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { flag = "error" });
                }
            }
            else
            {
                int defult = UserInfo.UpdateUserTOHospital(CurrentUser.ID, hid);
                foreach (Section section in allSection)
                {
                    UserToSection.Actor.UpdateUserToSection(CurrentUser.ID, section.ID, false);
                }
                UserToSection.Actor.UpdateUserToSection(CurrentUser.ID, sid, true);
                if (defult > 0)
                {
                    return Json(new { flag = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { flag = "error" });
                }
            }
        }
        #endregion

    }
}
