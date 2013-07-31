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
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.Pipeline;
using System.Xml.Serialization;
using System.IO;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.StaticModel.Serialized;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Repository;
using MohawkCollege.EHR.HL7v3.MIF.MIF20.Vocabulary;
using System.Diagnostics;
using System.Xml.Schema;
using System.Reflection;
using System.Xml;
using MARC.Everest.Xml;

namespace MohawkCollege.EHR.HL7v3.MIF.MIF20.PipelineLoader
{
    /// <summary>
    /// The MIF loader pipeline component is responsible for loading MIF files from the pipeline
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mif")]
    public class MifLoaderPipelineComponent : IPipelineComponent
    {

        // The pipeline this component will run in
        private Pipeline context;

        // Schemas 
        private XmlSchemaSet m_schemas = null;

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
            System.Diagnostics.Trace.WriteLine("Mohawk College MIF Loader Pipeline Component\r\nCopyright(C) 2008-2013 Mohawk College of Applied Arts and Technology", "information");
        }

        /// <summary>
        /// Execution of the pipeline
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Execute()
        {
            System.Diagnostics.Trace.WriteLine("\r\nLoading MIF Files", "information");

            // If no mif repository exists in the pipeline data segment, create one
            if (!context.Data.ContainsKey("MIF20Repository"))
                context.Data.Add("MIF20Repository", new PackageRepository());

            PackageRepository repository = (PackageRepository)context.Data["MIF20Repository"];
            List<PackageArtifact> loadedPackages = new List<PackageArtifact>();

            // Preserve the stack
            Stack<String> nonLoadedFiles = new Stack<string>();
            Type[] modelTypes = new Type[] { typeof(StaticModel.Flat.GlobalStaticModel), 
                typeof(DynamicModel.GlobalInteraction), 
                typeof(Vocabulary.GlobalCodeSystem), 
                typeof(Vocabulary.GlobalCodeSystemSupplement),  
                typeof(Vocabulary.GlobalValueSet), 
                typeof(Vocabulary.GlobalVocabularyModel), 
                typeof(Interfaces.CommonModelElementPackage),
                typeof(SerializedStaticModel)
            };

            MifTransformer transformer = new MifTransformer();

            // Load the mif files into a repository structure ;)
            while (context.InputFiles.Count > 0)
            {
                string fileName = context.InputFiles.Pop();

               
                Stream fs = null;

                System.Diagnostics.Trace.Write(".","information");

                try
                {

                    // JF: Revised so that we can load multiple versions of the MIF using compiler transforms
                    fs = transformer.GetFile(fileName);
                    if (fs == null)
                        continue;

                    // Validate
                    if (fs.CanSeek)
                    {
                        ValidateMifFile(fs, fileName);
                        fs.Seek(0, SeekOrigin.Begin);
                    }

                    bool loaded = false;
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
                                PackageArtifact model = o as PackageArtifact;

                                if (model.PackageLocation == null)
                                    throw new InvalidDataException("This model has no package location, aborting pipeline load");

                                loadedPackages.Add(o as PackageArtifact);
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
                            if (e.InnerException != null && e.InnerException is OperationCanceledException)
                                throw e.InnerException;
                        }
                        catch (OperationCanceledException e)
                        {
                            throw e;
                        }
                        catch (Exception)
                        {
                            //System.Diagnostics.Trace.Write(e.Message);
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
                catch (OperationCanceledException e)
                {
                    throw new OperationCanceledException(String.Format("Can't load MIF file '{0}' into repository", fileName), e);
                }
                catch (XmlSchemaException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Can't load {0}...", fileName), "warn1");
                    nonLoadedFiles.Push(fileName);
                    System.Diagnostics.Trace.WriteLine(e.ToString(), "warn1");
                }

                if (fs != null) fs.Close();
                
            }

            // Sort
            loadedPackages.Sort(repository);

            foreach (PackageArtifact model in loadedPackages)
                repository.Add(model);

            // Warn if a transform has been applied
            if (transformer.DidTransform)
                Trace.WriteLine("\r\n--!! TRANSFORM APPLIED, MIF CONTENTS IN PIPELINE MAY NOT MATCH PHYSICAL FILE !!--\r\n", "warn");

            // Hand off models we couldn't load back to the input file stack so another loader can load it
            while (nonLoadedFiles.Count > 0) context.InputFiles.Push(nonLoadedFiles.Pop());

        }

        
        /// <summary>
        /// Validate the MIF file
        /// </summary>
        public void ValidateMifFile(Stream fileStream, string fileName)
        {
            
            string mifDirectory = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "schemas"), "mif2.1.4");
            // First, validate the schema directory exists
            if (context.Mode != Pipeline.OperationModeType.Strict)
            {
                Trace.Write("Not operating in strict mode, not applying MIF validation", "warn1");
                return;
            }
            if (!Directory.Exists(mifDirectory))
                throw new InvalidOperationException("Cannot find the MIF schemas, ensure that the Schemas\\mif2.1.4 directory exists");

            // Load the schemas
            if (m_schemas == null)
            {
                m_schemas = new XmlSchemaSet();
                m_schemas.ValidationEventHandler += new ValidationEventHandler(m_schemas_ValidationEventHandler);
                foreach (var file in Directory.GetFiles(mifDirectory, "*.xsd"))
                {
                    XmlReader schemaReader = XmlReader.Create(file, new XmlReaderSettings() { ProhibitDtd = false });
                    try
                    {
                        m_schemas.Add("urn:hl7-org:v3/mif2", schemaReader);
                    }
                    finally
                    {
                        schemaReader.Close();       
                    }
                }
                m_schemas.Compile();
            }

            // Sanity check
            if (!m_schemas.IsCompiled)
                throw new InvalidOperationException("The schema set must compile successfully for execution to continue");

            // Reader
            XmlStateReader xr = null;
            try
            {
               
                // Create settings
                var settings = new XmlReaderSettings()
                {
                    ProhibitDtd = false,
                    ValidationType = ValidationType.Schema,
                    Schemas = m_schemas
                };
                
                
                // Is the object valid
                bool isValid = true;
                settings.ValidationEventHandler += new ValidationEventHandler(
                    delegate(object sender, ValidationEventArgs e)
                    {
                        Trace.WriteLine(String.Format("{0} : {1}", Path.GetFileName(fileName), e.Message), e.Severity == XmlSeverityType.Error ? "error" : "warn");
                        isValid &= e.Severity == XmlSeverityType.Warning;
                    }
                    );

                // Create the reader
                xr = new XmlStateReader(XmlReader.Create(fileStream, settings));

                // Read
                while (xr.Read()) ;

                if (!isValid)
                    throw new XmlSchemaValidationException("File failed basic schema check");
            }
            finally
            {
                if (xr != null)
                    xr.Close();
            }

            
        }

        /// <summary>
        /// Schema validation event handler
        /// </summary>
        void m_schemas_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Trace.WriteLine(String.Format("Schema Compile {0}: {1}", e.Severity, e.Message), "error");
        }

        #endregion
    }
}