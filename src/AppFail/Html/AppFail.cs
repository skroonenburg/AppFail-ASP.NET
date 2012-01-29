using System.Web;
using AppFail.Helpers;

namespace AppFail.Html
{
    public static class AppFailHtml
    {
        internal static string CurrentRelativePath
        {
            get
            {
                return HttpContext.Current.Request.Url.PathAndQuery.Substring(1);
            }
        }

        public static string RenderIncludes()
        {
            return string.Format("{0}{1}", RenderHelperScriptIncludes(), RenderErrorDataIncludes());
        }

        internal static string RenderErrorDataIncludes()
        {
            return string.Format(@"<script src=""{0}{1}"" type=""text/javascript""></script>", UrlLookup.GetFailsUrl, HttpUtility.UrlEncode(CurrentRelativePath.Replace("/", "-")));
        }

        public static string RenderStyles()
        {
            return string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}""/>", AppFailModule.GetStylesUrl);
        }

        internal static string RenderHelperScriptIncludes()
        {
            return string.Format(@"<script src=""{0}"" type=""text/javascript""></script>", AppFailModule.GetScriptUrl);
        }
    }
}
