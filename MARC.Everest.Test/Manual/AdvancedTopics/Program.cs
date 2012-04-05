using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using MARC.Everest;
using MARC.Everest.Xml;
using MARC.Everest.DataTypes;

using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.NE2008;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.COCT_MT050000UV01;
using MARC.Everest.RMIM.UV.NE2008.COCT_MT030000UV04;

//using MARC.Everest.Test.Manual_Interfaces;

namespace EverestConsoleApplication
{
    class Program
    {
        /// <summary>
        /// Creates a patient structure
        /// </summary>
        /// <param name="id">The unique identifier</param>
        /// <param name="name">The name of the patient</param>
        /// <param name="addr">The primary address</param>
        /// <param name="telecom">A primary telecom</param>
        /// <returns>A constructed patient structure</returns>

        public static Patient CreatePatient(
                II id,
                EN name,
                AD addr,
                TEL telecom
            )
        {
            // Instantiate the object
            var retVal = new Patient();

            // Populate address
            retVal.Addr = BAG<AD>.CreateBAG(addr);

            // Confidentiality Code
            retVal.ConfidentialityCode = "N";

            // Effective Time of the types
            // High is populated as "Not Applicable"
            retVal.EffectiveTime = new IVL<TS>(
                (TS)DateTime.Now,
                new TS() { NullFlavor = NullFlavor.NotApplicable }
                );

            // Telecom address
            retVal.Telecom = BAG<TEL>.CreateBAG(telecom);

            // Populate the ID
            retVal.Id = SET<II>.CreateSET(id);

            // Status Code
            retVal.StatusCode = RoleStatus.Active;

            // Set entity choice
            retVal.SetPatientEntityChoiceSubject(
                new Person()
                {
                    AdministrativeGenderCode = AdministrativeGender.Male,
                    Name = BAG<EN>.CreateBAG(name),
                    BirthTime = DateTime.Now
                }
                );

            // Return result
            return retVal;
        }

        static void Main(string[] args)
        {
            // Create a formatter with a graph aide
            XmlIts1Formatter fmtr = new XmlIts1Formatter();

            // Gets or sets a list of aide formatters
            // that assist in the formatting of this instance
            fmtr.GraphAides.Add(new DatatypeFormatter());

            // Format to a string
            StringWriter sw = new StringWriter();
            XmlStateWriter xmlSw = new XmlStateWriter(
                XmlWriter.Create(sw, new XmlWriterSettings(){Indent = true})
                );

            // Call our method here...
            var patient = CreatePatient(
                Guid.NewGuid(),
                EN.CreateEN(EntityNameUse.Legal,
                    new ENXP ("John", EntityNamePartType.Given),
                    new ENXP ("Smith", EntityNamePartType.Family)
                    ),
                AD.CreateAD(PostalAddressUse.HomeAddress,
                    "123 Main Street West",
                    "Hamilton",
                    "Ontario",
                    "Canada"
                ),
                 "john.smith@mohawkcollege.ca"
                );

            // Format to the formatter
            var details = fmtr.Graph(xmlSw, patient);

            // Flush and close
            xmlSw.Close();

            // Output to console
            Console.WriteLine("XML: ");
            Console.WriteLine(sw.ToString());
            Console.WriteLine("Validation:");
            foreach (var dtl in details.Details) {
                Console.WriteLine("{0} : {1}", dtl.Type, dtl.Message);
            }
            Console.ReadKey();
        }
    }
}