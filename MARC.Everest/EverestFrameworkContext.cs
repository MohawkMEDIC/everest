using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;

namespace MARC.Everest
{
    /// <summary>
    /// Everest Framework context
    /// </summary>
    public static class EverestFrameworkContext
    {
        /// <summary>
        /// Gets or sets the culture that Everest uses for formatting
        /// </summary>
        /// <remarks>
        /// This method controls the current culture used for the display of data-types in the ToString method
        /// </remarks>
        public static CultureInfo CurrentCulture { get; set; }

        /// <summary>
        /// Gets the framework version (rather than the assembly version)
        /// </summary>
        public static Version FrameworkVersion
        {
            get
            {
                var infoAtt = typeof(EverestFrameworkContext).Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true);
                if (infoAtt.Length == 1)
                    return new Version((infoAtt[0] as AssemblyInformationalVersionAttribute).InformationalVersion);
                else
                    return null;
            }
        }

        /// <summary>
        /// Framework context ctor
        /// </summary>
        static EverestFrameworkContext()
        {
            EverestFrameworkContext.CurrentCulture = CultureInfo.InvariantCulture;
        }

    }
}
