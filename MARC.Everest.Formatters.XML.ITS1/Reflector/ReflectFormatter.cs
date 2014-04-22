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
 * Date: 07-26-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using System.Reflection;
using MARC.Everest.Attributes;
using MARC.Everest.Interfaces;
using System.Collections;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;
using System.Diagnostics;

#if WINDOWS_PHONE
using MARC.Everest.Phone;
#endif

namespace MARC.Everest.Formatters.XML.ITS1.Reflector
{
    /// <summary>
    /// Handles reflection method of formatting
    /// </summary>
    internal class ReflectFormatter : ITypeFormatter
    {
        #region ITypeFormatter Members

        /// <summary>
        /// Gets or sets the host of the formatter
        /// </summary>
        public XmlIts1Formatter Host { get; set; }

        /// <summary>
        /// Graphs an object to the specified stream
        /// </summary>
        public void Graph(System.Xml.XmlWriter s, object o, MARC.Everest.Interfaces.IGraphable context, XmlIts1FormatterGraphResult resultContext)
        {
            ResultCode rc = ResultCode.Accepted;
            Type instanceType = o.GetType();

            // Verify o is not null
            if (o == null) throw new System.ArgumentNullException("o");

            // Attempt to get null flavor
            
            var nfp = o.GetType().GetProperty("NullFlavor");
            bool isInstanceNull = false, 
                isEntryPoint = false;
            if (nfp != null)
                isInstanceNull = nfp.GetValue(o, null) != null;

            // Interaction?
            object[] structureAttributes = instanceType.GetCustomAttributes(typeof(StructureAttribute), false);
            StructureAttribute structureAttribute = structureAttributes[0] as StructureAttribute;
            if (structureAttribute.StructureType == StructureAttribute.StructureAttributeType.Interaction)
            {
                isEntryPoint = true;
                s.WriteStartElement(structureAttribute.Name, structureAttribute.NamespaceUri);
                s.WriteAttributeString("ITSVersion","XML_1.0"); // Add ITS version
                s.WriteAttributeString("xmlns", "xsi", null, XmlIts1Formatter.NS_XSI);
            }
            else if (structureAttribute.IsEntryPoint && (s is MARC.Everest.Xml.XmlStateWriter && (s as MARC.Everest.Xml.XmlStateWriter).ElementStack.Count == 0 || s.WriteState == System.Xml.WriteState.Start))
            {
                isEntryPoint = true;
                if (isEntryPoint)
                {
                    s.WriteStartElement(structureAttribute.Name, structureAttribute.NamespaceUri);
                    s.WriteAttributeString("xmlns", "xsi", null, XmlIts1Formatter.NS_XSI);
                }
            }
                
            // Validate
            this.Host.ValidateHelper(s, o as IGraphable, this, resultContext);

            // Reflect the properties and ensure they are in the appropriate order
            List<PropertyInfo> buildProperties = GetBuildProperties(instanceType);
            
            // Attributes first
            foreach (var pi in buildProperties)
            {

                object[] propertyAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), false);
                object rawInstance = pi.GetValue(o, null);

                // list?
                ArrayList instances = new ArrayList();
                var realType = pi.PropertyType;

                if (pi.PropertyType.GetInterface(typeof(IList<>).FullName) != null && !typeof(ANY).IsAssignableFrom(pi.PropertyType))
                {
                    realType = realType.GetGenericArguments()[0];
                    instances.AddRange(rawInstance as ICollection);
                }
                else
                    instances.Add(rawInstance);

