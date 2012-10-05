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
using System.ComponentModel;
using System.Collections;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// A set of consecutive values of an ordered base data type
    /// </summary>
    /// <remarks>
    /// <para>QSET and its derivatives are concepts defined in HL7v3 data types R2, and as such
    /// should only be used when targeting R2 systems. QSET instances can be used in R1 instances within
    /// <see cref="T:GTS"/> and <see cref="T:SXPR{T}"/> however when formatted using an R1 formatter, the concepts
    /// are mapped to the equivalent R1 <see cref="T:SXCM{T}"/> concepts.</para>
    /// <example>
    /// <code lang="cs" title="Formatting R2 QSET with R1 Formatter">
    /// <![CDATA[
    /// GTS gts = new GTS();
    ///gts.Hull = new QSP<TS>(
    ///    new QSU<TS>(
    ///        new QSI<TS>(
    ///            ((TS)"2001").ToIVL(), 
    ///            ((TS)"20010403").ToIVL()
    ///        ),
    ///        new QSD<TS>(
    ///            new QSI<TS>(
    ///                ((TS)"2005").ToIVL(),
    ///                new PIVL<TS>(
    ///                    ((TS)"20050101").ToIVL(), 
    ///                    new PQ(1,"wk")
    ///                )
    ///            ),
    ///            ((TS)"20050304").ToIVL()
    ///        )
    ///    ),
    ///    ((TS)"200504").ToIVL()
    ///);
    /// ]]>
    /// </code>
    /// when passed through the R1 formatter will result in the following XML:
    /// <code lang="xml" title="R1 Formatter Result">
    /// <![CDATA[
    ///<sxpr xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="SXPR_TS" xmlns="urn:hl7-org:v3">
    ///  <comp xsi:type="SXPR_TS">
    ///    <comp xsi:type="SXPR_TS">
    ///      <comp xsi:type="IVL_TS">
    ///        <low inclusive="true" value="20010101000000.000-0500" />
    ///        <high inclusive="true" value="20011231235959.999-0500" />
    ///      </comp>
    ///      <comp xsi:type="IVL_TS" operator="A">
    ///        <low inclusive="true" value="20010403000000.000-0400" />
    ///        <high inclusive="true" value="20010403235959.999-0400" />
    ///      </comp>
    ///    </comp>
    ///    <comp operator="I" xsi:type="SXPR_TS">
    ///      <comp xsi:type="SXPR_TS">
    ///        <comp xsi:type="IVL_TS">
    ///          <low inclusive="true" value="20050101000000.000-0500" />
    ///          <high inclusive="true" value="20051231235959.999-0500" />
    ///        </comp>
    ///        <comp xsi:type="PIVL_TS" operator="A">
    ///          <phase>
    ///            <low inclusive="true" value="20050101000000.000-0500" />
    ///            <high inclusive="true" value="20050101235959.999-0500" />
    ///          </phase>
    ///          <period unit="wk" value="1" />
    ///        </comp>
    ///      </comp>
    ///      <comp xsi:type="IVL_TS" operator="E">
    ///        <low inclusive="true" value="20050304000000.000-0500" />
    ///        <high inclusive="true" value="20050304235959.999-0500" />
    ///      </comp>
    ///    </comp>
    ///  </comp>
    ///  <comp xsi:type="IVL_TS" operator="P">
    ///    <low inclusive="true" value="20090401000000.000-0400" />
    ///    <high inclusive="true" value="20090430235959.999-0400" />
    ///  </comp>
    ///</sxpr>
    /// ]]>
    /// </code>
    /// Which is equivalent but not identical to the original structure when parsed
    /// </example>
    /// <para>
    /// While it is possible to use this structure in R1 set expression constructs, it is not
    /// possible to do the opposite since the structure of an SXPR is more loosely defined than
    /// structures based on QSET.
    /// </para>
    /// </remarks>
    /// <seealso cref="T:QSET{T}"/>
    /// <seealso cref="T:QSD{T}"/>
    /// <seealso cref="T:QSI{T}"/>
    /// <seealso cref="T:QSP{T}"/>
    /// <seealso cref="T:QSS{T}"/>
    /// <seealso cref="T:QSU{T}"/>
    /// <seealso cref="T:SXPR{T}"/>
    /// <seealso cref="T:SXCM{T}"/>
    /// <seealso cref="T:GTS"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "QSET"), Serializable]
    [Structure(Name = "QSET", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("QSET", Namespace = "urn:hl7-org:v3")]
    public abstract class QSET<T> : ANY, IEquatable<QSET<T>>, ISetComponent<T>, IOriginalText, INormalizable
        where T : IAny
    {

        /// <summary>
        /// Default constructor for value
        /// </summary>
        public QSET() { }

        /// <summary>
        /// Reasoning behind the selection of the SET value
        /// </summary>
        [Property(Name = "originalText", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural,
            Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public ED OriginalText { get; set; }

        /// <summary>
        /// Equals override
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is QSET<T>)
                return Equals((QSET<T>)obj);
            return base.Equals(obj);
        }

        #region IEquatable<QSET<T>> Members

        /// <summary>
        /// Determines if this QSET is equal to <paramref name="other"/>
        /// </summary>
        /// <param name="other">The QSET to compare to</param>
        /// <returns>True if the two QSETs contain identical content</returns>
        public bool Equals(QSET<T> other)
        {
            bool result = false;
            if (other != null)
                result = (this.OriginalText != null ? this.OriginalText.Equals(other.OriginalText) : other.OriginalText == null) &&
                    base.Equals(other);
            return result;
        }

        #endregion

        #region To SXPR

        /// <summary>
        /// Get the equivalent set operator
        /// </summary>
        /// <returns></returns>
        protected abstract SetOperator GetEquivalentSetOperator();

        /// <summary>
        /// Translate this QSET to an SXPR
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">When a member of the set cannot be translated to an SXCM expression</exception>
        public SXPR<T> TranslateToSXPR()
        {
            SXPR<T> retVal = new SXPR<T>();

            if (this.NullFlavor != null)
                retVal.NullFlavor = this.NullFlavor.Clone() as CS<NullFlavor>;
            else if (this is IEnumerable)
            {
                IEnumerator ienum = (this as IEnumerable).GetEnumerator();
                while (ienum.MoveNext())
                {
                    IAny current = ienum.Current as IAny;
                    if (current is QSET<T>)
                    {
                        var sxpr = (current as QSET<T>).TranslateToSXPR();
                        sxpr.Operator = this.GetEquivalentSetOperator();
                        retVal.Add(sxpr);
                    }
                    else if (current is SXCM<T>)
                    {
                        var sxcm = current.Clone() as SXCM<T>;
                        sxcm.Operator = this.GetEquivalentSetOperator(); // This is a shallow clone, but that is ok since we only want to modify the SetOperator which is immutable
                        retVal.Add(sxcm);
                    }
                    else if (current is T)
                    {
                        SXCM<T> sxcm = null;
                        if (current is IImplicitInterval<T>)
                            sxcm = (current as IImplicitInterval<T>).ToIVL();
                        else
                            throw new InvalidOperationException("Cannot interpret the member of the set with type '{0}' as it cannot be converted to a union of interval");
                        sxcm.Operator = this.GetEquivalentSetOperator();
                        retVal.Add(sxcm);
                    }
                }
            }

            return retVal;
        }

        #endregion
        
        #region INormalizable Members

        /// <summary>
        /// Normalize this structure
        /// </summary>
        public abstract MARC.Everest.Interfaces.IGraphable Normalize();

        #endregion
    }
}