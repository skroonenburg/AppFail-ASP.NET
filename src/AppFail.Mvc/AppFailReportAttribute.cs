using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AppFail.Mvc
{
    /// <summary>
    /// A filter that causes all filters thrown by controller actions to be reported to AppFail.
    /// if you are using the MVC HandleError attribute, then it's important to define AppFailReport before
    /// HandleError, so that it is called for all exceptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AppFailReportAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            // Send this exception to appfail
            filterContext.Exception.SendToAppFail();
        }
    }
}
