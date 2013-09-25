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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// Common ancestor for attributes and datatype properties
    /// </summary>
    [XmlType(TypeName = "StructuralFeature", Namespace = "urn:hl7-org:v3/mif2")]
    public class Feature : ModelElement
    {
        private string defaultValue;
        private DefaultDeterminerKind defaultForm = DefaultDeterminerKind.Realm;
        private string fixedValue;
        private int? minimumLength;
        private int? maximumLength;
        private int? minimumMultiplicity;
        private int? maximumMultiplicity;
        private UpdateModeKind updateModeDefault = UpdateModeKind.Unknown;
        private string updateModesAllowed;
        private bool referenceHistory = false;
        private bool isMandatory = false;
        private ConformanceKind conformance = ConformanceKind.Optional;

        /// <summary>
        /// Identifies whether the element must be supported by implementers or not. If not present, indicates that 
        /// support is optional.
        /// </summary>
        [XmlAttribute("conformance")]
        public ConformanceKind Conformance
        {
            get { return conformance; }
            set { conformance = value; }
        }
	
        /// <summary>
        /// If true, null values may not be sent for this element
        /// </summary>
        [XmlAttribute("isMandatory")]
        public bool IsMandatory
        {
            get { return isMandatory; }
            set { isMandatory = value; }
        }
	

        /// <summary>
        /// Indicates that the element can include  a reference to its history
        /// </summary>
        [XmlAttribute("referenceHistory")]
        public bool ReferenceHistory
        {
            get { return referenceHistory; }
            set { referenceHistory = value; }
        }
	

        /// <summary>
        /// Identifies the list of update modes that may be used for this element
        /// </summary>
        [XmlAttribute("updateModesAllowed")]
        public string UpdateModesAllowed
        {
            get { return updateModesAllowed; }
            set { updateModesAllowed = value; }
        }
	

        /// <summary>
        /// Identifies the update mode that should be assumed if no updateMode is specified
        /// </summary>
        [XmlAttribute("updateModeDefault")]
        public UpdateModeKind UpdateModeDefault
        {
            get { return updateModeDefault; }
            set { updateModeDefault = value; }
        }
	

        /// <summary>
        /// Identifies the maximum number of repititions of this element that may occur within the containing
        /// element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.String)"), XmlAttribute("maximumMultiplicity")]
        public string MaximumMultiplicity
        {
            get { return maximumMultiplicity == null ? null : maximumMultiplicity.ToString(); }
            set { maximumMultiplicity = value == null ? (int?)null : value == "*" ? -1 : Convert.ToInt32(value); }
        }
	
        /// <summary>
        /// Identifies the minimum number of repetitions of this element that may occur within the containing element
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.String)"), XmlAttribute("minimumMultiplicity")]
        public string MinimumMultiplicity
        {
            get { return minimumMultiplicity == null ? null : minimumMultiplicity.ToString(); }
            set { minimumMultiplicity = value == null ? (int?)null : Convert.ToInt32(value); }
        }
	
        /// <summary>
        /// Identifies the maximum number of characters that are permitted to be present
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.String)"), XmlAttribute("maximumLength")]
        public string MaximumLength
        {
            get { return maximumLength == null ? null : maximumLength.ToString(); }
            set { maximumLength = value == null ? (int?)null : value == "*" ? -1 : Convert.ToInt32(value); }
        }
	

        /// <summary>
        /// Identifies the minimum number of characters that must be present
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.String)"), XmlAttribute("minimumLength")]
        public string MinimumLength
        {
            get { return minimumLength == null ? null : minimumLength.ToString(); }
            set { minimumLength = value == null ? (int?)null : Convert.ToInt32(value); }
        }

        /// <summary>
        /// Identifies the default value for the element if it is not present in an instance
        /// of the model
        /// </summary>
        [XmlAttribute("fixedValue")]
        public string FixedValue
        {
            get { return fixedValue; }
            set { fixedValue = value; }
        }

        /// <summary>
        /// Indicates how a default value should be inferred
        /// </summary>
        [XmlAttribute("defaultFrom")]
        public DefaultDeterminerKind DefaultFrom
        {
            get { return defaultForm; }
            set { defaultForm = value; }
        }
	
        /// <summary>
        /// Identifies the derfault value for the element if it is not present in an instance of the model
        /// </summary>
        [XmlAttribute("defaultValue")]
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
	
    }
}