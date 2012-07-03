namespace AppfailReporting.Helpers
{
    internal static class UrlLookup
    {
        internal const string GetFailsApiVersion = "1";
        internal const string ReportApiVersion = "2";

        internal static string ReportFailUrl
        {
            get { return string.Format("{0}{1}", ConfigurationModel.Instance.BaseApiUrl, "Fail"); }
        }

        internal static string GetScriptUrl
        {
            get { return string.Format("{0}{1}", ConfigurationModel.Instance.BaseApiUrl, "appfail-overlay.js"); }
        }
    }
}
