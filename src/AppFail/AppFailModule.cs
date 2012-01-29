using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AppFail
{
    public class AppFailModule : IHttpModule
    {
        internal const string GetScriptUrl = "/__AppFail/Include/Script";
        internal const string GetStylesUrl = "/__AppFail/Include/Styles";

        private IDictionary<string, Action<string, HttpContext>> _urlHandlers = new Dictionary<string, Action<string, HttpContext>>
                                                                                    {
                                                                                        { GetScriptUrl, HandleScriptRequest },
                                                                                        { GetStylesUrl, HandleStylesRequest }
                                                                                    }; 
        public void Init(HttpApplication application)
        {
            application.Error += ApplicationOnError;
            application.PreRequestHandlerExecute += ApplicationOnPreRequestHandlerExecute;
        }

        private void ApplicationOnPreRequestHandlerExecute(object sender, EventArgs eventArgs)
        {
            // Is this a request that we should override?
            var httpContext = HttpContext.Current;
            var request = httpContext.Request;

            var url = request.Url.PathAndQuery;
            
            if (url == null)
            {
                return;
            }

            // Invoke the handlers for this url
            var handled = false;
            foreach (var handler in _urlHandlers.Where(kvp => url.StartsWith(kvp.Key)))
            {
                handler.Value(url, httpContext);
                handled = true;
            }

            if (handled)
            {
                // Tell ASP.NET that the request has been handled, and to ignore the handler.
                httpContext.ApplicationInstance.CompleteRequest();
            }
        }

        private static void HandleScriptRequest(string url, HttpContext httpContext)
        {
            // TODO: Cache this in memory
            using (var sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("AppFailReporting.HelperScript.js")))
            {
                httpContext.Response.Write(sr.ReadToEnd());
                httpContext.Response.Flush();
            }
        }

        private static void HandleStylesRequest(string url, HttpContext httpContext)
        {
            // TODO: Cache this in memory
            using (var sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("AppFailReporting.HelperStyles.css")))
            {
                 httpContext.Response.Write(sr.ReadToEnd());
                httpContext.Response.ContentType = "text/css";
                httpContext.Response.Flush();
            }
        }

        private void ApplicationOnError(object sender, EventArgs eventArgs)
        {
            // Invoked when the asp.net application encounters an error
            var exception = HttpContext.Current.Server.GetLastError();
            exception.SendToAppFail();
        }

        public void Dispose()
        {
            
        }
    }
}
