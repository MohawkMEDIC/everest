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
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler
{
    /// <summary>
    /// Used for compiling different types of model elements
    /// </summary>
    internal interface IModelElementCompiler : ICompiler
    {

        /// <summary>
        /// The derivation suppliers from the package
        /// </summary>
        Dictionary<String, String> DerivationSuppliers { get; set; }

        /// <summary>
        /// Gets the type of model this compiler will compile
        /// </summary>
        Type ModelType { get; }

        /// <summary>
        /// Get the package that will contain this class
        /// </summary>
        MohawkCollege.EHR.gpmr.COR.SubSystem TargetPackage { set; get; }

        /// <summary>
        /// Get or set the target of compilation
        /// </summary>
        ModelElement ModelElement { set; }

        /// <summary>
        /// Get or set the package that owns this model element
        /// </summary>
        Package DefinitionPackage { get; set; }
    }
}