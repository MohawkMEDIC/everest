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
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Responsible for parsing template parameters from the MIF structures into a TypeParameter that 
    /// can be used by classes to mimic generics
    /// </summary>
    internal class TypeParameterParser
    {
        /// <summary>
        /// Private constructor so the compiler will not create a default one for this class.
        /// </summary>
        private TypeParameterParser() { }
        /// <summary>
        /// Parses a StaticModelClassTemplateParameter into a COR type parameter
        /// </summary>
        /// <param name="parm">The static mode template parameter to parse from the MIF</param>
        /// <returns>The compiled type parameter</returns>
        internal static TypeParameter Parse(StaticModelClassTemplateParameter parm)
        {
            TypeParameter retVal = parm.Interface == null ? new TypeParameter() : (TypeParameter)TypeReferenceParser.Parse(parm.Interface);
            
            // Documentation
            if (parm.Annotations != null)
                retVal.Documentation = DocumentationParser.Parse(parm.Annotations.Documentation);

            // Name of the parameter
            retVal.ParameterName = parm.Name;

            // Business name
            foreach (BusinessName bn in parm.BusinessName ?? new List<BusinessName>())
                if (bn.Language == MifCompiler.Language || bn.Language == null)
                    retVal.BusinessName = bn.Name;

            return retVal ;
            
        }
    }
}