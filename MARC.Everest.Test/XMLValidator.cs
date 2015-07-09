/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 3-6-2013
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Xml.Schema;
using System.Resources;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;
using System.Reflection;
using System.Diagnostics;

namespace MARC.Everest.Test
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "XML")]
    public static class XMLValidator
    {
        static Dictionary<string, XmlReaderSettings> ValidationSettings = new Dictionary<string,XmlReaderSettings>();
        /// <summary>
        /// HACK: Need to find a better way of doing this
        /// </summary>
        static List<String> supportedReleases = new List<string>() {
            "R02_04_01",
            "R02_04_02",
            "R02_04_03",
            "NE2008",
            "NE2010",
            "CDAr2"
        };

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        static XMLValidator()
        {

          
            //try
            //{
            //    foreach (String s in Directory.GetFiles(Environment.CurrentDirectory, "*.xsd"))
            //    {
            //        ValidationSettings.Schemas.Add(null, s);
            //    }
            //    foreach (String d in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"..\coreschemas"), "*.xsd"))
            //    {
            //        ValidationSettings.Schemas.Add(null, d);

            //    }


            //}
            //catch (Exception SchemaException)
            //{
            //    Assert.Fail("Exception: " + SchemaException.ToString());
            //}
        }

        static XmlReaderSettings GetSettings(String release)
        {

            XmlReaderSettings retVal = null;
            if (ValidationSettings.TryGetValue(release, out retVal))
                return retVal;

            string tmpDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tmpDir);

            Assembly asm = typeof(XMLValidator).Assembly;

            try
            {
                foreach (var item in asm.GetManifestResourceNames())
                {
                    if (!item.EndsWith("zip"))
                        continue;

                    string itemRelease = item.Replace("MARC.Everest.Test.Resources.", "").Replace(".zip", "");
                    if (itemRelease != release)
                        continue;

                    ZipInputStream zis = null;
                    Tracer.Trace(item);

                    try
                    {
                        zis = new ZipInputStream(asm.GetManifestResourceStream(item));
                        retVal = new XmlReaderSettings();

                        // Prepare the unzipping operation
                        ZipEntry entry = null;
                        String basePath = Path.Combine(tmpDir, item);
                        if (!Directory.Exists(basePath))
                            Directory.CreateDirectory(basePath);

                        List<String> files = new List<string>(10);

                        // Unzip the rmim package
                        while ((entry = zis.GetNextEntry()) != null)
                        {
                            if (entry.IsDirectory) // entry is a directory
                            {
                                string dirName = Path.Combine(basePath, entry.Name);
                                if (!Directory.Exists(dirName))
                                    Directory.CreateDirectory(dirName);
                            }
                            else if (entry.IsFile) // entry is file, so extract file.
                            {
                                string fName = Path.Combine(basePath, entry.Name);
                                FileStream fs = null;
                                try
                                {
                                    fs = File.Create(fName);
                                    byte[] buffer = new byte[2048]; // 2k buffer
                                    int szRead = 2048;
                                    while (szRead > 0)
                                    {
                                        szRead = zis.Read(buffer, 0, buffer.Length);
                                        if (szRead > 0)
                                            fs.Write(buffer, 0, szRead);
                                    }
                                }
                                finally
                                {
                                    if (fs != null)
                                        fs.Close();
                                }

                                if (fName.EndsWith(".xsd"))
                                    files.Add(fName);
                            }
                        }

                        foreach (var fName in files)
                            retVal.Schemas.Add("urn:hl7-org:v3", fName);

                        retVal.Schemas.ValidationEventHandler += new ValidationEventHandler(Schemas_ValidationEventHandler);
                        retVal.Schemas.Compile();
                        ValidationSettings.Add(release, retVal);
                        Directory.Delete(basePath, true);
                        return retVal;
                    }
                    catch (Exception e)
                    {
                        //Assert.Fail(e.ToString());
                        return retVal;
                    }
                    finally
                    {
                        if (zis != null)
                        {
                            zis.Close();
                            zis.Dispose();
                        }
                    }

                }
            }
            finally
            {
                Directory.Delete(tmpDir, true);
                System.GC.Collect();
            }
            return retVal;
        }

        static void Schemas_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Tracer.Trace(e.Exception.ToString());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "Interaction"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Xml"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Interaction"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static List<String> Validate(String Interaction, MemoryStream XmlInstance, Type TypeReference)
        {
            List<String> errorList = new List<string>(10);
            

            // rewind the stream so we can validate in memory
            XmlInstance.Seek(0, SeekOrigin.Begin);
            
            // TODO: verify schemas were loaded using ValidationSettings.Schemas.Count
            XmlReader rdr = null;
            
            try
            {

                Assembly release = TypeReference.Assembly;
                // Find the appropriate schema set to use
                XmlReaderSettings settings = null;
                object[] releaseName = release.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
                if (releaseName.Length > 0)
                {
                    settings = GetSettings((releaseName[0] as AssemblyInformationalVersionAttribute).InformationalVersion);
                    if (settings == null)
                        Assert.Fail("Cannot find validation settings for '{0}'", (releaseName[0] as AssemblyInformationalVersionAttribute).InformationalVersion);
                }
                else
                    Assert.Fail("This assembly carries no version information with it");
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += new ValidationEventHandler(delegate(object sender, ValidationEventArgs e)
                    {
                        if(e.Severity == XmlSeverityType.Error && !e.Message.Contains("specializationType") && !e.Message.Contains("incomplete") &&
                            !e.Message.Contains("equal its fixed value") && !e.Message.Contains("The Enumeration constraint failed") && !e.Message.Contains("memberTypes of the union")) // Usually incomplete is due to the simple type creator not creating the necessary elements
                                errorList.Add(String.Format("{0} : Validation exception: {1} @ {2},{3}" , e.Severity, e.Message, e.Exception.LineNumber, e.Exception.LinePosition));
                    });

                rdr = XmlReader.Create(XmlInstance, settings);

                try
                {
                    while (rdr.Read()) ;
                }
                catch (XmlException e)
                {
                    Tracer.Trace("Validation exception: " + e.Message);
                    Tracer.Trace("          Line Number: " + e.LineNumber);
                    errorList.Add("Error: " + e.Message + "Line Number: " + e.LineNumber);
                }

            }
            catch (Exception ReaderCreateException)
            {
                Assert.Fail("Exception: " + ReaderCreateException.ToString());
            }
                       

            if (rdr.ReadState == ReadState.EndOfFile)
            {
                //DebugMessage = "\nValidation complete.";
            }
            else if (rdr.ReadState == ReadState.Error)
            {
                errorList.Add("Validation encountered one or more serious errors and could not continue.");
            }
            //System.Diagnostics.Debug.WriteLine(DebugMessage);


            if (errorList.Count > 0)
            {
                errorList.ForEach(item => Trace.WriteLine(item));
                Trace.WriteLine(System.Text.Encoding.UTF8.GetString(XmlInstance.GetBuffer(), 0, (int)XmlInstance.Length));
            }
            return errorList;
        }
        

    }
}
