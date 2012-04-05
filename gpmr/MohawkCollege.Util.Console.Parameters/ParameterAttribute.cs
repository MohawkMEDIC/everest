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
 * Created by SharpDevelop.
 * User: justin
 * Date: 11/11/2008
 * Time: 7:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace MohawkCollege.Util.Console.Parameters
{
	/// <summary>
	/// The parameter attribute class marks a property as a 
	/// parameter that can be parsed as a console parameter
	/// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class ParameterAttribute : System.Attribute
	{
		
		private string name;
		
		/// <summary>
		/// Get or set the name of the parameter from the command prompt
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParameterName"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Parameter")]
        public ParameterAttribute(string ParameterName)
		{
			this.name = ParameterName;
		}
	}
}
