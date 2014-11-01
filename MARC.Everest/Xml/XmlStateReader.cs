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
 * Date: April 28, 2010
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;

namespace MARC.Everest.Xml
{
    /// <summary>
    /// An <see cref="T:System.Xml.XmlReader"/> that keeps track of its current state
    /// </summary>
    /// <example>
    /// See <see cref="T:MARC.Everest.Xml.XmlStateWriter"/> for an example of using these classes
    /// </example>
    public class XmlStateReader : XmlReader, IXmlNamespaceResolver
    {

        /// <summary>
        /// Represent this state reader as a string (path)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            
            return this.CurrentPath;
        }

        // inner reader for the XML Reader
        private XmlReader innerReader;
        private Stack<XmlQualifiedName> nameStack = new Stack<XmlQualifiedName>();
        // Allows formatters to add pseudo attributes that aren't part of the XML instance, but 
        // are added by meta data in the API. Example of using this would be flavors
        private Dictionary<String, String> fakeAttributes = new Dictionary<string, string>();
      
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xr"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xw")]
        public XmlStateReader(XmlReader xr)
        {
            innerReader = xr;

            nameStack = new Stack<XmlQualifiedName>(100);
            if (xr.ReadState != ReadState.Initial)
                nameStack.Push(new XmlQualifiedName(xr.LocalName, xr.NamespaceURI));

        }

        /// <summary>
        /// Get the inner reading settings
        /// </summary>
        public override XmlReaderSettings Settings
        {
            get
            {
                return innerReader.Settings;
            }
        }

        /// <summary>
        /// Add a fake attribute. Fake attributes don't appear in the stream, but can be used by 
        /// formatters to pass attributes to graph aides. Fake attributes appear to be "real"
        /// as they are returned by the <see cref="F:GetAttribute()"/> function.
        /// </summary>
        public void AddFakeAttribute(string attributeName, string attributeValue)
        {
            if (!fakeAttributes.ContainsKey(attributeName))
                fakeAttributes.Add(attributeName, attributeValue);
            //throw new InvalidOperationException(String.Format("The fake attribute '{0}' already exists in this state reader.", attributeName));
        }

        /// <summary>
        /// Get the current XML path 
        /// </summary>
        public string CurrentPath
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                XmlQualifiedName[] xqa = nameStack.ToArray();
                for(int i = xqa.Length - 1; i >= 0; i--)
                    sb.AppendFormat("/{0}#{1}", xqa[i].Namespace, xqa[i].Name);

                // Append the current node name if the current element is empty
                if (IsEmptyElement)
                    sb.AppendFormat("/{0}#{1}", this.NamespaceURI, this.LocalName);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Get the current XML path 
        /// </summary>
        public string CurrentXPath
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                XmlQualifiedName[] xqa = nameStack.ToArray();
                for (int i = xqa.Length - 1; i >= 0; i--)
                    sb.AppendFormat("/*[namespace-uri() = '{0}' and local-name() = '{1}']", xqa[i].Namespace, xqa[i].Name);

                // Append the current node name if the current element is empty
                if (IsEmptyElement)
                    sb.AppendFormat("/*[namespace-uri() = '{0}' and local-name() = '{1}']", this.NamespaceURI, this.LocalName);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Element stack
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Stack<XmlQualifiedName> ElementStack { get; set; }
        
        /// <summary>
        /// Gets the current name stack
        /// </summary>
        public XmlQualifiedName CurrentElement
        {
            get
            {
                if (this.IsEmptyElement) // The current element isn't on the stack
                    return new XmlQualifiedName(this.LocalName, this.NamespaceURI);
                return nameStack.Peek();
            }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override int AttributeCount
        {
            get { return innerReader.AttributeCount + fakeAttributes.Count; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string BaseURI
        {
            get { return innerReader.BaseURI; }
        }

#if !WINDOWS_PHONE
        /// <summary>
        /// Closes this stream and the underlying stream
        /// </summary>
        public override void Close()
        {
            innerReader.Close();
        }
#endif

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override int Depth
        {
            get { return innerReader.Depth; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool EOF
        {
            get { return innerReader.EOF; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string GetAttribute(int i)
        {
            string attributeValue = innerReader.GetAttribute(i);
            return attributeValue;
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string GetAttribute(string name, string namespaceURI)
        {
            string attributeData = null;
            if (!String.IsNullOrEmpty(namespaceURI) && !fakeAttributes.TryGetValue(name, out attributeData))
                attributeData = innerReader.GetAttribute(name, namespaceURI);
            return attributeData;
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string GetAttribute(string name)
        {
            string attributeData = null;
            if(!fakeAttributes.TryGetValue(name, out attributeData))
                attributeData = innerReader.GetAttribute(name);
            return attributeData;
        }

        /// <summary>
        /// Remove attribute
        /// </summary>
        public void RemoveAttribute(string name)
        {
            fakeAttributes.Add(name, null);
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool HasValue
        {
            get { return innerReader.HasValue; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool IsEmptyElement
        {
            get { return innerReader.IsEmptyElement; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string LocalName
        {
            get { return innerReader.LocalName; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string LookupNamespace(string prefix)
        {
            return innerReader.LookupNamespace(prefix);
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool MoveToAttribute(string name, string ns)
        {
            return innerReader.MoveToAttribute(name, ns);
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool MoveToAttribute(string name)
        {
            return innerReader.MoveToAttribute(name);
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool MoveToElement()
        {
            return innerReader.MoveToElement();
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool MoveToFirstAttribute()
        {
            return innerReader.MoveToFirstAttribute();
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool MoveToNextAttribute()
        {
            return innerReader.MoveToNextAttribute();
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override XmlNameTable NameTable
        {
            get { return innerReader.NameTable; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string NamespaceURI
        {
            get { return innerReader.NamespaceURI; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override XmlNodeType NodeType
        {
            get { return innerReader.NodeType; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string Prefix
        {
            get { return innerReader.Prefix; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="T:MARC.Everest.Exceptions.MessageValidationException">When an attempt is made to read a closing element that
        /// does not match the starting element</exception>
        public override bool Read()
        {
            bool retVal = innerReader.Read();
            if(retVal)
                switch (innerReader.NodeType)
                {
                    case XmlNodeType.Element:
                        fakeAttributes.Clear();  // clear fake attributes
                        if(!innerReader.IsEmptyElement) // push only if the element isn't empty
                            this.nameStack.Push(new XmlQualifiedName(innerReader.LocalName, innerReader.NamespaceURI));
                        break;
                    case XmlNodeType.EndElement:
                        XmlQualifiedName openName = this.nameStack.Pop(),
                            thisName = new XmlQualifiedName(innerReader.LocalName, innerReader.NamespaceURI);
                        if(thisName.Name != openName.Name)
                            throw new MARC.Everest.Exceptions.MessageValidationException(String.Format(CultureInfo.InvariantCulture, "XML Processing Error : Start element {0} does not match expected end element {1}", openName.Name, thisName.Name));

                        break;
                }
            return retVal;
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override bool ReadAttributeValue()
        {
            return innerReader.ReadAttributeValue();
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override ReadState ReadState
        {
            get { return innerReader.ReadState; }
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override void ResolveEntity()
        {
            innerReader.ResolveEntity();
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string Value
        {
            get { return innerReader.Value; }
        }

        #region IXmlNamespaceResolver Members

        /// <summary>
        /// Get namespaces in the specified scope
        /// </summary>
        public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
        {
            return (this.innerReader as IXmlNamespaceResolver).GetNamespacesInScope(scope);
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public string LookupPrefix(string namespaceName)
        {
            return (this.innerReader as IXmlNamespaceResolver).LookupPrefix(namespaceName);
        }

        #endregion
    }
}
