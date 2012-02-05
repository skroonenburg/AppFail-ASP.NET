using System;
using System.Text;
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
                return HttpContext.Current.Request.Url.AbsolutePath.Substring(1);
            }
        }

        internal static string EncodedCurrentUrl
        {
            get
            {
                var url = HttpContext.Current.Request.Url.ToString();
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
            }
        }

        public static string RenderIncludes()
        {
            return string.Format("{0}{1}", RenderHelperScriptIncludes(), RenderErrorDataIncludes());
        }

        internal static string RenderErrorDataIncludes()
        {
            return string.Format(@"<script src=""{0}{1}"" type=""text/javascript""></script>", UrlLookup.GetFailsUrl, HttpUtility.UrlEncode(EncodedCurrentUrl));
        }

        public static string RenderStyles()
        {
            return string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}""/>", UrlLookup.GetStylesUrl);
        }

        internal static string RenderHelperScriptIncludes()
        {
            return string.Format(@"<script src=""{0}"" type=""text/javascript""></script>", UrlLookup.GetScriptUrl);
        }
    }
}
