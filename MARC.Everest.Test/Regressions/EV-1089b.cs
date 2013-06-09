using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using System.IO;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Xml;
using System.Xml;
using MARC.Everest.RMIM.UV.NE2010.Interactions;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Attributes;
using MARC.Everest.Interfaces;
using System.ComponentModel;
using System.Drawing.Design;
using MARC.Everest.Design;
using MARC.Everest.Test.Manual.Formatters;

namespace MARC.Everest.Test.Regressions
{
    /// <summary>
    /// From Frederic when reporting EV-1089
    /// </summary>
    [Serializable
   , Structure(Name = "Component4", StructureType = StructureAttribute.StructureAttributeType.MessageType, IsEntryPoint = false, Model = "REPC_MT000400NL")
   , Realization(Name = "ActRelationship")]
    public class Component4 : MARC.Everest.RMIM.UV.NE2010.REPC_MT000400UV01.Component4, IGraphable
    {
        public Component4()
        {
        }

        [Editor(typeof(NewInstanceTypeEditor)
            , typeof(UITypeEditor))
            , TypeConverter(typeof(ExpandableObjectConverter))
            , Property(Name = "careStatement", PropertyType = PropertyAttribute.AttributeAttributeType.Traversable, Conformance = PropertyAttribute.AttributeConformanceType.Optional, SortKey = 1)
            , Realization(Name = "target")
            , Category("Optional")]
        public new MARC.Everest.RMIM.UV.NE2010.REPC_MT000100UV01.Observation CareStatement { get; set; }
    }


    [TestClass]
    public class EV_1089b
    {

        /// <summary>
        /// Sample (from bug reporter) test.
        /// </summary>
        [TestMethod]
        public void EV_1089SampleReflectParseTest()
        {
            try
            {
                var instance = ParseFromStreamTest.FindResource("REPC_IN002120UV01.xml");
                // Parse 
                XmlIts1Formatter fmtr = new XmlIts1Formatter()
                {
                    GraphAides = new List<Connectors.IStructureFormatter>() { 
                    new DatatypeFormatter()
                },
                    Settings = SettingsType.DefaultUniprocessor
                };

                XmlStateReader reader = new XmlStateReader(XmlReader.Create(ParseFromStreamTest.GetResourceStream(instance)));
                var result = fmtr.Parse(reader, typeof(PRPA_IN201301UV02).Assembly);
                Assert.IsNotNull(((((result.Structure as REPC_IN002120UV01).controlActProcess.Subject[0].act.PertinentInformation3[3].CareStatement as RMIM.UV.NE2010.REPC_MT000100UV01.Observation).TargetOf[1].CareEntry as MARC.Everest.RMIM.UV.NE2010.REPC_MT000100UV01.SubstanceAdministration).Consumable.ConsumableChoice as RMIM.UV.NE2010.COCT_MT230100UV.Medication).AdministerableMedicine.Name);
            }
            catch { }
        }


        /// <summary>
        /// Sample (from bug reporter) test.
        /// </summary>
        [TestMethod]
        public void EV_1089SampleLegacyParseTest()
        {
            try
            {
                var instance = ParseFromStreamTest.FindResource("REPC_IN002120UV01.xml");
                // Parse 
                XmlIts1Formatter fmtr = new XmlIts1Formatter()
                {
                    GraphAides = new List<Connectors.IStructureFormatter>() { 
                    new DatatypeFormatter()
                },
                    Settings = SettingsType.DefaultLegacy
                };

                XmlStateReader reader = new XmlStateReader(XmlReader.Create(ParseFromStreamTest.GetResourceStream(instance)));
                var result = fmtr.Parse(reader, typeof(PRPA_IN201301UV02).Assembly);
                Assert.IsNotNull(((((result.Structure as REPC_IN002120UV01).controlActProcess.Subject[0].act.PertinentInformation3[3].CareStatement as RMIM.UV.NE2010.REPC_MT000100UV01.Observation).TargetOf[1].CareEntry as MARC.Everest.RMIM.UV.NE2010.REPC_MT000100UV01.SubstanceAdministration).Consumable.ConsumableChoice as RMIM.UV.NE2010.COCT_MT230100UV.Medication).AdministerableMedicine.Name);
            }
            catch { }
        }

        /// <summary>
        /// Sample (from bug reporter) test.
        /// </summary>
        [TestMethod]
        public void EV_1089SampleParseTest()
        {
            try
            {
                var instance = ParseFromStreamTest.FindResource("REPC_IN002120UV01.xml");
                // Parse 
                XmlIts1Formatter fmtr = new XmlIts1Formatter()
                {
                    GraphAides = new List<Connectors.IStructureFormatter>() { 
                    new DatatypeFormatter()
                }
                };

                XmlStateReader reader = new XmlStateReader(XmlReader.Create(ParseFromStreamTest.GetResourceStream(instance)));
                var result = fmtr.Parse(reader, typeof(PRPA_IN201301UV02).Assembly);
                Assert.IsNotNull(((((result.Structure as REPC_IN002120UV01).controlActProcess.Subject[0].act.PertinentInformation3[3].CareStatement as RMIM.UV.NE2010.REPC_MT000100UV01.Observation).TargetOf[1].CareEntry as MARC.Everest.RMIM.UV.NE2010.REPC_MT000100UV01.SubstanceAdministration).Consumable.ConsumableChoice as RMIM.UV.NE2010.COCT_MT230100UV.Medication).AdministerableMedicine.Name);
            }
            catch { }
        }

        /// <summary>
        /// Test parsing of SET_T
        /// </summary>
        [TestMethod]
        public void EV_1089SETParseTest()
        {
            PRPA_IN201305UV02 test = new PRPA_IN201305UV02();
            test.Sender = new RMIM.UV.NE2010.MCCI_MT100200UV01.Sender();
            test.Sender.Device = new RMIM.UV.NE2010.MCCI_MT100200UV01.Device();
            test.Sender.Device.Id = new SET<II>() { NullFlavor = NullFlavor.NoInformation };

            XmlIts1Formatter fmtr = new XmlIts1Formatter();
            fmtr.GraphAides.Add(new DatatypeFormatter());

            var sw = new StringWriter();
            XmlStateWriter writer = new XmlStateWriter(XmlWriter.Create(sw));
            fmtr.Graph(writer, test);
            writer.Flush();
            String xmlString = sw.ToString();

            StringReader sr = new StringReader(xmlString);
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            var result = fmtr.Parse(rdr);
            Assert.IsNotNull(result.Structure as PRPA_IN201305UV02);
            Assert.IsNotNull((result.Structure as PRPA_IN201305UV02).Sender.Device.Id);
            //Assert.AreEqual(test, result.Structure);
        }



    }
}
