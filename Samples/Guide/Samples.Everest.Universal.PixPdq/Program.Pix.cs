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
    partial class Program
    {

        /// <summary>
        /// Generate PIX Message
        /// </summary>
        private static void GeneratePIX()
        {

            Console.WriteLine("\r\n\r\nGenerating PIXv3....\r\n\r\n");

            // Create the instance message
            PRPA_IN201309UV02 message = new PRPA_IN201309UV02(
                Guid.NewGuid(), // The sample message uses a Guid for message Id, this is a method you can use to do this
                DateTime.Now, // The time the message was created,
                PRPA_IN201309UV02.GetInteractionId(), // Specify the interaction we're using
                ProcessingID.Training, // Operating in training mode
                "T", // Transactional
                AcknowledgementCondition.Always, // Always acknowledge
                new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Receiver(
                    new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Device(
                        new SET<II>(new II("1.2.840.114350.1.13.99999.4567")) // Identifier of the receiver device
                    )
                    { // We can use initializer to construct extra properties
                        Telecom = new BAG<TEL>(
                            new TEL[] { "http://example.org/PIXQuery" } // The telecommunications address of the receiver
                        )
                    }
                ),
                new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Sender( // The device that is sending the instance
                    new MARC.Everest.RMIM.UV.NE2008.MCCI_MT100200UV01.Device(
                        new SET<II>(new II("1.2.840.114350.1.13.99997.2.7788"))
                    )
                ),
                new MARC.Everest.RMIM.UV.NE2008.QUQI_MT021001UV01.ControlActProcess<MARC.Everest.RMIM.UV.NE2008.PRPA_MT201307UV02.QueryByParameter>()
            );

            // shortcut for control act process
            var cactProcess = message.controlActProcess;
            // Tell the receiver what trigger event we're executing
            cactProcess.Code = new CD<string>(PRPA_IN201309UV02.GetTriggerEvent().Code);

            // Set the author or performer
            var author = new MARC.Everest.RMIM.UV.NE2008.MFMI_MT700701UV01.AuthorOrPerformer(
                "AUT"
            );
            // Set the participant
            author.SetParticipationChoice(new MARC.Everest.RMIM.UV.NE2008.COCT_MT090100UV01.AssignedPerson() // Type of participant is an assigned entity
            {
                Id = new SET<II>( // Set the identifier of the user
                    new II("1.2.840.114350.1.13.99997.2.7766", "USR5568")
                )
            }
            );

            // Set the query parameter
            cactProcess.queryByParameter = new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201307UV02.QueryByParameter(
                new II("1.2.840.114350.1.13.99999.4567.34", "33452"), // The unique id for the query
                "new", // The status code of the query
                new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201307UV02.ParameterList()
            )
            {
                ResponsePriorityCode = "I"
            };

            // Query by patient identifier
            cactProcess.queryByParameter.ParameterList.PatientIdentifier.Add(
                new MARC.Everest.RMIM.UV.NE2008.PRPA_MT201307UV02.PatientIdentifier(
                    new SET<II>(
                        new II("1.2.840.114350.1.13.99997.2.3412", "38273N237")
                    ),
                    "Patient.Id"
                )
            );

            FormatInstance(message);
        }
    }
}
