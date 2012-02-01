using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFail
{
    public interface IAppFailConfigurationBuilder
    {
        /// <summary>
        /// Sets the minimum batch size for reporting fails. AppFail will report fails in batches matching this size.
        /// If enough fails are not encountered within the maximum interval, then the fails will be reported anyway.
        /// </summary>
        /// <param name="minimumBatchSize">The minimum number of fails to report in a single batch. Must be 1 or more.</param>
        IAppFailConfigurationBuilder ReportingMinimumBatchSize(int minimumBatchSize);

        /// <summary>
        /// Sets the maximum time interval that can pass between reporting fails.
        /// If enough fails have been encountered within this interval, to satisfy the minimum batch size then the fails will be reported anyway.
        /// </summary>
        /// <param name="maximumInterval">The maximum time interval between fail reports to AppFail.</param>
        IAppFailConfigurationBuilder ReportingMaxmimumInterval(TimeSpan maximumInterval);

        /// <summary>
        /// A callback that AppFail will invoke on each fail occurrence to populate the username of the current user.
        /// </summary>
        /// <param name="populateUserCallback">A callback that will return the username of the current user, to be reported in the relevant fail report.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder PopulateUsernameWith(Func<string> populateUserCallback);

        /// <summary>
        /// Instructs AppFail not to report the username of the current user in fail reports.
        /// </summary>
        IAppFailConfigurationBuilder DoNotReportUsername { get; }

        /// <summary>
        /// Sets the API token for this application, as provided in the AppFail portal.
        /// </summary>
        /// <param name="apiToken">The API token used to authenticate with AppFail.</param>
        IAppFailConfigurationBuilder ApiToken(string apiToken);

        /// <summary>
        /// Sets the base url for AppFail's API.
        /// </summary>
        /// <param name="baseApiUrl">The base URL for AppFail's API.</param>
        IAppFailConfigurationBuilder BaseApiUrl(string baseApiUrl);
    }
}
