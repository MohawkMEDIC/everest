/*
 * This file is licensed under the Berkeley Software Distribution (BSD) license
 * 
 * Copyright (c) 2008, Mohawk College of Applied Arts and Technology
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 *
 * 	- Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 *	- Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
 *	- Neither the name of the Mohawk College nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 * Created by SharpDevelop.
 * User: justin
 * Date: 11/12/2008
 * Time: 6:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using MohawkCollege.Util.Console.Parameters;
using System.Collections.Generic;

namespace gpmr
{
	/// <summary>
	/// The mif converter parameter class contains data passed to the mif converter 
	/// application from the console. 
	/// <para>
	/// Parameters include
	/// </para>
	/// <list type="unordered">
	/// <item>-s / --source		-	The source MIF file</item>
	/// <item>-v / --verbose	-	Turn on verbosity level</item>
	/// <item>-f / --format 	-	Add a converter to this conversion (XML_ITS,XML_MMM,RIM_CS)</item>
	/// <item>-o / --output-dir	-	Set the directory to output files to</item>
	/// </list>
	/// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mif")]
    public class MifConverterParameters
	{
		
		#region Fields
		private StringCollection sources;
		private string verbosity;
		private StringCollection renderers;
		private string outputDir;
		private bool help = false;
		private bool version = false;
		private StringCollection renderExtensions;
        private StringCollection include;
        private Dictionary<String, StringCollection> extension;
		#endregion
		
		#region Properties

        /// <summary>
        /// Contains the list of extension objects that have been 
        /// added to the command parameter set by a particular extension module
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), ParameterExtension()]
        public Dictionary<String, StringCollection> Extensions
        {
            get { return extension; }
            set { extension = value; }
        }

		/// <summary>
		/// Adds pipeline extensions to load for this render
		/// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Description("Adds custom extensions to the rendering pipeline")]
		[Parameter("extension")]
		public StringCollection RenderExtensions
		{
			get { return renderExtensions; }
			set { renderExtensions = value; }
		}
		
		/// <summary>
		/// The mif files (names or wildcards) to render
		/// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Description("The MIF files to render. Either a file name or wild card")]
		[Parameter("s")]
		[Parameter("source")]
		[Parameter("*")]
		public StringCollection Sources
		{
			get { return sources; }
			set { sources = value; }
		}
		
		/// <summary>
		/// If true, the converter should show only its version and exit
		/// </summary>
		[Description("Show version of GMPC and exit")]
		[Parameter("version")]
		public bool ShowVersion
		{
			get { return version; }
			set { version = value; }
		}
		
		/// <summary>
		/// The verbosity level of the tool
		/// <list type="ordered">
		/// <item>None</item>
		/// <item>Exception</item>
		/// <item>Warn</item>
		/// <item>Debug</item>
		/// </list>
		/// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.String)"), Description("Set the verbosity of the converter's output")]
		[Parameter("v")]
		[Parameter("verbosity")]
		public string Verbosity
		{
			get { return verbosity; }
			set { 
				double d;
				if(double.TryParse(value, out d) == false || Convert.ToInt32(value) > 32)
					throw new ArgumentOutOfRangeException("Verbosity level must be between 0 and 31");
				verbosity = value;
			}
		}
		
		/// <summary>
		/// Set the renderers to use to render the mif files
		/// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderers"), Description("Configures a renderer component to render the MIF file")]
		[Parameter("r")]
		[Parameter("renderer")]
		public StringCollection Renderers
		{
			get { return renderers; }
			set { renderers = value; }
		}
		
		/// <summary>
		/// The directory to output the files to
		/// </summary>
		[Description("Sets the target directory for the rendered files")]
		[Parameter("o")]
		[Parameter("output")]
		public string OutputDirectory
		{
			get { return outputDir; }
			set { outputDir = value; }
		}
		
		/// <summary>
		/// If true, help should be shown
		/// </summary>
		[Description("Show help and exit")]
		[Parameter("?")]
		[Parameter("help")]
		public bool Help
		{
			get { return help; }
			set { help = value; }
		}


        /// <summary>
        /// Because MIF has no concept of file include, it is a reference, this parameter is used
        /// to collect mif files to include in the in memory repository.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mif"), Parameter("include")]
        [Description("Include a path/file to help process the transform")]
        public StringCollection MifClassPath
        {
            get { return include; }
            set { include = value; }
        }
		#endregion

        /// <summary>
        /// Operate GPMR in quirks mode
        /// </summary>
        [Description("Operate in quirks mode")]
        [Parameter("quirks")]
        public bool QuirksMode { get; set; }

        /// <summary>
        /// Operate GPMR in strict mode
        /// </summary>
        [Parameter("strict")]
        [Description("Operate in strict mode")]
        public bool StrictMode { get; set; }


        /// <summary>
        /// Debug mode
        /// </summary>
        [Parameter("d")]
        [Parameter("debug")]
        [Description("Shortcut to -v 15")]
        public bool Debug { get; set; }
        /// <summary>
        /// Only Errors
        /// </summary>
        [Parameter("e")]
        [Parameter("errors")]
        [Description("Shortcut to -v 3")]
        public bool ErrorsOnly { get; set; }
        /// <summary>
        /// All messages
        /// </summary>
        [Parameter("c")]
        [Parameter("chatty")]
        [Description("Shortcut to -v 31")]
        public bool Chatty { get; set; }
        [Parameter("q")]
        [Parameter("quiet")]
        [Description("No Messages (except copyright, etc...")]
        public bool Quiet { get; set; }

	}
}
