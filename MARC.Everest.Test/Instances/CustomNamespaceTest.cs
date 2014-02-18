using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using MARC.Everest.Attributes;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Interfaces;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MARC.Everest.Test.Instances
{
    [TestClass]
    public class CustomNamespaceTest
    {
        private const string CustomNamespace = "urn:custom";

        private static XmlIts1Formatter CreateStructureFormater()
        {
            var structureFormatter = new XmlIts1Formatter() { ValidateConformance = false };
            structureFormatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            structureFormatter.Settings  |= SettingsType.AlwaysCheckForOverrides;
            structureFormatter.ValidateConformance = false;
            structureFormatter.GraphAides.Add(new DatatypeFormatter() { ValidateConformance = false });
            structureFormatter.RegisterXSITypeName("custom:RecordTarget", typeof(RecordTarget));
            structureFormatter.RegisterXSITypeName("custom:ClinicalDocument", typeof(ClinicalDocument));
            structureFormatter.RegisterXSITypeName("custom:StructuredBody", typeof(StructuredBody));
            return structureFormatter;
        }

        private static XmlDocument Serialize(IGraphable graph)
        {
            using (var ms = new MemoryStream())
            using (var xmlWriter = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true }))
            using (var xsw = new XmlStateWriter(xmlWriter))
            {
                var structureFormatter = CreateStructureFormater();
                xsw.WriteStartElement("ClinicalDocument", "urn:hl7-org:v3");
                xsw.WriteAttributeString("xmlns", "xsi", null, XmlIts1Formatter.NS_XSI);
                xsw.WriteAttributeString("xmlns", null, null, "urn:hl7-org:v3");
                xsw.WriteAttributeString("xmlns", "custom", null, CustomNamespace);
                xsw.WriteAttributeString("xsi", "type", XmlIts1Formatter.NS_XSI, "custom:ClinicalDocument");

                structureFormatter.Graph(xsw, graph);
                xsw.Close();
                ms.Seek(0, SeekOrigin.Begin);
                Debug.WriteLine(Encoding.UTF8.GetString(ms.ToArray()));
                using (var reader = new XmlTextReader(ms))
                {
                    var result = new XmlDocument();
                    result.Load(reader);
                    return result;
                }
            }
        }

        internal static void XmlIsEquivalentAndDeserializable(string expectedXml, IGraphable actualXml)
        {
            XmlDocument e = new XmlDocument(),
                a = Serialize(actualXml);
            e.LoadXml(expectedXml);
            R2SerializationHelper.XmlIsEquivalent(e.DocumentElement, a.DocumentElement);
            R2SerializationHelper.XmlIsEquivalent(a.DocumentElement, e.DocumentElement);

            using (var xw = new XmlNodeReader(a))
            {
                var structureFormatter = CreateStructureFormater();
                
                var document = structureFormatter.Parse(xw, typeof(RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument));
                Assert.AreEqual(ResultCode.Accepted, document.Code);
            }
        }

        [TestMethod]
        public void SimpleCustomInstance()
        {
            string expectedXml = @"<ClinicalDocument xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""custom:ClinicalDocument"" classCode=""DOCCLIN"" moodCode=""EVN"" xmlns=""urn:hl7-org:v3"" xmlns:custom=""urn:custom""><recordTarget xsi:type=""custom:RecordTarget"" typeCode=""RCT"" contextControlCode=""OP""><custom:element value=""true"" /></recordTarget></ClinicalDocument>";

            var document = new RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument
            {
                RecordTarget = new List<RMIM.UV.CDAr2.POCD_MT000040UV.RecordTarget>
                {
                    new RecordTarget
                    {
                          Element = true,
                    }
                }
            };

            XmlIsEquivalentAndDeserializable(expectedXml, document);
        }

        [TestMethod]
        public void ClinicalDocumentInheritance()
        {
            string expectedXml = @"<ClinicalDocument xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""custom:ClinicalDocument"" classCode=""DOCCLIN"" moodCode=""EVN"" xmlns=""urn:hl7-org:v3"" xmlns:custom=""urn:custom""><custom:element value=""true"" /></ClinicalDocument>";

            var document = new ClinicalDocument
            {
                Element = true,
            };

            XmlIsEquivalentAndDeserializable(expectedXml, document);
        }

        [TestMethod]
        public void ChoiceInheritance()
        {
            string expectedXml = @"<ClinicalDocument xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""custom:ClinicalDocument"" classCode=""DOCCLIN"" moodCode=""EVN"" xmlns=""urn:hl7-org:v3"" xmlns:custom=""urn:custom""><component contextConductionInd=""true""><structuredBody xsi:type=""custom:StructuredBody""  classCode=""DOCBODY"" moodCode=""EVN""><custom:element value=""true""/></structuredBody></component></ClinicalDocument>";

            var document = new RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument
            {
                Component = new Component2
                {
                    BodyChoice = new StructuredBody
                    {
                        Element = true,
                    }
                }
            };

            XmlIsEquivalentAndDeserializable(expectedXml, document);
        }

        [TestMethod]
        public void ClinicalDocumentInheritanceWithCustomTypeTest()
        {
            string expectedXml = @"<ClinicalDocument xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""custom:ClinicalDocument"" classCode=""DOCCLIN"" moodCode=""EVN"" xmlns=""urn:hl7-org:v3"" xmlns:custom=""urn:custom""><custom:additionalInformation><custom:element value=""true"" /></custom:additionalInformation></ClinicalDocument>";

            var document = new ClinicalDocument
            {
                AdditionalInformation = new AdditionalInformation
                {
                    Element = true,
                }
            };

            XmlIsEquivalentAndDeserializable(expectedXml, document);
        }

        [Structure(Name = "Patient", NamespaceUri = CustomNamespace)]
        [Serializable]
        public class RecordTarget : RMIM.UV.CDAr2.POCD_MT000040UV.RecordTarget
        {
            [Property(Name = "element", NamespaceUri = CustomNamespace, Conformance = PropertyAttribute.AttributeConformanceType.Optional, DefaultUpdateMode = UpdateMode.Unknown, PropertyType = PropertyAttribute.AttributeAttributeType.Traversable)]
            public BL Element
            {
                get;
                set;
            }
        }

        [Structure(Name = "StructuredBody", NamespaceUri = CustomNamespace)]
        [Serializable]
        public class StructuredBody : RMIM.UV.CDAr2.POCD_MT000040UV.StructuredBody
        {
            [Property(Name = "element", NamespaceUri = CustomNamespace, Conformance = PropertyAttribute.AttributeConformanceType.Optional, DefaultUpdateMode = UpdateMode.Unknown, PropertyType = PropertyAttribute.AttributeAttributeType.Traversable)]
            public BL Element
            {
                get;
                set;
            }
        }

        [Structure(Name = "AdditionalInformation", NamespaceUri = CustomNamespace)]
        [Serializable]
        public class AdditionalInformation : IGraphable
        {
            [Property(Name = "element", NamespaceUri = CustomNamespace, Conformance = PropertyAttribute.AttributeConformanceType.Optional, DefaultUpdateMode = UpdateMode.Unknown, PropertyType = PropertyAttribute.AttributeAttributeType.Traversable)]
            public BL Element
            {
                get;
                set;
            }
        }

        [Structure(Name = "ClinicalDocument", NamespaceUri = CustomNamespace, IsEntryPoint = true)]
        [Serializable]
        public class ClinicalDocument : RMIM.UV.CDAr2.POCD_MT000040UV.ClinicalDocument
        {
            [Property(Name = "element", NamespaceUri = CustomNamespace, Conformance = PropertyAttribute.AttributeConformanceType.Optional, DefaultUpdateMode = UpdateMode.Unknown, PropertyType = PropertyAttribute.AttributeAttributeType.Traversable)]
            public BL Element
            {
                get;
                set;
            }

            [Property(Name = "additionalInformation", NamespaceUri = CustomNamespace, Conformance = PropertyAttribute.AttributeConformanceType.Optional, DefaultUpdateMode = UpdateMode.Unknown, PropertyType = PropertyAttribute.AttributeAttributeType.Traversable)]
            public AdditionalInformation AdditionalInformation
            {
                get;
                set;
            }
        }
    }
}