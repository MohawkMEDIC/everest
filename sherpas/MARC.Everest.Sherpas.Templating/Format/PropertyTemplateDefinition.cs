using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MARC.Everest.Attributes;
using System.Reflection;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Property template container
    /// </summary>
    [XmlType("PropertyTemplateDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class PropertyTemplateDefinition : PropertyTemplateContainer
    {

        /// <summary>
        /// Gets or sets the type that this particular property should take
        /// </summary>
        [XmlElement("type")]
        public TypeDefinition Type { get; set; }

        /// <summary>
        /// Indicates the conformance of the property
        /// </summary>
        [XmlAttribute("conformance")]
        public PropertyAttribute.AttributeConformanceType Conformance { get; set; }

        /// <summary>
        /// Indicates the minimum occurance of an element
        /// </summary>
        [XmlAttribute("minOccurs")]
        public String MinOccurs { get; set; }

        /// <summary>
        /// Indicates the maximum occurance for an element
        /// </summary>
        [XmlAttribute("maxOccurs")]
        public String MaxOccurs { get; set; }

        /// <summary>
        /// Indicates that the value of this property should be drawn form the specified type
        /// </summary>
        [XmlAttribute("ref")]
        public String TemplateReference { get; set; }

        /// <summary>
        /// Identifies that a choice within the property's type must contain the specified item
        /// </summary>
        [XmlAttribute("contains")]
        public String Contains { get; set; }

        /// <summary>
        /// Gets or sets the propertyInfo related to the property that this restricts
        /// </summary>
        [XmlIgnore]
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// Get the property attribute
        /// </summary>
        public PropertyAttribute GetPropertyAttribute()
        {
            if (this.Property == null || this.TraversalName == null) return null;
            PropertyAttribute travName = null;
            foreach (PropertyAttribute pa in this.Property.GetCustomAttributes(typeof(PropertyAttribute), false))
                if (pa.Name == this.TraversalName)
                    return pa;
                else
                    travName = pa;
            return travName;
        }

        /// <summary>
        /// Merge two templates together
        /// </summary>
        internal void Merge(PropertyTemplateDefinition propertyTemplateContainer)
        {
            if(propertyTemplateContainer.Name != this.Name && propertyTemplateContainer.TraversalName != this.TraversalName)
                throw new ArgumentException("Cannot merge unless properties match in name");

            foreach (var itm in propertyTemplateContainer.Templates)
            {
                var ptc = itm as PropertyTemplateDefinition;
                if (ptc == null || String.IsNullOrEmpty(ptc.Contains)) continue;
                
                // Containment
                ptc.MinOccurs = propertyTemplateContainer.MinOccurs;
                ptc.MaxOccurs = propertyTemplateContainer.MaxOccurs;
                // Validation routines
                this.Validation.AddRange(ptc.Validation);
                this.Initialize.AddRange(ptc.Validation);
                this.Templates.Add(itm);
            }
        }


    }
}
