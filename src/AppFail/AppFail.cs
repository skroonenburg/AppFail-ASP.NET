using System;
using System.Text;
using System.Web;
using AppFail.Helpers;
using AppFail.Model;
using AppFail.Reporting;

namespace AppFail
{
    public static class AppFail
    {
        /// <summary>
        /// Sends the given exception to AppFail
        /// </summary>
        /// <param name="e"></param>
        public static void SendToAppFail(this Exception e)
        {
            var failReport = FailOccurrenceFactory.FromException(HttpContext.Current, e);
            FailQueue.Enqueue(failReport);
        }

        public static IAppFailConfigurationBuilder Configure
        {
            get { return new AppFailConfigurationBuilder(); }
        }

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
            return String.Format("{0}{1}", RenderHelperScriptIncludes(), RenderErrorDataIncludes());
        }

        internal static string RenderErrorDataIncludes()
        {
            return String.Format(@"<script src=""{0}{1}"" type=""text/javascript""></script>", UrlLookup.GetFailsUrl, HttpUtility.UrlEncode(EncodedCurrentUrl));
        }

        public static string RenderStyles()
        {
            return String.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}""/>", UrlLookup.GetStylesUrl);
        }

        internal static string RenderHelperScriptIncludes()
        {
            return String.Format(@"<script src=""{0}"" type=""text/javascript""></script>", UrlLookup.GetScriptUrl);
        }
    }
}
