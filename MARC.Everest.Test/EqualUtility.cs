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
                            Debug.WriteLine(String.Format("{0} ({1}) {2} != {3}", aPath, aList[i].GetType().FullName, aList[i], bList[i]));

                        if (aList[i] != null && bList[i] != null &&
                            !typeof(MARC.Everest.DataTypes.ANY).IsAssignableFrom(aList[i].GetType()))
                            PrintEquals(aList[i], bList[i], aPath);
                    }
                }
                else if (!equals && aPiValue != null)
                {
                    Debug.WriteLine(String.Format("{0} ({1}) {2} != {3}", myPath, pi.PropertyType.FullName, aPiValue, bPiValue));
                    if (aPiValue != null && bPiValue != null &&
                        !typeof(MARC.Everest.DataTypes.ANY).IsAssignableFrom(aPiValue.GetType()))
                        PrintEquals(aPiValue, bPiValue, myPath);
                }
                
            }

        }
    }
}
