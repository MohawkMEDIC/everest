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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Represents a package, or a namespace. A grouping of classes
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SubSystem")]
    public class SubSystem : Feature
    {
        private List<Class> entryPoint;
        private string graphicRepresentation;

        /// <summary>
        /// Find a class within the subsystem
        /// </summary>
        /// <param name="ClassName">The name of the class to locate</param>
        /// <returns>The located class</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Class")]
        public Class FindClass(string ClassName)
        {
            return OwnedClasses.Find(o => o.Name == ClassName);
        }

        /// <summary>
        /// Get classes this sub-system owns
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Class> OwnedClasses
        {
            get
            {
                List<Class> retVal = new List<Class>();
                foreach (KeyValuePair<string, Feature> f in MemberOf)
                    if (f.Value is Class && (f.Value as Class).ContainerName == Name)
                        retVal.Add(f.Value as Class);
                return retVal;
            }
        }

        /// <summary>
        /// Get the graphic representation of this subsystem
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public string GraphicRepresentation
        {
            get 
            {
                if (graphicRepresentation != null) return graphicRepresentation;

                // Dictionary of Classes to draw in the sub-system they belong to
                Dictionary<string, List<Class>> classes = new Dictionary<string, List<Class>>();
                classes.Add(Name, new List<Class>()); // Add one for this package

                // Get all classes in this package
                foreach (KeyValuePair<string, Feature> kv in MemberOf)
                    if (kv.Value is Class && (kv.Value as Class).ContainerPackage == this)
                        classes[this.Name].Add(kv.Value as Class);

                // Foreach Class, get the associations so we may build packages for those
                foreach (Class cls in classes[this.Name])
                {
                    // Realizations
                    //foreach (TypeReference tr in cls.Realizations ?? new List<TypeReference>())
                    //{
                    //    // Add the package to the list of classes
                    //    if (tr.Class != null && !classes.ContainsKey(tr.Class.ContainerPackage.Name))
                    //        classes.Add(tr.Class.ContainerPackage.Name, new List<Class>());
                    //    // Add the class
                    //    if (tr.Class != null)
                    //        classes[tr.Class.ContainerPackage.Name].Add(tr.Class);
                    //}

                    foreach (ClassContent cnt in cls.Content)
                        if (cnt is Property && (cnt as Property).PropertyType == Property.PropertyTypes.TraversableAssociation &&
                            (cnt as Property).Type.Class != null && (cnt as Property).Type.Class.ContainerPackage != this) // We'll add the class
                        {
                            // Add the package to the list of classes
                            if (!classes.ContainsKey((cnt as Property).Type.Class.ContainerPackage.Name))
                                classes.Add((cnt as Property).Type.Class.ContainerPackage.Name, new List<Class>());
                            // Add the class
                            classes[(cnt as Property).Type.Class.ContainerPackage.Name].Add((cnt as Property).Type.Class);
                        }
                }

                // Now layout
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("digraph subsy_{0} {{ ratio=compress rankdir=BT fontname=\"Arial\" fontsize=10 ", Name);
                
                // Draw Contents
                foreach (KeyValuePair<string, List<Class>> kv in classes)
                    if (kv.Key != this.Name)
                        sb.Append(LayoutSubsystem(kv.Value, kv.Key));
                sb.Append(LayoutSubsystem(classes[this.Name], Name));


                sb.Append("}");

                graphicRepresentation = sb.ToString();

                return graphicRepresentation; 
            }
        }
        /// <summary>
        /// Layout a subsystem
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        //TODO: add descriptions for parameters.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private string LayoutSubsystem(List<Class> classes, string name)
        {

            StringBuilder sb = new StringBuilder();

            // Package
            sb.AppendFormat("subgraph cluster{0} {{ labeljust=l labelloc=b label=\"Subsystem {0}\" ", name);

            foreach (Class cls in classes) // Classes
            {
                // Have not defined this class yet
                if (!sb.ToString().Contains(string.Format("{1}_{0} [label=\"{{{0}", cls.Name, cls.ContainerName)))
                {
                    sb.AppendFormat("{0} ", cls.GraphicalRepresentationClassOnly().Replace("color=red", "color=black"));

                    // Associations
                    foreach (ClassContent cc in cls.Content)
                        if (cc is Property && (cc as Property).PropertyType == Property.PropertyTypes.TraversableAssociation)
                        {
                            if ((cc as Property).Type.Class != null && !sb.ToString().Contains(string.Format("{1}_{0} [label=\"{{{0}|", (cc as Property).Type.Class.Name, (cc as Property).Type.Class.ContainerName)))
                                sb.Append((cc as Property).Type.Class.GraphicalRepresentationClassOnly().Replace("color=red", "color=black"));

                            sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 style=\"solid\" arrowhead=none headlabel=\"{0}..{1}\" label=\"{2}\" color=black] {5}_{3}->{6}{4} ", (cc as Property).MinOccurs, (cc as Property).MaxOccurs, cc.Name, cls.Name, (cc as Property).Type.Class != null ? (cc as Property).Type.Class.Name : (cc as Property).Type.Name, cls.ContainerName, (cc as Property).Type.Class != null ? (cc as Property).Type.Class.ContainerName + "_" : "");
                        }
                }
            }
            sb.Append("} ");

            return sb.ToString();
        }


        /// <summary>
        /// Entry points into the sub-system. If the sub system is to represent a 
        /// serializable instance, which class(es) are valid entry points into the serialization
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Class> EntryPoint
        {
            get { return entryPoint; }
            set { entryPoint = value; }
        }


        
        /// <summary>
        /// Construct a string of this sub-system
        /// </summary>
        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("subsystem {0} {1};",
                Name,
                BusinessName != null ? "" : " alias '" + BusinessName + "'");

            return sb.ToString();

        }

    }
}
