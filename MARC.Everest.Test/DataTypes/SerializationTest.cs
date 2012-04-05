using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Xml;
using System.Xml;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace MARC.Everest.Test
{
    [TestClass]
    public class SerializationTestClassEqualityTest
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


        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Probability</term><description>0.0f</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void UVPEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.UVP<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.UVP<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Probability = (decimal)0.0f;
            aValue.Value = new MARC.Everest.DataTypes.INT(0);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.UVP<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.UVP<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }

        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Expression</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>ExpressionLanguage</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term></term><description>structureAttributet;System.Int32>>(0)</description></item>
        /// <item><term>UncertaintyType</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
        /// <item><term>Value</term><description>0</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void INTEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.INT aValue = new MARC.Everest.DataTypes.INT(), bValue = null;
            aValue.Value = 0;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.INT>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.INT)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Code</term><description>new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
        /// <item><term>CodeSystem</term><description>"0"</description></item>
        /// <item><term>CodeSystemName</term><description>"0"</description></item>
        /// <item><term>CodeSystemVersion</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void CSEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>DisplayName</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Group</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CDGroup>(0) { new MARC.Everest.DataTypes.CDGroup(new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;System.String>>(0) { new MARC.Everest.DataTypes.CR&lt;System.String>(new MARC.Everest.DataTypes.CV&lt;System.String>("0"),new MARC.Everest.DataTypes.CD&lt;System.String>("0")) }) }</description></item>
        /// <item><term>CodingRationale</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
        /// <item><term>Code</term><description>new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
        /// <item><term>CodeSystem</term><description>"0"</description></item>
        /// <item><term>CodeSystemName</term><description>"0"</description></item>
        /// <item><term>CodeSystemVersion</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void CVEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.DisplayName = "0";
            aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 }, "0");
            aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
            aValue.CodeSystem = "0";
            aValue.CodeSystemName = "0";
            aValue.CodeSystemVersion = "0";
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Translation</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
        /// <item><term>DisplayName</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Group</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CDGroup>(0) { new MARC.Everest.DataTypes.CDGroup(new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;System.String>>(0) { new MARC.Everest.DataTypes.CR&lt;System.String>(new MARC.Everest.DataTypes.CV&lt;System.String>("0"),new MARC.Everest.DataTypes.CD&lt;System.String>("0")) }) }</description></item>
        /// <item><term>CodingRationale</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
        /// <item><term>Code</term><description>new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
        /// <item><term>CodeSystem</term><description>"0"</description></item>
        /// <item><term>CodeSystemName</term><description>"0"</description></item>
        /// <item><term>CodeSystemVersion</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void CEEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
            aValue.DisplayName = "0";
            aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 }, "0");
            aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
            aValue.CodeSystem = "0";
            aValue.CodeSystemName = "0";
            aValue.CodeSystemVersion = "0";
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.CE<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Qualifier</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)),new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) }</description></item>
        /// <item><term>Translation</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
        /// <item><term>DisplayName</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Group</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CDGroup>(0) { new MARC.Everest.DataTypes.CDGroup(new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;System.String>>(0) { new MARC.Everest.DataTypes.CR&lt;System.String>(new MARC.Everest.DataTypes.CV&lt;System.String>("0"),new MARC.Everest.DataTypes.CD&lt;System.String>("0")) }) }</description></item>
        /// <item><term>CodingRationale</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
        /// <item><term>Code</term><description>new MARC.Everest.DataTypes.Primitives.CodeValue&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
        /// <item><term>CodeSystem</term><description>"0"</description></item>
        /// <item><term>CodeSystemName</term><description>"0"</description></item>
        /// <item><term>CodeSystemVersion</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void CDEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Qualifier = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)), new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))) };
            aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
            aValue.DisplayName = "0";
            aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 }, "0");
            aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
            aValue.CodeSystem = "0";
            aValue.CodeSystemName = "0";
            aValue.CodeSystemVersion = "0";
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.REAL(0.0f)</description></item>
        /// <item><term>Qualifier</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;System.String>>(0) { new MARC.Everest.DataTypes.CR&lt;System.String>(new MARC.Everest.DataTypes.CV&lt;System.String>("0"),new MARC.Everest.DataTypes.CD&lt;System.String>("0")) }</description></item>
        /// <item><term>Translation</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;System.String>>(0) { new MARC.Everest.DataTypes.CD&lt;System.String>("0") }</description></item>
        /// <item><term>DisplayName</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Group</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CDGroup>(0) { new MARC.Everest.DataTypes.CDGroup(new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;System.String>>(0) { new MARC.Everest.DataTypes.CR&lt;System.String>(new MARC.Everest.DataTypes.CV&lt;System.String>("0"),new MARC.Everest.DataTypes.CD&lt;System.String>("0")) }) }</description></item>
        /// <item><term>CodingRationale</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
        /// <item><term>Code</term><description>new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
        /// <item><term>CodeSystem</term><description>"0"</description></item>
        /// <item><term>CodeSystemName</term><description>"0"</description></item>
        /// <item><term>CodeSystemVersion</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void COEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.CO aValue = new MARC.Everest.DataTypes.CO(), bValue = null;
            aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 }, "0");
            aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
            aValue.Code.CodeSystem = "0";
            aValue.Code.CodeSystemName = "0";
            aValue.Code.CodeSystemVersion = "0";
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.CO>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.CO)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Use</term><description>MARC.Everest.DataTypes.EntityNameUse.Legal</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void ENEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.EN aValue = new MARC.Everest.DataTypes.EN(), bValue = null;
            aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(MARC.Everest.DataTypes.EntityNameUse.Legal);
            aValue.Flavor = "0";
            aValue.Part.Add(new MARC.Everest.DataTypes.ENXP("Bob", MARC.Everest.DataTypes.EntityNamePartType.Given));
            aValue.Part.Add(new MARC.Everest.DataTypes.ENXP("Dole", MARC.Everest.DataTypes.EntityNamePartType.Family));

            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.EN>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.EN)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Use</term><description>MARC.Everest.DataTypes.EntityNameUse.Legal</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void TNEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.TN aValue = new MARC.Everest.DataTypes.TN(), bValue = null;
            aValue.Part.Add(new MARC.Everest.DataTypes.ENXP("Hello"));
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.TN>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.TN)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        //[TestMethod]
        //public void SXCMEqualsSerializationTest()
        //{
        //    MARC.Everest.DataTypes.SXCM<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>(), bValue = null;
        //    aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
        //    aValue.Value = new MARC.Everest.DataTypes.INT(0);
        //    StringWriter sw = new StringWriter();
        //    DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
        //    XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
        //    xw.WriteStartElement("test");
        //    fmtr.GraphObject(xw, aValue);
        //    xw.WriteEndElement(); // comp
        //    xw.Flush();
        //    StringReader sr = new StringReader(sw.ToString());
        //    XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
        //    rdr.Read(); rdr.Read();
        //    bValue = Util.Convert<MARC.Everest.DataTypes.SXCM<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.SXCM<MARC.Everest.DataTypes.INT>)));
        //    Assert.AreEqual(aValue, bValue);
        //}
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Component</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.SXCM&lt;MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.SXCM&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) }</description></item>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void SXPREqualsSerializationTest()
        {
            MARC.Everest.DataTypes.SXPR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.SXPR<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Terms = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.SXCM<MARC.Everest.DataTypes.INT>>(0) { new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0)) };
            aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.SXPR<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.SXPR<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Numerator</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>Denominator</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>Expression</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>ExpressionLanguage</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term></term><description>structureAttributet;System.Double>>(0.0f)</description></item>
        /// <item><term>UncertaintyType</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void RTOEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT, MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT, MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Numerator = new MARC.Everest.DataTypes.INT(0);
            aValue.Denominator = new MARC.Everest.DataTypes.INT(0);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT, MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.RTO<MARC.Everest.DataTypes.INT, MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Event</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal)</description></item>
        /// <item><term>Offset</term><description>new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0,"0"))</description></item>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void EIVLEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Event = new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.DomainTimingEventType>(MARC.Everest.DataTypes.DomainTimingEventType.BeforeMeal);
            aValue.Offset = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.PQ>(new MARC.Everest.DataTypes.PQ((decimal)0, "0"));
            aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.EIVL<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Value</term><description>false</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void BLEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.BL aValue = new MARC.Everest.DataTypes.BL(), bValue = null;
            aValue.Value = false;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.BL>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.BL)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Value</term><description>"0"</description></item>
        /// <item><term>Code</term><description>"0"</description></item>
        /// <item><term>CodeSystem</term><description>"0"</description></item>
        /// <item><term>CodeSystemVersion</term><description>"0"</description></item>
        /// <item><term>Type</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.AddressPartType>(MARC.Everest.DataTypes.AddressPartType.AddressLine)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void ADXPEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.ADXP aValue = new MARC.Everest.DataTypes.ADXP(), bValue = null;
            aValue.Value = "0";
            aValue.Code = "0";
            aValue.Type = new System.Nullable<MARC.Everest.DataTypes.AddressPartType>(MARC.Everest.DataTypes.AddressPartType.AddressLine);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.ADXP>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.ADXP)));
            bValue.Type = MARC.Everest.DataTypes.AddressPartType.AddressLine;
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Probability</term><description>0.0f</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void URGEqualsSerializationHighWidthTest()
        {
            MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.Width = new MARC.Everest.DataTypes.PQ((decimal)0, "0");
            //aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.Probability = (decimal)0.0f;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Probability</term><description>0.0f</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void URGEqualsSerializationLowTest()
        {
            MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Low = new MARC.Everest.DataTypes.INT(0);
            //aValue.Width = new MARC.Everest.DataTypes.PQ((decimal)0, "0");
            //aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.Probability = (decimal)0.0f;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Probability</term><description>0.0f</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void URGEqualsSerializationHighTest()
        {
            MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.High = new MARC.Everest.DataTypes.INT(0);
            //aValue.Width = new MARC.Everest.DataTypes.PQ((decimal)0, "0");
            //aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.Probability = (decimal)0.0f;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Probability</term><description>0.0f</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void URGEqualsSerializationLowWidthTest()
        {
            MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Low = new MARC.Everest.DataTypes.INT(0);
            aValue.Width = new MARC.Everest.DataTypes.PQ((decimal)0, "0");
            //aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.Probability = (decimal)0.0f;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Probability</term><description>0.0f</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void URGEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Low = new MARC.Everest.DataTypes.INT(0);
            aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.Probability = (decimal)0.0f;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.URG<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Use</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) }</description></item>
        /// <item><term>UseablePeriod</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.SXCM&lt;MARC.Everest.DataTypes.TS>>(0) { new MARC.Everest.DataTypes.SXCM&lt;MARC.Everest.DataTypes.TS>(new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))) }</description></item>
        /// <item><term>Value</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void TELEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.TEL aValue = new MARC.Everest.DataTypes.TEL(), bValue = null;
            aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Home) };
            aValue.UseablePeriod = new MARC.Everest.DataTypes.GTS(new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.TS>(new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))));
            aValue.Value = "0";
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.TEL>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.TEL)));
            Assert.AreEqual(aValue, bValue);
        }

        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Hull</term><description>new MARC.Everest.DataTypes.SXCM&lt;MARC.Everest.DataTypes.TS>(new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")))</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void GTSEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.GTS aValue = new MARC.Everest.DataTypes.GTS(), bValue = null;
            aValue.Hull = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.TS>(new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10")));
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.GTS>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.GTS)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Use</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.PostalAddressUse>>(0) { new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.PostalAddressUse>(MARC.Everest.DataTypes.PostalAddressUse.HomeAddress) }</description></item>
        /// <item><term>UseablePeriod</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.SXCM&lt;MARC.Everest.DataTypes.TS>>(0) { new MARC.Everest.DataTypes.SXCM&lt;MARC.Everest.DataTypes.TS>(new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))) }</description></item>
        /// <item><term>IsNotOrdered</term><description>false</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void ADEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.AD aValue = new MARC.Everest.DataTypes.AD(), bValue = null;
            aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.PostalAddressUse>>(0) { new MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.PostalAddressUse>(MARC.Everest.DataTypes.PostalAddressUse.HomeAddress) };
            aValue.IsNotOrdered = false;
            aValue.Part.Add(new MARC.Everest.DataTypes.ADXP("123 Main Street West", MARC.Everest.DataTypes.AddressPartType.AddressLine));
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.AD>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.AD)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Phase</term><description>new MARC.Everest.DataTypes.IVL&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
        /// <item><term>Period</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Alignment</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.CalendarCycle>(MARC.Everest.DataTypes.CalendarCycle.Year)</description></item>
        /// <item><term>InstitutionSpecified</term><description>false</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void PIVLEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.PIVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.PIVL<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Phase = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
            aValue.Period = new MARC.Everest.DataTypes.PQ((decimal)0, "0");
            aValue.Alignment = new System.Nullable<MARC.Everest.DataTypes.CalendarCycle>(MARC.Everest.DataTypes.CalendarCycle.Year);
            aValue.InstitutionSpecified = false;
            aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.PIVL<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.PIVL<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Precision</term><description>0</description></item>
        /// <item><term>Expression</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>ExpressionLanguage</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term></term><description>structureAttributet;System.Double>>(0.0f)</description></item>
        /// <item><term>UncertaintyType</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
        /// <item><term>Value</term><description>0.0f</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void REALEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.REAL aValue = new MARC.Everest.DataTypes.REAL(), bValue = null;
            aValue.Value = 0.0f;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.REAL>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.REAL)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Currency</term><description>"0"</description></item>
        /// <item><term>Precision</term><description>0</description></item>
        /// <item><term>Expression</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>ExpressionLanguage</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term></term><description>structureAttributet;System.Double>>(0.0f)</description></item>
        /// <item><term>UncertaintyType</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
        /// <item><term>Value</term><description>0.0f</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void MOEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.MO aValue = new MARC.Everest.DataTypes.MO(), bValue = null;
            aValue.Currency = "0";
            aValue.Value = (decimal)0.0f;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.MO>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.MO)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Language</term><description>"0"</description></item>
        /// <item><term>Translation</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
        /// <item><term>Value</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void STEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.ST aValue = new MARC.Everest.DataTypes.ST(), bValue = null;
            aValue.Language = "0";
            aValue.Value = "0";
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.ST>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.ST)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Code</term><description>new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
        /// <item><term>CodeSystem</term><description>"0"</description></item>
        /// <item><term>CodeSystemName</term><description>"0"</description></item>
        /// <item><term>CodeSystemVersion</term><description>"0"</description></item>
        /// <item><term>DisplayName</term><description>"0"</description></item>
        /// <item><term>Language</term><description>"0"</description></item>
        /// <item><term>Translation</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ST>(0) { new MARC.Everest.DataTypes.ST("0") }</description></item>
        /// <item><term>Value</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void SCEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.SC aValue = new MARC.Everest.DataTypes.SC(), bValue = null;
            aValue.Code = new MARC.Everest.DataTypes.CD<System.String>("0");
            aValue.Language = "0";
            aValue.Value = "0";
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.SC>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.SC)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Unit</term><description>"0"</description></item>
        /// <item><term>Precision</term><description>0</description></item>
        /// <item><term>CodingRationale</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
        /// <item><term>Translation</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR(0.0f,"0","0") }</description></item>
        /// <item><term>Expression</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>ExpressionLanguage</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
