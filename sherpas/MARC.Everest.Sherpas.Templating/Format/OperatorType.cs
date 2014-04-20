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
    [Flags]
    [XmlType("OperatorType", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public enum OperatorType : int
    {
        [XmlEnum("?")]
        Unknown = 0,
        [XmlEnum("NOT")]
        Not = 1,
        [XmlEnum("AND")]
        And = 2,
        [XmlEnum("OR")]
        Or = 4,
        [XmlEnum("XOR")]
        Xor = 8,
        [XmlEnum("EQ")]
        Equals = 16,
        [XmlEnum("LT")]
        LessThan = 32,
        [XmlEnum("GT")]
        GreaterThan = 64,
        [XmlEnum("IS")]
        Is = 128,
        [XmlEnum("CONT")]
        Contains = 256,
        [XmlEnum("NE")]
        NotEquals = Not | Equals,
        [XmlEnum("NCONT")]
        NotContains = Not | Contains,
        [XmlEnum("NAND")]
        NotAnd = Not | And,
        [XmlEnum("NOR")]
        NotOr = Not | Or,
        [XmlEnum("NXOR")]
        NotXor = Not | Xor,
        [XmlEnum("GTE")]
        GreaterThanEqualTo = GreaterThan | OperatorType.Equals,
        [XmlEnum("LTE")]
        LessThanEqualTo = LessThan | OperatorType.Equals
    }
}
