namespace AppfailReporting
{
    internal sealed class AppfailFilterServerVariableConfigurationBuilder : IAppfailFilterServerVariableConfigurationBuilder
    {
        private readonly IAppfailConfigurationBuilder _appFailConfigurationBuilder;

        public AppfailFilterServerVariableConfigurationBuilder(IAppfailConfigurationBuilder pBuilder)
        {
            _appFailConfigurationBuilder = pBuilder;
        }

        /// <summary>
        /// Filters server variable values reported to Appfail, to exclude all values with a name containing any of the given strings.
        /// </summary>
        /// <param name="exception">An array of string names that should be ignore.</param>
        /// <returns></returns>
        public IAppfailConfigurationBuilder WithNameContaining(params string[] names)
        {
            ConfigurationModel.Instance.FilteredServerVariableNamesContaining.AddRange(names);

            return _appFailConfigurationBuilder;
        }
    }
}
