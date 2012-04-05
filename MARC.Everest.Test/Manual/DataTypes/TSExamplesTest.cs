using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for TSExamplesTest
    /// </summary>
    [TestClass]
    public class TSExamplesTest
    {
        public TSExamplesTest()
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

        /* Examople 30 */

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value               : assigned value for timestamp
        ///     NullFlavor          : null flavor of the timestamp
        /// And the following are Nullified:
        ///     DateValuePrecision  : specific precision of date object    
        /// </summary>
        [TestMethod]
        public void TSExample30Test01()
        {
            TS foo = new TS();
            foo.Value = "2008";
            foo.NullFlavor = NullFlavor.NoInformation;
            foo.DateValuePrecision = null;
            Assert.IsFalse(foo.Validate());
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     Value           : assigned value for timestamp
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test02()
        {
            TS foo = new TS();
            foo.Value = "2001";
            foo.NullFlavor = null;
            foo.DateValuePrecision = null;
            Assert.IsTrue(foo.Validate());
        }



        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     Value           : assigned value for timestamp
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test03()
        {
            TS foo = new TS();
            foo.Value = null;
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Month;
            Assert.IsFalse(foo.Validate());
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test04()
        {
            TS foo = new TS();
            foo.Value = "2001";
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Year;
            Assert.IsTrue(TS.IsValidDateFlavor(foo));
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test05()
        {
            TS foo = new TS();
            foo.Value = "2001";
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Month;
            Assert.IsTrue(TS.IsValidDateFlavor(foo));
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test06()
        {
            TS foo = new TS();
            foo.Value = "2001";
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Day;
            Assert.IsTrue(TS.IsValidDateFlavor(foo));
        }



        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test07()
        {
            TS foo = new TS();
            foo.Value = "2001";
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Hour;
            Assert.IsFalse(TS.IsValidDateFlavor(foo));
            // testing for valid date flavor using precision of hour
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test08()
        {
            TS foo = new TS();
            foo.Value = "2001";
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Minute;
            Assert.IsTrue(TS.IsValidDateTimeFlavor(foo));
            // testing for valid datetime flavor using precision of minute
        }



        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value               : assigned value for timestamp
        ///     DateValuePrecision  : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor          : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test09()
        {
            TS foo = new TS();
            foo.Value = "2001";
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Second;
            Assert.IsFalse(TS.IsValidDateTimeFlavor(foo));
            // testing for valid datetime flavor using precision of second
        }



        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test10()
        {
            TS foo = new TS();
            foo.Value = "2001";
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Month;
            Assert.IsFalse(TS.IsValidFullDateTimeFlavor(foo));
            // testing for valid fulldatetime flavor using precision of month
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test11()
        {
            TS foo = new TS();
            foo.Value = "2001";
            // Console.WriteLine(foo.DateValue);
            // Console.WriteLine(foo.DateValuePrecision);
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Second;
            Assert.IsTrue(TS.IsValidFullDateTimeFlavor(foo));
            // testing for valid fulldatetime flavor using precision of second
        }



        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test12()
        {
            TS foo = new TS();
            foo.Value = "2001";
            // Console.WriteLine(foo.DateValue);
            // Console.WriteLine(foo.DateValuePrecision);
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Month;
            Assert.IsFalse(TS.IsValidFullDateFlavor(foo));
            // testing for valid fulldate flavor using precision of month
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test13()
        {
            TS foo = new TS();
            foo.Value = "2001";
            // Console.WriteLine(foo.DateValue);
            // Console.WriteLine(foo.DateValuePrecision);
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Day;
            Assert.IsTrue(TS.IsValidFullDateFlavor(foo));
            // testing for valid fulldate flavor using precision of day
        }



        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test14()
        {
            TS foo = new TS();
            foo.Value = "2001";
            // Console.WriteLine(foo.DateValue);
            // Console.WriteLine(foo.DateValuePrecision);
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Day;
            Assert.IsFalse(TS.IsValidInstantFlavor(foo));
            // testing for valid instant flavor using precision of day
        }



        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : assigned value for timestamp
        ///     DateValuePrecision : assigned precision for timestamp
        /// And the following are Nullified:
        ///     NullFlavor      : null flavor of the timestamp     
        /// </summary>
        [TestMethod]
        public void TSExample30Test15()
        {
            TS foo = new TS();
            foo.Value = "2001";
            // Console.WriteLine(foo.DateValue);
            // Console.WriteLine(foo.DateValuePrecision);
            foo.NullFlavor = null;
            foo.DateValuePrecision = DatePrecision.Full;
            Assert.IsTrue(TS.IsValidInstantFlavor(foo));
            // testing for valid instant flavor using precision of day
        }
    }
}
