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
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Primitives;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// Represents a codified value with a series of equivalents.
    /// </summary>
    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance"), Serializable]
    //[XmlType("CE", Namespace = "urn:hl7-org:v3")]
    //[Obsolete("CE is obsolete, consider using CE<String>", true)]
    //public class CE : CE<string>
    //{
    //    /// <summary>
    //    /// Converts a strongly typed CD to a generic CD
    //    /// </summary>
    //    public static CE Parse<T>(CE<T> ce)
    //    {
    //        CE retVal = new CE();
    //        // Parse from wire format into this format
    //        retVal.Code = new CodeValue<string>(Util.ToWireFormat(ce.Code));
    //        // Code System
    //        retVal.CodeSystem = ce.CodeSystem;
    //        retVal.CodeSystemName = ce.CodeSystemName;
    //        retVal.CodeSystemVersion = ce.CodeSystemVersion;
    //        retVal.ControlActExt = ce.ControlActExt;
    //        retVal.ControlActRoot = ce.ControlActRoot;
    //        retVal.UpdateMode = ce.UpdateMode == null ? null : ce.UpdateMode.Clone() as CS<UpdateMode>;
    //        retVal.NullFlavor = ce.NullFlavor == null ? null : ce.NullFlavor.Clone() as CS<NullFlavor>;
    //        retVal.ValidTimeHigh = ce.ValidTimeHigh;
    //        retVal.ValidTimeLow = ce.ValidTimeLow;
    //        retVal.DisplayName = ce.DisplayName;
    //        retVal.OriginalText = ce.OriginalText == null ? null : ce.OriginalText.Clone() as ED;
    //        retVal.Translation = ce.Translation == null ? null : new SET<CD<string>>(ce.Translation, CD<String>.Comparator);
    //        return retVal;
    //    }
    //}

    /// <summary>
    ///  Coded with Equalivalents
    /// </summary>
    /// <remarks>
    /// 
    /// <para>Implements the CD.CE flavor.</para>
    /// <para>Represents a codified value with translations
    /// to other possible code systems</para>
    /// </remarks>
    /// <example>
    /// <code title="CE With Translation to internal Code System" lang="cs">
    /// <![CDATA[
    /// CE ce = new CE();
    /// ce.Code = "284196006";
    /// ce.CodeSystem = "2.16.840.1.113883.6.96";
    /// ce.CodeSystemName = "SNOMED CT";
    /// ce.DisplayName = "Burn of skin";
    /// ce.NullFlavor = null;
    /// ce.Translation = new SET<CD<string>>();
    /// ce.Translation.Add(
    /// new CD<string>()
    /// {
    ///     Code = "15376812",
    ///     CodeSystem = "2.16.840.1.113883.3.232.99.1",
    ///     CodeSystemName = "3M HDD",
    ///     DisplayName = "BurnOfSkinSCT",
    ///     NullFlavor = null
    /// }
    /// );
    /// 
    /// // using these values in the generic constructor CE<T>(T code, string codeSystem, IEnumerable<CD<T>> translation
    /// CE<String> practice2 = new CE<String>(ce.Code, ce.CodeSystem, ce.Translation);
    /// ]]>
    /// </code>
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
    [Structure(Name = "CE", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(String))]
    #if !WINDOWS_PHONE
    [Serializable]
    #endif
    public class CE<T> : CV<T>, ICodedEquivalents, IEquatable<CE<T>>
    {
        /// <summary>
        /// Create a new instance of CE
        /// </summary>
        public CE() { }
        /// <summary>
        /// Create a new instance of CE with the specified code
        /// </summary>
        /// <param name="code">The initial code</param>
        public CE(T code) : base(code) { }
        /// <summary>
        /// Create a new instance of CE with the specified code and code system
        /// </summary>
        /// <param name="code">The initial code of the CS</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        public CE(T code, string codeSystem) : base(code, codeSystem) { }
        /// <summary>
        /// Create a new instance of CE with the specified parameters
        /// </summary>
        /// <param name="code">The initial code</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="codeSystemName">The name of the code system</param>
        /// <param name="codeSystemVersion">The version of the code system</param>
        public CE(T code, string codeSystem, string codeSystemName, string codeSystemVersion) : base(code, codeSystem, codeSystemName, codeSystemVersion) { }
        /// <summary>
        /// Create a new instance of the CE data type with the parameters specified
        /// </summary>
        /// <param name="code">The initial code</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="codeSystemName">The name of the code system</param>
        /// <param name="codeSystemVersion">The version of the code system</param>
        /// <param name="displayName">The display name for the code</param>
        /// <param name="originalText">The original text, the reason the code was selected</param>
        public CE(T code, string codeSystem, string codeSystemName, string codeSystemVersion, ST displayName, ED originalText) : base(code, codeSystem, codeSystemName, codeSystemVersion, displayName, originalText) { }
        /// <summary>
        /// Create a new instance of CE with the specified code and code system
        /// </summary>
        /// <param name="code">The initial code of the CS</param>
        /// <param name="codeSystem">The code system the code was picked from</param>
        /// <param name="translation">Translations for this concept descriptor</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public CE(T code, string codeSystem, IEnumerable<CD<T>> translation) : base(code, codeSystem) 
        {
            Translation = new SET<CD<T>>(translation, CD<T>.Comparator);
        }
        

        /// <summary>
        /// Gets or sets a set of other concept descriptors that provide a translation of this concept descriptor in other code
        /// systems or a synonym to the code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), Property(Name = "translation", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlElement("translation")]
        public SET<CD<T>> Translation { get; set; }

        /// <summary>
        /// Determines if the CE is valid.
        /// </summary>
        /// <remarks>
        /// A CE is valid when:
        /// <list type="bullet">
        ///     <item><term>The base type is valid</term></item>
        ///     <item><term>When a translation is specified, a code is specified</term></item>
        ///     <item><item>None of the translations contain an original text, are valid and don't contain translations</item></item>
        /// </list>
        /// </remarks>
        /// <returns></returns>
        public override bool Validate()
        {
            bool isValid = base.Validate();


            isValid &= (Translation != null && Code != null) || (Translation == null);
            
            // Validate translations
            foreach (CD<T> translation in Translation ?? new SET<CD<T>>())
                isValid &= translation.Validate() && translation.OriginalText == null && translation.Translation == null;

            return isValid;
        }

        /// <summary>
        /// Validate the data type returning the results of valiation
        /// </summary>
        public override IEnumerable<IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());

            if (this.Translation != null && this.Code == null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CE", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "Translation", "Code"), null));
            foreach (var t in this.Translation ?? new SET<CD<T>>())
            {
                retVal.AddRange(t.ValidateEx());
                if (t.OriginalText != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CE", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "OriginalText", "Translation"), null));
                if (t.Translation != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CE", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "Translation", "Translation"), null));
            }
            return retVal;
        }


        #region Operators
        /// <summary>
        /// Converts a <see cref="T:MARC.Everest.Datatypes.CE`1"/> to a <typeparamref name="T"/>
        /// </summary>
        /// <param name="o">CE`1 to convert</param>
        /// <returns>Converted T</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator T(CE<T> o)
        {
            return o.Code;
        }

        /// <summary>
        /// Converts a <typeparamref name="T"/> to a <see cref="T:<MARC.Everest.Datatypes.CE`1"/>
        /// </summary>
        /// <param name="o">T to convert</param>
        /// <returns>Converted CE`1</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator CE<T>(T o)
        {
            CE<T> retVal = new CE<T>();
            retVal.Code = o;
            return retVal;
        }

        /// <summary>
        /// Converts a CE to a strongly typed CE
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        public static CE<T> Parse(CE<String> ce)
        {
            CE<T> retVal = new CE<T>();
            // Parse from wire format into this format
            retVal.Code = CodeValue<T>.Parse(ce.Code);
            // Code System
            retVal.Flavor = ce.Flavor;
            retVal.CodeSystem = ce.CodeSystem;
            retVal.CodeSystemName = ce.CodeSystemName;
            retVal.CodeSystemVersion = ce.CodeSystemVersion;
            retVal.ControlActExt = ce.ControlActExt;
            retVal.ControlActRoot = ce.ControlActRoot;
            retVal.UpdateMode = ce.UpdateMode == null ? null : ce.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.NullFlavor = ce.NullFlavor as CS<NullFlavor>;
            retVal.ValidTimeHigh = ce.ValidTimeHigh;
            retVal.ValidTimeLow = ce.ValidTimeLow;
            retVal.OriginalText = ce.OriginalText == null ? null : ce.OriginalText.Clone() as ED;
            retVal.DisplayName = ce.DisplayName;
            retVal.Translation = ce.Translation == null ? null : new SET<CD<T>>(ce.Translation, CD<T>.Comparator);
            retVal.CodingRationale = ce.CodingRationale;
            //retVal.Group = ce.Group != null ? ce.Group.Clone() as LIST<CDGroup> : null;
            retVal.UpdateMode = ce.UpdateMode;
            
            return retVal;
        }

        /// <summary>
        /// Explicit operator for casting a CE of <typeparamref name="T"/>
        /// to a string
        /// </summary>
        internal static CE<String> DownParse(CE<T> cd)
        {
            CE<String> retVal = new CE<String>();
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
            retVal.Translation = cd.Translation == null ? null : new SET<CD<string>>(cd.Translation, CD<String>.Comparator);
            retVal.Flavor = cd.Flavor;
            return retVal;
        }
        #endregion

        /// <summary>
        /// Comparator for sets
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static new Comparison<CE<T>> Comparator = delegate(CE<T> a, CE<T> b)
        {
            if (a.Code.Equals(b.Code) && a.CodeSystem == b.CodeSystem)
                return 0;
            else
                return 1;
        };

        #region ICodedExtended Members

        /// <summary>
        /// Translations of the original code. 
        /// </summary>
        LIST<MARC.Everest.Interfaces.IGraphable> ICodedEquivalents.Translation
        {
            get
            {
                return new LIST<MARC.Everest.Interfaces.IGraphable>(Translation);
            }
            set
            {
                this.Translation = new SET<CD<T>>(value, CD<T>.Comparator);
            }
        }

        #endregion

        #region IEquatable<CE<T>> Members

        /// <summary>
        /// Determine if this CE of T equals another CE of T
        /// </summary>
        public bool Equals(CE<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((CV<T>)other) &&
                    (other.Translation != null ? other.Translation.Equals(this.Translation) : this.Translation == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CE<T>)
                return Equals(obj as CE<T>);
            return base.Equals(obj);
        }

        #endregion
    }
}