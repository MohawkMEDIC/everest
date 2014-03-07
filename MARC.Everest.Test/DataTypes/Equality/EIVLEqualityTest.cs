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
    public class EIVLEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.AfterDinner)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsEventTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.AfterDinner);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)1,"1"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsOffsetTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)1,"1"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.PeriodicHull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsOperatorTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.PeriodicHull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsFlavorTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsControlActRootTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLNotEqualsControlActExtTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Event</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
/// <item><term>Offset</term><description>A=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0")),B=new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
/// <item><term>Operator</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull),B=new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void EIVLEqualsTest() {
MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>();
aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
bValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"));
bValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
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
