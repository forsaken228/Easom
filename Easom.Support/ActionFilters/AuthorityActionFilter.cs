using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Easom.Support.ActionFilters  
{
    public class AuthorityActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //开始权限验证
            string contorllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string authorityKey = contorllerName + "/" + actionName;

            base.OnActionExecuting(filterContext);
        }
    }
}
