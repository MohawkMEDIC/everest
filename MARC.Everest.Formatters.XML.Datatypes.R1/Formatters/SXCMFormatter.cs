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
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using System.Reflection;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Formatter for the SXCM type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SXCM")]
    public class SXCMFormatter : PDVFormatter
    {
        /// <summary>
        /// Host of this formatter
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }

        /// <summary>
        /// Parse an SXCM
        /// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        //public SXCM<T> Parse<T>(System.Xml.XmlReader s, DatatypeFormatterParseResult result) where T :  IAny, new()
        //{
        //    // Return value
        //    //SXCM<T> retVal = base.Parse<SXCM<TS>>(s result);

        //    //// Operator
        //    //if (s.GetAttribute("operator") != null)
        //    //    retVal.Operator = (SetOperator?)Util.FromWireFormat(s.GetAttribute("operator"), typeof(SetOperator));

        //    //return retVal;
        //    //result.AddResultDetail(new NotImplementedResultDetail(ResultDetailType.Error, "SXCM is an abstract class and cannot be instantiated by itself", s.ToString(), null));
        //    //return null;
        //}

        #region IDatatypeFormatter Members

        
        /// <summary>
        /// Graph the SXCM to the console
        /// </summary>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            // Output the object
            Type t = o.GetType();
            ANY any = (ANY)o;
            object oper = t.GetProperty("Operator").GetValue(o, null),
                valu = t.GetProperty("Value").GetValue(o, null);
            if (oper != null && any.NullFlavor == null)
                s.WriteAttributeString("operator", Util.ToWireFormat(oper));
            // Format the object
            var formatter = DatatypeFormatter.GetFormatter(valu.GetType());
            formatter.Host = this.Host; 
            formatter.Graph(s, valu, result);
        }

        /// <summary>
        /// Parse an SXCM from the wire
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Determine the generic type formatter
            var formatter = DatatypeFormatter.GetFormatter(GenericArguments[0]);

            // Create the return value
            Type sxcmType = typeof(SXCM<>);
            Type genType = sxcmType.MakeGenericType(GenericArguments);
            ConstructorInfo ci = genType.GetConstructor(Type.EmptyTypes);
            Object retVal = null;
            if (ci != null)
                retVal = ci.Invoke(null);
            else
                throw new ArgumentException("Constructor on type must have a parameterless constructor");

            // Operator
            if (s.GetAttribute("operator") != null)
                genType.GetProperty("Operator").SetValue(retVal, Util.FromWireFormat(s.GetAttribute("operator"), typeof(SetOperator)), null);
            // Value
            if (formatter != null)
            {
                formatter.Host = this.Host;
                var value = formatter.Parse(s, result);
                genType.GetProperty("Value").SetValue(retVal, value, null);

                if (value != null)
                {
                    ((ANY)retVal).NullFlavor = ((ANY)value).NullFlavor;
                    ((ANY)retVal).Flavor = ((ANY)value).Flavor;
                    ((ANY)value).NullFlavor = null;
                    ((ANY)value).Flavor = null;
                }
            }

            
            return retVal;
        }

        /// <summary>
        /// Handles SXCM
        /// </summary>
        public override string HandlesType
        {
            get { return "SXCM"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(SXCM<>).GetProperty("Operator"));
            retVal.Add(typeof(SXCM<>).GetProperty("Value"));
            return retVal;
        }
        #endregion
    }
}