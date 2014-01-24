using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.Attributes;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;

using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.ITS1;
using System.IO;
using System.Xml;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.Regressions
{
    /// <summary>
    /// Regression test for EV1102
    /// </summary>
    [TestClass]
    public class EV_1102
    {


        [Structure(Model = "POCD_MT000040", Name = "Observation", StructureType = StructureAttribute.StructureAttributeType.MessageType)]
        public class ObservationWithConfidentialityCode : Observation
        {
            [Property(Name = "confidentialityCode", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural)]
            public CE<x_BasicConfidentialityKind> ConfidentialityCode { get; set; }
        }

        [TestMethod]
        public void EV_1102_DefaultSerializationDefaultLegacy()
        {

            ClinicalDocument document = new ClinicalDocument();
            document.Component = new Component2();
            document.Component.SetBodyChoice(new StructuredBody());
            document.Component.GetBodyChoiceIfStructuredBody().Component.Add(new Component3());
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section = new Section();
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section.Entry.Add(new Entry());
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section.Entry[0].SetClinicalStatement(
                new ObservationWithConfidentialityCode()
                {
                    ConfidentialityCode = x_BasicConfidentialityKind.Restricted
                }
            );

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;
            formatter.Settings = SettingsType.DefaultLegacy;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var result = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                Assert.IsNotNull(result.Details.FirstOrDefault(o=>o is NotSupportedChoiceResultDetail));
            }
        }

        [TestMethod]
        public void EV_1102_AlwaysCheckForOverridesReflection()
        {

            ClinicalDocument document = new ClinicalDocument();
            document.Component = new Component2();
            document.Component.SetBodyChoice(new StructuredBody());
            document.Component.GetBodyChoiceIfStructuredBody().Component.Add(new Component3());
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section = new Section();
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section.Entry.Add(new Entry());
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section.Entry[0].SetClinicalStatement(
                new ObservationWithConfidentialityCode()
                {
                    ConfidentialityCode = x_BasicConfidentialityKind.Restricted
                }
            );

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;
            formatter.Settings = SettingsType.DefaultUniprocessor;
            formatter.RegisterXSITypeName("POCD_MT000040.Observation", typeof(ObservationWithConfidentialityCode));
            formatter.Settings |= SettingsType.AlwaysCheckForOverrides;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var result = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                Assert.IsNull(result.Details.FirstOrDefault(o => o is NotSupportedChoiceResultDetail));
            }
        }

        [TestMethod]
        public void EV_1102_AlwaysCheckForOverridesLegacy()
        {

            ClinicalDocument document = new ClinicalDocument();
            document.Component = new Component2();
            document.Component.SetBodyChoice(new StructuredBody());
            document.Component.GetBodyChoiceIfStructuredBody().Component.Add(new Component3());
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section = new Section();
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section.Entry.Add(new Entry());
            document.Component.GetBodyChoiceIfStructuredBody().Component[0].Section.Entry[0].SetClinicalStatement(
                new ObservationWithConfidentialityCode()
                {
                    ConfidentialityCode = x_BasicConfidentialityKind.Restricted
                }
            );

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;
            formatter.Settings = SettingsType.DefaultLegacy;
            formatter.RegisterXSITypeName("POCD_MT000040.Observation", typeof(ObservationWithConfidentialityCode));
            formatter.Settings |= SettingsType.AlwaysCheckForOverrides;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var result = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                Assert.IsNull(result.Details.FirstOrDefault(o => o is NotSupportedChoiceResultDetail));
            }
        }
    }
}
