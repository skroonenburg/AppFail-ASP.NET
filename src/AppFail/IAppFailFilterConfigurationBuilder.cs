using System;
using System.Net;
using System.Text.RegularExpressions;

namespace AppfailReporting
{
    public interface IAppfailFilterConfigurationBuilder
    {
        /// <summary>
        /// Filters exceptions reported to Appfail to ignore any of the given exception types.
        /// </summary>
        /// <param name="exception">An array of exception types that should be ignore.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder WithTypes(params Type[] exceptions);

        /// <summary>
        /// Filters exceptions reported to Appfail to ignore any where the exception message matches any of the given regular expressions.
        /// </summary>
        /// <param name="exceptions">An array of regular expressions.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder WithMessagesMatchingRegex(params Regex[] exceptions);

        /// <summary>
        /// Filters exceptions reported to Appfail to ignore any where the exception message matches any of the given regular expressions.
        /// </summary>
        /// <param name="exceptions">An array of regular expressions.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder WithMessagesMatchingRegex(params String[] exceptions);

        /// <summary>
        /// Filters exceptions reported to Appfail to ignore those that cause the given custom functions to return true.
        /// </summary>
        /// <param name="exceptionFilterFunctions">An array of custom filter functions.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder Where(params Func<Exception, bool>[] exceptionFilterFunctions);

        /// <summary>
        ///  Filters exceptions reported to Appfail to ignore those that are HttpExceptions with any of the given HTTP status codes.
        /// </summary>
        /// <param name="httpStatusCode">An array of HTTP status codes.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder WithHttpStatusCodes(params HttpStatusCode[] httpStatusCode);

        /// <summary>
        ///  Filters exceptions reported to Appfail to ignore those that were thrown handling a request with a relative URL matching any of the given values.
        /// </summary>
        /// <param name="urlsContaining">An array of strings.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder WithRelativeUrls(params string[] urlsContaining);

        /// <summary>
        ///  Filters exceptions reported to Appfail to ignore those that were thrown handling a request with a relative URL containing any of the given values.
        /// </summary>
        /// <param name="urlsContaining">An array of strings.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder WithRelativeUrlsContaining(params string [] urlsContaining);

        /// <summary>
        ///  Filters exceptions reported to Appfail to ignore those that were thrown handling a request with a relative URL starting with any of the given values.
        /// </summary>
        /// <param name="urlsContaining">An array of strings.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder WithRelativeUrlsStartingWith(params string [] urlsStartingWith);
    }
}
