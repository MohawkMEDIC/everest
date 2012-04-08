/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: $date$
 **/
using System;
using System.Collections.Generic;
using System.Text;

namespace MARC.Everest.VisualStudio.Wizards.Interfaces
{
    /// <summary>
    /// Parameter that is populated for the wizard parameter
    /// </summary>
    public class WizardParameter
    {
        /// <summary>
        /// The name of the parameter
        /// </summary>
        public string Name { get; set; }

    }
}