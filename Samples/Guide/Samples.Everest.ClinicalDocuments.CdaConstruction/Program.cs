/**
 * This sample will illustrate how the various features of the MARC-HI Everest Framework
 * can be used to construct a valid CDAr2 document with minimal coding effort. 
 * 
 * This example will construct the example CDA document contained in the SampleCDADocument.xml
 * file that is distributed with NE2010 of the HL7 standard.
 *
 * Author: Justin Fyfe
 * Date: July 18, 2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using System.IO;
using System.Xml;
using MARC.Everest.RMIM.UV.CDAr2;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using MARC.Everest;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Xml;
using System.Reflection;


namespace Samples.Everest.ClinicalDocuments.CdaConstruction
{
    class Program
    {


        static void Main(string[] args)
        {

            // We can speed up initial serialization by loading a cached formatter assembly
            MARC.Everest.Formatters.XML.ITS1.Formatter fmtr = new MARC.Everest.Formatters.XML.ITS1.Formatter();
            // If you want to experiment and see the difference in serialization times, try commenting these lines out 
            // and seeing the effect.
            fmtr.GraphAides.Add(new DatatypeFormatter());
            fmtr.ValidateConformance = false;

            // Create the CDA
            ClinicalDocument cda = new ClinicalDocument(
                ActClassClinicalDocument.ClinicalDocument, // Document type is clinical document
                new II("2.16.840.1.113883.19.4", "c266"), // Create an identifier for the document
                new CE<String>("33049-3", "2.16.840.1.113883.6.1", "LOINC", null, "Consultation note", null), // Specify the type of document
                DateTime.Now, // Effective time of the document (now)
                x_BasicConfidentialityKind.Normal, // Confidentiality code of N = Normal
                CreateRecordTarget(), // Create a record target, this is good for code reuse
                CreateAuthor(), // Create the author node
                CreateCustodian(), // Create custodian node
                CreateComponent() // Create component Node
            )
            {
                Title = UserPrompt<ST>("Document Title:")
            };

            Console.Clear();

            Console.WriteLine("Here is your CDA:");

            // Prepare the output 
            XmlStateWriter xsw = new XmlStateWriter(XmlWriter.Create(Console.OpenStandardOutput(), new XmlWriterSettings() { Indent = true }));
            DateTime start = DateTime.Now;
            var result = fmtr.Graph(xsw, cda);
            xsw.Flush();
            Console.WriteLine("Took {0} ms to render", DateTime.Now.Subtract(start).TotalMilliseconds);

            // We can serialize again to see the learning pattern of Everest
            for (int i = 2; i < 20; i++)
            {
                xsw = new XmlStateWriter(XmlWriter.Create(new MemoryStream(), new XmlWriterSettings() { Indent = true }));
                start = DateTime.Now;
                result = fmtr.Graph(xsw, cda);
                xsw.Flush();
                Console.WriteLine("Render #{1} Took {0} ms to render", DateTime.Now.Subtract(start).TotalMilliseconds, i);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Prompts the user for a piece of information and continues to prompt so long as the 
        /// information is invalid
        /// </summary>
        private static T UserPrompt<T>(string prompt) where T : IPrimitiveDataValue<String>, new()
        {
            Console.Write(prompt);
            string response = default(string);
            while (true)
            {
                response = Console.ReadLine();
                if (String.IsNullOrEmpty(response))
                    Console.Write("Invalid response! {0}", prompt);
                else
                    break;
            }
            return new T() { Value = response };
        }

        /// <summary>
        /// Function will create a component (the body of the CDA)
        /// </summary>
        private static Component2 CreateComponent()
        {
            // Create a component 2 structure, which contains the body of the CDA, set it's type code to COMP and
            // context conduction indicator (any data with context conduction code of AP will be conducted) to true
            var retVal = new Component2(ActRelationshipHasComponent.HasComponent, true);
            


            // Everest provides methods of creating abstract classes through a series of helper methods,
            // here we're creating an observation for asthma
            var asthma = ClinicalStatement.CreateObservation(
                ActClass.Observation,
                x_ActMoodDocumentObservation.Eventoccurrence,
                new CD<String>("30049385", "2.16.840.1.113883.6.96", "SNOMED-CT", null, "Asthma", null)
            );
            asthma.StatusCode = ActStatus.Completed;
            asthma.EffectiveTime = new IVL<TS>(new TS(DateTime.Now, DatePrecision.Year), null); // In Everest, we can create a date with a precision of year which can be interpreted as any time that year
            asthma.Reference.Add(new Reference(x_ActRelationshipExternalReference.Excerpts, ExternalActChoice.CreateExternalObservation(ActClass.Condition)));
            
            // BodyChoice is a choice of either nonXml content or structured body, how would we know that?
            // Well, whenever a class as a property XChoice and SetXChoice the SetXChoice will contain
            // the options of what XChoice can be set to
            retVal.SetBodyChoice(new StructuredBody()
            {
                Component = new List<Component3>() // We can use type initializers to create the components (sections)
                {
                    // We can create a section for history of present illness
                    new Component3(ActRelationshipHasComponent.HasComponent, true, 
                        new Section() {
                            Code = new CE<string>("10164-2", "2.16.840.1.113883.6.1"),
                            Title = "History of Present Illness",
                            Text = new ED("<content styleCode=\"Bold\">This is bold</content>") { Representation = EncapsulatedDataRepresentation.XML } // In the CDA ED type, we can use content tags to style the content
                        }),
                    new Component3(ActRelationshipHasComponent.HasComponent, true,
                        new Section() {
                            Code = new CE<string>("10153-2", "2.16.840.1.113883.6.1"),
                            Title = "Past Medical History",
                            Text = "We can also assign an ED directly as a string",
                            Entry = new List<Entry>() {  // We can also add observations and sub-components
                                new Entry(x_ActRelationshipEntry.HasComponent, true, asthma) // See where we defined astma above as a clinical statement
                            }
                        })
                }
            });

            return retVal;

        }

        /// <summary>
        /// Function will create custodian (record of truth)
        /// </summary>
        private static Custodian CreateCustodian()
        {
            //throw new NotImplementedException();
            return new Custodian(
                new AssignedCustodian(
                    RoleClass.AssignedEntity
                )
            );
        }

        /// <summary>
        /// Function will create the author of the document
        /// </summary>
        private static Author CreateAuthor()
        {
            return new Author(
                ContextControl.AdditivePropagating,
                DateTime.Now,
                new AssignedAuthor(
                    RoleClass.AssignedEntity,
                    new SET<II>(new II("1.2.3.4.5.6", "1234"))
                )
            );
        }

        /// <summary>
        /// Function will create the record target
        /// </summary>
        private static RecordTarget CreateRecordTarget()
        {
            return new RecordTarget(
                ContextControl.AdditivePropagating,
                new PatientRole(
                    new SET<II>(new II("1.2.3.4.5", "2431"))
                )
            );
        }
    }
}
