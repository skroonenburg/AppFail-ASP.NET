namespace AppfailReporting
{
    public interface IAppfailFilterServerVariableConfigurationBuilder
    {
        /// <summary>
        /// Filters server variable values reported to Appfail, to exclude all values with a name containing any of the given strings.
        /// </summary>
        /// <param name="exception">An array of string names that should be ignore.</param>
        /// <returns></returns>
        IAppfailConfigurationBuilder WithNameContaining(params string[] names);
    }
}