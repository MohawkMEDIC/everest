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
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes.Interfaces;
using System.Reflection;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for ThumbExamplesTest
    /// </summary>
    [TestClass]
    public class ThumbExamplesTest
    {
        public ThumbExamplesTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static string[] GetResourceList()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceNames();
        }

        public static Stream GetResourceStream(string scriptname)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(scriptname);
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

        /* Example 18 */


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Thumbnail       : Encapsulated data (image file)
        ///     Reference       : TEL.URL link to data
        /// And the following are Nullified:
        ///     NullFlavor
        ///     Data            : Raw Data
        /// </summary>
        [TestMethod]
        public void ThumbExampleTest01()
        {
            //build array of bytes for "ed.data"
            byte[] value = new byte[50];
            for (int i = 0; i < 50; i++) { value[i] = 0; }
            ED ed = new ED((TEL)"http://s.hhs.on.ca/media/loops/loop01.avi");

            // creates a new thumbnail
            ED thumbnail = new ED(System.Text.Encoding.UTF8.GetBytes("Hello, this is a test"), "image/gif");

            // Set the thumbnail to a compressed version of the image
            //ed.Compression = EncapsulatedDataCompression.GZ;
            ed.Thumbnail = thumbnail.Compress(EncapsulatedDataCompression.GZ);
            ed.Reference = "http://www.whatever.com/";
            ed.NullFlavor = null;
            ed.Data = null;
            Assert.IsTrue(ed.Validate());
        }

        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     Thumbnail       : Encapsulated data (image file)
        ///     Reference       : TEL.URL link to data
        ///     NullFlavor
        /// And the following are Nullified:
        ///     Data            : Raw Data    
        /// 
        /// </summary>
        [TestMethod]
        public void ThumbExampleTest02()
        {
            //build array of bytes for "ed.data"
            byte[] value = new byte[50];
            for (int i = 0; i < 50; i++) { value[i] = 0; }
            ED ed = new ED((TEL)"http://s.hhs.on.ca/media/loops/loop01.avi");

            // creates a new thumbnail
            ED thumbnail = new ED(System.Text.Encoding.UTF8.GetBytes("Hello, this is a test"), "image/gif");

            // Set the thumbnail to a compressed version of the image
            //ed.Compression = EncapsulatedDataCompression.GZ;
            ed.Thumbnail = thumbnail.Compress(EncapsulatedDataCompression.GZ);
            ed.Reference = null;
            ed.NullFlavor = NullFlavor.NoInformation;
            ed.Data = value;
            Assert.IsFalse(ed.Validate());
        }
    }
}
