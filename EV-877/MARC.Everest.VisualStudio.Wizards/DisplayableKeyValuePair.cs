using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.VisualStudio.Wizards
{
    /// <summary>
    /// Represents a key/value pair that can be displayed
    /// </summary>
    internal class DisplayableKeyValuePair<K,V>
    {

        /// <summary>
        /// Displayable key value pair
        /// </summary>
        public DisplayableKeyValuePair(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the key of the value pair
        /// </summary>
        public K Key { get; set; }
        /// <summary>
        /// Gets or sets the value of the value pair
        /// </summary>
        public V Value { get; set; }

        /// <summary>
        /// Represent this key value pair as a string
        /// </summary>
        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
