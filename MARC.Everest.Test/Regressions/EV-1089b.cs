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

namespace MARC.Everest.Test.Regressions
{
    [TestClass]
    public class EV_1089b
    {

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
            //Assert.AreEqual(test, result.Structure);
        }

    }
}
