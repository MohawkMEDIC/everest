using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.CA.R020401;
using MARC.Everest.RMIM.CA.R020401.Interactions;
using MARC.Everest.RMIM.CA.R020401.Vocabulary;
using MARC.Everest.Formatters.XML.ITS1;
using System.IO;
using MARC.Everest.Formatters.XML.Datatypes.R1;

// "Hello World" HL7v3 Client Registry Message Structure Example
// v0.1 
// Duane Bender
// October 3, 2009

// Note: this example renders a similar message to the one contained in the sample instances for pCS MR2009 "CR 01.01 - PRPA_EX040101CA - Find Candidates Query.xml"
// Note: this program attempts to write to c:\ in order to save the sample instance message

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            // This layer of the Everest Framework maps 1:1 with the underlying HL7v3 structures, so it requires a detailed understanding of HL7v3
            // This example shows some of the Mohawk Everest Framework features, such as:
            //
            //     - overloaded constructors
            //     - structural validation
            //     - vocabulary support with enumerations
            //     - smart datatype support with helper methods
            //     - intellisense support including MIF documentation pass-thru
            //     - built-in XML ITS formatting 
            //     - and other features (see API documentation)
            //
            // Higher levels of an API Framework would provide abstraction from these message details 
            // 


            // Create a client registry "find candidates query" message structure using the required parameters constructor
            // This constructor looks complicated at first, but it ensures that all required elements are present in the message structure

            PRPA_IN101103CA FindCandidatesStructure = new PRPA_IN101103CA( // use the required elements version of the constructor
                           new MARC.Everest.DataTypes.II(new Guid("6AE2BD87-332A-3B99-4EAD-FAF9CE012843")), // message id
                           new DateTime(2009, 03, 14, 19, 58, 55, 218), 
                           ResponseMode.Immediate, // response mode selected from enumeration
                           new MARC.Everest.DataTypes.II("2.16.840.1.113883.1.18", "PRPA_IN101103CA"), // interaction id
                           new MARC.Everest.DataTypes.LIST<II>() { new MARC.Everest.DataTypes.II("2.16.840.1.113883.2.20.2", "R02.04.00") }, // profileID
                           new CS<ProcessingID>(ProcessingID.Debugging), // processing ID code
                           new CS<AcknowledgementCondition>(AcknowledgementCondition.Never), // ack type
                           new MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA.Receiver(
                               new MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA.Device2(new II("2.16.840.1.113883.19.3.297.15.37.0.47", "DIS01"))),
                           new MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA.Sender(
                               new MARC.Everest.RMIM.CA.R020401.MCCI_MT002300CA.Device1(new II("2.16.840.1.113883.19.3.207.15.1.0.3", "DIS01"))));


            // prepare a coded value representing the trigger event - this will be attached to the control act event
            CV<String> triggerevent = new CV<String>() { Code = "PRPA_TE101103CA", CodeSystem = "2.16.840.1.113883.1.18" };

            // Create the control act event required for this message and attach it to the structure
            // Since this is a "Query" interaction type, we create a QueryByParameter structure and attach it to the control act
            // We will later fill in this query block with our specific parameters
            FindCandidatesStructure.controlActEvent = PRPA_IN101103CA.CreateControlActEvent(
                new II("2.16.840.1.113883.19.3.207.15.1.1", "0245285594892"),
                triggerevent,
                new MARC.Everest.RMIM.CA.R020401.MFMI_MT700711CA.Author(new TS(DateTime.Parse("2009-03-14 19:58:55.218"))),
                new MARC.Everest.RMIM.CA.R020401.MFMI_MT700746CA.QueryByParameter<MARC.Everest.RMIM.CA.R020401.PRPA_MT101103CA.ParameterList>(
                    II.CreateToken(Guid.NewGuid()),
                    new MARC.Everest.RMIM.CA.R020401.PRPA_MT101103CA.ParameterList()
                ));
                

            // add the name to search for and attach it to the parameter list
            List<ENXP> namelist = new List<ENXP>();  
            
            namelist.Add(new ENXP("Nuclear", EntityNamePartType.Family));
            namelist.Add(new ENXP("Nancy", EntityNamePartType.Given));

            PN pn1 = new PN(namelist);
            MARC.Everest.RMIM.CA.R020401.PRPA_MT101103CA.PersonName persname = new MARC.Everest.RMIM.CA.R020401.PRPA_MT101103CA.PersonName(pn1);
            // notice we use .Add() to attach a name to the parameter list - this is due to the fact that a person name is actually a list of names
            FindCandidatesStructure.controlActEvent.QueryByParameter.parameterList.PersonName.Add(persname);


            // add the birthday to search for and add it to the parameter list
            TS birthdate = new TS(new DateTime(1990, 1, 1));
            MARC.Everest.RMIM.CA.R020401.PRPA_MT101103CA.PersonBirthtime pbt = new MARC.Everest.RMIM.CA.R020401.PRPA_MT101103CA.PersonBirthtime(birthdate);
            // notice we use assignment to attach the populated object pdt (no .Add() method as above since it is a simple object -- see PersonName.Add() for contrasting example)
            FindCandidatesStructure.controlActEvent.QueryByParameter.parameterList.PersonBirthtime = pbt;

            FindCandidatesStructure.controlActEvent.LanguageCode = new CE<string>() { NullFlavor = MARC.Everest.DataTypes.NullFlavor.AskedUnknown };

            // Create an XML formatter
            XmlIts1Formatter its1Formatter = new XmlIts1Formatter();
            // Add the datatypes R1 graph aide
            its1Formatter.GraphAides.Add(new DatatypeFormatter());

            // Check the structure for conformance by forcing the formatter to render to a memory stream
            // Result.Code will then indicate if the message is conformant
            var Result = its1Formatter.Graph(new MemoryStream(), FindCandidatesStructure);

            if (Result.Code == MARC.Everest.Connectors.ResultCode.AcceptedNonConformant)
            {
                // we have a conformant message, so let's save it

                // print the XML to STDOUT
                its1Formatter.Graph(Console.OpenStandardOutput(), FindCandidatesStructure);

                // if we stream it through an XML writer and set the indent property the output will contain line breaks and indentation which makes it much easier to read (not necessaily required for machine-machine transmission)
                System.Xml.XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter x = XmlWriter.Create(@"PRPA_IN101103CA_Everest.xml", settings);
                // XmlStateWriter is required here to get around some complicated formatting issues
                MARC.Everest.Xml.XmlStateWriter sw = new MARC.Everest.Xml.XmlStateWriter(x);
                its1Formatter.Graph(sw, FindCandidatesStructure);
                sw.Flush();
            }
            else
            {
                Console.WriteLine("Error trying to save structure PRPA_IN101103CA. Formatter response: {0}", Result.Code.ToString());
            }
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


            
        }
    }
}
