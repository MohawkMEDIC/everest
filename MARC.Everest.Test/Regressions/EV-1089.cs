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
 * Date: 10-6-2013
 */
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
