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
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;
using System.IO;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for EDCompressionTest
    /// </summary>
    [TestClass]
    public class EDCompressionTest
    {
        public EDCompressionTest()
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

        /* Example 16 */

        // JF - Test has failed due to local resource being loaded
        ///// <summary>
        ///// Testing Function Validate must return TRUE.
        ///// When the following values are not Nullified:
        /////     Value
        /////     Compression
        ///// </summary>
        //[TestMethod]
        //public void EDCompTest01()
        //{
        //    ED edInstance = new ED(File.ReadAllBytes(@"C:\Users\pittersj\Dropbox\HL7\PRPA_IN101103CA.xml"),"text/xml");
                
        //    // Compress data
        //    ED compressionSample = edInstance.Compress(EncapsulatedDataCompression.GZ);
        //    Console.WriteLine("COMPRESSED:");
        //    Console.WriteLine(edInstance.ToString());

        //    // uncompress data and store in edInstance
        //    edInstance = compressionSample.UnCompress();

        //    Console.WriteLine("UNCOMPRESSED:");
        //    Console.WriteLine(edInstance.ToString());
        //    Assert.IsTrue(edInstance.Validate());
        //}


        // JF: Test has failed, need to load XML from a resource
        ///// <summary>
        ///// Testing Function Validate must return TRUE.  
        ///// Testing if the raw instance is equal after being compressed and then uncompressed.
        ///// </summary>
        //[TestMethod]
        //public void EDCompTest02()
        //{
        //    ED edInstance = new ED(File.ReadAllBytes(@"C:\Users\pittersj\Dropbox\HL7\PRPA_IN101103CA.xml"), "text/xml");

        //    // Compress data
        //    ED compressionSample = edInstance.Compress(EncapsulatedDataCompression.GZ);
        //    Console.WriteLine("COMPRESSED:");
        //    Console.WriteLine(edInstance.ToString());

        //    // uncompress data and store in a new instance of ED
        //    ED edInstance2 = compressionSample.UnCompress();
        //    Console.WriteLine("UNCOMPRESSED:");
        //    Console.WriteLine(edInstance.ToString());

        //    // compare compressed data with uncompressed data
        //    Assert.AreEqual(edInstance, edInstance2);
        //}

        // JF - Test has failed because data is loaded from disk
        ///// <summary>
        ///// Testing Function Validate must return FALSE.  
        ///// Testing if the raw instance is equal after being compressed.
        ///// </summary>
        //[TestMethod]
        //public void EDCompTest03()
        //{
        //    ED edInstance = new ED(File.ReadAllBytes(@"C:\Users\pittersj\Dropbox\HL7\PRPA_IN101103CA.xml"), "text/xml");

        //    // Compress data
        //    ED compressionSample = edInstance.Compress(EncapsulatedDataCompression.GZ);
        //    Console.WriteLine("COMPRESSED:");
        //    Console.WriteLine(edInstance.ToString());

        //    // store uncompressed data with new instance of ED
        //    ED edInstance2 = compressionSample.UnCompress();
        //    Console.WriteLine("UNCOMPRESSED:");
        //    Console.WriteLine(edInstance.ToString());
        //    Assert.AreNotEqual(compressionSample, edInstance);
        //}

        // JF - Test has failed because data is loaded from disk
        ///// <summary>
        ///// Testing Function Validate must return FALSE.  
        ///// Testing if the compressed instance is equal after being uncompressed.
        ///// </summary>
        //[TestMethod]
        //public void EDCompTest04()
        //{
        //    ED edInstance = new ED(File.ReadAllBytes(@"C:\Users\pittersj\Dropbox\HL7\PRPA_IN101103CA.xml"), "text/xml");

        //    // Compress data
        //    ED compressionSample = edInstance.Compress(EncapsulatedDataCompression.GZ);
        //    Console.WriteLine("COMPRESSED:");
        //    Console.WriteLine(edInstance.ToString());

        //    // store uncompressed data in new instance of ED
        //    ED edInstance2 = compressionSample.UnCompress();
        //    Console.WriteLine("UNCOMPRESSED:");
        //    Console.WriteLine(edInstance.ToString());
        //    Assert.AreNotEqual(compressionSample, edInstance2);
        //}
    }
}
