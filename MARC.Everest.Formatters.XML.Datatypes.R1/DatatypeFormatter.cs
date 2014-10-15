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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Interfaces;
using System.Xml;
using MARC.Everest.Formatters.XML.Datatypes.R1.Formatters;
using System.Reflection;
using MARC.Everest.Attributes;
using System.ComponentModel;
using System.Collections;
using MARC.Everest.Xml;
using System.Globalization;

#if WINDOWS_PHONE
using MARC.Everest.Phone;
#endif

namespace MARC.Everest.Formatters.XML.Datatypes.R1
{

    /// <summary>
    /// A linked class to <see cref="T:MARC.Everest.Formatters.XML.Datatypes.R1.DatatypeFormatter"/> that
    /// exists to retain backwards compatibility with pre 1.0 versions of Everest
    /// </summary>
    [Obsolete("Use DatatypeFormatter instead", false)]
    public sealed class Formatter : DatatypeFormatter
    {
    }

    /// <summary>
    /// Represents a data type formatter that already has the compatibility mode set to Canadian
    /// </summary>
    /// <remarks>
    /// Setting compatibility to Canadian will have several effects on the formatting of data types. This includes the 
    /// emitting of a specializationType attribute (in place of Flavor)
    /// </remarks>
    [Description("XML Data Types R1 with Canadian Extensions")]
    public class CanadianDatatypeFormatter : DatatypeFormatter, IXmlStructureFormatter, IDatatypeStructureFormatter
    {
        /// <summary>
        /// Creates a new instance of the Canadian datatype formatter
        /// </summary>
        public CanadianDatatypeFormatter()
            : base(DatatypeFormatterCompatibilityMode.Canadian)
        {
        }
    }

    /// <summary>
    /// Represents a data type formatter that is set to emulate Data Types used with CDAr2
    /// </summary>
    /// <remarks>
    /// Using this formatter will set the compatibility of the formatter to ClinicalDocumentArchitecture which
    /// means that some features of data types will not be represented on the wire.
    /// </remarks>
    [Description("CDA Data Types R1")]
    public class ClinicalDocumentDatatypeFormatter : DatatypeFormatter, IXmlStructureFormatter, IDatatypeStructureFormatter
    {
        /// <summary>
        /// Creates a new instance of the CDA datatype formatter
        /// </summary>
        public ClinicalDocumentDatatypeFormatter()
            : base(DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture)
        {
        }
    }

    /// <summary>
    /// Represents a formatter that can be used as a graph aide for the purpose of
    /// formatting data types instances to the XML Datatypes R1 specification.
    /// </summary>
    [Description("XML Data Types R1")]
    public class DatatypeFormatter : IXmlStructureFormatter, IDatatypeStructureFormatter, IValidatingStructureFormatter
    {

        // Cache of the formatters
        private static Dictionary<string,Type> structureFormatters = new Dictionary<string,Type>(100);
        private static Dictionary<Type, Type> formatters = new Dictionary<Type, Type>(100);
        private static Object s_lockObject = new Object();

        // Cache of the type names that formatters use
        private static List<string> formatterNames = new List<string>(20);
        // Unsupported types
        private static Type[] s_unsupportedNames = new Type[] {
            typeof(COLL<>),
            typeof(QSET<>)
        };


        /// <summary>
        /// The formatter culture
        /// </summary>
        internal static CultureInfo FormatterCulture { get; private set; }

        /// <summary>
        /// XSI
        /// </summary>
        internal const string NS_XSI = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// Gets or sets a property specifying the compatibility of the data types formatter
        /// </summary>
        /// <remarks>
        /// Specifies the compatibility of the data types formatter as supporting the Canadian
        /// extensions to data types R1 or the 
        /// </remarks>
        public DatatypeFormatterCompatibilityMode CompatibilityMode { get; set; }


