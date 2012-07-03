using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;

namespace AppfailReporting
{
    internal sealed class AppfailFilterConfigurationBuilder : IAppfailFilterConfigurationBuilder
    {
        private readonly IAppfailConfigurationBuilder _appFailConfigurationBuilder;

        public AppfailFilterConfigurationBuilder(IAppfailConfigurationBuilder pBuilder)
        {
            _appFailConfigurationBuilder = pBuilder;
        }

        public IAppfailConfigurationBuilder WithTypes(params Type[] exceptions)
        {
            ConfigurationModel.Instance.FilteredExceptionsByType.AddRange(exceptions);

            return _appFailConfigurationBuilder;
        }

        public IAppfailConfigurationBuilder WithMessagesMatchingRegex(params Regex[] exceptions)
        {
            ConfigurationModel.Instance.FilteredExceptionsByRegex.AddRange(exceptions);
                
            return _appFailConfigurationBuilder;
        }

        public IAppfailConfigurationBuilder WithRelativeUrls(params string[] urlsContaining)
        {
            ConfigurationModel.Instance.FilteredExceptionByRelativeUrls.AddRange(urlsContaining.Select(x => x.ToUpperInvariant()));

            return _appFailConfigurationBuilder;
        }

        public IAppfailConfigurationBuilder WithRelativeUrlsContaining(params string[] urlsContaining)
        {
            ConfigurationModel.Instance.FilteredExceptionByRelativeUrlsContaining.AddRange(urlsContaining.Select(x => x.ToUpperInvariant()));

            return _appFailConfigurationBuilder;
        }

        public IAppfailConfigurationBuilder WithRelativeUrlsStartingWith(params string[] urlsStartingWith)
        {
            ConfigurationModel.Instance.FilteredExceptionByRelativeUrlsStartingWith.AddRange(urlsStartingWith.Select(x => x.ToUpperInvariant()));

            return _appFailConfigurationBuilder;
        }

        public IAppfailConfigurationBuilder WithMessagesMatchingRegex(params String[] exceptions)
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

        public IAppfailConfigurationBuilder Where(params Func<Exception, bool>[] exceptionFilterFunctions)
        {
            ConfigurationModel.Instance.FilteredExceptionsByLambda.AddRange(exceptionFilterFunctions);
            return _appFailConfigurationBuilder;
        }

        public IAppfailConfigurationBuilder WithHttpStatusCodes(params HttpStatusCode[] httpStatusCode)
        {
            ConfigurationModel.Instance.FilteredExceptionsByHttpStatusCode.AddRange(httpStatusCode);
            return _appFailConfigurationBuilder;
        }
    }
}
