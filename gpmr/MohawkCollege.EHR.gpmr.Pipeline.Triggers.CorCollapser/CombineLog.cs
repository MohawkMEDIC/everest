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
 * Date: 02-08-2012
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorCollapser
{
    /// <summary>
    /// File format for storing combination logs, used to keep consistency between
    /// different renderings of assemblies
    /// </summary>
    [XmlRoot("combineLog", Namespace = "urn:marc-hi.ca/gpmr/combine")]
    [XmlType("combineLog", Namespace = "urn:marc-hi.ca/gpmr/combine")]
    public class CombineLog
    {

        /// <summary>
        /// Load a combination log
        /// </summary>
        public static CombineLog Load(String fileName)
        {
            Stream s = null;
            try
            {
                s = File.OpenRead(fileName);
                XmlSerializer xsz = new XmlSerializer(typeof(CombineLog));
                return (CombineLog)xsz.Deserialize(s);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
        }

        /// <summary>
        /// Save the combine log
        /// </summary>
        public void Save(String fileName)
        {
            Stream s = null;
            try
            {
                s = File.Create(fileName);
                XmlSerializer xsz = new XmlSerializer(typeof(CombineLog));
                xsz.Serialize(s, this);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
        }

        /// <summary>
        /// Combine operations
        /// </summary>
        public CombineLog()
        {
            CombineOps = new List<CombineInfo>();
        }

        /// <summary>
        /// Combine operations performed
        /// </summary>
        [XmlElement("combine")]
        public List<CombineInfo> CombineOps { get; set; }

    }

    /// <summary>
    /// Combination information
    /// </summary>
    [XmlType("combineInfo", Namespace = "urn:marc-hi.ca/gpmr/combine")]
    public class CombineInfo
    {

        /// <summary>
        /// Class information
        /// </summary>
        public CombineInfo()
        {
            this.Class = new List<string>();
        }

        /// <summary>
        /// The class that was actually chosen
        /// </summary>
        [XmlAttribute("destination")]
        public string Destination { get; set; }

        /// <summary>
        /// The classes that were combined into the chosen class
        /// </summary>
        [XmlElement("class")]
        public List<String> Class { get; set; }
    }
}