        /// <summary>
        /// Scan this assembly for datatype formatters
        /// </summary>
        public DatatypeFormatter()
        {
            DatatypeFormatter.FormatterCulture = CultureInfo.InvariantCulture;
            this.CompatibilityMode = DatatypeFormatterCompatibilityMode.Universal;
            this.ValidateConformance = true;
            if (structureFormatters.Count == 0)
            {
                lock (structureFormatters)
                {
                    if (structureFormatters.Count > 0) return; // check again once we have a lock
                    Assembly a = typeof(DatatypeFormatter).Assembly;
                    foreach (Type t in a.GetTypes()) // Get all types
                        if (t.GetInterface("MARC.Everest.Formatters.XML.Datatypes.R1.Formatters.IDatatypeFormatter", false) != null)
                        {
                            IDatatypeFormatter fmtr = (IDatatypeFormatter)a.CreateInstance(t.FullName);
                            if (!structureFormatters.ContainsKey(fmtr.HandlesType))
                                structureFormatters.Add(fmtr.HandlesType, t);
                            if (!formatterNames.Contains(fmtr.HandlesType))
                                formatterNames.Add(fmtr.HandlesType);
                        }
                }
            }
        }

        /// <summary>
        /// Creates a new instance of the data type formatter with the specified <paramref name="compatibilityMode"/>
        /// </summary>
        public DatatypeFormatter(DatatypeFormatterCompatibilityMode compatibilityMode)
            : this()
        {
            this.CompatibilityMode = compatibilityMode;
        }

        #region IStructuredFormatter Members

        /// <summary>
        /// Get or set graph hints for deserialization
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public Type[] GraphHint { get; set; }

        /// <summary>
        /// Get the datatypes that this formatter can handle
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<string> HandleStructure
        {
            get
            {
                return formatterNames;
            }
        }

        /// <summary>
        /// Get formatter
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static IDatatypeFormatter GetFormatter(Type type)
        {
            // Get the struct attribute
            IDatatypeFormatter formatter = null;
            Type cType = type;
            Type formatterType = null;

            // Get a cached formatter (try to)
            if (!formatters.TryGetValue(type, out formatterType))
            {
                // Find a structure attribute to format... 
                while (formatterType == null)
                {
                    StructureAttribute sta = cType.GetCustomAttributes(typeof(StructureAttribute), true)[0] as StructureAttribute;

                    formatterType = null;
                    if (structureFormatters.TryGetValue(sta.Name ?? "", out formatterType))
                        break;

                    // Swap cType
                    cType = cType.BaseType;
                    if (cType == null) return null; // Not available
                }
            }

            // Instantiate
            if (formatterType != null)
            {
                formatter = (IDatatypeFormatter)formatterType.Assembly.CreateInstance(formatterType.FullName);
                lock (s_lockObject) // Add to cache
                    if (!formatters.ContainsKey(type))
                        formatters.Add(type, formatterType);
            }

            // Populate the generic arguments
            formatter.GenericArguments = type.GetGenericArguments();
           
            return formatter;
        }


        /// <summary>
        /// Get formatter from xsi type
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        //internal static IDatatypeFormatter GetFormatter(string xsiType)
        //{

        //    // Formatter type
        //    Type formatterType = null;
        //    string xsiTypeRoot = xsiType;

        //    // Generic 
        //    if (!String.IsNullOrEmpty(xsiTypeRoot) && xsiTypeRoot.Contains("_"))
        //        xsiTypeRoot = xsiTypeRoot.Substring(0, xsiTypeRoot.IndexOf("_"));

        //    // Get the type
        //    if (String.IsNullOrEmpty(xsiType) ||
        //        !formatters.TryGetValue(xsiTypeRoot, out formatterType))
        //        return null;
        //    IDatatypeFormatter retVal = (IDatatypeFormatter)formatterType.Assembly.CreateInstance(formatterType.FullName);

        //    // Was there generic parameters?
        //    List<Type> genArgs = new List<Type>();
        //    string[] genParms = xsiType.Split('_');
        //    for (int i = 1; i < genParms.Length; i++)
        //    {
        //        // Find the structure that is defined by the type
        //        var genArg = Array.Find<Type>(typeof(PQ).Assembly.GetTypes(), o => o.GetCustomAttributes(typeof(StructureAttribute), false).Length > 0 &&
        //            (o.GetCustomAttributes(typeof(StructureAttribute), false)[0] as StructureAttribute).Name == genParms[i]); // find the type that is described by the xsiType

        //        if(genArg != null)
        //            genArgs.Add(genArg);
        //    }
            

        //    // Assign the generic arguments
        //    if (genArgs.Count > 0)
        //        retVal.GenericArguments = genArgs.ToArray();
        //    return retVal;
        //}

        /// <summary>
        /// Host of this structure formatter
        /// </summary>
        public IStructureFormatter Host { get; set; }

