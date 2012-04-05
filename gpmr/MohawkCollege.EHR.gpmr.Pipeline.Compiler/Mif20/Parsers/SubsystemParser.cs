/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat;
using System.Xml;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Responsible for parsing a package into a subsystem
    /// </summary>
    internal static class SubsystemParser
    {
        /// <summary>
        /// Template class that parses any package object into a subsytem object   
        /// </summary>
        /// <param name="p">The package to parse</param>
        /// <returns>An empty subsystem object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        private static MohawkCollege.EHR.gpmr.COR.SubSystem Parse(Package p)
        {

            MohawkCollege.EHR.gpmr.COR.SubSystem package = new MohawkCollege.EHR.gpmr.COR.SubSystem();

            // Set the derivation property
            package.DerivedFrom = p;

            // Set the name of the system to either "RIM" of the package we are parsing is a rim
            // artifact, or the naming convention sd_Air 
            package.Name = p.PackageLocation.Artifact == ArtifactKind.RIM ? "RIM" : p.PackageLocation.ToString(MifCompiler.NAME_FORMAT);

            // Set the realm
            //package.Realm = p.PackageLocation.Realm;

            // Set the sort key
            package.SortKey = p.SortKey;
            
            // Set copyright information
            package.Documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();

            if (p.Header.Copyright != null)
                package.Documentation.Copyright = string.Format("(C) {0}, {1}", p.Header.Copyright.Year, p.Header.Copyright.Owner);

            // set business names
            if (p.BusinessName != null && p.BusinessName.Count > 0)
            {
                foreach (BusinessName bn in p.BusinessName)
                    if (bn.Language == MifCompiler.Language || bn.Language == "")
                        package.BusinessName = bn.Name;
            }
            else
                package.BusinessName = p.Title;

            return package;
        }

        /// <summary>
        /// Parse a package into a COR subsystem object
        /// </summary>
        /// <param name="p">The package to parse</param>
        /// <returns>The subsystem that is parsed</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength")]
        public static MohawkCollege.EHR.gpmr.COR.SubSystem Parse(GlobalStaticModel p)
        {
            MohawkCollege.EHR.gpmr.COR.SubSystem retVal = Parse(p as Package);
            
            // Backup copyright
            string copy = retVal.Documentation.Copyright;

            // Parse documentation
            if (p.Annotations != null)
                retVal.Documentation = DocumentationParser.Parse(p.Annotations.Documentation);
            else
                retVal.Documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();

            retVal.Documentation.Copyright = copy;

            UpdateLegalInfo(retVal.Documentation, p.Header);

            // TODO: AppInfo here

            // Business name of the entry point overrides the business name of the subsystem
            if (retVal.BusinessName == null && p.OwnedEntryPoint != null && p.OwnedEntryPoint[0].BusinessName != null)
                foreach (BusinessName bn in p.OwnedEntryPoint[0].BusinessName)
                    if (bn.Language == MifCompiler.Language || bn.Language == "")
                        retVal.BusinessName = bn.Name;
                
            // Fire parsed event. This will trigger any class repositories in the current app domain 
            // to add the feature to their repository
            retVal.FireParsed();

            return retVal;
        }

        /// <summary>
        /// Update legalization information
        /// </summary>
        internal static void UpdateLegalInfo(MohawkCollege.EHR.gpmr.COR.Documentation documentation, Header header)
        {
            // Add legalese and other fun stuff
            if (header.Contributor != null)
            {
                documentation.Contributors = new List<string>(header.Contributor.Count);
                foreach (var kv in header.Contributor)
                    documentation.Contributors.Add(String.Format("{0}: {1} ({2})", kv.Role, kv.Name.Name, kv.Affiliation));
            }
            // Disclaimer
            if (header.Copyright != null && header.Copyright.Disclaimer != null)
            {
                documentation.Disclaimer = new List<string>();
               foreach (XmlElement xel in header.Copyright.Disclaimer.MarkupElements ?? new List<XmlElement>().ToArray())
                    documentation.Disclaimer.Add(xel.OuterXml.Replace(" xmlns:html=\"http://www.w3.org/1999/xhtml\"", "").Replace("html:", "")); // Clean mif doc data from docs
                if (header.Copyright.Disclaimer.MarkupText != null) documentation.Disclaimer.Add(header.Copyright.Disclaimer.MarkupText);
            }
            // Disclaimer
            if (header.Copyright != null && header.Copyright.LicenseTerms != null)
            {
                documentation.LicenseTerms = new List<string>();
                foreach (XmlElement xel in header.Copyright.LicenseTerms.MarkupElements ?? new List<XmlElement>().ToArray())
                    documentation.LicenseTerms.Add(xel.OuterXml.Replace(" xmlns:html=\"http://www.w3.org/1999/xhtml\"", "").Replace("html:", "")); // Clean mif doc data from docs
                if (header.Copyright.LicenseTerms.MarkupText != null) documentation.LicenseTerms.Add(header.Copyright.LicenseTerms.MarkupText);
            }

         }

    }
}
