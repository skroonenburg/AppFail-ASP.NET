using System;
using System.Collections.Generic;
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

        internal static string GetMachineName(HttpContextBase httpContext)
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

        internal static FailOccurrenceDto FromException(HttpContextBase httpContext, Exception e)
        {
            var baseException = e.GetBaseException();

            var request = httpContext.Request;
            var urlReferrer = request.UrlReferrer != null ? request.UrlReferrer.ToString() : null;

            var requestUrl = request.Url != null ? request.Url.ToString() : null;
            
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

            var httpException = e as HttpException;
            
            // Report all exceptions, starting from the top and moving toward the base exception
            var allExceptions = new List<ExceptionDto>();

            var nextException = e;

            while (nextException != null)
            {
                allExceptions.Insert(0, new ExceptionDto(nextException.StackTrace, nextException.Message, nextException.GetType().FullName));
                nextException = nextException.InnerException;
            }

            var report = new FailOccurrenceDto(allExceptions.ToArray(),
                                            requestUrl,
                                            request.HttpMethod,
                                            urlReferrer,
                                            DateTime.UtcNow,
                                            user,
                                            postValuePairs,
                                            queryValuePairs,
                                            request.UserAgent,
                                            serverVariables,
                                            cookies,
                                            machineName,
                                            
                                            httpException != null ? httpException.GetHttpCode() : (int?)null);

            return report;
        }
    }
}
