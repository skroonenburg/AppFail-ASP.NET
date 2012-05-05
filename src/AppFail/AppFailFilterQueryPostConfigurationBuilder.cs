using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFail
{
    internal sealed class AppFailFilterQueryPostConfigurationBuilder : IAppFailFilterQueryPostConfigurationBuilder
    {
        private readonly IAppFailConfigurationBuilder _appFailConfigurationBuilder;

        public AppFailFilterQueryPostConfigurationBuilder(IAppFailConfigurationBuilder pBuilder)
        {
            _appFailConfigurationBuilder = pBuilder;
        }

        /// <summary>
        /// Filters query or post values reported to AppFail, to exclude all values with a name containing any of the given strings.
        /// </summary>
        /// <param name="exception">An array of string names that should be ignore.</param>
        /// <returns></returns>
        public IAppFailConfigurationBuilder WithNameContaining(params string[] names)
        {
            ConfigurationModel.Instance.FilteredPostNamesContaining.AddRange(names);

            return _appFailConfigurationBuilder;
        }
    }
}
