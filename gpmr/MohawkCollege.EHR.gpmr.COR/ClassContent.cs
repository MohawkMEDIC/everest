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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Identifies a feature that can be represented as the contents of a class. 
    /// This includes:
    ///     Properties (code, id, etc...)
    ///     Choice Properties (patient1, patient2, patient3)
    /// </summary>
    public abstract class ClassContent : Feature
    {

        private string minOccurs;
        private string maxOccurs;
        private Feature container;

        /// <summary>
        /// Get a list of all the title documentation for this item
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Documentation.TitledDocumentation> TitledDocumentation
        {
            get
            {
                List<Documentation.TitledDocumentation> retVal = new List<Documentation.TitledDocumentation>();
                if(Documentation != null && Documentation.Appendix != null)
                    retVal.AddRange(Documentation.Appendix);
                if (Documentation != null && Documentation.Other != null)
                    retVal.AddRange(Documentation.Other);
                return retVal.Count > 0 ? retVal : null;
            }
        }

        /// <summary>
        /// Comparator for a class property
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class Comparator : IComparer<ClassContent>
        {
            #region IComparer<Property> Members

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "1#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
            public int Compare(ClassContent a, ClassContent b)
            {

                // Corrections
                if (b.SortKey == null) b.SortKey = new string('9', 20);
                if (a.SortKey == null) a.SortKey = new string('9', 20);

                //if (a.Name == "asPayeeRelationshipRole")
                //    System.Diagnostics.Debugger.Break();

                // Are the sort keys numeric?
                int xSortKeyInt = 0;
                int ySortKeyInt = 0;
                if (Int32.TryParse(a.SortKey, out xSortKeyInt) && Int32.TryParse(b.SortKey, out ySortKeyInt))
                {
                    if (a is Property) xSortKeyInt *= (int)(a as Property).PropertyType * 200; // Modify for attributes, structural
                    if (b is Property) ySortKeyInt *= (int)(b as Property).PropertyType * 200; // and assocs

                    if(!(!(a is Property) ||
                        (a as Property).PropertyType == (b as Property).PropertyType ||
                        ((a as Property).PropertyType == Property.PropertyTypes.Structural && xSortKeyInt <= ySortKeyInt ||
                        (b as Property).PropertyType == Property.PropertyTypes.Structural && ySortKeyInt <= xSortKeyInt)))
                        System.Diagnostics.Trace.WriteLine(string.Format("Assertion failed comparator comparison may not work as expected ... a=({0},{1}), b=({2},{3})",
                        (a as Property).PropertyType, xSortKeyInt, (b as Property).PropertyType, ySortKeyInt), "debug");

                    return xSortKeyInt.CompareTo(ySortKeyInt);
                }
                else if (a.SortKey == b.SortKey) // Fixes a sort bug where the sort key is identical
                    return string.CompareOrdinal(a.Name, b.Name);
                else
                {
                    
                    //return ordComp < 0 ? -1 : ordComp > 0 ? 1 : 0;
                    return a.SortKey.CompareTo(b.SortKey);
                }
            }

            #endregion
        }

        /// <summary>
        /// Identifies the property in the derivation supplier that this property realizes
        /// </summary>
        public List<ClassContent> Realization { get; set; }

        /// <summary>
        /// The class that contains this content
        /// </summary>
        public virtual Feature Container
        {
            get { return container; }
            set { container = value; }
        }
	
        /// <summary>
        /// Identifies conformance levels
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum ConformanceKind
        {
            Optional = 1,
            Required = 2,
            Mandatory = 4,
            Populated = 8
        }

        private ConformanceKind conformance = ConformanceKind.Optional;

        /// <summary>
        /// Identifies the type of conformance
        /// </summary>
        public ConformanceKind Conformance
        {
            get { return conformance; }
            set { conformance = value; }
        }
	
        /// <summary>
        /// Get the maximum occurences of this feature
        /// </summary>
        public string MaxOccurs
        {
            get { return maxOccurs; }
            set { maxOccurs = value; }
        }
	
        /// <summary>
        /// Get the minimum occurence of this feature
        /// </summary>
        public string MinOccurs
        {
            get { return minOccurs; }
            set { minOccurs = value; }
        }
	

    }
}
