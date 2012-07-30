using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for TSTest
    /// </summary>
    [TestClass]
    public class TSTest
    {
        public TSTest()
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
        /// Testing Function AreEqual must return TRUE.
        /// Cast a DateTime to a TS      
        /// Comparison of the values : Datetime and TS data types must return TRUE to validate the FUNCTION
        /// </summary>  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)"), TestMethod]
        public void TSDateOutputTest()
        {
            DateTime dt = DateTime.Now;
            TS ts = dt;
            string date = dt.ToString("yyyyMMddHHmmss.fffzzzz"); // JF -> TS should only have a 3 digit millisecond precision
            date = date.Replace(":", "");
            Assert.AreEqual(ts.Value,date);
        }

        /// <summary>
        /// Testing Function AreEqual must return TRUE.
        /// Cast a DateTime to a TS      
        /// Comparison of the values : Datetime and TS data types must return TRUE to validate the FUNCTION
        /// </summary>  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Datetime"), TestMethod]
        public void TSDatetimeCheck()
        {
            DateTime dt = DateTime.Now;
            TS ts = dt;
            Assert.AreEqual(ts.DateValue, dt);
        }

        /// <summary>
        /// Testing Function AreEqual must return TRUE.
        /// Variable to be tested:
        ///     Test    :   Flavor handler for TS.Date
        /// Comparison of the values : Flavor for TS and the expected date value 
        /// </summary>  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TSDATETIME"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.Parse(System.String)"), TestMethod]
        public void TSDATETIMEFlavorTest()
        {
            TS test = new TS(DateTime.Parse("September 17, 2009 10:09 AM"));
            test.Flavor = "TS.DATETIME";
            Assert.AreEqual("20090917100900-0400", test.ToString());
        }

        /// <summary>
        /// Testing Function AreNotEqual must return TRUE.
        /// Variable to be tested:
        ///     Test    :   Flavor handler for TS.Date
        /// Comparison of the values : Flavor for TS and the expected date value 
        /// </summary>  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TSDATETIME"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.Parse(System.String)"), TestMethod]
        public void TSDATETIMEFlavorTestWrong1()
        {
            TS test = DateTime.Parse("September 17, 2009 10:09 AM");
            test.Flavor = "DATETIME";
            Assert.AreNotEqual("20090917", test.ToString());
        }

        
        /// <summary>
        /// Testing Function AreEqual must return TRUE.
        /// Variable to be tested:
        ///     Test    :   Flavor handler for TS.Date
        /// Comparison of the values : Flavor for TS and the expected date value 
        /// </summary>  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TSDATE"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.Parse(System.String)"), TestMethod]
        public void TSDATEFlavorTest()
        {
            TS test = new TS(DateTime.Parse("September 17, 2009 10:09 AM"));
            test.Flavor = "DATE";
            Assert.AreEqual("20090917", test.ToString());
        }

        /// <summary>
        /// JF: Testing that precision correctly formats a date
        /// Test    :   DateValuePrecision handler for Year
        /// </summary>
        [TestMethod]
        public void TSDatePrecisionTest()
        {
            TS test = new TS(DateTime.Parse("01/01/2007 14:54"));
            test.DateValuePrecision = DatePrecision.Year;
            Assert.AreEqual("2007", test.Value);
        }

        /// <summary>
        /// JF: Testing that precision correctly formats a date
        /// Test    :   DateValuePrecision handler for Time
        /// </summary>
        [TestMethod]
        public void TSDatePrecisionTestNoTZTest()
        {
            TS test = new TS(DateTime.Parse("01/01/2007 14:54"));
            test.DateValuePrecision = DatePrecision.MinuteNoTimezone;
            Assert.AreEqual("200701011454", test.Value);
        }

        /// <summary>
        /// JF: Testing that parsing a datetime string interprets the correct date/time
        /// </summary>
        [TestMethod]
        public void TSDatePrecisionTestParseTest()
        {
            string strHrNoTz = "2007010114",
                strMinTz = "200701011453-0500",
                strYr = "2007";
            TS hrNoTz = new TS() { Value = strHrNoTz },
                minTz = new TS() { Value = strMinTz },
                yr = new TS() { Value = strYr };

            Assert.AreEqual<DatePrecision?>(DatePrecision.HourNoTimezone, hrNoTz.DateValuePrecision);
            Assert.AreEqual<DatePrecision?>(DatePrecision.Minute, minTz.DateValuePrecision);
            Assert.AreEqual<DatePrecision?>(DatePrecision.Year, yr.DateValuePrecision);


            // value 
            Assert.AreEqual(strHrNoTz, hrNoTz.Value);
            Assert.AreEqual(strMinTz, minTz.Value);
            Assert.AreEqual(strYr, yr.Value);

        }

        /// <summary>
        /// Testing Function AreNotEqual must return FALSE.
        /// Variable to be tested:
        ///     Test    :   Flavor handler for TS.Date
        /// Comparison of the values : Flavor for TS and the expected date value 
        /// </summary>  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TSDATETIME"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.Parse(System.String)"), TestMethod]
        public void TSDATETIMEFlavorTestWrong2Test()
        {
            TS test = new TS(DateTime.Parse("September 17, 2009 10:09 AM"));
            test.Flavor = "FULLDATETIME";
            Assert.AreNotEqual("20090917100900", test.ToString());
        }

        /// <summary>
        /// Casting a String to a TS
        /// Test    :   Explicit conversion from String to TS
        /// </summary>
        [TestMethod]
        public void TSCastStringToTSTest()
        {
            TS test = new TS();
            test = (TS)"2009";
            Assert.AreEqual("2009", test.Value);
            Assert.AreEqual(new DateTime(2009, 01, 01), test.DateValue);
            Assert.AreEqual(DatePrecision.Year, test.DateValuePrecision);
        }

        /// <summary>
        /// Validating the date value using TS.DateValidator()
        /// Test    :   Validating a Date Value
        /// </summary>
        [TestMethod]
        public void TSValidDateValidationTest()
        {
            TS test = new TS();            
            test.Value = "20090101";
            Assert.IsTrue(TS.IsValidDateFlavor(test));
        }

        /// <summary>
        /// Checking for invalid date value using TS.DateValidator()
        /// Test    :   Validating a Date Value
        /// </summary>
        [TestMethod]
        public void TSInvalidDateValidationTest()
        {
            TS test = new TS();
            test.Value = "200901011400";
            Assert.IsFalse(TS.IsValidDateFlavor(test));            
        }

        /// <summary>
        /// Checking for valid datetime value using TS.DateTimeValidator()
        /// Test    :   Validating a Date Value
        /// </summary>
        [TestMethod]
        public void TSValidDateTimeValidationTest()
        {
            TS test = new TS();
            test.Value = "200901011400";
            Assert.IsTrue(TS.IsValidDateTimeFlavor(test));
        }

        /// <summary>
        /// Checking for invalid datetime value using TS.DateTimeValidator()
        /// Test    :   Validating a Date Value
        /// </summary>
        [TestMethod]
        public void TSInvalidDateTimeValidationTest()
        {
            TS test = new TS();
            test.Value = "20090101140000.000-0500";                          
            //Throws FormatException. Bug ID: 1005. test.Value should be "20090101140000.000-0500"
            Assert.IsFalse(TS.IsValidDateTimeFlavor(test));
        }

        /// <summary>
        /// Checking for valid datetime value with timezone using TS.DateTimeValidator()
        /// Test    :   Validating a Date Value
        /// </summary>
        [TestMethod]
        public void TSValidDateTimeWithTZTest()
        {
            TS test = new TS();
            test.Value = "2009010114-0500";
            Assert.IsTrue(TS.IsValidDateTimeFlavor(test));
        }

        /// <summary>
        /// Checking for valid full datetime value using TS.FullDateTimeValidator()
        /// Test    :   Validating a Date Value
        /// </summary>
        [TestMethod]
        public void TSValidFullDateTimeTest()
        {
            TS test = new TS();
            test.Value = "20090101140033-0500";
            Assert.IsTrue(TS.IsValidFullDateTimeFlavor(test));
        }

        /// <summary>
        /// Checking for invalid full datetime value using TS.FullDateTimeValidator()
        /// Test    :   Validating a Date Value
        /// </summary>
        [TestMethod]
        public void TSInvalidFullDateTimeTest()
        {
            TS test = new TS();
            test.Value = "2009";
            Assert.IsFalse(TS.IsValidFullDateTimeFlavor(test));
        }

        /// <summary>
        /// Timestamp with some sort of magical 4 digit millisecond count
        /// </summary>
        [TestMethod]
        public void TSWithFourDigitMillisecondsWithTimezone()
        {
            TS test = new TS();
            test.Value = "20110103034932.1920-0500";
            Assert.AreEqual(DateTime.Parse("2011/01/03 03:49:32.192").Millisecond, test.DateValue.Millisecond);
        }

        /// <summary>
        /// Timestamp with some sort of magical 4 digit millisecond count
        /// </summary>
        [TestMethod]
        public void TSWithFourDigitMillisecondsWithoutTimezone()
        {
            TS test = new TS();
            test.Value = "20110103034932.1920";
            Assert.AreEqual(DateTime.Parse("2011/01/03 03:49:32.192").Millisecond, test.DateValue.Millisecond);
        }

      

        /// <summary>
        /// Converting a TS to an IVL with a precision of Year
        /// Test    :   Converting a TS to an IVL
        /// </summary>
        [TestMethod]
        public void TSToIVLYearTest()
        {
            TS test = new TS();
            test.Value = "2009";
            Assert.AreEqual(new TS(DateTime.Parse("2009/01/01 00:00:00.000")).ToString(), test.ToIVL().Low.ToString());
            //Bug: Should be using 12h format, instead of 24h.
            Assert.AreEqual(new TS(DateTime.Parse("2009/12/31 23:59:59.999")).ToString(), test.ToIVL().High.ToString());
        }

        /// <summary>
        /// Converting a TS to an IVL with a precision of Month
        /// Test    :   Converting a TS to an IVL
        /// </summary>
        [TestMethod]
        public void TSToIVLMonthTest()
        {
            TS test = new TS();
            test.Value = "200901";
            Assert.AreEqual(new TS(DateTime.Parse("2009/01/01 00:00:00.000")).ToString(), test.ToIVL().Low.ToString());
            //Bug: Should be using 12h format, instead of 24h.
            Assert.AreEqual(new TS(DateTime.Parse("2009/01/31 23:59:59.999")).ToString(), test.ToIVL().High.ToString());
        }

        /// <summary>
        /// Converting a TS to an IVL with a precision of Day
        /// Test    :   Converting a TS to an IVL
        /// </summary>
        [TestMethod]
        public void TSToIVLDayTest()
        {
            TS test = new TS();
            test.Value = "20090101";
            Assert.AreEqual(new TS(DateTime.Parse("2009/01/01 00:00:00.000")).ToString(), test.ToIVL().Low.ToString());
            //Bug: Should be using 12h format, instead of 24h.
            Assert.AreEqual(new TS(DateTime.Parse("2009/01/01 23:59:59.999")).ToString(), test.ToIVL().High.ToString());
        }

        /// <summary>
        /// Converting a TS to an IVL with a precision of Hour
        /// Test    :   Converting a TS to an IVL
        /// </summary>
        [TestMethod]
        public void TSToIVLHourTest()
        {
            TS test = new TS();
            test.Value = "2009010111";
            //Throws FormatException. Bug ID 1004. test.ToIVL() does not contain a case for HourNoTimezone
            Assert.AreEqual(new TS(DateTime.Parse("2009/01/01 11:00:00.000")).ToString(), test.ToIVL().Low.ToString());
            Assert.AreEqual(new TS(DateTime.Parse("2009/01/01 11:59:59.999")).ToString(), test.ToIVL().High.ToString());           
        }

        /// <summary>
        /// Converting a TS to an IVL with a precision of Full
        /// Test    :   Converting a TS to an IVL
        /// </summary>
        [TestMethod]
        public void TSToIVLFullTest()
        {
            TS test = new TS();
            DateTime testTime = DateTime.Now;
            test.Value = testTime.ToString("yyyyMMddHHmmss.fffzzzz").Replace(":","");
            
            //Throws FormatException. Bug ID 1004. test.ToIVL() does not contain a case for HourNoTimezone
            Assert.AreEqual(testTime.ToString("yyyyMMddHHmmss.fffzzzz"), test.ToIVL().Low.DateValue.ToString("yyyyMMddHHmmss.fffzzzz"));
            Assert.AreEqual(testTime.ToString("yyyyMMddHHmmss.fffzzzz"), test.ToIVL().High.DateValue.ToString("yyyyMMddHHmmss.fffzzzz"));
        }
    }
}
