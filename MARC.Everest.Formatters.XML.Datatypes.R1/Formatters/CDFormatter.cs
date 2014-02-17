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
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formatter for the CD Datatype
    /// </summary>
    public class CDFormatter : CEFormatter, IDatatypeFormatter
    {
        #region IDatatypeFormatter Members

      
        /// <summary>
        /// Graph object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Get an instance ref
            IConceptDescriptor instance_ics = (IConceptDescriptor)o;
             
            // Do a base format
            base.Graph(s, o, result);

            // Format the coded simple
            if (instance_ics.Qualifier != null) // Original Text
            {
                foreach (IGraphable ig in instance_ics.Qualifier)
                {
                    s.WriteStartElement("qualifier", null);
                    var hostResult = Host.Graph(s, ig);
                    result.AddResultDetail(hostResult.Details);
                    result.Code = hostResult.Code;
                    s.WriteEndElement();
                }
            }

        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to read from</param>
        /// <remarks>Because the way the stream operates, we need to duplicate code from the CV formatter</remarks>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            IResultDetail[] details = null;
            CD<String> retVal = CDFormatter.Parse<CD<String>>(s, Host, result);
            return retVal;
        }

        /// <summary>
        /// Get the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "CD"; }
        }

      

        #endregion
        //DOC: Documentation Required
        /// <summary>
        /// Parse a <typeparamref name="T"/> object from the stream
        /// </summary>
        /// <typeparam name="T">The type of object to parse</typeparam>
        /// <param name="s">The stream to parse from</param>
        /// <param name="host"></param>
        /// <param name="details">Details of the parse operation.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        internal static T Parse<T>(System.Xml.XmlReader s, IXmlStructureFormatter host, DatatypeFormatterParseResult result) where T : ANY, ICodedValue, new()
        {
            // Parse base (ANY) from the stream
            ANYFormatter anyFormatter = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;

            // Parse CV
            anyFormatter.Host = host;
            T retVal =  anyFormatter.Parse<T>(s, result);

            // Now parse our data out... Attributes
            // Was there a null flavor processed?
            if (s.GetAttribute("code") != null)
                retVal.CodeValue = s.GetAttribute("code");
            if (s.GetAttribute("codeSystem") != null)
                retVal.CodeSystem = s.GetAttribute("codeSystem");
            if (s.GetAttribute("codeSystemName") != null)
                retVal.CodeSystemName = s.GetAttribute("codeSystemName");
            if (s.GetAttribute("codeSystemVersion") != null)
                retVal.CodeSystemVersion = s.GetAttribute("codeSystemVersion");
            if (s.GetAttribute("displayName") != null)
                retVal.DisplayName = Util.Convert<ST>(s.GetAttribute("displayName"));

            // Elements
            #region Elements
            if (!s.IsEmptyElement)
            {

                int sDepth = s.Depth;
                string sName = s.Name;

                s.Read();
                // string Name
                while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                {
                    string oldName = s.Name; // Name
                    try
                    {
                        if (s.LocalName == "originalText") // Format using ED
                        {
                            EDFormatter edFormatter = new EDFormatter();
                            edFormatter.Host = host;
                            retVal.OriginalText = (ED)edFormatter.Parse(s, result); // Parse ED
                        }
                        else if (s.LocalName == "translation" && retVal is ICodedEquivalents) // Translation
                        {
                            LISTFormatter setFormatter = new LISTFormatter();
                            setFormatter.GenericArguments = new Type[] { typeof(CD<String>) };
                            setFormatter.Host = host;
                            if (retVal is ICodedEquivalents)
                                ((ICodedEquivalents)retVal).Translation = (LIST<IGraphable>)setFormatter.Parse(s, result); // Parse LIST
                            else
                                result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
                        }
                        else if (s.LocalName == "qualifier" && retVal is IConceptDescriptor) // Qualifier
                        {
                            SETFormatter setFormatter = new SETFormatter();
                            setFormatter.GenericArguments = new Type[] { typeof(CR<String>) };
                            setFormatter.Host = host;
                            if(retVal is IConceptDescriptor)
                                ((IConceptDescriptor)retVal).Qualifier = (LIST<IGraphable>)setFormatter.Parse(s, result); // Parse SET
                            else
                                result.AddResultDetail(new NotImplementedElementResultDetail(ResultDetailType.Warning, s.LocalName, s.NamespaceURI, s.ToString(), null));
                        }
                    }
                    catch (VocabularyException e)
                    {
                        result.AddResultDetail(new VocabularyIssueResultDetail(ResultDetailType.Error, e.Message, e));
                    }
                    catch (MessageValidationException e)
                    {
                        result.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, s.ToString(), e));
                    }
                    finally
                    {
                        if (s.Name == oldName) s.Read();
                    }
                }
            }
            #endregion


            // Validate
            anyFormatter.Validate(retVal, pathName, result);


            // Add validation to details
            return retVal;
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(CD<>).GetProperty("Qualifier"));
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;
        }
    }
}
