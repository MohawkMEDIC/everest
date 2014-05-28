/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 6-5-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.Parameters;
using System.ComponentModel;
using System.Collections.Specialized;

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
        [Parameter("*")]
        [Description("Identifies the CDA template that is to be generated")]
        public StringCollection Template { get; set; }

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
        public String OutputFile { get; set; }

        /// <summary>
        /// Identifies the renderer that should be used to output the class
        /// </summary>
        //[Parameter("renderer")]
        //[Parameter("r")]
        //[Description("Identifies the output format renderer that should be created")]
        //public String Renderer { get; set; }

        /// <summary>
        /// Specifies the locale in which comments should be generated
        /// </summary>
        [Parameter("locale")]
        [Parameter("l")]
        [Description("Identifies the locale from which comments should be used")]
        public String Locale { get; set; }

        /// <summary>
        /// Specifies a place to save the merged project
        /// </summary>
        [Parameter("save")]
        [Description("Identifies a file name to save the bound template file")]
        public String SaveTpl { get; set; }

        /// <summary>
        /// Show the help screen
        /// </summary>
        [Parameter("?")]
        [Parameter("help")]
        [Description("Shows the help screen")]
        public bool Help { get; set; }
    }
}
