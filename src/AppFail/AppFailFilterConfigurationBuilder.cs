using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFail
{
    internal sealed class AppFailFilterConfigurationBuilder : IAppFailFilterConfigurationBuilder
    {
        private readonly IAppFailConfigurationBuilder _appFailConfigurationBuilder;

        public AppFailFilterConfigurationBuilder(IAppFailConfigurationBuilder pBuilder)
        {
            _appFailConfigurationBuilder = pBuilder;
        }

        public IAppFailConfigurationBuilder Type(params Type[] exceptions)
        {
            ConfigurationModel.Instance.FilteredExceptionsByType.AddRange(exceptions);

            return _appFailConfigurationBuilder;
        }

        public IAppFailConfigurationBuilder Regex(params Regex[] exceptions)
        {
            ConfigurationModel.Instance.FilteredExceptionsByRegex.AddRange(exceptions);
                
            return _appFailConfigurationBuilder;
        }

        public IAppFailConfigurationBuilder Regex(params String[] exceptions)
        {
            foreach (var exception in exceptions)
            {
                try
                {
                    ConfigurationModel.Instance.FilteredExceptionsByRegex.Add(new Regex(exception));
                }
                catch (Exception)
                {
                    Debug.WriteLine("Incorrectly specified regex statement");
                }
            }

            return _appFailConfigurationBuilder;
        }

        public IAppFailConfigurationBuilder Exception(params Func<Exception, bool>[] exception)
        {
            ConfigurationModel.Instance.FilteredExceptionsByLambda.AddRange(exception);
            return _appFailConfigurationBuilder;
        }
    }
}
