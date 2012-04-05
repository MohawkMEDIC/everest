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
using System.Configuration;
using System.Xml;
using System.Reflection;
using MARC.Everest.Connectors.WCF.Core;

namespace MARC.Everest.Connectors.WCF.Configuration
{
    /// <summary>
    /// Configuration section handler for web.config and app.config. 
    /// This class is intended to be used only by the configuration 
    /// system, and not directly by user code.
    /// </summary>
    public class ConfigurationSection : IConfigurationSectionHandler
    {
        /// <summary>
        /// The name of the section the config was loaded from.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal static string SectionName;

        /// <summary>
        /// Gets or sets the formatter that will be used to format messages.
        /// </summary>
        public IStructureFormatter Formatter { get; set; }
        /// <summary>
        /// Gets or sets a dictionary of receivers for the given message types.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Dictionary<string, IMessageReceiver> Receiver { get; set; }
        /// <summary>
        /// Gets or sets a dictionary of actions that map data types to soap actions on the server.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Dictionary<string, string> Actions { get; set; }

        #region IConfigurationSectionHandler Members

        /// <summary>
        /// Create the configuration object
        /// <example>
        /// <code lang="xml">
        /// &lt;WcfConnector formatter="MARC.Everest.Formatters.ITS1.Formatter"&gt;
        ///    &lt;Classification match="/" handler="MyService.MyType"/&gt;
        ///    &lt;Action type="MARC.Everest.Core.Interactions.REPC_IN000076CA" action="urn:hl7-org:v3:REPC_IN000076CA"/&gt;
        /// &lt;/WcfConnector&gt;
        /// </code>
        /// </example>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {

            if (section == null)
                throw new InvalidOperationException("Can't find configuration section");

            SectionName = section.LocalName;
            XmlNodeList classifications = section.SelectNodes("./*[local-name() = 'messageHandler']");
            XmlNodeList actions = section.SelectNodes("./*[local-name() = 'action']");
            Receiver = new Dictionary<string, IMessageReceiver>();
            Actions = new Dictionary<string, string>();

            // Load all assemblies this app needs
            foreach (XmlNode nd in section.SelectNodes("./*[local-name() = 'assembly']"))
            {
                if (nd.Attributes["name"] == null)
                    throw new Exception("You must specify a 'name' attribute when using the 'assembly' element");
                // Load the assembly
                Assembly.Load(new AssemblyName(nd.Attributes["name"].Value));
            }

            // XmlNode
            if(actions != null)
                foreach (XmlNode xn in actions)
                {
                    if(xn.Attributes["type"] == null || xn.Attributes["action"] == null)
                        throw new Exception("You must specify both the 'type' and 'action' to the action element");
                    string action = xn.Attributes["action"].Value, type = xn.Attributes["type"].Value;
                    Actions.Add(type, action);
                }

            // XmlNode
            if (classifications != null)
                foreach (XmlNode xn in classifications)
                {
                    if (xn.Attributes["type"] == null || xn.Attributes["classifier"] == null)
                        throw new Exception("You must specify both the 'classifier' and 'type' to the messageHandler element");
                    // Find the assembly that contains the receiver
                    Type handlerType = Type.GetType(xn.Attributes["type"].Value);

                    // Was the type found quickly? If not, do a slower scan
                    if (handlerType == null)
                    {
                        Assembly tasm = Array.Find<Assembly>(AppDomain.CurrentDomain.GetAssemblies(), a => a.GetType(xn.Attributes["type"].Value) != null);
                        if (tasm != null)
                            handlerType = tasm.GetType(xn.Attributes["type"].Value);
                        else
                            throw new ConfigurationErrorsException(string.Format("Can't find classification handler '{0}'", xn.Attributes["type"].Value));
                    }

                    // Find a type in the app domain that matches the type specified
                    IMessageReceiver receiver = (IMessageReceiver)handlerType.Assembly.CreateInstance(handlerType.FullName);
                    Receiver.Add(xn.Attributes["classifier"].Value, receiver);
                }

            // Process Aides
            List<IStructureFormatter> aides = new List<IStructureFormatter>();
            if(section.SelectNodes("./@aide") != null)
                foreach (XmlNode nd in section.SelectNodes("./@aide"))
                {
                    Type t = Type.GetType(nd.Value);
                    if (t == null)
                        throw new ConfigurationErrorsException(String.Format("Cannot find the type '{0}'", nd.Value));
                    
                    // Get ctor
                    ConstructorInfo ci = t.GetConstructor(Type.EmptyTypes);
                    if (ci == null)
                        throw new ConfigurationErrorsException(String.Format("Cannot find a parameterless constructor on '{0}'", nd.Value));

                    // Construct and cast
                    IStructureFormatter isf = ci.Invoke(null) as IStructureFormatter;
                    if (isf == null)
                        throw new ConfigurationErrorsException(String.Format("The type '{0}' does not implement IStructureFormatter", nd.Value));
                    
                    aides.Add(isf);
                }

            // Add formatter
            XmlNode formatter = section.SelectSingleNode("./@formatter");
            if (formatter == null)
                throw new Exception("You must supply the 'formatter' attribute to the WcfConnector configuration element");

            //Assembly asmFindb = Array.Find<Assembly>(AppDomain.CurrentDomain.GetAssemblies(), a => a.GetType(formatter.Value) != null);
            // Get the formatter type
            Type fmtType = Type.GetType(formatter.Value);
            Formatter = (IStructureFormatter)fmtType.Assembly.CreateInstance(fmtType.FullName);

            if (!(Formatter is IXmlStructureFormatter))
                throw new ArgumentException("The formatter supplied MUST be implement IXmlStructureFormatter");
            
            // Assign aides
            Formatter.GraphAides = aides;
            
            return this;
        }

        #endregion
    }
}