using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Operator type
    /// </summary>
    [XmlType("OperatorType", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public enum OperatorType
    {
        [XmlEnum("AND")]
        And,
        [XmlEnum("OR")]
        Or,
        [XmlEnum("XOR")]
        Xor,
        [XmlEnum("EQ")]
        Equals,
        [XmlEnum("NE")]
        NotEquals,
        [XmlEnum("LT")]
        LessThan,
        [XmlEnum("GT")]
        GreaterThan,
        [XmlEnum("NCONT")]
        NotContains,
        [XmlEnum("IS")]
        Is
    }
}
