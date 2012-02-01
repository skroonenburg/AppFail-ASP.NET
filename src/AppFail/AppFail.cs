using System;
using System.Web;
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
        internal static void SendToAppFail(this Exception e)
        {
            var failReport = FailOccurrenceFactory.FromException(HttpContext.Current, e);
            FailQueue.Enqueue(failReport);
        }

        public static IAppFailConfigurationBuilder Configure
        {
            get { return new AppFailConfigurationBuilder(); }
        }
    }
}
