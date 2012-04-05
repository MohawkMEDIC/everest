using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Identifies an enumeration that is a code system
    /// </summary>
    public class CodeSystem : Enumeration
    {
        /// <summary>
        /// Get the type of enumeration structure this class represents
        /// </summary>
        public override string EnumerationType
        {
            get { return "Code System"; }
        }

    }
}
