using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easom.Core;
using System.Web.Mvc;
namespace Easom.Support
{
    public class Tool<T> : ActionResult
    {
        public T Obj
        {
            get;
            set;
        }

        public string CallBackName
        {
            get;
            set;
        }


        public Tool(T obj, string callback)
        {
            this.Obj = obj;
            this.CallBackName = callback;
        }

        /// <summary>
        ///判断请求是否合法  会根据url进行判断 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsSubmitByOkWay(string url, out HospitalWebsite entity)
        {
            bool returnData = false;
            entity = null;
            if (!string.IsNullOrEmpty(url))
            {
                HospitalWebsite currentEntity = HospitalWebsite.Actor.GetByURL(url.ToLower());
                if (currentEntity != null)
                {
                    entity = currentEntity;
                    returnData = true;
                }
            }
            return returnData;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            var jsonp = this.CallBackName + "(" + js.Serialize(this.Obj) + ")";

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Write(jsonp);
        }

        public static  string ReturnUrlHost(string url)
        {
            string returnUrl = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                System.Uri currentUrl = new Uri(url);
                returnUrl = currentUrl.Host.ToLower();
            }
            return returnUrl;
        }
    }
}
