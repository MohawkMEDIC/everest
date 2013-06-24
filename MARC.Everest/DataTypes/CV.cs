/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.

 * 
 * User: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using System.ComponentModel;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Primitives;

// WP7 Imports
#if !WINDOWS_PHONE
using System.Drawing.Design;
using MARC.Everest.Design;
#endif

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// The coded value data type is an example of a flavor that has been mapped into a full class in order to remain compatible with R1 data types. In R2 the CV data type is actually the CD (Concept Descriptor) data type with a flavor of CV.
    /// CV extends the CS data type by adding display name, coding rationale, and original text.
    /// Original
    /// </summary>
    /// string burnSkinCode = "284106006";
    /// string codeSystem = "2.16.840.1.113883.6.96";
    /// string codeSystemName = "SNOMED CT";
    /// string codeSystemVersion = "V3-2008N";
    /// string displayName = "Burn of skin";
    /// string originalText = "Burn between third and fourth toe";
    //[Serializable]
    //[XmlType("CV", Namespace = "urn:hl7-org:v3")]
    //[Obsolete("CV is obsoletee, consider using CV<String>", true)]
    //public class CV : CV<string>
    //{

    //    /// <summary>
    //    /// CV extends the CS data type by adding display name, coding rationale, and original text.
    //    /// Create a new instance of CV
    //    /// </summary>
    //    /// <example>
    //    /// <code lang="cs">
    //    /// <![CDATA[
    //    /// 
    //    /// CV burnSkin = new CV();
    //    /// 
    //    /// // One possible Coding Rationale Constructor
    //    /// SET<CodingRationale> cr00 = new SET<CodingRationale>(CodingRationale.Required,
    //    ///        (a, b) => ((CodingRationale)a).CompareTo(((CodingRationale)b)));
    //    ///
    //    /// // Another possible Coding Rationale Constructor ( this is the one we use when builing our example object )
    //    /// SET<CodingRationale> cr01 = new SET<CodingRationale>(
    //    ///                              new CodingRationale[]{
    //    ///                                            CodingRationale.Required,
    //    ///                                            CodingRationale.PostCoding
    //    ///                                                    },
    //    ///                                (a, b) => ((CodingRationale)a).CompareTo(((CodingRationale)b)));
    //    ///    
    //    ///    // setting the properties
    //    ///    burnSkin.Code = "284106006";
    //    ///    burnSkin.CodeSystem = "2.16.840.1.113883.6.96";
    //    ///    burnSkin.CodeSystemName = "SNOMED CT";
    //    ///    burnSkin.CodeSystemVersion = "V3-2008N";
    //    ///    burnSkin.DisplayName = "Burn of Skin";
    //    ///    burnSkin.OriginalText = "Burn between third and fourth toe";
    //    ///    burnSkin.CodingRationale = cr01;
    //    /// ]]>
    //    /// </code>
    //    /// </example>
    //    public CV() { }
    //    /// <summary>
    //    /// Create a new instance of CV with the specified code
    //    /// </summary>
    //    /// <param name="code">The initial code</param>
    //    public CV(string code) : base(code) { }
    //    /// <summary>
    //    /// Create a new instance of CV with the specified code and code system
    //    /// </summary>
    //    /// <param name="code">The initial code of the CS</param>
    //    /// <param name="codeSystem">The code system the code was picked from</param>
    //    public CV(string code, string codeSystem) : base(code, codeSystem) { }
    //    /// <summary>
    //    /// Create a new instance of CV with the specified parameters
    //    /// </summary>
    //    /// <param name="code">The initial code</param>
    //    /// <param name="codeSystem">The code system the code was picked from</param>
    //    /// <param name="codeSystemName">The name of the code system</param>
    //    /// <param name="codeSystemVersion">The version of the code system</param>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1025:ReplaceRepetitiveArgumentsWithParamsArray")]
    //    public CV(string code, string codeSystem, string codeSystemName, string codeSystemVersion) : base(code, codeSystem, codeSystemName, codeSystemVersion) { }
    //    /// <summary>
    //    /// Create a new instance of the CV data type with the parameters specified
    //    /// </summary>
    //    /// <param name="code">The initial code</param>
    //    /// <param name="codeSystem">The code system the code was picked from</param>
    //    /// <param name="codeSystemName">The name of the code system</param>
    //    /// <param name="codeSystemVersion">The version of the code system</param>
    //    /// <param name="displayName">The display name for the code</param>
    //    /// <param name="originalText">The original text, the reason the code was selected</param>
    //    public CV(string code, string codeSystem, string codeSystemName, string codeSystemVersion, string displayName, string originalText) : base(code, codeSystem, codeSystemName, codeSystemVersion, displayName, originalText) { }

    //    /// <summary>
    //    /// Converts a <see cref="string"/> to a <see cref="CV"/>
    //    /// </summary>
    //    /// <param name="s">string to convert</param>
    //    /// <returns>Converted CV</returns>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
    //    public static implicit operator CV(string s)
    //    {
    //        CV retVal = new CV();
    //        retVal.Code = s;
    //        return retVal;
    //    }

    //    /// <summary>
    //    /// Converts a <see cref="CV"/> to a <see cref="string"/>
    //    /// </summary>
    //    /// <param name="cs">CV to convert</param>
    //    /// <returns>Converted string</returns>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
    //    public static implicit operator string(CV cs)
    //    {
    //        return cs.Code;
    //    }


        
    //}

    /// <summary>
    /// Represents codified data whereby the codified value and code system are unknown
    /// </summary>
    /// <remarks>
    /// The coded value data type is an example of a flavor that has been mapped into a full class in order to remain compatible with R1 data types. In R2 the CV data type is actually the CD (Concept Descriptor) data type with a flavor of CV.
    /// CV extends the CS data type by adding display name, coding rationale, and original text.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
    [Structure(Name = "CV", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType=typeof(String))]
    #if !WINDOWS_PHONE
    [Serializable]
    #endif
    public class CV<T> : CS<T>, ICodedValue, IEquatable<CV<T>>
    {

        
        /// <summary>
        /// Create a new instance of CV
        /// </summary>
        public CV() : base () { }

        /// <summary>
        /// Create a new instance of CV with the specified code
        /// </summary>
        /// <param name="code">The initial code</param>
        public CV(T code) : base() { this.Code = code; }
    
        /// <summary>
        /// Create a new instance of CV with the specified code and code system
        /// </summary>
        /// <param name="code">The initial code of the CS</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        public CV(T code, string codeSystem) : this(code) { this.CodeSystem = codeSystem; }

        /// <summary>
        /// Create a new instance of CV with the specified parameters
        /// </summary>
        /// <param name="code">The initial code</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="codeSystemName">The name of the code system</param>
        /// <param name="codeSystemVersion">The version of the code system</param>
        public CV(T code, string codeSystem, string codeSystemName, string codeSystemVersion)
            : this(code, codeSystem)
        {
            this.CodeSystemName = codeSystemName;
            this.CodeSystemVersion = codeSystemVersion;
        }

        /// <summary>
        /// Create a new instance of the CV data type with the parameters specified
        /// </summary>
        /// <param name="code">The initial code</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="codeSystemName">The name of the code system</param>
        /// <param name="codeSystemVersion">The version of the code system</param>
        /// <param name="displayName">The display name for the code</param>
        /// <param name="originalText">The original text, the reason the code was selected</param>
        public CV(T code, string codeSystem, string codeSystemName, string codeSystemVersion, ST displayName, ED originalText)
            : this(code, codeSystem, codeSystemName, codeSystemVersion)
        {
            this.DisplayName = displayName;
            this.OriginalText = originalText;
        }

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
        [Property(Name = "code", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public override CodeValue<T> Code
        {
            get
            {
                return base.Code;
            }
            set
            {
                // Set the code system if one is not set  
                if (CodeSystem == null && value != null && !value.IsAlternateCodeSpecified && typeof(T).IsEnum) // Set the code system to the value's code system
                    this.CodeSystem = GetCodeSystem(value);
                if (DisplayName == null && value != null && !value.IsAlternateCodeSpecified && typeof(T).IsEnum)
                    this.DisplayName = GetDisplayName(value);
                base.Code = value;
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
        private string GetCodeSystem(CodeValue<T> value)
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
        [Property(Name = "codeSystem", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("codeSystem")]
        public string CodeSystem { get; set; }

        /// <summary>
        /// The name of the code system
        /// </summary>
        [Property(Name = "codeSystemName", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("codeSystemName")]
        public string CodeSystemName { get; set; }

        /// <summary>
        /// The code system version
        /// </summary>
        [Property(Name = "codeSystemVersion", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("codeSystemVersion")]
        public string CodeSystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the name or title for the code
        /// </summary>
        [Property(Name = "displayName", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("displayName")]
        public ST DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the text as seen and or selected by the user who entered the data
        /// </summary>
        [Property(Name = "originalText", ImposeFlavorId = "Text", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("originalText")]
        #if !WINDOWS_PHONE
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]
        #endif
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Property(Name = "codingRationale", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("codingRationale")]
        #if !WINDOWS_PHONE
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NewInstanceTypeEditor), typeof(UITypeEditor))]
        #endif
        public SET<CodingRationale> CodingRationale { get; set; }

        /// <summary>
        /// Identifies the value set that was applicable at the time of coding
        /// </summary>
        [Property(Name = "valueSet", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("valueSet")]
        public String ValueSet { get; set; }

        /// <summary>
        /// Identifies the version of the value set that was applicable at the time of coding
        /// </summary>
        [Property(Name = "valueSetVersion", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("valueSetVersion")]
        public String ValueSetVersion { get; set; }

        /// <summary>
        /// Validate the code is in the proper format
        /// </summary>
        /// <remarks>
        /// A code is valid when
        /// <list type="bullet">
        ///     <item>NullFlavor is specified, XOR</item>
        ///     <item><list type="bullet">
        ///         <item>Code, CodeSystem, DisplayName or CodeSystemName is populated, AND</item>
        ///         <item>Code is Specified, AND</item>
        ///         <item>If CodeSystemName is specified, CodeSystem is Specified, AND</item>
        ///         <item>If CodeSystemVersion is specified, CodeSystem is specified, AND</item>
        ///         <item>If CodeSystem is specified, Code is specified, AND</item>
        ///         <item>If DisplayName is specified, Code is specified</item>
        ///     </list></item>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            bool isValid = true;

            // If null flavor is other or null flavor not specified, validate content
            if (NullFlavor == null || ((DataTypes.NullFlavor)NullFlavor).IsChildConcept(DataTypes.NullFlavor.Other)
                )
                isValid &= (
                    ((Code != null) ^ (NullFlavor != null)) && 
                    ((CodeSystemName != null && CodeSystem != null) || (CodeSystemName == null)) &&
                    ((CodeSystemVersion != null && CodeSystem != null) || (CodeSystemVersion == null)) &&
                    ((CodeSystem != null && (Code != null || (NullFlavor)NullFlavor == DataTypes.NullFlavor.Other)) || (CodeSystem == null)) && // Code System cannot be specified without a code, unless a nullFlavor of other is specified
                    ((DisplayName != null && Code != null) || (DisplayName == null)) &&
                    ((Code != null && Code.IsAlternateCodeSpecified && CodeSystem != null) || (Code == null || !Code.IsAlternateCodeSpecified)) &&
                    ((ValueSetVersion != null && ValueSet != null) || (ValueSetVersion == null)) &&
                    ((NullFlavor != null && ((DataTypes.NullFlavor)NullFlavor).IsChildConcept(DataTypes.NullFlavor.Other)) || (NullFlavor == null)));
            else // Null flavor is not null
                isValid &= Code == null && DisplayName == null && CodeSystem == null && CodeSystemName == null && CodeSystemVersion == null &&
                    OriginalText == null && ValueSet == null && ValueSetVersion == null;
            
            return isValid;
            
        }

        /// <summary>
        /// Validate the data type and return the validation errors that have been detected in the validation
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());

            if (this.NullFlavor == null || ((DataTypes.NullFlavor)NullFlavor).IsChildConcept(DataTypes.NullFlavor.Other))
            { 
                if(Code != null &&NullFlavor != null )
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
                if(CodeSystemName != null && CodeSystem == null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "CodeSystemName", "CodeSystem"), null));
                if(CodeSystemVersion != null && CodeSystem == null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "CodeSystemVersion", "CodeSystem"), null));
                if(CodeSystem != null && (Code == null || NullFlavor != null && ((NullFlavor)NullFlavor).IsChildConcept(DataTypes.NullFlavor.Other)))
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "CodeSystem can only be used when Code is populated or NullFlavor does implies Other", null));
                if(DisplayName != null && Code == null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "DisplayName", "CodeSystem"), null));
                if(Code != null && Code.IsAlternateCodeSpecified && CodeSystem == null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "When Code has an alternate code specified, CodeSystem must be populated", null));
                if(ValueSetVersion != null && ValueSet == null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "ValueSetVersion", "ValueSet"), null));
            }
            else // NullfLavor is something other than OTHER
            { 
                if(Code != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "Code may only be specified when NullFlavor is not populated, or is populated with a NullFlavor that implies 'Other'", null));
                if(CodeSystem != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "CodeSystem may only be specified when NullFlavor is not populated, or is populated with a NullFlavor that implies 'Other'", null));
                if(CodeSystemVersion != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "CodeSystemVersion may only be specified when NullFlavor is not populated, or is populated with a NullFlavor that implies 'Other'", null));
                if(CodeSystemName != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "CodeSystemName may only be specified when NullFlavor is not populated, or is populated with a NullFlavor that implies 'Other'", null));
                if(OriginalText != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "OriginalText may only be specified when NullFlavor is not populated, or is populated with a NullFlavor that implies 'Other'", null));
                if(ValueSet != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "ValueSet may only be specified when NullFlavor is not populated, or is populated with a NullFlavor that implies 'Other'", null));
                if(ValueSetVersion != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CV", "ValueSetVersion may only be specified when NullFlavor is not populated, or is populated with a NullFlavor that implies 'Other'", null));

            }
            return retVal ;

        }

        #region Operators
        /// <summary>
        /// Converts a <see cref="CV"/> to a <typeparamref name="T"/>
        /// </summary>
        /// <param name="o">CV`1 to convert</param>
        /// <returns>Converted T</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator T(CV<T> o)
        {
            return o.Code;
        }

        /// <summary>
        /// Converts a <typeparamref name="T"/> to a <see cref="CV"/>
        /// </summary>
        /// <param name="o">T to convert</param>
        /// <returns>Converted CV`1</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator CV<T>(T o)
        {
            CV<T> retVal = new CV<T>();
            retVal.Code = o;
            return retVal;
        }

        /// <summary>
        /// Converts a CV to a strongly typed CV
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        internal static CV<T> Parse(CV<String> cv)
        {
            CV<T> retVal = new CV<T>();
            // Parse from wire format into this format
            retVal.Code = CodeValue<T>.Parse(cv.Code);
            // Code System
            retVal.CodeSystem = cv.CodeSystem;
            retVal.CodeSystemName = cv.CodeSystemName;
            retVal.CodeSystemVersion = cv.CodeSystemVersion;
            retVal.ControlActExt = cv.ControlActExt;
            retVal.ControlActRoot = cv.ControlActRoot;
            retVal.OriginalText = cv.OriginalText == null ? null : cv.OriginalText.Clone() as ED;
            retVal.UpdateMode = cv.UpdateMode == null ? null : cv.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.NullFlavor = cv.NullFlavor == null ? null : cv.NullFlavor.Clone() as CS<NullFlavor>;
            retVal.ValidTimeHigh = cv.ValidTimeHigh;
            retVal.ValidTimeLow = cv.ValidTimeLow;
            retVal.DisplayName = cv.DisplayName;
            retVal.Flavor = cv.Flavor;
            retVal.CodingRationale = cv.CodingRationale;
            //retVal.Group = cv.Group != null ? cv.Group.Clone() as LIST<CDGroup> : null;
            return retVal;
        }

        /// <summary>
        /// Converts a strongly typed CV to a generic CV
        /// </summary>
        internal static CV<String> DownParse(CV<T> cd)
        {
            CV<String> retVal = new CV<String>();
            // Parse from wire format into this format
            retVal.Code = new CodeValue<string>(Util.ToWireFormat(cd.Code));
            // Code System
            retVal.CodeSystem = cd.CodeSystem;
            retVal.CodeSystemName = cd.CodeSystemName;
            retVal.CodeSystemVersion = cd.CodeSystemVersion;
            retVal.ControlActExt = cd.ControlActExt;
            retVal.ControlActRoot = cd.ControlActRoot;
            retVal.UpdateMode = cd.UpdateMode == null ? null : cd.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.NullFlavor = cd.NullFlavor == null ? null : cd.NullFlavor.Clone() as CS<NullFlavor>;
            retVal.ValidTimeHigh = cd.ValidTimeHigh;
            retVal.ValidTimeLow = cd.ValidTimeLow;
            retVal.DisplayName = cd.DisplayName;
            retVal.OriginalText = cd.OriginalText == null ? null : cd.OriginalText.Clone() as ED;
            retVal.CodingRationale = cd.CodingRationale;
            //retVal.Group = cd.Group != null ? cd.Group.Clone() as LIST<CDGroup> : null;
            retVal.UpdateMode = cd.UpdateMode;
            retVal.Flavor = cd.Flavor;
            return retVal;
        }
        #endregion

        #region IEquatable<CV<T>> Members

        /// <summary>
        /// Determine if this CV of T equals another CV of T
        /// </summary>
        public bool Equals(CV<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((CS<T>)other) &&
                    (other.CodingRationale != null ? other.CodingRationale.Equals(this.CodingRationale) : this.CodingRationale == null) &&
                    (other.DisplayName == null ? this.DisplayName == null : other.DisplayName.Equals(this.DisplayName)) &&
                    (other.OriginalText != null ? other.OriginalText.Equals(this.OriginalText) : this.OriginalText == null) &&
                    (this.CodeSystem ?? GetCodeSystem(Code)) == (other.CodeSystem ?? GetCodeSystem(other.Code)) &&
                    this.CodeSystemName == other.CodeSystemName &&
                    this.CodeSystemVersion == other.CodeSystemVersion &&
                    this.ValueSet == other.ValueSet &&
                    this.ValueSetVersion == other.ValueSetVersion;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CV<T>)
                return Equals(obj as CV<T>);
            return base.Equals(obj);
        }

        #endregion

        #region ISemanticEquatable<CV<T>> Members

        /// <summary>
        /// Determines if this instance of CV is semantically equal to another
        /// </summary>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;
            
            ICodedValue otherCv = other as ICodedValue;
            if (otherCv != null) // CS? If so call it's equality
                return otherCv.CodeSystem == null ? this.CodeSystem == null : otherCv.CodeSystem.Equals(this.CodeSystem);
            return baseSem;            
        }

        #endregion
    }
}
