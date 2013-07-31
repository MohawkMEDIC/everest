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
 * Date: 11-11-2010
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Formatters.XML.ITS1
{
    /// <summary>
    /// Types of settings on the type formatter
    /// </summary>
    public enum SettingsType
    {
        /// <summary>
        /// Permits RMIM based classes to impose a Flavor on rendered datatypes
        /// </summary>
        AllowFlavorImposing = 1,
        /// <summary>
        /// Permits RMIM based classes to impose a default SupplierDomain on rendered datatypes
        /// </summary>
        AllowSupplierDomainImposing = 2,
        /// <summary>
        /// Permits RMIM based classes to impose a default UpdateMode on rendered datatypes
        /// </summary>
        AllowUpdateModeImposing = 4,
        /// <summary>
        /// When set prevents the formatter from emitting xsi:nil
        /// </summary>
        SuppressXsiNil = 64

#if !WINDOWS_PHONE
,
        /// <summary>
        /// Signals that the formatter should use the reflection method of formatting
        /// rather than code-dom
        /// </summary>
        UseReflectionFormat = 8,
        /// <summary>
        /// Signals that the formatter should use the generator (creating of code in memory)
        /// method of formatting
        /// </summary>
        UseGeneratorFormat = 16,
        /// <summary>
        /// Optimizes the formatter for long running applications where first serialization of 
        /// messages is not an issue
        /// </summary>
        EnableDeepLearning = 32,
        /// <summary>
        /// The default settings for a uniprocessor machine
        /// </summary>
        /// <remarks>
        /// Sets AllowFlavorImposing, AllowUpdateModeImposing, AllowSupplierDomainImposing, and UseReflectionFormat
        /// </remarks>
        DefaultUniprocessor = AllowFlavorImposing | AllowUpdateModeImposing | AllowSupplierDomainImposing | UseReflectionFormat,
        /// <summary>
        /// The default settings for a multi processor machine
        /// </summary>
        /// <remarks>
        /// Sets all options from DefaultUniprocessor plus UseGeneratorFormat
        /// </remarks>
        DefaultMultiprocessor = DefaultUniprocessor | UseGeneratorFormat,
        /// <summary>
        /// Default settings for older versions of everest
        /// </summary>
        DefaultLegacy = AllowFlavorImposing | AllowSupplierDomainImposing | AllowUpdateModeImposing | UseGeneratorFormat
#endif
    }
}
