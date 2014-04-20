using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MARC.Everest.Attributes;

namespace MARC.Everest.DataTypes.Primitives
{
    /// <summary>
    /// Identifies the values that are actually carried in CV, CD, CE, etc.
    /// </summary>
    public struct CodifiedValueDefinition<T>
    {
        private CodeValue<T> m_code;

        /// <summary>
        /// Gets or sets the code of the coded value
        /// </summary>
        /// <remarks>
        /// Setting a strongly bound CV (a CV that is bound to an enumeration) with
        /// no code system set will set the <see cref="P:CodeSystem"/> property to
        /// the specified code system.
        /// <example>
        /// <code lang="cs" title="Effects of Code property on CV">
        /// <![CDATA[
        /// CV<MARC.Everest.DataTypes.NullFlavor> nullFlavor = new CV<MARC.Everest.DataTypes.NullFlavor>();
        /// nullFlavor.Code = MARC.Everest.DataTypes.NullFlavor.NoInformation;
        /// Console.WriteLine(nullFlavor.CodeSystem);
        /// // output: 2.16.840.1.113883.5.1008
        /// nullFlavor = new CV<MARC.Everest.DataTypes.NullFlavor>();
        /// nullFlavor.CodeSystem = "Hello";
        /// nullFlavor.Code = MARC.Everest.DataTypes.NullFlavor.NoInformation;
        /// Console.WriteLine(nullFlavor.CodeSystem);
        /// // output: Hello
        /// ]]>
        /// </code>
        /// </example>
        /// </remarks>
        public CodeValue<T> Code
        {
            get
            {
                return this.m_code;
            }
            set
            {
                // Set the code system if one is not set  
                if (CodeSystem == null && value != null && !value.IsAlternateCodeSpecified && typeof(T).IsEnum) // Set the code system to the value's code system
                    this.CodeSystem = GetCodeSystem(value);
                if (DisplayName == null && value != null && !value.IsAlternateCodeSpecified && typeof(T).IsEnum)
                    this.DisplayName = GetDisplayName(value);
                this.m_code = value;
            }
        }

        /// <summary>
        /// Get the display name for the enumeration
        /// </summary>
        private ST GetDisplayName(CodeValue<T> value)
        {
            if (value == null || !typeof(T).IsEnum) return null;

#if !WINDOWS_PHONE
            object[] ea = typeof(T).GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (ea.Length != 0 && !string.IsNullOrEmpty((ea[0] as DescriptionAttribute).Description))
                return (ea[0] as DescriptionAttribute).Description;
#endif
            return null;
        }

        /// <summary>
        /// Get code system for the specified value
        /// </summary>
        internal string GetCodeSystem(CodeValue<T> value)
        {

            if (value == null || !typeof(T).IsEnum) return null;

            object[] ea = typeof(T).GetField(value.ToString()).GetCustomAttributes(typeof(EnumerationAttribute), false),
                                    oa = typeof(T).GetCustomAttributes(typeof(StructureAttribute), false);

            if (ea.Length != 0 && !string.IsNullOrEmpty((ea[0] as EnumerationAttribute).SupplierDomain))
                return (ea[0] as EnumerationAttribute).SupplierDomain;
            if (String.IsNullOrEmpty(this.CodeSystem) && oa.Length != 0)
                return (oa[0] as StructureAttribute).CodeSystem;
            return null;
        }

        /// <summary>
        /// The OID representing the system from which the code was drawn
        /// </summary>
        /// <remarks>
        /// This property is set automatically when a mnemonic is drawn from 
        /// the bound enumeration (ie: if <typeparam name="T"/> is an enumeration 
        /// and the enumeration value has a <see cref="T:MARC.Everest.Attributes.EnumerationAttribute"/>
        /// associated with it then the codeSystem is set to the supplierDomain property
        /// of that attribute).
        /// </remarks>
        public string CodeSystem { get; set; }

        /// <summary>
        /// The name of the code system
        /// </summary>
        public string CodeSystemName { get; set; }

        /// <summary>
        /// The code system version
        /// </summary>
        public string CodeSystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the name or title for the code
        /// </summary>
        public ST DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the text as seen and or selected by the user who entered the data
        /// </summary>
        public ED OriginalText { get; set; }

        /// <summary>
        /// Gets or sets addtional groups of logically related qualifiers
        /// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "group", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        //[TypeConverter(typeof(ExpandableObjectConverter))]
        //[Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]
        //[XmlElement("group")]
        //public LIST<CDGroup> Group { get; set; }

        /// <summary>
        /// Gets or sets the reason the code was provided
        /// </summary>
        public SET<CodingRationale> CodingRationale { get; set; }

        /// <summary>
        /// Identifies the value set that was applicable at the time of coding
        /// </summary>
        public String ValueSet { get; set; }

        /// <summary>
        /// Identifies the version of the value set that was applicable at the time of coding
        /// </summary>
        public String ValueSetVersion { get; set; }

        /// <summary>
        /// Gets or sets a set of other concept descriptors that provide a translation of this concept descriptor in other code
        /// systems or a synonym to the code
        /// </summary>
        public SET<CD<T>> Translation { get; set; }

        /// <summary>
        /// Specifies additonal codes that increase the specificity of the primary code
        /// </summary>
        public LIST<CR<T>> Qualifier { get; set; }

    }
}
