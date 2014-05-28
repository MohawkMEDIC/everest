/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 20-4-2014
 */
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
