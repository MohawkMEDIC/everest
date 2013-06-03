/* 
 * Copyright 2012 Mohawk College of Applied Arts and Technology
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
 * Author: Justin Fyfe
 * Date: 02-23-2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using MARC.Everest.Exceptions;

namespace MARC.Everest.Json
{
    /// <summary>
    /// Represents a state writer that can be used for writing JSON structured data
    /// </summary>
    /// <remarks>
    /// This state writer is capable of writing JSON structured data to a stream
    /// using similar methods as those found on the <see cref="T:System.Xml.XmlWriter"/>
    /// class. 
    /// <para>This state writer also keeps track of the current depth at which it is 
    /// currently writing and is used by Everest formatters to report a stack 
    /// trace.</para>
    /// </remarks>
    // The real reason this class was written; JSON makes no sense to me and 
    // I actually find it *harder* to parse than XML, but the agilites see it
    // as being easier to parse, and I guess if you're using JavaScript or
    // other flavor-du-jour toy languages then I guess they're right. 
    // This class is put in here to allow me to write Json crap once
    // and then just use it in a formatter.
    public class JsonStateWriter
    {
        
        /// <summary>
        /// The underlying text writer
        /// </summary>
        private TextWriter m_underlyingWriter;

        /// <summary>
        /// The name stack
        /// </summary>
        private Stack<JsonStackObject> m_nameStack = new Stack<JsonStackObject>();

        /// <summary>
        /// Indent 
        /// </summary>
        private int m_indent = 0;

        /// <summary>
        /// True if the next WriterStartX needs to insert a field seperator
        /// </summary>
        private bool m_needsFieldSeperator = false;

        /// <summary>
        /// Gets or sets the settings of the JsonStateWriter
        /// </summary>
        public JsonStateWriterSettings Settings { get; set; }

        /// <summary>
        /// Creates a new instance of the JSonStateWriter with the specified <paramref name="stream"/>
        /// </summary>
        /// <param name="stream"></param>
        public JsonStateWriter(Stream stream)
        {
            this.m_underlyingWriter = new StreamWriter(stream);
            this.WriteState = System.Xml.WriteState.Start;
            this.Settings = JsonStateWriterSettings.Default;
        }

        /// <summary>
        /// Creates a new instance of the JsonStateWriter with the specified <paramref name="settings"/>
        /// on the specified <paramref name="stream"/>
        /// </summary>
        public JsonStateWriter(Stream stream, JsonStateWriterSettings settings)
            : this(stream)
        {
            this.Settings = settings;
        }

        /// <summary>
        /// Creates a new instance of the JsonStateWriter with the specified <paramref name="writer"/>
        /// </summary>
        public JsonStateWriter(TextWriter writer)
        {
            this.m_underlyingWriter = writer;
            this.WriteState = System.Xml.WriteState.Start;
            this.Settings = JsonStateWriterSettings.Default;
        }

        /// <summary>
        /// Creates a new instance of the JsonStateWriter with the specified <paramref name="writer"/>
        /// and <paramref name="settings"/>
        /// </summary>
        public JsonStateWriter(TextWriter writer, JsonStateWriterSettings settings)
            : this(writer)
        {
            this.Settings = settings;
        }

        /// <summary>
        /// Represents the state writer as a string (current path)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return CurrentPath;
        }

        /// <summary>
        /// Element stack
        /// </summary>
        public Stack<JsonStackObject> ElementStack { get { return this.m_nameStack; } }
        /// <summary>
        /// Get the current element
        /// </summary>
        public JsonStackObject CurrentElement { get { return this.m_nameStack.Peek(); } }

        /// <summary>
        /// Get the current XML path 
        /// </summary>
        public string CurrentPath
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                JsonStackObject[] xqa = this.m_nameStack.ToArray();
                for (int i = xqa.Length - 1; i >= 0; i--)
                    sb.AppendFormat(".{0}", xqa[i].Name);
                return sb.ToString();
            }
        }

        #region Writer Methods

        /// <summary>
        /// Close the JsonStateWriter
        /// </summary>
        public void Close()
        {
            if(this.Settings.CloseOutput && this.m_underlyingWriter != null)
                m_underlyingWriter.Close();
            this.WriteState = System.Xml.WriteState.Closed;
        }

        /// <summary>
        /// Flush the JsonStateWriter
        /// </summary>
        public void Flush()
        {
            if (this.m_underlyingWriter != null)
                this.m_underlyingWriter.Flush();
        }

     
        /// <summary>
        /// Write end attribute
        /// </summary>
        public void WriteEndAttribute()
        {
            ThrowIfClosedOrError();

            // Sanity check, make sure we're not at the end of the element list already
            if (this.m_nameStack.Count == 0)
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException("No open elements");
            }


            // To close an attribute, need to be in an attribute
            if (this.WriteState == System.Xml.WriteState.Attribute)
            {
                //this.m_underlyingWriter.Write("\"");
                this.m_needsFieldSeperator = true;
                this.WriteState = System.Xml.WriteState.Element;
                if (this.m_nameStack.Pop().Type != JsonObjectType.Attribute)
                {
                    this.WriteState = System.Xml.WriteState.Error;
                    throw new JsonException("Expected element type did not match actual element type");
                }
            }
            else
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException(String.Format("Cannot write end attribute when writer is in '{0}' state", this.WriteState));
            }
        }

        /// <summary>
        /// Write the end of an element
        /// </summary>
        public void WriteEndElement()
        {
            WriteEndElement(false);
        }

        /// <summary>
        /// Write the end of an element
        /// </summary>
        private void WriteEndElement(bool internalCall)
        {
            ThrowIfClosedOrError();

            // Sanity check, make sure we're not at the end of the element list already
            if (this.m_nameStack.Count == 0)
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException("No open elements");
            }

            // Write end attribute if needed
            if (this.WriteState == System.Xml.WriteState.Attribute)
                this.WriteEndAttribute();

            // If in content (array) or element (object) then close
            if (this.WriteState == System.Xml.WriteState.Element ||
                this.WriteState == System.Xml.WriteState.Content)
            {

                // Indent turned on
                if (this.Settings.Indent)
                {
                    this.m_underlyingWriter.Write("\r\n");
                    this.m_indent--;
                }

                // Because we behave like XML Serialization we need to keep track if we
                // ended an array
                bool endedArray = false;

                // Are we in an array
                if (this.m_nameStack.Peek().Type == JsonObjectType.Array)
                {
                    this.m_underlyingWriter.Write("{0}]", this.Settings.Indent ? new String(this.Settings.IndentChar, this.m_indent) : string.Empty);
                    endedArray = true;
                }
                else
                    this.m_underlyingWriter.Write("{0}}}", this.Settings.Indent ? new String(this.Settings.IndentChar, this.m_indent) : string.Empty);

                this.m_needsFieldSeperator = true;
                if (this.m_nameStack.Pop().Type == JsonObjectType.Attribute)
                {
                    this.WriteState = System.Xml.WriteState.Error;
                    throw new JsonException("Expected element type did not match actual element type");
                }

                if (this.m_nameStack.Count > 0)
                    this.WriteState = this.m_nameStack.Peek().Type == JsonObjectType.Array ? System.Xml.WriteState.Content : System.Xml.WriteState.Element;
                // Did this end an array?
                if (endedArray && !internalCall)
                    this.WriteEndElement(); // call to end the parent
            }
            else
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException(String.Format("Cannot write end element when writer is in '{0}' state", this.WriteState));
            }
        }

        /// <summary>
        /// Finishes anything in the name stack
        /// </summary>
        public void WriteFullEndDocument()
        {

            ThrowIfClosedOrError();

            // Verify state
            if(this.WriteState != System.Xml.WriteState.Attribute &&
                this.WriteState != System.Xml.WriteState.Content &&
                this.WriteState != System.Xml.WriteState.Element)
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException(String.Format("Cannot write end element when writer is in '{0}' state", this.WriteState));
            }

            if (this.WriteState == System.Xml.WriteState.Attribute)
                this.WriteEndAttribute(); // Finish the attribute
            while (this.m_nameStack.Count > 0)
                this.WriteEndElement();
            
        }


        /// <summary>
        /// Write raw data to the stream
        /// </summary>
        /// <param name="data">The data to be written</param>
        public void WriteRaw(string data)
        {
            ThrowIfClosedOrError();
            if (this.WriteState == System.Xml.WriteState.Attribute)
                this.m_underlyingWriter.Write(data);
            else
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException(String.Format("Cannot write data when writer is in '{0}' state", this.WriteState));
            }
        }

        /// <summary>
        /// Write the start of an attribute
        /// </summary>
        /// <remarks>
        /// This is mimicing the XmlWriter class, technically there are no 
        /// "attributes" in JSON. For our purposes an "attribute" for JSON is
        /// a key where the value can only be an atomic string (example: "key":"value")
        /// </remarks>
        /// <param name="localName">The name of the attribute</param>
        public void WriteStartAttribute(string localName)
        {
            ThrowIfClosedOrError();

            switch (this.WriteState)
            {
                case System.Xml.WriteState.Element: // element is being written
                    this.m_underlyingWriter.Write("{0}{1}{2}\"{3}\" : ",
                        this.m_needsFieldSeperator ? "," : String.Empty, 
                        this.Settings.Indent ? "\r\n" : String.Empty,
                        this.Settings.Indent ? new String(this.Settings.IndentChar, this.m_indent) : String.Empty,
                        localName
                    );
                    this.m_needsFieldSeperator = false;
                    this.WriteState = System.Xml.WriteState.Attribute;
                    this.m_nameStack.Push(new JsonStackObject(localName, JsonObjectType.Attribute));
                    break;
                case System.Xml.WriteState.Attribute: // Attribute content is being written so we need to finish
                    this.WriteEndAttribute(); // Finish the end attribute
                    this.WriteStartAttribute(localName); // Writer the start attribute
                    break;
                case System.Xml.WriteState.Content: // In content, this means that we're writing an attribute after an array... Very odd but permitted in the land of JSON
                    this.WriteEndElement(true);
                    this.WriteStartAttribute(localName);
                    break;
                default:
                    this.WriteState = System.Xml.WriteState.Error;
                    throw new JsonException(String.Format("Cannot write end attribute when writer is in '{0}' state", this.WriteState));
            }
                
        }

        /// <summary>
        /// Write the document start 
        /// </summary>
        public void WriteStartDocument()
        {
            ThrowIfClosedOrError();

            if (this.WriteState == System.Xml.WriteState.Start)
                this.WriteStartElement(null, false);
            else
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException(String.Format("Cannot write start document when writer is in '{0}' state", this.WriteState));
            }
        }


        /// <summary>
        /// Write start element
        /// </summary>
        /// <remarks>
        /// To this method, a JSON element is any complex object that is nested. 
        /// Examples of elements in the JSON world would be : "key":{ ... 
        /// </remarks>
        /// <param name="localName">The name of the JSON element</param>
        public void WriteStartElement(string localName)
        {
            WriteStartElement(localName, false);
        }

        /// <summary>
        /// Write the start of an element. 
        /// </summary>
        /// <remarks>
        /// This method differs from <see cref="M:WriteStartElement(System.String)"/> in that
        /// it allows a developer to specify whether or not the element represents the start of 
        /// an array via <paramref name="isArray"/>
        /// </remarks>
        /// <param name="localName">The name of the JSON element</param>
        /// <param name="isArray">If true, instructs the start element procedure to write a JSON start array as well</param>
        public void WriteStartElement(string localName, bool isArray)
        {
            ThrowIfClosedOrError();

            switch (this.WriteState)
            {
                case System.Xml.WriteState.Element: // Currently in an element, can be an array or 

                    this.m_underlyingWriter.Write("{0}{1}{2}{3}{4}{5}",
                        this.m_needsFieldSeperator ? "," : String.Empty,
                        this.Settings.Indent ? "\r\n" : String.Empty,
                        this.Settings.Indent ? new String(this.Settings.IndentChar, this.m_indent) : String.Empty,
                        String.IsNullOrEmpty(localName) ? String.Empty : "\"" + localName + "\"",
                        String.IsNullOrEmpty(localName) ? String.Empty : ":",
                        isArray ? "[" : "{"
                    );
                    
                    this.m_indent++;
                    this.m_needsFieldSeperator = false;
                    // Special array stuff
                    if(isArray)
                    {
                        this.m_nameStack.Push(new JsonStackObject(localName, JsonObjectType.Array));
                        this.WriteState = System.Xml.WriteState.Content;
                        this.WriteStartElement(null, false);
                    }
                    else
                    {
                        this.m_nameStack.Push(new JsonStackObject(localName, JsonObjectType.Element));
                        this.WriteState = System.Xml.WriteState.Element;
                    }
                    break;
                case System.Xml.WriteState.Content: // current in content of an element (array)
                    // Sanity check, should be in an array
                    var currentElement = this.m_nameStack.Peek();
                    if (currentElement.Type != JsonObjectType.Array)
                    {
                        WriteState = System.Xml.WriteState.Error;
                        throw new JsonException("Expecting to be in a JSON array");
                    }

                    // Ensure the local name matches the name of the array object that we're in or that a local name is not specified
                    if (String.IsNullOrEmpty(localName) || currentElement.Name.Equals(localName))
                    {
                        this.WriteState = System.Xml.WriteState.Element;
                        WriteStartElement(null, false); // Write to the stream 
                    }
                    else // Finish and write
                    {
                        this.WriteEndElement(true);
                        this.WriteStartElement(localName, isArray);
                    }
                    break;
                case System.Xml.WriteState.Attribute: // currently in an attribute
                    this.WriteEndAttribute();
                    this.WriteStartElement(localName, isArray);
                    break;
                case System.Xml.WriteState.Start: // Start
                    this.WriteState = System.Xml.WriteState.Element;
                    WriteStartElement(null, false);
                    break;
                default:
                    this.WriteState = System.Xml.WriteState.Error;
                    throw new JsonException(String.Format("Cannot write start element when writer is in '{0}' state", this.WriteState));
            }
        }

        /// <summary>
        /// Gets the writer state
        /// </summary>
        public WriteState WriteState
        {
            get;
            private set;
        }

        /// <summary>
        /// Write a string to the output
        /// </summary>
        public void WriteString(string text)
        {
            ThrowIfClosedOrError();

            if (this.WriteState == System.Xml.WriteState.Attribute)
                this.m_underlyingWriter.Write("\"{0}\"", text);
            else
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException(String.Format("Cannot write content when writer is in '{0}' state", this.WriteState));
            }
        }

        /// <summary>
        /// Write an attribute string
        /// </summary>
        /// <remarks>
        /// This really just calls <see cref="M:WriteStartAttribute(System.String)"/>, 
        /// <see cref="M:WriteString(System.String)"/> and <see cref="M:WriteEndAttribute()"/>
        /// to produce an element with content: "<paramref name="localName"/>":"<paramref name="value"/>"
        /// </remarks>
        /// <param name="localName">The name of the JSON attribute</param>
        /// <param name="value">The value of the JSON attribute</param>
        /// <seealso cref="M:WriteStartAttribute(System.String)"/>
        /// <seealso cref="M:WriteString(System.String)"/>
        /// <seealso cref="M:WriteEndAttribute()"/>
        public void WriteAttributeString(string localName, string value)
        {
            this.WriteStartAttribute(localName);
            this.WriteString(value);
            this.WriteEndAttribute();
        }

        /// <summary>
        /// Write an element string
        /// </summary>
        /// <remarks>Writes a JSON structure that looks like : "<paramref name="localName"/>":{"":"<paramref name="value"/>"}</remarks>
        /// <param name="localName">The name of the JSON element</param>
        /// <param name="value">The value of the default attribute in the JSON attribute</param>
        public void WriteElementString(string localName, string value)
        {
            this.WriteStartElement(localName, false);
            this.WriteAttributeString(null, value);
            this.WriteEndElement();
        }

        /// <summary>
        /// Throw an exception if the stream is closed
        /// </summary>
        private void ThrowIfClosedOrError()
        {
            if (this.WriteState == System.Xml.WriteState.Closed)
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException("Stream is closed");
            }
            else if (this.WriteState == System.Xml.WriteState.Error)
            {
                this.WriteState = System.Xml.WriteState.Error;
                throw new JsonException("Cannot write to the stream when the writer is in an Error state");
            }
        }

        #endregion

    }
}
