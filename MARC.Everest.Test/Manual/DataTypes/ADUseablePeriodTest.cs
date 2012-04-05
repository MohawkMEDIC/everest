using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for ADExamplesTest01
    /// </summary>
    [TestClass]
    public class ADUseablePeriodTest
    {
        public ADUseablePeriodTest()
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

        /* Example 35 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Parts       : sequence of entity address expression parts that comprise the address
        /// And the following values are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void ADUPTest01()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[]{
                    new ADXP("321 Cedar Rd. North", AddressPartType.StreetAddressLine),
                    new ADXP("Thunder Bay", AddressPartType.City),
                    new ADXP("Ontario", AddressPartType.State),
                    new ADXP("Canada", AddressPartType.Country),
                    new ADXP("N2N2N4", AddressPartType.PostalCode)
                }
                );

            // Assign a usable period interval to the address
            vacationHome.UseablePeriod = new GTS(
                new PIVL<TS>(
                    new TS(DateTime.Parse("01-May-2011"),
                        DatePrecision.Month).ToIVL(),
                        new PQ (1, "a")
                    )
                );

            vacationHome.NullFlavor = null;
            Assert.IsTrue(vacationHome.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// Tesing if address is valid with empty useable period.
        /// </summary>
        [TestMethod]
        public void ADUPTest02()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[]{
                    new ADXP("321 Cedar Rd. North", AddressPartType.StreetAddressLine),
                    new ADXP("Thunder Bay", AddressPartType.City),
                    new ADXP("Ontario", AddressPartType.State),
                    new ADXP("Canada", AddressPartType.Country),
                    new ADXP("N2N2N4", AddressPartType.PostalCode)
                }
                );

            // Assign an empty usable period interval to the address
            vacationHome.UseablePeriod = new GTS();

            vacationHome.NullFlavor = null;

            Assert.IsTrue(vacationHome.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// Tesing if address is valid with logical useable period
        ///     (eg. January 1, 2012 - January 31, 2012)
        /// </summary>
        [TestMethod]
        public void ADUPTest03()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[]{
                    new ADXP("321 Cedar Rd. North", AddressPartType.StreetAddressLine),
                    new ADXP("Thunder Bay", AddressPartType.City),
                    new ADXP("Ontario", AddressPartType.State),
                    new ADXP("Canada", AddressPartType.Country),
                    new ADXP("N2N2N4", AddressPartType.PostalCode)
                }
                );

            // Assign a VALID usable period interval to the address
            vacationHome.UseablePeriod = new GTS(
                new PIVL<TS>(
                    new IVL<TS>(
                        new TS(new DateTime(2012, 01, 01), DatePrecision.Day),
                        new TS(new DateTime(2012, 01, 31), DatePrecision.Day)
                        ),
                        new PQ(1, "a")
                    )
                    );

            vacationHome.NullFlavor = null;

            Assert.IsTrue(vacationHome.Validate());
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// Tesing if address is valid with illogical useable period
        ///     (eg. January 31, 2012 - January 1, 2012)
        /// </summary>
        [TestMethod]
        public void ADUPTest04()
        {
            AD vacationHome = new AD(PostalAddressUse.VacationHome,
                new ADXP[]{
                    new ADXP("321 Cedar Rd. North", AddressPartType.StreetAddressLine),
                    new ADXP("Thunder Bay", AddressPartType.City),
                    new ADXP("Ontario", AddressPartType.State),
                    new ADXP("Canada", AddressPartType.Country),
                    new ADXP("N2N2N4", AddressPartType.PostalCode)
                }
                );

            try
            {
                // Assign an INVALID usable period interval to the address
                vacationHome.UseablePeriod = new GTS(
                    new PIVL<TS>(
                        new IVL<TS>(
                            new TS(new DateTime(2012, 01, 31), DatePrecision.Day),
                            new TS(new DateTime(2012, 01, 01), DatePrecision.Day)
                            ),
                            new PQ(1, "a")
                        )
                        );

                // test for valid instance of address
                Assert.IsTrue(vacationHome.Validate());
                Console.WriteLine("Useable period is valid.");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
                // should return error
                Assert.IsTrue(e.ToString().Contains("OutOfRangeException"));
            }
        }
    }
}
