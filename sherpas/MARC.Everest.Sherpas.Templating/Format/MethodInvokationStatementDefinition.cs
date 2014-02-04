using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Method invokation statement definition
    /// </summary>
    [XmlType("MethodInvokationStatementDefinition", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class MethodInvokationStatementDefinition : AssignmentStatementDefinition
    {

        /// <summary>
        /// Gets or sets the name of the method which this invokation statement executes
        /// </summary>
        [XmlAttribute("method")]
        public String MethodNameRef { get; set; }

        /// <summary>
        /// Gets or sets the parameters to the method invokcation
        /// </summary>
        [XmlElement("param")]
        public List<AssignmentStatementDefinition> Params { get; set; }

    }
}
