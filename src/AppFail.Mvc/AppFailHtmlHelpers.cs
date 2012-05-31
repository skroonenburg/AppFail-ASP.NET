using System.Web.Mvc;

namespace Appfail.Reporting.Mvc
{
    public static class AppfailHtmlHelpers
    {
        /// <summary>
        /// Includes a script tag to load the Appfail overlay on this page.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString IncludeAppfailOverlay(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(Appfail.RenderIncludes());
        }
    }
}
