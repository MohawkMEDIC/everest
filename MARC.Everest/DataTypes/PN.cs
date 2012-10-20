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
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    ///  A name for a person. A sequence of name parts, such as given name or family name, prefix, suffix, etc. PN differs from EN because the qualifier type cannot include LS (Legal Status).
    /// </summary>
    /// <seealso cref="T:MARC.Everest.DataTypes.EN"/>
    /// <example>Example of PN, which is a restriction of EN
    /// <code lang="us" title="Creating a PN">
    /// <![CDATA[
    ///                PN name = new PN(
    ///                        EntityNameUse.Legal, 
    ///                        new List<ENXP> 
    ///                            { 
    ///                                new ENXP("James", EntityNamePartType.Given), 
    ///                                new ENXP("T", EntityNamePartType.Given),
    ///                                new ENXP("Kirk", EntityNamePartType.Family)
    ///                            }
    ///                        );
    ///        Console.Write(name.ToString("{FAM}, {GIV}")); // Output is Kirk, JamesT
    ///        Console.ReadKey();
    /// ]]>
    /// </code>
    /// </example>
    [Serializable][Structure(Name = "PN", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("PN", Namespace = "urn:hl7-org:v3")]
    public class PN : EN, IEquatable<PN>
    {
        /// <summary>
        /// Create a new instance of the PN class
        /// </summary>
        public PN() : base () {}
        /// <summary>
        /// Create a new instance of the PN class using the parts specified
        /// </summary>
        /// <param name="parts">The parts</param>
        public PN(IEnumerable<ENXP> parts) : base() { this.Part = new List<ENXP>(parts); }
        /// <summary>
        /// Create a new instance of the PN class using the parts and use type specified
        /// </summary>
        /// <param name="use">The use type of the PN</param>
        /// <param name="parts">The parts</param>
        public PN(EntityNameUse use, IEnumerable<ENXP> parts) : base(use, parts) { }
        /// <summary>
        /// Create a new instance of a simple PN
        /// </summary>
        public PN(string value) : this(new ENXP[] { new ENXP(value) }) { }
        /// <summary>
        /// Create a new instance of a simple PN
        /// </summary>
        public PN(EntityNameUse use, string value) : this(use, new ENXP[] { new ENXP(value) }) { }

        /// <summary>
        /// Create a new instance of PN
        /// </summary>
        public static PN CreatePN(EntityNameUse use, params ENXP[] parts)
        {
            return new PN(use, parts);
        }

        /// <summary>
        /// TODO: Find a proper definition for PN.Basic
        /// </summary>
        /// <param name="p">The PN to validate</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "p"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "p"), Flavor(Name = "BASIC")]
        [Flavor(Name = "PN.BASIC")]
        public static bool IsValidBasicFlavor(PN p)
        {
            return true;
        }

        /// <summary>
        /// Validate this instance
        /// </summary>
        public override bool Validate()
        {
            bool isValid = (NullFlavor != null) ^ (Part.Count > 0);
            foreach (ENXP part in Part)
                isValid &= part.Qualifier == null || part.Qualifier.Find(ob => ob.Code.Equals(EntityNamePartQualifier.LegalStatus)) == null;
            return isValid;
        }

        /// <summary>
        /// Extended validation with additional data
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = base.ValidateEx() as List<IResultDetail>;
            foreach (var part in this.Part)
                if (part.Qualifier != null && part.Qualifier.Find(ob => ob.Code.Equals(EntityNamePartQualifier.LegalStatus)) != null)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "PN", String.Format(ValidationMessages.MSG_INVALID_VALUE, "LegalStatus", "Qualifier"), null));
            return retVal;
        }

        /// <summary>
        /// Parse the data from an EN into a PN
        /// </summary>
        internal static PN Parse(EN name)
        {
            var retVal = new PN();
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


        #region IEquatable<PN> Members

        /// <summary>
        /// Determine if this PN equals another PN
        /// </summary>
        public bool Equals(PN other)
        {
            bool result = this.Equals((EN)other);
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is PN)
                return Equals(obj as PN);
            return base.Equals(obj);
        }

        #endregion
    }
}