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
 * Date: 06-24-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using System.Reflection;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Formatters.XML.Datatypes.R2.Formatters
{
    /// <summary>
    /// Formatter for the HXIT datatype
    /// </summary>
    public class ANYFormatter : IDatatypeFormatter
    {

        #region IDatatypeFormatter Members

        /// <summary>
        /// Create a new instance of ANYFormatter
        /// </summary>
        public ANYFormatter() {  }

        /// <summary>
        /// Get or set the generic arguments to this type (if applicable)
        /// </summary>
        public Type[] GenericArguments { get; set; }

        /// <summary>
        /// Graph the object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
        public void Graph(System.Xml.XmlWriter s, object o ,DatatypeR2FormatterGraphResult result)
        {
            ANY instance = o as ANY;

            IResultDetail[] enumer = new IResultDetail[0], flavor = new IResultDetail[0],
                invalidAttributes = new IResultDetail[0];

            // Now render
            if (instance == null)
                throw new InvalidOperationException("Could not cast object to the ANY data type");

            if (instance.Flavor != null &&
                    (instance.NullFlavor == null || 
                    (instance is ICodedSimple && ((NullFlavor)instance.NullFlavor).IsChildConcept(NullFlavor.Other)) || 
                    (instance is IQuantity && ((NullFlavor)instance.NullFlavor).IsChildConcept(NullFlavor.Derived))
                    )
                )
                s.WriteAttributeString("flavorId", instance.Flavor);

            if (instance.NullFlavor != null)
                s.WriteAttributeString("nullFlavor", Util.ToWireFormat(instance.NullFlavor));
            else
            {
                
                
                // Warn if items can't be represented in R2
                if (instance.ControlActExt != null)
                    s.WriteAttributeString("controlInformationExtension", instance.ControlActExt);
                if (instance.ControlActRoot != null)
                    s.WriteAttributeString("controlInformationRoot", instance.ControlActRoot);
                if (instance.ValidTimeHigh != null && !instance.ValidTimeHigh.IsNull)
                    s.WriteAttributeString("validTimeHigh", instance.ValidTimeHigh.Value);
                if (instance.ValidTimeLow != null && !instance.ValidTimeLow.IsNull)
                    s.WriteAttributeString("validTimeLow", instance.ValidTimeLow.Value);
                if (instance.UpdateMode != null && !instance.UpdateMode.IsNull)
                    s.WriteAttributeString("updateMode", Util.ToWireFormat(instance.UpdateMode));

            }

            string currentElementName = "";
            if (s is MARC.Everest.Xml.XmlStateWriter)
                currentElementName = (s as MARC.Everest.Xml.XmlStateWriter).CurrentPath;

            // Validate
            if (result.ValidateConformance && !instance.Validate())
            {
                result.AddResultDetail(new DatatypeValidationResultDetail(ResultDetailType.Error, o.GetType().Name, currentElementName));
            }

            // Disabled for test
            // Validate flavor... 
            if (result.ValidateConformance && instance.Flavor != null && Util.ValidateFlavor(instance.Flavor.ToUpper(), instance, out flavor) == false)
            {
                result.AddResultDetail(new DatatypeFlavorValidationResultDetail(ResultDetailType.Warning, instance.GetType().Name, instance.Flavor, currentElementName));
                result.AddResultDetail(flavor);
            }

        }

        /// <summary>
        /// Parse the object
        /// </summary>
        [Obsolete("Use: T Parse<T>(XmlReader s)")]
        public object Parse(System.Xml.XmlReader s, DatatypeR2FormatterParseResult result)
        {
            return Parse<ANY>(s);
        }

        /// <summary>
        /// Parse ANY object using a concrete implementation of ANY
        /// </summary>
        internal T Parse<T>(System.Xml.XmlReader s) where T : ANY, new()
        {
            T instance = new T();
            // Read the NullFlavor, and Specialization data from the wire
            if (s.GetAttribute("nullFlavor") != null) // Stop processing if null flavor is present
                instance.NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));
            else
            {
                if (s.GetAttribute("flavorId") != null)
                    instance.Flavor = s.GetAttribute("flavorId");
                if (s.GetAttribute("validTimeLow") != null)
                    instance.ValidTimeLow = (TS)s.GetAttribute("validTimeLow");
                if (s.GetAttribute("validTimeHigh") != null)
                    instance.ValidTimeHigh = (TS)s.GetAttribute("validTimeHigh");
                if (s.GetAttribute("controlInformationRoot") != null)
                    instance.ControlActRoot = s.GetAttribute("controlInformationRoot");
                if (s.GetAttribute("controlInformationExtension") != null)
                    instance.ControlActExt = s.GetAttribute("controlInformationExtension");
                if (s.GetAttribute("updateMode") != null)
                    instance.UpdateMode = Util.Convert<UpdateMode>(s.GetAttribute("updateMode"));

            }

            // Create empty details
            return instance;
        }

        /// <summary>
        /// Validate the object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
        internal void Validate(ANY instance, string path, DatatypeR2FormatterParseResult resultDetails)
        {
            IResultDetail[] flavor;
            // Validate
            if (resultDetails.ValidateConformance && !instance.Validate())
                resultDetails.AddResultDetail(new DatatypeValidationResultDetail(ResultDetailType.Error, instance.GetType().Name, path));

            // Validate flavor... 
            if (resultDetails.ValidateConformance && instance.Flavor != null && Util.ValidateFlavor(instance.Flavor.ToUpper(), instance, out flavor) == false)
                resultDetails.AddResultDetail(new DatatypeFlavorValidationResultDetail(ResultDetailType.Warning, instance.GetType().Name, instance.Flavor, path));

        }

        /// <summary>
        /// Handles the type
        /// </summary>
        public string HandlesType
        {
            get { return "ANY"; }
        }

        /// <summary>
        /// Host context
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }
        #endregion

        #region IDatatypeFormatter Members

        /// <summary>
        /// Return all supported properties
        /// </summary>
        /// <returns></returns>
        public List<System.Reflection.PropertyInfo> GetSupportedProperties()
        {
            return new List<System.Reflection.PropertyInfo>(typeof(ANY).GetProperties(BindingFlags.Instance | BindingFlags.Public));
        }

        #endregion
    }
}
