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

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// An enumeration of the allowed update modes. See Members for modes.
    /// </summary>
    /// <example>
    /// <code title="Using update mode to dictate how a name should be updated" lang="cs">
    /// <![CDATA[
    ///     EN newName = new EN(
    ///         EntityNameUse.Legal, new ENXP[] { 
    ///         new ENXP("James", EntityNamePartType.Given), 
    ///             new ENXP("Tiberius", EntityNamePartType.Given), 
    ///             new ENXP("Kirk", EntityNamePartType.Family) });
    ///     
    ///     // When the name is encountered, the receiving name will add the name
    ///     // if not already on file, or will replace an existing name
    ///     newName.UpdateMode = UpdateMode.AddOrReplace;
    ///     
    /// ]]>
    /// </code>
    /// </example>
    [Structure(Name = "UpdateMode", CodeSystem = "2.16.840.1.113883.5.57", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    [XmlType("UpdateMode", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public enum UpdateMode
    {
        /// <summary>
        /// The item was (or is to be) added, having not been present before
        /// </summary>
        [XmlEnum("A")]
        [Enumeration(Value = "A")]
        Add,
        /// <summary>
        /// The item was(or is to be) removed
        /// </summary>
        [XmlEnum("D")]
        [Enumeration(Value = "D")]
        Remove,
        /// <summary>
        /// The item existed previously and has been (or is to be) revised
        /// </summary>
        [Enumeration(Value = "R")]
        [XmlEnum("R")]
        Replace,
        /// <summary>
        /// The item was (or is to be) either added or replaced
        /// </summary>
        [Enumeration(Value = "AR")]
        [XmlEnum("AR")]
        AddOrReplace,
        [Enumeration(Value = "AU")]
        [XmlEnum("AU")]
        AddOrUpdate,
        /// <summary>
        /// There was (or is to be) no change to the item
        /// </summary>
        [Enumeration(Value = "N")]
        [XmlEnum("N")]
        NoChange,
        /// <summary>
        /// It is not specified whether or what kind of change has occurred to the item
        /// </summary>
        [Enumeration(Value = "U")]
        [XmlEnum("U")]
        Unknown,
        /// <summary>
        /// The item is part of the identifying information for the object that contains it
        /// </summary>
        [Enumeration(Value = "K")]
        [XmlEnum("K")]
        Key
    }
}