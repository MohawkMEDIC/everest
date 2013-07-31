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
 * User: Trevor Davis
 * Date: July 22, 2009
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MARC.Everest.Xml
{
    /// <summary>
    /// A <see cref="T:System.Xml.XmlWriter"/> that tracks its state
    /// </summary>
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
    public class XmlStateWriter : XmlWriter
    {
        private XmlWriter m_InnerWriter;
        private Stack<XmlQualifiedName> m_NameStack = new Stack<XmlQualifiedName>();

        /// <summary>
        /// Represents the state writer as a string (current path)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return CurrentPath;
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xw"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xw")]
        public XmlStateWriter(XmlWriter xw)
        {
            m_InnerWriter = xw;
            m_NameStack = new Stack<XmlQualifiedName>();
        }

        /// <summary>
        /// Get the current XML path 
        /// </summary>
        public string CurrentPath
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                XmlQualifiedName[] xqa = m_NameStack.ToArray();
                for(int i = xqa.Length - 1; i >= 0; i--)
                    sb.AppendFormat("/{0}#{1}", xqa[i].Namespace, xqa[i].Name);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Get the inner reading settings
        /// </summary>
        public override XmlWriterSettings Settings
        {
            get
            {
                return m_InnerWriter.Settings;
            }
        }

        /// <summary>
        /// Element stack
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Stack<XmlQualifiedName> ElementStack { get { return m_NameStack; } }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public XmlQualifiedName CurrentElement { get { return m_NameStack.Peek(); } }

#if !WINDOWS_PHONE
        /// <summary>
        /// 
        /// </summary>
        public override void Close()
        {
            m_InnerWriter.Close();
            m_NameStack.Clear();
        }
#endif 

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override void Flush()
        {
            m_InnerWriter.Flush();
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public override string LookupPrefix(string ns)
        {
            return m_InnerWriter.LookupPrefix(ns);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            m_InnerWriter.WriteBase64(buffer, index, count);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public override void WriteCData(string text)
        {
            m_InnerWriter.WriteCData(text);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        public override void WriteCharEntity(char ch)
        {
            m_InnerWriter.WriteCharEntity(ch);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void WriteChars(char[] buffer, int index, int count)
        {
            m_InnerWriter.WriteChars(buffer, index, count);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public override void WriteComment(string text)
        {
            m_InnerWriter.WriteComment(text);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pubid"></param>
        /// <param name="sysid"></param>
        /// <param name="subset"></param>
        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            m_InnerWriter.WriteDocType(name, pubid, sysid, subset);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override void WriteEndAttribute()
        {
            m_InnerWriter.WriteEndAttribute();
            m_NameStack.Pop();
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override void WriteEndDocument()
        {
            m_InnerWriter.WriteEndDocument();
            m_NameStack.Clear();
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override void WriteEndElement()
        {
            m_InnerWriter.WriteEndElement();
            m_NameStack.Pop();
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public override void WriteEntityRef(string name)
        {
            m_InnerWriter.WriteEntityRef(name);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override void WriteFullEndElement()
        {
            m_InnerWriter.WriteFullEndElement();
            m_NameStack.Pop();
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public override void WriteProcessingInstruction(string name, string text)
        {
            m_InnerWriter.WriteProcessingInstruction(name, text);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public override void WriteRaw(string data)
        {
            m_InnerWriter.WriteRaw(data);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void WriteRaw(char[] buffer, int index, int count)
        {
            m_InnerWriter.WriteRaw(buffer, index, count);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="localName"></param>
        /// <param name="ns"></param>
        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            m_InnerWriter.WriteStartAttribute(prefix, localName, ns);
            m_NameStack.Push(new XmlQualifiedName(localName, ns));
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="standalone"></param>
        public override void WriteStartDocument(bool standalone)
        {
            m_InnerWriter.WriteStartDocument(standalone);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override void WriteStartDocument()
        {
            m_InnerWriter.WriteStartDocument();
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="localName"></param>
        /// <param name="ns"></param>
        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            m_InnerWriter.WriteStartElement(prefix, localName, ns);
            m_NameStack.Push(new XmlQualifiedName(localName, ns));
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override WriteState WriteState
        {
            get { return m_InnerWriter.WriteState; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public override void WriteString(string text)
        {
            m_InnerWriter.WriteString(text);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lowChar"></param>
        /// <param name="highChar"></param>
        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            m_InnerWriter.WriteSurrogateCharEntity(lowChar, highChar);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ws"></param>
        public override void WriteWhitespace(string ws)
        {
            m_InnerWriter.WriteWhitespace(ws);
        }

    }
}