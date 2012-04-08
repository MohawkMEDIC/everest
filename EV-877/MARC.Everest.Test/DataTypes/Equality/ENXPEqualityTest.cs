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
    public class ENXPEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="1"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsValueTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "1";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="1"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsCodeTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "1";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="1"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsCodeSystemTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "1";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="1"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsCodeSystemVersionTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "1";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Delimiter)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsTypeTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Delimiter);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(1) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.Suffix) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsQualifierTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(1) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.Suffix) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsFlavorTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ENXP"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Value</term><description>A="0",B="0"</description></item>
/// <item><term>Code</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystem</term><description>A="0",B="0"</description></item>
/// <item><term>CodeSystemVersion</term><description>A="0",B="0"</description></item>
/// <item><term>Type</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family),B=new System.Nullable&lt;MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family)</description></item>
/// <item><term>Qualifier</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) }</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void ENXPEqualsTest() {
MARC.Everest.DataTypes.ENXP aValue = new MARC.Everest.DataTypes.ENXP(), bValue = new MARC.Everest.DataTypes.ENXP();
aValue.Value = "0";
aValue.Code = "0";
aValue.CodeSystem = "0";
aValue.CodeSystemVersion = "0";
aValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
aValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Value = "0";
bValue.Code = "0";
bValue.CodeSystem = "0";
bValue.CodeSystemVersion = "0";
bValue.Type = new System.Nullable<MARC.Everest.DataTypes.EntityNamePartType>(MARC.Everest.DataTypes.EntityNamePartType.Family);
bValue.Qualifier = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNamePartQualifier>(MARC.Everest.DataTypes.EntityNamePartQualifier.LegalStatus) };
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
