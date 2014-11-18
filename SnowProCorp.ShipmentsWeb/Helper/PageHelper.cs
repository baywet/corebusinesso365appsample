using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using SnowProCorp.ShipmentsWeb.Models;

namespace SnowProCorp.ShipmentsWeb.Helper
{
    /// <summary>
    /// Html helper for rendering a paging control
    /// </summary>
    public static class PagingHelpers
    {
        /// <summary>
        /// Helper method for building paging links.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pagingInfo">The paging info.</param>
        /// <param name="pageUrl">The page URL.</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            if (pagingInfo == null)
            {
                return MvcHtmlString.Empty;
            }
            const int maxVisibleLinks = 3;
            var stringBuilder = new StringBuilder();

            if (pagingInfo.TotalPages > 1)
            {
                //if we are not at the first page display prev
                if (pagingInfo.CurrentPageIndex > 1)
                {
                    var anchorTagBuilderPrev = new TagBuilder("a");
                    anchorTagBuilderPrev.MergeAttribute("href", pageUrl(pagingInfo.CurrentPageIndex - 1));
                    anchorTagBuilderPrev.InnerHtml = "Précédente";
                    anchorTagBuilderPrev.MergeAttribute("id", "paging-prev");
                    stringBuilder.Append("<li>" + anchorTagBuilderPrev + "</li>");
                }

                //always display the first page
                if (pagingInfo.CurrentPageIndex == 1)
                {
                    stringBuilder.Append("<li class='active'>1</li>");
                }
                else
                {
                    var anchorTagBuilderCurrent = new TagBuilder("a");
                    anchorTagBuilderCurrent.MergeAttribute("href", pageUrl(1));
                    anchorTagBuilderCurrent.InnerHtml = string.Format("{0}", (1).ToString(CultureInfo.InvariantCulture));
                    stringBuilder.Append("<li>" + anchorTagBuilderCurrent + "</li>");
                }

                const int howManyTimes = 2 * maxVisibleLinks + 1;

                //restrict the range
                var left = Math.Max(2, pagingInfo.CurrentPageIndex - 2 * maxVisibleLinks - 1);
                var right = Math.Min(pagingInfo.TotalPages - 1, pagingInfo.CurrentPageIndex + 2 * maxVisibleLinks + 1);

                while (right - left > 2 * maxVisibleLinks)
                {
                    if (pagingInfo.CurrentPageIndex - left < right - pagingInfo.CurrentPageIndex)
                    {
                        right--;
                        right = right < pagingInfo.CurrentPageIndex ? pagingInfo.CurrentPageIndex : right;
                    }
                    else
                    {
                        left++;
                        left = left > pagingInfo.CurrentPageIndex ? pagingInfo.CurrentPageIndex : left;
                    }
                }
                if (left >= 3)
                {
                    stringBuilder.Append("<li>...</li>");
                }
                var outLeft = left;
                for (var i = 1; i <= howManyTimes; i++)
                {
                    if (outLeft > right)
                    {
                        continue;
                    }
                    if (outLeft == pagingInfo.CurrentPageIndex)
                    {
                        stringBuilder.Append("<li class='active'>" + outLeft + "</li>");
                    }
                    else
                    {
                        var anchorTagBuilder = new TagBuilder("a");
                        anchorTagBuilder.MergeAttribute("href", pageUrl(outLeft));
                        anchorTagBuilder.InnerHtml = string.Format("{0}", (outLeft).ToString(CultureInfo.InvariantCulture));
                        stringBuilder.Append("<li>" + anchorTagBuilder + "</li>");
                    }
                    outLeft++;
                }
                if (pagingInfo.TotalPages - right >= 2)
                {
                    stringBuilder.Append("<li>...</li>");
                }

                if (pagingInfo.CurrentPageIndex == pagingInfo.TotalPages)
                {
                    stringBuilder.Append("<li class='active'>" + pagingInfo.TotalPages + "</li>");
                }
                else
                {
                    var anchorTagBuilderN = new TagBuilder("a");
                    anchorTagBuilderN.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
                    anchorTagBuilderN.InnerHtml = string.Format("{0}", (pagingInfo.TotalPages).ToString(CultureInfo.InvariantCulture));
                    stringBuilder.Append("<li>" + anchorTagBuilderN + "</li>");
                }


                //Always display the last page
                if (pagingInfo.CurrentPageIndex < pagingInfo.TotalPages)
                {
                    var anchorTagBuilderSuiv = new TagBuilder("a");
                    anchorTagBuilderSuiv.MergeAttribute("href", pageUrl(pagingInfo.CurrentPageIndex + 1));
                    anchorTagBuilderSuiv.MergeAttribute("id", "paging-next");
                    anchorTagBuilderSuiv.InnerHtml = "Suivante";
                    stringBuilder.Append("<li>" + anchorTagBuilderSuiv + "</li>");
                }
            }
            return MvcHtmlString.Create(string.Format("<div class='pagination' id='pagination' data-courant='{1}'><ul>{0}</ul></div>", stringBuilder, pagingInfo.CurrentPageIndex));
        }
    }
}