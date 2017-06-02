using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text;
using CHCMS.Utility;

namespace Easom.Support.Extensions
{
    public static class HtmlHelperExtensions
    {
        #region MVC 分页条

        /// <summary>
        /// 分页条(mvc版)
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="preFix">前缀 : 如 /controller/action/kv</param>
        /// <param name="maxShowCount">最多显示页码数, 该数为奇数</param>
        /// <param name="isAlwaysShow">仅有一页时, 是否显示</param>
        public static MvcHtmlString PageBar(this HtmlHelper htmlHelper, object pageIndex, object pageSize, object pageCount, object preFix, int maxShowCount, bool isAlwaysShow)
        {
            int intPageIndex = ConvertUtility.ToInt32(pageIndex, 1);
            int intPageSize = ConvertUtility.ToInt32(pageSize, 20);
            int intPageCount = ConvertUtility.ToInt32(pageCount, -1);
            string strPreFix = ConvertUtility.ToString(preFix);

            if (intPageCount == -1)
            {
                return null;
            }

            if (!isAlwaysShow)
            {
                if (intPageCount <= intPageSize)
                {
                    return null;
                }
            }

            if (maxShowCount % 2 == 0)
            {
                maxShowCount = maxShowCount + 1;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='pageBar'>");
            string aHref = "<a href='" + preFix + "{0}'>{1}</a>";

            if (intPageIndex == 1)
            {
                sb.Append("<span>首页</span>");
                sb.Append(" ");
                sb.Append("<span>上一页</span>");
            }
            else
            {
                sb.Append(string.Format(aHref, 1, "首页"));
                sb.Append(" ");
                sb.Append(string.Format(aHref, intPageIndex - 1, " 上一页"));

            }

            int totalPages = intPageCount / intPageSize + 1;
            if (intPageCount % intPageSize == 0)
            {
                totalPages = intPageCount / intPageSize;
            }

            if (totalPages == 0)
            {
                totalPages = 1;
            }

            int begin = 1;
            int end = totalPages;

            if (totalPages > maxShowCount)
            {
                int split = maxShowCount / 2;


                begin = intPageIndex - split > 0 ? intPageIndex - split : 1;
                end = intPageIndex + split < totalPages ? intPageIndex + split : totalPages;

                while (end - begin + 1 != maxShowCount)
                {
                    if (begin == 1)
                    {
                        end++;
                        continue;
                    }
                    if (end == totalPages)
                    {
                        begin--;
                        continue;
                    }
                }
            }

            for (int i = begin; i <= end; i++)
            {
                sb.Append(" ");
                if (intPageIndex != i)
                {
                    sb.Append(string.Format(aHref, i, i));
                }
                else
                {
                    sb.Append("<span>" + i.ToString() + "</span>");
                }
            }

            if (intPageIndex == totalPages)
            {
                sb.Append(" ");
                sb.Append("<span>下一页</span>");
                sb.Append(" ");
                sb.Append("<span>末页</span>");
            }
            else
            {
                sb.Append(" ");
                sb.Append(string.Format(aHref, intPageIndex + 1, " 下一页"));
                sb.Append(" ");
                sb.Append(string.Format(aHref, totalPages, "末页"));
            }
            sb.Append(" ");
            sb.Append(string.Format("<span>{0}/<b>{1}</b></span>", intPageIndex, totalPages));

            sb.Append("</div>");

            return new MvcHtmlString(sb.ToString()); ;
        }

        #endregion
    }
}
