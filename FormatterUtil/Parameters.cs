using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.Parameters;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FormatterUtil
{
    public class Parameters
    {

        /// <summary>
        /// Gets or sets the target namespace
        /// </summary>
        [Parameter("target-ns")]
        [Description("The namespace in which helper classes should be generated")]
        public string TargetNs { get; set; }

        /// <summary>
        /// Gets or sets the assembly
        /// </summary>
        [Parameter("assembly")]
        [Description("The file for which helper classes should be generated")]
        public string AssemblyFile { get; set; }

        /// <summary>
        /// Interaction to create parser for
        /// </summary>
        [Parameter("interaction")]
        [Description("The name of the interactions to generate")]
        public StringCollection Interactions { get; set; }

        /// <summary>
        /// Gets the output file
        /// </summary>
        [Parameter("output")]
        [Description("Sets the output file")]
        public string Output { get; set; }

        /// <summary>
        /// Show help
        /// </summary>
        [Parameter("help")]
        public bool ShowHelp { get; set; }
    }
}
