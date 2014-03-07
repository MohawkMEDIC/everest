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
using MARC.Everest.Attributes;
using System.Diagnostics;

namespace MARC.Everest.Test
{
    public static class EqualUtility
    {
        /// <summary>
        /// Print equality
        /// </summary>
        public static void PrintEquals(Object a, Object b, string path)
        {

            // Overview of PI
            foreach (var pi in a.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object[] customAtt = pi.GetCustomAttributes(typeof(PropertyAttribute), true);
                if (customAtt.Length == 0) continue;

                // Print
                object aPiValue= pi.GetValue(a, null),
                    bPiValue = pi.GetValue(b, null);
                bool equals = aPiValue != null && aPiValue.Equals(bPiValue);
                string myPath = String.Format("{0}.{1}", path, pi.Name);

                if (aPiValue is System.Collections.IList)
                {
                    var aList = aPiValue as System.Collections.IList;
                    var bList = bPiValue as System.Collections.IList;
                    for (int i = 0; i < aList.Count; i++)
                    {
                        string aPath = String.Format("{0}[{1}]", myPath, i);
                        equals = aList[i] != null && aList[i].Equals(bList[i]);

                        if (!equals && aList[i] != null)
                            Debug.WriteLine(String.Format("{0} ({1})", aPath, aList[i].GetType().FullName));

                        if (aList[i] != null && bList[i] != null &&
                            !typeof(MARC.Everest.DataTypes.ANY).IsAssignableFrom(aList[i].GetType()))
                            PrintEquals(aList[i], bList[i], aPath);
                    }
                }
                else if (!equals && aPiValue != null)
                {
                    Debug.WriteLine(String.Format("{0} ({1})", myPath, pi.PropertyType.FullName));
                    if (aPiValue != null && bPiValue != null &&
                        !typeof(MARC.Everest.DataTypes.ANY).IsAssignableFrom(aPiValue.GetType()))
                        PrintEquals(aPiValue, bPiValue, myPath);
                }
                
            }

        }
    }
}
