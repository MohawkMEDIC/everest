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

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// A logically grouped list of related qualifiers
    /// </summary>
    /// <example>
    /// <code title="Creating a group of Qualifiers that have the same methods and properties" lang="cs">
    /// 
    /// <![CDATA[
    ///     CDGroup cdGroup01 = new CDGroup();
    ///     // qualifier01 and qualifier02 are used from the previous example
    ///     cdGroup01.Qualifier = new LIST<CR<string>> { qualifier01, qualifier02 };
    ///     cdGroup01.Validate();
    ///     cdGroup01.ControlActExt = "1.1.1.1";
    /// ]]>
    /// </code>
    /// </example> 
    [Structure(Name = "CDGroup", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("CDGroup", Namespace = "urn:hl7-org:v3")]
    [Obsolete("Class is no longer used", true)]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class CDGroup : ANY, IEquatable<CDGroup>
    {

        /// <summary>
        /// Creates a new instance of the CDGroup class
        /// </summary>
        public CDGroup() { }
        /// <summary>
        /// Creates a new instance of the CDGroup class using the specified list of <paramref name="qualifier"/>
        /// </summary>
        /// <param name="qualifier">The qualifier list to use</param>
        public CDGroup(IEnumerable<CR<String>> qualifier)
            : this(new LIST<CR<String>>(qualifier))
        {
        }
        /// <summary>
        /// Creates a new instance of the CDGroup class using the specified reference to the LIST of <paramref name="qualifier"/>
        /// </summary>
        /// <param name="qualifier">A reference to the list of qualifiers to use</param>
        public CDGroup(LIST<CR<String>> qualifier)
        {
            this.Qualifier = qualifier;
        }

        /// <summary>
        /// A list of qualifiers this group is composed of
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), Property(Name = "qualifier", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        public LIST<CR<String>> Qualifier { get; set; }

        /// <summary>
        /// Validate all qualifiers
        /// </summary>
        public override bool Validate()
        {
            return true;
        }

        #region IEquatable<CDGroup> Members

        /// <summary>
        /// Determine if this CDGroup equals another CDGroup
        /// </summary>
        public bool Equals(CDGroup other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((ANY)other) &&
                    other.Qualifier != null ? other.Qualifier.Equals(this.Qualifier) : this.Qualifier == null;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CDGroup)
                return Equals(obj as CDGroup);
            return base.Equals(obj);
        }

        #endregion
    }
}