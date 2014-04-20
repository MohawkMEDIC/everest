using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a null artifact
    /// </summary>
    [XmlType("NullArtifactTemplate", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class NullArtifactTemplate : ArtifactTemplateBase
    {
        [XmlAttribute("replacementFor")]
        public String ReplacementFor { get; set; }
    }
}
