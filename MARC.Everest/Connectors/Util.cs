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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using System.Reflection;
using MARC.Everest.Attributes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.DataTypes.Interfaces;
using System.ComponentModel;

namespace MARC.Everest.Connectors
{

    /// <summary>
    /// A delegate that is used by <see cref="T:Util.CreateXSIType"/> that 
    /// is used to override the manner in which XSI:TYPEs are created
    /// </summary>
    public delegate string CreateXSITypeNameDelegate(Type type);

    /// <summary>
    /// Utility class for formatting data
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Util")]
    public static class Util
    {

        /// <summary>
        /// Maps of enumerations and validators
        /// </summary>
        private static Dictionary<string, string> s_enumerationMaps = new Dictionary<string, string>();
        /// <summary>
        /// Flavor validation
        /// </summary>
        private static Dictionary<string, MethodInfo> s_flavorValidation = new Dictionary<string, MethodInfo>();
        /// <summary>
        /// Maps from wire format to real format
        /// Key - string in the format {FROM}>{TO}
        /// Value - MethodInfo of the method that will perform the operation to convert
        /// </summary>
        private static Dictionary<string, MethodInfo> s_wireMaps = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// Static constructor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
        static Util()
        {

            // Scan all types.. populate dictionaries
            foreach(Type t in typeof(II).Assembly.GetTypes())
                if (t.IsClass && t.GetInterface("MARC.Everest.Interfaces.IGraphable", true) != null)
                    foreach(MethodInfo mi in  t.GetMethods())
                    {
                        object[] fa = mi.GetCustomAttributes(typeof(FlavorAttribute), true);
                        lock (s_flavorValidation)
                            foreach(var validator in fa)
                                s_flavorValidation.Add(string.Format("{0}.{1}", t.FullName, (validator as FlavorAttribute).Name.ToUpper()), mi);
                    }
        }

        /// <summary>
        /// Parse maps from <paramref name="t"/>
        /// </summary>
        /// <param name="t"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        private static void ParseMaps(Type t)
        {
                
            foreach (FieldInfo fi in t.GetFields()) // Foreach 
            {
                // Skip processing
                string kName = string.Format("{0}.{1}", t.FullName, fi.Name);
                    if (s_enumerationMaps.ContainsKey(kName))
                        continue;

                    object[] ea = fi.GetCustomAttributes(typeof(EnumerationAttribute), true);
                    if (ea.Length != 0) // Lock and add
                    {
                        lock (s_enumerationMaps)
                        {
                            if (!s_enumerationMaps.ContainsKey(kName))
                                s_enumerationMaps.Add(kName, (ea[0] as EnumerationAttribute).Value);
                            if (!s_enumerationMaps.ContainsKey(string.Format("{0}.{1}", t.FullName, (ea[0] as EnumerationAttribute).Value)))
                                s_enumerationMaps.Add(string.Format("{0}.{1}", t.FullName, (ea[0] as EnumerationAttribute).Value), fi.Name.ToString());
                        }
                    }
                    else
                        lock (s_enumerationMaps)
                            if (!s_enumerationMaps.ContainsKey(kName))
                                s_enumerationMaps.Add(string.Format("{0}.{1}", t.FullName, fi.Name.ToString()), fi.Name.ToString());
                }
        }

        /// <summary>
        /// Convert the object <paramref name="value"/> to the type specified by <typeparam name="T"/> if possible. This is used to
        /// parse objects from a wire format to a friendly format
        /// </summary>
        /// <remarks>
        /// <para>This utility method is usually used when comparing string data to instance classes. It can also be
        /// used to develop custom formatters.</para>
        /// <example>
        /// <code lang="cs" title="Converting a string to a list">
        /// <![CDATA[
        ///     string adUseString = "PHYS BAD PUB";
        ///     SET<CS<PostalAddressUse>> instance = Util.Convert<SET<CS<PostalAddressUse>>>(adUseString);
        ///     
        ///     // Results in a SET<CS<PostalAddressUse>> with three items:
        ///     // 1. CS<PostalAddressUse> = PostalAddressUse.PhysicalVisit
        ///     // 2. CS<PostalAddressUse> = PostalAddressUse.BadAddress
        ///     // 3. CS<PostalAddressUse> = PostalAddressUse.Public
        /// ]]>
        /// </code>
        /// </example>
        /// </remarks>
        /// <exception cref="T:MARC.Everest.Exceptions.VocabularyException">If <paramref name="value"/> cannot be converted to <typeparamref name="T"/> because <typeparamref name="T"/> points to an enumeration
        /// and there is no known literal in <typeparamref name="T"/> that represents <paramref name="value"/></exception>
        /// <exception cref="T:MARC.Everest.Exceptions.FormatterException">When no method to convert <paramref name="object"/> to <typeparamref name="T"/> could be found</exception>
        public static T Convert<T>(object value)
        {
            object retVal = null;
            if (!TryFromWireFormat(value, typeof(T), out retVal))
                throw new FormatterException(String.Format("Can't find valid cast to from '{0}' to '{1}'", value.GetType(), typeof(T)));
            return (T)retVal;
        }

