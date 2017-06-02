using CHCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace  Easom.Support.App_Start
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string errorMsgMode = WebConfigUtility.GetAppSetting("errorMsgMode");

            if (string.IsNullOrEmpty(errorMsgMode))
            {
                errorMsgMode = "release";
            }

            errorMsgMode = errorMsgMode.ToLower();

            if (errorMsgMode != "debug")
            {
                Exception exception = Server.GetLastError();
                Response.Clear();
                Log4NetUtility.WriterErrorLog(exception.ToString());
                Server.ClearError();
                Server.Transfer("~/error");
            }
        }
    }
}
