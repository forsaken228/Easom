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


namespace Easom.Support.Controllers 
{

    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string userName, string password, string validateCode)
        {
            //string existsCode = RequestUtility.GetCookieValue("validateCode");

            //if (existsCode != null && existsCode.Replace(" ", "+") == SecurityUtility.DESEncrypt(validateCode))
            //{
                object obj = null;
                LoginState state = LoginState.Failed;
                try
                {
                    state = LoginManager.Login(userName, password);
                }
                catch (Exception ex)
                {
                    Log4NetUtility.WriterErrorLog(ex.ToString());
                    throw;
                }
                finally
                {

                    if (state == LoginState.PasswordError)
                    {
                        obj = new { flag = "passworderror" };
                    }
                    if (state == LoginState.Success)
                    {
                        obj = new { flag = "success" };
                    }
                    if (state == LoginState.UserNameError)
                    {
                        obj = new { flag = "usernameerror" };
                    }

                    if (state == LoginState.Failed)
                    {
                        obj = new { flag = "failed" };
                    }
                }
                return Json(obj);
            //}
            //else
            //{
            //    return Json(new { flag = "errorvalidatecode" });
            //}

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
