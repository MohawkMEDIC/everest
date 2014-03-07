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
    public class MOEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="1"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsCurrencyTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "1";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=1</description></item>
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
public void MONotEqualsPrecisionTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 1;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsExpressionTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsOriginalTextTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsUncertaintyTypeTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsValueTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsFlavorTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsControlActRootTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MONotEqualsControlActExtTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.MO"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Currency</term><description>A="0",B="0"</description></item>
/// <item><term>Precision</term><description>A=0,B=0</description></item>
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
public void MOEqualsTest() {
MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = new MARC.Everest.DataTypes.MO();
aValue.Currency = "0";
aValue.Precision = 0;
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
bValue.Currency = "0";
bValue.Precision = 0;
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
