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
    public class PQEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="1"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsUnitTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "1";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=1</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsPrecisionTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 1;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(1) { MARC.Everest.DataTypes.CodingRationale.Source }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsCodingRationaleTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(1) { MARC.Everest.DataTypes.CodingRationale.Source };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(1) { new MARC.Everest.DataTypes.PQR((decimal)1,"1","1") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsTranslationTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(1) { new MARC.Everest.DataTypes.PQR((decimal)1,"1","1") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsExpressionTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsOriginalTextTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 1 },"1");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Beta)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsUncertaintyTypeTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Beta);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)1</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsValueTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)1;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsFlavorTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.PQ"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Unit</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
/// <item><term>CodingRationale</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
/// <item><term>Translation</term><description>A=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") },B=new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") }</description></item>
/// <item><term>Expression</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>OriginalText</term><description>A=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0"),B=new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term>UncertaintyType</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform),B=new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
/// <item><term>Value</term><description>A=(decimal)0,B=(decimal)0</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void PQEqualsTest() {
MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = new MARC.Everest.DataTypes.PQ();
aValue.Unit = "0";
aValue.Precision = 0;
aValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
aValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
aValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
aValue.Value = (decimal)0;
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Unit = "0";
bValue.Precision = 0;
bValue.CodingRationale = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original };
bValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0,"0","0") };
bValue.Expression = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0");
bValue.UncertaintyType = new System.Nullable<MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform);
bValue.Value = (decimal)0;
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
