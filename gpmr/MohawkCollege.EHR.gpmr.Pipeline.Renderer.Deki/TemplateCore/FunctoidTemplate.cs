using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Reflection;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore
{
    /// <summary>
    /// Functoid template
    /// </summary>
    [XmlType(TypeName = "FunctoidTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
    [Serializable]
    public class FunctoidTemplate : NamedTemplate
    {

        /// <summary>
        /// Gets the member that represents the function in the
        /// .NET assembly
        /// </summary>
        [XmlAttribute("invoke")]
        public string MethodName { get; set; }

        /// <summary>
        /// The fully qualified name of the type
        /// </summary>
        [XmlAttribute("type")]
        public string ContainerType { get; set; }

        /// <summary>
        /// True when escaped
        /// </summary>
        [XmlAttribute("escaped")]
        public bool Escaped { get; set; }

        /// <summary>
        /// Execute the method and return result
        /// </summary>
        public object ExecuteMethod(object param)
        {
            // Get type
            Type rType = Type.GetType(this.ContainerType);
            if (rType == null)
                return String.Format("Could not find type '{0}'", this.ContainerType);

            // Get method
            var mi = rType.GetMethod(this.MethodName);
            if (mi == null)
                return String.Format("Could not find method '{0}' in '{1}'", this.MethodName, rType.FullName);

            // Invoke
            try
            {
                return mi.Invoke(null, new object[] { param });
            }
            catch (TargetInvocationException e)
            {
                return String.Format(@"<span style=""color:red"">Functoid Error: {0}</span>", (e.InnerException ?? e).Message);
            }
            catch (Exception e)
            {
                return String.Format(@"<span style=""color:red"">Error: {0}</span>", (e.InnerException ?? e).Message);

            }
        }

        /// <summary>
        /// Fill in template details
        /// </summary>
        public override string FillTemplate()
        {
            // Get the value of this item
            object value = ExecuteMethod(this.Context);

            if (value == null)
                return "";

            FeatureTemplate ftpl = NonParameterizedTemplate.Spawn(Parent.FindTemplate(value.GetType().FullName, value) as NonParameterizedTemplate, Parent, value) as FeatureTemplate;

            // Current template fields
            string[][] templateFields = new string[][] 
            {
                new string[] { "$feature$", ftpl == null ? "" : ftpl.FillTemplate()},
                new string[] { "$date$", DateTime.Now.ToString("yyyy-MM-dd") },
                new string[] { "$time$", DateTime.Now.ToString("HH:mm:ss") },
                new string[] { "$user$", SystemInformation.UserName },
                new string[] { "$value$", this.Escaped ? value.ToString().Replace(">", "&gt;").Replace("<","&lt;").Replace("$", "&#036;").Replace("@", "&#064;").Replace("^", "&#094;") : value.ToString().Replace("$", "&#036;").Replace("@", "&#064;").Replace("^", "&#094;").Replace("\\","\\\\") }, // Clean Template parameters from TOSTRING()
                new string[] { "$$", "&#036;" }, 
                new string[] { "@@", "&#064;" },
                new string[] { "^^", "&#094;" },
                new string[] { "$version$", Assembly.GetEntryAssembly().GetName().Version.ToString() },
                new string[] { "$typeName$", Context == null ? "" : Context.GetType().Name }
            };

            // Output
            string output = Content.Clone() as string;

            foreach (string[] kv in templateFields)
                output = output.Replace(kv[0], kv[1]);

            return output;
        }
    }
}
