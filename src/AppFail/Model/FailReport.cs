namespace AppFail.Model
{
    internal class FailReport
    {
        internal FailReport(string exceptionType, string stackTrace, string relativeUrl, string verb, string urlReferrer, string exceptionMessage)
        {
            ExceptionType = exceptionType;
            StackTrace = stackTrace;
            Verb = verb;
            UrlReferrer = urlReferrer;
            ExceptionMessage = exceptionMessage;
            RelativeUrl = relativeUrl;
        }

        public string RelativeUrl { get; private set; }
        public string ExceptionType { get; private set; }
        public string StackTrace { get; private set; }
        public string Verb { get; private set; }
        public string UrlReferrer { get; private set; }
        public string ExceptionMessage { get; private set; }
    }
}
