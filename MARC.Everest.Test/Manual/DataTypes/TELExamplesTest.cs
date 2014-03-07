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
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for TELExamplesTest
    /// </summary>
    [TestClass]
    public class TELExamplesTest
    {
        public TELExamplesTest()
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

        /* Example 6 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           : Phone number
        ///     Use             : Workplace, Direct
        /// And the following are Nullified:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void TELConstructionTest1()
        {
            // 
            TEL phone = new TEL("tel:+13335551212;postd=2345",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
            }
            );
            phone.NullFlavor = null;
            Assert.IsTrue(phone.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Value           : Phone number
        ///     Use             : Workplace, Direct
        ///     NullFlavor
        ///     
        /// </summary>
        [TestMethod]
        public void TELConstructionTest2()
        {
            TEL phone = new TEL("tel:+13335551212;postd=2345",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
            }
            );
            phone.NullFlavor = NullFlavor.Unknown;
            Assert.IsFalse(phone.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// 
        /// Testing URI/URL flavors.
        /// </summary>
        [TestMethod]
        public void TELConstructionTest3()
        {
            TEL mail = new TEL("mailto:abc@gmail.com",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
            }
            );
            mail.NullFlavor = null;
            Assert.IsTrue(TEL.IsValidEMailFlavor(mail));
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// 
        /// Testing URI/URL flavors
        /// </summary>
        [TestMethod]
        public void TELConstructionTest4()
        {
            TEL mail = new TEL("mailtoXYZ:abc@gmail.com",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
            }
            );
            mail.NullFlavor = null;
            Assert.IsFalse(TEL.IsValidEMailFlavor(mail));
        }


        ///// <summary>
        ///// Testing Function Validate must return TRUE.
        ///// Testing URI/URL flavors
        ///// </summary>
        //[TestMethod]
        //public void TELConstructionTest5()
        //{
        //    TEL url = new TEL("http://www.google.com",      // Google is not practical, of course,
        //        new CS<TelecommunicationAddressUse>[]{      // but should still qualify as a valid URL
        //            TelecommunicationAddressUse.WorkPlace,
        //            TelecommunicationAddressUse.Direct
        //        });
        //    url.NullFlavor = null;
        //    Assert.IsTrue(TEL.IsValidUrlFlavor(url));
        //}


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// 
        /// Testing URI/URL flavors
        /// </summary>
        [TestMethod]
        public void TELConstructionTest6()
        {
            TEL url = new TEL("httpXYZ://www.google.com",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
            }
            );
            url.NullFlavor = null;
            Assert.IsFalse(TEL.IsValidUriFlavor(url));
        }

        ///// <summary>
        ///// Testing Function Validate must return TRUE.
        ///// 
        ///// Testing URI/URL flavors
        ///// </summary>
        //[TestMethod]
        //public void TELConstructionTest7()
        //{
        //    TEL uri = new TEL("file://C:/Test%20Files/mydata.xml",
        //        new CS<TelecommunicationAddressUse>[]{
        //            TelecommunicationAddressUse.WorkPlace,
        //            TelecommunicationAddressUse.Direct
        //    }
        //    );
        //    uri.NullFlavor = null;
        //    Assert.IsTrue(TEL.IsValidUriFlavor(uri));
        //}


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// 
        /// Testing URI/URL flavors
        /// </summary>
        [TestMethod]
        public void TELConstructionTest8()
        {
            TEL uri = new TEL("fileXYZ://www.google.com",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
            }
            );
            uri.NullFlavor = null;
            Assert.IsFalse(TEL.IsValidUriFlavor(uri));
        }




        /* Example 7 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value
        ///     Use             :   Specific use for the telecommunications address
        /// And the following are nullified:
        ///     Capabilities    :   Identifies the capabilities of the device listening on the 
        ///                         telecommunications address.
        ///     Useable Period  :   Period of time during which the telecommunications
        ///                         addressed can be accessed.
        /// 
        /// </summary>
        [TestMethod]
        public void TELExample7Test01()
        {
            var tel = new TEL("tel:+13335551212;postd=2345",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
                }
            );
            tel.Capabilities = null;
            tel.UseablePeriod = null;
            Assert.IsTrue(tel.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE
        /// </summary>
        [TestMethod]
        public void TELExample7Test02()
        {
            var tel = new TEL("+13335551212;postd=2345",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
                }
            );
            Assert.IsFalse(TEL.IsValidPhoneFlavor(tel));
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Use     :   Specific use for the telecommunications address
        /// 
        /// </summary>
        [TestMethod]
        public void TELExample7Test03()
        {
            var tel = new TEL("",
                new CS<TelecommunicationAddressUse>[]{
                    TelecommunicationAddressUse.WorkPlace,
                    TelecommunicationAddressUse.Direct
                }
            );
            tel.Value = "tel:+13335551212;postd=2345";
            tel.Use = null;
            Assert.IsTrue(tel.Validate());
        }




        /* Example 8 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value 
        ///     Useable Period  :   Period of time during which the telecommunications
        ///                         addressed can be accessed.
        /// And the following values are Nullified:
        ///     Use             :   Specific use for the telecommunications address
        /// 
        /// </summary>
        [TestMethod]
        public void TELExample8Test01()
        {
            var tel = new TEL("tel:+190555525485");
            var weekdays = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("08-15-2011"), DateTime.Parse("08-19-2011")),
                new PQ(-1, "wk")
            );
            var nineToFive = new PIVL<TS>(
                // given low, high, and inaccurate width
                new IVL<TS>(DateTime.Parse("08-15-2011 09:00 AM"),
                    DateTime.Parse("08-19-2011 05:00 PM")),
                    new PQ(-1, "d")
                )
            {
                Operator = SetOperator.Intersect
            };
            tel.UseablePeriod = new GTS(new SXPR<TS>(
                new SXCM<TS>[] {
                    weekdays,
                    nineToFive
                }
            ));
            tel.Use = null;
            Assert.IsTrue(tel.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Value           :
        /// And the following values are not specified:
        ///     Useable Period  :   Period of time specified by PIVLs
        ///     Use             :   Specific use for the telecommunications address
        /// </summary>
        [TestMethod]
        public void TELExample8Test02()
        {
            var tel = new TEL("tel:+190555525485");
            var weekdays = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("08-15-2011"), DateTime.Parse("08-19-2011")),
                new PQ(-1, "wk")
            );

            var nineToFive = new PIVL<TS>(
                // given low, high, and inaccurate width
                new IVL<TS>(DateTime.Parse("08-15-2011 09:00 AM"),
                    DateTime.Parse("08-19-2011 05:00 PM")),
                    new PQ(-1, "d")
                )
            {
                Operator = SetOperator.Intersect
            };
            tel.UseablePeriod = null;
            tel.Use = null;
            Assert.IsTrue(tel.Validate());
        }
    }
}
