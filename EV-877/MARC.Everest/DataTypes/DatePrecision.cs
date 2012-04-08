/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * Original Date: 01-09-2009
 * 
 * Moved to new file: 09-09-2011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Date precision
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum DatePrecision
    {
        /// <summary>
        /// Year precision
        /// </summary>
        Year = 4,
        /// <summary>
        /// Month precision
        /// </summary>
        Month = 6,
        /// <summary>
        /// Day precision
        /// </summary>
        Day = 8,
        /// <summary>
        /// Hour precision with no timezone
        /// </summary>
        HourNoTimezone = 10,
        /// <summary>
        /// Minute no timezone
        /// </summary>
        MinuteNoTimezone = 12,
        /// <summary>
        /// Second no timezone
        /// </summary>
        SecondNoTimezone = 14,
        /// <summary>
        /// Full timestamp with no timezone
        /// </summary>
        FullNoTimezone = 18,
        /// <summary>
        /// Hour precision
        /// </summary>
        Hour = 15,
        /// <summary>
        /// Minute precision
        /// </summary>
        Minute = 17,
        /// <summary>
        /// Second precision
        /// </summary>
        Second = 19,
        /// <summary>
        /// Full precision
        /// </summary>
        Full = 23
    }

    /// <summary>
    /// Date precision utilities
    /// </summary>
    public static class DatePrecisionUtil
    {
        /// <summary>
        /// Returns true if date precision has a timezone
        /// </summary>
        public static bool HasTimeZone(this DatePrecision a)
        {
            switch (a)
            {
                case DatePrecision.FullNoTimezone:
                case DatePrecision.HourNoTimezone:
                case DatePrecision.MinuteNoTimezone:
                case DatePrecision.SecondNoTimezone:
                    return false;
                default:
                    return true;
            }
        }
    }
}
