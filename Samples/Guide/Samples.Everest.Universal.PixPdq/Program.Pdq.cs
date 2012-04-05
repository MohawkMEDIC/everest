/*
 * This sample illustrates the use of Everest to construct a PIX and PDQ Version 3 message. The 
 * message content is based on the samples provided by IHE as part of the ITI technical supplements.
 * 
 * Author: Justin Fyfe
 * Date: November 6, 2010
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.DataTypes;

namespace Samples.Everest.Universal.PixPdq
{
    /// <summary>
    /// PDQ Version 3 Construction Routines
    /// </summary>
    partial class Program
    {
        /// <summary>
        /// Generate PDQ Message
        /// </summary>
        /// <remarks>
        /// This will recreate the PDQ sample provided by IHE
        /// </remarks>
        private static void GeneratePDQ()
        {

            Console.WriteLine("\r\n\r\nGenerating PDQv3....\r\n\r\n");
            // Create the message structure
            PRPA_IN201305UV02 message = new PRPA_IN201305UV02(
                new II("1.2.840.114350.1.13.0.1.7.1.1", "35423"), // The identifier of the message
                DateTime.Now, // Identifies the time that the message was created
                PRPA_IN201305UV02.GetInteractionId(), // Identifies the interaction that is being executed
                ProcessingID.Training, // Identifies the processing code of the message (how the message is to be executed)
                "I", // Identifies the processing mode code 
                AcknowledgementCondition.Always, // Identifies under which conditions the message should be acknowledged
                null, // We'll fill this in later
                null, // We'll fill this in later
                new MARC.Everest.RMIM.UV.NE2008.QUQI_MT021001UV01.ControlActProcess<MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.QueryByParameter>()
            );

            // The receiver node of the message identifies the system that is the intended recipient of the 
            // message
            message.Receiver.Clear();
            message.Receiver.Add(new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Receiver(
                new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Device(
                    new SET<II>(new II("1.2.840.114350.1.13.999.234")) // Identifies the device
                )
                { // We can use an initializer to populate variables that don't appear in the constructors
                    Telecom = new BAG<TEL>( // Telecom is a bag of telecommunications addresses
                        new TEL[] { "http://servicelocation/PDQuery" }
                     )
                }
            ));

            // The sender node identifies the system that is the sender of the message
            message.Sender = new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Sender(
                new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Device(
                    new SET<II>(new II("1.2.840.114350.1.13.999.567"))
                )
            );

            // The "controlActProcess" is the main query parameters
            var controlActProcess = message.controlActProcess; // do this to keep lines shorter
            // We need to tell the receiver what action we're executing
            controlActProcess.Code = new CD<string>(PRPA_IN201305UV02.GetTriggerEvent().Code);

            // Create the query object
            controlActProcess.queryByParameter = new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.QueryByParameter()
            { // Once again, using the initializer
                QueryId = new II("1.2.840.114350.1.13.28.1.18.5.999", "18204"),
                StatusCode = "new",
                InitialQuantity = 2
            };

            // Create the criterion list
            controlActProcess.queryByParameter.MatchCriterionList = new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.MatchCriterionList()
            {
                MinimumDegreeMatch = new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.MinimumDegreeMatch(
                    new INT(75),
                    "Degree of match requested"
                )
            };

            // Create the parameter list
            controlActProcess.queryByParameter.ParameterList = new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.ParameterList();
            var parmList = controlActProcess.queryByParameter.ParameterList;

            // Query for Administrative Gender of Male
            parmList.LivingSubjectAdministrativeGender.Add(
                new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.LivingSubjectAdministrativeGender(
                    new SET<CE<AdministrativeGender>>(AdministrativeGender.Male),
                    "LivingSubject.administrativeGender"
                )
            );

            // Query for Birth Time of Aug 4, 1963
            parmList.LivingSubjectBirthTime.Add(
                new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.LivingSubjectBirthTime(
                    new SET<IVL<TS>>(
                        new IVL<TS>(new TS(
                                DateTime.Parse("August 04, 1963") // August 4, 1963
                            )
                        {
                            DateValuePrecision = DatePrecision.Day // Our date is precise to the day
                        }
                        )
                    ),
                    "LivingSubject.birthTime"
                )
            );

            // Query for Jimmy Jones
            parmList.LivingSubjectName.Add(
                new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.LivingSubjectName(
                    new SET<EN>(
                        new EN(EntityNameUse.Legal,
                            new ENXP[] { 
                                new ENXP("Jimmy", EntityNamePartType.Given),
                                new ENXP("Jones", EntityNamePartType.Family)
                            }
                        )
                    ),
                    "LivingSubject.name"
                )
            );

            // Retrieve alternate identifiers for the specified organization
            parmList.OtherIDsScopingOrganization.Add(
                new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.OtherIDsScopingOrganization(
                    new SET<II>(
                        new II("1.2.840.114350.1.13.99997.2.3412")
                    ),
                    "OtherIDs.scopingOrganization.id"
                )
            );

            // Retrieve alternate identifiers for the specified organization
            parmList.OtherIDsScopingOrganization.Add(
                new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201306UV02.OtherIDsScopingOrganization(
                    new SET<II>(
                        new II("2.16.840.1.113883.4.1")
                    ),
                    "OtherIDs.scopingOrganization.id"
                )
            );


            FormatInstance(message);

        }

    }
}
