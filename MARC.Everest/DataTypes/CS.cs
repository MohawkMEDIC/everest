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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Attributes;
using System.ComponentModel;
using MARC.Everest.Connectors;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Primitives;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// A Coded Simple value that is not bound to a value set
    /// The coded simple data type is used to represent codified data in its simplest form where only the code is not predetermined. 
    /// </summary>
    /// <remarks>
    /// Is equivalent to a CS{String}
    /// </remarks>
    //[Serializable]
    //[XmlType("CS", Namespace = "urn:hl7-org:v3")]
    //[Obsolete("CS is obsolete, consider using CS<String>", true)]
    //public class CS : CS<string>, ICodedSimple<string>
    //{
    //    /// <summary>
    //    /// Create a new instance of CS
    //    /// </summary>
    //    public CS() : base() { }

    //    /// <summary>
    //    /// Create a new instance of CS with the specified code
    //    /// </summary>
    //    /// <param name="code">The initial code</param>
    //    public CS(string code) : base(code) {  }

    //    /// <summary>
    //    /// Converts a <see cref="string"/> to a <see cref="CS"/>
    //    /// </summary>
    //    /// <param name="s">string to convert</param>
    //    /// <returns>Converted CS</returns>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
    //    public static implicit operator CS(string s)
    //    {
    //        CS retVal = new CS();
    //        retVal.Code = s;
    //        return retVal;
    //    }

    //    /// <summary>
    //    /// Converts a <see cref="CS"/> to a <see cref="string"/>
    //    /// </summary>
    //    /// <param name="cs">CS to convert</param>
    //    /// <returns>Converted string</returns>
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
    //    public static implicit operator string(CS cs)
    //    {
    //        return cs.Code;
    //    }

    //}

    /// <summary>
    /// Coded data in its simplest form where only the code is not predetermined. 
    /// </summary>
    /// <remarks>
    /// The code system and code system version are implied and fixed by the context in which the CS value occurs.
    /// </remarks>
    /// <example>
    /// <code lang="cs">
    /// <![CDATA[
    ///    // Creating an instance
    ///    PRLO_IN202010CA instance = new PRLO_IN202010CA();
    ///    // Using direct assignment
    ///    instance.ResponseModeCode = new CS<ResponseMode>(ResponseMode.Queue);
    ///    // Using properties
    ///    instance.ResponseModeCode = new CS<ResponseMode>();
    ///    instance.ResponseModeCode.Code = ResponseMode.Queue;
    /// ]]>
    /// </code>
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2218:OverrideGetHashCodeOnOverridingEquals"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix"), Serializable]
    [Structure(Name = "CS", StructureType = StructureAttribute.StructureAttributeType.DataType, DefaultTemplateType = typeof(String))]
    [DefaultProperty("Code")]
    public class CS<T> : ANY, ICodedSimple<T>, IEquatable<CS<T>>, IEquatable<T>
    {

        /// <summary>
        /// The code that is being used in this coded simple
        /// </summary>
        private CodeValue<T> m_code;

        /// <summary>
        /// Create a new instance of CS
        /// </summary>
        public CS() : base () { }

        /// <summary>
        /// Create a new instance of CS with the specified code
        /// </summary>
        /// <param name="code">The initial code</param>
        public CS(T code) : base() { this.Code = code; }

        /// <summary>
        /// The plain code symbol defined by the code system
        /// </summary>
        /// <remarks>The type of this property is a CodeValue&lt;<typeparam name="T"/>. This allows
        /// codes outside of the bound code system to be specified. 
        /// <example>
        ///     <code title="Using codes outside of bound code system" lang="cs">
        /// <![CDATA[
        ///    CS<AdministrativeGender> adminCode = Util.Convert<CS<AdministrativeGender>>("MAN");
        /// ]]>
        ///     </code>
        /// </example>
        /// </remarks>
        [Property(Name = "code", Conformance = PropertyAttribute.AttributeConformanceType.Optional, PropertyType = PropertyAttribute.AttributeAttributeType.Structural)]
        [XmlAttribute("code")]
        public virtual CodeValue<T> Code {
            get
            {
                return m_code;
            }
            set
            {
                this.m_code = value;
            }
        }
        
        /// <summary>
        /// Represent this as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return NullFlavor != null ? "" : Code == null ? "" : Code.ToString();
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <remarks>
        /// A coded simple is valid if:
        /// <list type="bullet">
        ///     <item>NullFlavor is specified, XOR</item>
        ///     <item>
        ///         <list type="bullet">
        ///             <item>Code is specified, AND</item>
        ///             <item>If Alternate (not in bound domain) code is specified, a CodeSystem is specified</item>
        ///         </list>
        ///     </item>
        /// </list>
        /// </remarks>
        public override bool Validate()
        {
            // Either the code is null and the code is strongly typed, or the code is not strongly typed and 
            // the code has a code system
            // Or the null flavor is set
            if (this is CV<T>) // Special case for non CS
                return base.Validate();
            else
                return (Code != null && !Code.IsAlternateCodeSpecified) ^ (NullFlavor != null) && base.Validate();
        }

        /// <summary>
        /// Validate the CS is valid, returning the detected issues
        /// </summary>
        public override IEnumerable<IResultDetail> ValidateEx()
        {
            var retVal = new List<IResultDetail>(base.ValidateEx());
            if (this is CV<T>)
                return retVal;
            else if (!((this.Code == null) ^ (this.NullFlavor == null)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CS", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            else if (this.Code.IsAlternateCodeSpecified)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "CS", string.Format("Code must be drawn from '{0}'", typeof(T).Name), null));
            return retVal;
        }

        #region Operators

        /// <summary>
        /// Converts a <see cref="CS"/> to a <see cref="CS"/>
        /// </summary>
        /// <param name="cs">CS to convert</param>
        /// <returns>Converted CS`1</returns>
        /// <remarks>Is a parse function as the string must be parsed by util</remarks>
        internal static CS<T> Parse(CS<String> cs)
        {
            CS<T> retVal = new CS<T>();
            // Parse from wire format into this format
            retVal.Code = CodeValue<T>.Parse(cs.Code);
            // Copy other members
            //retVal.CodeSystem = cs.CodeSystem;
            //retVal.CodeSystemName = cs.CodeSystemName;
            //retVal.CodeSystemVersion = cs.CodeSystemVersion;
            retVal.Flavor = cs.Flavor;
            retVal.NullFlavor = cs.NullFlavor == null ? null : cs.NullFlavor.Clone() as CS<NullFlavor>;
            retVal.UpdateMode = cs.UpdateMode == null ? null : cs.UpdateMode.Clone() as CS<UpdateMode>;
            retVal.ControlActRoot = cs.ControlActRoot;
            retVal.ControlActExt = cs.ControlActExt;
            retVal.ValidTimeHigh = cs.ValidTimeHigh;
            retVal.ValidTimeLow = cs.ValidTimeLow;
            retVal.NullFlavor = cs.NullFlavor;
            return retVal;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="CS"/>
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>Converted CS`1</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        internal static CS<T> Parse(string s)
        {
            // FIX: EV-881
            return Parse(new CS<String>(s));
        }

        /// <summary>
        /// Converts a <see cref="CS"/> to a <typeparamref name="T"/>
        /// </summary>
        /// <param name="o">CS`1 to convert</param>
        /// <returns>Converted T</returns>
        /// <exception cref="T:System.InvalidCastException">When the code value is null</exception>
        /// <exception cref="T:System.InvalidOperationException">When the code is bound to an enumerated vocabulary set and the value lies outside of the acceptable range (example: MMALE for AdministrativeGender)</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator T(CS<T> o)
        {
            if ((o == null || o.Code == null) && typeof(T).IsValueType)
                throw new InvalidCastException("Nullable type must have a value to be cast");
            else if (o.Code.IsAlternateCodeSpecified)
                throw new InvalidOperationException(String.Format("Cannot cast to '{0}' because the assigned value '{1}' lies outside the domain specified",
                    typeof(T).FullName, o.Code));
            return o.Code;
        }

        /// <summary>
        /// Converts a <typeparamref name="T"/> to a <see cref="CS"/>
        /// </summary>
        /// <param name="o">T to convert</param>
        /// <returns>Converted CS</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        public static implicit operator CS<T>(T o)
        {
            CS<T> retVal = new CS<T>();
            retVal.Code = o;
            return retVal;
        }

        #endregion
        //DOC: Documentation Required
        /// <summary>
        /// Hides the default implementation of GetType
        /// </summary>
        /// <remarks>This is used to force formatters to "cast"
        /// data from the bound data type <typeparamref name="T"/>
        /// to a CS&lt;T></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public new Type GetType()
        {
            return typeof(T);
        }

        /// <summary>
        /// Get the code value as a generic object
        /// </summary>
        /// <remarks>
        /// Used primarily for formatters
        /// </remarks>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object CodeValue
        {
            get { return Code; }
            set {
                if (value is CodeValue<T>)
                    this.Code = value as CodeValue<T>;
                else
                    Code = (CodeValue<T>)((T)value);  
            }
        }

        /// <summary>
        /// Default comparator for CS
        /// </summary>
        /// <remarks>This should be used when creating sets of CS</remarks>
        /// <example>
        /// <code title="Set of CS" lang="cs">
        /// <![CDATA[
        /// SET<CS<String>> csSet = new SET<CS<String>>(CS<String>.Comparator);
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static Comparison<CS<T>> Comparator = delegate(CS<T> a, CS<T> b)
        {
            // If null, the null flavors are the same, 
            // otherwise the code, codesystem and version of codesystem must be equal
            if ((a.Code == b.Code) ^ (a.NullFlavor == b.NullFlavor && a.NullFlavor != null))
                return 0;
            return 1;
        };


        #region IEquatable<CS<T>> Members

        /// <summary>
        /// Determine if this CS is equal to another CS
        /// </summary>
        public bool Equals(CS<T> other)
        {
            bool result = false;
            if (other != null)
                result = base.Equals((ANY)other) &&
                    (this.Code != null ? this.Code.Equals(other.Code) : this.Code == null);
                    
            return result;
        }

        /// <summary>
        /// Override of base equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CS<T>)
                return Equals(obj as CS<T>);
            
            return base.Equals(obj);
        }

        #endregion

        #region ISemanticEquatable<CS<T>> Members

        /// <summary>
        /// Determines semantic equality of this instance with another
        /// </summary>
        public override BL SemanticEquals(IAny other)
        {
            if (other == null)
                return null;
            else if (this.IsNull && other.IsNull)
                return new BL() { NullFlavor = NullFlavorUtil.CommonAncestorWith(this.NullFlavor, other.NullFlavor) };
            else if (this.IsNull ^ other.IsNull)
                return new BL() { NullFlavor = DataTypes.NullFlavor.NotApplicable };
            else if (!(other is ICodedSimple))
                return false;

            return !this.IsNull && !(other as ANY).IsNull && this.Code != null && (other as ICodedSimple).CodeValue != null && Util.ToWireFormat(this.Code).Equals(Util.ToWireFormat((other as ICodedSimple).CodeValue));
        }

        #endregion

        #region IEquatable<T> Members

        /// <summary>
        /// Determine if this equals
        /// </summary>
        public bool Equals(T other)
        {
            return other.Equals((T)this.Code);
        }

        #endregion
    }
}
