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
 * Date: 24-4-2014
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.StructDoc;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// SD Test
    /// </summary>
    [TestClass]
    public class SDTest
    {
        [TestMethod]
        public void SDBasicGraphTest()
        {

            String expected = "<test xmlns=\"urn:hl7-org:v3\" mediaType=\"text/x-hl7-title+xml\"><table test=\"32\"><thead></thead></table></test>";
            SD instance = new SD();
            var content = new StructDocElementNode("table").AddElement("thead").AddAttribute("test", "32");
            instance.Content.Add(content);
            String actual = R1SerializationHelper.SerializeAsString(instance);
            R2SerializationHelper.XmlIsEquivalent(expected, actual);
        }

        [TestMethod]
        public void SDBasicParseTest()
        {
            SD instance = new SD();
            instance.Content.Add(new StructDocElementNode("table").AddElement("thead").AddAttribute("test", "32"));
            String actual = R1SerializationHelper.SerializeAsString(instance);
            var parsed = R1SerializationHelper.ParseString<SD>(actual);
            Assert.AreEqual(instance, parsed);
        }

        [TestMethod]
        public void SDBasicNonEquals()
        {
            SD instance = new SD();
            instance.Content.Add(new StructDocElementNode("table").AddElement("thead").AddAttribute("test", "32"));
            String actual = R1SerializationHelper.SerializeAsString(instance);
            var parsed = R1SerializationHelper.ParseString<SD>(actual.Replace("thead />","th />"));
            Assert.AreNotEqual(instance, parsed);
        }

        [TestMethod]
        public void SDValidationPassTest()
        {
            SD instance = new SD();
            instance.Content.Add(new StructDocElementNode("table").AddElement("thead").AddAttribute("test", "32"));
            Assert.IsTrue(instance.Validate(), "Instance should be valid");
        }

        [TestMethod]
        public void SDValidationFailNFAndValueTest()
        {
            SD instance = new SD();
            instance.Content.Add(new StructDocElementNode("table").AddElement("thead").AddAttribute("test", "32"));
            instance.NullFlavor = NullFlavor.Derived;
            Assert.IsFalse(instance.Validate(), "Instance should be invalid");
        }

        [TestMethod]
        public void SDValidationExPassTest()
        {
            SD instance = new SD();
            instance.Content.Add(new StructDocElementNode("table").AddElement("thead").AddAttribute("test", "32"));
            Assert.IsTrue(instance.ValidateEx().Count() == 0, "Instance should have no ValidateEx");
            Assert.IsTrue(instance.ValidateEx().Count(e => e.Message.Contains("NullFlavor")) == 0, "Instance should have no nullFlavor warnings");
            Assert.IsTrue(instance.ValidateEx().Count(e => e.Message.Contains("MediaType")) == 0, "Instance should have no mediaType warnings");

        }

        [TestMethod]
        public void SDValidationExFailNFAndValueTest()
        {
            SD instance = new SD();
            instance.Content.Add(new StructDocElementNode("table").AddElement("thead").AddAttribute("test", "32"));
            instance.NullFlavor = NullFlavor.Derived;
            Assert.IsTrue(instance.ValidateEx().Count() == 1, "Instance should have one ValidateEx");
            Assert.IsTrue(instance.ValidateEx().Count(e => e.Message.Contains("NullFlavor")) == 1, "Instance should have one nullFlavor warnings");
            Assert.IsTrue(instance.ValidateEx().Count(e => e.Message.Contains("MediaType")) == 0, "Instance should have no mediaType warnings");
        }


        [TestMethod]
        public void SDValidationExFailMediaTypeTest()
        {
            SD instance = new SD();
            instance.Content.Add(new StructDocElementNode("table").AddElement("thead").AddAttribute("test", "32"));
            instance.MediaType = "text/xml";
            Assert.IsTrue(instance.ValidateEx().Count() == 1, "Instance should have one ValidateEx");
            Assert.IsTrue(instance.ValidateEx().Count(e => e.Message.Contains("NullFlavor")) == 0, "Instance should have one nullFlavor warnings");
            Assert.IsTrue(instance.ValidateEx().Count(e => e.Message.Contains("MediaType")) == 1, "Instance should have no mediaType warnings");
        }
    }
}
