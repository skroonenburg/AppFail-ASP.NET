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
        /// <summary>
        /// Includes a script tag to load the AppFail overlay on this page.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString IncludeAppFailOverlay(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(AppFail.RenderIncludes());
        }
    }
}
