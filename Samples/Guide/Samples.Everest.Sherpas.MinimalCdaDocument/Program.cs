using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Sherpas.Templates.Sandbox;
using MARC.Everest.DataTypes;
using MARC.Everest.Sherpas.Templates.Sandbox.Vocabulary;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using MARC.Everest.Sherpas.Formatter.XML.ITS1;
using System.Xml;
using System.IO;

// Sample: Sherpas
// Constructs a minimal CDA Document with an EKGImpressionSection using the Sherpas framework and 
// parses it back into memory

namespace Samples.Everest.Sherpas.MinimalCdaDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            // First, we construct the Minimal CDA Document using the Sherpas constructor
            // to populate the mandatory elements
            MinimalCDAdocument document = new MinimalCDAdocument(
                // ID of the document
                new II("1.2.3.4.5.6", "12034"),
                // Effective time of the document
                DateTime.Now,
                BasicConfidentialityKind.Normal,
                new MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.RecordTarget(
                    ContextControl.AdditiveNonpropagating,
                    new MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.PatientRole(
                        SET<II>.CreateSET(new II("1.2.3.4.5.5", "340-54"))
                    )
                ),
                // Author of the document
                new MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.Author(
                    ContextControl.AdditiveNonpropagating,
                    DateTime.Now,
                    new MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.AssignedAuthor(
                        SET<II>.CreateSET(new II("1.2.3.4.5.4", "304-305"))
                    )
                ),
                // Custodian of the document
                new CDAcustodian(
                    new CDAcustodian_AssignedCustodian(
                        new MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.AssignedCustodian(
                            new MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV.CustodianOrganization(
                                SET<II>.CreateSET(new II("1.2.3.4.5.3", "3049545"))
                            )
                        )
                    )
                ),
                // Component (null for now)
                null);

            // Sherpas allows us to always chose the correct choice by restricting the allowed
            // values. Here is an example where the component is a MinimalCDAdocument_Component
            // rather than a plain old clinical document component
            document.Component = new MinimalCDAdocument_Component();
            // Again, this next line will only create a StructuredBody as that is what is
            // identified in the sherpas template
            document.Component.BodyChoice = new MinimalCDAdocument_Component_BodyChoice();

            
            // Creating a section is easy as well, the section names match the business names of the
            // section which they created. We'll create an EKGImpressionSection next
            EKGImpressionSection section = new EKGImpressionSection(
                "EKG Impression",
                new SD(SD.CreateElement("format", "This is an EKG Impression Section", null).AddAttribute("styleCode", "Bold"))
            );
            
            // Now we can add this to our document
            document.Component.BodyChoice.AddComponent(new EKGImpressionSectionComponent(section));

            // Formatting in Sherpas is handled by the ClinicalDocumentFormatter
            ClinicalDocumentFormatter formatter = new ClinicalDocumentFormatter();
            // We should always register templates with the formatter as this will allow the
            // formatter to properly deserialize templates back into original classes. If we
            // forget this, the formatter will still parse the document however will be 
            // unable to determine which template identifiers match which types
            SherpasUtilities.RegisterTemplates(formatter);

            // Now we can format
            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = true }))
                    formatter.Graph(xw, document);
                Console.Write(sw);

                // Now parse back in
                using (XmlReader xr = XmlReader.Create(new StringReader(sw.ToString())))
                {
                    var result = formatter.Parse(xr);
                    Console.WriteLine("A {0} was parsed!", result.Structure.GetType().Name);
                }
            }

            Console.ReadKey();

        }
    }
}
