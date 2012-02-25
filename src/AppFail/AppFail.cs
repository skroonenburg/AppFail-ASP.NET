using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using AppFail.Filtering;
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
            if (!IsFilteredByFluentExpression(e) && !IsFilteredByWebConfig(e))
            {
                var failReport = FailOccurrenceFactory.FromException(HttpContext.Current, e);
                FailQueue.Enqueue(failReport);
            }
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

        public static string GetPublicCurrentUrl(HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Host = request.Url.Host,
                Path = request.Url.AbsolutePath,
                Port = request.Url.Port,
                Scheme = request.Url.Scheme
            };

            if (ConfigurationModel.Instance.AppHarborCompatibilityMode)
            {
                uriBuilder.Port = 80;
            }

            return uriBuilder.Uri.ToString();
        }

        internal static string EncodedCurrentUrl
        {
            get
            {
                var url = GetPublicCurrentUrl(HttpContext.Current.Request);
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

        internal static bool IsFilteredByFluentExpression(Exception e)
        {
            if (ConfigurationModel.Instance.FilteredExceptionsByLambda.Any(item => item(e)))
            {
                return true;
            }
            if (ConfigurationModel.Instance.FilteredExceptionsByType.Contains(e.GetType()))
            {
                return true;
            }
                   
            if (ConfigurationModel.Instance.FilteredExceptionsByRegex.Exists(m => m.Match(e.Message).Success))
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

        internal static bool IsFilteredByWebConfig(Exception e)
        {
            if (ConfigurationModel.Instance.GetIgnoreSettingsFromWebConfig.Count > 0)
            {
                foreach (var element in ConfigurationModel.Instance.GetIgnoreSettingsFromWebConfig)
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
                                    Debug.WriteLine(string.Format("AppFail: Could not parse or evaluate regex value - {0}" + regex.Message));
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
