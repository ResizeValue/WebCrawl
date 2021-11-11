using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawl.WebApplication.Helpers
{
    public static class Paging
    {
        public static StringHtmlContent GetPaging(this HtmlHelper helper, string id, int curPage, int totalPages)
        {
            TagBuilder tb = new TagBuilder("input");
            tb.Attributes.Add("type", "button");
            tb.AddCssClass("btn btn-secondary m-1 p-2");


            return new StringHtmlContent(tb.ToString());
        }
    }
}
