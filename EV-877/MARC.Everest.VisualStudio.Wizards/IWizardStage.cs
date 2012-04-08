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
using System.Windows.Forms;

namespace MARC.Everest.VisualStudio.Wizards.Interfaces
{
    public enum WizardDirection
    {
        Forward,
        Backward
    }

    /// <summary>
    /// Summary for IWizardStage
    /// </summary>
    public interface IWizardStage 
    {

      
        /// <summary>
        /// Gets or sets the wizard host
        /// </summary>
        IWizard Host { get; set; }

        /// <summary>
        /// The name of the wizard
        /// </summary>
        string StageName { get; }

        /// <summary>
        /// If true, then the finish button is shown
        /// </summary>
        bool IsTerminal { get; }

        /// <summary>
        /// If true then the exit button is shown
        /// </summary>
        bool CanExit { get; }
        
        /// <summary>
        /// Next stage to 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Next")]
        IWizardStage Next { get;  }

        /// <summary>
        /// Previous stage
        /// </summary>
        IWizardStage Previous { get; }

        /// <summary>
        /// This wizard stage is being shown
        /// </summary>
        /// <param name="previous">The stage being hidden</param>
        void Shown(IWizardStage previous, WizardDirection direction);

        /// <summary>
        /// Called when the stage is being hidden
        /// </summary>
        void Hiding();
    }
}