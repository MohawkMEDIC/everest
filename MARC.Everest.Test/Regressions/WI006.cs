using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.Formatters.XML.ITS1;
using System.Xml;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Xml;

namespace MARC.Everest.Test.Regressions
{
    [TestClass]
    public class WI006
    {
        /// <summary>
        /// This test will verify whether the formatter (XML ITS1) throws an exception
        /// when invalid characters are present in the stream
        /// </summary>
        [TestMethod]
        public void TestInvalidCharactersThrowUsingXmlStateReaderUniprocessor()
        {
            try
            {
                // Load the stream
                using (var str = this.GetType().Assembly.GetManifestResourceStream("MARC.Everest.Test.Resources.WI006Sample.xml"))
                {
                    using (XmlStateReader xsw = new XmlStateReader(XmlReader.Create(str)))
                    {
                        XmlIts1Formatter fmtr = new XmlIts1Formatter();
                        fmtr.GraphAides.Add(new DatatypeFormatter(DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture));
                        fmtr.Settings = SettingsType.DefaultUniprocessor;
                        fmtr.ValidateConformance = false;

                        // Parse
                        while (xsw.LocalName != "MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.SubstanceAdministration")
                            xsw.Read(); /// clear out the prolog stuff

                        var result = fmtr.Parse(xsw, typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.SubstanceAdministration));
                        Assert.Fail("Should throw exception!");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(XmlException));
            }
        }

        /// <summary>
        /// This test will verify whether the formatter (XML ITS1) throws an exception
        /// when invalid characters are present in the stream
        /// </summary>
        [TestMethod]
        public void TestInvalidCharactersThrowUsingXmlStateReader()
        {
            try
            {
                // Load the stream
                using (var str = this.GetType().Assembly.GetManifestResourceStream("MARC.Everest.Test.Resources.WI006Sample.xml"))
                {
                    using (XmlStateReader xsw = new XmlStateReader(XmlReader.Create(str)))
                    {
                        XmlIts1Formatter fmtr = new XmlIts1Formatter();
                        fmtr.GraphAides.Add(new DatatypeFormatter(DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture));
                        fmtr.Settings = SettingsType.DefaultLegacy;
                        fmtr.ValidateConformance = false;

                        // Parse
                        while (xsw.LocalName != "MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.SubstanceAdministration")
                            xsw.Read(); /// clear out the prolog stuff

                        var result = fmtr.Parse(xsw, typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.SubstanceAdministration));
                        Assert.Fail("Should throw exception!");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(XmlException));
            }
        }

        /// <summary>
        /// This test will verify whether the formatter (XML ITS1) throws an exception
        /// when invalid characters are present in the stream without using XmlStateReader
        /// </summary>
        [TestMethod]
        public void TestInvalidCharactersThrowUsingStream()
        {
            try
            {
                // Load the stream
                using (var str = this.GetType().Assembly.GetManifestResourceStream("MARC.Everest.Test.Resources.WI006Sample.xml"))
                {
                    XmlIts1Formatter fmtr = new XmlIts1Formatter();
                    fmtr.GraphAides.Add(new DatatypeFormatter(DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture));
                    fmtr.Settings = SettingsType.DefaultLegacy;
                    fmtr.ValidateConformance = false;
                    fmtr.AddRootNameMaps(new Type[] { typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.SubstanceAdministration) });
                    var result = fmtr.Parse(str);
                    Assert.Fail("Should throw exception!");
                }
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(XmlException));
            }
        }

    }
}
