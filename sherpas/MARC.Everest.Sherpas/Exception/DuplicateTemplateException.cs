/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: fyfej
 * Date: 4-2-2014
 */
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
