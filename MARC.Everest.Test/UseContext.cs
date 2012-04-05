using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Attributes;
using System.Reflection;

namespace MARC.Everest.Test
{
    /// <summary>
    /// Storage class for context for object generation.
    /// </summary>
    internal class UseContext
    {
        /// <summary>
        /// Gets or sets the PropertyAttribute if present for the current context..
        /// </summary>
        public PropertyAttribute PropertyAttribute { get; set; }
        /// <summary>
        /// Gets or sets the PropertyAttributes of the current context.
        /// </summary>
        public List<PropertyAttribute> PropertyAttributes { get; private set; }
        /// <summary>
        /// Gets or sets the PropertyInfo object associated with the current context.
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }
        /// <summary>
        /// Gets or sets the ParameterInfo object associated with the current context.
        /// </summary>
        public ParameterInfo ParameterInfo { get; set; }
        /// <summary>
        /// Gets or sets the Owning Type of object associated with the current context.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Type OwnerType { get; set; }
        /// <summary>
        /// Gets or sets the parent context of this context if one exists.
        /// </summary>
        public UseContext ParentContext { get; set; }

        /// <summary>
        /// Gets or sets the level of indentation that is present for debug messages.
        /// </summary>
        public string Indent
        {
            get
            {
                if (null != ParentContext)
                    return ParentContext.Indent + "  ";
                return string.Empty;
            }
        }

        /// <summary>
        /// Creates a new instance of the UseContext class specifying the owning type, property attribute, and property info.
        /// </summary>
        /// <param name="ownerType">The Owning Type of object associated with the current context.</param>
        /// <param name="propertyAttribute">The PropertyAttribute if present for the current context.</param>
        /// <param name="propertyInfo">The PropertyInfo object associated with the current context.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public UseContext(Type ownerType, PropertyAttribute propertyAttribute, PropertyInfo propertyInfo)
        {
            OwnerType = ownerType;
            PropertyInfo = propertyInfo;
            PropertyAttribute = propertyAttribute;
            PropertyAttributes = new List<PropertyAttribute>();
        }
        /// <summary>
        /// Creates a new instance of the UseContext class specifying the owning type, property attribute, and parameter info.
        /// </summary>
        /// <param name="ownerType">The Owning Type of object associated with the current context.</param>
        /// <param name="propertyAttribute">The PropertyAttribute if present for the current context.</param>
        /// <param name="parameterInfo">The ParameterInfo object associated with the current context.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public UseContext(Type ownerType, PropertyAttribute propertyAttribute, ParameterInfo parameterInfo)
        {
            OwnerType = ownerType;
            ParameterInfo = parameterInfo;
            PropertyAttribute = propertyAttribute;
            PropertyAttributes = new List<PropertyAttribute>();
        }
        /// <summary>
        /// Creates a new instance of the UseContext class specifying the owning type, and property info. A search for a PropertyAttribute will automatically be performed.
        /// </summary>
        /// <param name="ownerType">The Owning Type of object associated with the current context.</param>
        /// <param name="propertyInfo">The PropertyInfo object associated with the current context.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public UseContext(Type ownerType, PropertyInfo propertyInfo)
        {
            OwnerType = ownerType;
            PropertyInfo = propertyInfo;
            PropertyAttributes = new List<PropertyAttribute>();

            //Search for a PropertyAttribute on the property.
            if (null != propertyInfo)
                foreach (var attr in propertyInfo.GetCustomAttributes(typeof(MARC.Everest.Attributes.PropertyAttribute), false))
                {
                    PropertyAttributes.Add(attr as MARC.Everest.Attributes.PropertyAttribute);
                    PropertyAttribute = attr as MARC.Everest.Attributes.PropertyAttribute;
                }
        }
        /// <summary>
        /// Creates a new instance of the UseContext class specifying the owning type, and parameter info. A search for a PropertyAttribute will automatically be performed.
        /// </summary>
        /// <param name="ownerType">The Owning Type of object associated with the current context.</param>
        /// <param name="parameterInfo">The ParameterInfo object associated with the current context.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public UseContext(Type ownerType, ParameterInfo parameterInfo)
        {
            OwnerType = ownerType;
            ParameterInfo = parameterInfo;
            PropertyAttributes = new List<PropertyAttribute>();

            //Search for a PropertyAttribute on the parameter.
            if (null != parameterInfo)
                foreach (var attr in parameterInfo.GetCustomAttributes(typeof(MARC.Everest.Attributes.PropertyAttribute), false))
                {
                    PropertyAttributes.Add(attr as MARC.Everest.Attributes.PropertyAttribute);
                    PropertyAttribute = attr as MARC.Everest.Attributes.PropertyAttribute;
                }
        }
    }
}
