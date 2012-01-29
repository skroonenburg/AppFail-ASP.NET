using System;
using System.Web;

namespace AppFail.Model
{
    internal static class FailReportFactory
    {
        internal static FailReport FromException(HttpRequest request, Exception e)
        {
            var urlReferrer = request.UrlReferrer != null ? request.UrlReferrer.ToString() : null;
            var relativeUrl = request.Url != null ? request.Url.PathAndQuery : null;
            if (relativeUrl != null && relativeUrl.Length > 0)
            {
                relativeUrl = relativeUrl.Substring(1);
            }

            var report = new FailReport(e.GetType().FullName, e.StackTrace, relativeUrl, request.HttpMethod, urlReferrer, e.Message);

            return report;
        }
    }
}
