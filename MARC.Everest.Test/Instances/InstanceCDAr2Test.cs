using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MARC.Everest.Connectors;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using System.Diagnostics;
using MARC.Everest.Formatters.XML.ITS1;

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
        public void InstanceCDAR2Test_XSD_ClinicalDocument()
        {
            MemoryStream stream = new MemoryStream();

            try
            {
                IResultDetail[] details = null;
                TypeCreator tc = TypeCreator.GetCreator(typeof(ClinicalDocument));
                tc.GenerateOptional = true;
                MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument original = tc.CreateInstance() as MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument;
                original.ComponentOf = TypeCreator.GetCreator(typeof(Component1)).CreateInstance() as Component1;
                original.Component = new Component2();
                original.Component.SetBodyChoice(TypeCreator.GetCreator(typeof(StructuredBody)).CreateInstance() as StructuredBody);
                XmlIts1Formatter fmtr = new XmlIts1Formatter();
                fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
                fmtr.Graph(stream, original);
                stream.Seek(0, SeekOrigin.Begin);
                XMLGenerator.GenerateInstance(typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument), stream, out details);


                if (details.Length > 0)
                    foreach (var item in details)
                        if (item.Type == ResultDetailType.Error)
                            Tracer.Trace(item.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }

            stream.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            stream.Flush();

            string xml = new StreamReader(stream).ReadToEnd();

            stream.Seek(0, SeekOrigin.Begin);
            stream.Flush();

            var result = XMLValidator.Validate("ClinicalDocument", stream, typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument));

            if (result.Count > 0)
            {
                result.ForEach(item => Trace.WriteLine(item));
                Assert.Fail("Validation failed");
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
