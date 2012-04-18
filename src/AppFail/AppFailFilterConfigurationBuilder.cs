using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

        public IAppFailConfigurationBuilder WithTypes(params Type[] exceptions)
        {
            ConfigurationModel.Instance.FilteredExceptionsByType.AddRange(exceptions);

            return _appFailConfigurationBuilder;
        }

        public IAppFailConfigurationBuilder WithMessagesMatchingRegex(params Regex[] exceptions)
        {
            ConfigurationModel.Instance.FilteredExceptionsByRegex.AddRange(exceptions);
                
            return _appFailConfigurationBuilder;
        }

        public IAppFailConfigurationBuilder WithRelativeUrlsContaining(params string[] urlsContaining)
        {
            ConfigurationModel.Instance.FilteredExceptionByRelativeUrlsContaining.AddRange(urlsContaining);

            return _appFailConfigurationBuilder;
        }

        public IAppFailConfigurationBuilder WithRelativeUrlsStartingWith(params string[] urlsStartingWith)
        {
            ConfigurationModel.Instance.FilteredExceptionByRelativeUrlsStartingWith.AddRange(urlsStartingWith);

            return _appFailConfigurationBuilder;
        }

        public IAppFailConfigurationBuilder WithMessagesMatchingRegex(params String[] exceptions)
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

        public IAppFailConfigurationBuilder Where(params Func<Exception, bool>[] exceptionFilterFunctions)
        {
            ConfigurationModel.Instance.FilteredExceptionsByLambda.AddRange(exceptionFilterFunctions);
            return _appFailConfigurationBuilder;
        }

        public IAppFailConfigurationBuilder WithHttpStatusCodes(params HttpStatusCode[] httpStatusCode)
        {
            ConfigurationModel.Instance.FilteredExceptionsByHttpStatusCode.AddRange(httpStatusCode);
            return _appFailConfigurationBuilder;
        }
    }
}
