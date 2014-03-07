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
 * Date: 5-3-2014
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.Interfaces;
using MARC.Everest.DataTypes;
using System.Reflection;
using MARC.Everest.DataTypes.Interfaces;
using System.Collections;
using MARC.Everest.Attributes;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using System.IO;
using System.Xml;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Xml;
using MARC.Everest.Connectors;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MARC.Everest.Test.Regressions
{
    [TestClass]
    public class EV_1110
    {
        /// <summary>
        /// Cascade a null flavor
        /// </summary>
        static void CascadeNullFlavor(IImplementsNullFlavor me, NullFlavor flavor)
        {
            me.NullFlavor = flavor;
            foreach (var propertyInfo in me.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {

                Type propertyType = propertyInfo.PropertyType;
                if (typeof(IList).IsAssignableFrom(propertyType))  // correct for lists
                    propertyType = propertyType.GetGenericArguments()[0];

                // Only IImpleentsNullFlavor should be allowed
                if (!typeof(IImplementsNullFlavor).IsAssignableFrom(propertyType))
                    continue;

                var instance = propertyInfo.GetValue(me, null) as IImplementsNullFlavor;

                // Make some intelligent decisions about populating
                var propertyAtt = propertyInfo.GetCustomAttributes(typeof(PropertyAttribute), true);
                if (propertyAtt.Length > 0)
                {
                    var strongPropertyAtt = propertyAtt[0] as PropertyAttribute;
                    // Don't set a null flavor on structural (attributes)
                    if (strongPropertyAtt.PropertyType == PropertyAttribute.AttributeAttributeType.Structural)
                        continue;
                    if (strongPropertyAtt.Conformance == PropertyAttribute.AttributeConformanceType.Optional && instance == null)
                        continue;

                }

                // is this a list or IImplementsNullFlavor
                if (instance == null)
                {
                    var constructorInfo = propertyType.GetConstructor(Type.EmptyTypes);
                    if (constructorInfo == null) continue; // sanity check, can't create

                    // Construct an instance
                    instance = constructorInfo.Invoke(null) as IImplementsNullFlavor;
                }

                // Don't cascade down data types
                if (instance is IAny)
                    instance.NullFlavor = flavor;
                else
                    CascadeNullFlavor(instance, flavor);

                // Set if not a list
                if (propertyInfo.PropertyType == propertyType)
                    propertyInfo.SetValue(me, instance, null);
                else // Add to list if not
                    (propertyInfo.GetValue(me, null) as IList).Add(instance);

            }
        }

        [Structure(Model = "POCD_MT000040", Name = "Observation", StructureType = StructureAttribute.StructureAttributeType.MessageType)]
        public class ObservationMyProfile : Observation
        {
            [Property(Name = "value", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Mandatory)]
            public override ANY Value { get; set; }
        }

        /// <summary>
        /// Default legacy - inconsistent behavior reported on entries
        /// </summary>
        [TestMethod]
        public void EV_1110_DefaultUniprocessorCustomTypeTest()
        {

            // Load a sample because I don't want to write a full construction method
            ClinicalDocument clinicalDocument = null;
            using (Stream inStream = typeof(EV_1110).Assembly.GetManifestResourceStream("MARC.Everest.Test.Resources.phrDocTesting_140219112155-0500.xml"))
            {
                XmlIts1Formatter fmtr = new XmlIts1Formatter();
                fmtr.ValidateConformance = false;
                fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
                var result = fmtr.Parse(XmlReader.Create(inStream), typeof(ClinicalDocument));
                clinicalDocument = result.Structure as ClinicalDocument;
            }

            var observation = new ObservationMyProfile()
                {
                    MoodCode = x_ActMoodDocumentObservation.Eventoccurrence,
                    Code = "3202-20",
                    EntryRelationship = new List<EntryRelationship>()
                    {
                        new EntryRelationship(x_ActRelationshipEntryRelationship.HasComponent, true)
                        {
                            ClinicalStatement = new ObservationMyProfile()
                        }
                    }
                }
            ;


            CascadeNullFlavor(observation.EntryRelationship[0], NullFlavor.Unknown);

            // Cascade a null flavor on one of the entries
            clinicalDocument.Component.GetBodyChoiceIfStructuredBody().Component[1].Section.Entry.Add(new Entry(
                x_ActRelationshipEntry.HasComponent,
                false,
                observation
            ));

            // Cascade a null flavor on one of the entries
            clinicalDocument.Component.GetBodyChoiceIfStructuredBody().Component[2].Section.Entry.Add(new Entry(
                x_ActRelationshipEntry.HasComponent,
                false,
                observation
            ));

            // Cascade a null flavor on one of the entries
            clinicalDocument.Component.GetBodyChoiceIfStructuredBody().Component[3].Section.Entry.Add(new Entry(
                x_ActRelationshipEntry.HasComponent,
                false,
                observation
            ));

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true }))
            {
                XmlIts1Formatter fmtr = new XmlIts1Formatter();
                fmtr.ValidateConformance = false;
                fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
                fmtr.Settings = SettingsType.DefaultUniprocessor;
                fmtr.Settings |= SettingsType.SuppressXsiNil;
                fmtr.Settings |= SettingsType.SuppressNullEnforcement;
                fmtr.Settings |= SettingsType.AlwaysCheckForOverrides;
                fmtr.RegisterXSITypeName("POCD_MT000040.Observation", typeof(MARC.Everest.Test.Regressions.EV_1102.ObservationWithConfidentialityCode));

                using (XmlStateWriter xsw = new XmlStateWriter(xw))
                {
                    xsw.WriteStartElement("hl7", "ClinicalDocument", "urn:hl7-org:v3");
                    xsw.WriteAttributeString("xmlns", "xsi", null, XmlIts1Formatter.NS_XSI);
                    xsw.WriteAttributeString("schemaLocation", XmlIts1Formatter.NS_XSI, @"urn:hl7-org:v3 Schemas/CDA-PITO-E2E.xsd");
                    xsw.WriteAttributeString("xmlns", null, null, @"urn:hl7-org:v3");
                    xsw.WriteAttributeString("xmlns", "hl7", null, @"urn:hl7-org:v3");
                    xsw.WriteAttributeString("xmlns", "e2e", null, @"http://standards.pito.bc.ca/E2E-DTC/cda");
                    xsw.WriteAttributeString("xmlns", "xs", null, @"http://www.w3.org/2001/XMLSchema");

                    
                    IFormatterGraphResult result = fmtr.Graph(xsw, clinicalDocument);
                    foreach (ResultDetail itm in result.Details)
                        Trace.WriteLine(String.Format("{0}:{1} @ {2}", itm.Type, itm.Message, itm.Location));
                    xsw.WriteEndElement(); // clinical document
                    xsw.Flush();
                }
            }

            Trace.WriteLine(sw.ToString());
            Regex re = new Regex(@"\<entryRelationship.*/\>");
            if (re.IsMatch(sw.ToString()))
                Assert.Fail("Output of entry relationship is not as expected");

        }
    }
}
