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

namespace MARC.Everest.Test.Regressions
{
    [TestClass]
    public class EV_1089
    {
        /// <summary>
        /// Test parsing of ENXP
        /// </summary>
        [TestMethod]
        public void EV_1089ENXPParseTest()
        {
            EN myEn = EN.FromFamilyGiven(EntityNameUse.License, "Toet", "J");
            myEn.Part[0].Qualifier = new SET<CS<EntityNamePartQualifier>>();
            myEn.Part[1].Qualifier = new SET<CS<EntityNamePartQualifier>>();
            myEn.Part[0].Qualifier.Add(EntityNamePartQualifier.Birth);
            myEn.Part[1].Qualifier.Add(EntityNamePartQualifier.Initial);
            String xmlString = R1SerializationHelper.SerializeAsString(myEn);

            StringReader sr = new StringReader(xmlString);
            DatatypeFormatter fmtr = new DatatypeFormatter();
            XmlStateReader rdr = new XmlStateReader(XmlReader.Create(sr));
            while (rdr.NodeType != XmlNodeType.Element)
                rdr.Read();
            var result = fmtr.Parse(rdr, typeof(EN));
            Assert.AreEqual(0, result.Details.Count(o=>o.Type == Connectors.ResultDetailType.Error));

        }
    }
}
