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
using MARC.Everest.VisualStudio.Wizards.Interfaces;
using System.ComponentModel;
using MARC.Everest.VisualStudio.Wizards.Proxy;

namespace MARC.Everest.VisualStudio.Wizards.Parameters
{
    /// <summary>
    /// Parameter identifies the type of project being created
    /// </summary>
    public class InteractionDataParameter : WizardParameter
    {
        /// <summary>
        /// Structures
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<StructureListProxyClass.StructureData> Structures { get; set; }
        /// <summary>
        /// The selected structure data element to use
        /// </summary>
        public int SelectedIndex { get; set; }
        /// <summary>
        /// String of C# code dictating how the message should be populated
        /// </summary>
        public string PopulationString { get; set; }
    }
}