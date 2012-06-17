namespace AppfailReporting
{
    internal sealed class AppfailFilterCookieConfigurationBuilder : IAppfailFilterCookieConfigurationBuilder
    {
        private readonly IAppfailConfigurationBuilder _appFailConfigurationBuilder;

        public AppfailFilterCookieConfigurationBuilder(IAppfailConfigurationBuilder pBuilder)
        {
            _appFailConfigurationBuilder = pBuilder;
        }

        /// <summary>
        /// Filters cookie values reported to Appfail, to exclude all values with a name containing any of the given strings.
        /// </summary>
        /// <param name="exception">An array of string names that should be ignore.</param>
        /// <returns></returns>
        public IAppfailConfigurationBuilder WithNameContaining(params string[] names)
        {
            ConfigurationModel.Instance.FilteredCookieNamesContaining.AddRange(names);

            return _appFailConfigurationBuilder;
        }
    }
}
