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
using MohawkCollege.EHR.HL7v3.MIF.MIF20.DynamicModel;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using MohawkCollege.EHR.HL7v3.MIF.MIF20;
using MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Parsers;
using MohawkCollege.EHR.gpmr.COR;
using System.Xml;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Compiler.Mif20.Compilers
{
    /// <summary>
    /// Summary of InteractionCompiler
    /// </summary>
    public class InteractionCompiler : IPackageCompiler
    {
        #region IPackageCompiler Members

        private GlobalInteraction interactionModel;
        private PackageRepository repository;

        /// <summary>
        /// The type of package that this compiler processes
        /// </summary>
        public Type PackageType
        {
            get { return typeof(GlobalInteraction); }
        }

        /// <summary>
        /// Sets the package that this compiler will process
        /// </summary>
        public object Package
        {
            set { interactionModel = value as GlobalInteraction; }
        }

        /// <summary>
        /// Gets or sets the MIF package repository
        /// </summary>
        public object PackageRepository
        {
            get
            {
                return repository;
            }
            set
            {
                repository = value as PackageRepository;
            }
        }

        #endregion

        static List<String> processingStack = new List<string>();

        #region ICompiler Members

        /// <summary>
        /// Get or set the compiled class repository
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public MohawkCollege.EHR.gpmr.COR.ClassRepository ClassRepository { get; set; }

        /// <summary>
        /// Compile the package
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Compile()
        {

            // Already processing?
            if (interactionModel == null || processingStack.Contains(interactionModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)))
                return;

            // Add to the processing stack
            processingStack.Add(interactionModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT));

            // Otput the name of the package.
            System.Diagnostics.Trace.WriteLine(string.Format("Compiling interaction model package '{0}'...", interactionModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)), "debug");

            // Check if the package has already been "compiled"
            if (ClassRepository.ContainsKey(interactionModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT)))
                return; // Already compiled

            // Process the interaction
            MohawkCollege.EHR.gpmr.COR.Interaction interaction = new MohawkCollege.EHR.gpmr.COR.Interaction();
            interaction.Name = interactionModel.PackageLocation.ToString(MifCompiler.NAME_FORMAT);
            //interaction.Realm = interactionModel.PackageLocation.Realm;

            // Process business names
            foreach (BusinessName bn in interactionModel.BusinessName ?? new List<BusinessName>())
                if (bn.Language == MifCompiler.Language || bn.Language == null)
                    interaction.BusinessName = bn.Name;

            // Process documentation
            if(interactionModel.Annotations != null)
                interaction.Documentation = DocumentationParser.Parse(interactionModel.Annotations.Documentation);

            // Set the derivation from pointer
            interaction.DerivedFrom = interactionModel;

            // Trigger event
            interaction.TriggerEvent = interactionModel.InvokingTriggerEvent.ToString(MifCompiler.NAME_FORMAT);

            // Types
            TypeReference tr = new TypeReference();

            // Has the entry class been created yet?
            if (!ClassRepository.ContainsKey(interactionModel.ArgumentMessage.ToString(MifCompiler.NAME_FORMAT)))
                // Process
                PackageParser.Parse(interactionModel.ArgumentMessage.ToString(MifCompiler.NAME_FORMAT), repository, ClassRepository);

            var entry = (ClassRepository[interactionModel.ArgumentMessage.ToString(MifCompiler.NAME_FORMAT)] as MohawkCollege.EHR.gpmr.COR.SubSystem);

            // Could we even find the model?
            if (entry == null)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Could not find the argument message '{0}', interaction '{1}' can't be processed",
                    interactionModel.ArgumentMessage.ToString(MifCompiler.NAME_FORMAT), interaction.Name), "error");
                return;
            }
            else if (entry.EntryPoint.Count == 0)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Argument message '{0}' must have an entry point, interaction '{1}' can't be processed",
                    entry.Name, interaction.Name), "error");
                return;
            }
            else if (entry.EntryPoint.Count != 1)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Ambiguous entry point for argument message '{0}', interaction '{1}' can't be processed", entry.Name, interaction.Name), "error");
                return;
            }

            // Set the entry class
            tr = entry.EntryPoint[0].CreateTypeReference();
            tr.MemberOf = ClassRepository; // Set member of property
            ProcessTypeParameters(interactionModel.ArgumentMessage.ParameterModel, tr, interaction);
            interaction.MessageType = tr;

            #region Response types
            if (interactionModel.ReceiverResponsibilities != null)
            {
                // Create the array
                interaction.Responses = new List<MohawkCollege.EHR.gpmr.COR.Interaction>();

                //  Iterate through
                foreach (ReceiverResponsibility rr in interactionModel.ReceiverResponsibilities)
                {
                    if (rr.InvokeInteraction == null)
                    {
                        System.Diagnostics.Trace.WriteLine("Invoking interaction on receiver responsibility is missing", "warn");
                        continue;
                    }

                    // Does the receiver responsibility exist in the class repository
                    if (!ClassRepository.ContainsKey(rr.InvokeInteraction.ToString(MifCompiler.NAME_FORMAT)))
                    {
                        InteractionCompiler icc = new InteractionCompiler();
                        icc.PackageRepository = repository;
                        icc.Package = repository.Find(o => o.PackageLocation.ToString(MifCompiler.NAME_FORMAT) == rr.InvokeInteraction.ToString(MifCompiler.NAME_FORMAT));
                        icc.ClassRepository = ClassRepository;
                        icc.Compile();
                    }

                    MohawkCollege.EHR.gpmr.COR.Feature foundFeature = null;
                    if (ClassRepository.TryGetValue(rr.InvokeInteraction.ToString(MifCompiler.NAME_FORMAT), out foundFeature))
                    {

                        // Reason element for documentation
                        if (rr.Reason != null && (rr.Reason.Language == MifCompiler.Language || rr.Reason.Language == null)
                            && (rr.Reason.MarkupElements != null || rr.Reason.MarkupText != null))
                        {
                            MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation td= new MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation()
                            {
                                Title = "Reason", Name = "Reason", Text = new List<string>()
                            };
                            if (rr.Reason.MarkupText != null)
                                td.Text.Add(rr.Reason.MarkupText);
                            if(rr.Reason.MarkupElements != null)
                                foreach(XmlElement xe in rr.Reason.MarkupElements)
                                    td.Text.Add(xe.OuterXml.Replace(" xmlns:html=\"http://www.w3.org/1999/xhtml\"", "").Replace("html:", ""));

                            // Append the documentation
                            if (interaction.Documentation == null)
                                interaction.Documentation = new MohawkCollege.EHR.gpmr.COR.Documentation();
                            if (interaction.Documentation.Other == null)
                                interaction.Documentation.Other = new List<MohawkCollege.EHR.gpmr.COR.Documentation.TitledDocumentation>();
                            interaction.Documentation.Other.Add(td);
                        }
                        interaction.Responses.Add(foundFeature as MohawkCollege.EHR.gpmr.COR.Interaction);
                    }
                    else
                        System.Diagnostics.Trace.WriteLine(String.Format("Can't find response interaction '{0}'...", rr.InvokeInteraction.ToString(MifCompiler.NAME_FORMAT)), "warn");


                }
            }
            #endregion

            // Fire the complete method
            interaction.FireParsed();
        }

        #endregion

        //DOC: Documentation Required
        /// <summary>
        /// Append the traversal name to the list of alternate traversal names on all properties within the 
        /// class <paramref name="ClassRef"/>.
        /// </summary>
        /// <param name="ClassRef">The class to append a traversal to</param>
        /// <param name="ParameterName">The name of the type parameter to append traversal names to</param>
        /// <param name="TraversalName">The traversal name</param>
        /// <param name="CaseWhen">When the traversal name should be applied</param>
        /// <param name="InteractionOwner"></param>        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private void AppendTraversalName(TypeReference ClassRef, string ParameterName, string TraversalName, TypeReference CaseWhen, MohawkCollege.EHR.gpmr.COR.Interaction InteractionOwner, Stack<String> processStack)
        {
            if (processStack.Contains(ClassRef.ToString()))
                return;

            processStack.Push(ClassRef.ToString());

            // Iterate through each of the class contents
            foreach(ClassContent cc in ClassRef.Class.Content)
                if (cc is Property)
                {
                    // Assign Alternative traversal name if the type of property matches the paramter name 
                    // and the new traversal name is not the same as the old 
                    if ((cc as Property).Type.Name == ParameterName &&
                        TraversalName != cc.Name )
                        (cc as Property).AddTraversalName(TraversalName, CaseWhen, InteractionOwner);
                    else if ((cc as Property).Type.Class != null)
                        AppendTraversalName((cc as Property).Type, ParameterName, TraversalName, CaseWhen, InteractionOwner, processStack);
                }

            if (processStack.Pop() != ClassRef.ToString())
                throw new PipelineExecutionException(MifCompiler.hostContext, Pipeline.PipelineStates.Error, MifCompiler.hostContext.Data,
                    new Exception("Error occurred traversing traversal name to populate parameter"));
        }
        /// <summary>
        /// Process type parameters and create a type reference
        /// </summary>
        /// <param name="parms">The parameter models to process</param>
        /// <param name="baseRef">The type reference to add to</param>
        /// <param name="ownerInteraction"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private void ProcessTypeParameters(List<ParameterModel> parms, TypeReference baseRef, MohawkCollege.EHR.gpmr.COR.Interaction ownerInteraction)
        {
            if (parms != null && baseRef.Class != null && baseRef.Class.TypeParameters != null && parms.Count != baseRef.Class.TypeParameters.Count)
                Trace.WriteLine(
                    string.Format("The argument message '{0}.{1}' requires {2} parameter messages however interaction '{3}' only specifies {4}",
                    baseRef.Class.ContainerName, baseRef.Class.Name, baseRef.Class.TypeParameters.Count, ownerInteraction.Name, parms.Count)
                    , "warn");
            else if (parms == null || parms.Count == 0) return; // Check for null

            // Setup the parameters
            foreach (ParameterModel p in parms)
            {
                // Check if the parameter model exists
                if (!ClassRepository.ContainsKey(p.ToString(MifCompiler.NAME_FORMAT)))
                    PackageParser.Parse(p.ToString(MifCompiler.NAME_FORMAT), repository, ClassRepository); // Process the package if it doesn't

                // Check again, if this fails all hell breaks loose
                var model = (ClassRepository[p.ToString(MifCompiler.NAME_FORMAT)] as MohawkCollege.EHR.gpmr.COR.SubSystem);
                if (model == null)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Could not find the parameter model '{0}'",
                        p.ToString(MifCompiler.NAME_FORMAT)), "error");
                    return;
                }
                else if (model.EntryPoint.Count == 0)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Parameter model '{0}' must have an entry point",
                        p.ToString(MifCompiler.NAME_FORMAT)), "error");
                    return;
                }
                else if (model.EntryPoint.Count != 1)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Ambiguous entry point for parameter model '{0}'",
                        p.ToString(MifCompiler.NAME_FORMAT)), "error");
                    return;
                }

                // Entry class for p
                TypeReference parmRef = model.EntryPoint[0].CreateTypeReference();

                // Find any reference and set an alternate traversal name for that property
                if (p.Specialization.Count == 0)
                    AppendTraversalName(baseRef, p.ParameterName, p.TraversalName, parmRef, ownerInteraction, new Stack<string>());
                else
                    ProcessSpecializations(p, p.Specialization, baseRef, ownerInteraction, null);

                // Process Children
                ProcessTypeParameters(p.ParameterModel, parmRef, ownerInteraction);

                // Assign for tr as a parameter reference
                try
                {
                    baseRef.AddGenericSupplier(p.ParameterName, parmRef);
                }
                catch (ArgumentException e) // This is thrown when there are more than one supplier binding
                {
                    // Was more than one specified
                    if (baseRef.GenericSupplier.Exists(o => (o as TypeParameter).ParameterName == p.ParameterName)) 
                    {
                        //baseRef.GenericSupplier.RemoveAll(o => (o as TypeParameter).ParameterName == p.ParameterName);  // remove the existing type reference
                        // Add the generic supplier manually for the new type
                        baseRef.AddGenericSupplier(p.ParameterName, parmRef, false);
                        Trace.WriteLine(String.Format("Generic supplier {0} has been specified more than once, will use base object in it's place", p.ParameterName), "warn");
                    }
                }
                catch (Exception e)
                {
                    // JF - Some UV models attempt to bind to classes that don't support binding
                    if (baseRef.Class.TypeParameters == null)
                    {
                        System.Diagnostics.Trace.WriteLine(String.Format("{0} can't force bind because the target class has not template parameters", e.Message), "error");
                        if (MifCompiler.hostContext.Mode == Pipeline.OperationModeType.Quirks)
                            System.Diagnostics.Trace.WriteLine(String.Format("{0} will ignore this binding in order to continue. This interaction will effectively be useless", ownerInteraction.Name));
                        else
                            throw new InvalidOperationException(String.Format("Cannot bind parameter '{0}' to class '{1}' because '{1}' does not support templates", parmRef.Name, baseRef.Name));
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine(String.Format("{0} will try force binding", e.Message), "error");
                        foreach (var t in baseRef.Class.TypeParameters)
                            if (baseRef.GenericSupplier.Find(o => o.Name.Equals(t.ParameterName)) == null)
                            {
                                baseRef.AddGenericSupplier(t.ParameterName, parmRef);
                                System.Diagnostics.Trace.WriteLine(String.Format("Bound {0} to {1} in {2}", parmRef, t.ParameterName, baseRef), "warn");
                                break;
                            }
                    }
                }
                
            }
        }

        /// <summary>
        /// Process specializations
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "interactionModel"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        private void ProcessSpecializations(ParameterModel p, List<AssociationEndSpecialization> list, TypeReference baseRef, MohawkCollege.EHR.gpmr.COR.Interaction interactionModel, TypeReference hint)
        {
            TypeReference parmRef = null;
            foreach (var specialization in list) // This shouldn't ever be done ... ever, but this is v3 land
            {
                Class specClass = (ClassRepository[p.ToString(MifCompiler.NAME_FORMAT)] as MohawkCollege.EHR.gpmr.COR.SubSystem).OwnedClasses.Find(c => c.Name == specialization.ClassName);
                if(specClass == null && hint != null)
                {
                    // Find all sub-classes and see if the hint contains them
                    TypeReference specClassRef = hint.Class.SpecializedBy.Find(o => o.Class.Name == specialization.ClassName);
                    // Make the reference a concreate COR class 
                    if (specClassRef != null) specClass = specClassRef.Class;
                }
                if (specClass == null) // Do a CMET lookup
                {
                    MohawkCollege.EHR.gpmr.COR.Feature cmetFeature = null;
                    if (ClassRepository.TryGetValue(specialization.ClassName, out cmetFeature))
                        specClass = (cmetFeature as CommonTypeReference).Class.Class;
                    else
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("Can't find specialization '{0}' for parameter '{1}' this traversal will be ignored.", specialization.ClassName, p.ParameterName), "warn");
                        continue;
                    }
                }

                parmRef = specClass.CreateTypeReference();
                // Append the traversal name
                AppendTraversalName(baseRef, p.ParameterName, specialization.TraversalName, parmRef, interactionModel, new Stack<string>());
                if (specialization.Specialization.Count > 0)
                    ProcessSpecializations(p, specialization.Specialization, baseRef, interactionModel, parmRef);
            }
        }
    }
}