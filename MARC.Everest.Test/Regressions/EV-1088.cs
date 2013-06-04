using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.Regressions
{
    [TestClass]
    public class EV_1088
    {
        ///// <summary>
        ///// Tests that the PQ instance is formatted correctly for french decimals
        ///// </summary>
        //[TestMethod]
        //public void EV_1088_PQLocalizationR1Test()
        //{

        //    // Setup
        //    var defaultCulture = EverestFrameworkContext.CurrentCulture;
        //    EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);

        //    // Graph 
        //    PQ test = new PQ((decimal)1.20, "mm");

        //    // Serialize
        //    string serialized = R1SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,2"));

        //    test.Precision = 2;
        //    serialized = R1SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,20"));

        //    // Parse
        //    PQ parsed = R1SerializationHelper.ParseString<PQ>(serialized);
        //    Assert.AreEqual(test, parsed);

        //    // Teardown
        //    EverestFrameworkContext.CurrentCulture = defaultCulture;

        //}

        ///// <summary>
        ///// Tests that the PQ instance is formatted correctly for french decimals
        ///// </summary>
        //[TestMethod]
        //public void EV_1088_PQLocalizationR2Test()
        //{

        //    // Setup
        //    var defaultCulture = EverestFrameworkContext.CurrentCulture;
        //    EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);

        //    // Graph 
        //    PQ test = new PQ((decimal)1.20, "mm");

        //    // Serialize
        //    string serialized = R2SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,2"));

        //    test.Precision = 2;
        //    serialized = R2SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,20"));

        //    // Parse
        //    PQ parsed = R2SerializationHelper.ParseString<PQ>(serialized);
        //    Assert.AreEqual(test, parsed);

        //    // Teardown
        //    EverestFrameworkContext.CurrentCulture = defaultCulture;

        //}

        ///// <summary>
        ///// Tests that the MO instance is formatted correctly for french decimals
        ///// </summary>
        //[TestMethod]
        //public void EV_1088_MOLocalizationR1Test()
        //{

        //    // Setup
        //    var defaultCulture = EverestFrameworkContext.CurrentCulture;
        //    EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);

        //    // Graph 
        //    MO test = new MO((decimal)1.20, "cad");

        //    // Serialize
        //    string serialized = R1SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,2"));

        //    test.Precision = 2;
        //    serialized = R1SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,20"));

        //    // Parse
        //    MO parsed = R1SerializationHelper.ParseString<MO>(serialized);
        //    Assert.AreEqual(test, parsed);

        //    // Teardown
        //    EverestFrameworkContext.CurrentCulture = defaultCulture;

        //}

        ///// <summary>
        ///// Tests that the MO instance is formatted for the french locale
        ///// </summary>
        //[TestMethod]
        //public void EV_1088_MOLocalizationR2Test()
        //{

        //    // Setup
        //    var defaultCulture = EverestFrameworkContext.CurrentCulture;
        //    EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);

        //    // Graph 
        //    MO test = new MO((decimal)1.20, "mm");

        //    // Serialize
        //    string serialized = R2SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,2"));

        //    test.Precision = 2;
        //    serialized = R2SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,20"));

        //    // Parse
        //    MO parsed = R2SerializationHelper.ParseString<MO>(serialized);
        //    Assert.AreEqual(test, parsed);

        //    // Teardown
        //    EverestFrameworkContext.CurrentCulture = defaultCulture;

        //}
        ///// <summary>
        ///// Tests that the REAL instance is formatted for the french locale
        ///// </summary>
        //[TestMethod]
        //public void EV_1088_REALLocalizationR1Test()
        //{

        //    // Setup
        //    var defaultCulture = EverestFrameworkContext.CurrentCulture;
        //    EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);

        //    // Graph 
        //    REAL test = new REAL(1.20f);

        //    // Serialize
        //    string serialized = R1SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,2"));

        //    test.Precision = 2;
        //    serialized = R1SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,20"));

        //    // Parse
        //    REAL parsed = R1SerializationHelper.ParseString<REAL>(serialized);
        //    Assert.AreEqual(test, parsed);

        //    // Teardown
        //    EverestFrameworkContext.CurrentCulture = defaultCulture;

        //}

        ///// <summary>
        ///// Tests that the REAL instance for formatted correctly for the french locale
        ///// </summary>
        //[TestMethod]
        //public void EV_1088_REALLocalizationR2Test()
        //{

        //    // Setup
        //    var defaultCulture = EverestFrameworkContext.CurrentCulture;
        //    EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);

        //    // Graph 
        //    REAL test = new REAL(1.20f);

        //    // Serialize
        //    string serialized = R2SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,2"));

        //    test.Precision = 2;
        //    serialized = R2SerializationHelper.SerializeAsString(test);
        //    Assert.IsTrue(serialized.Contains("1,20"));

        //    // Parse
        //    REAL parsed = R2SerializationHelper.ParseString<REAL>(serialized);
        //    Assert.AreEqual(test, parsed);

        //    // Teardown
        //    EverestFrameworkContext.CurrentCulture = defaultCulture;

        //}


        /// <summary>
        /// Tests that PQ.ToString() is formatted correctly for the french locale
        /// </summary>
        [TestMethod]
        public void EV_1088_PQToStringLocalizationTest()
        {
            // Setup
            var defaultCulture = EverestFrameworkContext.CurrentCulture;
            EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);
            
            // Graph 
            PQ test = new PQ((decimal)1.20, "mm");
            test.Precision = 2;
            Assert.AreEqual("1,20 mm", test.ToString());
            // Teardown
            EverestFrameworkContext.CurrentCulture = defaultCulture;
        }

        /// <summary>
        /// Tests that MO.ToString() is formatted correctly for the french locale
        /// </summary>
        [TestMethod]
        public void EV_1088_MOToStringLocalizationTest()
        {
            // Setup
            var defaultCulture = EverestFrameworkContext.CurrentCulture;
            EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);

            // Graph 
            MO test = new MO((decimal)1.20, "CAD");
            test.Precision = 2;
            Assert.AreEqual("1,20 CAD", test.ToString());
            // Teardown
            EverestFrameworkContext.CurrentCulture = defaultCulture;
        }

        /// <summary>
        /// Tests that REAL.ToString() is formatted correctly for the french locale
        /// </summary>
        [TestMethod]
        public void EV_1088_REALToStringLocalizationTest()
        {
            // Setup
            var defaultCulture = EverestFrameworkContext.CurrentCulture;
            EverestFrameworkContext.CurrentCulture = new System.Globalization.CultureInfo("fr-FR", false);

            // Graph 
            REAL test = new REAL(1.20f);
            test.Precision = 2;
            Assert.IsTrue(test.ToString().StartsWith("1,20"));
            // Teardown
            EverestFrameworkContext.CurrentCulture = defaultCulture;
        }
    }
}
