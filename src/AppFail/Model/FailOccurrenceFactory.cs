using System;
using System.Collections.Specialized;
using System.Linq;
using System.Security;
using System.Web;

namespace AppfailReporting.Model
{
    internal static class FailOccurrenceFactory
    {
        internal static string Mask(Func<string, bool> maskCallback, string key, string maskValue)
        {
            if (maskValue == null)
            {
                return null;
            }

            if (maskCallback == null)
            {
                return maskValue;
            }

            if (maskCallback(key))
            {
                return "####";
            }

            return maskValue;
        }

        internal static string[][] CreateCopy(NameValueCollection nameValueCollection, Func<string, bool> includeCallback = null)
        {
            if (includeCallback == null)
            {
                includeCallback = x => false;
            }

            return nameValueCollection.AllKeys
                    .Select(k => new string[] { k, Mask(includeCallback, k, nameValueCollection[k]) }).ToArray();
        }

        internal static string[][] CreateCopy(HttpCookieCollection cookieCollection, Func<string, bool> includeCallback = null)
        {
            if (includeCallback == null)
            {
                includeCallback = x => false;
            }

            return cookieCollection.AllKeys
                    .Select(c => new string[] { c, Mask(includeCallback, cookieCollection[c].Name, cookieCollection[c].Value) }).ToArray();
        }

        internal static string GetMachineName(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                try
                {
                    return System.Environment.MachineName;
                }
                catch (SecurityException)
                {
                }

                return null;
            }

            try
            {
                return httpContext.Server.MachineName;
            }
            catch (HttpException)
            {
            }
            catch (SecurityException)
            {
            }

            return null;
        }

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
            
            string machineName = GetMachineName(httpContext);

            // Filter query & post value pairs according to locally defined rules
            string[][] postValuePairs = null;
            string[][] queryValuePairs = null;
            string[][] serverVariables = null;
            string[][] cookies = null;

            try
            {
                queryValuePairs = CreateCopy(request.QueryString, x => Appfail.IsPostFiltered(x));
                postValuePairs = CreateCopy(request.Form, x => Appfail.IsPostFiltered(x));
                serverVariables = CreateCopy(request.ServerVariables, x => Appfail.IsServerVariableFiltered(x));
                cookies = CreateCopy(request.Cookies, x => Appfail.IsCookieFiltered(x));
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
                                            queryValuePairs,
                                            request.UserAgent,
                                            serverVariables,
                                            cookies,
                                            machineName);

            return report;
        }
    }
}
