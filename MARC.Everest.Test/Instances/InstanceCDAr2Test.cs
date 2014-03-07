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
using System.Xml;

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
        public void InstanceCDAR2Test_Order_SubstanceAdministration()
        {
            MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument original = TypeCreator.GetCreator(typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument)).CreateInstance() as MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument;
            original.Component = new Component2();
            original.Component.SetBodyChoice(new StructuredBody());
            
            original.Component.GetBodyChoiceIfStructuredBody().Component.Add(new Component3()
            {
                Section = new Section()
                {
                    Entry = new List<Entry>() {
                        new Entry() {
                            ClinicalStatement = new SubstanceAdministration() {
                                Consumable = new Consumable(),
                                EntryRelationship = new List<EntryRelationship>()
                                {
                                    new EntryRelationship()
                                }
                            }
                        }
                    }
                }
            });

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
            String xmlString = System.Text.Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            Assert.IsTrue(xmlString.IndexOf("<entryRelationship") > xmlString.IndexOf("<consumable"), "entryRelationship must appear after consumable");

        }

        [TestMethod]
        public void InstanceCDAR2Test_XSD_ClinicalDocument()
        {
            MemoryStream stream = new MemoryStream();

            try
            {
                IFormatterGraphResult gresult;
                TypeCreator tc = TypeCreator.GetCreator(typeof(ClinicalDocument));
                tc.GenerateOptional = true;
                MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument original = tc.CreateInstance() as MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument;
                original.TypeId = new Everest.DataTypes.II("2.16.840.1.113883.1.3", "POCD_HD000040");
                original.ComponentOf = TypeCreator.GetCreator(typeof(Component1)).CreateInstance() as Component1;
                original.Component = new Component2();
                original.Component.SetBodyChoice(TypeCreator.GetCreator(typeof(StructuredBody)).CreateInstance() as StructuredBody);
                XmlIts1Formatter fmtr = new XmlIts1Formatter();
                fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
                using (XmlWriter xw = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
                    gresult = fmtr.Graph(xw, original);
                stream.Seek(0, SeekOrigin.Begin);
                //XMLGenerator.GenerateInstance(typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument), stream, out details);


                if (gresult.Details.Count() > 0)
                    foreach (var item in gresult.Details)
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
        public void InstanceCDAr2Test_DefaultUniProcessor_MARCEverestRMIMUVCDAr2POCD_MT000040UVClinicalDocument()
        {


            MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument original = TypeCreator.GetCreator(typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument)).CreateInstance() as MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument;

            // New ms
            MemoryStream ms = new MemoryStream();

            // Format
            MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter fmtr = new MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter();
            fmtr.GraphAides.Add(new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture });
            fmtr.ValidateConformance = false;
            fmtr.Settings = SettingsType.DefaultUniprocessor;
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

        [TestMethod]
        public void InstanceCDAr2Test_DefaultLegacy_MARCEverestRMIMUVCDAr2POCD_MT000040UVClinicalDocument()
        {


            MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument original = TypeCreator.GetCreator(typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument)).CreateInstance() as MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument;

            // New ms
            MemoryStream ms = new MemoryStream();

            // Format
            MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter fmtr = new MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter();
            fmtr.GraphAides.Add(new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture });
            fmtr.ValidateConformance = false;
            fmtr.Settings = SettingsType.DefaultLegacy;
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
