using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using System.IO;
using System.Xml;

namespace MARC.Everest.Test.Regressions
{
    /// <summary>
    /// Summary description for EV_1101
    /// </summary>
    [TestClass]
    public class EV_1101
    {
        public EV_1101()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void EV_1101_DefaultSerializationLegacy()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author nullFlavor=\"NI\" xsi:nil=\"true\" /></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = DateTime.Now;
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.NewGuid()));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;
            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;
            formatter.Settings =  SettingsType.DefaultLegacy;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);

            }
            
        }

        [TestMethod]
        public void EV_1101_SuppressNullEnforcementLegacy()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author xsi:nil=\"true\" typeCode=\"AUT\" nullFlavor=\"NI\" contextControlCode=\"OP\"/></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = DateTime.Now;
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.NewGuid()));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.Settings = SettingsType.DefaultLegacy;
            formatter.Settings |= SettingsType.SuppressNullEnforcement;
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var results = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);

            }

        }

        [TestMethod]
        public void EV_1101_SuppressXsiNilLegacy()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author typeCode=\"AUT\" nullFlavor=\"NI\" contextControlCode=\"OP\"><time value=\"20140116103733.292-0500\" /><assignedAuthor classCode=\"ASSIGNED\"><id root=\"2BCF1373-9199-4574-A254-0E7558DE9825\" /></assignedAuthor></author></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = (TS)"20140116103733.292-0500";
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.Parse("2BCF1373-9199-4574-A254-0E7558DE9825")));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.Settings = SettingsType.DefaultLegacy;
            formatter.Settings |= SettingsType.SuppressXsiNil | SettingsType.SuppressNullEnforcement;
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var results = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);
            }

        }

        [TestMethod]
        public void EV_1101_DefaultSerializationReflect()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author nullFlavor=\"NI\" xsi:nil=\"true\" /></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = DateTime.Now;
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.NewGuid()));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;
            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;
            formatter.Settings = SettingsType.DefaultUniprocessor;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);

            }

        }

        [TestMethod]
        public void EV_1101_SuppressNullEnforcementReflect()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author xsi:nil=\"true\" typeCode=\"AUT\" nullFlavor=\"NI\" contextControlCode=\"OP\"/></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = DateTime.Now;
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.NewGuid()));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.Settings = SettingsType.DefaultUniprocessor;
            formatter.Settings |= SettingsType.SuppressNullEnforcement;
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var results = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);

            }

        }

        [TestMethod]
        public void EV_1101_SuppressXsiNilReflect()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author typeCode=\"AUT\" nullFlavor=\"NI\" contextControlCode=\"OP\"><time value=\"20140116103733.292-0500\" /><assignedAuthor classCode=\"ASSIGNED\"><id root=\"2BCF1373-9199-4574-A254-0E7558DE9825\" /></assignedAuthor></author></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = (TS)"20140116103733.292-0500";
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.Parse("2BCF1373-9199-4574-A254-0E7558DE9825")));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.Settings = SettingsType.DefaultUniprocessor;
            formatter.Settings |= SettingsType.SuppressXsiNil | SettingsType.SuppressNullEnforcement;
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var results = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);
            }

        }

        [TestMethod]
        public void EV_1101_DefaultSerializationMultiprocessor()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author nullFlavor=\"NI\" xsi:nil=\"true\" /></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = DateTime.Now;
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.NewGuid()));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;
            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;
            formatter.Settings = SettingsType.DefaultMultiprocessor;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);

            }

        }

        [TestMethod]
        public void EV_1101_SuppressNullEnforcementMultiprocessor()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author xsi:nil=\"true\" typeCode=\"AUT\" nullFlavor=\"NI\" contextControlCode=\"OP\"/></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = DateTime.Now;
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.NewGuid()));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.Settings = SettingsType.DefaultMultiprocessor;
            formatter.Settings |= SettingsType.SuppressNullEnforcement;
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var results = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);

            }

        }

        [TestMethod]
        public void EV_1101_SuppressXsiNilMultiprocessor()
        {
            String expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ClinicalDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" classCode=\"DOCCLIN\" moodCode=\"EVN\" xmlns=\"urn:hl7-org:v3\"><author typeCode=\"AUT\" nullFlavor=\"NI\" contextControlCode=\"OP\"><time value=\"20140116103733.292-0500\" /><assignedAuthor classCode=\"ASSIGNED\"><id root=\"2BCF1373-9199-4574-A254-0E7558DE9825\" /></assignedAuthor></author></ClinicalDocument>";

            ClinicalDocument document = new ClinicalDocument();
            Author a = new Author();
            a.Time = (TS)"20140116103733.292-0500";
            a.AssignedAuthor = new AssignedAuthor(SET<II>.CreateSET(Guid.Parse("2BCF1373-9199-4574-A254-0E7558DE9825")));
            document.Author.Add(a);
            a.NullFlavor = NullFlavor.NoInformation;

            XmlIts1Formatter formatter = new XmlIts1Formatter();
            formatter.Settings = SettingsType.DefaultMultiprocessor;
            formatter.Settings |= SettingsType.SuppressXsiNil | SettingsType.SuppressNullEnforcement;
            formatter.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            formatter.ValidateConformance = false;

            StringWriter sw = new StringWriter();
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                var results = formatter.Graph(xw, document);
                xw.Flush();
                String data = sw.ToString();
                R2SerializationHelper.XmlIsEquivalent(expected, data);
            }

        }
    }
}
