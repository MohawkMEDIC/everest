using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Java.Util
{
    /// <summary>
    /// Java utility classes
    /// </summary>
    public static class JabaUtils
    {

        /// <summary>
        /// Creates a directory name from a package name
        /// </summary>
        public static string PackageNameToDirectory(string packageName)
        {
            String output = "";
            foreach (var part in packageName.Split('.'))
                output = Path.Combine(output, part);
            return output;
        }

        /// <summary>
        /// Gets the java path
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string GetJavaCPath(string basePath)
        {
            string[] path = { "javac.exe", "javac", "bin{0}javac.exe", "bin{0}javac" };
            foreach (var p in path)
            {
                string spath = Path.Combine(basePath, String.Format(p, Path.DirectorySeparatorChar));
                if (File.Exists(spath))
                    return spath;
            }
            return null;
        }
    }
}
