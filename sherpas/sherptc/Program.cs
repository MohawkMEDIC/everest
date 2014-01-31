using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.Parameters;

namespace sherptc
{
    public class Program
    {

        /// <summary>
        /// The main entry point to the program
        /// </summary>
        public static void Main(String[] args)
        {

            // Parser for parameters
            ParameterParser<ConsoleParameters> parser = new ParameterParser<ConsoleParameters>();

            try
            {

                Console.WriteLine("SHERPAS CDA Template Compiler for MARC-HI Everest Framework");
                Console.WriteLine("Copyright (C) 2014, Mohawk College of Applied Arts and Technology");

                // Parse parameters
                var parameters = parser.Parse(args);



            }
            catch (Exception e)
            {

#if DEBUG
                Console.WriteLine(e.ToString());
#else
                Console.WriteLine(e.Message);
#endif
            }


        }

    }
}
