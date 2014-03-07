using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.Regressions
{
    [TestClass]
    public class EV_1111
    {
        /// <summary>
        /// Regression test for EV1111
        /// </summary>
        [TestMethod]
        public void GTSTestXsiOutputIVL_TS_Test()
        {
            // Constrct a GTS
            GTS test = new IVL<TS>(DateTime.Today, DateTime.Now);
            String xml = R1SerializationHelper.SerializeAsString(test, Formatters.XML.Datatypes.R1.DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture);
            Assert.IsTrue(xml.Contains("xsi:type=\"IVL_TS\""));
        }

        /// <summary>
        /// Regression test for EV1111
        /// </summary>
        [TestMethod]
        public void GTSTestXsiOutputPIVL_TS_Test()
        {
            // Constrct a GTS
            GTS test = new PIVL<TS>(new IVL<TS>(DateTime.Today, DateTime.Now), new PQ(1, "d"));
            String xml = R1SerializationHelper.SerializeAsString(test, Formatters.XML.Datatypes.R1.DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture);
            Assert.IsTrue(xml.Contains("xsi:type=\"PIVL_TS\""));
        }

        /// <summary>
        /// Regression test for EV1111
        /// </summary>
        [TestMethod]
        public void GTSTestXsiOutputEIVL_TS_Test()
        {
            // Constrct a GTS
            GTS test = new EIVL<TS>(DomainTimingEventType.AfterBreakfast, null);
            String xml = R1SerializationHelper.SerializeAsString(test, Formatters.XML.Datatypes.R1.DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture);
            Assert.IsTrue(xml.Contains("xsi:type=\"EIVL_TS\""));
        }

        /// <summary>
        /// Regression test for EV1111
        /// </summary>
        [TestMethod]
        public void GTSTestXsiOutputSXPR_TS_Test()
        {
            // Constrct a GTS
            GTS test = SXPR<TS>.CreateSXPR(
                new IVL<TS>(DateTime.Now),
                new PIVL<TS>(new IVL<TS>(DateTime.Now), new PQ(1, "d"))
            );
            String xml = R1SerializationHelper.SerializeAsString(test, Formatters.XML.Datatypes.R1.DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture);
            Assert.IsTrue(xml.Contains("xsi:type=\"SXPR_TS\""));
            Assert.IsTrue(xml.Contains("xsi:type=\"IVL_TS\""));
            Assert.IsTrue(xml.Contains("xsi:type=\"PIVL_TS\""));
        }


        /// <summary>
        /// Regression test for EV1111
        /// </summary>
        [TestMethod]
        public void GTSTestXsiOutputSXCM_TS_Test()
        {
            // Constrct a GTS
            GTS test = new SXCM<TS>(DateTime.Now);
            String xml = R1SerializationHelper.SerializeAsString(test, Formatters.XML.Datatypes.R1.DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture);
            Assert.IsFalse(xml.Contains("xsi:type=\"SXCM_TS\""));
        }

    
    }
}
