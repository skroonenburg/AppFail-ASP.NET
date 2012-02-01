using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFail
{
    internal class ConfigurationModel
    {
        private ConfigurationModel()
        {}

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

        private string EnforceTrailingSlash(string url)
        {
            return url.Trim().EndsWith("/") ? url : url.Trim() + "/";
        }
    }
}
