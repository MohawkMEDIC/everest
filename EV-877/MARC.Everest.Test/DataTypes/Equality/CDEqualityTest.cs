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
    public class CDEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(1) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1))) }</description></item>
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
public void CDNotEqualsQualifierTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(1) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1))) };
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
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsTranslationTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsCodeTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsCodeSystemTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsCodeSystemNameTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsCodeSystemVersionTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsDisplayNameTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsOriginalTextTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsCodingRationaleTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsValueSetTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsValueSetVersionTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsFlavorTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) },B=new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
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
public void CDEqualsTest() {
MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>();
aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
bValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
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
