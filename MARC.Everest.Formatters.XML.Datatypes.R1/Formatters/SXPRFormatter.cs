using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using System.Reflection;
using System.Collections;
using MARC.Everest.Xml;
using MARC.Everest.Interfaces;
using MARC.Everest.Exceptions;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Attributes;

namespace MARC.Everest.Formatters.XML.Datatypes.R1.Formatters
{
    /// <summary>
    /// SXPR Formatter helper
    /// </summary>
    public class SXPRFormatter : ANYFormatter
    {
        #region IDatatypeFormatter Members


        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        public override void Graph(System.Xml.XmlWriter s, object o, DatatypeFormatterGraphResult result)
        {
            Type sxprType = typeof(SXPR<>);
            Type sxprGenericType = sxprType.MakeGenericType(GenericArguments);

            // Format the base type
            base.Graph(s, o, result);

            // Was a nullflavor present
            if ((o as ANY).NullFlavor != null)
                return;

            // Current path
            string currentPath = s is XmlStateWriter ? (s as XmlStateWriter).CurrentPath : "NA";

            // Graph the operator
            PropertyInfo operatorProperty = sxprGenericType.GetProperty("Operator"),
                componentProperty = sxprGenericType.GetProperty("Terms");
            SetOperator? operatorValue = (SetOperator?)operatorProperty.GetValue(o, null);
            IEnumerable componentValue = (IEnumerable)componentProperty.GetValue(o, null);

            // Write the operator out
            if (operatorValue != null)
                s.WriteAttributeString("operator", Util.ToWireFormat(operatorValue));

            // Elements
            if (componentValue != null)
            {
                int count = 0;
                foreach (var component in componentValue)
                {
                    s.WriteStartElement("comp", null);
                    object value = component;
                    var compType = component.GetType();

                    string xsiTypeName = Util.CreateXSITypeName(compType);

                    // Write the type
                    if (this.Host.Host == null)
                        s.WriteAttributeString("type", DatatypeFormatter.NS_XSI, xsiTypeName.ToString());
                    else
                        s.WriteAttributeString("xsi", "type", DatatypeFormatter.NS_XSI, xsiTypeName.ToString());

                    if (count == 0) // the first element should have no operator
                    {
                        var pi = compType.GetProperty("Operator");
                        if (pi.GetValue(component, null) != null)
                            result.AddResultDetail(new ResultDetail(ResultDetailType.Warning, "Operator won't be represented in the first object in the SXPR", s.ToString(), null));
                        pi.SetValue(component, null, null);
                    }

                    var hostGraphResult = Host.Graph(s, (IGraphable)value);
                    result.AddResultDetail(hostGraphResult.Details);

                    s.WriteEndElement(); // comp
                    count++; 
                }
            }
        }

        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public override object Parse(System.Xml.XmlReader s, DatatypeFormatterParseResult result)
        {
            // Create the types
            Type sxprType = typeof(SXPR<>);
            Type sxprGenericType = sxprType.MakeGenericType(GenericArguments);

            // Details

            // Create an instance of rto from the rtoType
            object instance = sxprGenericType.GetConstructor(Type.EmptyTypes).Invoke(null);

            if (s.GetAttribute("nullFlavor") != null)
                ((ANY)instance).NullFlavor = (NullFlavor)Util.FromWireFormat(s.GetAttribute("nullFlavor"), typeof(NullFlavor));
                // Try get operator 
                if (s.GetAttribute("operator") != null)
                    sxprGenericType.GetProperty("Operator").SetValue(instance, Util.FromWireFormat(s.GetAttribute("operator"), typeof(SetOperator?)), null);
                if (s.GetAttribute("specializationType") != null)
                {
                    if (result.CompatibilityMode == DatatypeFormatterCompatibilityMode.Canadian)
                        sxprGenericType.GetProperty("Flavor").SetValue(instance, s.GetAttribute("specializationType"), null);
                    else
                        result.AddResultDetail(new UnsupportedDatatypeR1PropertyResultDetail(ResultDetailType.Warning, "Flavor", "SXPR", s.ToString()));
                }
                #region Element Processing 

                // List of components
                LIST<IGraphable> componentList = new LIST<IGraphable>();

                // Parse elements
                if (!s.IsEmptyElement)
                {

                    int sDepth = s.Depth;
                    string sName = s.Name;

                    s.Read();
                    // string Name
                    while (!(s.NodeType == System.Xml.XmlNodeType.EndElement && s.Depth == sDepth && s.Name == sName))
                    {
                        string oldName = s.Name; // Name
                        try
                        {

                            if (s.LocalName == "comp") // component
                            {
                                // Now determine the type of GTS
                                //string typeName = s.GetAttribute("type", DatatypeFormatter.NS_XSI);
                                //IDatatypeFormatter formatter = DatatypeFormatter.GetFormatter(typeName);

                                //if (formatter == null)
                                //{
                                //    result.AddResultDetail(new ResultDetail(String.Format("Cannot parse a SXPR member of type '{0}'", typeName)));
                                //    return null;
                                //}
                                //else
                                //{
                                //    // Graph the Hull
                                //    formatter.Host = this.Host;
                                //    formatter.GenericArguments = GenericArguments;
                                //    componentList.Add(formatter.Parse(s, result) as IGraphable);
                                //}
                                var hostResult = this.Host.Parse(s, GenericArguments[0]);
                                result.Code = hostResult.Code;
                                result.AddResultDetail(hostResult.Details);
                                componentList.Add(hostResult.Structure as IGraphable);
                            }
                        }
                        catch (MessageValidationException e) // Message validation error
                        {
                            result.AddResultDetail(new MARC.Everest.Connectors.ResultDetail(MARC.Everest.Connectors.ResultDetailType.Error, e.Message, e));
                        }
                        finally
                        {
                            if (s.Name == oldName) s.Read();
                        }
                    }
                }

                ((IListContainer)instance).ContainedList = componentList;
                #endregion

            // Validate
            ANYFormatter validation = new ANYFormatter();
            string pathName = s is XmlStateReader ? (s as XmlStateReader).CurrentPath : s.Name;
            validation.Validate(instance as ANY, pathName, result);

            return instance;
        }

        /// <summary>
        /// Gets the type that this formatter handles
        /// </summary>
        public override string HandlesType
        {
            get { return "SXPR"; }
        }

        /// <summary>
        /// Get the supported properties for the rendering
        /// </summary>
        public override List<PropertyInfo> GetSupportedProperties()
        {
            List<PropertyInfo> retVal = new List<PropertyInfo>(2);
            retVal.Add(typeof(SXPR<>).GetProperty("Operator"));
            retVal.Add(typeof(SXPR<>).GetProperty("Terms"));
            retVal.AddRange(base.GetSupportedProperties());
            return retVal;

        }
        #endregion
    }
}
