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
 * Date: 07-12-2011
 */
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Interfaces
{
    /// <summary>
    /// Identifies a feature rendering component
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderer")]
    public interface IFeatureRenderer
    {
        /// <summary>
        /// Render feature <paramref name="f"/> into the stream identified by <paramref name="tw"/>
        /// </summary>
        /// <param name="f">The feature to render</param>
        /// <param name="tw">The text writer to write feature to</param>
        /// <param name="ownerPackage">Owner namespace</param>
        /// <param name="apiNs">The target namespace of the generated api</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Owner"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ns"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "tw"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "f")]
        void Render(String ownerPackage, String apiNs, Feature f, TextWriter tw);

        /// <summary>
        /// Called by the controller when the file was / needs to be created
        /// </summary>
        /// <param name="f">The feature to publish</param>
        /// <param name="FilePath">The path to place the file in</param>
        /// <returns>The name of the created file</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "File"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "f")]
        string CreateFile(Feature f, string FilePath);
    }
}