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
 * Date: 07-19-2011
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Util
{
    /// <summary>
    /// Utility class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Util")]
    public static class Util
    {
        /// <summary>
        /// Pascal cases the specified string and removes any unfriendly (to java) characters
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string MakeFriendly(string original)
        {
            if (original == null || original.Length == 0) return original;

            string retVal = original;
            foreach (char c in original)
            {
                Regex validChars = new Regex("[A-Za-z0-9_]");
                if (!validChars.IsMatch(c.ToString()))
                    retVal = retVal.Replace(c.ToString(), "");
            }

            // Remove non-code chars
            //foreach (char c in nonCodeChars)
            //    retVal = retVal.Replace(c.ToString(), "");
            //retVal = retVal.Replace("-", "_");


            // Check for numeric start
            Regex re = new Regex("^[0-9]");
            if (re.Match(retVal).Success)
                retVal = "_" + retVal;

            return retVal.Length == 0 ? null : retVal;
        }


        /// <summary>
        /// Pascal case the specified string
        /// </summary>
        /// <param name="original">The original string</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
        public static string PascalCase(string original)
        {

             if (original == null || original.Length == 0) return original;

            string retVal = "";
            foreach (string s in original.Split(' ', '/'))
                if (s.Length > 1)
                    retVal += s.ToUpper().Substring(0, 1) + s.Substring(1);
                else
                    retVal += s.ToUpper() + "_";

            return MakeFriendly(retVal);
        }

        /// <summary>
        /// String escape a string
        /// </summary>
        internal static string StringEscape(string p)
        {
            return p.Replace("\"", "\\\"");
        }

    }
}