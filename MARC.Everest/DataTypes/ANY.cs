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
using System.ComponentModel;
using MARC.Everest.Design;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Defines the basic properties of every data value. 
    /// </summary>
    /// <remarks>
    /// This is an abstract type meaning that no value
    /// can be just a data value without belonging to any concrete type. Every concrete type is a specialization
    /// of this general abstract data value type    
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ANY"), Serializable]
    [Structure(Name = "ANY", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("ANY", Namespace = "urn:hl7-org:v3")]
    public class ANY : HXIT, IAny, IImplementsNullFlavor, IEquatable<ANY>, ISemanticEquatable
    {

        /// <summary>
        /// Creates a new instance of the ANY class
        /// </summary>
        public ANY() { }

        
        /// <summary>
        /// Gets or sets an exceptional <see cref="T:MARC.Everest.DataTypes.NullFlavor"/> value that identifies why the 
        /// contents of a datatype are not being processed.
        /// </summary>
        /// <remarks>
        /// Typically, when a null flavor is specified the data value within the instance is not written on the wire. For exceptions to this
        /// rule see the reference guide.
        /// <para>
        /// Null flavors are heirarchical and may imply one another. For example, a NullFlavor or PINF implies OTH which implies NI. <see cref="M:MARC.Everest.DataTypes.NullFlavorUtil.IsChildConcept(MARC.Everest.DataTypes.NullFlavor)"/>
        /// provides a mechanism for determining the implies relationship between null flavors
        /// </para>
        /// </remarks>
        [Property(Name = "nullFlavor", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        [Marker(MarkerType = MarkerAttribute.MarkerAttributeType.NullFlavor)]
        [TypeConverter(typeof(DataTypeConverter))]
        [XmlElement("nullFlavor")]
        public CS<NullFlavor> NullFlavor { get; set; }

        /// <summary>
        /// Gets or sets the update mode of the datatype
        /// </summary>
        /// <remarks>
        /// the update mode dictates how a receiver will treat this particular data value when posted
        /// </remarks>
        [Property(Name = "updateMode", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        [Marker(MarkerType = MarkerAttribute.MarkerAttributeType.UpdateMode)]
        [TypeConverter(typeof(DataTypeConverter))]
        [XmlElement("updateMode")]
        public CS<UpdateMode> UpdateMode { get; set; }

        /// <summary>
        /// Gets or sets the flavor identifier for the datatype
        /// </summary>
        /// <remarks>The flavor identifier is used by specializations to validate the contents of the datatype according
        /// to its value
        /// </remarks>
        [Property(Name = "flavorId", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        [Marker(MarkerType = MarkerAttribute.MarkerAttributeType.Flavor)]
        [XmlAttribute("flavorId")]
        public virtual string Flavor { get; set; }

        /// <summary>
        /// The data type of the value
        /// </summary>
        /// <remarks>Every proper data value implicitly carries information about its datatype
        /// </remarks>
        public Type DataType 
        {
            get
            {
                return this.GetType();
            }
        }

        /// <summary>
        /// A predicate indicating that a value is an exceptional value or a null value
        /// </summary>
        public virtual bool IsNull
        {
            get
            {
                return NullFlavor != null;
            }

        }

        /// <summary>
        /// Clone this object
        /// </summary>
        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        #region IEquatable<ANY> Members

        /// <summary>
        /// Determine if this ANY is equal to another ANY
        /// </summary>
        public bool Equals(ANY other)
        {
            bool result = false;
            if(other != null)
                result = other.ControlActExt == this.ControlActExt &&
                    other.ControlActRoot == this.ControlActRoot &&
                    other.Flavor == this.Flavor &&
                    (other.NullFlavor == null ? this.NullFlavor == null : other.NullFlavor.Equals(this.NullFlavor)) &&
                    (other.UpdateMode == null ? this.UpdateMode == null : other.UpdateMode.Equals(this.UpdateMode)) &&
                    (other.ValidTimeHigh == null ? this.ValidTimeHigh == null : other.ValidTimeHigh.Equals(this.ValidTimeHigh)) &&
                    (other.ValidTimeLow == null ? this.ValidTimeLow == null : other.ValidTimeLow.Equals(this.ValidTimeLow)) &&
                    (this.DataType == other.DataType);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ANY)
                return Equals(obj as ANY);
            return base.Equals(obj);
        }

        #endregion

        #region ISemanticEquatable<ANY> Members

        /// <summary>
        /// Compares this instance of ANY with another
        /// </summary>
        /// <param name="other">The other <see cref="T:MARC.Everest.Datatypes.ANY"/> to comapre</param>
        /// <returns>True if this instance and <paramref name="other"/> are semantically equal</returns>
        /// <remarks>Since the semantic equals method uses the native boolean type for its implementation, the following alterations have been made
        /// to the equality rules:
        /// <list type="number">
        /// <item><description>If <paramref name="other"/> is null the result is null</description></item>
        /// <item><description>If <paramref name="other"/> has a nullFlavor or this instance has a nullflavor the result is a BL with a nullFlavor of NA (as per 7.1.4 of reference guide) or the most common null flavor between the two. </description></item>
        /// <item><description>If <paramref name="other"/>'s data type does not equal this instance's data type the result is false</description></item>
        /// <item><description>If <paramref name="other"/> and this instance have a null flavor, the common anscestor </description></item>
        /// </list>
        /// </para></remarks>
        public virtual BL SemanticEquals(IAny other)
        {
            if (other == null)
                return null;
            else if (this.IsNull && other.IsNull)
                return new BL() { NullFlavor = NullFlavorUtil.GetCommonParent(this.NullFlavor, other.NullFlavor) };
            else if (this.IsNull ^ other.IsNull)
                return new BL() { NullFlavor = DataTypes.NullFlavor.NotApplicable };
            // Have the same type
            bool isEqual = other.DataType == this.DataType;

            return isEqual;
        }

        #endregion

        /// <summary>
        /// Validate the ANY
        /// </summary>
        /// <remarks>An ANY is valid if the type is not ANY (is a subclass of ANY), or
        /// is an ANY, has a nullFlavor and the nullFlavor is not a child of Invalid</remarks>
        public override bool Validate()
        {
            bool isAny = this.GetType() == typeof(ANY),
                isNullFlavorSet = this.NullFlavor != null,
                isNullFlavorINV = isNullFlavorSet && ((NullFlavor)this.NullFlavor).IsChildConcept(MARC.Everest.DataTypes.NullFlavor.Invalid);

            return base.Validate() &&
                !isAny || (isAny && isNullFlavorSet && !isNullFlavorINV);
        }

        /// <summary>
        /// Validate the ANY meets validaton criteria and identifies the problems
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());
            bool isAny = this.GetType() == typeof(ANY),
                isNullFlavorSet = this.NullFlavor != null,
                isNullFlavorINV = isNullFlavorSet && ((NullFlavor)this.NullFlavor).IsChildConcept(MARC.Everest.DataTypes.NullFlavor.Invalid);

            if (isAny && !isNullFlavorSet)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ANY", "When ANY is used it must carry a NullFlavor", null));
            else if (isAny && !isNullFlavorINV)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ANY", "NullFlavor must imply 'Invalid'", null));
            return retVal;
        }
    }
}