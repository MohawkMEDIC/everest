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
using MARC.Everest.Attributes;
using System.Xml.Serialization;

namespace MARC.Everest.DataTypes
{

    /// <summary>
    /// Null Flavor utility
    /// </summary>
    public static class NullFlavorUtil
    {
        // Null flavor heirarchy
        private static Dictionary<NullFlavor, NullFlavor> m_heirarchy = new Dictionary<NullFlavor, NullFlavor>()
            {
                { NullFlavor.Invalid, NullFlavor.NoInformation },
                { NullFlavor.Other, NullFlavor.Invalid },
                { NullFlavor.PositiveInfinity, NullFlavor.Other },
                { NullFlavor.NegativeInfinity, NullFlavor.Other },
                { NullFlavor.UnEncoded, NullFlavor.Invalid },
                { NullFlavor.Derived, NullFlavor.Invalid },
                { NullFlavor.Unknown, NullFlavor.NoInformation },
                { NullFlavor.AskedUnknown, NullFlavor.Unknown },
                { NullFlavor.Unavailable, NullFlavor.AskedUnknown }, 
                { NullFlavor.NotAsked, NullFlavor.Unknown },
                { NullFlavor.SufficientQuantity, NullFlavor.Unknown },
                { NullFlavor.Trace, NullFlavor.Unknown },
                { NullFlavor.Masked, NullFlavor.NoInformation },
                { NullFlavor.NotApplicable, NullFlavor.NoInformation }
            };

        /// <summary>
        /// Returns true if <paramref name="a"/> implies <paramref name="implied"/>
        /// </summary>
        public static bool IsChildConcept(this NullFlavor a, NullFlavor parent)
        {
            NullFlavor parentAttempt = a;
            do
            {
                if (parentAttempt == parent)
                    return true;
            } while (m_heirarchy.TryGetValue(parentAttempt, out parentAttempt));
            return false;
        }

        /// <summary>
        /// Gets a common ancestor
        /// </summary>
        public static NullFlavor GetCommonParent(this NullFlavor a, NullFlavor other)
        {
            NullFlavor parentAttempt = a;
            do
            {
                if (other.IsChildConcept(parentAttempt))
                    return parentAttempt;
            } while (m_heirarchy.TryGetValue(parentAttempt, out parentAttempt));
            return NullFlavor.NoInformation;
        }

        /// <summary>
        /// Gets a common ancestor
        /// </summary>
        public static NullFlavor GetCommonParent(this CS<NullFlavor> a, CS<NullFlavor> other)
        {
            return NullFlavorUtil.GetCommonParent((NullFlavor)a, (NullFlavor)other);
        }
    }

    /// <summary>
    /// Indicates why a value is not present.
    /// </summary>
    [Structure(Name = "NullFlavor", CodeSystem = "2.16.840.1.113883.5.1008", StructureType = StructureAttribute.StructureAttributeType.ValueSet, Publisher = "Health Level 7 International")]
    [XmlType("NullFlavor", Namespace = "urn:hl7-org:v3")]
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public enum NullFlavor
    {
        /// <summary>
        /// Information was sought but not found (e.g., patient was asked but didn't know).
        /// </summary>
        [Enumeration(Value = "ASKU")]
        [XmlEnum("ASKU")]
        AskedUnknown,
        /// <summary>
        /// An actual value may exist, but it must be derived from the provided information (usually an EXPR generic data type extension will be used to convey the derivation expressionexpression ..
        /// </summary>
        [Enumeration(Value = "DER")]
        [XmlEnum("DER")]
        Derived,
        /// <summary>
        /// The value as represented in the instance is not a member of the set of permitted data values in the constrained value domain of a variable..
        /// </summary>
        [Enumeration(Value = "INV")]
        [XmlEnum("INV")]
        Invalid,
        /// <summary>
        /// There is information on this item available but it has not been provided by the sender due to security, privacy or other reasons. There may be an alternate mechanism for gaining access to this information. Note: using this null flavor does provide information that may be a breach of confidentiality, even though no detail data is provided. Its primary purpose is for those circumstances where it is necessary to inform the receiver that the information does exist without providing any detail. .
        /// </summary>
        [Enumeration(Value = "MSK")]
        [XmlEnum("MSK")]
        Masked,
        /// <summary>
        /// Known to have no proper value (e.g., last menstrual period for a male)..
        /// </summary>
        [Enumeration(Value = "NA")]
        [XmlEnum("NA")]
        NotApplicable,
        /// <summary>
        /// This information has not been sought (e.g., patient was not asked).
        /// </summary>
        [Enumeration(Value = "NASK")]
        [XmlEnum("NASK")]
        NotAsked,
        /// <summary>
        /// Information is not available at this time but it is expected that it will be available later..
        /// </summary>
        [Enumeration(Value = "NAV")]
        [XmlEnum("NAV")]
        Unavailable,
        /// <summary>
        /// No information whatsoever can be inferred from this exceptional value. This is the most general exceptional value. It is also the default exceptional value..
        /// </summary>
        [Enumeration(Value = "NI")]
        [XmlEnum("NI")]
        NoInformation,
        /// <summary>
        /// Negative infinity of numbers..
        /// </summary>
        [Enumeration(Value = "NINF")]
        [XmlEnum("NINF")]
        NegativeInfinity,
        /// <summary>
        /// The actual value is not an element in the value domain of a variable. (e.g., concept not provided by required code system)..
        /// </summary>
        [Enumeration(Value = "OTH")]
        [XmlEnum("OTH")]
        Other,
        /// <summary>
        /// Positive infinity of numbers..
        /// </summary>
        [Enumeration(Value = "PINF")]
        [XmlEnum("PINF")]
        PositiveInfinity,
        /// <summary>
        /// The specific quantity is not known, but is known to be non-zero and is not specified because it makes up the bulk of the material. 'Add 10mg of ingredient X, 50mg of ingredient Y, and sufficient quantity of water to 100mL.' The null flavor would be used to express the quantity of water. .
        /// </summary>
        [Enumeration(Value = "QS")]
        [XmlEnum("QS")]
        SufficientQuantity,
        /// <summary>
        /// The content is greater than zero, but too small to be quantified..
        /// </summary>
        [Enumeration(Value = "TRC")]
        [XmlEnum("TRC")]
        Trace,
        /// <summary>
        /// The actual value has not yet been encoded within the approved valueset for the domain..
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Un")]
        [Enumeration(Value = "UNC")]
        [XmlEnum("UNC")]
        UnEncoded,
        /// <summary>
        /// A proper value is applicable, but not known..
        /// </summary>
        [Enumeration(Value = "UNK")]
        [XmlEnum("UNK")]
        Unknown
    }
}