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

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Serialized
{
    /// <summary>
    /// An association that points to an association end attached to a class
    /// </summary>
    [XmlType(TypeName = "SerializedAssociationEnd", Namespace = "urn:hl7-org:v3/mif2")]
    public class SerializedAssociationEnd : AssociationBase
    {
        /// <summary>
        /// Comparator
        /// </summary>
        //TODO: add more explanation for Comparator
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class Comparator : IComparer<SerializedAssociationEnd>
        {
            #region IComparer<SerializedAssociationEnd> Members
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)")]
            public int Compare(SerializedAssociationEnd x, SerializedAssociationEnd y)
            {
                return x.SortKey.CompareTo(y.SortKey);
            }

            #endregion
        }

        private AssociationEndWithClass targetConnection;
        private SerializedAssociationEnds sourceConnection;

        /// <summary>
        /// The details of the association in the opposite direction
        /// </summary>
        [XmlElement("sourceConnection")]
        public SerializedAssociationEnds SourceConnection
        {
            get { return sourceConnection; }
            set { sourceConnection = value; }
        }
	
        /// <summary>
        /// The end at the opposite end of the association
        /// </summary>
        [XmlElement("targetConnection")]
        public AssociationEndWithClass TargetConnection
        {
            get { return targetConnection; }
            set { targetConnection = value; }
        }

    }
}