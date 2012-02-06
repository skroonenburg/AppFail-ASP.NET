using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFail
{
    internal sealed class AppFailConfigurationBuilder : IAppFailConfigurationBuilder
    {
        public IAppFailConfigurationBuilder ReportingMinimumBatchSize(int minimumBatchSize)
        {
            ConfigurationModel.Instance.ReportingMinimumBatchSize = minimumBatchSize;
            return this;
        }

        public IAppFailConfigurationBuilder ReportingMaxmimumInterval(TimeSpan maximumInterval)
        {
            ConfigurationModel.Instance.ReportingMaximumInterval = maximumInterval;
            return this;
        }

        public IAppFailConfigurationBuilder PopulateUsernameWith(Func<string> populateUserCallback)
        {
            ConfigurationModel.Instance.PopulateUsernameFrom = populateUserCallback;
            return this;
        }

        public IAppFailConfigurationBuilder DoNotReportUsername
        {
            get
            {
                ConfigurationModel.Instance.ReportCurrentUsername = false;
                return this;
            }
        }

        public IAppFailConfigurationBuilder ApiToken(string apiToken)
        {
            ConfigurationModel.Instance.ApiToken = apiToken;
            return this;
        }

        public IAppFailConfigurationBuilder BaseApiUrl(string baseApiUrl)
        {
            ConfigurationModel.Instance.BaseApiUrl = baseApiUrl;
            return this;
        }

        public IAppFailConfigurationBuilder AppHarborCompatibilityMode(bool compatibilityMode = true)
        {
            ConfigurationModel.Instance.AppHarborCompatibilityMode = compatibilityMode;
            return this;
        }
    }
}
