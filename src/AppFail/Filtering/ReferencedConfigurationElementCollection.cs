using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AppFail.Filtering
{
    public class ReferencedConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T> where T : ConfigurationElement, new()
    {
        private readonly List<T> _elements = new List<T>();

        protected override ConfigurationElement CreateNewElement()
        {
            var newElement = new T();
            _elements.Add(newElement);
            return newElement;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return _elements.Find(m => m.Equals(element));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
