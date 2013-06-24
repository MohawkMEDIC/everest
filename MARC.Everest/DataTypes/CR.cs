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
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// A concept qualifier code with optionally named role. Both the qualifier role and value codes must 
    /// be defined by the coding system of the CD containing the concept qualifier.
    /// </summary>
    //[XmlType("CR", Namespace = "urn:hl7-org:v3")]
    //[Obsolete("CR is obsolete, consider using CR<String>", true)]
    //public class CR : CR<string>
    //{
    //}

    /// <summary>
    /// A concept qualifier code with optionally named role. 
    /// </summary>
    /// <remarks>
    /// Both the qualifier role and value codes must 
    /// be defined by the coding system of the CD containing the concept qualifier.
    /// </remarks>
    /// <example>
    /// An example of the use of CR can be seen in the <see cref="T:MARC.Everest.DataTypes.CD{}"/> datatype
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
    [Structure(Name = "CR", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(String))]
    #if !WINDOWS_PHONE
    [Serializable]
    #endif
    public class CR<T> : ANY, IConceptQualifier, IEquatable<CR<T>>
    {

        /// <summary>
        /// Default constructor for the CR type
        /// </summary>
        public CR() { }
        /// <summary>
        /// Creates a CR instance with the specified <paramref name="name"/> and <paramref name="value"/>
        /// </summary>
        /// <param name="name">The name of the qualifier</param>
        /// <param name="value">The value of the qualifier</param>
        public CR(CV<T> name, CD<T> value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Specifies the manner in which the concept role value contributes to the meaning of a code phrase
        /// </summary>
        [Property(Name = "name", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Optional, ImposeFlavorId="CV")]
        public CV<T> Name { get; set; }
        /// <summary>
        /// The concept that modifies the primary code of a code phrase through the role relation
        /// </summary>
        [Property(Name = "value", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public CD<T> Value { get; set; }
        /// <summary>
        /// Indicates if the sense of the name is inverted
        /// </summary>
        /// <remarks>
        /// <para>To be used when a code system supports inversion but does not provide inverted concepts. For example, the concept of "causes" could be
        /// inverted to "caused-by" by setting the inverted flag to true if the concept domain does not provide
        /// a "caused-by" code.</para>
        /// </remarks>
        [Property(Name = "inverted", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public bool Inverted { get; set; }

        /// <summary>
        /// Validate this item
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            bool isValid = (NullFlavor != null) ^ (Value != null && Name != null && Value.OriginalText == null && Value.Validate() && Name.Validate());
            return isValid;
        }

        /// <summary>
        /// Validates this instance of the data type and returns the validations errors
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());

            if (this.NullFlavor == null)
            {
                // Name
                if (Name == null || !this.Name.Validate())
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CR", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_POPULATED, "Name", "CV"), null));
                else
                    retVal.AddRange(this.Name.ValidateEx());

                // Value
                if (Value == null || !this.Value.Validate())
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CR", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_NOT_POPULATED, "Value", "CD"), null));
                else
                    retVal.AddRange(this.Value.ValidateEx());
            }
            else if (this.Value != null || this.Name != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CR", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            return retVal;
        }

        #region IConceptQualifier Members

        /// <summary>
        /// Specifies the manner in which the concept role value contributes to the meaning of a code phrase
        /// </summary>
        ICodedValue IConceptQualifier.Name
        {
            get
            {
                
                return Name;
            }
            set
            {
                if (value is CV<T>)
                    this.Name = (CV<T>)value;
                else
                    this.Name = CV<T>.Parse((CV<String>)value);
            }
        }

        /// <summary>
        /// The concept that modifies the primary code of a code phrase through the role relation
        /// </summary>
        IConceptDescriptor IConceptQualifier.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                if (value is CD<T>)
                    this.Value = (CD<T>)value;
                else
                    this.Name = CD<T>.Parse((CD<String>)value);

            }
        }

        #endregion

        /// <summary>
        /// Comparator for sets
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static new Comparison<CR<T>> Comparator = delegate(CR<T> a, CR<T> b)
        {
            if ((bool)a.Name.SemanticEquals(b.Name) && a.Value == b.Value)
                return 0;
            else
                return 1;
        };

        /// <summary>
        /// Operator casts a <see cref="T:CR"/> to a genericized CR
        /// </summary>
        internal static CR<T> Parse(CR<String> cr)
        {
            CR<T> retVal = new CR<T>();
            retVal.Inverted = cr.Inverted;
            retVal.Name = cr.Name == null ? null : CV<T>.Parse(cr.Name);
            retVal.Value = cr.Value == null ? null : CD<T>.Parse(cr.Value);
            retVal.UpdateMode = cr.UpdateMode == null ? null : cr.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.NullFlavor = cr.NullFlavor as CS<NullFlavor>;
            retVal.ValidTimeHigh = cr.ValidTimeHigh;
            retVal.ValidTimeLow = cr.ValidTimeLow;
            return retVal;
        }

        /// <summary>
        /// Operator casts a <see cref="T:CR"/> to a non-genericized CR
        /// </summary>
        internal static CR<String> DownParse(CR<T> cr)
        {
            CR<String> retVal = new CR<String>();
            retVal.Inverted = cr.Inverted;
            retVal.Name = cr.Name == null ? null : CV<T>.DownParse(cr.Name);
            retVal.Value = cr.Value == null ? null : CD<T>.DownParse(cr.Value);
            retVal.UpdateMode = cr.UpdateMode == null ? null : cr.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.NullFlavor = cr.NullFlavor as CS<NullFlavor>;
            retVal.ValidTimeHigh = cr.ValidTimeHigh;
            retVal.ValidTimeLow = cr.ValidTimeLow;
            return retVal;
        }

        #region IEquatable<CR<T>> Members

        /// <summary>
        /// Determine if this CR of T is equal to another CR of T
        /// </summary>
        public bool Equals(CR<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((ANY)other) &&
                    other.Inverted == this.Inverted &&
                    (other.Name != null ? other.Name.Equals(this.Name) : this.Name == null) &&
                    (other.Value != null ? other.Value.Equals(this.Value) : this.Value == null);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CR<T>)
                return Equals(obj as CR<T>);
            return base.Equals(obj);
        }

        #endregion
    }
}