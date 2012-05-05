using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AppFail.Filtering
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
