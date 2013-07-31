/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * User: Justin Fyfe
 * Date: 02-09-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;

namespace MARC.Everest.DataTypes.Converters
{
    /// <summary>
    /// An implementation of the IUnitConverter for converting
    /// SI units
    /// </summary>
    public class SimpleSiUnitConverter : IUnitConverter
    {
        #region IUnitConverter Members

        // SI
        private static readonly Dictionary<string, double> s_siPrefixes = new Dictionary<string, double>()
        {
                { "u", 0.000001d },
                { "m", 0.001d },
                { "c", 0.01d },
                { "d", 0.1d },
                { "", 1d },
                { "h", 100d },
                { "k", 1000d },
                { "M", 1000000d },
                { "G", 1000000000d },
                { "T", 1000000000000d }
        };
        private static readonly List<String> s_siUnits = new List<string>{
            "m", // meter
            "L", // liter
            "mol", // mol
            "g", // gram
            "Pa", // Pascal
            "K", // Kelvin
            "N", // Newton
            "J", // Joule
            "V", // Volt
            "W", // Watt
            "lm"
        };

        /// <summary>
        /// Returns true if <paramref name="from"/> can be converted
        /// to the <paramref name="unitTo"/>
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">If any of the arguments are Null or NullFlavored</exception>
        public bool CanConvert(PQ from, string unitTo)
        {
            if (from == null || from.IsNull)
                throw new ArgumentNullException("from");
            else if (unitTo == null)
                throw new ArgumentNullException("unitTo");

            bool isSiMeasure = !String.IsNullOrEmpty(GetSiUnit(unitTo).Value) && !String.IsNullOrEmpty(GetSiUnit(from.Unit).Value);

            if (isSiMeasure)
                isSiMeasure = GetSiUnit(unitTo).Value.Equals(GetSiUnit(from.Unit).Value);
            return isSiMeasure;
        }

        /// <summary>
        /// Get SI portions of the specified unit
        /// </summary>
        private KeyValuePair<String, String> GetSiUnit(string unit)
        {
            string unitPart = unit, prefixPart;
            // First, is the unit strictly an SI unit with no prefix
            if (SimpleSiUnitConverter.s_siUnits.Contains(unitPart))
                return new KeyValuePair<string, string>(String.Empty, unitPart);
            else if (unitPart.Length > 1)
            {
                prefixPart = unitPart[0].ToString();
                unitPart = unitPart.Substring(1);
                if (SimpleSiUnitConverter.s_siUnits.Contains(unitPart) && SimpleSiUnitConverter.s_siPrefixes.ContainsKey(prefixPart))
                    return new KeyValuePair<string, string>(prefixPart, unitPart);
            }
            return new KeyValuePair<String, String>();

        }
        /// <summary>
        /// Convert <paramref name="original"/> to a new instance
        /// of PQ with the unit <paramref name="unitTo"/>
        /// </summary>
        public PQ Convert(PQ original, string unitTo)
        {
            if (!String.IsNullOrEmpty(GetSiUnit(unitTo).Value))
            {
                KeyValuePair<String, String> newSiData = GetSiUnit(unitTo),
                    oldSiData = GetSiUnit(original.Unit);
                double oldSiBase = SimpleSiUnitConverter.s_siPrefixes[oldSiData.Key],
                    newSiBase = SimpleSiUnitConverter.s_siPrefixes[newSiData.Key];
                return new PQ((decimal)((double)original.Value * oldSiBase / newSiBase), unitTo);
            }
            return new PQ() { NullFlavor = NullFlavor.Unknown };
        }

        #endregion
    }
}
