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
using MARC.Everest.Interfaces;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;
using System.ComponentModel;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// The primitive data value  (PDV) data type is not explicitly defined by HL7, however it is used here as a 
    /// shortcut to allow many types to share functionality
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PDV"), Serializable]
    [Structure(Name = "PDV", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [DefaultProperty("Value")]
    public abstract class PDV<T> : ANY, IPrimitiveDataValue<T>, IEquatable<PDV<T>>
    {

        // Floating point equality tolerance
        protected double p_floatingPointEqualityTolerance = 1e-15;

        /// <summary>
        /// Create a new instance of PDV
        /// </summary>
        public PDV() { }

        /// <summary>
        /// Create a new instance of PDV with <paramref name="value"/> as initial value
        /// </summary>
        /// <param name="value">Initial value</param>
        public PDV(T value) { this.Value = value; }

        /// <summary>
        /// Gets or sets the value encapsulated by this object
        /// </summary>
        [Property(Name = "value", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public virtual T Value { get; set; }

        /// <summary>
        /// Validate this class
        /// </summary>
        /// <remarks>
        /// The instance is valid if either the nullFlavor is set XOR the value is set and all 
        /// validation rules from the base class are succeed.
        /// </remarks>
        public override bool Validate()
        {
            return ((this.NullFlavor != null) ^ (Value != null)) && base.Validate();
        }

        /// <summary>
        /// Return this PDV as a string
        /// </summary>
        public override string ToString()
        {
            return NullFlavor != null ? "" : Value == null ? "" : Value.ToString();
        }

        /// <summary>
        /// Comparator for sets
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static Comparison<PDV<T>> Comparator = delegate(PDV<T> a, PDV<T> b)
        {
            if (a.Value.Equals(b.Value))
                return 0;
            else
                return 1;
        };

        #region IEquatable<PDV<T>> Members

        /// <summary>
        /// Determine if this PDV of T is equal to another PDV of T
        /// </summary>
        public bool Equals(PDV<T> other)
        {
            bool result = false;
            if (other != null)
            {
                // JF - When dealing with float or real, it is possible that some values may have lost
                // some precision, to compensate for this, we'll ensure that the different between
                // the two values is acceptable
                if ((other.Value is float? || other.Value is double?) &&
                    this.Value != null && other.Value != null)
                {
                    result = base.Equals((ANY)other);
                    double otherValue = Convert.ToDouble(other.Value),
                        thisValue = Convert.ToDouble(this.Value);
                    if (double.IsNaN(otherValue)) result &= double.IsNaN(thisValue);
                    else if (double.IsInfinity(otherValue)) result &= double.IsInfinity(thisValue);
                    else if (otherValue == 0) result &= thisValue == 0;
                    else 
                        result &= Math.Abs(otherValue - thisValue) <= Math.Abs(otherValue * this.p_floatingPointEqualityTolerance);

                }
                else
                    result = base.Equals((ANY)other) &&
                        (this.Value != null ? this.Value.Equals(other.Value) : other.Value == null);
            }
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is PDV<T>)
                return Equals(obj as PDV<T>);
            return base.Equals(obj);
        }

        #endregion



        #region IPrimitiveDataValue Members

        /// <summary>
        /// Gets or sets the value of the primitive
        /// </summary>
        object IPrimitiveDataValue.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = Util.Convert<T>(value);
            }
        }

        #endregion
    }
}