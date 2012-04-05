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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20
{
    /// <summary>
    /// A reference to a datatype definition
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Datatype"), XmlType(TypeName = "DatatypeRef", Namespace = "urn:hl7-org:v3/mif2")]
    public class DatatypeRef
    {

        private string name;
        private List<DatatypeRef> supplierBindingArgumentDatatype;

        /// <summary>
        /// Identifies a datatype to bind the one of the referenced datatypes template parameters
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Datatype"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("argumentDatatype")]
        public List<DatatypeRef> SupplierBindingArgumentDatatype
        {
            get { return supplierBindingArgumentDatatype; }
            set { supplierBindingArgumentDatatype = value; }
        }
	
        /// <summary>
        /// The name of the datatype being referred.
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Parse a string representation of a datatype reference (example: LIST&lt;II&gt;) into a 
        /// proper datatype reference
        /// </summary>
        /// <param name="s">The datatype string to parse</param>
        /// <returns>The parsed datatype reference</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.LastIndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static DatatypeRef Parse(String s)
        {
            DatatypeRef r = new DatatypeRef();

            if (!s.Contains("<")) // Generic?
                r.Name = s;
            else
            {
                r.Name = s.Substring(0, s.IndexOf("<"));
                r.SupplierBindingArgumentDatatype = new List<DatatypeRef>();
                string suppliers = s.Substring(s.IndexOf("<") + 1, s.LastIndexOf(">") - s.IndexOf("<") - 1);
                foreach (string sp in suppliers.Split(','))
                    r.supplierBindingArgumentDatatype.Add(DatatypeRef.Parse(sp));
            }
            return r;
        }
    }
}