using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.Parameters;
using System.ComponentModel;

namespace MARC.Everest.Test.Generator
{
    /// <summary>
    /// Represents console parameters that can be passed to the test generator
    /// </summary>
    public class ConsoleParameters
    {
        /// <summary>
        /// Gets or sets the verbosity of the test generator
        /// </summary>
        [Parameter("v")]
        [Parameter("verbosity")]
        [Description("Sets the verbosity of the test generator")]
        public string Verbosity { get; set; }

        /// <summary>
        /// Gets or sets the output of the test generator
        /// </summary>
        [Parameter("o")]
        [Parameter("output")]
        [Description("Sets the output file name of the generated tests")]
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets the assembly for which to generate the tests
        /// </summary>
        [Parameter("a")]
        [Parameter("assembly")]
        [Description("Sets the assembly for which tests should be generated")]
        public string Assembly { get; set; }

        /// <summary>
        /// If true, the test generator will generate instance tests
        /// </summary>
        [Parameter("instances")]
        [Description("If set, generates the instance tests")]
        public bool GenerateInstanceTests { get; set; }

        /// <summary>
        /// If true, the test generator will generate formatter tests
        /// </summary>
        [Parameter("formatter")]
        [Description("If set, generates formatter tests")]
        public bool GenerateFormatterTests { get; set; }

    }
}