                for(int iter = 0; iter < instances.Count; iter++)
                {
                    var instance = instances[iter];

                    if (propertyAttributes.Length == 1) // Not a choice
                    {
                        PropertyAttribute pa = propertyAttributes[0] as PropertyAttribute;

                        // Validation Rule Change: We'll require the user to perform this
                        // Is this a required attribute that is null? We'll set a null flavor 
                        if ((pa.Conformance == PropertyAttribute.AttributeConformanceType.Required || pa.Conformance == PropertyAttribute.AttributeConformanceType.Populated) &&
                            pa.PropertyType != PropertyAttribute.AttributeAttributeType.Structural &&
                            pi.PropertyType.GetProperty("NullFlavor") != null &&
                            !pi.PropertyType.IsAbstract &&
                            pi.CanWrite)
                        {
                            var nullFlavorProperty = pi.PropertyType.GetProperty("NullFlavor");
                            // Locate the default property 
                            if (instance == null && Host.CreateRequiredElements && nullFlavorProperty != null)
                            {
                                ConstructorInfo ci = pi.PropertyType.GetConstructor(Type.EmptyTypes);
                                instance = ci.Invoke(null);
                                nullFlavorProperty.SetValue(instance, Util.FromWireFormat("NI", nullFlavorProperty.PropertyType), null);
                            }
                        }


                        // Property type
                        switch (pa.PropertyType)
                        {
                            case PropertyAttribute.AttributeAttributeType.Structural:
                                if ((Host.Settings & SettingsType.SuppressNullEnforcement) == 0)
                                {
                                    if (instance != null && !isInstanceNull)
                                        s.WriteAttributeString(pa.Name, Util.ToWireFormat(instance));
                                    else if (isInstanceNull && pi.Name == "NullFlavor")
                                        Host.WriteNullFlavorUtil(s, (IGraphable)instance);
                                }
                                else if (instance != null)
                                {
                                    if (instance != null && pi.Name == "NullFlavor")
                                        Host.WriteNullFlavorUtil(s, (IGraphable)instance);
                                    else if (instance != null)
                                        s.WriteAttributeString(pa.Name, Util.ToWireFormat(instance));
                                }

                                break;
                            default:

                                // Instance is null
                                if (instance == null)
                                    continue;
                                else if (isInstanceNull && (Host.Settings & (SettingsType.SuppressNullEnforcement | SettingsType.SuppressXsiNil)) != 0)
                                    resultContext.AddResultDetail(new FormalConstraintViolationResultDetail(ResultDetailType.Information, "The context is null however SuppressNullEnforcement and SuppressXsiNil are set, therefore elements will be graphed. This is not necessarily HL7v3 compliant", s.ToString(), null));
                                else if (isInstanceNull)
                                    continue;

                                // Impose flavors or code?
                                if (pa.DefaultUpdateMode != MARC.Everest.DataTypes.UpdateMode.Unknown &&
                                    pi.PropertyType.GetProperty("UpdateMode") != null &&
                                    pi.PropertyType.GetProperty("UpdateMode").GetValue(instance, null) == null &&
                                    (this.Host.Settings & SettingsType.AllowUpdateModeImposing) == SettingsType.AllowUpdateModeImposing)
                                    pi.PropertyType.GetProperty("UpdateMode").SetValue(instance, Util.FromWireFormat(pa.DefaultUpdateMode, pi.PropertyType.GetProperty("UpdateMode").PropertyType), null);
                                if (pa.ImposeFlavorId != null &&
                                    instance is IAny &&
                                    (Host.Settings & SettingsType.AllowFlavorImposing) == SettingsType.AllowFlavorImposing)
                                    (instance as IAny).Flavor = pa.ImposeFlavorId;
                                if (pa.SupplierDomain != null &&
                                    instance is ICodedValue &&
                                    (instance as ICodedSimple).CodeValue != null &&
                                    (instance as ICodedValue).CodeSystem == null &&
                                    (instance as IImplementsNullFlavor).NullFlavor == null &&
                                    (Host.Settings & SettingsType.AllowSupplierDomainImposing) == SettingsType.AllowSupplierDomainImposing)
                                    (instance as ICodedValue).CodeSystem = pa.SupplierDomain;

                                
                                // Instance is graphable
                                if (instance is IGraphable)
                                {
                                    // Ensure the data is not empty
                                    if (instance is IColl && (instance as IColl).IsEmpty && (instance as IImplementsNullFlavor).NullFlavor == null)
                                        continue;
                                    Host.WriteElementUtil(s, pa.NamespaceUri, pa.Name, instance as IGraphable, realType, context, resultContext);
                                }
                                else if (instance is ICollection)
                                {
                                    Type genType = pi.PropertyType.GetGenericArguments()[0];
                                    foreach (object itm in (instance as ICollection))
                                        Host.WriteElementUtil(s, pa.NamespaceUri, pa.Name, itm as IGraphable, genType, context, resultContext);
                                }
                                else
                                    s.WriteElementString(pa.Name, instance.ToString());
                                break;
                        }
                    }
                    else if (propertyAttributes.Length > 1) // Choice
                    {
                        // Instance is null
                        if (instance == null)
                            continue;
                        else if (isInstanceNull && (Host.Settings & (SettingsType.SuppressNullEnforcement | SettingsType.SuppressXsiNil)) != 0)
                            resultContext.AddResultDetail(new FormalConstraintViolationResultDetail(ResultDetailType.Information, "The context is null however SuppressNullEnforcement and SuppressXsiNil are set, therefore elements will be graphed. This is not necessarily HL7v3 compliant", s.ToString(), null));
                        else if (isInstanceNull)
                            continue;
#if WINDOWS_PHONE
                    PropertyAttribute formatAs = propertyAttributes.Find(cpa => (cpa as PropertyAttribute).Type == null) as PropertyAttribute;
#else
                        PropertyAttribute formatAs = Array.Find(propertyAttributes, cpa => (cpa as PropertyAttribute).Type == null) as PropertyAttribute;
#endif
                        // Search by type and interaction
                        foreach (PropertyAttribute pa in propertyAttributes)
                        {
                            if (pa.Type != null && instance.GetType() == pa.Type && (context != null && context.GetType() == pa.InteractionOwner || (pa.InteractionOwner == null && formatAs == null)))
                            {
                                formatAs = pa;
                                if (context == null || context.GetType() == formatAs.InteractionOwner)
                                    break;
                            }
                        }

                        // Slow check
                        if (formatAs == null && (this.Host.Settings & SettingsType.AlwaysCheckForOverrides) != 0)
                        {
                            foreach (PropertyAttribute pa in propertyAttributes)
                            {
                                if (pa.Type != null && pa.Type.IsAssignableFrom(instance.GetType()) && (context != null && context.GetType() == pa.InteractionOwner || (pa.InteractionOwner == null && formatAs == null)))
                                {
                                    formatAs = pa;
                                    if (context == null || context.GetType() == formatAs.InteractionOwner)
                                        break;
                                }
                            }
                        }

                        //if(formatAs == null) // try to find a regular choice
                        //    foreach(PropertyAttribute pa in propertyAttributes)
                        //        if (pa.Type != null && instance.GetType() == pa.Type)
                        //        {
                        //            formatAs = pa;
                        //            break;
                        //        }


                        // Format
                        if (formatAs == null)
                            resultContext.AddResultDetail(new NotSupportedChoiceResultDetail(ResultDetailType.Error, String.Format("Type {0} is not a valid choice according to available choice elements and won't be formatted", instance.GetType()), s.ToString(), null));
                        else if (instance.GetType().GetInterface("MARC.Everest.Interfaces.IGraphable", false) != null) // Non Graphable
                            Host.WriteElementUtil(s, formatAs.NamespaceUri, formatAs.Name, (MARC.Everest.Interfaces.IGraphable)instance, formatAs.Type, context, resultContext);
                        else if (instance.GetType().GetInterface("System.Collections.IEnumerable", false) != null) // List
                            foreach (MARC.Everest.Interfaces.IGraphable ig in instance as IEnumerable) { Host.WriteElementUtil(s, formatAs.NamespaceUri, formatAs.Name, ig, instance.GetType(), context, resultContext); }
                        else // Not recognized
                            s.WriteElementString(formatAs.Name, formatAs.NamespaceUri, instance.ToString());

                    }
                }
            }

