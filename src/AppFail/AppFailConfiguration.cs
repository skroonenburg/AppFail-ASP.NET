using System.Configuration;
using AppfailReporting.Filtering;

namespace AppfailReporting
{
    public class AppfailConfiguration : ConfigurationSection
    {
        public static AppfailConfiguration Current
        {
            get
            {
                return (AppfailConfiguration)ConfigurationManager.GetSection("appfail");
            }
        }

        [ConfigurationProperty("apiToken")]
        public string ApiToken
        {
            get { return (string)this["apiToken"]; }
        }

        [ConfigurationProperty("reportingMinimumBatchSize", DefaultValue = 10)]
        public int ReportingMinimumBatchSize
        {
            get { return (int)this["reportingMinimumBatchSize"]; }
        }

        [ConfigurationProperty("reportingOccurrenceMaxSizeBytes", DefaultValue = (long)102400)] // 100KB per occurrence
        public long ReportingOccurrenceMaxSizeBytes
        {
            get { return (long)this["reportingOccurrenceMaxSizeBytes"]; }
        }

        [ConfigurationProperty("reportingMaximumIntervalMinutes", DefaultValue = 1)]
        public int ReportingMaximumIntervalMinutes
        {
            get { return (int)this["reportingMaximumIntervalMinutes"]; }
        }

        [ConfigurationProperty("baseApiUrl", DefaultValue = "https://api.appfail.net/")]
        public string BaseApiUrl
        {
            get { return (string)this["baseApiUrl"]; }
        }

        [ConfigurationProperty("reportCurrentUsername", DefaultValue = true)]
        public bool ReportCurrentUsername
        {
            get { return (bool)this["reportCurrentUsername"]; }
        }

        [ConfigurationProperty("disableInDebugMode", DefaultValue = false)]
        public bool DisableInDebugMode
        {
            get { return (bool)this["disableInDebugMode"]; }
        }

        /// <summary>
        /// An example of what could go into web.config
        ///  <appFail apiToken="0ece6d98-7769-xxx" >
        ///   <ignoreExceptions>
        ///         <add type="HttpExceptionType" value="NotImplemented" />
        ///         <add type="HttpStatusCode" value="404" />
        ///         <add type="ExceptionMessage" value="^favicon" />
        ///   </ignoreExceptions>
        /// </appFail>
        /// </summary>
        [ConfigurationProperty("ignoreExceptions", IsRequired = false, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfigurationElement))]
        public ReferencedConfigurationElementCollection<IgnoreExceptionElement> IgnoreExceptions
        {
            get { return (ReferencedConfigurationElementCollection<IgnoreExceptionElement>)this["ignoreExceptions"]; }
        }

        /// <summary>
        /// An example of what could go into web.config
        ///  <appFail apiToken="0ece6d98-7769-xxx" >
        ///   <ignorePostValues>
        ///         <add nameContains="CreditCardNumber" />
        ///         <add nameContains="CCV" />
        ///         <add nameContains="Password" />
        ///   </ignorePostValues>
        /// </appFail>
        /// </summary>
        [ConfigurationProperty("ignorePostValues", IsRequired = false, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfigurationElement))]
        public ReferencedConfigurationElementCollection<IgnoreNamedValueElement> IgnorePostValues
        {
            get { return (ReferencedConfigurationElementCollection<IgnoreNamedValueElement>)this["ignorePostValues"]; }
        }

        /// <summary>
        /// An example of what could go into web.config
        ///  <appFail apiToken="0ece6d98-7769-xxx" >
        ///   <ignoreCookies>
        ///         <add nameContains="ASPX_AUTH" />
        ///   </ignoreCookies>
        /// </appFail>
        /// </summary>
        [ConfigurationProperty("ignoreCookies", IsRequired = false, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfigurationElement))]
        public ReferencedConfigurationElementCollection<IgnoreNamedValueElement> IgnoreCookies
        {
            get { return (ReferencedConfigurationElementCollection<IgnoreNamedValueElement>)this["ignoreCookies"]; }
        }


        /// <summary>
        /// An example of what could go into web.config
        ///  <appFail apiToken="0ece6d98-7769-xxx" >
        ///   <ignoreServerVariables>
        ///         <add nameContains="AUTH_NAME" />
        ///   </ignoreCookies>
        /// </appFail>
        /// </summary>
        [ConfigurationProperty("ignoreServerVariables", IsRequired = false, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfigurationElement))]
        public ReferencedConfigurationElementCollection<IgnoreNamedValueElement> IgnoreServerVariables
        {
            get { return (ReferencedConfigurationElementCollection<IgnoreNamedValueElement>)this["ignoreServerVariables"]; }
        }
    }
}