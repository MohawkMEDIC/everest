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
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Interfaces;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Attributes;
using System.IO;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.RimbaCS.Renderer
{
    /// <summary>
    /// Renders a sub-system from COR to C#
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderer"), FeatureRenderer(Feature = typeof(MohawkCollege.EHR.gpmr.COR.SubSystem), IsFile = false)]
    public class SubsystemRenderer : IFeatureRenderer
    {
        #region IFeatureRenderer Members

        /// TODO: Explanation of parameters missing: OwnerNS, apiNs, f and tw
        ///       Summary explanation needed
        public void Render(String OwnerNS, String apiNs, MohawkCollege.EHR.gpmr.COR.Feature f, System.IO.TextWriter tw)
        {
            return; // no file is created
        }

        /// TODO: Explanation of parameters missing: f and FilePath
        ///       Description of the value returned missing
        /// <summary>
        /// This actually creates a directory
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public string CreateFile(Feature f, string FilePath)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(FilePath, f.Name));
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