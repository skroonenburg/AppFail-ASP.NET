using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFail
{
    public interface IAppFailFilterQueryPostConfigurationBuilder
    {
        /// <summary>
        /// Filters query or post values reported to AppFail, to exclude all values with a name containing any of the given strings.
        /// </summary>
        /// <param name="exception">An array of string names that should be ignore.</param>
        /// <returns></returns>
        IAppFailConfigurationBuilder WithNameContaining(params string[] names);
    }
}
