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
using System.Reflection;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.CA.R020401;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.Test
{
    internal static class SimpleTypeCreator
    {
        internal static string CreateString(UseContext context)
        {
            Tracer.Trace("Creating string", context);
            return
                context.ParameterInfo != null ? context.ParameterInfo.Name :
                context.PropertyInfo != null ? context.PropertyInfo.Name :
                "String";

        }

        internal static ED CreateED(UseContext context)
        {
            return new ED(new byte[] { 0, 1, 1, 2, 3, 5, 8, 13 }, "other/fibanacci");
        }

        internal static TN CreateTN(UseContext context)
        {
            return new TN("Bob The Builder");
        }

        internal static RTO<IQuantity, IQuantity> CreateRtoDefault(UseContext context)
        {
            var res = CreateRtoOfPqAndPq(context);
            return new RTO<IQuantity, IQuantity>(
                res.Numerator, res.Denominator
            );
        }

        internal static RTO<PQ, PQ> CreateRtoOfPqAndPq(UseContext context)
        {
            Tracer.Trace("Creating RTO<PQ,PQ>", context);

            RTO<PQ, PQ> result = new RTO<PQ, PQ>();

            result.Numerator = new PQ();
            result.Denominator = new PQ();

            result.Numerator.Value = 1000000001;
            result.Numerator.Unit = "CAD";

            result.Denominator.Unit = "day";
            result.Denominator.Value = 1;

            return result;
        }

        internal static GTS CreateGTS(UseContext context)
        {
            Tracer.Trace("Create GTS", context);
            return new GTS(new IVL<TS>(CreateTS(context), CreateTS(context)));
        }

        internal static RTO<MO, PQ> CreateRtoOfMoAndPq(UseContext context)
        {
            Tracer.Trace("Creating RTO<MO,PQ>", context);

            RTO<MO, PQ> result = new RTO<MO, PQ>();

            result.Numerator = new MO();
            result.Denominator = new PQ();

            result.Numerator.Value = 1000000001;
            result.Numerator.Currency = "CAD";

            result.Denominator.Unit = "day";
            result.Denominator.Value = 1;

            return result;
        }

        internal static IVL<PQ> CreateIVLPQ(UseContext context)
        {
            Tracer.Trace("Creating IVL<PQ>", context);

            IVL<PQ> result = new IVL<PQ>();

            result.Low = new PQ((decimal)1,"d");
            result.High = new PQ((decimal)3, "d");

            return result;
        }

        internal static IVL<TS> CreateIVLTS(UseContext context)
        {
            Tracer.Trace("Creating IVL<TS>", context);

            IVL<TS> result = new IVL<TS>();

            result.Low = CreateTS(context);
            result.High = CreateTS(context).DateValue.AddDays(1);

            return result;
        }

        internal static PIVL<TS> CreatePIVLTS(UseContext context)
        {
            Tracer.Trace("Creating PIVL", context);
            PIVL<TS> result = new PIVL<TS>(
                CreateIVLTS(context),
                new PQ((decimal)1.0, "d")
            );
            return result;
        }

        internal static PQ CreatePQ(UseContext context)
        {
            Tracer.Trace("Creating PQ", context);
            return new PQ((decimal)1.0, "d");
        }

        internal static MO CreateMO(UseContext context)
        {
            Tracer.Trace("Creating MO", context);

            MO result = new MO();

            result.Value = 1000000001;
            result.Currency = "CAD";

            return result;
        }

        /// <summary>
        /// Can create an ANY (ie: any datatype) so assign it a code
        /// </summary>
        internal static ANY CreateANY(UseContext context)
        {
            return new CS<String>("InsteadofAny");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "context")]
        internal static AD CreateAD(UseContext context)
        {
            List<ADXP> result = new List<ADXP>();

            result.Add(new ADXP("123", AddressPartType.BuildingNumber));
            result.Add(new ADXP("Fake", AddressPartType.StreetNameBase));
            result.Add(new ADXP("Street", AddressPartType.StreetType));
            result.Add(new ADXP("1", AddressPartType.UnitIdentifier));
            result.Add(new ADXP("Door", AddressPartType.UnitDesignator));
            result.Add(new ADXP("Anytown", AddressPartType.City));
            result.Add(new ADXP("AnyCounty", AddressPartType.County));
            result.Add(new ADXP("Ontario", AddressPartType.State));
            result.Add(new ADXP("Canada", AddressPartType.Country));

            AD ad = new AD(PostalAddressUse.HomeAddress, result);

            return ad;
        }

        internal static BL CreateBL(UseContext context)
        {
            Tracer.Trace("Creating BL", context);
            return true;
        }

        internal static PN CreatePN(UseContext context)
        {
            Tracer.Trace("Creating PN", context);
            PN result = new PN();

            result.Part.Add(new ENXP("McLovin", EntityNamePartType.Family));

            return result;
            
        }

        internal static REAL CreateREAL(UseContext context)
        {
            Tracer.Trace("Creating REAL", context);
            return Math.PI;
        }

        internal static ST CreateST(UseContext context)
        {
            Tracer.Trace("Creating ST", context);
            return "String";
        }

        internal static SC CreateSC(UseContext context)
        {
            Tracer.Trace("Creating SC", context);
            return new SC("Hello", "en-US", new CD<String>("120394"));
        }

        internal static TS CreateTS(UseContext context)
        {
            Tracer.Trace("Creating TS", context);
            return DateTime.Parse("2011-02-09 12:55:00");
        }


        internal static ON CreateON(UseContext context)
        {
            return new ON(EntityNameUse.Legal,
                new ENXP[] { 
                    new ENXP("St", EntityNamePartType.Prefix),
                    new ENXP("Mary's", EntityNamePartType.Suffix)
                });
        }

        internal static EN CreateEN(UseContext context)
        {
            return new EN(EntityNameUse.Legal, 
                new ENXP[] { 
                    new ENXP("John", EntityNamePartType.Given),
                    new ENXP("Jacob", EntityNamePartType.Given),
                    new ENXP("Jingleheimer", EntityNamePartType.Family),
                    new ENXP("-", EntityNamePartType.Delimiter),
                    new ENXP("Schmidt", EntityNamePartType.Given)
                }
            );
        }

        internal static INT CreateINT(UseContext context)
        {
            Tracer.Trace("Creating INT", context);

            if (null != context && null != context.PropertyAttribute && !string.IsNullOrEmpty(context.PropertyAttribute.ImposeFlavorId))
            {
                switch (context.PropertyAttribute.ImposeFlavorId)
                {
                    case "INT.POS":
                        return 22;
                    case "INT.NONNEG":
                        return 0;
                    default:
                        return -42;
                }
            }
            else
            {
                return 55;
            }
        }

        internal static CD<String> CreateCD(UseContext context)
        {
            Tracer.Trace("Creating CD", context);

            return new CD<String>()
            {
                Code = "284196006",
                CodeSystem = "2.16.840.1.113883.6.96"
            };
        }

        internal static CV<String> CreateCV(UseContext context)
        {
            return new CV<String>()
            {
                Code = "284196006",
                CodeSystem = "2.16.840.1.113883.6.96"
            };
        }

        internal static CE<String> CreateCE(UseContext context)
        {
            return new CE<String>()
            {
                Code = "284196006",
                CodeSystem = "2.16.840.1.113883.6.96"
            };
        }

        internal static PQR CreatePQR(UseContext context)
        {

            return new PQR((decimal)1.23,
                "284196006",
                "2.16.840.1.113883.6.96"
            );
        }
        internal static CO CreateCO(UseContext context)
        {

            return new CO((decimal)1.23, new CD<string>(
                "284196006",
                "2.16.840.1.113883.6.96"
            ));
        }
        //internal static ResponseMode CreateResponseMode(UseContext context)
        //{
        //    Tracer.Trace("Creating ResponseMode", context);

        //    return ResponseMode.Immediate;
        //}

        //internal static QueryRequestLimit CreateQueryRequestLimit(UseContext context)
        //{
        //    Tracer.Trace("Creating QueryRequestLimit", context);

        //    return QueryRequestLimit.Record;
        //}

        internal static URG<PQ> CreateURG_PQ(UseContext context)
        {
            Tracer.Trace("Creating URG<PQ>", context);

            URG<PQ> result = new URG<PQ>();

            result.Value = (decimal)Math.PI;

            result.Probability = (decimal)0.9999999999999;

            return result;
        }

        /// <summary>
        /// Create a CS
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static CS<String> CreateCS(UseContext context)
        {
            Tracer.Trace("Creating CS", context);

            if(context.PropertyAttribute != null && context.PropertyAttribute.Name == "moodCode")
                return new CS<string>("EVN");
            return new CS<String>()
            {
                Code = "PPP"
            };
        }

        internal static TEL CreateTEL(UseContext context)
        {
            Tracer.Trace("Creating TEL", context);

            if (null != context && null != context.PropertyAttribute && !string.IsNullOrEmpty(context.PropertyAttribute.ImposeFlavorId))
            {
                TEL result = new TEL();

                if (null == result.Use)
                    result.Use = new SET<CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>();

                switch (context.PropertyAttribute.ImposeFlavorId)
                {
                    case "TEL.URI":
                        #region URI
                        result.Use.Add(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Direct);
                        

                        result.Value = "http://www.marc-hi.ca";

                        return result;
                        #endregion
                    case "TEL.PHONE":
                        #region Phone
                        result.Use.Add(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.WorkPlace);
                       

                        result.Value = "+1-905-575-1212x3112";

                        return result;
                        #endregion
                    case "TEL.PHONEMAIL":
                        #region Email
                        result.Use.Add(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.WorkPlace);
                        

                        result.Value = "mailto:marc-hi@mohawkcollege.ca";

                        return result; 
                        #endregion
                    default:
                        #region Default - URI
                        result.Use.Add(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Direct);
                     
                        result.Value = "http://www.marc-hi.ca";

                        return result; 
                        #endregion
                }
            }
            else
            {
                #region Unknown - Assume URI
                TEL result = new TEL();

                if (null == result.Use)
                    result.Use = new SET<CS<MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse>>();


                result.Use.Add(MARC.Everest.DataTypes.Interfaces.TelecommunicationAddressUse.Direct);

                result.Value = "http://www.marc-hi.ca";

                return result; 
                #endregion
            }
        }




        internal static II CreateII(UseContext context)
        {
            Tracer.Trace("Creating II", context);

            if (null != context && null != context.PropertyAttribute && !string.IsNullOrEmpty(context.PropertyAttribute.ImposeFlavorId))
            {
                switch (context.PropertyAttribute.ImposeFlavorId)
                {
                    case "II.BUS":
                        return new II("1.2.3.4", Guid.NewGuid().ToString().Replace("{", "").Replace("}", ""));
                    case "II.TOKEN":
                        return new II(Guid.NewGuid());
                    default:
                        return new II("1.2.3.4", Guid.NewGuid().ToString().Replace("{", "").Replace("}", ""));
                }
            }
            else //Return the default
            {
                return new II("1.2.3.4",
                    Guid.NewGuid().ToString().Replace("{", "").Replace("}", ""));
            }
        }

        #region Static Control Code, Do NOT Edit
        internal static Dictionary<Type, Func<UseContext, object>> SimpleTypeCreators
        {
            get
            {
                if (null == m_TypeCreatorCache)
                {
                    m_TypeCreatorCache = new Dictionary<Type, Func<UseContext, object>>();

                    var t = typeof(SimpleTypeCreator);

                    foreach (var method in t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        var imethod = method;

                        m_TypeCreatorCache.Add(method.ReturnType, context => imethod.Invoke(null, new object[] { context }));
                    }
                }

                return m_TypeCreatorCache;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        private static Dictionary<Type, Func<UseContext, object>> m_TypeCreatorCache;
        #endregion

    }
}
