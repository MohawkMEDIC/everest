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
    public class TNEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(1) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Search) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TNNotEqualsUseTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(1) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Search) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TNNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TNNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TNNotEqualsFlavorTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TNNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TNNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void TNNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TNNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TN"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TNEqualsTest() {
MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = new MARC.Everest.DataTypes.TN();
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>(MARC.Everest.DataTypes.EntityNameUse.Legal) };
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
