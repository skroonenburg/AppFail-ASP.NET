using System;

namespace AppfailReporting
{
    public interface IAppfailConfigurationBuilder
    {
        /// <summary>
        /// Sets the minimum batch size for reporting fails. Appfail will report fails in batches matching this size.
        /// If enough fails are not encountered within the maximum interval, then the fails will be reported anyway.
        /// </summary>
        /// <param name="minimumBatchSize">The minimum number of fails to report in a single batch. Must be 1 or more.</param>
        IAppfailConfigurationBuilder ReportingMinimumBatchSize(int minimumBatchSize);

        /// <summary>
        /// Sets the maximum allowed size (in bytes) for a single fail occurrence. Appfail will reject large submissions, and
        /// this setting tells the ASP.NET module the limit, so that it doesn't use unnecessary bandwidth.
        /// </summary>
        /// <param name="minimumBatchSize">The maximum allowed reporting size for a single fail occurrence, in bytes.</param>
        IAppfailConfigurationBuilder ReportingOccurrenceMaxSizeBytes(long maxSizeBytes);

        /// <summary>
        /// Sets the maximum time interval that can pass between reporting fails.
        /// If enough fails have been encountered within this interval, to satisfy the minimum batch size then the fails will be reported anyway.
        /// </summary>
        /// <param name="maximumInterval">The maximum time interval between fail reports to Appfail.</param>
        IAppfailConfigurationBuilder ReportingMaxmimumInterval(TimeSpan maximumInterval);

        /// <summary>
        /// A callback that Appfail will invoke on each fail occurrence to populate the username of the current user.
        /// </summary>
        /// <param name="populateUserCallback">A callback that will return the username of the current user, to be reported in the relevant fail report.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder PopulateUsernameWith(Func<string> populateUserCallback);

        /// <summary>
        /// Instructs Appfail not to report the username of the current user in fail reports.
        /// </summary>
        IAppfailConfigurationBuilder DoNotReportUsername { get; }

        /// <summary>
        /// Instructs Appfail not to report the failures when the application is running in debug mode.
        /// </summary>
        IAppfailConfigurationBuilder DisableInDebugMode { get; }

        /// <summary>
        /// Sets the API token for this application, as provided in the Appfail portal.
        /// </summary>
        /// <param name="apiToken">The API token used to authenticate with Appfail.</param>
        IAppfailConfigurationBuilder ApiToken(string apiToken);

        /// <summary>
        /// Sets the base url for Appfail's API.
        /// </summary>
        /// <param name="baseApiUrl">The base URL for Appfail's API.</param>
        IAppfailConfigurationBuilder BaseApiUrl(string baseApiUrl);

        /// <summary>
        /// Do not report exceptions matching the following criteria
        /// </summary>
        IAppfailFilterConfigurationBuilder IgnoreExceptions { get; }

        /// <summary>
        /// Do not report form POST values matching the following criteria
        /// </summary>
        IAppfailFilterQueryPostConfigurationBuilder IgnorePostValues { get; }

        IAppfailFilterCookieConfigurationBuilder IgnoreCookies { get; }
        IAppfailFilterServerVariableConfigurationBuilder IgnoreServerVariables { get; }
    }
}
