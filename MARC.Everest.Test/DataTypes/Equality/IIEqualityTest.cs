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
    public class IIEqualityTest
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="1"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsRootTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "1";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="1"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsExtensionTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "1";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="1"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsIdentifierNameTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "1";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=true</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsDisplayableTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = true;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.ViewSpecificIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsScopeTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.ViewSpecificIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.UsedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsReliabilityTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.UsedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="1"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsAssigningAuthorityNameTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "1";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Version)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsUseTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Version);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.Unknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsNullFlavorTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Key)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsUpdateModeTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="1"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsFlavorTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsValidTimeLowTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-2-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsValidTimeHighTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="1"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsControlActRootTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is not equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IINotEqualsControlActExtTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
/// Determine if one <see cref="T:MARC.Everest.DataTypes.II"/> is  equal to another
/// <list type="table">
/// <listheader><term>Property</term><description>Comments</description></listheader>
/// <item><term>Root</term><description>A="0",B="0"</description></item>
/// <item><term>Extension</term><description>A="0",B="0"</description></item>
/// <item><term>IdentifierName</term><description>A="0",B="0"</description></item>
/// <item><term>Displayable</term><description>A=false,B=false</description></item>
/// <item><term>Scope</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
/// <item><term>Reliability</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
/// <item><term>AssigningAuthorityName</term><description>A="0",B="0"</description></item>
/// <item><term>Use</term><description>A=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business),B=new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
/// <item><term>NullFlavor</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
/// <item><term>UpdateMode</term><description>A=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add),B=new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
/// <item><term>Flavor</term><description>A="0",B="0"</description></item>
/// <item><term>ValidTimeLow</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ValidTimeHigh</term><description>A=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")),B=new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
/// <item><term>ControlActRoot</term><description>A="0",B="0"</description></item>
/// </list>
[TestMethod]
public void IIEqualsTest() {
MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = new MARC.Everest.DataTypes.II();
aValue.Root = "0";
aValue.Extension = "0";
aValue.IdentifierName = "0";
aValue.Displayable = false;
aValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
aValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
aValue.AssigningAuthorityName = "0";
aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
aValue.NullFlavor = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown);
aValue.UpdateMode = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add);
aValue.Flavor = "0";
aValue.ValidTimeLow = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ValidTimeHigh = new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"));
aValue.ControlActRoot = "0";
aValue.ControlActExt = "0";
bValue.Root = "0";
bValue.Extension = "0";
bValue.IdentifierName = "0";
bValue.Displayable = false;
bValue.Scope = new System.Nullable<MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier);
bValue.Reliability = new System.Nullable<MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem);
bValue.AssigningAuthorityName = "0";
bValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
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
