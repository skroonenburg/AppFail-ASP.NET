using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AppFail.Mvc
{
    public static class AppFailHtmlHelpers
    {
        public static MvcHtmlString AppFailIncludes(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(AppFail.RenderIncludes());
        }

        public static MvcHtmlString AppFailStyles(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(AppFail.RenderStyles());
        }
    }
}
