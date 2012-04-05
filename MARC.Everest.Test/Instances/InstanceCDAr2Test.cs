using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MARC.Everest.Connectors;
using MARC.Everest.RMIM.CA.R020401.Interactions;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace MARC.Everest.Test
{
    [TestClass]
    public class InstanceCDAr2Test
    {

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



        [TestMethod]
        public void InstanceCDAr2Test_FMTR_MARCEverestRMIMUVCDAr2POCD_MT000040UVClinicalDocument()
        {


            MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument original = TypeCreator.GetCreator(typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument)).CreateInstance() as MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument;

            // New ms
            MemoryStream ms = new MemoryStream();

            // Format
            MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter fmtr = new MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter();
            fmtr.GraphAides.Add(new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture });
            fmtr.ValidateConformance = false;
            var graphResult = fmtr.Graph(ms, original);

            Assert.IsTrue(graphResult.Code == MARC.Everest.Connectors.ResultCode.Accepted || graphResult.Code == MARC.Everest.Connectors.ResultCode.AcceptedNonConformant);

            // Seek back to begin
            ms.Seek(0, SeekOrigin.Begin);

            // Parse
            MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument parsed = (MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument)fmtr.Parse(ms, original.GetType().Assembly).Structure;

            // Assert
            Assert.AreEqual(original.ClassCode, parsed.ClassCode);
            Assert.AreEqual(original.MoodCode, parsed.MoodCode);
            Assert.AreEqual(original.Id, parsed.Id);
            Assert.AreEqual(original.Code, parsed.Code);
            Assert.AreEqual(original.Title, parsed.Title);
            Assert.AreEqual(original.EffectiveTime, parsed.EffectiveTime);
            Assert.AreEqual(original.ConfidentialityCode, parsed.ConfidentialityCode);
            Assert.AreEqual(original.LanguageCode, parsed.LanguageCode);
            Assert.AreEqual(original.SetId, parsed.SetId);
            Assert.AreEqual(original.VersionNumber, parsed.VersionNumber);
            Assert.AreEqual(original.CopyTime, parsed.CopyTime);
            Assert.AreEqual(original.DataEnterer, parsed.DataEnterer);
            Assert.AreEqual(original.Custodian, parsed.Custodian);
            Assert.AreEqual(original.LegalAuthenticator, parsed.LegalAuthenticator);
            Assert.AreEqual(original.Component, parsed.Component);
            Assert.AreEqual(original.ComponentOf, parsed.ComponentOf);
            Assert.AreEqual(original.NullFlavor, parsed.NullFlavor);
            Assert.AreEqual(original.RealmCode, parsed.RealmCode);
            Assert.AreEqual(original.TypeId, parsed.TypeId);
            Assert.AreEqual(original.TemplateId, parsed.TemplateId);

        }



    }
}
