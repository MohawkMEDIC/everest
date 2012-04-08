using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Attributes;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Interfaces;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Renderer
{
    [FeatureRenderer(Feature = typeof(SubSystem), IsFile = false)]
    public class SubsystemRenderer : IFeatureRenderer
    {
        #region IFeatureRenderer Members

        /// TODO: Explanation of parameters missing: OwnerNS, apiNs, f and tw
        ///       Summary explanation needed
        public void Render(String ownerPackage, String apiNs, MohawkCollege.EHR.gpmr.COR.Feature f, System.IO.TextWriter tw)
        {
            return; // no file is created
        }

        /// <summary>
        /// This actually creates a directory
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public string CreateFile(Feature f, string FilePath)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(FilePath, f.Name.ToLower()));
                return Path.Combine(FilePath, f.Name);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Can't create directory '{0}', {1}", Path.Combine(FilePath, f.Name), e.Message), "error");
                throw new DirectoryNotFoundException(e.Message, e);
            }
        }

        #endregion
    }
}
