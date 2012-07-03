using System;
using System.Reflection;

namespace AppfailReporting.Model
{
    internal class FailOccurrenceDto
    {
        internal FailOccurrenceDto(ExceptionDto[] exceptions, string relativeUrl, string verb, string referrerUrl, DateTime occurrenceTimeUtc, string user, string[][] postValuePairs, string[][] queryValuePairs, string userAgent, string[][] serverVariables, string[][] cookies, string machineName, int? httpStatus)
        {
            Exceptions = exceptions;
            HttpVerb = verb;
            ReferrerUrl = referrerUrl;
            RequestUrl = relativeUrl;
            OccurrenceTimeUtc = occurrenceTimeUtc.ToString("MM/dd/yyyy HH:mm:ss.FFFF");
            User = user;
            PostValuePairs = postValuePairs;
            QueryValuePairs = queryValuePairs;
            SubmissionAttempts = 0;
            UniqueId = Guid.NewGuid().ToString();
            UserAgent = userAgent;
            ServerVariables = serverVariables;
            Cookies = cookies;
            MachineName = machineName;
            HttpStatus = httpStatus;
        }

        public string RequestUrl { get; private set; }
        public string ExceptionType { get; private set; }
        public string StackTrace { get; private set; }
        public string HttpVerb { get; private set; }
        public string ReferrerUrl { get; private set; }
        public string ExceptionMessage { get; private set; }
        public string OccurrenceTimeUtc { get; private set; }
        public string User { get; private set; }
        public string[][] PostValuePairs { get; private set; }
        public string[][] QueryValuePairs { get; private set; }
        public string[][] ServerVariables { get; private set; }
        public string[][] Cookies { get; private set; }
        public string UniqueId { get; private set; }
        public string UserAgent { get; private set; }
        public string MachineName { get; private set; }
        public int? HttpStatus { get; private set; }
        public ExceptionDto[] Exceptions { get; private set; }

        internal int SubmissionAttempts { get; private set; }
        internal void IncrementSubmissionAttempts()
        {
            SubmissionAttempts += 1;
        }
    }
}
