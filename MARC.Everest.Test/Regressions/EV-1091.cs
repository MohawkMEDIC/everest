using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Test.Regressions
{
    /// <summary>
    /// Work item 1091 regression
    /// </summary>
    [TestClass]
    public class EV_1091
    {
        /// <summary>
        /// Verify serialization of IVL works when all properties (value, low, high) are involved with warnings
        /// </summary>
        [TestMethod]
        public void IVL_TS_FullSerializationTest()
        {

            IVL<TS> origin = new IVL<TS>((TS)"20080102");
            origin.Low = (TS)"20080101";
            origin.High = (TS)"20080103";

            // Serialize
            String expected = "<test value=\"20080102\" xmlns=\"urn:hl7-org:v3\"><low value=\"20080101\"/><high value=\"20080103\"/></test>";
            String actual = R1SerializationHelper.SerializeAsString(origin);
            R2SerializationHelper.XmlIsEquivalent(expected, actual);

        }
    }
}
