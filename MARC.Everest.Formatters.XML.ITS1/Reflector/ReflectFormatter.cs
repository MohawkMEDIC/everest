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
                s.WriteStartElement(structureAttribute.Name, "urn:hl7-org:v3");
                s.WriteAttributeString("ITSVersion","XML_1.0"); // Add ITS version
                s.WriteAttributeString("xmlns", "xsi", null, XmlIts1Formatter.NS_XSI);
            }
            else if (structureAttribute.IsEntryPoint && (s is MARC.Everest.Xml.XmlStateWriter && (s as MARC.Everest.Xml.XmlStateWriter).ElementStack.Count == 0 || s.WriteState == System.Xml.WriteState.Start))
            {
                isEntryPoint = true;
                if (isEntryPoint)
                {
                    s.WriteStartElement(structureAttribute.Name, "urn:hl7-org:v3");
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
                object instance = pi.GetValue(o, null);

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
                            if (instance != null && !isInstanceNull)
                                s.WriteAttributeString(pa.Name, Util.ToWireFormat(instance));
                            else if (isInstanceNull && pi.Name == "NullFlavor")
                                Host.WriteNullFlavorUtil(s, (IGraphable)instance);
                            break;
                        default:

                            // Instance is null
                            if (instance == null || isInstanceNull)
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
                                Host.WriteElementUtil(s, pa.Name, instance as IGraphable, pi.PropertyType, context, resultContext);
                            }
                            else if (instance is ICollection)
                            {
                                Type genType = pi.PropertyType.GetGenericArguments()[0];
                                foreach (object itm in (instance as ICollection))
                                    Host.WriteElementUtil(s, pa.Name, itm as IGraphable, genType, context, resultContext);
                            }
                            else
                                s.WriteElementString(pa.Name, instance.ToString());
                            break;
                    }
                }
                else if(propertyAttributes.Length > 1 && instance != null && !isInstanceNull) // Choice
                {
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
                            if(context == null || context.GetType() == formatAs.InteractionOwner) 
                                break;
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
                        Host.WriteElementUtil(s, formatAs.Name, (MARC.Everest.Interfaces.IGraphable)instance, formatAs.Type, context, resultContext);
                    else if (instance.GetType().GetInterface("System.Collections.IEnumerable", false) != null) // List
                        foreach (MARC.Everest.Interfaces.IGraphable ig in instance as IEnumerable) { Host.WriteElementUtil(s, formatAs.Name, ig, instance.GetType(), context, resultContext); }
                    else // Not recognized
                        s.WriteElementString(formatAs.Name, "urn:hl7-org:v3", instance.ToString());

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

            String nil = s.GetAttribute("nil", XmlIts1Formatter.NS_XSI);
            if (!String.IsNullOrEmpty(nil) && Convert.ToBoolean(nil))
                return null;

            ConstructorInfo ci = useType.GetConstructor(Type.EmptyTypes);
            if(ci == null)
                throw new InvalidOperationException(String.Format("Cannot create an instance of type '{0}' as it defines no default constructor", useType.FullName));

            // Create the instance
            object instance = ci.Invoke(null);

            PropertyInfo[] properties = useType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

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
                    else if (s.Prefix == "xmlns" || s.LocalName == "xmlns" || s.LocalName == "ITSVersion")
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

            // Is reader at an empty element
            if (s.IsEmptyElement) return instance;

            // Read content
            string currentElementName = s.LocalName,
                lastElementRead = s.LocalName;
            while(true)
            {

                // End of stream or item not read
                if (lastElementRead == s.LocalName && !s.Read())
                    break;

                lastElementRead = s.LocalName;

                // Element is end element and matches the starting element namd
                if (s.NodeType == System.Xml.XmlNodeType.EndElement && s.LocalName == currentElementName)
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
                    if (String.IsNullOrEmpty(s.GetAttribute("specializationType")) && s is MARC.Everest.Xml.XmlStateReader && (this.Host.Settings & SettingsType.AllowFlavorImposing) == SettingsType.AllowFlavorImposing) (s as MARC.Everest.Xml.XmlStateReader).AddFakeAttribute("specializationType", pa.ImposeFlavorId);

                    // Cannot deserialize this
                    if (pa.Type == null && pi.PropertyType == typeof(System.Object))
                        resultContext.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, pi.Name, "urn:hl7-org:v3", s.ToString(), null));
                    // Simple deserialization if PA type has IGraphable or PI type has IGraphable and PA type not specified
                    else if (pi.GetSetMethod() != null &&
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

                }
            }
            
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
        public bool Validate(MARC.Everest.Interfaces.IGraphable o, string location, out MARC.Everest.Connectors.IResultDetail[] details)
        {
            List<IResultDetail> dtls = new List<IResultDetail>(10);
            bool isValid = true;

            // Null return bool
            if (o == null)
            {
                details = dtls.ToArray();
                return true;
            }

            PropertyInfo nullFlavorAttrib = o.GetType().GetProperty("NullFlavor");
            if (nullFlavorAttrib != null && nullFlavorAttrib.GetValue(o, null) != null)
            {
                details = dtls.ToArray();
                return true;
            }

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
                    else if (pa.MinOccurs != 0)
                    {
                        int minOccurs = pa.MinOccurs, 
                            maxOccurs = pa.MaxOccurs < 0 ? Int32.MaxValue : pa.MaxOccurs;
                        var piCollection = piValue as ICollection;
                        if(piCollection != null && (piCollection.Count > maxOccurs || piCollection.Count < minOccurs))
                        { 
                            isValid = false; 
                            dtls.Add(new InsufficientRepetitionsResultDetail(ResultDetailType.Error, String.Format("Property {0} in {2} does not have enough elements in the list, need between {1} and {3} elements!", pi.Name, minOccurs, o.GetType().FullName, maxOccurs), location));
                        }
                    }
                }
            }

            details = dtls.ToArray();
            return isValid;
        }

        #endregion
    }
}
