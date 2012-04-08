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
 * Date: $date$
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace MARC.Everest.VisualStudio.Wizards.Proxy
{
    /// <summary>
    /// </summary>
    public class StructureListProxyClass :
         Component
    {
        /// <summary>
        /// Interaction data structure
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), Serializable]
        public struct StructureData 
        {
            public string structureType; 
            public string typeName;
            public string assemblyName;
            public string displayName;
            public string typeNamespace;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void LoadAssembly(string fileName)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Loading assembly {0}...", fileName));
            Assembly.LoadFrom(fileName);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<StructureData> GetStructures()
        {

            // Structure attribute type
            Assembly everestCoreAssembly = Array.Find<Assembly>(AppDomain.CurrentDomain.GetAssemblies(), o => o.GetName().Name == "MARC.Everest");

            if (everestCoreAssembly == null)
                return null;

            Type structureAttributeType = everestCoreAssembly.GetType("MARC.Everest.Attributes.StructureAttribute");


            // Return value
            List<StructureData> retVal = new List<StructureData>();

            foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                foreach(Type t in asm.GetTypes())
                    // IGraphable
                    if (t.GetInterface("MARC.Everest.Interfaces.IGraphable") != null)
                    {
                        object[] descriptionAttributes = t.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        object[] structureAttributes = t.GetCustomAttributes(structureAttributeType, true);

                        // Get struct and description attribute values
                        if (descriptionAttributes.Length == 0 || structureAttributes.Length == 0)
                            continue;

                        StructureData data = new StructureData();
                        data.assemblyName = asm.FullName;
                        data.typeName = t.Name;
                        data.typeNamespace = t.Namespace;
                        data.displayName = (descriptionAttributes[0] as DescriptionAttribute).Description;
                        data.structureType = structureAttributeType.GetProperty("StructureType").GetValue(structureAttributes[0], null).ToString();
                        retVal.Add(data); 
                    }

            return retVal; // return retVal
        }

    }
}