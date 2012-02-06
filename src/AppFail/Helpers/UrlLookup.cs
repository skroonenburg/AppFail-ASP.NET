using System;
using System.Web;

namespace AppFail.Helpers
{
    internal static class UrlLookup
    {
        internal static string GetFailsUrl
        {
            get { return ConfigurationModel.Instance.BaseApiUrl + "FailsFor/"; }
        }

        internal static string ReportFailUrl
        {
            get { return ConfigurationModel.Instance.BaseApiUrl + "Fail"; }
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
