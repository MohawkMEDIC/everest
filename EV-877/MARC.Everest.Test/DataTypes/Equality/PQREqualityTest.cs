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
    public class PQREqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)1</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsValueTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)1;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=1</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsPrecisionTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 1;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("1")</description></item>
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
public void PQRNotEqualsCodeTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("1");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsCodeSystemTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsCodeSystemNameTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsCodeSystemVersionTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsDisplayNameTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsOriginalTextTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsCodingRationaleTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsValueSetTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsValueSetVersionTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsFlavorTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQRNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQR"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0"),B=new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
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
public void PQREqualsTest() {
MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = new MARC.Everest.DataTypes.PQR();
aValue.Value = (decimal)0;
aValue.Precision = 0;
aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
bValue.Value = (decimal)0;
bValue.Precision = 0;
bValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
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
