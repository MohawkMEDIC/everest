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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Identifies a property or field within a class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
    public class Property : ClassContent
    {

        /// <summary>
        /// Override the clone method
        /// </summary>
        public override object Clone()
        {
            object retVal = base.Clone();
            if (this.AlternateTraversalNames != null)
                (retVal as Property).AlternateTraversalNames = new List<AlternateTraversalData>(this.AlternateTraversalNames);
            return retVal;
        }

        /// <summary>
        /// Alternate traversal data
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public struct AlternateTraversalData
        {
            public string TraversalName { get; set; }
            public TypeReference CaseWhen { get; set; }
            public Interaction InteractionOwner { get; set; }
        }

        /// <summary>
        /// Determines the type of property
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
        public enum PropertyTypes
        {
            Structural = 1,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NonStructural")]
            NonStructural = 2,
            TraversableAssociation = 4,
            NonTraversableAssociation = 8
        }

        /// <summary>
        /// Instructions on how the initialization should be performed
        /// </summary>
        //TODO: Change Spelling of Initializor to Initializer
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Initializor"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
        public enum InitializorTypes
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ITS")]
            ITS,
            Realm,
            ReferencedAttributes,
            DefaultValue
        }

        private PropertyTypes ptype;
        private string fixedValue;
        private string defaultValue;
        private Enumeration supplierDomain;
        private List<string> validation;
        private string updateMode;
        private StateMachine behavior;
        private string maxLength;
        private string minLength;

        /// <summary>
        /// Gets or sets the maximum length of the string for a datatype
        /// </summary>
        public string MaxLength { get { return this.maxLength; } set { this.maxLength = value; } }
        /// <summary>
        /// Gets or sets the minimum length of the string for a property
        /// </summary>
        public string MinLength { get { return this.minLength; } set { this.minLength = value; } }
        /// <summary>
        /// Get or sets the alternate traversal names for this property when the property appears within a choice
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<AlternateTraversalData> AlternateTraversalNames { get; set; }

        /// <summary>
        /// Update modes allowed
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<string> AllowedUpdateModes { get; set; }

        /// <summary>
        /// Identifies how the default value should be calculated or derived from
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Initializor")]
        public InitializorTypes Initializor { get; set; }

        /// <summary>
        /// Get or set the state machine that dictates the values on this property
        /// </summary>
        public StateMachine StateMachine
        {
            get { return behavior; }
            set { behavior = value; }
        }
	
        /// <summary>
        /// Update mode
        /// </summary>
        public string UpdateMode
        {
            get { return updateMode; }
            set { updateMode = value; }
        }


        /// <summary>
        /// Get or set the validation rules
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<string> Validation
        {
            get { return validation; }
            set { validation = value; }
        }
	
        /// <summary>
        /// Identifies the type of coding strength
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum CodingStrengthKind
        {
            /// <summary>
            /// If not null, the element must be coded and must be drawn from the set of codes determined by the 
            /// domain and realm specified
            /// </summary>
            CodedNoExtensions,
            /// <summary>
            /// If not null the element may be coded if there is an appropriate code available.
            /// </summary>
            CodedWithExtensions
        }

        private CodingStrengthKind? supplierStrength;
        private TypeReference type;

        /// <summary>
        /// Identifies the datatype and flavor that should be used for this element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public TypeReference Type
        {
            get { return type; }
            set { type = value; }
        }
     
        /// <summary>
        /// Identifies the strength of the supplierDomain over this element
        /// </summary>
        public CodingStrengthKind? SupplierStrength
        {
            get { return supplierStrength; }
            set { supplierStrength = value; }
        }

        /// <summary>
        /// Identifies the code domain that supplies this element with values
        /// </summary>
        public Enumeration SupplierDomain
        {
            get { return supplierDomain; }
            set {
                supplierDomain = value; 
            }
        }

        /// <summary>
        /// Identifies the initialization values
        /// </summary>
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
	
        /// <summary>
        /// Identifies the fixed value of this element
        /// </summary>
        public string FixedValue
        {
            get { return fixedValue; }
            set { fixedValue = value; }
        }
	
        /// <summary>
        /// Identifies if the element is structural
        /// </summary>
        public PropertyTypes PropertyType
        {
            get { return ptype; }
            set { ptype = value; }
        }

        /// <summary>
        /// Represent this property as a string
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])")]
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} {1} property {2} {3}{4}\r\n\t [{5}..{6}]{7}{8};",
                Conformance, PropertyType,
                Type.ToString(), Name,
                BusinessName == null ? "" : " alias '" + BusinessName + "'",
                MinOccurs, MaxOccurs, 
                fixedValue != null ? "\r\n\t fixed to '" + fixedValue + "'" : "",
                defaultValue != null ? "\r\n\t default to '" + defaultValue + "'" : "");

            return sb.ToString();
        }

        /// <summary>
        /// Add a traversal name so that it may be rendered when the class property
        /// type is of this type 
        /// </summary>
        /// <param name="TraversalName">The name of the traversal</param>
        /// <param name="CaseWhen">The type to perform this for</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Traversal"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Case")]
        public void AddTraversalName(string TraversalName, TypeReference CaseWhen)
        {
            if (AlternateTraversalNames == null) AlternateTraversalNames = new List<AlternateTraversalData>();
            AlternateTraversalNames.Add(new AlternateTraversalData() { CaseWhen = CaseWhen, TraversalName = TraversalName}); 
        }

        /// <summary>
        /// Add a traversal name so that it may be rendered when the class property type of this type 
        /// </summary>
        /// <param name="TraversalName">The name of the traversal</param>
        /// <param name="CaseWhen">The type to perform this traversal for</param>
        /// <param name="InteractionOwner">The interaction where this traversal is valid</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Traversal"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Interaction"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Case")]
        public void AddTraversalName(string TraversalName, TypeReference CaseWhen, Interaction InteractionOwner)
        {
            if (CaseWhen != null && CaseWhen.MemberOf == null)
                CaseWhen.MemberOf = this.MemberOf ?? this.Container.MemberOf;
            if (AlternateTraversalNames == null) AlternateTraversalNames = new List<AlternateTraversalData>();
            AlternateTraversalNames.Add(new AlternateTraversalData() { CaseWhen = CaseWhen, TraversalName = TraversalName, InteractionOwner = InteractionOwner }); 
        }

    }
}
