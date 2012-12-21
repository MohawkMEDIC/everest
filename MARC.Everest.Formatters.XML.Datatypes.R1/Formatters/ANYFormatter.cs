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
using MARC.Everest.Exceptions;
using MARC.Everest.Xml;
using System.Reflection;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// Summary of ANY
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ANY")]
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
        public void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            ANY instance = o as ANY;
            XmlStateWriter stw = s as XmlStateWriter;

            // Now render
            if (instance == null)
                throw new InvalidOperationException("Could not cast object to the ANY data type");
            else if (instance.NullFlavor != null)
                s.WriteAttributeString("nullFlavor", Util.ToWireFormat(instance.NullFlavor));
            else if (instance.Flavor != null)
            {
                if (result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                    s.WriteAttributeString("specializationType", instance.Flavor);
                else
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Flavor", "ANY", s.ToString()));
            }

            // Validate
            if (result.ValidateConformance && !instance.Validate())
            {
                foreach (var r in instance.ValidateEx())
                {
                    r.Location = s.ToString();
                    result.AddResultDetail(r);
                }
                result.Code = ResultCode.Rejected;
            }
            // Disabled for test
            // Validate flavor... 
            IResultDetail[] flavor;
            if (instance.Flavor != null && result.ValidateConformance && Util.ValidateFlavor(instance.Flavor.ToUpper(), instance, out flavor) == false)
            {
                result.AddResultDetail(new DatatypeFlavorValidationResultDetail(ResultDetailType.Warning, instance.GetType().Name, instance.Flavor, s.ToString()));
                result.AddResultDetail(flavor);
                if (result.Code < ResultCode.AcceptedNonConformant)
                    result.Code = ResultCode.AcceptedNonConformant;
            }

            // Warn if items can't be represented in R1
            if (instance.ControlActExt != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "ControlActExt", "ANY", s.ToString()));
            if(instance.ControlActRoot != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "ControlActRoot", "ANY", s.ToString()));
            if (instance.ValidTimeHigh != null && !(instance is EN))
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "ValidTimeHigh", "ANY", s.ToString()));
            if (instance.ValidTimeLow != null && !(instance is EN))
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "ValidTimeLow", "ANY", s.ToString()));
            if (instance.UpdateMode != null)
                result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "UpdateMode", "ANY", s.ToString()));

        }

        /// <summary>
        /// Parse the object
        /// </summary>
        /// <remarks>ANY can only be used when there is a nullFlavor associated with it</remarks>
        [Obsolete("Use: T Parse<T>(XmlReader s)")]
        public object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            ANY instance = new ANY();
            // Read the NullFlavor, and Specialization data from the wire
            if (s.GetAttribute("nullFlavor") != null) // Stop processing if null flavor is present
                instance.NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));
            else if (s.GetAttribute("specializationType") != null && result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                instance.Flavor = s.GetAttribute("specializationType");

            if (result.ValidateConformance && !instance.Validate())
                foreach (var r in instance.ValidateEx())
                {
                    r.Location = s.ToString();
                    result.AddResultDetail(r);
                }
                //result.AddResultDetail(new DatatypeValidationResultDetail(ResultDetailType.Error, instance.GetType().Name, s.ToString()));
            // Disabled for test
            // Validate flavor... 
            IResultDetail[] flavor;
            if (instance.Flavor != null && result.ValidateConformance && Util.ValidateFlavor(instance.Flavor.ToUpper(), instance, out flavor) == false)
            {
                result.AddResultDetail(new DatatypeFlavorValidationResultDetail(ResultDetailType.Warning, instance.GetType().Name, instance.Flavor, s.ToString()));
                result.AddResultDetail(flavor);
            }

            return instance;
        }

        /// <summary>
        /// Parse ANY object using a concrete implementation of ANY
        /// </summary>
        internal T Parse<T>(System.Xml.XmlReader s, DatatypeFormatterParseResult result) where T : ANY, new()
        {
            T instance = new T();
            // Read the NullFlavor, and Specialization data from the wire
            if (s.GetAttribute("nullFlavor") != null) // Stop processing if null flavor is present
                instance.NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));
            else if (s.GetAttribute("specializationType") != null)
                instance.Flavor = s.GetAttribute("specializationType");

            return instance;
        }

        /// <summary>
        /// Validate the object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
        internal void Validate(ANY instance, string path, DatatypeFormatterParseResult resultDetails)
        {
            IResultDetail[] flavor;

            // Validate
            if (resultDetails.ValidateConformance && !instance.Validate())
                resultDetails.AddResultDetail(new DatatypeValidationResultDetail(ResultDetailType.Error, instance.GetType().Name, path));

            // Validate flavor... 
            if (instance.Flavor != null && resultDetails.ValidateConformance && Util.ValidateFlavor(instance.Flavor.ToUpper(), instance, out flavor) == false)
                resultDetails.AddResultDetail(new DatatypeFlavorValidationResultDetail(ResultDetailType.Warning, instance.GetType().Name, instance.Flavor, path));

            // Append details
        }

        /// <summary>
        /// Handles the type
        /// </summary>
        public virtual string HandlesType
        {
            get { return "ANY"; }
        }

        /// <summary>
        /// Host context
        /// </summary>
        public IXmlStructureFormatter Host { get; set; }


        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public virtual List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(10);
            retVal.Add(typeof(ANY).GetProperty("NullFlavor"));
            return retVal;
        }
        #endregion

    }
}