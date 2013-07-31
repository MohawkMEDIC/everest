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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Parses a datatype reference (for use in CMETS) from MIF 1.0 datatype refs into an actual type reference in COR
    /// </summary>
    internal class TypeReferenceParser 
    {
        /// <summary>
        /// Private constructor so the compiler will not create a default one for this class.
        /// </summary>
        private TypeReferenceParser() { }
        /// <summary>
        /// Parse a datatype reference into a type refernce
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
        public static MohawkCollege.EHR.gpmr.COR.TypeReference Parse(DatatypeRef dtr)
        {
            MohawkCollege.EHR.gpmr.COR.TypeReference retVal = new MohawkCollege.EHR.gpmr.COR.TypeReference();

            retVal.Name = dtr.Name.Contains(".") ? dtr.Name.Substring(0, dtr.Name.IndexOf(".")) : dtr.Name;
            retVal.Flavor = dtr.Name.Contains(".") ? dtr.Name : null; // JF: Fixed the removal of the . 

            if (dtr.SupplierBindingArgumentDatatype != null && dtr.SupplierBindingArgumentDatatype.Count > 0)
            {
                retVal.GenericSupplier = new List<MohawkCollege.EHR.gpmr.COR.TypeReference>();
                foreach (DatatypeRef sba in dtr.SupplierBindingArgumentDatatype)
                    retVal.GenericSupplier.Add(TypeReferenceParser.Parse(sba));
            }

            return retVal;
        }

        /// <summary>
        /// Parse a type reference from a string
        /// </summary>
        /// <param name="p">The string to parse</param>
        /// <returns>The processed type parameter</returns>
        internal static MohawkCollege.EHR.gpmr.COR.TypeReference Parse(string p)
        {

            DatatypeRef r = DatatypeRef.Parse(p);
            return Parse(r);
        }
    }
}