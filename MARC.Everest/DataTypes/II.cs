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
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes.Primitives;
using System.Collections.Generic;
using MARC.Everest.Connectors;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Identifies the scope to which an II applies
    /// </summary>
    [Serializable]
    [Structure(Name = "IdentifierScope", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    [XmlType("IdentifierScope", Namespace = "urn:hl7-org:v3")]
    public enum IdentifierScope
    {
        /// <summary>
        /// An identifier that is associated with the object due to the business practices associated with the object
        /// </summary>
        [Enumeration(Value = "BUSN")]
        [XmlEnum("BUSN")]
        BusinessIdentifier,
        /// <summary>
        /// ObjectIdentifier: An identifier associated with a particular object
        /// </summary>
        [Enumeration(Value = "OBJ")]
        [XmlEnum("OBJ")]
        ObjectIdentifier,
        /// <summary>
        /// VersionIdentifier: An identifier that references a particular object
        /// </summary>
        [Enumeration(Value = "VER")]
        [XmlEnum("VER")]
        VersionIdentifier,
        /// <summary>
        /// ViewSpecificIdentifier: An identifier for a particular snapshot of a version of the object
        /// </summary>
        [Enumeration(Value = "VW")]
        [XmlEnum("VW")]
        ViewSpecificIdentifier
    }

    /// <summary>
    /// Identifies the reliability of the II
    /// </summary>
    [XmlType("IdentifierReliability", Namespace = "urn:hl7-org:v3")]
    [Structure(Name = "IdentifierReliability", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    public enum IdentifierReliability
    {
        /// <summary>
        /// IssuedBySystem: The identifier was issued by the system responsible for constructing the instance
        /// </summary>
        [Enumeration(Value = "ISS")]
        [XmlEnum("ISS")]
        IssuedBySystem,
        /// <summary>
        /// VerifiedBySystem: The identifier was not issued by the system responsible for constructing the instance but
        /// was verified by it
        /// </summary>
        [Enumeration(Value = "VRF")]
        [XmlEnum("VRF")]
        VerifiedBySystem,
        /// <summary>
        /// UsedBySystem: The identifier was provided to the system that constructed the instance but can't be verified.
        /// </summary>
        [Enumeration(Value = "USE")]
        [XmlEnum("USE")]
        UsedBySystem
    }

    /// <summary>
    /// Identifies how the II should be used
    /// </summary>
    [XmlType("IdentifierUse", Namespace = "urn:hl7-org:v3")]
    public enum IdentifierUse
    {
        /// <summary>
        /// Business: The identifier belongs to an object that is associated with specific business practices.
        /// </summary>
        [Enumeration(Value = "BUS")]
        [XmlEnum("BUS")]
        Business,

        /// <summary>
        /// Version: The identifier is used with a specific instance of an object.
        /// </summary>
        [Enumeration(Value = "VER")]
        [XmlEnum("VER")]
        Version
    }

    /// <summary>
    /// A unique reference number that identifies a thing or object.
    /// </summary>
    /// <remarks>
    /// <para>As stated within HL7 documentation, information processing systems claiming conformance to HL7v3
    /// shall never assume receiving applications can infer the identity of an issuing authority.
    /// </para>
    /// <para>
    /// An identifier allows a system to uniquely identify not only the object, but what domain (root)
    /// that particular object was assigned within. 
    /// </para>
    /// <para>
    /// Some examples of an instance identifier are a health care identifier number where by the number
    /// itself is meaningless without knowing the domain 
    /// </para>
    /// </remarks>
    /// <example>
    /// <code title="Creating a new II" lang="cs">
    /// <![CDATA[
    ///     II instance = new II(new OID("1.1.1.2"), "123987");
    ///     		instance.AssigningAuthorityName = "Dr. Acula";
    ///             instance.Use = IdentifierUse.Business;
    ///             instance.Scope = IdentifierScope.BusinessIdentifier;
    ///             instance.Validate();
    ///        		Console.WriteLine(instance.Root + instance.Extension);
    ///     		Console.ReadKey();
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    [Structure(Name = "II", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("II", Namespace = "urn:hl7-org:v3")]
    public class II : ANY, IInstanceIdentifier, IEquatable<II>
    {

        /// <summary>
        /// Create a new instance of the II
        /// </summary>
        public II() : base() { }
        /// <summary>
        /// Create a new instance of an instance identifier with the specified root
        /// </summary>
        /// <param name="root">The root of the new II</param>
        public II(OID root) : base() { this.Root = root; }
        /// <summary>
        /// Create a new instance identifier with the specified root and extension
        /// </summary>
        /// <param name="root">The root of the new II</param>
        /// <param name="extension">The extension of the new II</param>
        public II(OID root, string extension) : base() { this.Root = root; this.Extension = extension; }
        /// <summary>
        /// Creates a new instance identifier with a GUID as the root
        /// </summary>
        public II(Guid root) : this() { this.Root = root.ToString("D").ToUpper(); }
        /// <summary>
        /// A unique identifier that guarantees the global uniqueness of the instance identifier
        /// </summary>
        [Property(Name = "root", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("root")]
        public string Root { get; set; }
        /// <summary>
        /// A character string as a unqiue identifier within the scope of the identifier root
        /// </summary>
        [Property(Name = "extension", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("extension")]
        public string Extension { get; set; }
        /// <summary>
        /// The human readable name of the identifier (ITS DT R2 only)
        /// </summary>
        [Property(Name = "identifierName", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("identifierName")]
        public string IdentifierName { get; set; }
        /// <summary>
        /// Specifies if the identifier is intended for human display and data entry
        /// </summary>
        [Property(Name = "displayable", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("displayable")]
        public bool? Displayable { get; set; }
        /// <summary>
        /// Specifies the scope in which the identifier applies to the object
        /// </summary>
        [Property(Name = "scope", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("scope")]
        public IdentifierScope? Scope { get; set; }
        /// <summary>
        /// Specifies the reliability of the instance identifier
        /// </summary>
        [Property(Name = "reliability", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("reliability")]
        public IdentifierReliability? Reliability { get; set; }
        /// <summary>
        /// The authority responsible for assigning the II
        /// </summary>
        [Property(Name = "assigningAuthorityName", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("assigningAuthorityName")]
        public string AssigningAuthorityName { get; set; }
        /// <summary>
        /// How the identifier is intended to be used
        /// </summary>
        /// <remarks>In Everest 1.0 this property is being deprecated in favour of
        /// the Scope property.</remarks>
        /// <example cref="T:System.InvalidOperationException">When the Scope property value cannot be translated to IdentifierUse</example>
        [Property(Name = "use", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional)]
        [XmlAttribute("use")]
        [Obsolete("Use is deprecated. Use Scope instead", false)]
        public IdentifierUse? Use
        {
            get
            {
                if (!this.Scope.HasValue)
                    return null;
                switch (this.Scope.Value)
                {
                    case IdentifierScope.BusinessIdentifier:
                        return IdentifierUse.Business;
                    case IdentifierScope.VersionIdentifier:
                        return IdentifierUse.Version;
                    default:
                        throw new InvalidOperationException(String.Format("Cannot determine map from value '{0}'", this.Scope.Value));
                }
            }
            set
            {
                if (value == null || !value.HasValue)
                    this.Scope = null;
                else
                    switch (value.Value)
                    {
                        case IdentifierUse.Business:
                            this.Scope = IdentifierScope.BusinessIdentifier;
                            break;
                        case IdentifierUse.Version:
                            this.Scope = IdentifierScope.VersionIdentifier;
                            break;
                    }
            }
        }

        /// <summary>
        /// Helper function, returns true if the specified II has an OID
        /// </summary>
        private static bool IsRootOid(II ii)
        {
            Regex oidRegex = new Regex("^(\\d+?\\.){1,}\\d+$");
            return oidRegex.IsMatch(ii.Root ?? "");
        }

        /// <summary>
        /// Determines if the root is a guid
        /// </summary>
        private static bool IsRootGuid(II ii)
        {
            // Bah! I know this is potentially slow
            // hack: Guid.TryParse isn't supported until .net 4
            // todo: when Everest supports .net 4.0, change to Guid.TryParse
            //try
            //{
            //    new Guid(ii.Root);
            //    return true;
            //}
            //catch { return false; }

            // JF - Optimization with regex

            Regex oidRegex = new Regex("[{]?[A-F0-9]{8}-?([A-F0-9]{4}-?){3}[A-F0-9]{12}", RegexOptions.IgnoreCase);
            return oidRegex.IsMatch(ii.Root ?? "");
        }

        /// <summary>
        /// Validate
        /// </summary>
        public override bool Validate()
        {
            if ((Root != null) ^ (NullFlavor != null))
                return IsRootOid(this) ^ IsRootGuid(this);
            return false;
        }

        /// <summary>
        /// Validate the instance identifier is valid, returning the detected issues
        /// </summary>
        public override System.Collections.Generic.IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>( base.ValidateEx());

            if (!((Root != null) ^ (NullFlavor != null)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "II", "Root and NullFlavor must be used exclusively", null));
            else if (!(IsRootOid(this) ^ IsRootGuid(this)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "II", "Root must be a GUID or OID in form x.x.x.x", null));
            return retVal;
        }

        /// <summary>
        /// Flavor validator for Token, only needs a root
        /// </summary>
        [Flavor(Name = "II.TOKEN")]
        [Flavor(Name = "TOKEN")]
        public static bool IsValidTokenFlavor(II ii)
        {
            // Try to make a guid
            return ii.Displayable == null &&
                ii.Use == null &&
                (ii.Root != null && IsRootGuid(ii) && ii.Extension == null) ^ (ii.NullFlavor != null);
        }

        /// <summary>
        /// Flavor validator for Public
        /// </summary>
        /// <param name="ii">The instance to validate</param>
        [Flavor(Name = "II.PUBLIC")]
        [Flavor(Name = "PUBLIC")]
        public static bool IsValidPublicFlavor(II ii)
        {
            return ii.Displayable == true &&
                ii.Use == IdentifierUse.Business &&
                (ii.Root != null && ii.Extension != null && IsRootOid(ii)) ^ (ii.NullFlavor != null);
        }

        /// <summary>
        /// Flavor validator for OID
        /// </summary>
        [Flavor(Name = "OID")]
        [Flavor(Name = "II.OID")]
        public static bool IsValidOidFlavor(II ii)
        {
            return (ii.Root != null && IsRootOid(ii) && ii.Extension == null) ^ (ii.NullFlavor != null);
        }

        /// <summary>
        /// BUS flavor validator
        /// </summary>
        [Flavor(Name = "BUS")]
        [Flavor(Name = "II.BUS")]
        public static bool IsValidBusFlavor(II ii)
        {
            return ii.Displayable == null &&
                ii.Use == IdentifierUse.Business &&
                (ii.Root != null && ((IsRootOid(ii) && ii.Extension != null) || (IsRootGuid(ii) && ii.Extension == null))) ^ (ii.NullFlavor != null);
        }

        /// <summary>
        /// VER flavor validator
        /// </summary>
        [Flavor(Name = "VER")]
        [Flavor(Name = "II.VER")]
        public static bool IsValidVerFlavor(II ii)
        {
            return ii.Displayable == null &&
                ii.Use == IdentifierUse.Version &&
                (ii.Root != null && ii.Extension == null && IsRootGuid(ii)) ^ (ii.NullFlavor != null);
        }

        /// <summary>
        /// Bus and Ver validator
        /// </summary>
        /// <remarks>Is this just II.BUS and II.VER? If so that doesn't make sense as the use for each of these is specified
        /// as BUS for BUS and VER for VER ..</remarks>
        [Flavor(Name = "BUS_AND_VER")]
        [Flavor(Name = "II.BUS_AND_VER")]
        public static bool IsValidBusAndVerFlavor(II ii)
        {
            return IsValidVerFlavor(ii) && IsValidBusFlavor(ii);
        }

        /// <summary>
        /// Default comparator for II
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static readonly Comparison<II> Comparator = delegate(II a, II b)
        {
            // If null, the null flavors are the same, 
            // otherwise the root and extension must be equal
            if ((a.Root == b.Root && a.Extension == b.Extension) ^ (a.NullFlavor == b.NullFlavor && a.NullFlavor != null))
                return 0;
            return 1;
        };

        #region Operators

        /// <summary>
        /// Allows the developer to cast a <see cref="T:System.Guid"/> instance into an II instance
        /// instance
        /// </summary>
        /// <param name="g">The guid that is being cast</param>
        /// <returns>The created II instance</returns>
        public static implicit operator II(Guid g)
        {
            return new II(g);
        }

        /// <summary>
        /// Allows the developer to cast a <see cref="T:MARC.Everest.DataTypes.Primitives.OID"/> to an II instance
        /// </summary>
        /// <param name="o">The OID to cast to an II</param>
        public static implicit operator II(OID o)
        {
            return new II(o);
        }
        #endregion

        #region Creator Methods

        /// <summary>
        /// Creates an II.TOKEN from the specified <paramref name="root"/>
        /// </summary>
        /// <param name="root">The GUI to create as the token's value</param>
        /// <returns>The instantiated II.TOKEN</returns>
        public static II CreateToken(Guid root)
        {
            II token = new II(root);
            token.Flavor = "II.TOKEN";
            return token;
        }

        /// <summary>
        /// Creates an II.BUS with the specified <paramref name="root"/>
        /// </summary>
        /// <param name="root">The GUID representing the root of the II.BUS</param>
        public static II CreateBus(Guid root)
        {
            II bus = new II(root);
            bus.Flavor = "II.BUS";
            return bus;
        }


        /// <summary>
        /// Creates an II.PUBLIC with the specified <paramref name="root"/> and <paramref name="extension"/>
        /// </summary>
        /// <param name="root">The OID representing the root of the II.PUBLIC</param>
        /// <param name="extension">The extension of the II.PUBLIC</param>
        public static II CreatePublic(OID root, string extension)
        {
            II pub = new II(root, extension);
            pub.Flavor = "II.PUBLIC";
            pub.Displayable = true;
            pub.Use = IdentifierUse.Business;
            return pub;
        }

        /// <summary>
        /// Creates an II.BUS from the specified <paramref name="root"/> and <paramref name="extension"/>
        /// </summary>
        /// <param name="root">The root of the II.BUS</param>
        /// <param name="extension">The extension of the II.BUS</param>
        /// <remarks>Extension can only be present when the root is an OID</remarks>
        public static II CreateBus(OID root, string extension)
        {
            II bus = new II(root, extension);
            bus.Flavor = "II.BUS";
            return bus;
        }
        #endregion


        #region IEquatable<II> Members

        /// <summary>
        /// Determine if this II equals another instance of an II
        /// </summary>
        public bool Equals(II other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((ANY)other) &&
                    this.AssigningAuthorityName == other.AssigningAuthorityName &&
                    this.Displayable == other.Displayable &&
                    this.Extension == other.Extension &&
                    this.IdentifierName == other.IdentifierName &&
                    this.Reliability == other.Reliability &&
                    this.Root == other.Root &&
                    this.Scope == other.Scope;
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is II)
                return Equals(obj as II);
            return base.Equals(obj);
        }

        #endregion

        #region ISemanticEquatable<II> Members

        /// <summary>
        /// Semantic equals
        /// </summary>
        public override BL SemanticEquals(IAny other)
        {
            var baseSem = base.SemanticEquals(other);
            if (!(bool)baseSem)
                return baseSem;

            var otherII = other as II;
            return otherII != null && !other.IsNull && !this.IsNull && otherII.Root == this.Root &&
                (otherII.Extension == this.Extension);
        }

        #endregion
    }
}
