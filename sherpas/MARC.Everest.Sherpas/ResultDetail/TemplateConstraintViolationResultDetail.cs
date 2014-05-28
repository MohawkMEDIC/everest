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
 * Date: 20-4-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;

namespace MARC.Everest.Sherpas.ResultDetail
{

    /// <summary>
    /// Represents a violation of a template constraint
    /// </summary>
    public abstract class TemplateConstraintViolationResultDetail : FormalConstraintViolationResultDetail
    {

        /// <summary>
        /// Creates a new constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail() : base(null)
        {

        }

        /// <summary>
        /// Creates a new template constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail(ResultDetailType type, String message)
            : this(type, message, null, null)
        {
        }

        /// <summary>
        /// Creates a new template constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail(ResultDetailType type, String message, String location) : this(type, message, location, null)
        {
        }

        /// <summary>
        /// Template constraint violation result detail
        /// </summary>
        public TemplateConstraintViolationResultDetail(ResultDetailType type, String message, String location, System.Exception cause) : base(type, message, location, cause)
        {
        }

    }
}
