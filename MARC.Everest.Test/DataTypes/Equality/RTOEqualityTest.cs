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
    public class RTOEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(1)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsNumeratorTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(1);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(1)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsDenominatorTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(1);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsExpressionTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsOriginalTextTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Beta)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsUncertaintyTypeTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Beta);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsFlavorTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsControlActRootTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTONotEqualsControlActExtTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Numerator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Denominator</term><description>A=new MARC.Everest.DataTypes.INT(0),B=new MARC.Everest.DataTypes.INT(0)</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void RTOEqualsTest() {
MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT,MARC.Everest.DataTypes.INT>();
aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Numerator = new MARC.Everest.DataTypes.INT(0);
bValue.Denominator = new MARC.Everest.DataTypes.INT(0);
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
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
