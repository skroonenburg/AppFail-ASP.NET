using System;
using System.Configuration;

namespace AppFail
{
    public class AppFailConfiguration : ConfigurationSection
    {
        public static AppFailConfiguration Current
        {
            get
            {
                return (AppFailConfiguration)ConfigurationManager.GetSection("appFail");
            }
        }

        [ConfigurationProperty("apiToken", IsRequired = true)]
        public string ApiToken
        {
            get { return (string)this["apiToken"]; }
        }

        [ConfigurationProperty("reportingMinimumBatchSize", DefaultValue = 10)]
        public int ReportingMinimumBatchSize
        {
            get { return (int)this["reportingMinimumBatchSize"]; }
        }

        [ConfigurationProperty("reportingOccurrenceMaxSizeBytes", DefaultValue = (long)102400)] // 100KB per occurrence
        public long ReportingOccurrenceMaxSizeBytes
        {
            get { return (long)this["reportingOccurrenceMaxSizeBytes"]; }
        }

        [ConfigurationProperty("reportingMaximumIntervalMinutes", DefaultValue = 1)]
        public int ReportingMaximumIntervalMinutes
        {
            get { return (int)this["reportingMaximumIntervalMinutes"]; }
        }

        [ConfigurationProperty("baseApiUrl", DefaultValue = "http://api.appfail.net/")]
        public string BaseApiUrl
        {
            get { return (string)this["baseApiUrl"]; }
        }

        [ConfigurationProperty("reportCurrentUsername", DefaultValue = true)]
        public bool ReportCurrentUsername
        {
            get { return (bool)this["reportCurrentUsername"]; }
        }
    }
}