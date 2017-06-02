using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Easom.Support
{
    public class ExcelActionResult : ActionResult
    {

        public string Paths
        {
            get;
            set;
        }

        public ExcelActionResult(string path)
        {
            Paths = path;
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

            System.IO.FileInfo file = new System.IO.FileInfo(Paths);
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.Charset = "GB2312";
            context.HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            // 添加頭信息，,彈出另存為窗口  
            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + context.HttpContext.Server.UrlEncode(file.Name));
            //讓瀏覽器顯示下載信息  
            context.HttpContext.Response.AddHeader("Content-Length", file.Length.ToString());

            // 指定返回一個不能被客戶端讀取的流，下載  
            context.HttpContext.Response.ContentType = "application/ms-excel";

            //把文件流下載到客戶端  
            context.HttpContext.Response.WriteFile(file.FullName);
            // 停止頁面的執行  

            context.HttpContext.Response.End();
        }
    }
}
