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
 * Date: 09-26-2011
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// Identifies data related to a delta 
    /// </summary>
    [XmlType("ClassDelta", Namespace = "urn:infoway.ca/deltaSet")]
    public class ClassDeltaData : DeltaData
    {
        // Stores data related to delta sets
        private List<KeyValuePair<DeltaType, RelationshipDeltaData>> m_deltas = null;

        

        /// <summary>
        /// Gets the description
        /// </summary>
        [XmlAttribute("description")]
        public String Description { get; set; }

        /// <summary>
        /// Identifies deltas that modify attributes and/or associations
        /// </summary>
        /// <remarks>Should somehow think of how to combine this into one array rather than two</remarks>
        [XmlElement("attributeDelta", typeof(RelationshipDeltaData))]
        [XmlElement("associationDelta", typeof(RelationshipDeltaData))]
        [XmlChoiceIdentifier("DeltaTypeInternal")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public RelationshipDeltaData[] DeltaInternal { get; set; }
        
        /// <summary>
        /// Identifies the type of deltas
        /// </summary>
        [XmlIgnore()]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public DeltaType[] DeltaTypeInternal { get; set; }

        /// <summary>
        /// Gets the deltas for the delta set as a coherent list
        /// </summary>
        [XmlIgnore()]
        public List<KeyValuePair<DeltaType, RelationshipDeltaData>> Deltas
        {
            get
            {
                // If deltas have not been consolodated then consolodate them
                if (this.m_deltas == null && this.DeltaInternal != null)
                {
                    this.m_deltas = new List<KeyValuePair<DeltaType,RelationshipDeltaData>>(this.DeltaInternal.Length);
                    for (int i = 0; i < this.DeltaInternal.Length; i++)
                        this.m_deltas.Add(new KeyValuePair<DeltaType, RelationshipDeltaData>(this.DeltaTypeInternal[i], this.DeltaInternal[i]));
                }
                return this.m_deltas;
            }
        }
    }
}
