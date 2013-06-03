using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Json
{
    /// <summary>
    /// Represents settings for the JsonStateWriter
    /// </summary>
    public class JsonStateWriterSettings
    {

        // Default Settings
        public static JsonStateWriterSettings Default = new JsonStateWriterSettings();

        /// <summary>
        /// Creates a new instance of the JsonStateWriterSettings class
        /// </summary>
        public JsonStateWriterSettings()
        {
            this.IndentChar = '\t';
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the underlying stream should be
        /// closed when the JsonStateWriter is closed
        /// </summary>
        public bool CloseOutput { get; set; }

        /// <summary>
        /// Gets or sets a value which indicates whether the output of the JsonStateWriter should
        /// be indented.
        /// </summary>
        public bool Indent { get; set; }

        /// <summary>
        /// Gets or sets the indentation character
        /// </summary>
        public char IndentChar { get; set; }
    }
}
