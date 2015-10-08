using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.Xml;
using System.Xml;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.Regressions
{
    [TestClass]
    public class WI005
    {
        /// <summary>
        /// Codeplex Work Item #5
        /// </summary>
        [TestMethod]
        public void TestDoesntParseEmptyPIVL()
        {
            // Load the stream
            using (var str = this.GetType().Assembly.GetManifestResourceStream("MARC.Everest.Test.Resources.WI005Sample.xml"))
            {
                using (XmlStateReader xsw = new XmlStateReader(XmlReader.Create(str)))
                {
                    XmlIts1Formatter fmtr = new XmlIts1Formatter();
                    fmtr.GraphAides.Add(new DatatypeFormatter(DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture));
                    fmtr.Settings = SettingsType.DefaultLegacy;
                    fmtr.ValidateConformance = false;

                    // Parse
                    while (xsw.LocalName != "substanceAdministration")
                        xsw.Read(); /// clear out the prolog stuff

                    var result = fmtr.Parse(xsw, typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.SubstanceAdministration));
                    Assert.IsInstanceOfType(result.Structure, typeof(MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.SubstanceAdministration));
                    Assert.AreEqual((result.Structure as MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.SubstanceAdministration).EffectiveTime.Count, 2);

                }
            }
        }
    }
}
