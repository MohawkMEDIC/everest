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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for URGTest
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestClass]
    public class URGTest
    {
        public URGTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     OriginalText        : Text indicating where this interval was derived
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor          
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGOriginalTextTest()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = null;
            urg.OriginalText = "test";
            Assert.IsFalse(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     OriginalText        : Text indicating where this interval was derived
        ///     NullFlavor
        /// And, there are no variables with Nullifiedvalues.
        /// </summary> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGOriginalTextNullFlavorTest()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = NullFlavor.NotAsked;
            urg.OriginalText = "test";
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor          
        /// And, the rest of the variables are Nullified:
        ///     Low        : This is the low limit. If the low limit is not known a null flavor should be specified
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGNullFlavor1Test()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = NullFlavor.NotAsked; 
            urg.Low = null;
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And, the rest of the variables are Nullified:
        ///     LowIncluded  : Specifies wheter low is included in the IVL or excluded from the IVL          
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGNullFlavor2Test()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = NullFlavor.NotAsked;
            urg.LowClosed = null;
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And, the rest of the variables are Nullified:
        ///     High        : The high limit. If the hign limit is not known, a null flavour should be specified
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGNullFlavor3Test()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = NullFlavor.NotAsked;
            urg.High = null;
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And, the rest of the variables are Nullified:
        ///     HighIncluded  : Specifies whether high is inlcluded in the IVL or excluded in the IVL
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGNullFlavor4Test()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = NullFlavor.NotAsked;
            urg.HighClosed = null;
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor
        /// And, the rest of the variables are Nullified:
        ///     Width       : The difference between the high and low bondary. 
        ///                   Width is used when the size of the interval is known
        ///                   but the actual start and end points are not known. 
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGNullFlavor5Test()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = NullFlavor.NotAsked;
            urg.Width = null;
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Width       : The difference between the high and low bondary. 
        ///                   Width is used when the size of the interval is known
        ///                   but the actual start and end points are not known. 
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor
        ///     Low         : This is the low limit. If the low limit is not known a null flavor should be specified
        ///     High        : The high limit. If the hign limit is not known, a null flavour should be specified
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGWidthTest()
        {
            URG<II> urg = new URG<II>();
            urg.Probability = (decimal)1;
            urg.NullFlavor = null;
            urg.Width = 10;
            urg.Low = null;
            urg.High = null;
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Width       : The difference between the high and low bondary. 
        ///                   Width is used when the size of the interval is known
        ///                   but the actual start and end points are not known. 
        ///     Low         : This is the low limit. If the low limit is not known a null flavor should be specified
        ///     High        : The high limit. If the hign limit is not known, a null flavour should be specified
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGWidthLowHighTest()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = null;
            urg.Probability = (decimal)1;
            urg.Width = "10";
            urg.Low = new II("2.1",null);
            urg.High = new II("1.4",null);
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Low         : This is the low limit. If the low limit is not known a null flavor should be specified
        ///     High        : The high limit. If the hign limit is not known, a null flavour should be specified
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor
        ///     Width       : The difference between the high and low bondary. 
        ///                   Width is used when the size of the interval is known
        ///                   but the actual start and end points are not known. 
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGLowHighTest()
        {
            URG<II> urg = new URG<II>();
            urg.Probability = (decimal)1;
            urg.NullFlavor = null;
            urg.Width = null;
            urg.Low = new II("2.2",null); 
            urg.High = new II("1.4", null);
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Low         : This is the low limit. If the low limit is not known a null flavor should be specified
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor
        ///     Width       : The difference between the high and low bondary. 
        ///                   Width is used when the size of the interval is known
        ///                   but the actual start and end points are not known. 
        ///     High        : The high limit. If the hign limit is not known, a null flavour should be specified
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGLow()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = null;
            urg.Probability = (decimal)1;
            urg.Width = null;
            urg.Low = new II("1.3.6.2", null);
            urg.High = null;
            Assert.IsTrue(urg.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     High        : The high limit. If the hign limit is not known, a null flavour should be specified
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor
        ///     Width       : The difference between the high and low bondary. 
        ///                   Width is used when the size of the interval is known
        ///                   but the actual start and end points are not known. 
        ///     Low         : This is the low limit. If the low limit is not known a null flavor should be specified
        /// </summary>         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "URG"), TestMethod]
        public void URGHighTest()
        {
            URG<II> urg = new URG<II>();
            urg.NullFlavor = null;
            urg.Width = null;
            urg.Probability = (decimal)1;
            urg.Low = null;
            urg.High = new II("1.4", null);
            Assert.IsTrue(urg.Validate());
        }

        #region Surrogates removed from Everest 1.1

        /// <summary>
        /// Testing when a concreate URG is converted to a generic version
        /// Must return TRUE
        /// Variables being converted:
        ///     ControlActRoot not nullified
        /// </summary> 
        /// Obsolete with hierarchy change
        //[TestMethod]
        //public void URGControlActRootSurrogateTest()
        //{
        //    MARC.Everest.DataTypes.URG<PQ> o = new MARC.Everest.DataTypes.URG<PQ>();
        //    o.ControlActRoot = "1";
        //    URG<String> test = URG<String>.Parse(o);
        //    Assert.IsTrue(o.ControlActRoot == test.ControlActRoot);

        //}

        /// <summary>
        /// Testing when a concreate URG is converted to a generic version
        /// Must return TRUE
        /// Variables being converted:
        ///     ControlActExt not nullified
        /// </summary> 
        /// Obsolete with hierarchy change
        /// [TestMethod]
        //public void URGControlActExtSurrogateTest()
        //{
        //    MARC.Everest.DataTypes.URG<Object> o = new MARC.Everest.DataTypes.URG<Object>();
        //    o.ControlActExt = "1";
        //    URG<String> test = URG<String>.Parse(o);
        //    Assert.IsTrue(o.ControlActExt == test.ControlActExt);
        //}

        /// <summary>
        /// Testing when a concreate URG is converted to a generic version
        /// Must return TRUE
        /// Variables being converted:
        ///     Probability not nullified
        /// </summary>         
        //[TestMethod]
        //public void URGProbabilitySurrogateTest()
        //{
        //    MARC.Everest.DataTypes.URG<Object> o = new MARC.Everest.DataTypes.URG<Object>();
        //    o.Probability = (decimal)0.50;
        //    URG<double?> test = URG<double?>.Parse(o);
        //    Assert.IsTrue(o.Probability == test.Probability);
        //}

        /// <summary>
        /// Testing when a concreate URG is converted to a generic version
        /// Must return TRUE
        /// Variables being converted:
        ///     Flavor not nullified
        /// </summary>         
        //[TestMethod]
        //public void URGFlavorSurrogateTest()
        //{
        //    MARC.Everest.DataTypes.URG<Object> o = new MARC.Everest.DataTypes.URG<Object>();
        //    o.Flavor = "hola";
        //    URG<String> test = URG<String>.Parse(o);
        //    Assert.IsTrue(o.Flavor == test.Flavor);
        //}

        /// <summary>
        /// Testing when a concreate URG is converted to a generic version
        /// Must return TRUE
        /// Variables being converted:
        ///     NullFlavor is nullified
        /// </summary>         
        //[TestMethod]
        //public void URGNullFlavorSurrogate1Test()
        //{
        //    MARC.Everest.DataTypes.URG<Object> o = new MARC.Everest.DataTypes.URG<Object>();
        //    o.NullFlavor = null;
        //    URG<String> test = URG<String>.Parse(o);
        //    Assert.IsTrue(o.NullFlavor == test.NullFlavor);
        //}

        /// <summary>
        /// Testing when a concreate URG is converted to a generic version
        /// Must return TRUE
        /// Variables being converted:
        ///     NullFlavor is not nullified
        /// </summary>         
        //[TestMethod]
        //public void URGPNullFlavorSurrogate2Test()
        //{
        //    MARC.Everest.DataTypes.URG<Object> o = new MARC.Everest.DataTypes.URG<Object>();
        //    o.NullFlavor = NullFlavor.NotAsked;
        //    URG<String> test = URG<String>.Parse(o);
        //    Assert.IsTrue(o.NullFlavor.Equals(test.NullFlavor));
        //}
        #endregion
    }
}
