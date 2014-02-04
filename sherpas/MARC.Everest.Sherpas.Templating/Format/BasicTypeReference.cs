using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Sherpas.Formatter.XML.ITS1;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a reference to a complete concept domain
    /// </summary>
    [XmlType("BasicTypeReference", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class BasicTypeReference
    {
        /// <summary>
        /// Formatter
        /// </summary>
        private static ClinicalDocumentFormatter s_formatter = new ClinicalDocumentFormatter();
        
        /// <summary>
        /// Gets or sets the name of the referenced system
        /// </summary>
        [XmlAttribute("name")]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the flavor 
        /// </summary>
        [XmlAttribute("flavor")]
        public String Flavor { get; set; }

        /// <summary>
        /// Gets or sets the hard linked type
        /// </summary>
        [XmlIgnore]
        public Type Type
        {
            get
            {
                if (this.Name == null || this.Name == "IGraphable")
                    return typeof(IGraphable);

                Type t = typeof(object);
                try
                {
                    t = Util.ParseXSITypeName(this.Name);
                }
                catch { }
                if (t == typeof(object))
                    t = s_formatter.ParseXSITypeName(this.Name);
                return t;
            }
            set
            {
                if (value == typeof(IGraphable))
                    this.Name = "IGraphable";
                else if (typeof(ANY).IsAssignableFrom(value))
                    this.Name = Util.CreateXSITypeName(value);
                else
                    this.Name = s_formatter.CreateXSITypeName(value);
            }
        }
    }
}
