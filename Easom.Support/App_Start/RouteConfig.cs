using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Easom.Support.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Adminstrator

            routes.MapRoute(
                "Administrator" + "_useredit",
                "administrator" + "/user/edit/{id}",
                new { controller = "Administrator", action = "UserEdit" },
                new { id = @"\w+" }
            );

            routes.MapRoute(
                "Tool" + "_usermyinformation",
                "tool" + "/user/",
                new { controller = "Tool", action = "MyInformation" }
            );

           routes.MapRoute(
                "Administrator" + "_usersetrole",
                "administrator" + "/user/setrole/{id}",
                new { controller = "Administrator", action = "SetRole" },
                new { id = @"\w+" }
            );

            routes.MapRoute(
                "Administrator" + "_usersethospital",
                "administrator" + "/user/sethospital/{id}",
                new { controller = "Administrator", action = "SetHospital" },
                new { id = @"\w+" }
            );

            routes.MapRoute(
                "Administrator" + "_usergetListuserdata",
                "administrator" + "/user/getlistuserdata",
                new { controller = "Administrator", action = "GetListUserData" }
            );

            routes.MapRoute(
                "Administrator" + "_useradd",
                "administrator" + "/user/add",
                new { controller = "Administrator", action = "UserAdd" }
            );

            routes.MapRoute(
                "Administrator" + "_userdelete",
                "administrator" + "/user/delete",
                new { controller = "Administrator", action = "UserDelete" }
            );

            routes.MapRoute(
                "Administrator" + "_userindex",
                "administrator" + "/user/{kv}/{pageIndex}",
                new { controller = "Administrator", action = "UserIndex", kv = "list", pageIndex = 1 }
            );

            routes.MapRoute(
                "Administrator" + "_rolersetauthor",
                "administrator" + "/role/setauthor/{roleID}",
                new { controller = "Administrator", action = "RoleSetAuthor" }
            );
            routes.MapRoute(
             "Administrator" + "_rolemanger",
             "administrator" + "/role/RoleManger/{roleID}",
             new { controller = "Administrator", action = "RoleManger" }
         );

            routes.MapRoute(
                "Administrator" + "_rolerdelete",
                "administrator" + "/role/delete/",
                new { controller = "Administrator", action = "RoleDelete" }
            );

            routes.MapRoute(
                "Administrator" + "_rolegetlistdata",
                "administrator" + "/role/getlistdata",
                new { controller = "Administrator", action = "GetListRoleData" }
            );

            routes.MapRoute(
                "Administrator" + "_roleredit",
                "administrator" + "/role/edit/{id}",
                new { controller = "Administrator", action = "RoleEdit" },
                new { id = @"\w+" }
            );

            routes.MapRoute(
                "Administrator" + "_roleradd",
                "administrator" + "/role/add",
                new { controller = "Administrator", action = "RoleAdd" }
            );

            routes.MapRoute(
                "Administrator" + "_roleindex",
                "administrator" + "/role/{kv}/{pageIndex}",
                new { controller = "Administrator", action = "RoleIndex", kv = "list", pageIndex = 1 }
            );

            routes.MapRoute(
                "Administrator" + "_operationlogindex",
                "administrator" + "/operationLogIndex/{kv}/{pageIndex}",
                new { controller = "Administrator", action = "OperationLogIndex", kv = "list", pageIndex = 1 }
            );

            routes.MapRoute(
             "Administrator" + "_operationloggetlistdata",
             "administrator" + "/operationLog/getlistdata",
             new { controller = "Administrator", action = "GetListOperationLogData" }
         );

            routes.MapRoute(
                "Administrator" + "_authordelete",
                "administrator" + "/author/delete/",
                new { controller = "Administrator", action = "AuthorDelete" }
            );

            routes.MapRoute(
                "Administrator" + "_authoredit",
                "administrator" + "/author/edit/{id}",
                new { controller = "Administrator", action = "AuthorEdit" },
                new { id = @"\w+" }
            );

            routes.MapRoute(
                "Administrator" + "_authoradd",
                "administrator" + "/author/add",
                new { controller = "Administrator", action = "AuthorAdd" }
            );

            routes.MapRoute(
                "Administrator" + "_authorgetlistdata",
                "administrator" + "/author/getlistdata",
                new { controller = "Administrator", action = "GetListAuthorData" }
            );

            routes.MapRoute(
                "Administrator" + "_authorindex",
                "administrator" + "/author/{kv}/{pageIndex}",
                new { controller = "Administrator", action = "AuthorIndex", kv = "list", pageIndex = 1 }
            );

            routes.MapRoute(
               "Administrator" + "_notifierdelete",
               "administrator" + "/notifier/delete/",
               new { controller = "Administrator", action = "NotifierDelete" }
           );


            routes.MapRoute(
                "Administrator" + "_notifieradd",
                "administrator" + "/notifier/add",
                new { controller = "Administrator", action = "NotifierAdd" }
            );

            routes.MapRoute(
                "Administrator" + "_notifieredit",
                "administrator" + "/notifier/edit/{id}",
                new { controller = "Administrator", action = "NotifierEdit" }
            );

            routes.MapRoute(
                "Administrator" + "_getalluserdata",
                "administrator" + "/notifier/getalluserdata",
                new { controller = "Administrator", action = "GetAllUserData" }
            );

            routes.MapRoute(
               "Administrator" + "_getnotifierlistdata",
               "administrator" + "/notifier/getlistdata",
               new { controller = "Administrator", action = "GetNotifierListData" }
           );

            routes.MapRoute(
                "Administrator" + "_notifierindex",
                "administrator" + "/notifier/{kv}/{pageIndex}",
                new { controller = "Administrator", action = "NotifierIndex", kv = "list", pageIndex = 1 }
            );
            routes.MapRoute(
               "Administrator" + "_notifylogindex",
               "administrator" + "/notifylog/index/{id}",
               new { controller = "Administrator", action = "NotifyLogIndex" },
               new { id = @"\w+" }
           );

            #endregion

            #region Error

            routes.MapRoute(
                "error",
                "error",
                new { controller = "Error", action = "Index" }
            );

            #endregion

            #region Login

            routes.MapRoute(
                "login",
                "login",
                new { controller = "Login", action = "Login" }
            );

            routes.MapRoute(
                "logout",
                "logout",
                new { controller = "Login", action = "LogOut" }
            );

            #endregion

            #region Default

            routes.MapRoute(
                "default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            #endregion
        }
    }
}