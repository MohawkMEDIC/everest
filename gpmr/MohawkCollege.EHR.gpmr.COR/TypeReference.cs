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

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Represents a reference to a type
    /// </summary>
    public class TypeReference
    {


        private string name;
        private string flavor;
        private List<TypeReference> genericSuppliers;
        private ClassContent container;
        internal Class cachedClassRef;

        /// <summary>
        /// Add a generic parameter supplier
        /// </summary>
        /// <param name="tr">The generic supplier to add</param>
        /// <param name="ParameterName">The generic parameter name</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Parameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "tr"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public void AddGenericSupplier(string ParameterName, TypeReference typeReference)
        {
            AddGenericSupplier(ParameterName, typeReference, true);

        }

        /// <summary>
        /// The class repository that this is a member of
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ClassRepository MemberOf { get; set; }

        /// <summary>
        /// Get the class's documentation
        /// </summary>
        public Documentation ClassDocumentation 
        {
            get
            {
                if (Class != null)
                    return Class.Documentation;
                return null;
            }
        }

        /// <summary>
        /// Get the name of the datatype this reference points to (null if it is not a datatype reference)
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Datatype")]
        public string CoreDatatypeName
        {
            get 
            {
                if (Class != null) return null;
                return name;
            }
        }

        /// <summary>
        /// Get a reference to the class this object points to (Null if this is a base type)
        /// </summary>
        public Class Class 
        {
            get
            {
                if (cachedClassRef != null) // Already referenced
                    ;//Cleans up the code
                else if (Name == null)
                    cachedClassRef = null;
                else if (MemberOf != null && MemberOf.ContainsKey(Name))
                    cachedClassRef = MemberOf[Name] as Class;
                else if (this.container != null && (Container.MemberOf ?? Container.Container.MemberOf ?? new ClassRepository()).ContainsKey(Name))
                    cachedClassRef = (Container.MemberOf ?? Container.Container.MemberOf)[Name] as Class;
                else if (this.container == null || this.container.Container == null || this.container.Container.MemberOf == null || Name == null)
                    cachedClassRef = null;
                else
                    cachedClassRef = null;
                return cachedClassRef;
            }
        }

        /// <summary>
        /// The property that holds this type reference object
        /// </summary>
        public ClassContent Container
        {
            get { return container; }
            set { container = value; }
        }
	
        /// <summary>
        /// Represents classes that supply context to generics
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<TypeReference> GenericSupplier
        {
            get { return genericSuppliers; }
            set { genericSuppliers = value; }
        }
	
        /// <summary>
        /// Identifies a particular flavor of a type
        /// </summary>
        public string Flavor
        {
            get { return flavor; }
            set { flavor = value; }
        }
	
        /// <summary>
        /// Identifies the name of the type reference
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; cachedClassRef = null; }
        }

        /// <summary>
        /// Convert this to a string
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Name);

            if (Flavor != null)
                sb.AppendFormat("({0})", Flavor);

            // Generics
            if (GenericSupplier != null && GenericSupplier.Count > 0)
            {
                sb.Append("&lt;");
                foreach (TypeReference tr in GenericSupplier)
                    sb.AppendFormat("{0},", tr.ToString());
                sb.Remove(sb.Length - 1, 1);
                sb.Append("&gt;");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        public object Clone()
        {
            object retVal = this.MemberwiseClone();
            (retVal as TypeReference).Container = null;
            (retVal as TypeReference).GenericSupplier = new List<TypeReference>(this.genericSuppliers ?? new List<TypeReference>());
            
            return retVal;
        }

        /// <summary>
        /// Add a generic supplier to the type reference 
        /// </summary>
        public void AddGenericSupplier(string parameterName, TypeReference typeReference, bool throwOnError)
        {
            if (GenericSupplier == null) GenericSupplier = new List<TypeReference>();

            // Sanity check

            if (Class != null && (Class.TypeParameters == null ||
                Class.TypeParameters.Find(o => o.ParameterName == parameterName) == null))
                throw new InvalidOperationException(string.Format("Can't assign type parameter '{0}' on class '{1}' as no such parameter exists", parameterName, Name));

            TypeParameter tp = new TypeParameter();
            tp.ParameterName = parameterName;
            tp.Name = typeReference.name;
            tp.container = typeReference.container;
            tp.MemberOf = typeReference.MemberOf;
            tp.GenericSupplier = typeReference.GenericSupplier;
            tp.Flavor = typeReference.Flavor;

            // Turns out that the generic supplier can in fact contain more than one traversal name
            // for different types that are placed at the point of contact... soo... 
            // this can cause 
            if (GenericSupplier.Exists(o => (o as TypeParameter).ParameterName.Equals(tp.ParameterName)) && throwOnError)
                throw new ArgumentException(String.Format("More than one supplier is provided for the same type parameter '{0}' on class '{1}'. This is not permitted!", tp.ParameterName, Name));

            GenericSupplier.Add(tp);
        }
    }
}
