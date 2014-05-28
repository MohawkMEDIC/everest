/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 6-5-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Attributes;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using System.ComponentModel;
using MARC.Everest.Sherpas.Exception;
using System.Reflection;
using MARC.Everest.Sherpas.ResultDetail;
using MARC.Everest.Sherpas.Interface;
using System.Xml;
using MARC.Everest.Sherpas;

namespace MARC.Everest.Sherpas.Formatter.XML.ITS1
{
    /// <summary>
    /// Represents a formatter that can serialize HL7v3 structures to/from XML ITS1 with CDA Data Types R1
    /// </summary>
    /// <remarks>
    /// <para>This particular formatter has the ability to create and parse XML instances containing CDA R2 structures.</para>
    /// <para>The formatter uses the <see cref="T:MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter"/> for formatting and adds the ability to register templates and represent those templates
    /// on the wire in a format which is much easier for the developer.</para>
    /// </remarks>
    [Description("Clinical Document Architecture R2 Formatter with Templating Ability")]
    public class ClinicalDocumentFormatter : XmlIts1Formatter
    {


        // Template types that have been registered
        private Dictionary<String, Type> m_templateTypes = new Dictionary<string, Type>();
        // Container types
        private List<KeyValuePair<Type, TemplateContainerAttribute>> m_containerTypes = new List<KeyValuePair<Type, TemplateContainerAttribute>>();

        /// <summary>
        /// Register all templates in this formatter for use in the document
        /// </summary>
        public void RegisterTemplates(Type[] types)
        {
            foreach (var type in types)
                this.RegisterTemplate(type);
        }

        /// <summary>
        /// Register a template
        /// </summary>
        public void RegisterTemplate(Type type)
        {
            Type eType = null;  // existing template type
            var templateIds = type.GetCustomAttributes(typeof(TemplateAttribute), false);
            foreach (TemplateAttribute templateId in templateIds)
            {
                if (this.m_templateTypes.TryGetValue(templateId.TemplateId, out eType) && eType != type)
                    throw new DuplicateTemplateException(templateId.TemplateId, type, eType);
                lock (this.m_syncRoot)
                    this.m_templateTypes.Add(templateId.TemplateId, type);
            }
        }

        /// <summary>
        /// Registers a type that is used to contain the specified type
        /// </summary>
        public void RegisterContainer(Type containerType)
        {
            // The type that would appear in the parse
            var properties = containerType.GetCustomAttributes(typeof(TemplateContainerAttribute), false);
            if(properties.Length > 0)
                this.m_containerTypes.Add(new KeyValuePair<Type, TemplateContainerAttribute>(containerType, properties[0] as TemplateContainerAttribute));
        }

        /// <summary>
        /// Get a type that implements the template specified
        /// </summary>
        public Type GetTemplateType(String templateId)
        {
            Type tType = null;
            if (this.m_templateTypes.TryGetValue(templateId, out tType))
                return tType;
            return null;
        }

        /// <summary>
        /// Get template type
        /// </summary>
        public Type GetTemplateType(IImplementsTemplateId instance)
        {
            // todo: Finish this to support multiple template ids
            return this.GetTemplateType(instance.TemplateId.First.Root);
            
        }

        /// <summary>
        /// Clinical document formatter constructor sets up the default options for the clinical document formatter for use in Sherpas
        /// </summary>
        public ClinicalDocumentFormatter()
        {
            base.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
        }

