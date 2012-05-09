using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using AppFail.Helpers;

namespace AppFail
{
    public class AppFailModule : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.Error += ApplicationOnError;
        }

        private void ApplicationOnError(object sender, EventArgs eventArgs)
        {
            // Invoked when the asp.net application encounters an error
            // Need to be cautious not to cause unhandled exceptions in this code.
            var current = HttpContext.Current;
            if (current != null && current.Server != null)
            {
                var exception = HttpContext.Current.Server.GetLastError();
                exception.SendToAppFail();
            }
        }

        public void Dispose()
        {
        }
    }
}
