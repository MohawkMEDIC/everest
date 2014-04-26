using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.StructDoc;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Extension methods for the ED data type
    /// </summary>
    public static class StructDocExtensionMethods
    {

        /// <summary>
        /// Initialize an ED to carry structured document data
        /// </summary>
        /// <param name="me">The ED to add the structured document data</param>
        /// <returns>A <see cref="T:MARC.Everest.Sherpas.StructDoc.StructDocElementNode"/> representing the root</returns>
        public static StructDocElementNode CreateStructDoc(this SD me, string language = null, string ID = null)
        {
            me.Language= language;
            me.Id = ID;
            var retVal = new StructDocElementNode();
            return retVal;
        }

        /// <summary>
        /// Parses an ED into a <see cref="T:MARC.Everest.Sherpas.StructDoc.StructDocNode"/>
        /// </summary>
        /// <param name="me">The ED to retrieve the StructDoc content from</param>
        /// <returns>The content of the ED</returns>
        public static StructDocNode GetStructDocContent(this ED me)
        {

            me.Data

        }
    }
}
