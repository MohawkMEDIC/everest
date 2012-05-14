/* 
 * Copyright 2011 Mohawk College of Applied Arts and Technology
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
 * Date: 06-17-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using System.Reflection;
using MARC.Everest.Attributes;
using MARC.Everest.Interfaces;
using System.Xml;
using MARC.Everest.DataTypes;
using System.Collections;

namespace MARC.Everest.Formatters.XML.Datatypes.R2
{
    /// <summary>
    /// Formatter for DataTypes R2
    /// </summary>
    /// <remarks>
    /// This formatter should be used when systems are utilizing the ISO 21090
    /// datatypes harmonization for HL7v3
    /// </remarks>
    public class DatatypeR2Formatter : IDatatypeStructureFormatter, IXmlStructureFormatter, IValidatingStructureFormatter
    {

        // The names of the structures that this formatter can serialize
        private static List<String> m_formatterNames = new List<string>(10);
        // A reference to all formatters that this formatter uses to actually serialize messages
        private static Dictionary<String, Type> m_formatters = new Dictionary<string,Type>(10);
        // Synchronization root
        private static object m_syncRoot = new object();
        // XSI Namespace
        internal const string NS_XSI = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// Creates a new instance of the data type r2 formatter
        /// </summary>
        public DatatypeR2Formatter()
        {
            this.ValidateConformance = true;
        }

        /// <summary>
        /// Statuc ctor for the Formatter class
        /// </summary>
        static DatatypeR2Formatter()
        {
            lock (m_syncRoot)
            {
                if (m_formatters.Count > 0) return; // check again once we have a lock
                Assembly a = typeof(DatatypeR2Formatter).Assembly;
                foreach (Type t in a.GetTypes()) // Get all types
                    if (t.GetInterface("MARC.Everest.Formatters.XML.Datatypes.R2.IDatatypeFormatter") != null)
                    {
                        IDatatypeFormatter fmtr = (IDatatypeFormatter)a.CreateInstance(t.FullName);
                        if (!m_formatters.ContainsKey(fmtr.HandlesType))
                            m_formatters.Add(fmtr.HandlesType, t);
                        if (!m_formatterNames.Contains(fmtr.HandlesType))
                            m_formatterNames.Add(fmtr.HandlesType);
                    }
            }
        }

        #region IXmlStructureFormatter Members

        /// <summary>
        /// Get the datatype formatter for the specified type
        /// </summary>
        private IDatatypeFormatter GetFormatter(Type type)
        {
            // Get the struct attribute
            IDatatypeFormatter formatter = null;
            Type cType = type;

            // Find a structure attribute to format... 
            while (formatter == null)
            {
                StructureAttribute sta = cType.GetCustomAttributes(typeof(StructureAttribute), true)[0] as StructureAttribute;

                // Find the object that we want to render
                Type formatterType = null;
                if (m_formatters.TryGetValue(sta.Name ?? "", out formatterType))
                    formatter = (IDatatypeFormatter)formatterType.Assembly.CreateInstance(formatterType.FullName);

                // Swap cType
                cType = cType.BaseType;
                if (cType == null) return null; // Not available
            }

            // Populate the generic arguments
            formatter.GenericArguments = type.GetGenericArguments();

            return formatter;
        }

        /// <summary>
        /// Get a formatter based on the XSI type name
        /// </summary>
        internal IDatatypeFormatter GetFormatter(string xsiType)
        {

            // Formatter type
            Type formatterType = null;


            // Generic 
            if (!String.IsNullOrEmpty(xsiType) && xsiType.Contains("_"))
                xsiType = xsiType.Substring(0, xsiType.IndexOf("_"));

            // Get the type
            if (!String.IsNullOrEmpty(xsiType) &&
                m_formatters.TryGetValue(xsiType, out formatterType))
                return (IDatatypeFormatter)formatterType.Assembly.CreateInstance(formatterType.FullName);
            return null;
        }

        #endregion

        #region IStructureFormatter Members

        /// <summary>
        /// Gets or sets the list of graph aides that this formatter can employ
        /// </summary>
        public List<IStructureFormatter> GraphAides { get; set; }

        /// <summary>
        /// Gets or sets the host structure formatter, that is the formatter to which this formatter belongs
        /// </summary>
        public IStructureFormatter Host { get; set; }

        /// <summary>
        /// Gets a list of structure types that this formatter can "handle"
        /// </summary>
        public List<string> HandleStructure
        {
            get { return m_formatterNames; }
        }


        #endregion

        #region ICloneable Members

        /// <summary>
        /// Clone this formatter
        /// </summary>
        public object Clone()
        {
            DatatypeR2Formatter retVal = (DatatypeR2Formatter)this.MemberwiseClone();
            return retVal;
        }

        #endregion

        #region IXmlStructureFormatter Members


        /// <summary>
        /// Graphs object <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The object to graph</param>
        /// <returns>A formatter graphing result</returns>
        public IFormatterGraphResult Graph(XmlWriter s, IGraphable o)
        {
            if (o == null)
                return new DatatypeR2FormatterGraphResult(ResultCode.Accepted, null, this.ValidateConformance);


            try
            {
                IDatatypeFormatter formatter = GetFormatter(o.GetType());

                if (formatter == null)
                    return new DatatypeR2FormatterGraphResult(ResultCode.NotAvailable, new IResultDetail[] {
                        new NotImplementedResultDetail(ResultDetailType.Error, String.Format("Could not find formatter for '{0}'", o.GetType().FullName), null)
                    }, this.ValidateConformance);

                // Set the host
                formatter.Host = (IXmlStructureFormatter)(this.Host ?? this);

                var result = new DatatypeR2FormatterGraphResult(ResultCode.Accepted, null, this.ValidateConformance);

                formatter.Graph(s, o, result);

                return result;
            }
            catch (Exception e)
            {
                return new DatatypeR2FormatterGraphResult(ResultCode.Error, new IResultDetail[] {
                    new ResultDetail(ResultDetailType.Error, e.Message, s.ToString(), e)
                }, this.ValidateConformance);
            }
        }

        /// <summary>
        /// Parse an object from <paramref name="r"/>
        /// </summary>
        public IFormatterParseResult Parse(XmlReader r)
        {
            return new DatatypeR2FormatterParseResult(ResultCode.Rejected,
                new IResultDetail[] { new NotImplementedResultDetail(ResultDetailType.Error, "Must supply a type hint to this formatter", null) }, this.ValidateConformance);

        }

        /// <summary>
        /// Parse an object from <paramref name="r"/> using the type hint <paramref name="t"/>
        /// </summary>
        /// <param name="r">The reader to read from</param>
        /// <param name="t">The type hint for the formatter</param>
        /// <returns>A structured result</returns>
        public IFormatterParseResult Parse(XmlReader r, Type t)
        {
            // Get the struct attribute
            IDatatypeFormatter formatter = null;
            Type cType = t;


            //if (formatter != null && t != typeof(GTS) && formatter.GenericArguments == null )
            //    formatter.GenericArguments = t.GetGenericArguments();
            if (t.GetInterface(typeof(IEnumerable).FullName) != null && !t.IsAbstract)
                formatter = GetFormatter(t);
            else
            {
                // Force processing as an XSI:Type
                if (r.GetAttribute("type", NS_XSI) != null)
                    cType = Util.ParseXSITypeName(r.GetAttribute("type", NS_XSI));

                formatter = GetFormatter(cType);
            }

            if (formatter == null)
                return null;

            // Set host and parse
            DatatypeR2FormatterParseResult result = new DatatypeR2FormatterParseResult(ResultCode.Accepted, null, this.ValidateConformance);
            formatter.Host = (IXmlStructureFormatter)(this.Host ?? this);
            
            // Structure graph
            var structure = formatter.Parse(r, result);
            object retVal = null;
            if (!Util.TryFromWireFormat(structure, cType, out retVal))
                retVal = structure as IGraphable; //, cType) as IGraphable;
            
            // Set the return structure
            result.Structure = retVal as IGraphable;

            return result; 
        }

        #endregion

        #region IStructureFormatter Members


        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public IFormatterGraphResult Graph(System.IO.Stream s, IGraphable o)
        {
            return new DatatypeR2FormatterGraphResult(ResultCode.Rejected, new IResultDetail[] {
                new NotImplementedResultDetail(ResultDetailType.Error, "Can't use the datatypes R1 formatter on a stream", null)
            }, this.ValidateConformance);
        }

        /// <summary>
        /// Parse a structure from <paramref name="s"/>
        /// </summary>
        public IFormatterParseResult Parse(System.IO.Stream s)
        {
            return new DatatypeR2FormatterParseResult(ResultCode.Rejected, null, this.ValidateConformance);
        }

        #endregion

        #region IDatatypeStructureFormatter Members

        /// <summary>
        /// Get the supported properties for the type
        /// </summary>
        public PropertyInfo[] GetSupportedProperties(Type dataType)
        {
            // Get the formatter helper
            var fmtr = GetFormatter(dataType);
            if (fmtr == null)
                return dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return fmtr.GetSupportedProperties().ToArray();
        }

        #endregion

        /// <summary>
        /// Copy base attributes from one ANY to another
        /// </summary>
        internal static void CopyBaseAttributes(ANY source, ANY dest)
        {
            dest.ControlActExt = source.ControlActExt;
            dest.ControlActRoot = source.ControlActRoot;
            dest.Flavor = source.Flavor;
            dest.NullFlavor = source.NullFlavor;
            dest.UpdateMode = source.UpdateMode;
            dest.ValidTimeHigh = source.ValidTimeHigh;
            dest.ValidTimeLow = source.ValidTimeLow;
        }

        #region IValidatingStructureFormatter Members

        /// <summary>
        /// When true, instructs the formatter to conduct validation
        /// </summary>
        /// <remarks>This is default to true</remarks>
        public bool ValidateConformance
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Create the XSI type name, ignoring some of the generic types that don't 
        /// emit the _
        /// </summary>
        internal static string CreateXSITypeName(Type type)
        {
            // Get the generic type definition
            var tc = type;
            if (tc.IsGenericType)
                tc = tc.GetGenericTypeDefinition();

            // If the type is RTO then only emit RTO
            // HACK: This is done because RTO is not generic in the R2 XSD
            if (tc == typeof(RTO<,>))
                return "RTO";
            else if (tc == typeof(SET<>)) // HACK: In R2, a SET is actually known as a DSET
                return String.Format("D{0}", Util.CreateXSITypeName(type, CreateXSITypeName));
            else if (tc == typeof(ON) || tc == typeof(PN) || tc == typeof(TN))
                return "EN";
            else
                return Util.CreateXSITypeName(type);
        }

        #region IDisposable Members

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            // No special dispose logic required
            return;
        }

        #endregion
    }
}