/// <item><term></term><description>structureAttributet;System.Decimal>>((decimal)0)</description></item>
        /// <item><term>UncertaintyType</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType>(MARC.Everest.DataTypes.Interfaces.QuantityUncertaintyType.Uniform)</description></item>
        /// <item><term>Value</term><description>(decimal)0</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void PQEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.PQ aValue = new MARC.Everest.DataTypes.PQ(), bValue = null;
            aValue.Unit = "0";
            aValue.Precision = 0;
            aValue.Translation = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.PQR>(0) { new MARC.Everest.DataTypes.PQR((decimal)0.0f, "0", "0") };
            aValue.Value = (decimal)0;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.PQ>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.PQ)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Root</term><description>"0"</description></item>
        /// <item><term>Extension</term><description>"0"</description></item>
        /// <item><term>IdentifierName</term><description>"0"</description></item>
        /// <item><term>Displayable</term><description>false</description></item>
        /// <item><term>Scope</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierScope>(MARC.Everest.DataTypes.IdentifierScope.BusinessIdentifier)</description></item>
        /// <item><term>Reliability</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierReliability>(MARC.Everest.DataTypes.IdentifierReliability.IssuedBySystem)</description></item>
        /// <item><term>AssigningAuthorityName</term><description>"0"</description></item>
        /// <item><term>Use</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void IIEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.II aValue = new MARC.Everest.DataTypes.II(), bValue = null;
            aValue.Root = "0";
            aValue.Extension = "0";
            aValue.Displayable = false;
            aValue.Use = new System.Nullable<MARC.Everest.DataTypes.IdentifierUse>(MARC.Everest.DataTypes.IdentifierUse.Business);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.II>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.II)));
            Assert.AreEqual(aValue, bValue);
        }

        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>DateValuePrecision</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.DatePrecision>(MARC.Everest.DataTypes.DatePrecision.Year)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>DateValue</term><description>DateTime.Parse("2011-1-10")</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void TSEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.TS aValue = new MARC.Everest.DataTypes.TS(), bValue = null;
            aValue.Flavor = "0";
            aValue.DateValue = DateTime.Parse("2011-1-10");
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.TS>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.TS)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Use</term><description>MARC.Everest.DataTypes.EntityNameUse.Legal</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void ONEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.ON aValue = new MARC.Everest.DataTypes.ON(), bValue = null;
            aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(MARC.Everest.DataTypes.EntityNameUse.Legal);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.ON>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.ON)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Items</term><description>new System.Collections.Generic.List&lt;MARC.Everest.DataTypes.INT>(0) { new MARC.Everest.DataTypes.INT(0) }</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void LISTEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Items = new System.Collections.Generic.List<MARC.Everest.DataTypes.INT>(0) { new MARC.Everest.DataTypes.INT(0) };
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.LIST<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Data</term><description>new System.Byte[] { 0 }</description></item>
        /// <item><term>Compression</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF)</description></item>
        /// <item><term>Representation</term><description>MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT</description></item>
        /// <item><term>Language</term><description>"0"</description></item>
        /// <item><term>Translation</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.ED>(0) { new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0") }</description></item>
        /// <item><term>MediaType</term><description>"0"</description></item>
        /// <item><term>IntegrityCheck</term><description>new System.Byte[] { 0 }</description></item>
        /// <item><term>IntegrityCheckAlgorithm</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1)</description></item>
        /// <item><term>Thumbnail</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void EDEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.ED aValue = new MARC.Everest.DataTypes.ED(), bValue = null;
            aValue.Data = new System.Byte[] { 0 };
            aValue.Compression = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataCompression.DF);
            aValue.Representation = MARC.Everest.DataTypes.Interfaces.EncapsulatedDataRepresentation.TXT;
            aValue.Language = "0";
            aValue.MediaType = "0";
            aValue.IntegrityCheck = new System.Byte[] { 0 };
            aValue.IntegrityCheckAlgorithm = new System.Nullable<MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm>(MARC.Everest.DataTypes.Interfaces.EncapsulatedDataIntegrityAlgorithm.SHA1);
            aValue.Thumbnail = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 }, "0");
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.ED>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.ED)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Use</term><description>MARC.Everest.DataTypes.EntityNameUse.Legal</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void PNEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.PN aValue = new MARC.Everest.DataTypes.PN(), bValue = null;
            aValue.Use = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.CS<MARC.Everest.DataTypes.EntityNameUse>>(MARC.Everest.DataTypes.EntityNameUse.Legal);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.PN>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.PN)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Name</term><description>new MARC.Everest.DataTypes.CV&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.CD&lt;MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0))</description></item>
        /// <item><term>Inverted</term><description>false</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void CREqualsSerializationTest()
        {
            MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Name = new MARC.Everest.DataTypes.CV<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
            aValue.Value = new MARC.Everest.DataTypes.CD<MARC.Everest.DataTypes.INT>(new MARC.Everest.DataTypes.INT(0));
            aValue.Inverted = false;
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.CR<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Items</term><description>new System.Collections.Generic.List&lt;MARC.Everest.DataTypes.INT>(0) { new MARC.Everest.DataTypes.INT(0) }</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void SETEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Items = new System.Collections.Generic.List<MARC.Everest.DataTypes.INT>(0) { new MARC.Everest.DataTypes.INT(0) };
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.SET<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }

        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>Value</term><description>0.0f</description></item>
        /// <item><term>Precision</term><description>0</description></item>
        /// <item><term>Qualifier</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;System.String>>(0) { new MARC.Everest.DataTypes.CR&lt;System.String>(new MARC.Everest.DataTypes.CV&lt;System.String>("0"),new MARC.Everest.DataTypes.CD&lt;System.String>("0")) }</description></item>
        /// <item><term>Translation</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CD&lt;System.String>>(0) { new MARC.Everest.DataTypes.CD&lt;System.String>("0") }</description></item>
        /// <item><term>DisplayName</term><description>"0"</description></item>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Group</term><description>new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CDGroup>(0) { new MARC.Everest.DataTypes.CDGroup(new MARC.Everest.DataTypes.LIST&lt;MARC.Everest.DataTypes.CR&lt;System.String>>(0) { new MARC.Everest.DataTypes.CR&lt;System.String>(new MARC.Everest.DataTypes.CV&lt;System.String>("0"),new MARC.Everest.DataTypes.CD&lt;System.String>("0")) }) }</description></item>
        /// <item><term>CodingRationale</term><description>new MARC.Everest.DataTypes.SET&lt;MARC.Everest.DataTypes.CodingRationale>(0) { MARC.Everest.DataTypes.CodingRationale.Original }</description></item>
        /// <item><term>Code</term><description>new MARC.Everest.DataTypes.Primitives.CodeValue&lt;System.String>("0")</description></item>
        /// <item><term>CodeSystem</term><description>"0"</description></item>
        /// <item><term>CodeSystemName</term><description>"0"</description></item>
        /// <item><term>CodeSystemVersion</term><description>"0"</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void PQREqualsSerializationTest()
        {
            MARC.Everest.DataTypes.PQR aValue = new MARC.Everest.DataTypes.PQR(), bValue = null;
            aValue.Value = (decimal)0.0f;
            aValue.DisplayName = "0";
            aValue.OriginalText = new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 }, "0");
            aValue.Code = new MARC.Everest.DataTypes.Primitives.CodeValue<System.String>("0");
            aValue.CodeSystem = "0";
            aValue.CodeSystemName = "0";
            aValue.CodeSystemVersion = "0";
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.PQR>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.PQR)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void IVLEqualsSerializationTest()
        {
            MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Low = new MARC.Everest.DataTypes.INT(0);
            aValue.LowClosed = false;
            aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.HighClosed = false;
            aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void IVLEqualsSerializationLowWidthTest()
        {
            MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Low = new MARC.Everest.DataTypes.INT(0);
            aValue.LowClosed = false;
            //aValue.High = new MARC.Everest.DataTypes.INT(0);
            //aValue.HighIncluded = false;
            aValue.Width = new MARC.Everest.DataTypes.PQ(0, "0");
            aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void IVLEqualsSerializationHighWidthTest()
        {
            MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>(), bValue = null;
            //aValue.Low = new MARC.Everest.DataTypes.INT(0);
            //aValue.LowIncluded = false;
            aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.HighClosed = false;
            aValue.Width = new MARC.Everest.DataTypes.PQ(0, "0");
            aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void IVLEqualsSerializationHighTest()
        {
            MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>(), bValue = null;
            //aValue.Low = new MARC.Everest.DataTypes.INT(0);
            //aValue.LowIncluded = false;
            aValue.High = new MARC.Everest.DataTypes.INT(0);
            aValue.HighClosed = false;
            //aValue.Width = new MARC.Everest.DataTypes.PQ(0, "0");
            aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
        /// <summary>
        /// Serialize an instance and deserialize it to ensure the types are the same using the following initialization data
        /// <list type="table">
        /// <listheader><term>Property</term><description>Comments</description></listheader>
        /// <item><term>OriginalText</term><description>new MARC.Everest.DataTypes.ED(new System.Byte[] { 0 },"0")</description></item>
        /// <item><term>Low</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>LowIncluded</term><description>false</description></item>
        /// <item><term>High</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>HighIncluded</term><description>false</description></item>
        /// <item><term>Width</term><description>new MARC.Everest.DataTypes.PQ((decimal)0,"0")</description></item>
        /// <item><term>Operator</term><description>new System.Nullable&lt;MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull)</description></item>
        /// <item><term>Value</term><description>new MARC.Everest.DataTypes.INT(0)</description></item>
        /// <item><term>NullFlavor</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.AskedUnknown)</description></item>
        /// <item><term>UpdateMode</term><description>new MARC.Everest.DataTypes.CS&lt;MARC.Everest.DataTypes.UpdateMode>(MARC.Everest.DataTypes.UpdateMode.Add)</description></item>
        /// <item><term>Flavor</term><description>"0"</description></item>
        /// <item><term>ValidTimeLow</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ValidTimeHigh</term><description>new MARC.Everest.DataTypes.TS(DateTime.Parse("2011-1-10"))</description></item>
        /// <item><term>ControlActRoot</term><description>"0"</description></item>
        /// </list>
        [TestMethod]
        public void IVLEqualsSerializationLowTest()
        {
            MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT> aValue = new MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>(), bValue = null;
            aValue.Low = new MARC.Everest.DataTypes.INT(0);
            aValue.LowClosed = false;
            //aValue.High = new MARC.Everest.DataTypes.INT(0);
            //aValue.HighIncluded = false;
            //aValue.Width = new MARC.Everest.DataTypes.PQ(0, "0");
            aValue.Operator = new System.Nullable<MARC.Everest.DataTypes.SetOperator>(MARC.Everest.DataTypes.SetOperator.Hull);
            aValue.Flavor = "0";
            StringWriter sw = new StringWriter();
            DatatypeFormatter fmtr = new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.Canadian };
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8 }));
            xw.WriteStartElement("test");
            fmtr.GraphObject(xw, aValue);
            xw.WriteEndElement(); // comp
            xw.Flush();
            StringReader sr = new StringReader(sw.ToString());
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            rdr.Read(); rdr.Read();
            bValue = Util.Convert<MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>>(fmtr.ParseObject(rdr, typeof(MARC.Everest.DataTypes.IVL<MARC.Everest.DataTypes.INT>)));
            Assert.AreEqual(aValue, bValue);
        }
    }
}
