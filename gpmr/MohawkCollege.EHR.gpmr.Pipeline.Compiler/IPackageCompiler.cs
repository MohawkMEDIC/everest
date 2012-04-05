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

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler
{
    /// <summary>
    /// The ICompiler interface defines a standard method of defining a compiler utility class
    /// that transforms a MIF format file to the CR representation
    /// </summary>
    internal interface IPackageCompiler : ICompiler
    {
        /// <summary>
        /// The name of the model type that this class handles
        /// </summary>
        Type PackageType { get; }

        /// <summary>
        /// The package this compiler is going to transform
        /// </summary>
        object Package { set; }

        /// <summary>
        /// Get or set the package repository
        /// </summary>
        object PackageRepository { get; set; }
    }
}