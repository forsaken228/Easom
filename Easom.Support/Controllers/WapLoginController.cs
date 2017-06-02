using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Web;
using CHCMS.Utility;
using Easom.Core;
using Easom.Support;


namespace Easom.Wap.Support.Controllers
{

    public class WapLoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string userName, string password, string validateCode)
        {
            HttpCookie validateType = RequestUtility.GetCookie("validateType");
            if (validateType == null)
            {
                validateType = new HttpCookie("validateType");
            }
            validateType.Value = "0";
            RequestUtility.SetResponseCookie(validateType);
            string validateTypestr = RequestUtility.GetCookieValue("validateType");
            string existsCode = RequestUtility.GetCookieValue("validateCode");
            if (validateTypestr == "0")//等于0表示没有错误无需验证
            {
                object obj = null;
                LoginState state = LoginManager.Login(userName, password);

                if (state == LoginState.PasswordError)
                {
                    validateType.Value = "1";
                    RequestUtility.SetResponseCookie(validateType);
                    obj = new { flag = "passworderror" };
                }
                if (state == LoginState.Success)
                {
                    validateType.Value = "0";
                    RequestUtility.SetResponseCookie(validateType);
                    obj = new { flag = "success" };
                }
                if (state == LoginState.UserNameError)
                {
                    validateType.Value = "1";
                    RequestUtility.SetResponseCookie(validateType);
                    obj = new { flag = "usernameerror" };
                }

                if (state == LoginState.Failed)
                {
                    validateType.Value = "1";
                    RequestUtility.SetResponseCookie(validateType);
                    obj = new { flag = "failed" };
                }
                return Json(obj);
            }
            else
            {
                if (existsCode != null && existsCode.Replace(" ", "+") == SecurityUtility.DESEncrypt(validateCode))
                {
                    object obj = null;
                    LoginState state = LoginManager.Login(userName, password);

                    if (state == LoginState.PasswordError)
                    {
                        validateType.Value = "1";
                        RequestUtility.SetResponseCookie(validateType);
                        obj = new { flag = "passworderror" };
                    }
                    if (state == LoginState.Success)
                    {
                        validateType.Value = "0";
                        RequestUtility.SetResponseCookie(validateType);
                        obj = new { flag = "success" };
                    }
                    if (state == LoginState.UserNameError)
                    {
                        validateType.Value = "1";
                        RequestUtility.SetResponseCookie(validateType);
                        obj = new { flag = "usernameerror" };
                    }

                    if (state == LoginState.Failed)
                    {
                        validateType.Value = "1";
                        RequestUtility.SetResponseCookie(validateType);
                        obj = new { flag = "failed" };
                    }
                    return Json(obj);
                }
                else
                {
                    return Json(new { flag = "errorvalidatecode" });
                }
            }
        }

        public ActionResult Logout()
        {
            LoginManager.Logout();
            return RedirectToAction("Login");
        }

        public ActionResult PhoneLogin()
        {
            return View();
        }
    }
}
