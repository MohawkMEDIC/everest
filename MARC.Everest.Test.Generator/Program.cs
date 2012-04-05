using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.Parameters;
using MohawkCollege.Util.Console.MessageWriter;
using System.IO;

namespace MARC.Everest.Test.Generator
{
    /// <summary>
    /// This program is a hackish way to auto-generate some unit tests that can test the 
    /// creation of instances generated from GPMR
    /// </summary>
    class Program
    {

        /// <summary>
        /// Gets or sets the name of the file that the test generator should create
        /// </summary>
        public static string FileName { get; set; }

        /// <summary>
        /// Gets or sets the contents of the output file
        /// </summary>
        public static string FileContents
        {
            get
            {
                return File.ReadAllText(FileName);
            }
            set
            {
                File.WriteAllText(FileName, value);
            }
        }

        static void Main(string[] args)
        {
            // Parse paramters
            try
            {
                // Setup parameters
                ParameterParser<ConsoleParameters> parser = new ParameterParser<ConsoleParameters>();
                ConsoleParameters parms = parser.Parse(args);
                MessageWriter mw = new MessageWriter(Console.Out, String.IsNullOrEmpty(parms.Verbosity) ? MessageWriter.VerbosityType.Information : (MessageWriter.VerbosityType)Int32.Parse(parms.Verbosity));
                System.Diagnostics.Trace.Listeners.Add(mw);
                if (String.IsNullOrEmpty(parms.Output) || String.IsNullOrEmpty(parms.Assembly))
                {
                    Console.WriteLine("Must specify the output and assembly parameters");
                    return;
                }

                FileName = parms.Output;

                FileContents = GenerateScaffold(parms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
#if DEBUG
                System.Console.ReadKey();
#endif
            }
        }

        /// <summary>
        /// Generate scafflding
        /// </summary>
        private static string GenerateScaffold(ConsoleParameters parms)
        {
            string content = Template.ClassTemplate;
            string[][] parameters = {
                                        new string[] { "className", Path.GetFileNameWithoutExtension(FileName)},
                                        new string[] { "fileName", FileName},
                                        new string[] { "tests", GenerateTests(parms) }
                                    };
            foreach (var parm in parameters)
                content = content.Replace(String.Format("${0}$", parm[0]), parm[1]);

            return content;
        }

        /// <summary>
        /// Generate tests
        /// </summary>
        private static string GenerateTests(ConsoleParameters parms)
        {
            StringWriter retVal = new StringWriter();
            if (parms.GenerateInstanceTests)
                retVal.WriteLine(InstanceGenerator.GenerateInstanceTests(parms.Assembly));
            if (parms.GenerateFormatterTests)
                retVal.WriteLine(InstanceGenerator.GenerateFormatterTests(parms.Assembly));
            return retVal.ToString();
        }
    }
}
