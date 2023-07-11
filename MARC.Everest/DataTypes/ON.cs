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
 * Date: 10-20-2010
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MARC.Everest.Attributes;
using MARC.Everest.Connectors;

#if WINDOWS_PHONE
using MARC.Everest.Phone;
#endif

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// A name for an organization
    /// </summary>
    /// <remarks>
    /// Organization Name
    /// </remarks>
    [XmlType("ON", Namespace = "urn:hl7-org:v3")]
    [Structure(Name = "ON", StructureType = StructureAttribute.StructureAttributeType.DataType)]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class ON : EN, IEquatable<ON>
    {

        /// <summary>
        /// Create a new instance of EN
        /// </summary>
        public ON() : base() { Part = new List<ENXP>(); }
        /// <summary>
        /// Create a new entity named instance using the specified values
        /// </summary>
        /// <param name="parts">The parts of the names</param>
        /// <param name="use">The uses of this name</param>
        public ON(EntityNameUse use, IEnumerable<ENXP> parts)
            : base(use, parts)
        {
        }
        /// <summary>
        /// Create a new entity named instance using the specified values
        /// </summary>
        /// <param name="parts">The parts of the names</param>
        /// <param name="use">The uses of this name</param>
        public ON(SET<CS<EntityNameUse>> use, IEnumerable<ENXP> parts)
            : base(use, parts)
        {
        }

        /// <summary>
        /// Creates an organization name
        /// </summary>
        public static ON CreateON(EntityNameUse use, params ENXP[] parts)
        {
            return new ON(use, parts);
        }

        /// <summary>
        /// Creates an organization name
        /// </summary>
        public static ON CreateON(SET<CS<EntityNameUse>> use, params ENXP[] parts)
        {
            return new ON(use, parts);

        }
        /// <summary>
        /// Validate the organization name
        /// </summary>
        /// <remarks>
        /// An organization name is valid if:
        /// <list type="bullet">
        ///     <item>All validation rules from <see cref="T:MARC.Everest.DataTypes.EN"/> are satisfied</item>
        ///     <item>The <see cref="Use"/> property is not one of {Indigenous, Pseudonym, Anonymous, Artist, Religious, MaidenName }</item>
        ///     <item>The <see cref="F:MARC.Everest.DataTypes.ENXP.Type"/> property of each <see cref="Part"/> is not in { Given, Family }</item>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            bool isValid = base.Validate();
            List<EntityNamePartType> disallowedPartTypes = new List<EntityNamePartType>()
                { 
                    EntityNamePartType.Given, 
                    EntityNamePartType.Family 
                };
            List<EntityNameUse?> disallowedUses = new List<EntityNameUse?>()
            {
                EntityNameUse.Indigenous,
                EntityNameUse.Pseudonym,
                EntityNameUse.Anonymous,
                EntityNameUse.Artist,
                EntityNameUse.Religious,
                EntityNameUse.MaidenName
            };

            //object obj = disallowedQualifiers.Find(o => Use.Find(u => u.Code.Equals(o)) != null);
            if(Use != null)
                isValid &= !disallowedUses.Exists(o => Use.Find(u=>u.Code.Equals(o)) != null);

            // Validate parts
            foreach (var part in Part)
                isValid &= !disallowedPartTypes.Contains(part.Type.HasValue ? part.Type.Value : EntityNamePartType.Title);

            return isValid;
        }

        /// <summary>
        /// Extended validation with reported problems
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = base.ValidateEx() as List<IResultDetail>;


            List<EntityNamePartType> disallowedPartTypes = new List<EntityNamePartType>()
                { 
                    EntityNamePartType.Given, 
                    EntityNamePartType.Family 
                };
            List<EntityNameUse?> disallowedUses = new List<EntityNameUse?>()
            {
                EntityNameUse.Indigenous,
                EntityNameUse.Pseudonym,
                EntityNameUse.Anonymous,
                EntityNameUse.Artist,
                EntityNameUse.Religious,
                EntityNameUse.MaidenName
            };

            //object obj = disallowedQualifiers.Find(o => Use.Find(u => u.Code.Equals(o)) != null);
            if (Use != null && disallowedUses.Exists(o => Use.Find(u => u.Code.Equals(o)) != null))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ON", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_INVALID_VALUE, this.Use, "Use")));

            // Validate parts
            foreach (var part in Part)
                if(disallowedPartTypes.Contains(part.Type.HasValue ? part.Type.Value : EntityNamePartType.Title))
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "ON", String.Format(EverestFrameworkContext.CurrentCulture, ValidationMessages.MSG_INVALID_VALUE, part.Type, "Part.Type")));

            return retVal;
        }

        /// <summary>
        /// Parse the data from an EN into an ON
        /// </summary>
        internal static ON Parse(EN name)
        {
            var retVal = new ON();
            retVal.Part.AddRange(name.Part);
            retVal.ControlActExt = name.ControlActExt;
            retVal.ControlActRoot = name.ControlActRoot;
            retVal.Flavor = name.Flavor;
            retVal.NullFlavor = name.NullFlavor != null ? name.NullFlavor.Clone() as CS<NullFlavor> : null;
            retVal.UpdateMode = name.UpdateMode != null ? name.UpdateMode.Clone() as CS<UpdateMode> : null;
            retVal.Use = name.Use != null ? new SET<CS<EntityNameUse>>(name.Use) : null;
            retVal.ValidTimeHigh = name.ValidTimeHigh;
            retVal.ValidTimeLow = name.ValidTimeLow;
            return retVal;
        }

        #region IEquatable<ON> Members

        /// <summary>
        /// Determine if this ON equals another ON
        /// </summary>
        public bool Equals(ON other)
        {
            bool retVal = base.Equals((EN)other);
            return retVal;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ON)
                return Equals(obj as ON);
            return base.Equals(obj);
        }

        #endregion

       
    }
}
