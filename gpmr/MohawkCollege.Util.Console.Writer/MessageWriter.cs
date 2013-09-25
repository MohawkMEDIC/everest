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
 * Created by SharpDevelop.
 * User: justin
 * Date: 11/12/2008
 * Time: 7:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace MohawkCollege.Util.Console.MessageWriter
{
	/// <summary>
	/// The message writer class is responsible for writing data to 
	/// a source based on the verbosity setting of the message.
	/// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    public class MessageWriter : System.Diagnostics.TraceListener
	{

        /// <summary>
        /// Public constructor 
        /// </summary>
        public MessageWriter()
        {
            MessageCount = new Dictionary<string, int>();
        }

        /// <summary>
        /// The number of messages written to the listener
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Dictionary<String, Int32> MessageCount { get; set; }

		/// <summary>
		/// Verbosity levels
		/// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags")]
        public enum VerbosityType : int
		{
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
			None = 0,
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            Fatal = 1,
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
			Error = 2,
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
			Warn = 4,
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
			Debug = 8,
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            Information = 16
		}
		
		private VerbosityType verbosity;
		private TextWriter writer;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
		public VerbosityType Verbosity
		{
			get { return verbosity; }
			set { verbosity = value; }
		}
		
		/// <summary>
		/// Create a new instance of the MessageWriter class
		/// </summary>
		/// <param name="tw">The textwriter to write messages to</param>
		/// <param name="Verbosity">The verbosity level of the writer</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Verbosity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "tw")]
        public MessageWriter(TextWriter tw, VerbosityType Verbosity)
		{
			this.writer = tw;
			this.verbosity = Verbosity;
            MessageCount = new Dictionary<string, int>();
		}
		
		/// <summary>
		/// Print a message to the writer
		/// </summary>
		/// <param name="e">An exception to write</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "e")]
        public void Print(Exception e)
		{
			if((verbosity & VerbosityType.Error) == VerbosityType.Error)
			{
				Exception oe = e;
				while(oe != null)
				{
					writer.WriteLine(oe.ToString());
					if(oe != e)
						writer.WriteLine("-- caused by --");
					oe = oe.InnerException;
				}
			}
		}
		
		/// <summary>
		/// Write a warning to the message
		/// </summary>
		/// <param name="s">The message to write</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public void Warn(String s)
		{
			if((verbosity & VerbosityType.Warn) == VerbosityType.Warn)
				writer.WriteLine(s);
		}
		
		/// <summary>
		/// Print a debug message to the writer
		/// </summary>
		/// <param name="s">The string to write</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public void Print(String s)
		{
			if((verbosity & VerbosityType.Debug) == VerbosityType.Debug)
				writer.WriteLine(s);
		}
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)")]
        public override void Write(string message)
        {
            if ((verbosity & VerbosityType.Debug) == VerbosityType.Debug && message.StartsWith("debug:") ||
                (verbosity & VerbosityType.Warn) == VerbosityType.Warn && message.StartsWith("warn:") ||
                (verbosity & VerbosityType.Fatal) == VerbosityType.Fatal && message.StartsWith("fatal:") ||
                (verbosity & VerbosityType.Error) == VerbosityType.Error && message.StartsWith("error:") ||
                (verbosity & VerbosityType.Information) == VerbosityType.Information && message.StartsWith("information:"))
                writer.Write(message.Substring(message.IndexOf(":") + 2));
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.StartsWith(System.String)")]
        public override void WriteLine(string message)
        {
            // PREFIX
            if ((verbosity & VerbosityType.Warn) == VerbosityType.Warn && (message.StartsWith("warn:") || message.StartsWith("quirks:")))
            {
                if (message.StartsWith("quirks:"))
                {
                    writer.Write("\r\n!!!! QUIRKS MODE: !!!!\r\n");
                }
                else
                    writer.Write("WARN:");
            }
            else if ((verbosity & VerbosityType.Error) == VerbosityType.Error && message.StartsWith("error:"))
                writer.Write("ERROR:");
            else if ((verbosity & VerbosityType.Fatal) == VerbosityType.Fatal && message.StartsWith("fatal:"))
                writer.Write("\r\n-- FATAL --\r\n\r\n");

            // Message
            if ((verbosity & VerbosityType.Debug) == VerbosityType.Debug && message.StartsWith("debug:") ||
                (verbosity & VerbosityType.Warn) == VerbosityType.Warn && (message.StartsWith("warn:") || message.StartsWith("quirks:")) ||
                (verbosity & VerbosityType.Fatal) == VerbosityType.Fatal && message.StartsWith("fatal:") ||
                (verbosity & VerbosityType.Error) == VerbosityType.Error && message.StartsWith("error:") ||
                (verbosity & VerbosityType.Information) == VerbosityType.Information && message.StartsWith("information:"))
                writer.WriteLine(message.Substring(message.IndexOf(":") + 2));

            // Count the message

            string category = null;
            if(message.Contains(":"))
                category = message.Substring(0, message.IndexOf(":"));

            if (!MessageCount.ContainsKey(category ?? ""))
                MessageCount.Add(category ?? "", 0);
            MessageCount[category ?? ""]++;
        }
    }
}
