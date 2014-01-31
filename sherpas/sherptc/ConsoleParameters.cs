using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.Parameters;
using System.ComponentModel;

namespace sherptc
{
    /// <summary>
    /// Template parameters that can be used on the command line
    /// </summary>
    public class ConsoleParameters
    {
        /// <summary>
        /// Gets or sets the CDA templates that should be processed
        /// </summary>
        [Parameter("source")]
        [Parameter("s")]
        [Description("Identifies the CDA template that is to be generated")]
        public String Template { get; set; }

        /// <summary>
        /// Gets or sets the verbosity
        /// </summary>
        [Parameter("verbose")]
        [Parameter("v")]
        [Description("When set, indicates the tool should be verbose for output")]
        public bool Verbose { get; set; }

        /// <summary>
        /// Identifies the assembly to which the template should be applied
        /// </summary>
        [Parameter("asm")]
        [Parameter("a")]
        [Description("Identifies the RMIM assembly to generate templates against")]
        public String EverestAssembly { get; set; }

        /// <summary>
        /// The directory to which the template should be output
        /// </summary>
        [Parameter("output")]
        [Parameter("o")]
        [Description("Identifies the file where the generated template should be placed")]
        public String OutputDirectory { get; set; }

        /// <summary>
        /// Identifies the renderer that should be used to output the class
        /// </summary>
        [Parameter("renderer")]
        [Parameter("r")]
        [Description("Identifies the output format renderer that should be created")]
        public String Renderer { get; set; }

        /// <summary>
        /// Gets or sets the pipeline that is to be applied
        /// </summary>
        [Parameter("parser")]
        [Parameter("p")]
        [Description("Identifies the pipeline parser that should be used to parse the template file")]
        public String Parser { get; set; }
    }
}
