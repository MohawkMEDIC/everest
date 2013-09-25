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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Parser for connection strings
    /// </summary>
    /// <remarks>
    /// <para>This class is commonly used by connectors within the Everest Framework to facilitate the parsing
    /// of connection strings. Connection strings are in the format:</para>
    /// <code>key=value;key=value</code>
    /// <para>This static class provides mechanisms for parsing such strings into a dictionary of key/value pairs</para>
    /// </remarks>
    public static class ConnectionStringParser
    {

        /// <summary>
        /// Parse a connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to parse.</param>
        /// <returns>The keys and values for the keys.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static Dictionary<String, List<String>> ParseConnectionString(string connectionString)
        {
            Dictionary<String,List<String>> retVal = new Dictionary<string,List<string>>();

            foreach (String kvPair in connectionString.Split(';'))
            {
                string[] kv = kvPair.Split('=');
                if (kv.Length != 2) continue;
                string key = kv[0].ToLower().Trim(), value = kv[1].Trim();
                if (!retVal.ContainsKey(key))
                    retVal.Add(key, new List<string>(new string[] { value }));
                else
                    retVal[key].Add(kv[1].Trim());
            }
            return retVal;

        }
    }
}