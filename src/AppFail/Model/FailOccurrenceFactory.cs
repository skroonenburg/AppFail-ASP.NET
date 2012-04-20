using System;
using System.Linq;
using System.Web;

namespace AppFail.Model
{
    internal static class FailOccurrenceFactory
    {
        internal static FailOccurrence FromException(HttpContext httpContext, Exception e)
        {
            var request = httpContext.Request;
            var urlReferrer = request.UrlReferrer != null ? request.UrlReferrer.ToString() : null;

            var relativeUrl = request.Url != null ? request.Url.AbsolutePath : null;

            string user = null;

            if (ConfigurationModel.Instance.ReportCurrentUsername)
            {
                if (ConfigurationModel.Instance.PopulateUsernameFrom != null)
                {
                    user = ConfigurationModel.Instance.PopulateUsernameFrom();
                }
                else if (httpContext.User != null && httpContext.User.Identity != null)
                {
                    user = httpContext.User.Identity.Name;
                }
            }

            var postValuePairs = request.Form.Keys.OfType<string>().Select(k => new string[] {k, request.Form[k]}).ToArray();
            var queryValuePairs = request.QueryString.Keys.OfType<string>().Select(k => new string[] { k, request.QueryString[k] }).ToArray();

            var report = new FailOccurrence(e.GetType().FullName, e.StackTrace, relativeUrl, request.HttpMethod, urlReferrer, e.Message, DateTime.UtcNow, user, postValuePairs, queryValuePairs);

            return report;
        }
    }
}
