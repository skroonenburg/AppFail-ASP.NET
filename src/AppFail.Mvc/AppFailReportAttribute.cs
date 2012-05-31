using System;
using System.Web.Mvc;

namespace AppfailReporting.Mvc
{
    /// <summary>
    /// A filter that causes all filters thrown by controller actions to be reported to Appfail.
    /// if you are using the MVC HandleError attribute, then it's important to define AppfailReport before
    /// HandleError, so that it is called for all exceptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AppfailReportAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            // Only log handled exceptions. Appfail will catch all unhandled exceptions anyway.
            if (filterContext.ExceptionHandled)
            {
                filterContext.Exception.SendToAppfail();
            }
        }
    }
}
