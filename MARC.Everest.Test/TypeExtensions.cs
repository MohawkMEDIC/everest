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

namespace MARC.Everest.Test
{
    public static class TypeExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t")]
        public static string GetCSharpGenericName(this Type t)
        {
            if (!t.IsGenericType)
                return t.Name;

            StringBuilder sb = new StringBuilder();

            sb.Append(t.Name.Remove(t.Name.IndexOf('`')));

            sb.Append("<");

            var parms = t.GetGenericArguments();

            for (int i = 0; i < parms.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");

                sb.Append(parms[i].GetCSharpGenericName());
            }

            sb.Append(">");

            return sb.ToString();

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c")]
        public static bool IsAssignableFromEx(this Type t, Type c)
        {
            if (t.IsAssignableFrom(c))
                return true;

            List<MethodInfo> methods = new List<MethodInfo>();

            methods.AddRange(t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic));
            methods.AddRange(c.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic));

            foreach (var method in methods)
            {
                if (!method.IsHideBySig || !method.IsSpecialName || !method.IsStatic)
                    continue;

                if (method.ReturnType != t)
                    continue;

                var mparms = method.GetParameters();

                if (mparms.Length != 1)
                    continue;

                foreach (var mparm in mparms)
                    if (mparm.ParameterType == c)
                        return true;

            }

            return false;

        }
    }
}
