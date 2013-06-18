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
using System.Xml.Serialization;
using System.ComponentModel;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Trivial Name: A restriction of <see cref="T:MARC.Everest.DataTypes.EN"/>(Entity Name) that is a string used for simple names.
    /// </summary>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// // Name(Brother Justin Crowe) used for religious reasons
    /// TN myTN = new TN(EntityNameUse.Religious, "Brother Justin Crowe");
    /// ]]>
    /// </code>
    /// </example>
    [Structure(Name = "TN", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("TN", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class TN : EN, IEquatable<TN>
    {
        /// <summary>
        /// Create a new instance of the TN class
        /// </summary>
        public TN() : base() { }
        /// <summary>
        /// Create a new instance of the trivial name class using the value specified
        /// </summary>
        /// <param name="s">The value of the name</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public TN(String s) : base() { Part.Add(new ENXP(s) { Type = null }); }
        /// <summary>
        /// Create a new instance of the trivial name class using the "use" and value specified
        /// </summary>
        /// <param name="use">Indicates how to use the TN</param>
        /// <param name="s">Indicates the value of the TN</param>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        //public TN(EntityNameUse use, String s) : base(use, new ENXP[] { new ENXP(s) { Type = null } } ) { }

        /// <summary>
        /// Use is not permitted for TN
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override SET<CS<EntityNameUse>> Use { get; set; }

        /// <summary>
        /// Convert a string to a TN
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static implicit operator TN(string s)
        {
            return new TN(s);
        }

        /// <summary>
        /// Parse the data from an EN into a TN
        /// </summary>
        internal static TN Parse(EN name)
        {
            var retVal = new TN();
            retVal.Part.AddRange(name.Part);
            retVal.ControlActExt = name.ControlActExt;
            retVal.ControlActRoot = name.ControlActRoot;
            retVal.Flavor = name.Flavor;
            retVal.NullFlavor = name.NullFlavor != null ? name.NullFlavor.Clone() as CS<NullFlavor> : null;
            retVal.UpdateMode = name.NullFlavor != null ? name.UpdateMode.Clone() as CS<UpdateMode> : null;
            retVal.Use = name.Use != null ? new SET<CS<EntityNameUse>>(name.Use) : null;
            retVal.ValidTimeHigh = name.ValidTimeHigh;
            retVal.ValidTimeLow = name.ValidTimeLow;
            return retVal;
        }

        /// <summary>
        /// Validate this TN
        /// </summary>
        /// <remarks>
        /// A Trivial name is valid if
        /// <list type="bullet">
        ///     <item>Null Flavor is specified XOR,</item>
        ///     <item>There are exactly one name parts, AND</item>
        ///     <item>The only name part has no part type</item>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            bool isBasicValid = base.Validate();
            if (NullFlavor == null)
                return isBasicValid && (Part.Count == 1 && Part[0].Type == null && Part[0].Qualifier == null);
            return isBasicValid;
        }

        /// <summary>
        /// Validate this TN
        /// </summary>
        /// <remarks>
        /// A Trivial name is valid if
        /// <list type="bullet">
        ///     <item>Null Flavor is specified XOR,</item>
        ///     <item>There are exactly one name parts, AND</item>
        ///     <item>The only name part has no part type</item>
        /// </list>
        /// </remarks>
        /// <returns>A collection of result details explaining validation issues</returns>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = base.ValidateEx() as List<IResultDetail>;
            if (this.NullFlavor == null)
            {
                if (Part.Count != 1)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "TN", ValidationMessages.MSG_INSUFFICIENT_TERMS, null));
                if (this.Part[0].Type != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "TN", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "Type", "Part"), null));
                if(this.Part[0].Qualifier != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "TN", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_PERMITTED_ON_PROPERTY, "Qualifier", "Part"), null));
            }
            return retVal;
        }

        #region IEquatable<TN> Members

        /// <summary>
        /// Determines if the current TN matches another TN
        /// </summary>
        public bool Equals(TN other)
        {
            bool result = base.Equals((EN)other);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is TN)
                return Equals(obj as TN);
            return base.Equals(obj);
        }

        #endregion
    }
}