            // Is Entry point
            if (isEntryPoint)
                s.WriteEndElement();

        }

        /// <summary>
        /// Get build properties
        /// </summary>
        /// <remarks>
        /// This ensures that the properties in the <paramref name="instanceType"/> 
        /// </remarks>
        private List<PropertyInfo> GetBuildProperties(Type instanceType)
        {
            List<PropertyInfo> buildProperties = new List<PropertyInfo>(10);
            Type cType = instanceType;
            int nonTrav = 0, nonStruct = 0;
            while (cType != typeof(System.Object))
            {

#if WINDOWS_PHONE
                PropertyInfo[] typeTypes = cType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                typeTypes = typeTypes.FindAll(o => o.GetCustomAttributes(typeof(PropertyAttribute), false).Length > 0);
                PropertyInfo[] nonTraversable = typeTypes.FindAll(o => !IsTraversable(o) && !IsNonStructural(o) && !buildProperties.Exists(f => f.Name == o.Name)),
                    traversable = typeTypes.FindAll(o => IsTraversable(o) && !buildProperties.Exists(f => f.Name == o.Name)),
               nonStructural = typeTypes.FindAll(o => IsNonStructural(o) && !buildProperties.Exists(f => f.Name == o.Name));
                //Array.Sort<PropertyInfo>(traversable, (a, b) => (a.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).SortKey.CompareTo((b.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).SortKey));
#else
                PropertyInfo[] typeTypes = cType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                typeTypes = Array.FindAll(typeTypes, o => o.GetCustomAttributes(typeof(PropertyAttribute), false).Length > 0);
                PropertyInfo[] nonTraversable = Array.FindAll(typeTypes, o => !IsTraversable(o) && !IsNonStructural(o) && !buildProperties.Exists(f => f.Name == o.Name)),
                    traversable = Array.FindAll(typeTypes, o => IsTraversable(o) && !buildProperties.Exists(f => f.Name == o.Name)),
               nonStructural = Array.FindAll(typeTypes, o=> IsNonStructural(o) && !buildProperties.Exists(f=>f.Name == o.Name));
#endif
                //Array.Sort<PropertyInfo>(traversable, (a, b) => (a.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).SortKey.CompareTo((b.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute).SortKey));
                nonTrav += nonTraversable.Length + nonStructural.Length;
                nonStruct += nonTraversable.Length;
                // NonTraversable properties need to be serialized first
                buildProperties.InsertRange(0, nonTraversable);
                buildProperties.InsertRange(nonStruct, nonStructural);
                buildProperties.InsertRange(nonTrav, traversable);
                cType = cType.BaseType;
            }
            return buildProperties;
        }

        /// <summary>
        /// Returns true if the property is traversable
        /// </summary>
        public bool IsTraversable(PropertyInfo pi)
        {
            object[] propertyAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), false);
            if (propertyAttributes.Length > 0)
                return (propertyAttributes[0] as PropertyAttribute).PropertyType == PropertyAttribute.AttributeAttributeType.Traversable;
            else
                return false;
        }

        /// <summary>
        /// Returns true if the property is traversable
        /// </summary>
        public bool IsNonStructural(PropertyInfo pi)
        {
            object[] propertyAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), false);
            if (propertyAttributes.Length > 0)
                return (propertyAttributes[0] as PropertyAttribute).PropertyType == PropertyAttribute.AttributeAttributeType.NonStructural;
            else
                return false;
        }

        /// <summary>
        /// Parses an instance of the specified type from the specified stream
        /// </summary>
        public object Parse(System.Xml.XmlReader s, Type useType, Type currentInteractionType, XmlIts1FormatterParseResult resultContext)
        {

            ConstructorInfo ci = useType.GetConstructor(Type.EmptyTypes);
            if(ci == null)
                throw new InvalidOperationException(String.Format("Cannot create an instance of type '{0}' as it defines no default constructor", useType.FullName));

            // Create the instance
            object instance = ci.Invoke(null);

            PropertyInfo[] properties = useType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            String nil = s.GetAttribute("nil", XmlIts1Formatter.NS_XSI);

            // If XSI:NIL is true and there is no other data, then the data is null
            if ((this.Host.Settings & SettingsType.SuppressNullEnforcement) == 0 &&
                s.AttributeCount == 1 && nil != null && Convert.ToBoolean(nil) &&
                s.IsEmptyElement)
                return null;

            // Move the reader to the first attribute
            if (s.MoveToFirstAttribute())
            {
                // Now we read the attributes and match with the properties
                do
                {



#if WINDOWS_PHONE
                    PropertyInfo pi = properties.Find(o => o.GetCustomAttributes(true).Count(pa => pa is PropertyAttribute && (pa as PropertyAttribute).Name == s.LocalName) > 0);
#else
                    PropertyInfo pi = Array.Find<PropertyInfo>(properties, o => o.GetCustomAttributes(true).Count(pa => pa is PropertyAttribute && (pa as PropertyAttribute).Name == s.LocalName) > 0);
#endif              
                    // Can we set the PI?
                    if (s.LocalName == "ITSVersion" && s.Value != "XML_1.0")
                        throw new System.InvalidOperationException(System.String.Format("This formatter can only parse XML_1.0 structures. This structure claims to be '{0}'.", s.Value));
                    else if (s.Prefix == "xmlns" || s.LocalName == "xmlns" || s.LocalName == "ITSVersion" || s.NamespaceURI == XmlIts1Formatter.NS_XSI)
                        continue;
                    else if (pi == null)
                    {
                        resultContext.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, String.Format("@{0}", s.LocalName), s.NamespaceURI, s.ToString(), null));
                        continue;
                    }

                    var paList = pi.GetCustomAttributes(typeof(PropertyAttribute), true); // property attributes
                    
                    // VAlidate list of PA
                    if(paList == null || paList.Length != 1)
                    {
                        resultContext.AddResultDetail(new ResultDetail(ResultDetailType.Warning, String.Format("Invalid number of PropertyAttributes on property '{0}', ignoring", pi.Name), s.ToString(), null));
                        continue; // not a property to be parsed
                    }

                    if (pi.GetSetMethod() == null)
                    {
                        if (!Util.ToWireFormat(pi.GetValue(instance, null)).Equals(s.Value))
                            resultContext.AddResultDetail(new FixedValueMisMatchedResultDetail(s.Value, pi.GetValue(instance, null).ToString(), true, s.ToString()));
                    }
                    else if (!String.IsNullOrEmpty((paList[0] as PropertyAttribute).FixedValue))
                    {
                        if (!(paList[0] as PropertyAttribute).FixedValue.Equals(s.Value))
                        {
                            resultContext.AddResultDetail(new FixedValueMisMatchedResultDetail(s.Value, (paList[0] as PropertyAttribute).FixedValue, false, s.ToString()));
                            pi.SetValue(instance, Util.FromWireFormat(s.Value, pi.PropertyType), null);
                        }
                    }
                    else
                        pi.SetValue(instance, Util.FromWireFormat(s.Value, pi.PropertyType), null);
                }
                while (s.MoveToNextAttribute());
                s.MoveToElement();
            }

            // Nil? 
            // BUG: Fixed, xsi:nil may also have a null-flavor
            if (!String.IsNullOrEmpty(nil) && Convert.ToBoolean(nil) && (this.Host.Settings & SettingsType.SuppressNullEnforcement) == 0)
                return instance;


            // Is reader at an empty element
            if (s.IsEmptyElement) return instance;

            // Read content
            instance = this.Host.ParseElementContent(s, instance, s.LocalName, s.Depth, currentInteractionType, resultContext);
            
            return instance;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public Type HandlesType
        {
            get { return typeof(System.Object); }
        }

        /// <summary>
        /// Validates the specified object
        /// </summary>
        public IEnumerable<IResultDetail> Validate(MARC.Everest.Interfaces.IGraphable o, string location)
        {
            List<IResultDetail> dtls = new List<IResultDetail>(10);
            bool isValid = true;

            // Null return bool
            if (o == null)
                return dtls;

            // Check constraints
            object[] checkConstraints = o.GetType().GetCustomAttributes(typeof(FormalConstraintAttribute), true);
            foreach (FormalConstraintAttribute ca in checkConstraints)
            {
                MethodInfo mi = o.GetType().GetMethod(ca.CheckConstraintMethod, new Type[] { o.GetType() });
                if (mi == null)
                    dtls.Add(new ResultDetail(ResultDetailType.Warning, String.Format("Could not check formal constraint as method {0} could not be found", ca.CheckConstraintMethod), location, null));
                else if (!(bool)mi.Invoke(null, new object[] { o }))
                    dtls.Add(new FormalConstraintViolationResultDetail(ResultDetailType.Error, ca.Description, location, null));
            }

            PropertyInfo nullFlavorAttrib = o.GetType().GetProperty("NullFlavor");
            if (nullFlavorAttrib != null && nullFlavorAttrib.GetValue(o, null) != null)
                return dtls;

            // Scan property info for violations
            foreach (var pi in this.GetBuildProperties(o.GetType()))
            {

                if (pi.GetGetMethod().GetParameters().Length != 0)
                {
                    dtls.Add(new NotImplementedResultDetail(
                        ResultDetailType.Warning,
                        String.Format("Could not validate '{0}' as the property is indexed", pi.Name),
                        location
                    ));
                    continue;
                }

                object piValue = pi.GetValue(o, null);
                object[] propAttributes = pi.GetCustomAttributes(typeof(PropertyAttribute), true);
                
                if (propAttributes.Length > 0)
                {
                    PropertyAttribute pa = propAttributes[0] as PropertyAttribute;
                    if (pa.Conformance == PropertyAttribute.AttributeConformanceType.Mandatory &&
                        pi.PropertyType.GetInterface(typeof(IImplementsNullFlavor).FullName, false) != null &&
                        (piValue == null || (piValue as IImplementsNullFlavor).NullFlavor != null))
                    {
                        isValid = false;
                        dtls.Add(new MandatoryElementMissingResultDetail(ResultDetailType.Error, String.Format("Property {0} in {1} is marked mandatory and is either not assigned, or is assigned a null flavor. This is not permitted.", pi.Name, o.GetType().FullName), location));
                    }
                    else if (pa.Conformance == PropertyAttribute.AttributeConformanceType.Populated && piValue == null)
                    {
                        isValid &= Host.CreateRequiredElements;
                        dtls.Add(new RequiredElementMissingResultDetail(isValid ? ResultDetailType.Warning : ResultDetailType.Error, String.Format("Property {0} in {1} is marked 'populated' and isn't assigned (you must at minimum, assign a nullFlavor for this attribute)!", pi.Name, o.GetType().FullName), location));
                    }
                    else if (pa.MinOccurs != 0 || ((pa.MaxOccurs != -1 && pa.MaxOccurs != 1)))
                    {
                        int minOccurs = pa.MinOccurs, 
                            maxOccurs = pa.MaxOccurs < 0 ? Int32.MaxValue : pa.MaxOccurs;
                        var piCollection = piValue as ICollection;
                        if(piCollection != null && (piCollection.Count > maxOccurs || piCollection.Count < minOccurs))
                        { 
                            isValid = false; 
                            dtls.Add(new InsufficientRepetitionsResultDetail(ResultDetailType.Error, pa.Name, pa.MinOccurs, maxOccurs, piCollection.Count, location));
                        }
                    }
                }

                // Check constraints
                checkConstraints = pi.GetCustomAttributes(typeof(FormalConstraintAttribute), false);
                foreach (FormalConstraintAttribute ca in checkConstraints)
                {

                    
                    MethodInfo mi = o.GetType().GetMethod(ca.CheckConstraintMethod, new Type[] { pi.PropertyType });
                    if (mi == null)
                    {
                        if (pi.PropertyType.GetInterface(typeof(IList<>).FullName) != null)
                        {
                            mi = o.GetType().GetMethod(ca.CheckConstraintMethod, new Type[] { pi.PropertyType.GetGenericArguments()[0] });
                            if(mi == null)
                                dtls.Add(new ResultDetail(ResultDetailType.Warning, String.Format("Could not check formal constraint as method {0} could not be found", ca.CheckConstraintMethod), location, null));
                            else
                                foreach(var i in piValue as IEnumerable)
                                    if (!(bool)mi.Invoke(null, new object[] { i }))
                                        dtls.Add(new FormalConstraintViolationResultDetail(ResultDetailType.Error, ca.Description, location, null));
                        }
                        else
                            dtls.Add(new ResultDetail(ResultDetailType.Warning, String.Format("Could not check formal constraint as method {0} could not be found", ca.CheckConstraintMethod), location, null));
                    }
                    else if (!(bool)mi.Invoke(null, new object[] { piValue }))
                        dtls.Add(new FormalConstraintViolationResultDetail(ResultDetailType.Error, ca.Description, location, null));
                }
            }

            return dtls;
        }

        #endregion


        /// <summary>
        /// Parse element contents
        /// </summary>
        public Object ParseElementContent(System.Xml.XmlReader s, Object instance, string terminationElement, int terminationDepth, Type currentInteractionType, XmlIts1FormatterParseResult resultContext)
        {
            String lastElementRead = s.LocalName;
            bool fromExternalSource = !(terminationElement == lastElementRead && s.NodeType == System.Xml.XmlNodeType.Element);
            var properties = instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            while (true)
            {

                // End of stream or item not read
                if (fromExternalSource)
                    fromExternalSource = false; // HACK: Skip the first read as we're already given context here
                else if (lastElementRead == s.LocalName && !s.Read())
                    break;
                
                lastElementRead = s.LocalName;

                // Element is end element and matches the starting element namd
                if (s.NodeType == System.Xml.XmlNodeType.EndElement && s.LocalName == terminationElement && s.Depth == terminationDepth)
                    break;
                // Element is an end element
                //else if (s.NodeType == System.Xml.XmlNodeType.EndElement)
                //    currentDepth--;
                // Element is a start element
                else if (s.NodeType == System.Xml.XmlNodeType.Element)
                {
                    // Get the element choice property
#if WINDOWS_PHONE
                    PropertyInfo pi = properties.Find(o => o.GetCustomAttributes(true).Count(a => 
                        a is PropertyAttribute && 
                        (a as PropertyAttribute).Name == s.LocalName && 
                        ((a as PropertyAttribute).InteractionOwner ?? currentInteractionType) == currentInteractionType) > 0);
#else
                    PropertyInfo pi = Array.Find(properties, o => o.GetCustomAttributes(true).Count(a =>
                        a is PropertyAttribute &&
                        (a as PropertyAttribute).Name == s.LocalName &&
                        ((a as PropertyAttribute).InteractionOwner ?? currentInteractionType) == currentInteractionType) > 0);
#endif
                    if (pi == null)
                    {
                        resultContext.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
                        continue;
                    }

                    // Get the property attribute that defined the choice
#if WINDOWS_PHONE
                    PropertyAttribute pa = pi.GetCustomAttributes(true).Find(p => 
                        p is PropertyAttribute && 
                        (p as PropertyAttribute).Name == s.LocalName && 
                        ((p as PropertyAttribute).InteractionOwner ?? currentInteractionType) == currentInteractionType) as PropertyAttribute;
#else
                    PropertyAttribute pa = Array.Find(pi.GetCustomAttributes(true), p =>
                        p is PropertyAttribute &&
                        (p as PropertyAttribute).Name == s.LocalName &&
                        ((p as PropertyAttribute).InteractionOwner ?? currentInteractionType) == currentInteractionType) as PropertyAttribute;
#endif
                    // Can we set the PI?
                    if (pi == null || !pi.CanWrite) continue;

                    // Now time to set the PI
                    if (String.IsNullOrEmpty(s.GetAttribute("specializationType")) && s is MARC.Everest.Xml.XmlStateReader && (this.Host.Settings & SettingsType.AllowFlavorImposing) == SettingsType.AllowFlavorImposing && !String.IsNullOrEmpty(pa.ImposeFlavorId)) (s as MARC.Everest.Xml.XmlStateReader).AddFakeAttribute("specializationType", pa.ImposeFlavorId);

                    // Cannot deserialize this
                    if (pa.Type == null && pi.PropertyType == typeof(System.Object))
                        resultContext.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, pi.Name, "urn:hl7-org:v3", s.ToString(), null));
                    // Simple deserialization if PA type has IGraphable or PI type has IGraphable and PA type not specified
                    else if (pi.GetSetMethod() != null &&
                        pi.PropertyType.GetInterface(typeof(IGraphable).FullName) != null && 
                        (pa.Type != null && pa.Type.GetInterface(typeof(IGraphable).FullName, false) != null) ||
                        (pa.Type == null && pi.PropertyType.GetInterface(typeof(IGraphable).FullName, false) != null))
                    {
                        object tempFormat = Host.ParseObject(s, pa.Type ?? pi.PropertyType, currentInteractionType, resultContext);
                        if (!String.IsNullOrEmpty(pa.FixedValue) && !pa.FixedValue.Equals(Util.ToWireFormat(tempFormat)) && pa.PropertyType != PropertyAttribute.AttributeAttributeType.Traversable)
                            resultContext.AddResultDetail(new FixedValueMisMatchedResultDetail(Util.ToWireFormat(tempFormat), pa.FixedValue, s.ToString()));
                        pi.SetValue(instance, Util.FromWireFormat(tempFormat, pa.Type ?? pi.PropertyType), null);
                    }
                    // Call an Add method on a collection type
                    else if (pi.PropertyType.GetMethod("Add") != null) // Collection type
                        pi.PropertyType.GetMethod("Add").Invoke(pi.GetValue(instance, null), new object[] { 
                            Util.FromWireFormat(Host.ParseObject(s, pi.PropertyType.GetGenericArguments()[0], currentInteractionType, resultContext), pi.PropertyType.GetGenericArguments()[0])
                        });
                    // Call the ParseXML custom function on object
                    else if (pi.GetSetMethod() != null && pi.PropertyType.GetMethod("ParseXml", BindingFlags.Public | BindingFlags.Static) != null)
                        pi.SetValue(instance, pi.PropertyType.GetMethod("ParseXml").Invoke(instance, new object[] { s }), null);
                    // Property type is a simple string
                    else if (pi.GetSetMethod() != null && pi.PropertyType == typeof(string)) // Read content... 
                        pi.SetValue(instance, Util.FromWireFormat(s.ReadInnerXml(), typeof(String)), null);
                    // No Set method is used, fixed value?
                    else
                    {
                        object tempFormat = Host.ParseObject(s, pa.Type ?? pi.PropertyType, currentInteractionType, resultContext);
                        if (tempFormat.ToString() != pi.GetValue(instance, null).ToString() && pa.PropertyType != PropertyAttribute.AttributeAttributeType.Traversable)
                            resultContext.AddResultDetail(new MARC.Everest.Connectors.FixedValueMisMatchedResultDetail(tempFormat.ToString(), pi.GetValue(instance, null).ToString(), s.ToString()));
                    }

                    // Need to switch or re-evaluate our instance?
                    var ma = pi.GetCustomAttributes(typeof(MarkerAttribute), true);
                    if (ma.Length > 0)
                        switch ((ma[0] as MarkerAttribute).MarkerType)
                        {
                            case MarkerAttribute.MarkerAttributeType.TemplateId:
                            case MarkerAttribute.MarkerAttributeType.TypeId:

                                var newInstance = this.Host.CorrectInstance(instance);
                                if (newInstance.GetType() != instance.GetType())
                                    return this.Host.ParseElementContent(s, newInstance, terminationElement, s.Depth, currentInteractionType, resultContext);
                                break;
                            default:
                                break;
                        }

                }
            }

            return instance;
        }
    }
}
