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
using System.ComponentModel;

namespace MARC.Everest.DataTypes.Interfaces
{


    /// <summary>
    /// Identifies mechanisms that are supported by a telecommunications address
    /// </summary>
    [Structure(Name = "TelecommunicationCabability", CodeSystem = "2.16.840.1.113883.5.1118", StructureType = StructureAttribute.StructureAttributeType.ValueSet, Publisher = "Health Level 7 International")]

    public enum TelecommunicationCabability
    {
        /// <summary>
        /// The device can receive voice calls
        /// </summary>
        [Enumeration(Value = "voice")]
        Voice,
        /// <summary>
        /// The device can receive fax data
        /// </summary>
        [Enumeration(Value = "fax")]
        Fax,
        /// <summary>
        /// The device can receive a data stream
        /// </summary>
        [Enumeration(Value = "data")]
        Data,
        /// <summary>
        /// The device can receive text telephone calls 
        /// </summary>
        [Enumeration(Value = "text")]
        Text,
        /// <summary>
        /// The device can receive SMS messages
        /// </summary>
        [Enumeration(Value = "sms")]
        SMS
    }

    /// <summary>
    /// Identifies ways in which a telecommunications address can be used
    /// </summary>
    [Structure(Name = "TelecommunicationsAddressUse", CodeSystem = "2.16.840.1.113883.5.1011", StructureType = StructureAttribute.StructureAttributeType.ValueSet, Publisher = "Health Level 7 International")]
    public enum TelecommunicationAddressUse
    {
        /// <summary>
        /// Identifies a communication address at a home. Should not be used 
        /// for business purposes.
        /// </summary>
        [Enumeration(Value = "H")]
        Home,
        /// <summary>
        /// The primary address to reach a contact after business hours
        /// </summary>
        [Enumeration(Value = "HP")]
        PrimaryHome,
        /// <summary>
        /// A vacation home to reach a person while on vacation
        /// </summary>
        [Enumeration(Value = "HV")]
        VacationHome,
        /// <summary>
        /// An office address, should be used for business communications
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "WorkPlace")]
        [Enumeration(Value = "WP")]
        WorkPlace,
        /// <summary>
        /// Indicates a workplace address that reaches the person directly without
        /// intermediaries (for example a private line)
        /// </summary>
        [Enumeration(Value = "DIR")]
        Direct,
        /// <summary>
        /// Indicates an address that is a standard address that may be 
        /// subject to a switchboard or operator prior to reaching the intended
        /// entity.
        /// </summary>
        [Enumeration(Value = "PUB")]
        Public,
        /// <summary>
        /// When set, indicates that an address is bad and is useless
        /// </summary>
        [Enumeration(Value = "BAD")]
        BadAddress,
        /// <summary>
        /// A temporary address that may be good for visiting or mailing
        /// </summary>
        [Enumeration(Value = "TMP")]
        TemporaryAddress,
        /// <summary>
        /// An automatic answering machine that can be used for less urgent communications
        /// </summary>
        [Enumeration(Value = "AS")]
        AnsweringService,
        /// <summary>
        /// A contact that is designated for contact in case of an emergency
        /// </summary>
        [Enumeration(Value = "EC")]
        EmergencyContact,
        /// <summary>
        /// A telecommunication device that is kept with the entity as they move such as a
        /// mobile device
        /// </summary>
        [Enumeration(Value = "MC")]
        MobileContact,
        /// <summary>
        /// A paging device that can be used to solicit a callback
        /// </summary>
        [Enumeration(Value = "PG")]
        Pager
    }

    /// <summary>
    /// Indicates that a class implements a telecommunications address
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ITelecommunicationAddress
    {
        /// <summary>
        /// The value of the communications
        /// </summary>
        string Value { get; set; }
        /// <summary>
        /// Indicates how the tel address can be used
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        ISet<CS<TelecommunicationAddressUse>> Use { get;}
        /// <summary>
        /// Indicates the period that this particular telecommunications address is valid
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        ISetComponent<IPointInTime> UseablePeriod { get;}
    }
}
