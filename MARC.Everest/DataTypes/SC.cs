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
 * User: Jaspinder Singh
 * Date: 01-09-2009
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;
using System.ComponentModel;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Primitives;
using System.Globalization;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// The SC class represents an <see cref="ST"/> that optionally has a code attached.
    /// </summary>
    /// <remarks>
    /// The text must always be present if a code is present.  
    /// Often the code specified is a local code.
    /// </remarks>
    /// <example>An instance of an SC
    /// <code lang="cs" title="An instance of an SC">
    /// <![CDATA[
    ///        string aThing = "A thing"; 
    ///        SC sc = aThing; //implicit conversion from string to SC
    ///        //Populate the properties
    ///        sc.Code = "3216543576"; 
    ///        sc.CodeSystem = "2.16.840.1.113883.6.96";
    ///        sc.CodeSystemName = "hl7-org:v3";
    ///        sc.CodeSystemVersion = "V3-2008N";
    ///        sc.DisplayName = "Laceration";
    ///        sc.NullFlavor = null; //sc has values set, no null flavor needed
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    [Structure(Name = "SC", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("SC", Namespace = "urn:hl7-org:v3")]
    public class SC : ST, IEquatable<SC>
    {

        #region Constructors

        /// <summary>
        /// Creates a new instance of SC.
        /// </summary>
        public SC() 
            : base()
        {

        }

        /// <summary>
        /// Creates a new instance of SC.
        /// </summary>
        /// <param name="data">The data to create SC with.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SC(string data)
            : this()
        {
            this.Value = data;
        }

        /// <summary>
        /// Creates a new instance of SC.
        /// </summary>
        /// <param name="data">The data to create SC with.</param>
        /// <param name="language">The language of the data.</param>
        public SC(string data, string language)
            : this(data)
        {
            this.Language = language;
        
        }

        /// <summary>
        /// Creates a new instance of SC.
        /// </summary>
        /// <param name="data">The data to create SC with.</param>
        /// <param name="language">The language of the data.</param>
        /// <param name="code">The desired code for the SC</param>
        public SC(string data, string language, CD<String> code)
            : this(data, language)
        {
            this.Code = code;
        }
        #endregion
      
        #region Properties

        /// <summary>
        /// Gets or sets the code of attached to the string
        /// </summary>
        [Property(Name = "code", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
        public CD<String> Code { get; set; }

        #endregion

        #region Implicit Operators
        /// <summary>
        /// Implicit case to string.
        /// </summary>
        /// <param name="o">SC instance to cast.</param>
        /// <returns>string representation of SC.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator string(SC o)
        {
            return o.Value;
        }

        /// <summary>
        /// Implicit cast from string.
        /// </summary>
        /// <param name="o">string to cast.</param>
        /// <returns>SC representation of string.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator SC(string o)
        {
            SC retVal = new SC();
            retVal.Value = o;
            retVal.Language = CultureInfo.CurrentCulture.Name; 
            return retVal;
        }
        #endregion

        #region Overrides

        /// <summary>
        /// Validate this class.
        /// </summary>
        /// <returns>True if valid, false otherwise.</returns>
        public override bool Validate()
        {
            // If code is present there must be a value otherwise a null flavor must be specified
            bool isValid = base.Validate();
            // If a code is specified, a value must also be specified and code cannot have a nullFlavor
            isValid &= this.Code != null && this.Value != null && !this.Code.IsNull || this.Code == null;
            // No code is specified when a null flavor is specified
            isValid &= this.Code == null && this.NullFlavor != null || this.NullFlavor == null;
            // Code is not permitted to have an original text
            isValid &= this.Code != null && this.Code.OriginalText == null || this.Code == null;
            isValid &= this.Code != null && this.Code.Validate() || this.Code == null;
            return isValid;
        }

        #endregion

        #region IEquatable<SC> Members

        /// <summary>
        /// Determine if this SC is equal to another SC
        /// </summary>
        public bool Equals(SC other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((ST)other) &&
                    (other.Code != null ? other.Code.Equals(this.Code) : this.Code == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is SC)
                return Equals(obj as SC);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// SC.NT (no translations) flavor
        /// </summary>
        [Flavor("NT")]
        [Flavor("SC.NT")]
        public static bool IsValidNtFlavor(SC sc)
        {
            return sc.Translation == null || sc.Translation.IsEmpty;
        }

        /// <summary>
        /// Creates an instance of the <see cref="T:MARC.Everest.DataTypes.SC"/> type such that it is conformant to the NoTranslations 
        /// flavor
        /// </summary>
        /// <param name="value">The value of the string</param>
        /// <returns>The constructed <see cref="T:MARC.Everest.DataTypes.SC"/> conformant to the NT flavor</returns>
        public static SC CreateNt(string value)
        {
            return new SC(value) { Translation = null };
        }
    }
}
