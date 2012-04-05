using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.IO;
using System.Xml.Serialization;

namespace MARC.Everest.Exceptions
{
    /// <summary>
    /// Extends FormatterException to add more detail.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This exception is thrown whenever a <see cref="T:MARC.Everest.Connectors.ICodeDomFormatter"/> fails to generate
    /// the necessary code to properly validate or graph/parse an instance. This exception rarely occurs, and is usually
    /// seen when developing custom Code Dom based formatters.
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), Serializable]
    public class FormatterCompileException : FormatterException
    {
        /// <summary>
        /// The compilers results.
        /// </summary>
        public CompilerResults Results { get; private set; }
        /// <summary>
        /// A collection of errors.
        /// </summary>
        public CompilerErrorCollection Errors { get { return Results.Errors; } }
        /// <summary>
        /// Gets the contents of the file that threw the exception.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public string TempFile
        {
            get
            {
                foreach (string file in Results.TempFiles)
                    if (Path.GetExtension(file).ToLower() == ".cs")
                    return File.ReadAllText(file.ToString());

                return string.Empty;
            }
        }
        /// <summary>
        /// Gets the result from the original exception.
        /// </summary>
        /// <param name="results">The result from the initial exception.</param>
        public FormatterCompileException(System.CodeDom.Compiler.CompilerResults results)
            : base("A compile exception has occured during creation of the formatter assembly")
        {
            Results = results;
        }
        /// <summary>
        /// Converts the FormatterCompileException to a string.
        /// </summary>
        /// <returns>Returns the error message, all error details, and the error code.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Message);
            sb.AppendLine();

            sb.AppendLine("Errors:");

            foreach (var error in Errors)
                sb.AppendLine(error.ToString());

            sb.AppendLine();
            sb.AppendLine();

            string code = TempFile;

            if (!string.IsNullOrEmpty(code))
            {
                sb.AppendLine("Code: ");
                sb.Append(code);
            }

            return sb.ToString();
        }
    }
}
