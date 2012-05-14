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
using MARC.Everest.Interfaces;
using System.Xml.Serialization;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// This implementation of the HXIT type is used for R1 data types compatibility. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// In the MIF for R1 data types, HXIT&lt;<typeparamref name="T"/>&gt; is referenced when a data type can 
    /// use the HXIT members. In the R1 formatter, no HXIT data types are serialized or parsed by 
    /// default (as all DTs in the class lib have HXIT attributes, but not all DTs in R1 can 
    /// use HXIT attributes). When a HXIT&lt;T&gt; is used, it marks the core DT as being allowed
    /// to use the HXIT properties at the base of the inheritence tree.
    /// </para>
    /// <para>
    /// R1 can't represent the ValidTimeLow property of the CS because R1 doesn't allow all DTs to 
    /// carry history data. When this CS is formatted, only the Code attribute will be serialized. By
    /// wrapping the CS class in an HXIT the formatter is instructed to use the HXIT members of the CS
    /// </para><para>
    /// The serialization will include the code and valid time low property
    /// </para>
    /// </remarks>
    /// <example>
    /// 
    /// <code lang="cs" title="CS in DT R1 has all the HXIT data, so theoretically">
    /// CS p = new CS();
    /// p.ValidTimeLow = DateTime.Now;
    /// p.Code = "M";
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// HXIT&lt;CS&gt; p = new HXIT&lt;CS&gt;();
    /// p.ValidTimeLow = DateTime.Now;
    /// </code>
    /// </example>
    /// <typeparam name="T">The type to mark as using the HXIT parameters</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HXIT"), Serializable]
    [Structure(Name = "HXIT", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("HXIT", Namespace = "urn:hl7-org:v3")]
    public class HXIT<T> : HXIT
        where T : ANY, new()
    {

        /// <summary>
        /// Create a new instance of HXIT
        /// </summary>
        public HXIT() : base() { }
        /// <summary>
        /// Create a new instance of HXIT using the value <paramref name="value"/>
        /// </summary>
        /// <param name="value">The initial value of the HXIT</param>
        public HXIT(T value) : this() { this.Value = value; }

        /// <summary>
        /// The value of the HXIT
        /// </summary>
        [Property(IgnoreTraversal = true)]
        public T Value { get; set; }

        #region Operators
        /// <summary>
        /// Converts a <see cref="HXIT"/> to a <typeparamref name="T"/>
        /// </summary>
        /// <param name="o">HXIT`1 to convert</param>
        /// <returns>Converted T</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator T(HXIT<T> o) { return o.Value; }

        /// <summary>
        /// Converts a <typeparamref name="T"/> to a <see cref="HXIT"/>
        /// </summary>
        /// <param name="o">T to convert</param>
        /// <returns>Converted HXIT`1</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator HXIT<T>(T o) { return new HXIT<T>(o); }

        #endregion

        /// <summary>
        /// Validate this HXIT
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return Value != null;
        }
    }

    /// <summary>
    /// The HistoryItem or HXIT is a generic data type extension that tags a time range and control
    /// act event to any datatype. The time range is the time in which the information represented
    /// by the value is (was) valid and which control act event modified the value
    /// </summary>
    /// <remarks>
    /// <para>Business Name: HistoryItem</para>
    /// <para>
    /// HXIT class in ITS. This implementation is in the style of ITS R2 HXIT. This is because of 
    /// a severe implementation problem with the R1 HXIT class
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HXIT"), Serializable]
    [Structure(Name = "_HXIT", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    public abstract class HXIT : IGraphable
    {
        /// <summary>
        /// Identifies the time that the given information has or will become valid
        /// </summary>
        [Property(Name = "validTimeLow", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public virtual TS ValidTimeLow { get; set; }
        /// <summary>
        /// Identifies the time that the given information has or will no longer be valid
        /// </summary>
        [Property(Name = "validTimeHigh", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public virtual TS ValidTimeHigh { get; set; }
        /// <summary>
        /// Identifies the root of the identifier of the event associated with the setting of the data type to the value
        /// </summary>
        [Property(Name = "controlActRoot", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public virtual string ControlActRoot { get; set; }
        /// <summary>
        /// Identifies the extension of the identifier of the event associated with the setting of the data type to the value
        /// </summary>
        [Property(Name = "controlActExt", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        public virtual string ControlActExt { get; set; }

        #region IGraphable Members

        /// <summary>
        /// When overriden in a derived class, this method validates that the contents of the data type
        /// are valid.
        /// </summary>
        /// <remarks>
        /// An HXIT passes validation when a ControlActRoot and ControlActExt are populated or 
        /// neither ControlActRoot and ControlActExt are populated.
        /// </remarks>
        public virtual bool Validate()
        {
            return this.ControlActRoot == null && this.ControlActExt == null ||
                this.ControlActRoot != null && this.ControlActExt != null;
        }

        /// <summary>
        /// Validates the structure is conformant, returning the result details that are in violation
        /// </summary>
        public virtual IEnumerable<IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>();
            if ((this.ControlActRoot == null) ^ (this.ControlActExt == null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "HXIT",
                    this.ControlActExt == null ? "ControlActExt must be populated when ControlActRoot is populated" :
                    "ControlActRoot must be populated when ControlActExt is populated", null));
            return retVal;
        }

        #endregion
    }
}