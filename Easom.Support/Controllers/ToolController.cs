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

    public class ToolController : SysAdminBaseController
    {
        public ActionResult UpdatePwd()
        {
            //if (!LoginManager.HasAutority(AuthorityConst.G_USERS))
            //{
            //    LoginManager.GoUnAccessPage();
            //}
            UserInfo userEntity = Easom.Core.UserInfo.Actor.GetByID(CurrentUser.ID);
            return View(userEntity);
        }
        [HttpPost]
        public JsonResult UpdatePwd(FormCollection form)
        {
            UserInfo userInfo = Easom.Core.UserInfo.Actor.GetByID(CurrentUser.ID);
            //oldpassword
            string aa = SecurityUtility.DESDecrypt(userInfo.PassWord);
            if (GetFormString(form, "oldpassword") != SecurityUtility.DESDecrypt(userInfo.PassWord))
            {
                return Json(new { flag = "oldpwderror" });
            }
            userInfo.PassWord = SecurityUtility.DESEncrypt(GetFormString(form, "password"));
             Easom.Core.UserInfo.Actor.Update(userInfo);
            return  Json(new { flag = "success" }) ;
        }

        public ActionResult MyInformation()
        {
            Easom.Core.UserInfo entity = Easom.Core.UserInfo.Actor.GetByID(CurrentUser.ID);
            if (entity != null)
            {
                return View(entity);
            }
            return View();
        }
        [HttpPost]
        public JsonResult MyInformation(FormCollection  form,int id)
        {
            UserInfo userInfo = Easom.Core.UserInfo.Actor.GetByID(id);
            //UserInfo entity = UserInfo.Actor.GetByEamil(GetFormString(form, "email"));
            //UserInfo entityTel = UserInfo.Actor.GetByTel(GetFormString(form, "mobile"));

            //if (entity != null && entity.ID != id)
            //{
            //    return Json(new { flag = HCMS.Reservation.Core.Support.UserInformationEum.HaveEmile.ToString() });
            //}

            //if (entityTel != null && entityTel.ID != id)
            //{
            //    return Json(new { flag = HCMS.Reservation.Core.Support.UserInformationEum.HaveMobile.ToString() });
            //}

            userInfo.TrueName = GetFormString(form, "usertruename");
            userInfo.Email = GetFormString(form, "email");
            userInfo.Telephone = GetFormString(form, "mobile");
            if (GetFormString(form, "password") != userInfo.PassWord)
            {
                userInfo.PassWord = SecurityUtility.DESEncrypt(GetFormString(form, "password"));
            }

            Easom.Core.UserInfo.Actor.Update(userInfo);
            return  Json(new { flag = "success" });
        }

        public ActionResult UploadPhoto()
        {
            return View();
        }
    }
}
