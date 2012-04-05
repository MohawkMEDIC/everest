using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using System.IO;
using System.Xml;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.Interfaces;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace DataTypeExplorer
{
    static class FormatterHelper
    {

        /// <summary>
        /// Format a data type into a string
        /// </summary>
        /// <param name="dataType">The data type to format</param>
        /// <param name="rootName">The name of the root element</param>
        /// <returns>A pretty-printed XML string</returns>
        internal static string FormatDataType(IGraphable dataType, string rootName)
        {
            MemoryStream ms = new MemoryStream();
            // Format to XML Writer
            XmlWriter xmlWriter = XmlWriter.Create(ms, new XmlWriterSettings() { Indent = true, Encoding = System.Text.Encoding.ASCII });
            xmlWriter.WriteStartElement(rootName, "urn:hl7-org:v3");

            // Don't worry about these lines right now
            XmlIts1Formatter fmtr = new XmlIts1Formatter();
            fmtr.GraphAides.Add(new DatatypeFormatter());
            fmtr.GraphObject(xmlWriter, dataType);
            // Finish the string
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            return System.Text.Encoding.UTF8.GetString(ms.GetBuffer());
        }
    }
}
