using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// An annotation that is used to identify that another message type has been collapsed into the 
    /// current feature
    /// </summary>
    public class CodeCombineAnnotation : Annotation
    {
         
        /// <summary>
        /// Gets the reference to the original type
        /// </summary>
        public TypeReference OriginalType { get; set; }

        /// <summary>
        /// Creates a new instance of the code combine annotation
        /// </summary>
        public CodeCombineAnnotation(TypeReference originalType)
        {
            this.OriginalType = originalType;
        }
    }
}
