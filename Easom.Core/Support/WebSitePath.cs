using System;
using System.Collections.Generic;
using System.Text;
using CHCMS.Utility;
using System.IO;

namespace Easom.Core.Support
{
    public class WebSitePath
    {

        /// <summary>
        /// 获取文件上传绝对存储路径
        /// </summary>
        public static string GetPath(string key)
        {

            string path = PathUtility.GetPath(key);
            path = path.Replace("/", "\\");

            if (path.StartsWith(@"\"))
            {
                path = path.TrimStart('\\');
                path = SiteUtility.CurAppDomainPath + path;
            }

            path = path + "\\" + GetAutoPath();
            return path;
        }

        /// <summary>
        /// 获取指定类型的Url路径地址
        /// </summary>
        public static string GetURL(string key)
        {
            string path = PathUtility.GetURL(key);
            path = path.Replace("\\", "/");

            if (path.StartsWith("/"))
            {
                path = SiteUtility.CurBaseURL + path;
            }

            return path;
        }

        /// <summary>
        /// 获取自动生成的图片URL
        /// </summary>
        public static string GetAutoURL()
        {
            string path = DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("M-d");

            return path;
        }

        /// <summary>
        /// 获取自动生成的图片路径
        /// </summary>
        public static string GetAutoPath()
        {
            string path = DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("M-d");

            return path;
        }

        /// <summary>
        /// 获取小图片查看时的URL,filePath为数据库存储的路径
        /// </summary>
        public static string GetViewSmallPicURL(string filePath,string key)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }
            string host = GetURL(key);
            string fileName = FileUtility.GetFileName(filePath);
                    
            return host + "/" + filePath.Replace(fileName, "") + "s_" + fileName;
        }

        /// <summary>
        /// 获取图片查看时的url,filePath为数据库存储的路径
        /// </summary>
        public static string GetViewPicURL(string filePath,string key)
        {
            return GetURL(key) + "/" + filePath;
        }

    }
}
