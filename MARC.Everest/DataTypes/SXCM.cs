/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// A code specifying whether the set component is included (union) or excluded (difference) from the set, or other set operations with the current set component and the set as constructed from the representation stream up to the current point.
    /// </summary>
    [XmlType("SetOperator", Namespace = "urn:hl7-org:v3")]
    public enum SetOperator
    {
        /// <summary>
        /// Obsolete, Replaced with Hull
        /// </summary>
        /// <remarks><see cref="E:Hull"/></remarks>
        [Obsolete("Use Hull")]
        H = 1,
        /// <summary>
        /// Form the convex hull with the value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "H")]
        [Enumeration(Value = "H")]
        [XmlEnum("H")]
        Hull = 1,
        /// <summary>
        /// Include the value in the value set (union).
        /// </summary>
        [Enumeration(Value = "I")]
        [XmlEnum("I")]
        Inclusive = 2,
        /// <summary>
        /// Exclude the value from the set (difference).
        /// </summary>
        [Enumeration(Value = "E")]
        [XmlEnum("E")]
        Exclusive = 3,
        /// <summary>
        /// Obsolete, replaced with Intersect
        /// </summary>
        /// <remarks><see cref="E:Intersect"/></remarks>
        [Obsolete("Use Intersect")]
        A = 4,
        /// <summary>
        /// Intersect: Form the intersection with the value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A")]
        [Enumeration(Value = "A")]
        [XmlEnum("A")]
        Intersect = 4,
        /// <summary>
        /// Obsolete, replaced with PeriodicHull
        /// </summary>
        /// <remarks><see cref="E:PeriodicHull"/></remarks>
        [Obsolete("Use PeriodicHull")]
        P = 5,
        /// <summary>
        /// Form the periodic hull with the value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "P")]
        [Enumeration(Value = "P")]
        [XmlEnum("P")]
        PeriodicHull = 5
    }

    /// <summary>
    /// Set component: An individual component belonging to a set. See <see cref="T:MARC.Everest.DataTypes.IVL"/> for an example of a specific type of set.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In R1 SXCM is the base from which all set components are derived, in R2 data types this is QSET.  To keep backwards compatibility, Everest maps
    /// the R2 concept of QSET to an SXCM.
    /// </para>
    /// </remarks>
    /// <seealso cref="T:MARC.Everest.DataTypes.SET"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SXCM")]
    [Structure(Name = "SXCM", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("SXCM", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class SXCM<T> : PDV<T>, IEquatable<SXCM<T>>, ISetComponent<T>
        where T : IAny
    {

        /// <summary>
        /// Create a new instance of SXCM
        /// </summary>
        public SXCM() : base() { }
        /// <summary>
        /// Create a new instance of SXCM with the specified value
        /// </summary>
        /// <param name="value">The value</param>
        public SXCM(T value) : base(value) { }
        /// <summary>
        /// Gets or sets the identifier that dictates how the set component is included as part of the set
        /// </summary>
        [Property(Name = "operator", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public SetOperator? Operator { get; set; }

        /// <summary>
        /// Represent this object as a string
        /// </summary>
        public override string ToString()
        {
            if(Value != null)
                return Value.ToString();
            return base.ToString();
        }

        #region IEquatable<SXCM<T>> Members

        /// <summary>
        /// Determines if this SXCM of T equals another SXCM of T
        /// </summary>
        public bool Equals(SXCM<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((PDV<T>)other) &&
                    other.Operator == this.Operator;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is SXCM<T>)
                return Equals(obj as SXCM<T>);
            return base.Equals(obj);
        }

        #endregion

        /// <summary>
        /// Translate to QSET equivalent
        /// </summary>
        internal ISetComponent<T> TranslateToQSETComponent()
        {
            if (this is IVL<T> || this is PIVL<T> || this is EIVL<T>)
                return this;
            else if (this is SXPR<T>)
                return (this as SXPR<T>).TranslateToQSET();
            else if (this is SXCM<T>) // This is a value that will appear in a QSS
                return QSS<T>.CreateQSS(this.Value);
            return null;
        }

        /// <summary>
        /// Performs validation on the SXCM set
        /// </summary>
        /// <remarks>This function really checks that value isn't set and provides a warning if it does</remarks>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>();
            if (this.Value != null)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Warning, "SXCM", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_PROPERTY_SCHEMA_ONLY, "Value"), null));
            return retVal;
        }

    }
}