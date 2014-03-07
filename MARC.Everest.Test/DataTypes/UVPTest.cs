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
    /// Summary description for UVPTest
    /// </summary>
    [TestClass]
    public class UVPTest
    {
        public UVPTest()
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
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     Value               : The value of the UVP.
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// And, No variables are not Nullified:
        /// </summary> 
        [TestMethod]
        public void UVPValueProbabilityTest1()
        {
            UVP<TEL> uvp = new UVP<TEL>();
            uvp.Value = "HOLA";
            uvp.Probability = (decimal)0.50;
            Assert.IsTrue(uvp.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns TRUE
        /// When the following values are not Nullified:
        ///     Value               : The value of the UVP.
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// And, No variables are not Nullified:
        /// </summary> 
        [TestMethod]
        public void UVPValueProbabilityTest2()
        {
            string p1 = "HOLA";
            double p2 = 0.50;
            UVP<TEL> uvp = new UVP<TEL>((TEL)p1,(decimal)p2);
            Assert.IsTrue(uvp.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE because Probability is out of range.
        /// When the following values are not Nullified:
        ///     Value               : The value of the UVP.
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// And, No variables are not Nullified:
        /// </summary> 
        [TestMethod]
        public void UVPValueWrongProbabilityTest1()
        {
            UVP<TEL> uvp = new UVP<TEL>();
            uvp.Value = "HOLA";
            uvp.Probability = (decimal)1.01;
            Assert.IsFalse(uvp.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE because Probability is out of range.
        /// When the following values are not Nullified:
        ///     Value               : The value of the UVP.
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// And, No variables are not Nullified:
        /// </summary> 
        [TestMethod]
        public void UVPValueWrongProbabilityTest2()
        {
            string p1 = "HOLA";
            double p2 = 1.01;
            UVP<TEL> uvp = new UVP<TEL>(p1,(decimal)p2);
            Assert.IsFalse(uvp.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE because Probability is out of range.
        /// When the following values are not Nullified:
        ///     Value               : The value of the UVP.
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// And, No variables are not Nullified:
        /// </summary> 
        [TestMethod]
        public void UVPValueWrongProbabilityTest3()
        {
            UVP<TEL> uvp = new UVP<TEL>();
            uvp.Value = "HOLA";
            uvp.Probability = (decimal)-0.01;
            Assert.IsFalse(uvp.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE because Probability is out of range.
        /// When the following values are not Nullified:
        ///     Value               : The value of the UVP.
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// And, No variables are not Nullified:
        /// </summary> 
        [TestMethod]
        public void UVPValueWrongProbabilityTest4()
        {
            string p1 = "HOLA";
            double p2 = -0.01;
            UVP<TEL> uvp = new UVP<TEL>(p1, (Decimal)p2);
            Assert.IsFalse(uvp.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE.
        /// When the following values are not Nullified:
        ///     Value               : The value of the UVP.
        /// And, the rest of the variables are Nullified:
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// </summary> 
        [TestMethod]
        public void UVPValueTest()
        {
            UVP<TEL> uvp = new UVP<TEL>();
            uvp.Value = "HOLA";
            uvp.Probability = null;
            Assert.IsFalse(uvp.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE.
        /// When the following values are not Nullified:
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// And, the rest of the variables are Nullified:
        ///     Value               : The value of the UVP.
        /// </summary> 
        [TestMethod]
        public void UVPProbatilityTest()
        {
            UVP<TEL> uvp = new UVP<TEL>();
            uvp.Value = null;
            uvp.Probability = (decimal)0.50;
            Assert.IsFalse(uvp.Validate());
        }

        /// <summary>
        /// Testing Function Validate must returns FALSE.
        /// When there are not variables are not Nullified:
        /// And, the rest of the variables are Nullified:
        ///     Value               : The value of the UVP.
        ///     Probability         : The probability assigned to the value a decimal between 0 and 1.
        /// </summary> 
        [TestMethod]
        public void UVPNullAttributesTest()
        {
            UVP<TEL> uvp = new UVP<TEL>();
            uvp.Value = null;
            uvp.Probability = null;
            Assert.IsFalse(uvp.Validate());
        }

        #region Surrogates removed from Everest 1.1
        /// <summary>
        /// Testing when a concreate UVP is converted to a generic version
        /// Must returns TRUE
        /// Variables being converted:
        ///     ControlActRoot not nullified
        /// </summary> 
        //[TestMethod]
        //public void UVPControlActRootSurrogateTest()
        //{
        //    MARC.Everest.DataTypes.UVP<Object> o = new MARC.Everest.DataTypes.UVP<Object>();
        //    o.ControlActRoot = "1";
        //    UVP<String> test = UVP<String>.Parse(o);
        //    Assert.IsTrue(o.ControlActRoot==test.ControlActRoot);

        //}

        /// <summary>
        /// Testing when a concreate UVP is converted to a generic version
        /// Must returns TRUE
        /// Variables being converted:
        ///     ControlActExt not nullified
        /// </summary> 
        //[TestMethod]
        //public void UVPControlActExtSurrogateTest()
        //{
        //    MARC.Everest.DataTypes.UVP<Object> o = new MARC.Everest.DataTypes.UVP<Object>();
        //    o.ControlActExt = "1";
        //    UVP<String> test = UVP<String>.Parse(o);
        //    Assert.IsTrue(o.ControlActExt==test.ControlActExt);
        //}

        /// <summary>
        /// Testing when a concreate UVP is converted to a generic version
        /// Must returns TRUE
        /// Variables being converted:
        ///     Probability not nullified
        /// </summary> 
        //[TestMethod]
        //public void UVPProbabilitySurrogateTest()
        //{
        //    MARC.Everest.DataTypes.UVP<Object> o = new MARC.Everest.DataTypes.UVP<Object>();
        //    o.Probability = (decimal)0.50;
        //    UVP<double?> test = UVP<double?>.Parse(o);
        //    Assert.IsTrue(o.Probability==test.Probability);
        //}

        /// <summary>
        /// Testing when a concreate UVP is converted to a generic version
        /// Must returns TRUE
        /// Variables being converted:
        ///     Flavor not nullified
        /// </summary> 
        //[TestMethod]
        //public void UVPFlavorSurrogateTest()
        //{
        //    MARC.Everest.DataTypes.UVP<Object> o = new MARC.Everest.DataTypes.UVP<Object>();
        //    o.Flavor = "hola";
        //    UVP<String> test = UVP<String>.Parse(o);
        //    Assert.IsTrue(o.Flavor==test.Flavor);
        //}

        /// <summary>
        /// Testing when a concreate UVP is converted to a generic version
        /// Must returns TRUE
        /// Variables being converted:
        ///     NullFlavor when it is Nullified
        /// </summary>
        //[TestMethod]
        //public void UVPNullFlavorSurrogate1Test()
        //{
        //    MARC.Everest.DataTypes.UVP<Object> o = new MARC.Everest.DataTypes.UVP<Object>();
        //    o.NullFlavor = null;
        //    UVP<String> test = UVP<String>.Parse(o);
        //    Assert.IsTrue(o.NullFlavor==test.NullFlavor);
        //}


        /// <summary>
        /// Testing when a concreate UVP is converted to a generic version
        /// Must returns TRUE
        /// Variables being converted:
        ///     NullFlavor when it is not Nullified
        /// </summary>
        //[TestMethod]
        //public void UVPNullFlavorSurrogate2Test()
        //{
        //    MARC.Everest.DataTypes.UVP<object> o = new MARC.Everest.DataTypes.UVP<Object>();
        //    o.NullFlavor = NullFlavor.NotAsked;
        //    UVP<String> test = UVP<String>.Parse(o);
        //    Assert.IsTrue(o.NullFlavor.Equals(test.NullFlavor));
        //}
        #endregion

    }
}
