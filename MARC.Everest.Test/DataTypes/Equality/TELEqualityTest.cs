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
    public class TELEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="1"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsValueTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "1";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(1) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Pager) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsUseTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(1) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Pager) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(1) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.SMS) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsCapabilitiesTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(1) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.SMS) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.AfterDinner,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)1,"1"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsUseablePeriodTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.AfterDinner,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)1,"1"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsFlavorTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.TEL"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
/// <item><term>Capabilities</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) }</description></item>
/// <item><term>UseablePeriod</term><description>A=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")))),B=new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL&lt;MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))))</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void TELEqualsTest() {
MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = new MARC.Everest.DataTypes.TEL();
aValue.Value = "0";
aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
aValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
bValue.Capabilities = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability>(MARC.Everest.DataTypes.Interfaces.TelecommunicationCabability.Voice) };
bValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.TS>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal,new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))));
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
