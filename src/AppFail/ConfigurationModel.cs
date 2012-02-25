using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using AppFail.Filtering;

namespace AppFail
{
    internal class ConfigurationModel
    {
        private ConfigurationModel()
        {
            FilteredExceptionsByType = new List<Type>();
            FilteredExceptionsByRegex = new List<Regex>();
            FilteredExceptionsByLambda = new List<Func<Exception, bool>>();
            FilteredExceptionsByHttpStatusCode = new List<HttpStatusCode>();
        }

        static ConfigurationModel()
        {
            Instance = new ConfigurationModel();
        }

        public static ConfigurationModel Instance { get; internal set; }

        public Func<string> UserPopulationCallback { get; set; }

        private int? _reportingMinimumBatchSize;
        public int ReportingMinimumBatchSize
        {
            get { return _reportingMinimumBatchSize ?? AppFailConfiguration.Current.ReportingMinimumBatchSize; }
            set { _reportingMinimumBatchSize = value; }
        }

        private TimeSpan? _reportingMaximumInterval;
        public TimeSpan ReportingMaximumInterval
        {
            get { return _reportingMaximumInterval ?? TimeSpan.FromMinutes(AppFailConfiguration.Current.ReportingMaximumIntervalMinutes); }
            set { _reportingMaximumInterval = value; }
        }

        public Func<string> PopulateUsernameFrom { get; set; }

        private string _apiToken;
        public string ApiToken
        {
            get { return _apiToken ?? AppFailConfiguration.Current.ApiToken; }
            set { _apiToken = value; }
        }

        private string _baseApiUrl;
        public string BaseApiUrl
        {
            get { return EnforceTrailingSlash(_baseApiUrl ?? AppFailConfiguration.Current.BaseApiUrl); }
            set { _baseApiUrl = value; }
        }

        private bool? _reportCurrentUsername;
        public bool ReportCurrentUsername
        {
            get { return _reportCurrentUsername ?? AppFailConfiguration.Current.ReportCurrentUsername; }
            set { _reportCurrentUsername = value; }
        }

        private bool? _appHarborCompatibilityMode;
        public bool AppHarborCompatibilityMode
        {
            get { return _appHarborCompatibilityMode ?? AppSettingsAppHarborCompatibilityMode; }
            set { _appHarborCompatibilityMode = value; }
        }

        private static bool AppSettingsAppHarborCompatibilityMode
        {
            get
            {
                var mode = ConfigurationManager.AppSettings["appHarborCompatibilityMode"];
                var modeBool = false;

                if (bool.TryParse(mode, out modeBool))
                {
                    return modeBool;
                }

                return false;
            }
        }
        private string EnforceTrailingSlash(string url)
        {
            return url.Trim().EndsWith("/") ? url : url.Trim() + "/";
        }

        private List<Type> _filteredExceptionsByType;
        public List<Type> FilteredExceptionsByType
        {
            get { return _filteredExceptionsByType; }
            set { _filteredExceptionsByType = value; }
        }

        private List<Regex> _filteredExceptionsByRegex;
        public List<Regex> FilteredExceptionsByRegex
        {
            get { return _filteredExceptionsByRegex; }
            set { _filteredExceptionsByRegex = value; }
        }

        private List<Func<Exception, bool>> _filteredExceptionsByLambda;
        public List<Func<Exception, bool>> FilteredExceptionsByLambda
        {
            get { return _filteredExceptionsByLambda; }
            set { _filteredExceptionsByLambda = value; }
        }

        private List<HttpStatusCode> _filteredExceptionsByHttpStatusCode;
        public List<HttpStatusCode> FilteredExceptionsByHttpStatusCode
        {
            get { return _filteredExceptionsByHttpStatusCode; }
            set { _filteredExceptionsByHttpStatusCode = value; }
        }

        public ReferencedConfigurationElementCollection<IgnoreElement> GetIgnoreSettingsFromWebConfig
        {
            get { return AppFailConfiguration.Current.Ignore; }
        }

    }
}
