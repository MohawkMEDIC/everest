using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// A generic interface that represents a constraint value
    /// </summary>
    [XmlType("ConstraintValueType", Namespace = "urn:infoway.ca/deltaSet")]
    public abstract class ConstraintValueBase : BaseXmlType
    {
    }
}
