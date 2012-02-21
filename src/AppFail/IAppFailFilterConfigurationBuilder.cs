using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFail
{
    public interface IAppFailFilterConfigurationBuilder
    {
        /// <summary>
        /// Allows to fluently specify an array of exceptions that the ASP.NET module will not log
        /// </summary>
        /// <param name="exception">An array of types that are exceptions</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder Type(params Type[] exceptions);

        /// <summary>
        /// Allows to fluently specify an array of regex expressions to filter out exceptions
        /// </summary>
        /// <param name="exceptions">An array of regex expressions</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder Regex(params Regex[] exceptions);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptions"></param>
        /// <returns></returns>
        IAppFailConfigurationBuilder Regex(params String[] exceptions);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        IAppFailConfigurationBuilder Exception(params Func<Exception, bool>[] exception);
    }
}
