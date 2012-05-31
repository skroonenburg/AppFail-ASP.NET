using System;
using System.Configuration;

namespace AppfailReporting.Filtering
{
    public class IgnoreExceptionElement : ConfigurationElement
    {
        public IgnoreExceptionElement()
        {
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return this["type"] as String; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return this["value"] as String; }
        }
    }
}
