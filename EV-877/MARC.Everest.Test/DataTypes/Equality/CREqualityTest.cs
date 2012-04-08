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
    public class CREqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsNameTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsValueTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(1));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=true</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsInvertedTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = true;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsFlavorTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CRNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Name</term><description>A=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Value</term><description>A=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),B=new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
/// <item><term>Inverted</term><description>A=false,B=false</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void CREqualsTest() {
MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>();
aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
aValue.Inverted = false;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
bValue.Inverted = false;
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
