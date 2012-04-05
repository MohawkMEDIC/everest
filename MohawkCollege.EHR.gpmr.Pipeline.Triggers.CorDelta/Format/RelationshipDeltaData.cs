using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// Relationship data 
    /// </summary>
    [XmlType("RelationshipDelta", Namespace = "urn:infoway.ca/deltaSet")]
    public class RelationshipDeltaData : DeltaData
    {
        /// <summary>
        /// Identifies the type of change that has been made
        /// </summary>
        public ConstraintDeltaType DeltaType { get; set; }

    }
}
