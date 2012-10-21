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
        public static string GetJavaTool(string basePath, string toolName)
        {
            string[] path = { 
                                "{0}.exe", 
                                "{0}", 
                                "bin{{0}}{0}.exe", 
                                "bin{{0}}{0}" };
            
            foreach (var p in path)
            {
                string spath = Path.Combine(basePath, String.Format(String.Format(p, toolName), Path.DirectorySeparatorChar));
                if (File.Exists(spath))
                    return spath;
            }
            return null;
        }

        /// <summary>
        /// Generate a list of classes recursively
        /// </summary>
        internal static List<String> GenerateClassIndex(string baseDir)
        {
            var retVal = new List<String>();
            foreach (var f in Directory.GetFiles(baseDir, "*.class"))
                retVal.Add(f);
            foreach (var d in Directory.GetDirectories(baseDir))
                retVal.AddRange(GenerateClassIndex(d));
            return retVal;
        }
    }
}