        /// <summary>
        /// Convert from wire format
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dest"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Convert<T>", false)]
        public static object FromWireFormat(object value, Type destType)
        {

            object retVal = null;
            if(!TryFromWireFormat(value, destType, out retVal))
                throw new FormatterException(String.Format("Can't find valid cast to from '{0}' to '{1}'", value.GetType(), destType));
            return retVal;

        }

        /// <summary>
        /// Find the converter for the types specified
        /// </summary>
        /// <param name="scanType">The type to scan in</param>
        /// <param name="sourceType">The source type</param>
        /// <param name="destType">The destination type</param>
        /// <returns></returns>
        internal static MethodInfo FindConverter(Type scanType, Type sourceType, Type destType)
        {
            lock(scanType)
                foreach (MethodInfo mi in scanType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    if (mi.GetParameters().Length == 1 && 
                        (mi.ReturnType.IsSubclassOf(destType) || destType == mi.ReturnType) &&
                        mi.GetParameters()[0].ParameterType.FullName == sourceType.FullName)
                        return mi;
            return null;
        }

        /// <summary>
        /// Map an enumeration value from the literal to the wire format
        /// </summary>
        /// <param name="instanceValue">The value of the literal</param>
        /// <returns>A string representing the enumeration's literal value on the wire</returns>
        /// <remarks>
        /// Can be used to represent any data type as it's wire level rendering string (as it would appear if it were a structural attribute)
        /// <example>
        /// <code lang="cs" title="Converting a set to a string">
        /// <![CDATA[
        ///  SET<CS<PostalAddressUse>> instance = new SET<CS<PostalAddressUse>>() { 
        ///      PostalAddressUse.PhysicalVisit,
        ///      PostalAddressUse.BadAddress,
        ///      PostalAddressUse.Direct
        ///  };
        ///  string output = MARC.Everest.Connectors.Util.ToWireFormat(instance);
        ///     
        ///  // Results in a string containing:
        ///  // "PHYS BAD DIR"
        /// ]]>
        /// </code>
        /// </example>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Enumeration"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public static string ToWireFormat(object instanceValue)
        {

            // Null?
            if (instanceValue == null)
                return null;

            // Basic formatting 
            if (instanceValue is bool || instanceValue is bool?)
                return instanceValue.ToString().ToLower();

            // Key format
            Type realType = instanceValue.GetType().IsEnum ?
                instanceValue.GetType() :
                instanceValue.GetType().IsGenericType ?
                instanceValue.GetType().GetGenericArguments()[0] : // Coded value
                typeof(String);
            string enumName = realType.FullName;
            
            // Get the enum value
            if(instanceValue.GetType().GetProperty("Code") != null)
                instanceValue = instanceValue.GetType().GetProperty("Code").GetValue(instanceValue, null);

            string kFormat = string.Format("{0}.{1}", enumName, instanceValue);

            if (!s_enumerationMaps.ContainsKey(kFormat))
                ParseMaps(realType);

            if (s_enumerationMaps.ContainsKey(kFormat)) // Return enumeration map
                return s_enumerationMaps[kFormat];

            return String.Format("{0}", instanceValue);
        }

        /// <summary>
        /// Validate <paramref name="instance"/> to the flavor <paramref name="flavor"/>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "p"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#")]
        public static bool? ValidateFlavor(string flavor, ANY instance, out IResultDetail[] details)
        {
            // Does this flavor exist?
            string kFormat = string.Format("{0}.{1}", instance.GetType().FullName, flavor.ToUpper());
            
            // JF - Makes this much faster
            MethodInfo validatorMi = null;
            if (s_flavorValidation.TryGetValue(kFormat, out validatorMi))
            {
                details = new IResultDetail[0];
                return (bool)validatorMi.Invoke(null, new object[] { instance });
            }

            details = new IResultDetail[0];

            // Add warning
            if(flavor != instance.GetType().Name)
                details = new IResultDetail[] { new ResultDetail(ResultDetailType.Warning, string.Format("Can't find flavor '{0}'", flavor), null, null) };

            return null;
        }

        /// <summary>
        /// Parse XSI Type Name
        /// </summary>
        /// <param name="xsiType"></param>
        /// <returns></returns>
        public static Type ParseXSITypeName(string xsiType)
        {
            // Type tokens for the 
            Queue<String> typeNames = new Queue<string>(xsiType.Split('_'));
            var t = ParseXSITypeNameInternal(typeNames);
            if (typeNames.Count > 0)
                throw new InvalidOperationException(String.Format("Generic parameter supplied to a non-generic type '{0}' used to construct '{1}'", xsiType, t.FullName));
            return t;
        }

        /// <summary>
        /// Parse XSI Type names
        /// </summary>
        private static Type ParseXSITypeNameInternal(Queue<string> typeNames)
        {
            string typeName = typeNames.Dequeue();
            
            // Fix: EV-876
            Type cType = Array.Find(typeof(Util).Assembly.GetTypes(), delegate(Type a)
            {
                var structureAtt = a.GetCustomAttributes(typeof(StructureAttribute), false);
                if (structureAtt.Length == 0)
                    return false;

                // Type maps
                var typeMapAtt = a.GetCustomAttributes(typeof(TypeMapAttribute), false);
                bool result = false;
                foreach (TypeMapAttribute tma in typeMapAtt)
                    result |= (tma.Name == typeName) && (String.IsNullOrEmpty(tma.ArgumentType) ^ (tma.ArgumentType == typeNames.Peek()));
                return result || (structureAtt[0] as StructureAttribute).Name == typeName;
            });

            // Determine if the type is generic in nature?
            if (cType.IsGenericTypeDefinition) // Yes
            {
                List<Type> genericParameters = new List<Type>();
                // Construct the parameter types 
                if (typeNames.Count == 0) // use default parameters
                {
                    var structureAttributes = cType.GetCustomAttributes(typeof(StructureAttribute), false);
                    if (structureAttributes.Length > 0)
                        for (int i = 0; i < cType.GetGenericArguments().Length; i++)
                            genericParameters.Add((structureAttributes[0] as StructureAttribute).DefaultTemplateType);
                    else
                        throw new InvalidOperationException("Should not be here, could not construct a data type with the specified parameters");
                }
                else // Recurse
                {
                    for (int i = 0; i < cType.GetGenericArguments().Length; i++)
                        genericParameters.Add(ParseXSITypeNameInternal(typeNames));
                }

                // Gen Type
                Type genType = cType.MakeGenericType(genericParameters.ToArray());
                return genType;
            }
            else
            {
                
                // Fix: EV-876
                object[] typeMapAttribute = cType.GetCustomAttributes(typeof(TypeMapAttribute), false);
                if (typeMapAttribute.Length > 0)
                {
                    var tma = Array.Find(typeMapAttribute, o => (o as TypeMapAttribute).Name == typeName) as TypeMapAttribute;
                    if (tma != null && !String.IsNullOrEmpty(tma.ArgumentType))
                    {
                        var genType = typeNames.Dequeue();
                        if (genType != tma.ArgumentType)
                            throw new InvalidOperationException(String.Format("Argument type to type map is incorrect, should not be here. Expected '{0}' found '{1}'", genType, tma.ArgumentType));
                    }
                }

                return cType;
            }
        }

        /// <summary>
        /// Create an XSI Type name
        /// </summary>
        public static string CreateXSITypeName(Type compType)
        {
            return CreateXSITypeName(compType, Util.CreateXSITypeName);
        }

        /// <summary>
        /// Creates an XSI type name with the specified <paramref name="subTypeCreator"/> used
        /// to create nested generic types
        /// </summary>
        public static string CreateXSITypeName(Type compType, CreateXSITypeNameDelegate subTypeCreator)
        {
            StringBuilder xsiTypeName = new StringBuilder();
            if (compType.IsGenericType)
            {
                Type[] genType = compType.GetGenericArguments();
                compType = compType.GetGenericTypeDefinition();
                object[] stAtt = compType.GetCustomAttributes(typeof(StructureAttribute), false);
                if (stAtt.Length > 0)
                    xsiTypeName.AppendFormat("{0}_", (stAtt[0] as StructureAttribute).Name);
                foreach (Type t in genType)
                {
                    stAtt = t.GetCustomAttributes(typeof(StructureAttribute), false);
                    if (stAtt.Length > 0 && (stAtt[0] as StructureAttribute).StructureType == StructureAttribute.StructureAttributeType.DataType)
                    {
                        if (t.IsGenericType)
                            xsiTypeName.AppendFormat("{0}_", subTypeCreator(t));
                        else
                            xsiTypeName.AppendFormat("{0}_", (stAtt[0] as StructureAttribute).Name);

                    }
                }
                xsiTypeName = xsiTypeName.Remove(xsiTypeName.Length - 1, 1);
            }
            else
            {
                object[] stAtt = compType.GetCustomAttributes(typeof(StructureAttribute), false);
                if (stAtt.Length > 0)
                    xsiTypeName.Append((stAtt[0] as StructureAttribute).Name);
            }
            return xsiTypeName.ToString();
        }

        /// <summary>
        /// Attempt casting <paramref name="value"/> to <paramref name="destType"/> placing the result 
        /// in <paramref name="result"/>
        /// </summary>
        public static bool TryFromWireFormat(object value, Type destType, out object result)
        {
            // The type represents a wrapper for an enumeration
            Type m_destType = destType;

            bool requiresExplicitCastCall = false;
            if (value == null)
            {
                result = null;
                return true;
            }
            else if (m_destType.IsGenericType && !value.GetType().IsEnum)
            {
                m_destType = m_destType.GetGenericArguments()[0];
                requiresExplicitCastCall = true;
            }

            // Is there a cast?
            if (destType.IsAssignableFrom(value.GetType())) //  (m_destType.IsAssignableFrom(value.GetType())) // Same type
            {
                result = value;
                return true;
            }
            else if (value is ICodedSimple || value is IConceptQualifier) // hack: For parsing values that are not in an known domain
                ;
            else if (m_destType.IsEnum && s_enumerationMaps.ContainsKey(string.Format("{0}.{1}", m_destType.FullName, value)))
            {
                value = Enum.Parse(m_destType, s_enumerationMaps[string.Format("{0}.{1}", m_destType.FullName, value)]);
                if (!requiresExplicitCastCall)
                {
                    result = value;
                    return true;
                }
            }
            else if (m_destType.IsEnum) // No map exists yet
            {
                ParseMaps(m_destType);
                if (!s_enumerationMaps.ContainsKey(string.Format("{0}.{1}", m_destType.FullName, value)) && !requiresExplicitCastCall)
                    throw new VocabularyException(string.Format("Can't find value '{0}' in domain '{1}'.", value, m_destType.Name), value.ToString(), m_destType.Name, null);

                try
                {
                    value = Enum.Parse(m_destType, s_enumerationMaps[string.Format("{0}.{1}", m_destType.FullName, value)]);
                }
                catch
                {
                    if (!requiresExplicitCastCall)
                        throw new VocabularyException(string.Format("Can't find value '{0}' in domain '{1}'.", value, m_destType.Name), value.ToString(), m_destType.Name, null);
                }

                // Can we just return as is, or is a function needed?
                if (!requiresExplicitCastCall)
                {
                    result = value;
                    return true;
                }

            }
            else if (destType.FullName.StartsWith("System.Nullable"))
                destType = m_destType; // Transparency for nullable types

            // Is there a built in method that can convert this
            MethodInfo mi;
            if (!s_wireMaps.TryGetValue(string.Format("{0}>{1}", value.GetType().FullName, destType.FullName), out mi))
            {
                // Try to find a map first...
                // Using an operator overload
                mi = FindConverter(m_destType, value.GetType(), destType);
                if (mi == null)
                    mi = FindConverter(value.GetType(), value.GetType(), destType);
                if (mi == null && m_destType != destType) // Using container type
                    mi = FindConverter(destType, value.GetType(), destType);
                if (mi == null) // Using System.Convert as a last resort
                    mi = FindConverter(typeof(System.Convert), value.GetType(), destType);

                if (mi != null)
                {
                    lock (s_wireMaps)
                        if (!s_wireMaps.ContainsKey(string.Format("{0}>{1}", value.GetType().FullName, destType.FullName)))
                            s_wireMaps.Add(string.Format("{0}>{1}", value.GetType().FullName, destType.FullName), mi);
                }
                else
                {
                    // Last ditch effort to parse
                    // Compare apples to apples
                    // We have two generic types, however the dest type generic doesn't match the 
                    // value type generic. This is common with type overrides where the object container
                    // doesn't match the value. For example, attempting to assign a CV<String> to a CS<ResponseMode>
                    if (value.GetType().IsGenericType &&
                        destType.IsGenericType &&
                        destType.GetGenericArguments()[0] != value.GetType().GetGenericArguments()[0] &&
                        destType.GetGenericTypeDefinition() != value.GetType().GetGenericTypeDefinition())
                    {
                        Type valueCastType = value.GetType().GetGenericTypeDefinition().MakeGenericType(
                            destType.GetGenericArguments()
                        );
                        try
                        {
                            result = FromWireFormat(value, valueCastType);
                            return true;
                        }
                        catch
                        {
                            result = null;
                            return false;
                        }
                    }
                    else
                    {
                        result = null;
                        return false;
                    }

                }
            }


            try
            {
                result = mi.Invoke(null, new object[] { value }); // Invoke the conversion method
                return result != null;
            }
            catch { result = null; return false; }
        }
    }
}