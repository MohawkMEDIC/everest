/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: 09-26-2011
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// Represents a collection of all jurisdictional
    /// constraints on a model
    /// </summary>
    [XmlType("deltaSet", Namespace = "urn:infoway.ca/deltaSet")]
    [XmlRoot("deltaSet", Namespace = "urn:infoway.ca/deltaSet")]
    public class DeltaSet : BaseXmlType
    {

        /// <summary>
        /// Identifies the version of the delta set
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; }
        /// <summary>
        /// Identifies the realm that the delta set has been constrained for
        /// </summary>
        [XmlElement("realm")]
        public CodeValue Realm { get; set; }
        /// <summary>
        /// Identifies meta data about the delta set
        /// </summary>
        [XmlElement("metaData")]
        public MetaData MetaData { get; set; }
        /// <summary>
        /// Identifies entry points that are owned by the delta-set
        /// </summary>
        [XmlElement("ownedEntryPoint")]
        public List<OwnedEntryPoint> EntryPoint { get; set; }

        /// <summary>
        /// Load the delta set
        /// </summary>
        public static DeltaSet Load(string fileName)
        {
            Stream fs = null;
            try
            {
                fs = File.OpenRead(fileName);
                XmlSerializer xsz = new XmlSerializer(typeof(DeltaSet));
                var retVal = xsz.Deserialize(fs) as DeltaSet;
                return retVal;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
