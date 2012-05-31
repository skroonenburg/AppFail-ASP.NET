namespace AppfailReporting
{
    internal sealed class AppfailFilterQueryPostConfigurationBuilder : IAppfailFilterQueryPostConfigurationBuilder
    {
        private readonly IAppfailConfigurationBuilder _appFailConfigurationBuilder;

        public AppfailFilterQueryPostConfigurationBuilder(IAppfailConfigurationBuilder pBuilder)
        {
            _appFailConfigurationBuilder = pBuilder;
        }

        /// <summary>
        /// Filters query or post values reported to Appfail, to exclude all values with a name containing any of the given strings.
        /// </summary>
        /// <param name="exception">An array of string names that should be ignore.</param>
        /// <returns></returns>
        public IAppfailConfigurationBuilder WithNameContaining(params string[] names)
        {
            ConfigurationModel.Instance.FilteredPostNamesContaining.AddRange(names);

            return _appFailConfigurationBuilder;
        }
    }
}
