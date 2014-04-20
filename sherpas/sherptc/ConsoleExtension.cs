using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sherptc
{
    /// <summary>
    /// Console extension
    /// </summary>
    public static class ConsoleExtension
    {
        /// <summary>
        /// Write line if the condition is true
        /// </summary>
        public static void WriteLineIf(this TextWriter me, bool condition, String format, params Object[] values)
        {
            if (condition)
                Console.WriteLine(format, values);

        }
    }
}
