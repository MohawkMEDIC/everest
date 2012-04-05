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
 * Date: 07-22-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Identifies a class that renders datatypes
    /// </summary>
    /// <remarks>
    /// This interface includes the ability to programatically determine which properties
    /// from the Everest Datatypes classes are supported by the particular formatter
    /// </remarks>
    public interface IDatatypeStructureFormatter : IStructureFormatter
    {

        /// <summary>
        /// Get the supported properties for the specified datatype
        /// </summary>
        /// <example>
        /// <code lang="cs" title="Getting supported properties">
        /// <![CDATA[
        /// // Create the data type formatter
        ///IDatatypeStructureFormatter datatypeFormatter = new DatatypeFormatter();
        ///
        /// // Get supported properties for ED
        ///var supportedProperties = datatypeFormatter.GetSupportedProperties(typeof(ED));
        ///
        /// // Determine if Translation is supported
        ///bool isTranslationSupported = Array.Exists(supportedProperties, o => o.Name == "Translation");
        ///Console.WriteLine("ED.Translation is supported? {0}", isTranslationSupported);
        /// ]]>
        /// </code>
        /// </example>
        PropertyInfo[] GetSupportedProperties(Type dataType);

    }
}
