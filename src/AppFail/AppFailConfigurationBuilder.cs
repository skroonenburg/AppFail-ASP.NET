using System;

namespace AppfailReporting
{
    internal sealed class AppfailConfigurationBuilder : IAppfailConfigurationBuilder
    {
        private readonly IAppfailFilterConfigurationBuilder _appFailFilterConfigurationBuilder;
        private readonly IAppfailFilterQueryPostConfigurationBuilder _appFailFilterQueryPostConfigurationBuilder;
        private readonly IAppfailFilterCookieConfigurationBuilder _appFailFilterCookiesConfigurationBuilder;
        private readonly IAppfailFilterServerVariableConfigurationBuilder _appFailFilterServerVariablesConfigurationBuilder;

        public AppfailConfigurationBuilder()
        {
            _appFailFilterConfigurationBuilder = new AppfailFilterConfigurationBuilder(this);
            _appFailFilterQueryPostConfigurationBuilder = new AppfailFilterQueryPostConfigurationBuilder(this);
            _appFailFilterCookiesConfigurationBuilder = new AppfailFilterCookieConfigurationBuilder(this);
            _appFailFilterServerVariablesConfigurationBuilder = new AppfailFilterServerVariableConfigurationBuilder(this);
        }

        public IAppfailConfigurationBuilder ReportingMinimumBatchSize(int minimumBatchSize)
        {
            ConfigurationModel.Instance.ReportingMinimumBatchSize = minimumBatchSize;
            return this;
        }

        public IAppfailConfigurationBuilder ReportingMaxmimumInterval(TimeSpan maximumInterval)
        {
            ConfigurationModel.Instance.ReportingMaximumInterval = maximumInterval;
            return this;
        }

        public IAppfailConfigurationBuilder PopulateUsernameWith(Func<string> populateUserCallback)
        {
            ConfigurationModel.Instance.PopulateUsernameFrom = populateUserCallback;
            return this;
        }

        public IAppfailConfigurationBuilder ReportingOccurrenceMaxSizeBytes(long maxSizeBytes)
        {
            ConfigurationModel.Instance.ReportingOccurrenceMaxSizeBytes = maxSizeBytes;

            return this;
        }

        public IAppfailConfigurationBuilder DoNotReportUsername
        {
            get
            {
                ConfigurationModel.Instance.ReportCurrentUsername = false;
                return this;
            }
        }

        public IAppfailConfigurationBuilder DisableInDebugMode
        {
            get
            {
                ConfigurationModel.Instance.DisableInDebugMode = true;

                return this;
            }
        }

        public IAppfailConfigurationBuilder ApiToken(string apiToken)
        {
            ConfigurationModel.Instance.ApiToken = apiToken;
            return this;
        }

        public IAppfailConfigurationBuilder BaseApiUrl(string baseApiUrl)
        {
            ConfigurationModel.Instance.BaseApiUrl = baseApiUrl;
            return this;
        }

        public IAppfailFilterConfigurationBuilder IgnoreExceptions
        {
            get { return _appFailFilterConfigurationBuilder; }
        }

        public IAppfailFilterQueryPostConfigurationBuilder IgnorePostValues
        {
            get { return _appFailFilterQueryPostConfigurationBuilder; }
        }

        public IAppfailFilterCookieConfigurationBuilder IgnoreCookies
        {
            get { return _appFailFilterCookiesConfigurationBuilder; }
        }

        public IAppfailFilterServerVariableConfigurationBuilder IgnoreServerVariables
        {
            get { return _appFailFilterServerVariablesConfigurationBuilder; }
        }
    }
}
