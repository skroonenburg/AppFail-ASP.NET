using System;

namespace AppfailReporting.Model
{
    internal class FailOccurrence
    {
        internal FailOccurrence(string exceptionType, string stackTrace, string relativeUrl, string verb, string referrerUrl, string exceptionMessage, DateTime occurrenceTimeUtc, string user, string[][] postValuePairs, string [][] queryValuePairs)
        {
            ExceptionType = exceptionType;
            StackTrace = stackTrace;
            HttpVerb = verb;
            ReferrerUrl = referrerUrl;
            ExceptionMessage = exceptionMessage;
            RelativeUrl = relativeUrl;
            ApplicationType = "ASP.NET";
            OccurrenceTimeUtc = occurrenceTimeUtc.ToString("MM/dd/yyyy HH:mm:ss.FFFF");
            User = user;
            PostValuePairs = postValuePairs;
            QueryValuePairs = queryValuePairs;
            SubmissionAttempts = 0;
            UniqueId = Guid.NewGuid().ToString();
        }

        public string RelativeUrl { get; private set; }
        public string ExceptionType { get; private set; }
        public string StackTrace { get; private set; }
        public string HttpVerb { get; private set; }
        public string ReferrerUrl { get; private set; }
        public string ExceptionMessage { get; private set; }
        public string ApplicationType { get; private set; }
        public string OccurrenceTimeUtc { get; private set; }
        public string User { get; private set; }
        public string[][] PostValuePairs { get; private set; }
        public string[][] QueryValuePairs { get; private set; }
        public string UniqueId { get; private set; }

        internal int SubmissionAttempts { get; private set; }
        internal void IncrementSubmissionAttempts()
        {
            SubmissionAttempts += 1;
        }
    }
}
