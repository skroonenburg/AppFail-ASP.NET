using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFail
{
    public interface IAppFailFilterConfigurationBuilder
    {
        /// <summary>
        /// Filters exceptions reported to AppFail to ignore any of the given exception types.
        /// </summary>
        /// <param name="exception">An array of exception types that should be ignore.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder WithTypes(params Type[] exceptions);

        /// <summary>
        /// Filters exceptions reported to AppFail to ignore any where the exception message matches any of the given regular expressions.
        /// </summary>
        /// <param name="exceptions">An array of regular expressions.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder WithMessagesMatchingRegex(params Regex[] exceptions);

        /// <summary>
        /// Filters exceptions reported to AppFail to ignore any where the exception message matches any of the given regular expressions.
        /// </summary>
        /// <param name="exceptions">An array of regular expressions.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder WithMessagesMatchingRegex(params String[] exceptions);

        /// <summary>
        /// Filters exceptions reported to AppFail to ignore those that cause the given custom functions to return true.
        /// </summary>
        /// <param name="exceptionFilterFunctions">An array of custom filter functions.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder Where(params Func<Exception, bool>[] exceptionFilterFunctions);

        /// <summary>
        ///  Filters exceptions reported to AppFail to ignore those that are HttpExceptions with any of the given HTTP status codes.
        /// </summary>
        /// <param name="httpStatusCode">An array of HTTP status codes.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder WithHttpStatusCodes(params HttpStatusCode[] httpStatusCode);

        /// <summary>
        ///  Filters exceptions reported to AppFail to ignore those were thrown handling a request with a relative URL containing any of the given values.
        /// </summary>
        /// <param name="urlsContaining">An array of strings.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder WithRelativeUrlsContaining(params string [] urlsContaining);

        /// <summary>
        ///  Filters exceptions reported to AppFail to ignore those were thrown handling a request with a relative URL starting with any of the given values.
        /// </summary>
        /// <param name="urlsContaining">An array of strings.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder WithRelativeUrlsStartingWith(params string [] urlsStartingWith);
    }
}
