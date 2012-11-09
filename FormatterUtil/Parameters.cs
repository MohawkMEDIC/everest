using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.Parameters;
using System.Collections.Specialized;

namespace FormatterUtil
{
    public class Parameters
    {

        /// <summary>
        /// Gets or sets the target namespace
        /// </summary>
        [Parameter("target-ns")]
        public string TargetNs { get; set; }

        /// <summary>
        /// Gets or sets the assembly
        /// </summary>
        [Parameter("assembly")]
        public string AssemblyFile { get; set; }

        /// <summary>
        /// Interaction to create parser for
        /// </summary>
        [Parameter("interaction")]
        public StringCollection Interactions { get; set; }

        /// <summary>
        /// Gets the output file
        /// </summary>
        [Parameter("output")]
        public string Output { get; set; }

    }
}
