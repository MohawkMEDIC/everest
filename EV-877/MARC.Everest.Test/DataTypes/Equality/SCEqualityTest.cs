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
    public class SCEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("1")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsCodeTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("1");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="1"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsLanguageTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "1";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(1) { new MARC.Everest.DataTypes.ST("1") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsTranslationTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(1) { new MARC.Everest.DataTypes.ST("1") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="1"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsValueTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "1";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsFlavorTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.SC"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Code</term><description>A=new MARC.Everest.DataTypes.CD&lt;System.String>("0"),B=new MARC.Everest.DataTypes.CD&lt;System.String>("0")</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void SCEqualsTest() {
MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = new MARC.Everest.DataTypes.SC();
aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
aValue.Value = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") };
bValue.Value = "0";
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
