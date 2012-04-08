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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat
{
    //DOC: Documentation Required
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance"), XmlRoot(ElementName = "staticModel", Namespace = "urn:hl7-org:v3/mif2")]
    [XmlType(TypeName = "GlobalStaticModel", Namespace = "urn:hl7-org:v3/mif2")]
    public class GlobalStaticModel : StaticModel
    {
       
        private List<String> supportedSchemaVersions = new List<string>() { "2.1.4" };
       
        // Initialize
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        internal override void Initialize()
        {
            if (!supportedSchemaVersions.Contains(schemaVersion))
                System.Diagnostics.Trace.WriteLine(String.Format("MIF version '{0}' is not officially supported", schemaVersion), "warn");

            base.Initialize();
        }
    }
}