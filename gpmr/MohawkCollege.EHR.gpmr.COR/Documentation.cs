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
    /// Documentation represents any documentation related to an item
    /// </summary>
    public class Documentation
    {

        /// <summary>
        /// Represents a titled documentation
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class TitledDocumentation
        {
            private string title;
            private string name;
            private List<string> text = new List<string>();

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
            public List<string> Text
            {
                get { return text; }
                set { text = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
	
            public string Title
            {
                get { return title; }
                set { title = value; }
            }
	
        }

        private List<String> description;
        private List<String> definition;
        private List<String> rationale;
        private List<String> walkthrough;
        private string copyright;
        private List<TitledDocumentation> other;
        private List<TitledDocumentation> appendix;
        private List<String> usage;

        /// <summary>
        /// Gets or sets the terms under which this item can be used (usually found on subsystems)
        /// </summary>
        public List<string> LicenseTerms { get; set; }

        /// <summary>
        /// Gets or sets the disclaimer elements
        /// </summary>
        public List<string> Disclaimer { get; set; }

        /// <summary>
        /// Gets or sets a list of contributors
        /// </summary>
        public List<string> Contributors { get; set; }

        /// <summary>
        /// Usage instructions
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<String> Usage
        {
            get { return usage; }
            set { usage = value; }
        }

        /// <summary>
        /// Appendicies
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<TitledDocumentation> Appendix
        {
            get { return appendix; }
            set { appendix = value; }
        }
	
        /// <summary>
        /// Additional documentation
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<TitledDocumentation> Other
        {
            get { return other; }
            set { other = value; }
        }

        /// <summary>
        /// Get or set the copyright information
        /// </summary>
        public string Copyright
        {
            get { return copyright; }
            set { copyright = value; }
        }
	
        /// <summary>
        /// Get or set the walkthrough information
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<String> Walkthrough
        {
            get { return walkthrough; }
            set { walkthrough = value; }
        }

        /// <summary>
        /// Get or set the reasoning for having this item
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<String> Rationale
        {
            get { return rationale; }
            set { rationale = value; }
        }

        /// <summary>
        /// Get or set the definition of this feature
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<String> Definition
        {
            get { return definition; }
            set { definition = value; }
        }
	
        /// <summary>
        /// Get or set the description of this feature
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<String> Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Returns true if the documentation is for all intents purposes ... empty
        /// </summary>
        /// <param name="d">The documentation item to check</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "d")]
        public static bool IsEmpty(Documentation d)
        {

            return d == null || (d.Appendix == null || d.Appendix.Count == 0) && d.Copyright == null && (d.Definition == null || d.Definition.Count == 0) &&
                (d.Description == null || d.Description.Count == 0) && (d.Other == null || d.other.Count == 0) && (d.Rationale == null || d.Rationale.Count == 0) 
                && (d.Usage == null || d.usage.Count == 0) && (d.Walkthrough == null || d.Walkthrough.Count == 0);
        }

        /// <summary>
        /// String representation of the object (Returns the definition)
        /// </summary>
        public override string ToString()
        {
            string retVal = "";
            foreach (String s in Definition ?? new List<String>())
                retVal += s;
            foreach (String s in Description ?? new List<String>())
                retVal += s;
            return retVal;
        }


        /// <summary>
        /// Merge <paramref name="documentation"/> with the current documentation
        /// </summary>
        public void Merge(Documentation documentation)
        {

            // Appendix
            if (documentation.Appendix != null)
            {
                if (this.Appendix == null) this.Appendix = new List<TitledDocumentation>();
                this.Appendix.AddRange(documentation.Appendix);
            }

            // Contributors
            if (documentation.Contributors != null)
            {
                if (this.Contributors == null) this.Contributors = new List<String>();
                this.Contributors.AddRange(documentation.Contributors);
            }

            // Definition
            if (documentation.Definition != null)
            {
                if (this.Definition == null) this.Definition = new List<String>();
                this.Definition.AddRange(documentation.Definition);
            }

            // Description
            if (documentation.Description != null)
            {
                if (this.Description == null) this.Description = new List<String>();
                this.Description.AddRange(documentation.Description);
            }

            // Disclaimer
            if (documentation.Disclaimer != null)
            {
                if (this.Disclaimer == null) this.Disclaimer = new List<String>();
                this.Disclaimer.AddRange(documentation.Disclaimer);
            }

            // Other
            if (documentation.Other != null)
            {
                if (this.Other == null) this.Other = new List<TitledDocumentation>();
                this.Other.AddRange(documentation.Other);
            }

            // Rationale
            if (documentation.Rationale != null)
            {
                if (this.Rationale == null) this.Rationale = new List<String>();
                this.Rationale.AddRange(documentation.Rationale);
            }

            // Usage
            if (documentation.Usage != null)
            {
                if (this.Usage == null) this.Usage = new List<String>();
                this.Usage.AddRange(documentation.Usage);
            }

            // Walkthrough
            if (documentation.Walkthrough != null)
            {
                if (this.Walkthrough == null) this.Walkthrough = new List<String>();
                this.Walkthrough.AddRange(documentation.Walkthrough);
            }

            // License terms
            if (documentation.LicenseTerms != null)
            {
                if (this.LicenseTerms == null) this.LicenseTerms = new List<string>();
                this.LicenseTerms.AddRange(documentation.LicenseTerms);
            }
        }
    }
}