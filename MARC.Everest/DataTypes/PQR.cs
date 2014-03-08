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
using MARC.Everest.DataTypes.Interfaces;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// An extension of the coded value datatype representing a physical quantity using an unit from any code 
    /// system . Used to show alternative representation for a physical quantity. The coded value represents 
    /// the unit (usually in some other coding system other than UCCUM)
    /// </summary>
    /// <seealso cref="T:MARC.Everest.DataTypes.CD`1"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PQR"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    [Structure(Name = "PQR", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("PQR", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class PQR : CV<String>, IRealValue, IEquatable<PQR>
    {

        // Precision
        private int m_precision = 0;

        /// <summary>
        /// Create a new instance of PQR
        /// </summary>
        public PQR() { }

        /// <summary>
        /// Create a new instance of PQR
        /// </summary>
        /// <param name="value">The value of the measurement</param>
        /// <param name="code">The code of the unit of measure</param>
        /// <param name="codeSystem">The code system the unit of measure was drawn from</param>
        public PQR(decimal value, string code, string codeSystem)
        {
            Value = value;
            Code = code;
            CodeSystem = codeSystem;
        }

        /// <summary>
        /// The value of the translation
        /// </summary>
        [Property(Name = "value", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, 
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public decimal? Value { get; set; }

        /// <summary>
        /// The number of significant digits of the decimal representation.
        /// </summary>
        /// <remarks>
        /// <para>Setting the precision has an effect on the graphing of the instance and is populated in the R1 formatter by 
        /// the processing of parsing.</para>
        /// </remarks>
        /// <seealso cref="P:MARC.Everest.Datatypes.REAL.Precision"/>
        public int Precision
        {
            get { return this.m_precision; }
            set
            {
                this.m_precision = value;
            }
        }


        /// <summary>
        /// Represent this PQR as a string
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public override string ToString()
        {
            return string.Format(EverestFrameworkContext.CurrentCulture, "{0} {1}", this.Value, this.Code);
        }

        /// <summary>
        /// Comparator for sets
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public new static Comparison<PQR> Comparator = delegate(PQR a, PQR b)
        {
            if (a.Value == b.Value && a.Code.Equals(b.Code) && a.CodeSystem == b.CodeSystem)
                return 0;
            else
                return 1;
        };

        #region IEquatable<PQR> Members

        /// <summary>
        /// Determine if this PQR equals another PQR
        /// </summary>
        public bool Equals(PQR other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((CV<String>)other) &&
                    other.Precision == this.Precision &&
                    other.Value == this.Value;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is PQR)
                return Equals(obj as PQR);
            return base.Equals(obj);
        }

        #endregion

        #region IQuantity Members

        /// <summary>
        /// Returns the representation of this instance as a double
        /// </summary>
        public double ToDouble()
        {
            return Convert.ToDouble(this.Value);
        }

        /// <summary>
        /// Returns the representation of this instance as an integer
        /// </summary>
        public int ToInt()
        {
            return Convert.ToInt32(this.Value);
        }

        #endregion


        #region IPrimitiveDataValue Members

        /// <summary>
        /// Value of the primitive data value
        /// </summary>
        object IPrimitiveDataValue.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = Util.Convert<Decimal?>(value);
            }
        }

        #endregion
    }
}