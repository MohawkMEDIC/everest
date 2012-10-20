/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.HeuristicEngine
{
    /// <summary>
    /// Since GPMR is a .NET program, it is impossible for it to scan the Java
    /// archive libraries for information, so instead we use a heuristics file
    /// </summary>
    [XmlType(Namespace = "urn:marc-hi:gpmr:heuristic")]
    [XmlRoot(ElementName = "heuristic", Namespace = "urn:marc-hi:gpmr:heuristic")]
    public class HeuristicData
    {

        /// <summary>
        /// Interfaces
        /// </summary>
        [XmlElement(ElementName = "interface")]
        public List<InterfaceData> Interfaces { get; set; }

        /// <summary>
        /// Datatypes
        /// </summary>
        [XmlElement(ElementName = "datatype")]
        public List<DatatypeData> Datatypes { get; set; }

        /// <summary>
        /// Datatypes
        /// </summary>
        [XmlElement(ElementName = "vocabulary")]
        public List<DatatypeData> Vocabulary { get; set; }

    }

    /// <summary>
    /// Data related to interfaces
    /// </summary>
    [XmlType(Namespace = "urn:marc-hi:gpmr:heuristic")]
    public class InterfaceData
    {
        /// <summary>
        /// The name of the interface
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// The list of properties within the interface that must be implemented
        /// </summary>
        [XmlElement("property")]
        public List<PropertyInfoData> Properties { get; set; }
        /// <summary>
        /// The generic parameter
        /// </summary>
        [XmlAttribute("genericParameter")]
        public string GenericParameter { get; set; }
    }

    /// <summary>
    /// Data related to property information
    /// </summary>
    [XmlType(Namespace = "urn:marc-hi:gpmr:heuristic")]
    public class PropertyInfoData
    {
        /// <summary>
        /// The name of the property
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// The datatype of the property
        /// </summary>
        [XmlAttribute("type")]
        public string DataType { get; set; }
        /// <summary>
        /// Argument type(s)
        /// </summary>
        [XmlAttribute("argumentType")]
        public string ArgumentType { get; set; }
    }

    /// <summary>
    /// Data related to data types
    /// </summary>
    [XmlType(Namespace = "urn:marc-hi:gpmr:heuristic")]
    public class DatatypeData
    {
        /// <summary>
        /// The name of the datatype in the MIF
        /// </summary>
        [XmlAttribute("name")]
        public string MifDatatype { get; set; }
        /// <summary>
        /// The name of the type in java
        /// </summary>
        [XmlAttribute("type")]
        public string JavaType { get; set; }
        /// <summary>
        /// Default java binding type
        /// </summary>
        [XmlAttribute("defaultBind")]
        public string DefaultBind { get; set; }
        /// <summary>
        /// The generic parameter to the type
        /// </summary>
        [XmlAttribute("genericParameter")]
        public string TemplateParameter { get; set; }
        /// <summary>
        /// Setter overrides for the particular datatype
        /// </summary>
        [XmlElement("setterOverride")]
        public List<SetterOverrideData> SetterOverride { get; set; }

        /// <summary>
        /// Type map
        /// </summary>
        [XmlElement("map")]
        public List<MapData> TypeMap { get; set; }
    }

    /// <summary>
    /// Represents a type map
    /// </summary>
    [XmlType(Namespace = "urn:marc-hi:gpmr:heuristic")]
    public class MapData
    {
        /// <summary>
        /// The name of the from type
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// The name of the argument on the map type
        /// </summary>
        [XmlAttribute("argument")]
        public string Argument { get; set; }
    }

    /// <summary>
    /// Data related to a setter override
    /// </summary>
    [XmlType(Namespace = "urn:marc-hi:gpmr:heuristic")]
    public class SetterOverrideData
    {
        /// <summary>
        /// Parameters to the overridden setter
        /// </summary>
        [XmlElement("parameter")]
        public List<PropertyInfoData> Parameters { get; set; }
        /// <summary>
        /// The value instance that is created in the setter text that describes the value 
        /// instance variable name to set the backing field to.
        /// </summary>
        [XmlElement("value")]
        public PropertyInfoData ValueInstance { get; set; }
        /// <summary>
        /// The text of the Setter
        /// </summary>
        [XmlText]
        public string SetterText { get; set; }
    }
    
}
