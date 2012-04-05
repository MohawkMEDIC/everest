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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{

    /// <summary>
    /// A feature that is a list of values to choose from
    /// </summary>
    public abstract class Enumeration : Feature
    {

        /// <summary>
        /// This is used when this enumeration merely points to another valueset or code system from
        /// which this reference draws its values.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<Enumeration> EnumerationReference { get; set; }

        /// <summary>
        /// The realm (example CODD_VOUV) that "owns" this enumeration. This is done because there may be 
        /// a variety of value sets (example: HealthCareProviderRoleType) that have the same name
        /// </summary>
        public string OwnerRealm { get; set; }

        /// <summary>
        /// Gets or sets the date that the enumeration codes were released
        /// </summary>
        public DateTime VersionDate { get; set; }

        /// <summary>
        /// Represents one value from the enumeration
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2218:OverrideGetHashCodeOnOverridingEquals"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class EnumerationValue : Feature
        {

            /// <summary>
            /// Get or sets the code system the value comes from
            /// </summary>
            public string CodeSystem { get; set; }
            /// <summary>
            /// Gets or sets the name of the code system the value comes from
            /// </summary>
            public string CodeSystemName
            {
                get
                {
                    if (this.MemberOf == null) return null;
                    var csEnum = this.MemberOf.Find(o => o is Enumeration && (o as Enumeration).Id.Equals(CodeSystem));
                    if (csEnum != null)
                        return csEnum.Name;
                    return null;
                }
            }
            /// <summary>
            /// Code system type
            /// </summary>
            public string CodeSystemType
            {
                get;
                set;
            }

            /// <summary>
            /// Gets a reference to the container code system
            /// </summary>
            public Enumeration Container {
                get 
                {
                    if (this.MemberOf == null) return null;
                    var csEnum = this.MemberOf.Find(o => o is Enumeration && (o as Enumeration).Id.Equals(CodeSystem));
                    if (csEnum != null)
                        return csEnum as Enumeration;
                    return null;
                }
            }

            /// <summary>
            /// Gets or sets a list of codes that are related to the current enumeration
            /// </summary>
            public List<EnumerationValue> RelatedCodes { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
            public override bool Equals(object obj)
            {
                return (this.BusinessName == (obj as Feature).BusinessName &&
                    this.Name == (obj as Feature).Name);
                    
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
            public override string ToString()
            {
                return string.Format("{0}{1};", Name, BusinessName != null ? " alias '" + BusinessName + "'" : "");
            }
        }

        // Literals
        private List<EnumerationValue> literals = new List<EnumerationValue>();

        /// <summary>
        /// Partial set?
        /// </summary>
        public bool IsPartial { get; set;  }

        /// <summary>
        /// The OID of the code system that this enumeration is derived from.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets a description of the type of enumeration that a particular implementer represents
        /// </summary>
        public abstract string EnumerationType { get; }
        /// <summary>
        /// Identifies the literals for this enumeration
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<EnumerationValue> Literals
        {
            get { return literals; }
            set { literals = value; }
        }

        /// <summary>
        /// The base enumeration (specializes)
        /// </summary>
        public Enumeration BaseEnumeration { get; set; }

        /// <summary>
        /// True if the code set is a value set
        /// </summary>
        public virtual bool IsValueSet { get { return false; } }

        /// <summary>
        /// Gets or sets the OID of the content within this value
        /// </summary>
        public string ContentOid { get; set; }

        /// <summary>
        /// Gets or sets the name of the content
        /// </summary>
        public string ContentName
        {
            get
            {
                if (this.MemberOf == null) return null;
                var csEnum = this.MemberOf.Find(o => o is Enumeration && (o as Enumeration).Id.Equals(ContentOid));
                if (csEnum != null)
                    return csEnum.Name;
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the name of the content
        /// </summary>
        public Enumeration ContentRef
        {
            get
            {
                if (this.MemberOf == null) return null;
                var csEnum = this.MemberOf.Find(o => o is Enumeration && (o as Enumeration).Id.Equals(ContentOid));
                if (csEnum != null)
                    return csEnum as Enumeration;
                return null;
            }
        }

        /// <summary>
        /// Represent this enumeration as a string
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("enum {0}{1} (\r\n", Name, BusinessName != null ? " alias '" + BusinessName + "'" : "");

            foreach (EnumerationValue val in Literals)
                sb.AppendFormat("\t{0}\r\n", val.ToString());

            sb.Append(");");

            return sb.ToString();
        }

        public override void FireParsed()
        {
            base.FireParsed();
            foreach (var en in Literals ?? new List<EnumerationValue>())
                en.MemberOf = this.MemberOf;
        }


        /// <summary>
        /// Get a flat list of literals
        /// </summary>
        public List<Enumeration.EnumerationValue> GetEnumeratedLiterals()
        {
            return BuildFlattenedLiteralTree(this.literals);
        }

        /// <summary>
        /// Build flattened literal tree
        /// </summary>
        private List<EnumerationValue> BuildFlattenedLiteralTree(List<EnumerationValue> literals)
        {
            var retVal = new List<EnumerationValue>(literals.Count);
            foreach (var lt in literals)
            {
                retVal.Add(lt);
                if (lt.RelatedCodes != null)
                    foreach (var lt2 in BuildFlattenedLiteralTree(lt.RelatedCodes))
                        if (!retVal.Contains(lt2))
                            retVal.Add(lt2);
            }
            return retVal;
        }
    }
}