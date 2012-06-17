using System;
using System.Configuration;

namespace AppfailReporting.Filtering
{
    public class IgnoreNamedValueElement : ConfigurationElement
    {
        public IgnoreNamedValueElement()
        {
        }

        [ConfigurationProperty("nameContains", IsRequired = true)]
        public string NameContains
        {
            get { return this["nameContains"] as String; }
        }
    }
}
