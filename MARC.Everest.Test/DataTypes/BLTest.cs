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
    /// Summary description for BLTest
    /// </summary>
    [TestClass]
    public class BLTest
    {
        public BLTest()
        {
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
        /// Ensures that validation fails when both the value and null flavor are null.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neg"), TestMethod]
        public void BLNonNegNullTest()
        {
            BL bl = new BL();
            bl.Value = null;
            bl.NullFlavor = null;
            Assert.IsFalse(bl.Validate());
        }
        /// <summary>
        /// Ensure that validation succeeds when value is populated and null flavor is null.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neg"), TestMethod]
        public void BLNonNegValueTest()
        {
            BL bl = new BL();
            bl.Value = true;
            bl.NullFlavor = null;
            Assert.IsTrue(bl.Validate());
        }
        /// <summary>
        /// Ensures that validation fails when both the value and null flavor are populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neg"), TestMethod]
        public void BLNonNegNullFlavorValueTest()
        {
            BL bl = new BL();
            bl.Value = false;
            bl.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(bl.Validate());
        }
        /// <summary>
        /// Ensures that validation succeeds when value is null and null flavor is populated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Neg"), TestMethod]
        public void BLNonNegNullFlavorTest()
        {
            BL bl = new BL();
            bl.Value = null;
            bl.NullFlavor = NullFlavor.Invalid;
            Assert.IsTrue(bl.Validate());
        }
        /// <summary>
        /// Tests the XOR method and operator.
        /// </summary>
        [TestMethod]
        public void BLXorTest()
        {
            BL result = null;
            BL trueBL = true;
            BL falseBL = false;
            BL nullBL = new BL() { Value = null };

            //Tests the false combonations using the XOR method and operator.
            result = falseBL.Xor(falseBL);
            Assert.IsFalse((bool)result);
            result = falseBL.Xor(trueBL);
            Assert.IsTrue((bool)result);
            result = falseBL.Xor(nullBL);
            Assert.IsNull(result.Value);
                       
            result = falseBL ^ falseBL;
            Assert.IsFalse((bool)result);
            result = falseBL ^ trueBL;
            Assert.IsTrue((bool)result);
            result = falseBL ^ nullBL;
            Assert.IsNull(result.Value);

            //Tests the true combonations using the XOR method and operator.
            result = trueBL.Xor(falseBL);
            Assert.IsTrue((bool)result);
            result = trueBL.Xor(trueBL);
            Assert.IsFalse((bool)result);
            result = trueBL.Xor(nullBL);
            Assert.IsNull(result.Value);

            result = trueBL ^ falseBL;
            Assert.IsTrue((bool)result);
            result = trueBL ^ trueBL;
            Assert.IsFalse((bool)result);
            result = trueBL ^ nullBL;
            Assert.IsNull(result.Value);

            //Tests the null combonations using the XOR method and operator.
            result = nullBL.Xor(falseBL);
            Assert.IsNull(result.Value);
            result = nullBL.Xor(trueBL);
            Assert.IsNull(result.Value);
            result = nullBL.Xor(nullBL);
            Assert.IsNull(result.Value);

            result = nullBL ^ falseBL;
            Assert.IsNull(result.Value);
            result = nullBL ^ trueBL;
            Assert.IsNull(result.Value);
            result = nullBL ^ nullBL;
            Assert.IsNull(result.Value);
        }
        /// <summary>
        /// Tests the AND method and operator.
        /// </summary>
        [TestMethod]
        public void BLAndTest()
        {
            BL result = null;
            BL trueBL = true;
            BL falseBL = false;
            BL nullBL = new BL() { Value = null };

            //Tests the false combonations using the AND method and operator.
            result = falseBL.And(falseBL);
            Assert.IsFalse((bool)result);
            result = falseBL.And(trueBL);
            Assert.IsFalse((bool)result);
            result = falseBL.And(nullBL);
            Assert.IsFalse((bool)result);
            
            result = falseBL & falseBL;
            Assert.IsFalse((bool)result);
            result = falseBL & trueBL;
            Assert.IsFalse((bool)result);
            result = falseBL & nullBL;
            Assert.IsFalse((bool)result);

            //Tests the true combonations using the AND method and operator.
            result = trueBL.And(falseBL);
            Assert.IsFalse((bool)result);
            result = trueBL.And(trueBL);
            Assert.IsTrue((bool)result);
            result = trueBL.And(nullBL);
            Assert.IsNull(result.Value);

            result = trueBL & falseBL;
            Assert.IsFalse((bool)result);
            result = trueBL & trueBL;
            Assert.IsTrue((bool)result);
            result = trueBL & nullBL;
            Assert.IsNull(result.Value);
            
            //Tests the null combonations using the AND method and operator.
            result = nullBL.And(falseBL);
            Assert.IsFalse((bool)result);
            result = nullBL.And(trueBL);
            Assert.IsNull(result.Value);
            result = nullBL.And(nullBL);
            Assert.IsNull(result.Value);

            result = nullBL & falseBL;
            Assert.IsFalse((bool)result);
            result = nullBL & trueBL;
            Assert.IsNull(result.Value);
            result = nullBL & nullBL;
            Assert.IsNull(result.Value);
        }
        /// <summary>
        /// Test the NOT method and operator.
        /// </summary>
        [TestMethod]
        public void BLNotTest()
        {
            BL trueBL = true;
            BL falseBL = false;
            BL nullBL = new BL() { Value = null };
            BL result = null;
            //Test the methods.
            result = trueBL.Not();
            Assert.IsFalse((bool)result);
            result = falseBL.Not();
            Assert.IsTrue((bool)result);
            result = nullBL.Not();
            Assert.IsNull(result.Value);
            //Test the operators.
            result = !trueBL;
            Assert.IsFalse((bool)result);
            result = !falseBL;
            Assert.IsTrue((bool)result);
            result = !nullBL;
            Assert.IsNull(result.Value);

        }
        /// <summary>
        /// Test the OR method and operator.
        /// </summary>
        [TestMethod]
        public void BLOrTest()
        {
            BL trueBL = true;
            BL falseBL = false;
            BL nullBL = new BL() { Value = null };
            BL result = null;

            //Tests the false combonations using the OR method and operator.
            result = falseBL.Or(falseBL);
            Assert.IsFalse((bool)result);
            result = falseBL.Or(trueBL);
            Assert.IsTrue((bool)result);
            result = falseBL.Or(nullBL);
            Assert.IsNull(result.Value);
            
            result = falseBL | falseBL;
            Assert.IsFalse((bool)result);
            result = falseBL | trueBL;
            Assert.IsTrue((bool)result);
            result = falseBL | nullBL;
            Assert.IsNull(result.Value);

            //Tests the true combonations using the OR method and operator.
            result = trueBL.Or(falseBL);
            Assert.IsTrue((bool)result);
            result = trueBL.Or(trueBL);
            Assert.IsTrue((bool)result);
            result = trueBL.Or(nullBL);
            Assert.IsTrue((bool)result);
            
            result = trueBL | falseBL;
            Assert.IsTrue((bool)result);
            result = trueBL | trueBL;
            Assert.IsTrue((bool)result);
            result = trueBL | nullBL;
            Assert.IsTrue((bool)result);

            //Tests the null combonations using the OR method and operator.
            result = nullBL.Or(falseBL);
            Assert.IsNull(result.Value);
            result = nullBL.Or(trueBL);
            Assert.IsTrue((bool)result);
            result = nullBL.Or(nullBL);
            Assert.IsNull(result.Value);

            result = nullBL | falseBL;
            Assert.IsNull(result.Value);
            result = nullBL | trueBL;
            Assert.IsTrue((bool)result);
            result = nullBL | nullBL;
            Assert.IsNull(result.Value);
        }
        /// <summary>
        /// Tests the setting of a BL using the string "true".
        /// </summary>
        [TestMethod]
        public void BLCastTrueFromStringTest()
        {
            BL bl = new BL();
            bl = "true";
            if (!bl.Value.HasValue)
                Assert.Fail("BL Value is null when it should have a value.");
            Assert.IsTrue(bl.Value.Value);
        }
        /// <summary>
        /// Tests the setting of a BL using the string "false".
        /// </summary>
        [TestMethod]
        public void BLCastFalseFromStringTest()
        {
            BL bl = new BL();
            bl = "false";
            if (!bl.Value.HasValue)
                Assert.Fail("BL Value is null when it should have a value.");
            Assert.IsFalse(bl.Value.Value);
        }
        /// <summary>
        /// Tests the ToString method with a value of true and when a null flavor is properly set.
        /// </summary>
        [TestMethod]
        public void BLToStringTest()
        {
            BL bl = true;
            string newString = bl.ToString();
            Assert.AreEqual("true", newString);
            bl = new BL();
            bl.NullFlavor = NullFlavor.Unknown;
            newString = bl.ToString();
            Assert.IsNull(newString);
        }
        /// <summary>
        /// Tests casting a BL to a boolean.
        /// </summary>
        [TestMethod]
        public void BLCastToBooleanTest()
        {
            BL bl = true;
            Boolean boolean = (bool)bl;
            Assert.IsTrue(boolean);
        }
        /// <summary>
        /// Tests casting a boolean to a BL.
        /// </summary>
        [TestMethod]
        public void BLCastFromBooleanTest()
        {
            BL bl = true;
            Assert.IsTrue((bool)bl);
        }
        /// <summary>
        /// Tests casting a BL to a nullable boolean.
        /// </summary>
        [TestMethod]
        public void BLCastToNullableBooleanTest()
        {
            BL bl = new BL();
            bl.Value = null;
            bool? nulledboolean = (bool?)bl;
            Assert.IsNull(nulledboolean);
        }
        /// <summary>
        /// Tests casting a nullable boolean into a BL.
        /// </summary>
        [TestMethod]
        public void BLCastFromNullableBooleanTest()
        {
            bool? nulledBoolean = null;
            BL bl = (BL)nulledBoolean;
            Assert.IsNull(bl.Value);
        }
        /// <summary>
        /// Tests casting a null BL to a boolean.
        /// </summary>
        [TestMethod]
        public void BLCastFromNullBLToBoolTest()
        {
            BL bl = new BL();
            bl.Value = null;
            bl.NullFlavor = null;

            
            try {
                bool value = (bool)bl;
            }
            catch
            {
                Assert.IsFalse(false);
            }
        }
       
        /// <summary>
        /// Tests the true BL.NonNull() method. 
        /// The BL.NonNull returns a true if the BL object has a value. 
        /// 
        /// Expected result: True.
        /// </summary>
        [TestMethod]
        public void BLNonNullSuccessTest()
        {
            BL validNonNull = new BL();
            validNonNull.Value = false;


            Assert.IsTrue(BL.IsValidNonNullFlavor(validNonNull));
        }

        /// <summary>
        /// Tests the false BL.NonNull() method.
        /// BL.NonNull() returns false if the BL object does not contain a value. 
        /// 
        /// Expected Result: False
        /// </summary>
        [TestMethod]
        public void BLNonNullFailureTest()
        {
            BL invalidNonNull = new BL();
            invalidNonNull.NullFlavor = NullFlavor.NoInformation;

            Assert.IsFalse(BL.IsValidNonNullFlavor(invalidNonNull));
        }

        /// <summary>
        /// Tests the .XOR method using both a null BL and a BL with a value. 
        /// with a null, BL.Xor should return with a nullflavor of NoInformation.
        /// 
        /// Expected Result: BL.Xor(null).NullFlavor = NoInformation
        /// </summary>
        [TestMethod]
        public void BLXORwithNullTest()
        {
            BL nullXor = new BL(true);
      
            Assert.AreEqual<NullFlavor>(NullFlavor.NoInformation, (NullFlavor)nullXor.Xor(null).NullFlavor );
        }

        /// <summary>
        /// Tests the ability to cast a BL from a string. 
        /// Giving a BL a value of 0 results in a BL value of false. 
        /// 
        /// Expected Result: False. 
        /// </summary>
        [TestMethod]
        public void BLCastFromStringTest()
        {
            BL stringCast = new BL();
            stringCast = "0";
            Assert.IsFalse((bool)stringCast);
        }

        /// <summary>
        /// Tests the And() method of the BL class.
        /// Passing a null in the AND method should not cause an exception.
        /// 
        /// Expected ResulT: No exception. 
        /// </summary>
        [TestMethod]
        public void BLAndWithNullTest()
        {
            bool exceptionThrown = false;
            BL stringCast = new BL();
            try
            {
                stringCast.And(null);
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }
    }
}
