using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace MARC.Everest.Exceptions
{
    /// <summary>
    /// An Exception that occurs in the formatter.
    /// </summary>
    /// <remarks>
    /// This exception occurs as the result of an exceptional condition during the formatting of an instance.
    /// </remarks>
    [Serializable]
    public class FormatterException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the System.Exception class.
        /// </summary>
        public FormatterException()
        {

        }
        /// <summary>
        /// Initializes a new instance of the System.Exception class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FormatterException(string message)
            :base(message)
        {

        }
        /// <summary>
        /// Initializes a new instance of the System.Exception class with serialized
        /// data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized
        /// object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual
        /// information about the source or destination.</param>
        protected FormatterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        /// <summary>
        /// Initializes a new instance of the System.Exception class with a specified
        /// error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        ///(Nothing in Visual Basic) if no inner exception is specified.</param>
        public FormatterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
