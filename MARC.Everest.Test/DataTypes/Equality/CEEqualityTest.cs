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
 * Date: 3-6-2013
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Xml;
using System.Xml;

namespace MARC.Everest.Test
{
    [TestClass]
    public class CEEqualityTest
    {
		
		private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        

/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(1) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsTranslationTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(1) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsCodeTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="1"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsCodeSystemTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "1";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="1"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsCodeSystemNameTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "1";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="1"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsCodeSystemVersionTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "1";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="1"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsDisplayNameTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "1";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsOriginalTextTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(1) { MARC.Everest.DataTypes.CodingRationale.Source }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsCodingRationaleTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(1) { MARC.Everest.DataTypes.CodingRationale.Source };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="1"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsValueSetTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "1";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="1"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsValueSetVersionTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "1";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsFlavorTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "1";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsControlActRootTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "1";
bValue.ControlActExt = "0";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CENotEqualsControlActExtTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "1";
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemName</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>DisplayName</term><description>A="0",B="0"</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>ValueSet</term><description>A="0",B="0"</description></item>
/// <item><term>ValueSetVersion</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CEEqualsTest() {
MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>();
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.CodeSystem = "0";
aValue.CodeSystemName = "0";
aValue.CodeSystemVersion = "0";
aValue.DisplayName = "0";
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.ValueSet = "0";
aValue.ValueSetVersion = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.CodeSystem = "0";
bValue.CodeSystemName = "0";
bValue.CodeSystemVersion = "0";
bValue.DisplayName = "0";
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.ValueSet = "0";
bValue.ValueSetVersion = "0";
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
Assert.AreEqual(aValue, bValue);
}
}}
