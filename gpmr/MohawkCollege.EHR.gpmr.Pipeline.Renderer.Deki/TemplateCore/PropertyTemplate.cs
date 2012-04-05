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
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Reflection;
using MohawkCollege.EHR.gpmr.COR;
using System.Collections;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore
{
	/// <summary>
	/// Identifies a template that applies to properties
	/// </summary>
	[XmlType(TypeName = "PropertyTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
	[Serializable]
	public class PropertyTemplate : NamedTemplate, IComparer<PropertyTemplate>
	{
		private string property;
		private bool escaped = false;

		/// <summary>
		/// True if the value should be escaped
		/// </summary>
		[XmlAttribute("escaped")]
		public bool Escaped
		{
			get { return escaped; }
			set { escaped = value; }
		}
	
		/// <summary>
		/// Identifies the property this template is applied for
		/// </summary>
		[XmlAttribute("property")]
		public string Property
		{
			get { return property; }
			set { property = value; }
		}


		#region IComparer<PropertyTemplate> Members
		//DOC: Documentation Required
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(PropertyTemplate x, PropertyTemplate y)
		{
			string xRealName = x.Name ?? x.Property;
			string yRealName = y.Name ?? y.Property;
			
			// Compare
			return yRealName.Length.CompareTo(xRealName.Length);
		}

		#endregion

		/// <summary>
		/// Fill this property template
		/// </summary>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)")]
		public override string FillTemplate()
		{

			// First... see if there is a feature that could process this item
			if (Context == null) return "";

			FeatureTemplate ftpl = Content.Contains("$feature$") ? NonParameterizedTemplate.Spawn(Parent.FindTemplate(Context.GetType().FullName, Context) as NonParameterizedTemplate, Parent, Context) as FeatureTemplate : null;

			// Current template fields
			string[][] templateFields = new string[][] 
			{
				new string[] { "$feature$", ftpl == null ? "" : ftpl.FillTemplate()},
				new string[] { "$date$", DateTime.Now.ToString("yyyy-MM-dd") },
				new string[] { "$time$", DateTime.Now.ToString("HH:mm:ss") },
				new string[] { "$user$", SystemInformation.UserName },
				new string[] { "$guid$", Guid.NewGuid().ToString() },
				new string[] { "$value$", escaped ? Context.ToString().Replace(">", "&gt;").Replace("<","&lt;").Replace("$","&#036;").Replace("@", "&#64;").Replace("^", "&#094;") : Context.ToString().Replace("$","&#036;").Replace("@", "&#64;").Replace("^", "&#094;").Replace("\\","\\\\") }, // Clean Template parameters from TOSTRING()
				new string[] { "$$", "&#036;" }, 
				new string[] { "@@", "&#064;" },
				new string[] { "^^", "&#094;" },
				new string[] { "$version$", Assembly.GetEntryAssembly().GetName().Version.ToString() },
				new string[] { "$typeName$", Context == null ? "" : Context.GetType().Name }
			};

			if (this.Context is Feature && (this.Context as Feature).Annotations.Exists(o => o is SuppressBrowseAnnotation))
			{
				System.Diagnostics.Trace.WriteLine(String.Format("Feature '{0}' won't be published as a SuppressBrowse annotation was found", (this.Context as Feature).Name), "warn");
				return "";
			}

			string output = Content.Clone() as string;

			foreach (string[] kv in templateFields)
				output = output.Replace(kv[0], kv[1]);
			
			// Process output for literals
			#region Literals
			while (output.Contains("$"))
			{
				int litPos = output.IndexOf('$');
				int ePos = output.IndexOf('$', litPos + 1);

				System.Diagnostics.Debug.Assert(ePos != -1, String.Format("Template field not closed at '{0}'", this.Name));

				string parmName = output.Substring(litPos + 1, ePos - litPos - 1);

				// First, we'll look for a property template that has the specific name
				object content = GetContextPropertyValue(parmName);
				string value = content == null ? "" : content.ToString().Replace("$", "&#036;").Replace("@", "&#064;").Replace("^", "&#094;");
				output = output.Replace(string.Format("${0}$", parmName), string.Format("{0}", value));
			}
			#endregion

			#region Arrays
			while (output.Contains("@"))
			{
				int litPos = output.IndexOf('@');
				int ePos = output.IndexOf('@', litPos + 1);
				string parmName = output.Substring(litPos + 1, ePos - litPos - 1);

				// First, we'll look for a property template that has the specific name
				object[] value = ConvertToObjectArray(GetContextPropertyValue(parmName));

				if (value != null)
				{
					foreach (object o in value)
					{
						string ovalue = o == null ? "" : o.ToString().Replace("$", "&#36;").Replace("@", "&#64;").Replace("^", "&#094;");
						output = output.Replace(string.Format("@{0}@", parmName), string.Format("{0}@{1}@", ovalue, parmName));
					}
					output = output.Replace(string.Format("@{0}@", parmName), "");
				}
				else
					output = output.Replace(string.Format("@{0}@", parmName), "");
			}
			#endregion

			return output;
		}

	}
}