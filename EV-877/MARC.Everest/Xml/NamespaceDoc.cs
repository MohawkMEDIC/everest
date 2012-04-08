using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Xml
{
    /// <summary>
    /// Classes within this namespace are used to facilitate the formatting of 
    /// data to/from XML
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="T:MARC.Everest.Xml.XmlStateWriter"/> and <see cref="T:MARC.Everest.Xml.XmlStateReader"/>
    /// classes are wrapper for the <see cref="T:System.Xml.XmlWriter"/> and <see cref="T:System.Xml.XmlReader"/>
    /// classes respectively. These classes add the functionality of determining the path to the current 
    /// element and thus, are used by formatters to report errors.</para>
    /// </remarks>
    /// <example>
    /// The following example illustrates the use of the XmlStateWriter
    /// <code lang="cs" title="Getting the current path">
    /// Stream s = Console.OpenStandardOutput();
    /// XmlStateWriter writer = new XmlStateWriter(XmlWriter.Create(s));
    /// 
    /// // Write the start of an HTML document
    /// writer.WriteStartElement("html");
    /// writer.WriteStartElement("body");
    /// 
    /// // currentPath will be "/html/body"
    /// string currentPath = writer.CurrentPath; 
    /// 
    /// writer.WriteStartElement("p"); 
    /// // currentPath will be "/html/body/p"
    /// currentPath = writer.CurrentPath; 
    /// 
    /// writer.WriteEndElement();
    /// writer.WriteEndElement();
    /// // currentPath will be "/html"
    /// currentPath = writer.CurrentPath;
    /// 
    /// writer.WriteEndElement();
    /// </code>
    /// </example>
    internal class NamespaceDoc
    {
    }
}
