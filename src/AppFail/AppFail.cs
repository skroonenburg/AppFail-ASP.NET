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
            var failReport = FailReportFactory.FromException(HttpContext.Current.Request, e);
            FailQueue.Enqueue(failReport);
        }
    }
}
