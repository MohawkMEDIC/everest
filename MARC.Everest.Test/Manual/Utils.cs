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
using MARC.Everest.RMIM.UV.NE2008.COCT_MT050000UV01;    // used to create new Patient
using MARC.Everest.RMIM.UV.NE2008.COCT_MT030000UV04;
using MARC.Everest.RMIM.CA.R020402.Interactions;
using MARC.Everest.RMIM.CA.R020403.REPC_MT500005CA;
using System.Xml.Schema;
using System.Reflection;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.Manual.Interfaces
{
    class Utils
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

            // Return result
            return retVal;
        }


        public static void PrintIds(IGraphable g)
        {
            // Can't scan a null object
            if (g == null)
                return;

            Type gType = g.GetType();
            foreach (PropertyInfo pi in gType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                // Store the value of the current property of the instance
                object value = pi.GetValue(g, null);

                // Don't report null objects (objects without members)
                if (value == null)
                    continue;

                // Passes the value of the property into an object
                // that follows the IIdentifiable interface rules
                IIdentifiable identifiableObject = value as IIdentifiable;

                // Passes the value of the property into an object
                // that follows the ISetIdentifiable interface rules
                ISetIdentifiable setIdentifiableObject = value as ISetIdentifiable;

                if (identifiableObject != null)

                    // Write the property name, the and object's root and extension.
                    Console.WriteLine("{0} : {1}@{2}", pi.Name, identifiableObject.Id.Root, identifiableObject.Id.Extension);

                else if (setIdentifiableObject != null)
                {
                    // Iterates through all instance identifiers
                    // in a single id of the instance.
                    foreach (var ii in setIdentifiableObject.Id)
                        // Write the property name, the and object's extension.
                        Console.WriteLine("{0} : {1}@{2}", pi.Name, ii.Root, ii.Extension);
                }

                // Recurse and Scan type
                PrintIds(value as IGraphable);
            } // end foreach
        }
    }
}
