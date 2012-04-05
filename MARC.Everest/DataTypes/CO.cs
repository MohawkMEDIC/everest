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
using System.Xml.Serialization;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Represents data where coded values are associated with a specific order. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// CO may be used for things that model rankings and scores (for example: pain, APGAR values, etc) where there
    /// is an implied ordering, no implication of the distance betweeen each value is constant and the
    /// total number of values is finite.
    /// </para>
    /// <para>
    /// The CO data type seems to completely change meaning in DT R2.  In DT R1, CO was an extension of CV
    /// with no added functionality. In DT R2, CO is a QTY with attached CD. This should be taken into account 
    /// by developers when writing structures that use the CO data type
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance"), Serializable]
    [Structure(Name = "CO", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("CO", Namespace = "urn:hl7-org:v3")]
    public class CO : QTY<Decimal?>, IQuantity, IEquatable<CO>
    {

        /// <summary>
        /// Creates a new instance of the CO class
        /// </summary>
        public CO() : base() { }
        /// <summary>
        /// Creates a new instance of the CO class with the specified <paramref name="value"/>
        /// </summary>
        /// <param name="value">The initial value of the CO</param>
        public CO(Decimal? value) : this(value, null) { }
        /// <summary>
        /// Creates a new instance of the CO class with the specified <paramref name="code"/>
        /// </summary>
        /// <remarks>
        /// Recommended constructor call when using only R1 formatter
        /// </remarks>
        /// <param name="code">A code value that defines the CO ordinal</param>
        public CO(CD<String> code) : this(null, code) { }
        /// <summary>
        /// Creates a new instance of the CO class with the specified <paramref name="code"/> and <paramref name="value"/>
        /// </summary>
        /// <param name="value">The initial value of the CO instance</param>
        /// <param name="code">A code value that defines the CO ordinal</param>
        public CO(Decimal? value, CD<String> code) : base(value) { this.Code = code; }

        /// <summary>
        /// Gets or sets the code that represents the definition of the ordinal item
        /// </summary>
        /// <remarks>
        /// When using datatypes R1 formatter, this will be the only data that is serialized
        /// to the wire. All other properties are ignored by the R1 formatter
        /// </remarks>
        [Property(Name = "code", IgnoreTraversal = false, PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Mandatory)]
        public CD<String> Code { get; set; }

        /// <summary>
        /// Validate to invariant rules, checks to make sure that things that are supposed to be set are set
        /// </summary>
        /// <remarks>A CO is valid when either a nullflavor is specified or 
        /// a value and code are specified (but not both)</remarks>
        public override bool Validate()
        {
            return (NullFlavor != null) ^ (Code != null || Value != null && Value.HasValue) &&
                ((Code != null && Code.Validate()) || Code == null);
        }

        #region IEquatable<CO> Members

        /// <summary>
        /// Determine if this CO equals another CO
        /// </summary>
        public bool Equals(CO other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((QTY<Decimal?>)other) &&
                    (this.Code != null ? this.Code.Equals(other.Code) : (bool)(other.Code == null));
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CO)
                return Equals(obj as CO);
            return base.Equals(obj);
        }

        #endregion

        #region ISemanticEquatable<ICodedSimple> Members

        /// <summary>
        /// Determine semantic equality to a coded simple
        /// </summary>
        public override BL SemanticEquals(IAny other)
        {
            if(other == null)
                return null;
            else if (this.IsNull && other.IsNull)
                return new BL() { NullFlavor = NullFlavorUtil.CommonAncestorWith(this.NullFlavor, other.NullFlavor) };
            else if (this.IsNull ^ other.IsNull)
                return new BL() { NullFlavor = DataTypes.NullFlavor.NotApplicable };
            else if (other is CO)
            {
                CO otherCo = other as CO;
                if (this.Code == null || otherCo.Code == null)
                    return false;
                return this.Code.SemanticEquals(otherCo.Code);
            }
            else if (other is ICodedSimple && this.Code != null)
                return this.Code.SemanticEquals(other);
            else
                return false;
        }

        #endregion

      
    }
}
