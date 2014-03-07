using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using MARC.Everest.Sherpas.Templating.Renderer;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a method instruction
    /// </summary>
    public interface IMethodInstruction
    {

        /// <summary>
        /// Convert to a code dom statement
        /// </summary>
        CodeStatementCollection ToCodeDomStatement(RenderContext scope);

    }
}
