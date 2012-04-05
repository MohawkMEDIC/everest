using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for ADTest
    /// </summary>
    [TestClass]
    public class ADTest
    {
        public ADTest()
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
        /// Tests that AD (A) does not equal a string A
        /// </summary>
        [TestMethod]
        public void ADTypeMismatchEqualityTest()
        {
            AD a = new AD(
                new ADXP[] { 
                    new ADXP("123 Main Street West Hamilton ON")
                }
            );
            Assert.IsFalse(a.Equals("123 Main Street West Hamilton ON"));
        }

        /// <summary>
        /// Tests that AD (A) equals AD (B)
        /// </summary>
        [TestMethod]
        public void ADContentSameEqualityTest()
        {
            AD a = new AD(
                new ADXP[] { 
                    new ADXP("123 Main Street West Hamilton ON")
                }
            ), b = new AD(new ADXP[] { 
                    new ADXP("123 Main Street West Hamilton ON")
                }
            );
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.Equals((object)b));
        }

        /// <summary>
        /// Tests that AD (A) does not equal AD (B) with a different address
        /// </summary>
        [TestMethod]
        public void ADContentDifferentEqualityTest()
        {
            AD a = new AD(
                new ADXP[] { 
                    new ADXP("123 Main Street West Hamilton ON")
                }
            ), b = new AD(new ADXP[] { 
                    new ADXP("123 Main Street West Hamilton")
                }
            );
            Assert.IsFalse(a.Equals(b));
        }

        /// <summary>
        /// Test the AD tostring method with no parameters
        /// </summary>
        [TestMethod]
        public void ADToStringTest()
        {
            AD a = new AD(
                new ADXP[] { 
                    new ADXP("123", AddressPartType.BuildingNumber),
                    new ADXP("Main", AddressPartType.StreetName),
                    new ADXP("Street", AddressPartType.StreetType),
                    new ADXP("West", AddressPartType.Direction)
                });
            Assert.AreEqual(a.ToString(), "123MainStreetWest");
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, there are no nullified variables. 
        /// </summary>
        [TestMethod]
        public void ADNullFlavorTest()
        {
            AD ad = new AD();
            ad.NullFlavor = NullFlavor.NotAsked;
            Assert.IsTrue(ad.Validate());

        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Part    :   The parts to create
        /// And, the following values are nullified. 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADPartTest()
        {
            AD ad = new AD();
            ad.Part.AddRange(GenerateAddress());
            ad.NullFlavor = null;
            Assert.IsTrue(ad.Validate());
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When there are no populated variables:
        /// And, the following values are nullified. 
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADNullTest()
        {
            AD ad = new AD();
            ad.NullFlavor = null;
            Assert.IsFalse(ad.Validate());
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Part        :   The parts to create
        ///     NullFlavor
        /// And, there are no nullified variables. 
        /// </summary>
        [TestMethod]
        public void ADPartNullFlavorTest()
        {
            AD ad = new AD();
            ad.Part.AddRange(GenerateAddress());
            ad.NullFlavor = NullFlavor.NotApplicable;
            Assert.IsFalse(ad.Validate());
        }

        /// <summary>
        /// Generates the part types for the address: 123 Fake Street Door 1, Anytown, AnyCounty, Ontario, Canada
        /// </summary>
        /// <returns>a List of ADXP containing the address.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private List<ADXP> GenerateAddress()
        {
            List<ADXP> result = new List<ADXP>();

            result.Add(new ADXP("123", AddressPartType.BuildingNumber));
            result.Add(new ADXP("Fake", AddressPartType.StreetNameBase));
            result.Add(new ADXP("Street", AddressPartType.StreetType));
            result.Add(new ADXP("1", AddressPartType.UnitIdentifier));
            result.Add(new ADXP("Door", AddressPartType.UnitDesignator));
            result.Add(new ADXP("Anytown", AddressPartType.City));
            result.Add(new ADXP("AnyCounty", AddressPartType.County));
            result.Add(new ADXP("Ontario", AddressPartType.State));
            result.Add(new ADXP("Canada", AddressPartType.County));
            
            return result;
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     A List of ADXP containing the address
        ///     NullFlavor
        /// And, there are no nullified variables. 
        /// </summary>
        [TestMethod]
        public void ADNullFlavorValidationTest()
        {
            AD ad = new AD(new ADXP[] {new ADXP("123", AddressPartType.BuildingNumber)});
            ad.NullFlavor = NullFlavor.Invalid;
            Assert.IsFalse(ad.Validate());
        }

        /// <summary>
        /// Ensures that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Part where a value of ADXP is being added
        ///     NullFlavor
        /// And, there are no nullified variables. 
        /// </summary>
        [TestMethod]
        public void ADPartNullFlavorValidationTest()
        {
            AD ad = new AD();
            ad.NullFlavor = NullFlavor.Invalid;
            ad.Part.Add(new ADXP("hello"));
            Assert.IsFalse(ad.Validate());
        }
        /// <summary>
        /// Tests the PostalAddressUse/IEnumerable<ADXP> constructor.
        /// Add a PostalAddressUse and 4 Address Parts to an AD object in the constructor.
        /// 
        /// Expected Result: AD object with 1 PostalAddressUse and 4 Address Parts.
        /// /// </summary>
        [TestMethod]
        public void ADPostalAddressUseIEnumerableADXP_CTORTest()
        {
            AD ctorPostalAddressUseAndADXPArray =
              new AD(
                PostalAddressUse.Direct,
              new ADXP[]
              {
                    new ADXP("Street",AddressPartType.StreetType),
                    new ADXP("Main",AddressPartType.StreetName),
                    new ADXP("122",AddressPartType.BuildingNumber),
                    new ADXP("Hamilton",AddressPartType.City)      
              }
           );
            // True if the the AD object has 4 address parts and 1 postal address use.
            Assert.IsTrue(ctorPostalAddressUseAndADXPArray.Part.Count == 4 && ctorPostalAddressUseAndADXPArray.Use.Count == 1);
        }

        /// <summary>
        /// Tests the SET<PostalAddressUse>, IEnumerable<ADXP> constructor.
        /// Add 2 PostalAddressUses and 4 Address Parts to an AD object in the constructor.
        /// 
        /// Expected result: AD Object with 2 PostalAddressUses and 4 AddressParts. 
        /// </summary>
        [TestMethod]
        public void ADSETPostalAddressUseIEnumerableADXP_CTORTest()
        {
            AD ctorPostalAddressUseAndADXPArray =
                new AD(
                    new SET<CS<PostalAddressUse>>(
                        new CS<PostalAddressUse>[]
                        {
                            PostalAddressUse.HomeAddress,
                            PostalAddressUse.Direct
                        },CS<PostalAddressUse>.Comparator
                        ),

                    new ADXP[]
                    {
                        new ADXP("Street",AddressPartType.StreetType),
                        new ADXP("Main",AddressPartType.StreetName),
                        new ADXP("122",AddressPartType.BuildingNumber),
                        new ADXP("Hamilton",AddressPartType.City)      
                    }
                );
            Assert.AreEqual(ctorPostalAddressUseAndADXPArray.Part.Count ,4);
            Assert.AreEqual(ctorPostalAddressUseAndADXPArray.Use.Count ,2);
        }
        /// <summary>
        /// Tests the AD.Basic() static method, which validates the AD object to see if it is a
        /// BASIC flavored AD. Basic flavor is defined as "City, Province".
        /// 
        /// Expected result: AD.Basic returning False. 
        /// </summary>
        [TestMethod]
        public void ADInvalidADBasicFlavorTest()
        {
            AD invalidBasicFlavorTest = new AD();

            invalidBasicFlavorTest.Part.Add(new ADXP("Hamilton",AddressPartType.City));
            invalidBasicFlavorTest.Part.Add(new ADXP("Ontario",AddressPartType.State));
            invalidBasicFlavorTest.Part.Add(new ADXP("Main Street",AddressPartType.StreetType));

            Assert.IsFalse(AD.IsValidBasicFlavor(invalidBasicFlavorTest));
        }
        /// <summary>
        /// Tests the AD.Basic() static method, which validates the AD object to see if it is a
        /// BASIC flavored AD. Basic flavor is defined as "City, Province".
        /// 
        /// Expected result: AD.Basic returning False. 
        /// </summary>
        [TestMethod]
        public void ADValidADBasicFlavorTest()
        {
            AD invalidBasicFlavorTest = new AD();

            invalidBasicFlavorTest.Part.Add(new ADXP("Hamilton", AddressPartType.City));
            invalidBasicFlavorTest.Part.Add(new ADXP("Ontario", AddressPartType.State));

            Assert.IsTrue(AD.IsValidBasicFlavor(invalidBasicFlavorTest));
        }

        /// <summary>
        /// Tests the .ToString() method for proper formatting. 
        /// Given a formatted string, the .ToString() method should 
        /// return an appropriately formatted output.
        /// 
        /// Expected Result: "Hamilton Ontario CA"
        /// </summary>
        [TestMethod]
        public void ADToStringCorrectTest()
        {
            AD toStringValid = new AD(
                new ADXP[]
                {
                    new ADXP("Hamilton",AddressPartType.City),
                    new ADXP("Ontario", AddressPartType.State),
                    new ADXP("CA",AddressPartType.Country)
                }
            );

            Assert.AreEqual("Hamilton Ontario CA", toStringValid.ToString("{CTY}{STA}{CNT}"));
        }

        /// <summary>
        /// Tests the IsNull property of the ANY Datatype. 
        /// If the NullFlavor is set, the isNull property should be true.
        /// 
        /// Expected Result: "True"
        /// </summary>
        [TestMethod]
        public void ANYIsNullTest()
        {
            AD isNull = new AD();
            isNull.NullFlavor = NullFlavor.AskedUnknown;

            Assert.AreEqual(true, isNull.IsNull);
        }


    }
}
