using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Test
{
    internal static class Tracer
    {
        public static void Trace(string message)
        {
            //Trace(message, null);
        }

        public static void Trace(string message, UseContext context)
        {
#if DEBUG
           // string indent = context != null ? context.Indent : string.Empty;
            //System.Diagnostics.Trace.WriteLine(indent + message);
#endif
        }
    }
}
