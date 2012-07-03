using System;
using System.Web;

namespace AppfailReporting
{
    public class AppfailModule : IHttpModule
    {
        void IHttpModule.Init(HttpApplication application)
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

                exception.SendToAppfail();
            }
        }

        public void Dispose()
        {
        }
    }
}
