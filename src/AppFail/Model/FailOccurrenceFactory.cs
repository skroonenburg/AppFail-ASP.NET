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

            var relativeUrl = request.Url != null ? request.Url.ToString() : null;

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

            // Filter query & post value pairs according to locally defined rules
            string[][] postValuePairs = null;
            string[][] queryValuePairs = null;

            try
            {
                queryValuePairs = request.QueryString.Keys.OfType<string>()
                    .Where(x => !AppFail.IsPostFiltered(x))
                    .Select(k => new string[] {k, request.QueryString[k]}).ToArray();

                postValuePairs = request.Form.Keys.OfType<string>()
                                        .Where(x => !AppFail.IsPostFiltered(x))
                                        .Select(k => new string[] { k, request.Form[k] }).ToArray();
            }
            catch (HttpRequestValidationException)
            {}

            var report = new FailOccurrence(e.GetType().FullName,
                                            e.StackTrace,
                                            relativeUrl,
                                            request.HttpMethod,
                                            urlReferrer,
                                            e.Message,
                                            DateTime.UtcNow,
                                            user,
                                            postValuePairs,
                                            queryValuePairs);

            return report;
        }
    }
}
