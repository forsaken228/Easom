using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Easom.Support.Controllers
{
     public class ErrorController:Controller
    {
         /// <summary>
         /// 出错了
         /// </summary>
         /// <returns></returns>
         public ActionResult Index()
         {
             return View();
         }
         
         /// <summary>
         /// 未授权
         /// </summary>
         /// <returns></returns>
         public ActionResult UnAccess()
         {
             return View();
         }

         /// <summary>
         /// 未授权
         /// </summary>
         /// <returns></returns>
         public ActionResult UnAccessPop()
         {
             return View();
         }
    }
}
