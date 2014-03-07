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
    }
}
