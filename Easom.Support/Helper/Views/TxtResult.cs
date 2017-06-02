using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CHCMS.DataCenter.Web.Support.Helper.Views
{
    /// <summary>
    /// 提供将泛型集合数据导出Excel文档。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TxtResult : ActionResult
    {
        /// <summary>
        /// Content
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        public TxtResult(string content)
        {
            Content = content;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            SetResponse(context);
        }

        /// <summary>
        /// 设置并向客户端发送请求响应。
        /// </summary>
        /// <param name="context"></param>
        private void SetResponse(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Charset = "utf-8";
            context.HttpContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            context.HttpContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            context.HttpContext.Response.ContentType = "application/ms-txt";
            context.HttpContext.Response.Write(Content);
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.End();
        }

    }
}
