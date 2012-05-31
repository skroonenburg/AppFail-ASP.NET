using System;
using System.Configuration;

namespace AppfailReporting.Filtering
{
    public class IgnorePostValueElement : ConfigurationElement
    {
        public IgnorePostValueElement()
        {
        }

        [ConfigurationProperty("nameContains", IsRequired = true)]
        public string NameContains
        {
            get { return this["nameContains"] as String; }
        }
    }
}
