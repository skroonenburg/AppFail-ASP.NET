using System;
using System.Web;

namespace AppFail.Helpers
{
    internal static class UrlLookup
    {
        internal const string GetFailsApiVersion = "1";
        internal const string ReportApiVersion = "1";

        internal static string GetFailsUrl
        {
            get { return string.Format("{0}v{1}/{2}", ConfigurationModel.Instance.BaseApiUrl, GetFailsApiVersion, "Fails/Url/"); }
        }

        internal static string ReportFailUrl
        {
            get { return string.Format("{0}{1}", ConfigurationModel.Instance.BaseApiUrl, "Fail"); }
        }

        internal static string GetScriptUrl
        {
            get { return VirtualPathUtility.ToAbsolute("/__AppFail/Include/Script"); }
        }
        
        internal static string GetStylesUrl
        {
            get { return VirtualPathUtility.ToAbsolute("~/__AppFail/Include/Styles"); }
        }
    }
}
