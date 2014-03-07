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
 * Date: 16-1-2014
 */
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
            formatter.RegisterXSITypeName("POCD_MT000040UV.Observation", typeof(ObservationWithConfidentialityCode));
            formatter.Settings |= SettingsType.AlwaysCheckForOverrides;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var result = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                Assert.IsFalse(data.Contains("xsi:type"), "xsi:type is present and shouldn't be");
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
            formatter.RegisterXSITypeName("POCD_MT000040UV.Observation", typeof(ObservationWithConfidentialityCode));
            formatter.Settings |= SettingsType.AlwaysCheckForOverrides;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var result = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                Assert.IsFalse(data.Contains("xsi:type"), "xsi:type is present and shouldn't be");
                Assert.IsNull(result.Details.FirstOrDefault(o => o is NotSupportedChoiceResultDetail));
            }
        }
    }
}
