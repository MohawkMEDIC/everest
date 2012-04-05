using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.Util.Console.MessageWriter;
using System.Drawing;

namespace gpmr
{
    public class ConsoleTraceWriter : MessageWriter
    {
        public ConsoleTraceWriter(VerbosityType verbosity) : base(Console.Out, verbosity)
        {
        }

        /// <summary>
        /// Writeline
        /// </summary>
        /// <param name="o"></param>
        public override void WriteLine(string message)
        {
            if (message.StartsWith("quirks:"))
                Console.ForegroundColor = ConsoleColor.Green;
            else if (message.StartsWith("error:") || message.StartsWith("fatal:"))
                Console.ForegroundColor = ConsoleColor.Red;
            else if (message.StartsWith("warn:"))
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (message.StartsWith("debug:"))
                Console.ForegroundColor = ConsoleColor.Cyan;
            else
                Console.ForegroundColor = ConsoleColor.Gray;

            base.WriteLine(message);
        }
    }
}
