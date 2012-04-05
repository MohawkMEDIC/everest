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
