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
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Interfaces;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers
{
    /// <summary>
    /// Summary of CommonTypeReferenceParser
    /// </summary>
    internal class CommonTypeReferenceParser
    {
        /// <summary>
        /// Private constructor so the compiler will not create a default one for this class.
        /// </summary>
        private CommonTypeReferenceParser() { }
        
        //DOC: Documentation Required
        /// <summary>
        /// Parse a MIF common model element into a CORE common type reference
        /// </summary>
        /// <param name="cme">The common model element to parse</param>
        /// /// <param name="memberOf"></param>
        /// <returns>The COR representation</returns>        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        internal static CommonTypeReference Parse(CommonModelElement cme, ClassRepository memberOf)
        {
            CommonTypeReference retVal = new CommonTypeReference();
            retVal.Name = cme.Name;
            retVal.SortKey = cme.SortKey;

            // Annotations
            if (cme.Annotations != null)
                retVal.Documentation = DocumentationParser.Parse(cme.Annotations.Documentation);

            retVal.DerivedFrom = cme;
            
            // Create a type reference
            retVal.Class = new TypeReference();

            // Entry Class specified?
            if (cme.EntryClass == null)
            {
                // Need to find it
                Package p = (Package)cme.Container.MemberOfRepository.Find(cme.BoundStaticModel);
                if (p is GlobalStaticModel && (p as GlobalStaticModel).OwnedEntryPoint.Count > 0)
                {
                    cme.EntryClass = new SpecializationClass();
                    cme.EntryClass.Name = (p as GlobalStaticModel).OwnedEntryPoint[0].ClassName;
                }
                else if (p != null)
                    throw new InvalidOperationException(string.Format("Can't determine the entry point for '{0}'!", p.PackageLocation.ToString(MifCompiler.NAME_FORMAT)));
                else
                    return null; // Can't find the package
            }

            retVal.Class.Name = string.Format("{0}.{1}", cme.BoundStaticModel.ToString(MifCompiler.NAME_FORMAT), cme.EntryClass.Name);

            // Pseudo Heiarchy to get back to the class repository
            retVal.MemberOf = memberOf;
            retVal.Class.MemberOf = memberOf;

            // Classifier code
            retVal.ClassifierCode = cme.SupplierStructuralDomain.Code.Code;

            // Notify complete
            retVal.FireParsed();

            // Now return 
            return retVal;
        }

        /// <summary>
        /// Parse the specified stub as a common model element reference
        /// </summary>
        internal static CommonTypeReference Parse(StubDefinition stub, ClassRepository classRepository)
        {
            CommonTypeReference retVal = new CommonTypeReference();

            // Create the return value basic parameters
            retVal.Name = stub.Name;
            retVal.SortKey = stub.SortKey;

            if(stub.Annotations != null)
                retVal.Documentation = DocumentationParser.Parse(stub.Annotations.Documentation);

            retVal.DerivedFrom = stub;

            if (stub.SupplierStructuralDomain != null && stub.SupplierStructuralDomain.Code != null)
                retVal.ClassifierCode = stub.SupplierStructuralDomain.Code.Code;

            retVal.Class = new TypeReference();

            // Return value class binding
            if (stub.TypeStaticModel != null)
            {
                Package p = (Package)stub.Container.MemberOfRepository.Find(stub.TypeStaticModel);
                if (p is GlobalStaticModel && (p as GlobalStaticModel).OwnedEntryPoint.Count > 0)
                {
                    string entryClassName = stub.EntryClass;

                    if(entryClassName == null)
                        throw new InvalidOperationException(string.Format("Package '{1}' points to a valid model with more than one entry point, however no entryClass was specified ", stub.TypeStaticModel.ToString(MifCompiler.NAME_FORMAT)));

                    // Did we find the class with an entry point?
                    var search = (p as GlobalStaticModel).OwnedClass.Find(o=>o.Choice is MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class && (o.Choice as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class).Name == entryClassName);
                    var entryClass = search.Choice as MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Flat.Class;

                    // Entry class is not null
                    if (entryClass == null)
                        throw new InvalidOperationException(string.Format("Can't find entry class '{0}' in package '{1}', or entry class is not a class", entryClassName, stub.TypeStaticModel.ToString(MifCompiler.NAME_FORMAT)));

                    retVal.Class.Name = string.Format("{0}.{1}", stub.TypeStaticModel.ToString(MifCompiler.NAME_FORMAT), entryClass.Name);
                }
                else if (p is GlobalStaticModel && (p as GlobalStaticModel).OwnedEntryPoint.Count == 1)
                    retVal.Class.Name = string.Format("{0}.{1}", stub.TypeStaticModel.ToString(MifCompiler.NAME_FORMAT), (p as GlobalStaticModel).OwnedEntryPoint[0].Name);
                else if (p != null)
                    throw new InvalidOperationException(string.Format("Can't find static model '{0}'!", p.PackageLocation.ToString(MifCompiler.NAME_FORMAT)));
                else
                    return null; // Can't find the package
            }

            // Pseudo Heiarchy to get back to the class repository
            retVal.MemberOf = classRepository;
            retVal.Class.MemberOf = classRepository;

            // Notify complete
            retVal.FireParsed();

            // Now return 
            return retVal;
        }
    }
}