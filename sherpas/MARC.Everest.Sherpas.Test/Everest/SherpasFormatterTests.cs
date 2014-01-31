using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.Test.Sherpas.Templates;
using MARC.Everest.Sherpas.Formatter.XML.ITS1;
using System.IO;
using System.Xml;

namespace MARC.Everest.Test.Sherpas
{
    [TestClass]
    public class SherpasFormatterTests
    {
        /// <summary>
        /// This test will ensure that the Sherpas ClinicalDocumentFormatter is formatting a basic template into a CDA 
        /// </summary>
        [TestMethod]
        public void SherpasGraphTemplateBasicLegacyTest()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><component contextConductionInd=\"true\"><structuredBody classCode=\"DOCBODY\" moodCode=\"EVN\"><component typeCode=\"COMP\" contextConductionInd=\"false\"><section moodCode=\"EVN\" classCode=\"DOCSECT\"><templateId root=\"2.16.840.1.113883.3.1937.99.61.3.10.3001\" /><code code=\"18844-1\" codeSystem=\"2.16.840.1.113883.6.1\" /><title language=\"en-CA\">Test Title</title><text representation=\"TXT\" mediaType=\"text/plain\" language=\"en-CA\">Test Body content</text></section></component></structuredBody></component></ClinicalDocument>";

            ClinicalDocument testDocument = new ClinicalDocument();
            testDocument.Component = new Component2();
            testDocument.Component.SetBodyChoice(new StructuredBody());
            testDocument.Component.GetBodyChoiceIfStructuredBody().AddEKGImpressionSection(SectionFactory.CreateEKGImpressionSection("Test Title", "Test Body content"));
            
            MARC.Everest.Sherpas.Formatter.XML.ITS1.ClinicalDocumentFormatter docFormatter = new ClinicalDocumentFormatter();
            docFormatter.Settings = Formatters.XML.ITS1.SettingsType.DefaultLegacy;
            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                docFormatter.Graph(xw, testDocument);
                xw.Flush();
            }
            R2SerializationHelper.XmlIsEquivalent(expected, sw.ToString());
        }

        /// <summary>
        /// This test will ensure that the Sherpas ClinicalDocumentFormatter is parsing a basic template from a CDA 
        /// </summary>
        [TestMethod]
        public void SherpasParseTemplateBasicLegacyTest()
        {
            string xmlData = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><component contextConductionInd=\"true\"><structuredBody classCode=\"DOCBODY\" moodCode=\"EVN\"><component typeCode=\"COMP\" contextConductionInd=\"false\"><section moodCode=\"EVN\" classCode=\"DOCSECT\"><templateId root=\"2.16.840.1.113883.3.1937.99.61.3.10.3001\" /><code code=\"18844-1\" codeSystem=\"2.16.840.1.113883.6.1\" /><title language=\"en-CA\">Test Title</title><text representation=\"TXT\" mediaType=\"text/plain\" language=\"en-CA\">Test Body content</text></section></component></structuredBody></component></ClinicalDocument>";

            ClinicalDocumentFormatter docFormatter = new ClinicalDocumentFormatter();
            docFormatter.Settings = Formatters.XML.ITS1.SettingsType.DefaultLegacy;
            docFormatter.RegisterSimpleCDADocumentTemplates();

            using (XmlReader xr = XmlReader.Create(new StringReader(xmlData)))
            {
                var result = docFormatter.Parse(xr);
                Assert.IsInstanceOfType(result.Structure, typeof(ClinicalDocument));
                Assert.IsInstanceOfType((result.Structure as ClinicalDocument).Component.GetBodyChoiceIfStructuredBody().Component[0].Section, typeof(EKGImpressionSection));
            }
        }

        /// <summary>
        /// This test will ensure that the Sherpas ClinicalDocumentFormatter is formatting a basic template into a CDA 
        /// </summary>
        [TestMethod]
        public void SherpasGraphTemplateBasicReflectTest()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><component contextConductionInd=\"true\"><structuredBody classCode=\"DOCBODY\" moodCode=\"EVN\"><component typeCode=\"COMP\" contextConductionInd=\"false\"><section moodCode=\"EVN\" classCode=\"DOCSECT\"><templateId root=\"2.16.840.1.113883.3.1937.99.61.3.10.3001\" /><code code=\"18844-1\" codeSystem=\"2.16.840.1.113883.6.1\" /><title language=\"en-CA\">Test Title</title><text representation=\"TXT\" mediaType=\"text/plain\" language=\"en-CA\">Test Body content</text></section></component></structuredBody></component></ClinicalDocument>";

            ClinicalDocument testDocument = new ClinicalDocument();
            testDocument.Component = new Component2();
            testDocument.Component.SetBodyChoice(new StructuredBody());
            testDocument.Component.GetBodyChoiceIfStructuredBody().AddEKGImpressionSection(SectionFactory.CreateEKGImpressionSection("Test Title", "Test Body content"));

            MARC.Everest.Sherpas.Formatter.XML.ITS1.ClinicalDocumentFormatter docFormatter = new ClinicalDocumentFormatter();
            docFormatter.Settings = Formatters.XML.ITS1.SettingsType.DefaultUniprocessor;
            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                docFormatter.Graph(xw, testDocument);
                xw.Flush();
            }
            R2SerializationHelper.XmlIsEquivalent(expected, sw.ToString());
        }

        /// <summary>
        /// This test will ensure that the Sherpas ClinicalDocumentFormatter is parsing a basic template from a CDA 
        /// </summary>
        [TestMethod]
        public void SherpasParseTemplateBasicReflectTest()
        {
            string xmlData = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><component contextConductionInd=\"true\"><structuredBody classCode=\"DOCBODY\" moodCode=\"EVN\"><component typeCode=\"COMP\" contextConductionInd=\"false\"><section moodCode=\"EVN\" classCode=\"DOCSECT\"><templateId root=\"2.16.840.1.113883.3.1937.99.61.3.10.3001\" /><code code=\"18844-1\" codeSystem=\"2.16.840.1.113883.6.1\" /><title language=\"en-CA\">Test Title</title><text representation=\"TXT\" mediaType=\"text/plain\" language=\"en-CA\">Test Body content</text></section></component></structuredBody></component></ClinicalDocument>";

            ClinicalDocumentFormatter docFormatter = new ClinicalDocumentFormatter();
            docFormatter.Settings = Formatters.XML.ITS1.SettingsType.DefaultUniprocessor;
            docFormatter.RegisterSimpleCDADocumentTemplates();

            using (XmlReader xr = XmlReader.Create(new StringReader(xmlData)))
            {
                var result = docFormatter.Parse(xr);
                Assert.IsInstanceOfType(result.Structure, typeof(ClinicalDocument));
                Assert.IsInstanceOfType((result.Structure as ClinicalDocument).Component.GetBodyChoiceIfStructuredBody().Component[0].Section, typeof(EKGImpressionSection));
            }
        }

    }
}
