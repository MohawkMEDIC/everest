using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Sherpas.Exception
{
    /// <summary>
    /// Represents a condition where a template is registered that already exists
    /// </summary>
    public class DuplicateTemplateException : System.Exception
    {

        /// <summary>
        /// Gets the template id that was conflicted
        /// </summary>
        public String TemplateId { get; private set; }

        /// <summary>
        /// Gets the type that was attempted to be registered
        /// </summary>
        public Type ConflictingType { get; private set; }

        /// <summary>
        /// Gets the type that is already registered
        /// </summary>
        public Type ExistingType { get; set; }

        /// <summary>
        /// Creates new DuplicateTemplateException instance
        /// </summary>
        public DuplicateTemplateException(String templateId, Type conflictingType, Type existingType)
        {
            this.TemplateId = templateId;
            this.ConflictingType = conflictingType;
            this.ExistingType = existingType;
        }

        /// <summary>
        /// Gets the human readable message that represents this exception
        /// </summary>
        public override string Message
        {
            get
            {
                return String.Format("Attempted to register template '{0}' for type '{1}' which has already been registered to '{2}'", TemplateId, ConflictingType, ExistingType);
            }
        }

    }
}
