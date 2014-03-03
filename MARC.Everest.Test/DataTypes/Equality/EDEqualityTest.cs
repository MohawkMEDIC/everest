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
    public class EDEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 1 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsDataTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 1 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.Z7)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsCompressionTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.Z7);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("1")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsDescriptionTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("1");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.XML</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsRepresentationTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Data = new System.Byte[] { 0 };
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
bValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
bValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
bValue.Flavor = "0";
bValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
bValue.ControlActRoot = "0";
bValue.ControlActExt = "0";
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.XML;
Assert.AreNotEqual(aValue, bValue);
}
/// <summary>
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="1"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsLanguageTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "1";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(1) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsTranslationTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(1) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="1"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsMediaTypeTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "1";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 1 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsIntegrityCheckTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 1 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA256)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsIntegrityCheckAlgorithmTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA256);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsThumbnailTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("1")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsReferenceTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("1");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsFlavorTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.ED"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Data</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>Compression</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
/// <item><term>Description</term><description>A=new MARC.Everest.DataTypes.ST("0"),B=new MARC.Everest.DataTypes.ST("0")</description></item>
/// <item><term>Representation</term><description>A=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT,B=MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
/// <item><term>Language</term><description>A="0",B="0"</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
/// <item><term>MediaType</term><description>A="0",B="0"</description></item>
/// <item><term>IntegrityCheck</term><description>A=new System.Byte[] { 0 },B=new System.Byte[] { 0 }</description></item>
/// <item><term>IntegrityCheckAlgorithm</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
/// <item><term>Thumbnail</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>Reference</term><description>A=new MARC.Everest.DataTypes.TEL("0"),B=new MARC.Everest.DataTypes.TEL("0")</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EDEqualsTest() {
MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = new MARC.Everest.DataTypes.ED();
aValue.Data = new System.Byte[] { 0 };
aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
aValue.Description = new MARC.Everest.DataTypes.ST("0");
aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
aValue.Language = "0";
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
aValue.MediaType = "0";
aValue.IntegrityCheck = new System.Byte[] { 0 };
aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.Reference = new MARC.Everest.DataTypes.TEL("0");
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Data = new System.Byte[] { 0 };
bValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
bValue.Description = new MARC.Everest.DataTypes.ST("0");
bValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
bValue.Language = "0";
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") };
bValue.MediaType = "0";
bValue.IntegrityCheck = new System.Byte[] { 0 };
bValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
bValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.Reference = new MARC.Everest.DataTypes.TEL("0");
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