        /// <summary>
        /// Write element util in a way that xsi:type is suppressed for templated classes
        /// </summary>
        public override void WriteElementUtil(System.Xml.XmlWriter s, string namespaceUri, string elementName, Interfaces.IGraphable g, Type propType, Interfaces.IGraphable context, XmlIts1FormatterGraphResult resultContext)
        {
            base.ThrowIfDisposed();
            
            // Graph is nothing
            if (g == null)
                return;

            // Normalize
            if (g is INormalizable)
                g = (g as INormalizable).Normalize();
            //if (g is IMessageTypeTemplate)
            //    (g as IMessageTypeTemplate).InitializeInstance();

            // Write start of element
            s.WriteStartElement(elementName, namespaceUri);

            // JF: Output XSI:Type as long as we're not writing out a template
            object[] templateAttributes = g.GetType().GetCustomAttributes(typeof(TemplateAttribute), false);
            if (templateAttributes.Length > 0)
            {
                IImplementsTemplateId templateId = g as IImplementsTemplateId;
                if (templateId.TemplateId == null)
                    templateId.TemplateId = new LIST<II>();
                // Append the template identifiers which the class implements
                foreach (var tplId in templateAttributes)
                {
                    TemplateAttribute ta = tplId as TemplateAttribute;
                    if (templateId.TemplateId.Find(o => o.Root == ta.TemplateId) == null)
                        templateId.TemplateId.Add(new II(ta.TemplateId));
                }
            }
            else if (!g.GetType().Equals(propType))
            {
                string xsiType = String.Empty;
                if (typeof(ANY).IsAssignableFrom(g.GetType()))
                {
                    xsiType += s.LookupPrefix("urn:hl7-org:v3");
                    if (!String.IsNullOrEmpty(xsiType))
                        xsiType += ":";
                    xsiType += Util.CreateXSITypeName(g.GetType());
                }
                else if (propType != null && g.GetType().Assembly.FullName != propType.Assembly.FullName)
                {
                    string typeName = this.CreateXSITypeName(g.GetType(), context != null ? context.GetType() : null, s as IXmlNamespaceResolver);

                    // If there is no different then don't output
                    if (typeName != this.CreateXSITypeName(propType, context == null ? null : context.GetType(), s as IXmlNamespaceResolver))
                    {
                        xsiType = typeName;
                        lock (this.m_syncRoot)
                            if (!this.s_typeNameMaps.ContainsKey(typeName))
                                this.RegisterXSITypeName(typeName, g.GetType());
                    }

                }
                if (!String.IsNullOrEmpty(xsiType) && !xsiType.EndsWith(":"))
                    s.WriteAttributeString("xsi", "type", XmlIts1Formatter.NS_XSI, xsiType);

            }

            // Graph the object
            base.GraphObject(s, g, g.GetType(), context, resultContext);
            s.WriteEndElement();
        }

        /// <summary>
        /// Parse an object from the base formatter and translate it to a templated class if not already done
        /// </summary>
        public override Interfaces.IGraphable ParseObject(System.Xml.XmlReader r, Type useType, Type interactionContext, XmlIts1FormatterParseResult resultContext)
        {
            
            // Correct ClinicalDocument specialty to a generic CDA for now and then we'll convert it
            String originalLocation = r.ToString();

            // TODO: How to do this?? Perhaps we might have to write an overridable hook?
            var baseParseResult = base.ParseObject(r, useType, interactionContext, resultContext);
            var imt = baseParseResult as IMessageTypeTemplate;
            // Couldn't determine template?
            var iit = baseParseResult as IImplementsTemplateId;
            if (iit != null && iit.TemplateId != null && !iit.TemplateId.IsEmpty && imt == null)
                resultContext.AddResultDetail(new UnknownTemplateResultDetail(ResultDetailType.Warning, iit, originalLocation));

            // Is there any substitution that can occur?
            if (baseParseResult != null && !(baseParseResult is ANY) && !(baseParseResult is IMessageTypeTemplate))
            {
                // Container types
                var subst = this.m_containerTypes.Where(o => o.Key.BaseType == baseParseResult.GetType());
                foreach (var sub in subst)
                {
                    var pi = sub.Key.BaseType.GetProperty(sub.Value.PropertyName);
                    if(pi == null) continue;
                    var instance = pi.GetValue(baseParseResult, null);
                    if (instance != null && sub.Value.TemplateType.IsAssignableFrom(instance.GetType()))
                    {
                        // Now construct
                        var ci = sub.Key.GetConstructor(new Type[] { baseParseResult.GetType() });
                        if (ci == null) continue;
                        return ci.Invoke(new object[] { baseParseResult }) as IGraphable;
                    }
                }
                // Correct instance?
                
            }

            return baseParseResult; // this.CorrectInstance(baseParseResult) as IGraphable;
            
        }

        /// <summary>
        /// Correct an instance from the templateId
        /// </summary>
        public override object CorrectInstance(object instance)
        {
            var templateId = instance as IImplementsTemplateId;

            if (templateId == null || templateId.TemplateId == null || templateId.TemplateId.IsNull || templateId.TemplateId.IsEmpty)
                return instance; // no corrections to make

            // We need to determine the deserialization type
            var candidateType = this.GetTemplateType(templateId);
            if (candidateType == null || candidateType == instance.GetType())
                return instance;

            // We need a default ctor!
            var ci = candidateType.GetConstructor(new Type[] { instance.GetType() });
            if (ci != null)
                return ci.Invoke(new object[] { instance });
            else
            {
                ci = candidateType.GetConstructor(Type.EmptyTypes);
                if (ci == null)
                    return instance;

                var correctInstance = ci.Invoke(null);
                // Copy
                foreach (var pi in instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var destPi = correctInstance.GetType().GetProperty(pi.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    if (destPi == null)
                        destPi = correctInstance.GetType().GetProperty(pi.Name, BindingFlags.Public | BindingFlags.Instance);
                    destPi.SetValue(correctInstance, Util.FromWireFormat(pi.GetValue(instance, null), destPi.PropertyType), null);
                }
                return correctInstance as IGraphable;
            }
        }
    }
}
