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
using MARC.Everest.DataTypes.Converters;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Unit tests for the Containment methods
    /// </summary>
    [TestClass]
    public class ContainmentTests
    {
        public ContainmentTests()
        {
           
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        /// <summary>
        /// Tests that an INT is contained within
        /// an Interval of ints
        /// </summary>
        [TestMethod]
        public void IVLINTContainsTest()
        {
            IVL<INT> container = new IVL<INT>(1, 10);
            Assert.IsTrue(container.Contains(9));
        }

        /// <summary>
        /// Tests that an INT is not contained within
        /// an Interval of ints with a width (function
        /// should throw exception)
        /// </summary>
        [TestMethod]
        public void IVLINTNotContainsExceptionTest()
        {
            IVL<INT> container = new IVL<INT>(1, null);
            container.Width = new PQ(1, "ft");
            try
            {
                container.Contains(9);
                Assert.IsTrue(false);
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        /// <summary>
        /// Tests that an INT is not contained within
        /// an Interval of ints
        /// </summary>
        [TestMethod]
        public void IVLINTNotContainsTest()
        {
            IVL<INT> container = new IVL<INT>(1, 10);
            Assert.IsFalse(container.Contains(90));
        }

        /// <summary>
        /// Tests that an PQ is not contained within
        /// an Interval of PQs
        /// </summary>
        [TestMethod]
        public void IVLPQNotContainsTest()
        {
            IVL<PQ> ageRange = new IVL<PQ>(
                new PQ(1, "a"),
                new PQ(3, "a")
            );
            Assert.IsTrue(ageRange.Contains(new PQ(24,"mo")));
        }

        /// <summary>
        /// Tests that an TS is contained within
        /// an Interval of TS
        /// </summary>
        [TestMethod]
        public void IVLTSContainsTest()
        {
            IVL<TS> container = new IVL<TS>(DateTime.MinValue, DateTime.MaxValue);
            Assert.IsTrue(container.Contains(DateTime.Now));
        }

        /// <summary>
        /// Tests that an TS is contained within
        /// an Interval of TS
        /// </summary>
        [TestMethod]
        public void IVLTSNotContainsTest()
        {
            IVL<TS> container = new IVL<TS>(DateTime.MinValue, DateTime.Now);
            Assert.IsFalse(container.Contains(DateTime.MaxValue));
        }

        /// <summary>
        /// Tests that an TS is contained within
        /// an Interval of TS with a low and width specified
        /// </summary>
        [TestMethod]
        public void IVLTSContainsWidthTest()
        {
            // Create an IVL starting on 20000101
            IVL<TS> container = new IVL<TS>(DateTime.Parse("2000-01-01"), null);
            container.Width = new PQ(1, "a");
            Assert.IsTrue(container.Contains(DateTime.Parse("2000-05-22")));
        }

        /// <summary>
        /// Tests that an TS is contained within
        /// an Interval of TS with a low and width specified
        /// </summary>
        [TestMethod]
        public void IVLTSNotContainsWidthTest()
        {
            // Create an IVL starting on 20000101
            IVL<TS> container = new IVL<TS>(DateTime.Parse("2000-01-01"), null);
            container.Width = new PQ(1, "a");
            Assert.IsFalse(container.Contains(DateTime.Parse("2002-05-22")));
        }

        /// <summary>
        /// Tests the translation of an IVL
        /// </summary>
        [TestMethod]
        public void IVLValueTranslationTest()
        {
            IVL<TS> start = new IVL<TS>(DateTime.Parse("2001-01-01"));
            var translated = start.Translate(new PQ(1, "a"));
            Assert.AreEqual(2002, translated.Value.DateValue.Year);
        }

        /// <summary>
        /// Tests the translation of an IVL
        /// </summary>
        [TestMethod]
        public void IVLLowHighTranslationTest()
        {
            IVL<TS> start = new IVL<TS>(DateTime.Parse("2001-01-01"), DateTime.Parse("2002-01-01"));
            var translated = start.Translate(new PQ(1, "a"));
            Assert.AreEqual(2002, translated.Low.DateValue.Year);
            Assert.AreEqual(2003, translated.High.DateValue.Year);
        }

        /// <summary>
        /// Tests that a periodic interval contains a value
        /// in the range of the Phase
        /// </summary>
        [TestMethod]
        public void PIVLPQContainmentTest()
        {
            // 3 - 6 ft with period of 20 ft
            // PIVL has set of
            // 3..6, 23..26, 43..46
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            PIVL<PQ> start = new PIVL<PQ>(
                new IVL<PQ>((PQ)"3 m", (PQ)"6 m"),
                (PQ)"10 km"
            );
            Assert.IsTrue(start.Contains((PQ)"4 m"));
        }

        /// <summary>
        /// Tests that a periodic interval contains a value
        /// in the range of the Phase
        /// </summary>
        [TestMethod]
        public void PIVLPQExtensionContainmentTest()
        {
            // 3 - 6 ft with period of 20 ft
            // PIVL has set of
            // 3..6, 23..26, 43..46
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            PIVL<PQ> start = new PIVL<PQ>(
                new IVL<PQ>((PQ)"3 m", (PQ)"6 m"),
                (PQ)"10 km"
            );
            Assert.IsTrue(start.Contains((PQ)"60.004 km"));
        }

        /// <summary>
        /// Tests that a periodic interval does not contains a value
        /// in the range of the Phase
        /// </summary>
        [TestMethod]
        public void PIVLPQNotContainmentTest()
        {
            // 3 - 6 ft with period of 20 ft
            // PIVL has set of
            // 3..6, 23..26, 43..46
            PQ.UnitConverters.Add(new SimpleSiUnitConverter());
            PIVL<PQ> start = new PIVL<PQ>(
                new IVL<PQ>((PQ)"3 ft", (PQ)"6 ft"),
                (PQ)"20 ft"
            );
            Assert.IsFalse(start.Contains((PQ)"18 ft"));
        }

        /// <summary>
        /// Tests that a periodic interval contains a value
        /// in the range of the Phase
        /// </summary>
        [TestMethod]
        public void PIVLTSContainmentTest()
        {
            // Every Monday
            PIVL<TS> start = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("2011-08-29"), null) { Width = new PQ(1, "d") },
                (PQ)"1 wk"
            );
            Assert.IsTrue(start.Contains((TS)DateTime.Parse("2011-08-29 5:55:00 PM")));
        }

        /// <summary>
        /// Tests that a periodic interval contains a value
        /// in the range of the Phase
        /// </summary>
        [TestMethod]
        public void PIVLTSExtensionContainmentTest()
        {
            // Every Monday
            PIVL<TS> start = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("2011-08-29"), null) { Width = new PQ(1, "d") },
                (PQ)"1 wk"
            );
            Assert.IsTrue(start.Contains((TS)DateTime.Parse("2012-05-07 5:55:00 PM")));
        }

        /// <summary>
        /// First week of may test
        /// </summary>
        [TestMethod]
        public void PIVLTSExtensionExtremityContainmentTest()
        {
            // Every Monday
            PIVL<TS> start = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("2011-05-01"), DateTime.Parse("2011-05-07")),
                (PQ)"1 a"
            );
            Assert.IsTrue(start.Contains((TS)DateTime.Parse("2025-05-04 5:55:00 PM")));
        }

        /// <summary>
        /// First week of may test
        /// </summary>
        [TestMethod]
        public void PIVLTSExtensionExtremityNotContainmentTest()
        {
            // Every Monday
            PIVL<TS> start = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("2011-05-02"), DateTime.Parse("2011-05-06")),
                (PQ)"1 wk"
            );
            Assert.IsFalse(start.Contains((TS)DateTime.Parse("2025-04-27 5:55:00 PM")));
        }
        /// <summary>
        /// Tests that a periodic interval does not contains a value
        /// in the range of the Phase
        /// </summary>
        [TestMethod]
        public void PIVLTSNotContainmentTest()
        {
            // Every Monday
            PIVL<TS> start = new PIVL<TS>(
                new IVL<TS>(DateTime.Parse("2011-08-29"), null) { Width = new PQ(1, "d") },
                (PQ)"1 wk"
            );
            Assert.IsFalse(start.Contains((TS)DateTime.Parse("2012-05-09 5:55:00 PM")));
        }
    }
}
