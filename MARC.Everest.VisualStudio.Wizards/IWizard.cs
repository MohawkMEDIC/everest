/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
    /// Summary for IWizard
    /// </summary>
    public interface IWizard
    {

        /// <summary>
        /// Get or set the parameters for this wizard
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        List<WizardParameter> Parameters { get; set; }

        /// <summary>
        /// Get the current stage
        /// </summary>
        IWizardStage CurrentStage { get;  }

        /// <summary>
        /// Show a wizard stage
        /// </summary>
        void ShowStage(IWizardStage stage, WizardDirection dir);

        /// <summary>
        /// Disable next button
        /// </summary>
        void DisableNext();
        /// <summary>
        /// Enable next button
        /// </summary>
        void EnableNext();
        void DisableBack();
        void EnableBack();
    }
}