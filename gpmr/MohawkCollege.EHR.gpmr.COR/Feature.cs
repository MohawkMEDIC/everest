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
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace MohawkCollege.EHR.gpmr.COR
{

    /// <summary>
    /// Fires when a feature has been parsed
    /// </summary>
    internal delegate void FeatureParsedHandler(Feature sender);

    /// <summary>
    /// Represents a base class that identifies a feature
    /// </summary>
    public abstract class Feature
    {

        private string name;
        private Documentation documentation;
        private string businessName;
        private object definedAs;
        private ClassRepository memberOf;

        /// <summary>
        /// Creates a new instance of Feature
        /// </summary>
        public Feature()
        {
            Annotations = new List<Annotation>(10);
        }

        /// <summary>
        /// Annotations
        /// </summary>
        public List<Annotation> Annotations { 
            get; 
            set; 
        }

        /// <summary>
        /// The sort key is used by the renderer to sort features within a container class
        /// </summary>
        public string SortKey { get; set; }

        /// <summary>
        /// The repository this class is a member of
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ClassRepository MemberOf
        {
            get { return memberOf; }
            set { memberOf = value; }
        }

        /// <summary>
        /// Get or set the MIF model element that this feature was defined from
        /// </summary>
        public object DerivedFrom
        {
            get { return definedAs; }
            set { definedAs = value; }
        }
	
        /// <summary>
        /// Get or set the business name of this item
        /// </summary>
        public string BusinessName
        {
            get { return businessName; }
            set { businessName = value; }
        }

        /// <summary>
        /// Get or set documentation related to this feature
        /// </summary>
        public Documentation Documentation
        {
            get { return documentation; }
            set { documentation = value; }
        }


        /// <summary>
        /// Summary documentation
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<string> SummaryDocumentation
        {
            get
            {
                if(Documentation != null)
                    return Documentation.Definition;
                return null;
            }
        }

        /// <summary>
        /// Get or set the name of this feature
        /// </summary>
        public string Name
        {
            get { return name; }
            set 
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Clone this feature, resets the memberOf to null
        /// </summary>
        public virtual object Clone()
        {
            object retVal = this.MemberwiseClone();
            (retVal as Feature).memberOf = null;
            (retVal as Feature).Annotations = new List<Annotation>(Annotations ?? new List<Annotation>());
            return retVal;
        }

        /// <summary>
        /// Event triggered when item has been parsed
        /// </summary>
        internal static event FeatureParsedHandler Parsed;

        /// <summary>
        /// Fires when this item has been parsed or prepared 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public virtual void FireParsed()
        {
            if (Parsed != null) Parsed(this);
        }


    }
}