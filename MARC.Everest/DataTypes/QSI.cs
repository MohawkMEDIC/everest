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
 * Date: 09-22-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Attributes;
using System.Xml.Serialization;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using MARC.Everest.Interfaces;
using System.ComponentModel;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Represents a <see cref="T:QSET{T}"/> that has been specialized as an intersection of other sets.
    /// </summary>
    /// <seealso cref="T:QSET{T}"/>
    /// <seealso cref="T:QSD{T}"/>
    /// <seealso cref="T:QSI{T}"/>
    /// <seealso cref="T:QSP{T}"/>
    /// <seealso cref="T:QSS{T}"/>
    /// <seealso cref="T:QSU{T}"/>
    /// <seealso cref="T:SXPR{T}"/>
    /// <seealso cref="T:SXCM{T}"/>
    /// <seealso cref="T:GTS"/>
    [Structure(Name = "QSI", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [XmlType("QSI", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class QSI<T> : QSC<T>, ICollection<ISetComponent<T>>, IListContainer
        where T : IAny
    {

        /// <summary>
        /// Creates a new instance of the QSET intersection class
        /// </summary>
        public QSI() : base() { this.Terms = new List<ISetComponent<T>>(); }

        /// <summary>
        /// Creates a new instance of the QSET intersection class containing the specified sets
        /// </summary>
        /// <param name="terms">The terms contained within the union</param>
        public QSI(IEnumerable<ISetComponent<T>> terms)
        {
            this.Terms = new List<ISetComponent<T>>(terms);
        }

        /// <summary>
        /// Creates a new instance of the QSI class with the specified <paramref name="terms"/>
        /// </summary>
        public static QSI<T> CreateQSI(params ISetComponent<T>[] terms)
        {
            return new QSI<T>(terms);
        }


        /// <summary>
        /// Extended validation routine which returns a list of detected issues
        /// </summary>
        /// <remarks>An instance of QSET is considered valid when it contains at least two items and no property contains
        /// a null component</remarks>
        public override IEnumerable<IResultDetail> ValidateEx()
        {
            List<IResultDetail> retVal = new List<IResultDetail>();

            if (!((this.NullFlavor != null) ^ (this.Terms.Count > 0)))
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSI", ValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
            else if(this.Terms.Count < 2)
                retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSI", "QSI must contain at least two items for intersection", null));
            foreach (var qs in Terms ?? new List<ISetComponent<T>>())
                if (qs == null || qs.IsNull)
                    retVal.Add(new DatatypeValidationResultDetail(ResultDetailType.Error, "QSI", ValidationMessages.MSG_NULL_COLLECTION_VALUE, null));
            return retVal;
        }

        /// <summary>
        /// Get equivalent set operator
        /// </summary>
        protected override SetOperator GetEquivalentSetOperator()
        {
            return SetOperator.Intersect;
        }

        /// <summary>
        /// Parse from another instance of QSI. Used for formatters
        /// </summary>
        internal static QSI<T> Parse(QSI<ISetComponent<IAny>> other)
        {
            QSI<T> retVal = new QSI<T>();
            retVal.NullFlavor = other.NullFlavor.Clone() as CS<NullFlavor>;
            retVal.ControlActExt = other.ControlActExt;
            retVal.ControlActRoot = other.ControlActRoot;
            retVal.Flavor = other.Flavor;
            retVal.OriginalText = other.OriginalText.Clone() as ED;
            foreach (ISetComponent<IAny> itm in other)
                retVal.Add((ISetComponent<T>)itm);
            return retVal;
        }

        /// <summary>
        /// Normalizes the QSI to include only QS* content
        /// </summary>
        public override IGraphable Normalize()
        {
            var retVal = this.Clone() as QSI<T>;
            retVal.Terms = new List<ISetComponent<T>>(this.Terms);
            for (int i = 0; i < retVal.Terms.Count; i++)
                if (retVal.Terms[i] is SXPR<T>)
                    retVal.Terms[i] = (retVal.Terms[i] as SXPR<T>).TranslateToQSET();
            return retVal;
        }
    }
}
