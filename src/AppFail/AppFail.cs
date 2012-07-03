using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using AppfailReporting.Helpers;
using AppfailReporting.Model;
using AppfailReporting.Reporting;

namespace AppfailReporting
{
    public static class Appfail
    {
        /// <summary>
        /// Sends the given exception to Appfail
        /// </summary>
        /// <param name="e"></param>
        public static void SendToAppfail(this Exception e, HttpContextBase httpContext = null)
        {
            if (e == null)
            {
                return;
            }

            try
            {
                if (httpContext == null)
                {
                    httpContext = new HttpContextWrapper(HttpContext.Current);
                }

                if (ConfigurationModel.Instance.DisableInDebugMode && httpContext.IsDebuggingEnabled)
                {
                    // We are in debug mode, and should disable during debug mode, so exit and don't report the failure.
                    return;
                }

                var url = httpContext.Request.Url.AbsolutePath.ToString();

                // The base exception is the true 'cause' of the error, this is what we report for the
                // exception type, message and stack trace.
                var baseException = e.GetBaseException();

                if (!IsFilteredByFluentExpression(baseException, url) && !IsFilteredByWebConfig(baseException, url))
                {
                    var failReport = FailOccurrenceFactory.FromException(httpContext, e);
                    FailQueue.Enqueue(failReport);
                }
            }
            catch (Exception)
            {
                // Yes this is a catch-all exception, but warranted here. Appfail's reporting module
                // should NEVER cause an unhandled exception. We can't be bringing down client applications.
            }
        }

        public static IAppfailConfigurationBuilder Configure
        {
            get { return new AppfailConfigurationBuilder(); }
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
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(HttpContext.Current.Request.Url.AbsolutePath));
            }
        }

        public static string RenderIncludes()
        {
            return String.Format(@"<script src=""{0}"" type=""text/javascript""></script>", UrlLookup.GetScriptUrl);
        }

        internal static bool IsPostFiltered(string name)
        {
            return ConfigurationModel.Instance.FilteredPostNamesContaining.Any(x => name.Contains(x))
                   || ConfigurationModel.Instance.IgnorePostValuesSettingsFromWebConfig.Any(x => name.Contains(x.NameContains));
        }

        internal static bool IsCookieFiltered(string name)
        {
            return ConfigurationModel.Instance.FilteredCookieNamesContaining.Any(x => name.Contains(x))
                   || ConfigurationModel.Instance.IgnoreCookiesSettingsFromWebConfig.Any(x => name.Contains(x.NameContains));
        }

        internal static bool IsServerVariableFiltered(string name)
        {
            return ConfigurationModel.Instance.FilteredServerVariableNamesContaining.Any(x => name.Contains(x))
                   || ConfigurationModel.Instance.IgnoreServerVariablesSettingsFromWebConfig.Any(x => name.Contains(x.NameContains));
        }

       
        internal static bool IsFilteredByFluentExpression(Exception e, string url)
        {
            if (ConfigurationModel.Instance.FilteredExceptionsByLambda.Any(item => item(e))
                || ConfigurationModel.Instance.FilteredExceptionsByType.Contains(e.GetType())
                || ConfigurationModel.Instance.FilteredExceptionsByRegex.Exists(m => m.Match(e.Message).Success)
                || ConfigurationModel.Instance.FilteredExceptionByRelativeUrls.Any(u => UrlHelpers.UrlsAreEqual(url, u))
                || ConfigurationModel.Instance.FilteredExceptionByRelativeUrlsContaining.Any(u => UrlHelpers.UrlContains(url, u))
                || ConfigurationModel.Instance.FilteredExceptionByRelativeUrlsStartingWith.Any(u => UrlHelpers.UrlStartsWith(url, u)))
            {
                return true;
            }

            if (e is HttpException)
            {
                var httpStatusCode = GetHttpStatusCode(e as HttpException);

                if (ConfigurationModel.Instance.FilteredExceptionsByHttpStatusCode.Contains(httpStatusCode))
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool IsFilteredByWebConfig(Exception e, string url)
        {
            if (ConfigurationModel.Instance.IgnoreExceptionSettingsFromWebConfig.Count > 0)
            {
                foreach (var element in ConfigurationModel.Instance.IgnoreExceptionSettingsFromWebConfig)
                {
                    switch (element.Type)
                    {
                        case "HttpStatusCode":
                            {
                                var httpStatusCode = 0;

                                if (int.TryParse(element.Value, out httpStatusCode))
                                {
                                    if (e is HttpException)
                                    {
                                        var exceptionHttpStatusCode = GetHttpStatusCode(e as HttpException);

                                        if (httpStatusCode == (int)exceptionHttpStatusCode)
                                        {
                                            return true;
                                        }
                                    }
                                }

                                break;
                            }

                        case "ExceptionMessage":
                            {
                                try
                                {
                                    var regex = new Regex(element.Value);

                                    if (regex.Match(e.Message).Success)
                                    {
                                        return true; 
                                    }
                                }
                                catch (Exception regex)
                                {
                                    Debug.WriteLine(string.Format("Appfail: Could not parse or evaluate regex value - {0}" + regex.Message));
                                }

                                break;
                            }

                        case "HttpExceptionType":
                            {
                                if (string.Equals(element.Value, e.GetType().Name) || string.Equals(element.Value, e.GetType().FullName))
                                {
                                    return true;
                                }

                                break;
                            }
                        case "RelativeUrlContains":
                            {
                                return UrlHelpers.UrlContains(url, element.Value);
                            }
                        case "RelativeUrlStartsWith":
                            {
                                return UrlHelpers.UrlStartsWith(url, element.Value);
                            }
                        case "RelativeUrl":
                            {
                                return UrlHelpers.UrlsAreEqual(url, element.Value);
                            }
                    }
                }
            }


            return false;
        }

        internal static HttpStatusCode GetHttpStatusCode(HttpException e)
        {
            var httpCode = (e).GetHttpCode();

            return (HttpStatusCode) httpCode;
        }
    }
}
