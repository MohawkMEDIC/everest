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
 * Created by SharpDevelop.
 * User: justin
 * Date: 11/11/2008
 * Time: 7:45 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Collections.Specialized;

namespace MohawkCollege.Util.Console.Parameters
{
	/// <summary>
	/// The parameter parser class is responsible for parsing a class that contains
	/// properties set with the parameter attribute. 
	/// </summary>
	/// <typeparamref name="T">The class to parse</typeparamref>
	public class ParameterParser<T> where T : new()
	{
		
		private enum ValueScanTime
		{
			ThisIteration,
			NextIteration,
			NotNeeded
		}
		
		/// <summary>
		/// Creates a new instance of the parameter parser class
		/// </summary>
		public ParameterParser()
		{
		}
		
		/// <summary>
		/// Write help using the <see cref="System.ComponentModel.DescriptionAttribute"/> and <see cref="MohawkCollege.Util.Console.Parameters.ParameterAttribute"/> 
		/// attributes.
		/// </summary>
		/// <param name="tw">The text writer to write the help to</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "tw"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        public void WriteHelp(TextWriter tw)
		{
			
			foreach(PropertyInfo pi in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
				if(pi.GetCustomAttributes(typeof(ParameterAttribute), true).Length > 0)
				{
					int i = 0;
					foreach(ParameterAttribute pa in pi.GetCustomAttributes(typeof(ParameterAttribute), true))
					{
						DescriptionAttribute description = (DescriptionAttribute)(pi.GetCustomAttributes(typeof(DescriptionAttribute), true).Length > 0 ? pi.GetCustomAttributes(typeof(DescriptionAttribute), true)[0] : null);
						
						if(description != null && pa.Name != "*") 
						{	// No description attribute so we can't output help
							
							// Write the parameter syntax
							tw.Write(string.Format("{0}{1}{2}", pa.Name.Length > 1 ? "--" : " -", 
							                       pa.Name, new String(' ', 20 - pa.Name.Length - 2)));
							
							// Output the description and attribute
							if(i == 0)
								tw.Write(description.Description);
							
							tw.WriteLine();
							i++;
						}
					}
				}
			
		}
		
		/// <summary>
		/// Parse a parameter array into a class of <typeparamref name="T"/>. This parses an array that 
		/// is passed to a console program and populates a new instance of T. An example of the accepted
		/// formats for the parameters is attached below
		/// <example>
		/// -j hello			Sets parameter j to hello
		/// --journal=hello		Sets parameter journal to hello
		/// -j					Sets parameter j to true
		/// --journal			Sets parameter journal to true
		/// -j hello1 -j hello2	Adds the value hello1 and hello2 to parameter j
		/// -jbd				Sets parameters j b and d to true
		/// </example>
		/// </summary>
		/// <param name="argv">The argument list to parse</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "argv"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public T Parse(string[] argv)
		{
			
			#region Scan for parameters to look for
			
			Dictionary<String, PropertyInfo> parameterList = new Dictionary<String, PropertyInfo>();
            PropertyInfo extensionValueProperty = null;

			// Loop through property information for the class we are scanning and find all of the parameters
			// that we will look for in the argument list
            foreach (PropertyInfo p in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                if (p.GetCustomAttributes(typeof(ParameterAttribute), true).Length > 0)
                    foreach (ParameterAttribute a in p.GetCustomAttributes(typeof(ParameterAttribute), true))
                    {
                        if (parameterList.ContainsKey(a.Name.ToLower())) // Sanity check
                            throw new InvalidOperationException(String.Format("ParameterAttribute {0} on member {1} has already been declared", a.Name, p.Name));

                        parameterList.Add(a.Name.ToLower(), p);
                    }
                else if (p.GetCustomAttributes(typeof(ParameterExtensionAttribute), true).Length > 0)
                    extensionValueProperty = p;
                
    		#endregion
			
			#region Prepare for scanning of string
			
			T retVal = new T();
			ValueScanTime needsValue = ValueScanTime.NotNeeded;
            Dictionary<String, StringCollection> extensionValues = new Dictionary<string, StringCollection>();

			string parmName = "";

			#endregion
			
			#region Scan argument list
			
			// Scan all strings in the argv array of parameters
			foreach(string str in argv)
			{
                try
                {
                    string valStr = str;

                    // Set needs value
                    if (needsValue == ValueScanTime.NextIteration) needsValue = ValueScanTime.ThisIteration;

                    // We are expecting a parameter
                    if (needsValue == ValueScanTime.NotNeeded && str.StartsWith("--")) // Longhand parameter
                    {

                        parmName = String.Empty;

                        // Handle the = case
                        if (valStr.Contains("="))
                        {
                            parmName = valStr;
                            valStr = parmName.Substring(parmName.IndexOf("=") + 1);
                            parmName = parmName.Substring(0, parmName.IndexOf("="));
                            needsValue = ValueScanTime.ThisIteration;
                        }

                        parmName = parmName == "" ? valStr.Substring(2).ToLower() : parmName.Substring(2).ToLower();

                        // Not expecting this parameter!
                        if (!parameterList.ContainsKey(parmName) && !extensionValues.ContainsKey(parmName))
                            extensionValues.Add(parmName, new StringCollection());

                        // Do we need a value?
                        if (needsValue == ValueScanTime.NotNeeded)
                        {
                            needsValue = parameterList[parmName].PropertyType == typeof(bool) ? ValueScanTime.NotNeeded : ValueScanTime.NextIteration;
                            if (needsValue == ValueScanTime.NotNeeded) parameterList[parmName].SetValue(retVal, true, null);
                        }
                    }
                    else if (needsValue == ValueScanTime.NotNeeded && str.StartsWith("-")) // shorthand, could get messy
                    {
                        // Parameter may be mashed (Example: -ahfjr)
                        string tstr = str.Substring(1);

                        // Loop through each character
                        foreach (char c in tstr.ToLower().ToCharArray())
                        {
                            // Sanity check for parameters
                            if (needsValue == ValueScanTime.NextIteration) throw new ArgumentException(String.Format("Parameter {0} is expected to have a value assignment.", parmName));

                            parmName = c.ToString();

                            // Not expecting this parameter!
                            if (!parameterList.ContainsKey(parmName) && !extensionValues.ContainsKey(parmName))
                                extensionValues.Add(parmName, new StringCollection());

                            // Determine if this parameter needs a value
                            needsValue = parameterList[parmName].PropertyType == typeof(bool) ? ValueScanTime.NotNeeded : ValueScanTime.NextIteration;
                            if (needsValue == ValueScanTime.NotNeeded) parameterList[parmName].SetValue(retVal, true, null);
                        }
                    }
                    else if (needsValue == ValueScanTime.NotNeeded)
                    {
                        // Parameter needs a value and has no name, it is the "catch" condition
                        parmName = "*";

                        if (!parameterList.ContainsKey(parmName))
                            throw new ArgumentException("No catch-all parameter has been specified");
                        else if (parameterList[parmName].PropertyType != typeof(System.Collections.Specialized.StringCollection))
                            throw new InvalidOperationException(String.Format("Catch-all parameter must be a string collection"));

                        needsValue = ValueScanTime.ThisIteration;
                    }

                    // We need to read a value
                    if (needsValue == ValueScanTime.ThisIteration)
                    {
                        // Determine if we are using a string collection, etc
                        if (parameterList.ContainsKey(parmName) && parameterList[parmName].PropertyType == typeof(System.Collections.Specialized.StringCollection))
                        {
                            // Set to a new string collection
                            if (parameterList[parmName].GetValue(retVal, null) == null)
                                parameterList[parmName].SetValue(retVal, new System.Collections.Specialized.StringCollection(), null);
                            (parameterList[parmName].GetValue(retVal, null) as System.Collections.Specialized.StringCollection).Add(valStr);
                        }
                        else if (parameterList.ContainsKey(parmName))
                            parameterList[parmName].SetValue(retVal, valStr, null);
                        else
                            (extensionValues[parmName] as StringCollection).Add(valStr);

                        needsValue = ValueScanTime.NotNeeded;
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(String.Format("Don't understand the parameter '{0}'", str));
                }
			}

            if(extensionValueProperty != null)
                extensionValueProperty.SetValue(retVal, extensionValues, null);
			#endregion


            return retVal;
		}
	}
}
