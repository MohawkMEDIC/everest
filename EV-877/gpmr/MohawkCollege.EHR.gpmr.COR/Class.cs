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
using System.Drawing;
using System.IO;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Represents a class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Class")]
    public class Class : Feature
    {

        // Class diagram constants
        private const int CD_PADDING_X = 6;
        private const int CD_ASSOC_LINE_LENGTH = 50;
        private const int CD_PADDING_Y = 2;
        private const int FONT_SIZE = 12;

        private List<ClassContent> content;
        private bool _abstract;
        private TypeReference baseClass;
        private SubSystem container;

        /// <summary>
        /// Get the container sub-system's name
        /// </summary>
        public string ContainerName { get { return container.Name; } }
        private List<TypeReference> specializedBy;
        //private List<ClassContent> associatedWith;
        private string classDiagram = null;
        private List<TypeReference> realizations;
        private List<TypeParameter> typeParameters;

        /// <summary>
        /// Identifies the names of type parameters
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<TypeParameter> TypeParameters
        {
            get { return typeParameters; }
            set { typeParameters = value; }
        }
	
        /// <summary>
        /// For items that are serialized, identifies the RIM class or CMET
        /// that this class is derived from
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<TypeReference> Realizations
        {
            get { return realizations; }
            set { realizations = value; }
        }

        /// <summary>
        /// Represent this image as a base64 encoded PNG of a class diagram representing the class structure
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public string GraphicalRepresentation 
        {
            get
            {
                if (classDiagram != null) return classDiagram;

                // Dictionary of Classes to draw in the sub-system they belong to
                Dictionary<string, List<Class>> classes = new Dictionary<string, List<Class>>();
                classes.Add(ContainerPackage.Name, new List<Class>()); // Add one for this package

                // Get all classes in this package
                classes[ContainerPackage.Name].Add(this);

                // Base Classes
                TypeReference bc = this.baseClass;
                while (bc != null && bc.Class != null)
                {
                    // Add the package to the list of classes
                    if (bc.Class != null && !classes.ContainsKey(bc.Class.ContainerPackage.Name))
                        classes.Add(bc.Class.ContainerPackage.Name, new List<Class>());
                        // Add the class
                    if(bc.Class != null)
                        classes[bc.Class.ContainerPackage.Name].Add(bc.Class);
                    bc = bc.Class.BaseClass;
                }

                // Realizations
                //foreach (TypeReference tr in Realizations ?? new List<TypeReference>())
                //{
                //    // Add the package to the list of classes
                //    if (tr.Class != null && !classes.ContainsKey(tr.Class.ContainerPackage.Name))
                //        classes.Add(tr.Class.ContainerPackage.Name, new List<Class>());
                //    // Add the class
                //    if (tr.Class != null)
                //        classes[tr.Class.ContainerPackage.Name].Add(tr.Class);
                //}

                // Foreach Class, get the associations so we may build packages for those
                List<ClassContent> myContent = new List<ClassContent>();
                myContent.AddRange(Content);
                myContent.AddRange(BaseContent);


                foreach (ClassContent cnt in myContent)
                    if (cnt is Property && (cnt as Property).PropertyType == Property.PropertyTypes.TraversableAssociation &&
                        (cnt as Property).Type.Class != null && (cnt as Property).Type.Class.ContainerPackage != this.ContainerPackage) // We'll add the class
                    {
                        // Add the package to the list of classes
                        if (!classes.ContainsKey((cnt as Property).Type.Class.ContainerPackage.Name))
                            classes.Add((cnt as Property).Type.Class.ContainerPackage.Name, new List<Class>());
                        // Add the class
                        classes[(cnt as Property).Type.Class.ContainerPackage.Name].Add((cnt as Property).Type.Class);
                    }

                // Now layout
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("digraph class_{0} {{ ratio=compress rankdir=BT fontname=\"Arial\" fontsize=10 ", Name);

                // Draw Contents
                foreach (KeyValuePair<string, List<Class>> kv in classes)
                    if(kv.Key != this.ContainerName)
                        sb.Append(LayoutSubsystem(kv.Value, kv.Key));
                sb.Append(LayoutSubsystem(classes[this.ContainerName], this.ContainerName));

                sb.Append("}");

                classDiagram = sb.ToString();

                return classDiagram;
            }
        }

        /// <summary>
        /// Layout a subsystem
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
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

                            sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 arrowhead=none headlabel=\"{0}..{1}\" label=\"{2}\" color=black style=\"solid\"] {5}_{3}->{6}{4} ", (cc as Property).MinOccurs, (cc as Property).MaxOccurs, cc.Name, cls.Name, (cc as Property).Type.Class != null ? (cc as Property).Type.Class.Name : (cc as Property).Type.Name, cls.ContainerName, (cc as Property).Type.Class != null ? (cc as Property).Type.Class.ContainerName + "_" : "");
                        }
                }
            }
            sb.Append("} ");

            return sb.ToString();
        }

        /// <summary>
        /// Get only the class representation
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        internal string GraphicalRepresentationClassOnly()
        {
            StringBuilder sb = new StringBuilder();

            // Members
            string memberString = "";
            foreach (ClassContent cc in this.content)
                if (cc is Property)
                    switch ((cc as Property).PropertyType)
                    {
                        case Property.PropertyTypes.Structural:
                            memberString += string.Format("@ {0}{2} : {1}\\l", cc.Name, (cc as Property).Type.ToString().Replace("&gt;", "\\>").Replace("&lt;", "\\<"), cc.BusinessName != null ? " (" + cc.BusinessName.Replace("&gt;", "\\>").Replace("&lt;", "\\<").Replace("\r","").Replace("\n","") + ")" : "");
                            break;
                        case Property.PropertyTypes.NonStructural:
                            memberString += string.Format("+ {0}{2} : {1}\\l", cc.Name, (cc as Property).Type.ToString().Replace("&gt;", "\\>").Replace("&lt;", "\\<"), cc.BusinessName != null ? " (" + cc.BusinessName.Replace("&gt;", "\\>").Replace("&lt;", "\\<").Replace("\r", "").Replace("\n", "") + ")" : "");
                            break;
                    }

            // Declare this as a node
            sb.AppendFormat("node [fontname=\"Arial\" fontsize=10 shape=record color=red] {2}_{0} [label=\"{{{0}{3}|{1}}}\"] ", Name, memberString, ContainerName, BusinessName != null ? " (" + BusinessName.Replace("&gt;", "\\>").Replace("&lt;", "\\<").Replace("\r", "").Replace("\n", "") + ")" : "");

            // Draw inheritence
            // Get the base representation
            if (this.baseClass != null && this.baseClass.Class != null)
                sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 arrowhead=empty color=black style=\"solid\" headlabel=\"\" label=\"\"] node[color=black] {2}_{0}->{3}_{1} ", Name, baseClass.Class.Name, ContainerName, baseClass.Class.ContainerName);
            // Draw Realization or Projections
            if (this.Realizations != null)
                foreach (TypeReference tr in Realizations)
                    sb.AppendFormat("edge [arrowhead=empty color=black style=dashed headlabel=\"\" label=\"\"] node[color=black] {2}_{0}->{3}_{1} ", Name, tr.Class.Name, ContainerName, tr.Class.ContainerName);

            return sb.ToString();
        }

        /// <summary>
        /// Specialized by
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<TypeReference> SpecializedBy
        {
            get { return specializedBy; }
            set { specializedBy = value; }
        }
	
        /// <summary>
        /// Get all members defined in base classes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<ClassContent> BaseContent 
        {
            get
            {
                if(BaseClass == null) return new List<ClassContent>(); // No base class

                List<ClassContent> retVal = new List<ClassContent>();

                // Get base class
                if (BaseClass.Class == null) return null; // Base class is not defined
                retVal.AddRange(baseClass.Class.BaseContent ?? new List<ClassContent>());
                retVal.AddRange(baseClass.Class.Content);

                return retVal;
            }
        }

        /// <summary>
        /// Gets or sets the package this class resides in
        /// </summary>
        public SubSystem ContainerPackage
        {
            get { return container; }
            set { container = value; }
        }
	
        /// <summary>
        /// The class that acts as a base to this class
        /// </summary>
        public TypeReference BaseClass
        {
            get { return baseClass; }
            set { baseClass = value; }
        }

        /// <summary>
        /// Get or set if this class is abstract
        /// </summary>
        public bool IsAbstract
        {
            get { return _abstract; }
            set { _abstract = value; }
        }
	
        /// <summary>
        /// Contents of this class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<ClassContent> Content
        {
            get { return content; }
            set { content = value; }
        }

        
        /// <summary>
        /// Find a structural member
        /// </summary>
        /// <param name="Name">The name of the member/Property</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Name"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Deep")]
        /// <param name="Deep">True if a deep find is to be performed on all parent classes</param>
        /// <param name="propType">The type of proeprty to find</param>
        public Property FindMember(string Name, bool Deep, Property.PropertyTypes propType)
        {

            foreach (ClassContent c in this.Content)
                if (c is Property && ((c as Property).PropertyType & propType) == (c as Property).PropertyType && (c as Property).Name == Name)
                    return c as Property;

            if(Deep) //Deep search?
                foreach (ClassContent c in this.BaseContent ?? new List<ClassContent>())
                    if (c is Property && ((c as Property).PropertyType & propType) == propType && (c as Property).Name == Name)
                        return c as Property;

            return null;
        }

        
        /// <summary>
        /// Represent this class as a string
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();

            // Template parameter
            string tprString = "";
            string whereString = "";
            foreach (TypeParameter t in TypeParameters ?? new List<TypeParameter>())
            {
                tprString += t.ParameterName + ",";
                if (t.Class != null)
                {
                    if (string.IsNullOrEmpty(whereString)) whereString = "where ";
                    else whereString += ",";
                    whereString += string.Format("{0}:{1}", (t as TypeReference).ToString());
                }
            }
            tprString = tprString.Length > 0 ? "<" + tprString.Substring(0, tprString.Length - 1) + ">" : "";

            sb.AppendFormat("{0}class {1}.{2}{5} {3}{4} {6}(\r\n",
                IsAbstract ? "abstract " : "concrete ",
                ContainerPackage.Name,
                Name,
                baseClass == null ? "" : " : " + baseClass.ToString(),
                BusinessName == null ? "" : " alias '" + BusinessName + "'",
                tprString, 
                whereString);

            foreach (ClassContent cc in Content)
            {
                if (BaseClass != null && BaseClass.Class != null && BaseClass.Class.FindMember(cc.Name, true,  Property.PropertyTypes.Structural) != null) // We could have an override
                    sb.AppendFormat("\toverride {0}\r\n", cc.ToString());
                else
                    sb.AppendFormat("\t{0}\r\n", cc.ToString());
            }
            
            sb.Append(");");

            return sb.ToString();
        }

        /// <summary>
        /// Create a type reference object that points to this class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public TypeReference CreateTypeReference()
        {
            TypeReference tr = new TypeReference();
            tr.Name = string.Format("{0}.{1}", this.ContainerName, Name);
            tr.Container = new Property();
            tr.Container.Container = this;
            tr.Flavor = null;
            tr.GenericSupplier = null;
            tr.cachedClassRef = this;
            return tr;
        }

        /// <summary>
        /// Add a type parameter to this class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "p")]
        public void AddTypeParameter(TypeParameter p)
        {
            p.Container = new Property();
            p.Container.Container = this;
            if (TypeParameters == null) TypeParameters = new List<TypeParameter>();
            TypeParameters.Add(p);
        }

        /// <summary>
        /// Add content to the class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c")]
        public void AddContent(ClassContent c)
        {
            c.Container = this;
            content.Add(c);

            // Sort, any structural properties come before non structural props.
            content.Sort(new ClassContent.Comparator());
        }

        /// <summary>
        /// Get the full content (defined in this class and in all base classes)
        /// </summary>
        public List<ClassContent> GetFullContent()
        {
            List<ClassContent> content = new List<ClassContent>(BaseContent);
            content.AddRange(Content);
            return content;
        }
    }
}