        /// <summary>
        /// clone an instnace of this object
        /// </summary>
        public Object Clone()
        {
            DatatypeFormatter retVal = (DatatypeFormatter)this.MemberwiseClone();
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
                return new DatatypeFormatterGraphResult(this.CompatibilityMode, ResultCode.Accepted, null, this.ValidateConformance);

            try
            {
                IDatatypeFormatter formatter = GetFormatter(o.GetType());

                if (formatter == null)
                    return new DatatypeFormatterGraphResult(this.CompatibilityMode, ResultCode.NotAvailable, new IResultDetail[] {
                        new NotImplementedResultDetail(ResultDetailType.Error, String.Format("Could not find formatter for '{0}'", o.GetType().FullName), null)
                    }, this.ValidateConformance);

                // Set the host
                formatter.Host = (IXmlStructureFormatter)(this.Host ?? this);

                var result = new DatatypeFormatterGraphResult(this.CompatibilityMode, ResultCode.Accepted, null, this.ValidateConformance);

                formatter.Graph(s, o, result);

                return result;
            }
            catch (Exception e)
            {
                return new DatatypeFormatterGraphResult(this.CompatibilityMode, ResultCode.Error, new IResultDetail[] {
                    new ResultDetail(ResultDetailType.Error, e.Message, s.ToString(), e)
                }, this.ValidateConformance);
            }
        }

        /// <summary>
        /// Parse an object from <paramref name="r"/>
        /// </summary>
        public IFormatterParseResult Parse(XmlReader r)
        {
            return new DatatypeFormatterParseResult(this.CompatibilityMode, ResultCode.Rejected,
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

            // Don't check for XSI type if the type is a GTS or iterable
            // This is because the XSI type on these classes will mislead the formatter selector
            if (t == typeof(GTS) || t.GetInterface(typeof(IEnumerable).FullName, false) != null && !t.IsAbstract)
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
            DatatypeFormatterParseResult result = new DatatypeFormatterParseResult(this.CompatibilityMode, ResultCode.Accepted, null, this.ValidateConformance);
            formatter.Host = (IXmlStructureFormatter)(this.Host ?? this);

            
#if WINDOWS_PHONE
            bool hasErrors = s_unsupportedNames.Exists(o => (t.IsGenericType ? t.GetGenericTypeDefinition() : t).Equals(o));
#else
            bool hasErrors = Array.Exists(s_unsupportedNames, o => (t.IsGenericType ? t.GetGenericTypeDefinition() : t).Equals(o));
#endif
            // Errors
            if (hasErrors)
            {
                var structAtt = t.GetCustomAttributes(typeof(StructureAttribute), false);
                if (structAtt.Length > 0)
                    result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(
                        ResultDetailType.Warning,
                        "All",
                        (structAtt[0] as StructureAttribute).Name,
                        r.ToString()
                    ));
            }
            // Structure graph
            result.Structure = formatter.Parse(r, result) as IGraphable;
            
            return result;
        }

        #endregion

        #region IStructureFormatter Members

        /// <summary>
        /// Graph <paramref name="o"/> onto <paramref name="s"/>
        /// </summary>
        public IFormatterGraphResult Graph(System.IO.Stream s, IGraphable o)
        {
            return new DatatypeFormatterGraphResult(this.CompatibilityMode, ResultCode.Rejected, new IResultDetail[] {
                new NotImplementedResultDetail(ResultDetailType.Error, "Can't use the datatypes R1 formatter on a stream", null)
            }, this.ValidateConformance);
        }

        /// <summary>
        /// Parse a structure from <paramref name="s"/>
        /// </summary>
        public IFormatterParseResult Parse(System.IO.Stream s)
        {
            return new DatatypeFormatterParseResult(this.CompatibilityMode, ResultCode.Rejected, new IResultDetail[] {
                new NotImplementedResultDetail(ResultDetailType.Error, "Can't use the datatypes R1 formatter on a stream", null)
            }, this.ValidateConformance);
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

        #region IStructureFormatter Members

        /// <summary>
        /// Gets or sets the aides on this formatter
        /// </summary>
        public List<IStructureFormatter> GraphAides
        {
            get;
            set;
        }

        #endregion

        #region IValidatingStructureFormatter Members

        /// <summary>
        /// Validate conformance
        /// </summary>
        public bool ValidateConformance { get; set; }

        #endregion


        #region IDisposable Members

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            // No special dispose logic required
            return;
        }

        #endregion
    }
}