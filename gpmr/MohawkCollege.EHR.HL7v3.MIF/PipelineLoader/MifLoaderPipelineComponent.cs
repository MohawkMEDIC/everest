/* 
 * Copyright 2008/2009 Mohawk College of Applied Arts and Technology
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
using MohawkCollege.EHR.gpmr.Pipeline;
using System.Xml.Serialization;
using System.IO;
using MohawkCollege.EHR.HL7v3.MIF.MIF10.StaticModel.Serialized;
using MohawkCollege.EHR.HL7v3.MIF.MIF10.Repository;
using MohawkCollege.EHR.HL7v3.MIF.MIF10.Vocabulary;
using System.Xml;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF10.PipelineLoader
{
    /// <summary>
    /// The MIF loader pipeline component is responsible for loading MIF files from the pipeline
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mif")]
    public class MifLoaderPipelineComponent : IPipelineComponent
    {

        // The pipeline this component will run in
        private Pipeline context;

        #region IPipelineComponent Members
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public int ExecutionOrder
        {
            get { return 0; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public PipelineComponentType ComponentType
        {
            get { return PipelineComponentType.Loader; }
        }

        /// <summary>
        /// Initialize the pipeline component
        /// </summary>
        public void Init(Pipeline Context)
        {
            this.context = Context;
            System.Diagnostics.Trace.WriteLine("Mohawk College MIF Loader Pipeline Component\r\nCopyright(C) 2008/2009 Mohawk College of Applied Arts and Technology", "information");
        }

        /// <summary>
        /// Execution of the pipeline
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Execute()
        {
            System.Diagnostics.Trace.WriteLine("\r\nLoading MIF Files", "information");

            // If no mif repository exists in the pipeline data segment, create one
            if (!context.Data.ContainsKey("Mif10Repository"))
                context.Data.Add("Mif10Repository", new PackageRepository());

            PackageRepository repository = (PackageRepository)context.Data["Mif10Repository"];
            List<Package> loadedPackages = new List<Package>();

            // Preserve the stack
            Stack<String> nonLoadedFiles = new Stack<string>();
            Type[] modelTypes = new Type[] { typeof(SerializedStaticModel), typeof(StaticModel.Flat.StaticModel), typeof(VocabularyModel), typeof(DynamicModel.DynamicModel), typeof(Interfaces.CommonModelElementPackage) };

            // Load the mif files into a repository structure ;)
            while (context.InputFiles.Count > 0)
            {
                string fileName = context.InputFiles.Pop();

                Stream fs = null;

                System.Diagnostics.Trace.Write(".","information");

                try
                {
                    fs = File.OpenRead(fileName);

                    // Should we even be looking at this file?
                    XmlDocument tdoc = new XmlDocument();
                    tdoc.Load(fs);
                    bool loaded = false;
                    if (tdoc.DocumentElement.NamespaceURI == "urn:hl7-org:v3/mif")
                    {
                        foreach (Type t in modelTypes)
                        {
                            try
                            {
                                fs.Seek(0, SeekOrigin.Begin);
                                XmlSerializer xsz = new XmlSerializer(t);
                                Object o = xsz.Deserialize(fs);

                                if (o == null)
                                    throw new NotSupportedException("Loader didn't recognize data as this type");
                                else
                                {
                                    Package model = o as Package;

                                    if (model.PackageLocation == null)
                                        throw new InvalidDataException("This model has no package location, aborting pipeline load");

                                    loadedPackages.Add(o as Package);
                                    loaded = true;
                                    break;
                                }
                            }
                            catch (InvalidDataException e)
                            {
                                throw new InvalidDataException(String.Format("Could not load '{0}'", fileName), e);
                            }
                            catch (InvalidOperationException e)
                            {
                                if (!e.Message.Contains(" (1,") && !e.Message.Contains(" (2,"))
                                    System.Diagnostics.Trace.Write(String.Format("{0} -> {1}", e.Message, e.InnerException != null ? e.InnerException.Message : "N/A"), "warn");
                            }
                            catch (Exception)
                            {
                                //System.Diagnostics.Trace.Write(e.Message);
                            }

                        }
                    }

                    if (!loaded)
                    {
                        throw new NotSupportedException(String.Format("File format not recognized : '{0}'", fileName));
                    }
                }
                catch (InvalidDataException e)
                {
                    throw e;
                }
                catch (Exception)
                {
                    //System.Diagnostics.Trace.WriteLine(string.Format("Can't load {0}...", fileName), "warn");
                    nonLoadedFiles.Push(fileName);
                    //System.Diagnostics.Trace.WriteLine(e.ToString(), "warn");
                }

                if (fs != null) fs.Close();
                
            }

            // Sort
            loadedPackages.Sort(repository);

            foreach (Package model in loadedPackages)
                repository.Add(model);

            // Hand off models we couldn't load back to the input file stack so another loader can load it
            while (nonLoadedFiles.Count > 0) context.InputFiles.Push(nonLoadedFiles.Pop());

        }

        #endregion
    }
}
