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
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MohawkCollege.EHR.gpmr.COR
{

    /// <summary>
    /// Identifies the operator type
    /// </summary>
    public enum Operator
    {
        Union,
        Intersect
    }

    /// <summary>
    /// Filter expression
    /// </summary>
    [Flags]
    public enum Filter
    {
        All = 1,
        Specializations = 2,
        Include = 4,
        Exclude = 8,
        ExcludeSpecializations = Exclude | Specializations,
        IncludeSpecializations = Include | Specializations,
        IncludeAll = Include | All,
        ExcludeAll = Exclude | All
    }

    /// <summary>
    /// Represents a filter expression
    /// </summary>
    public class FilterExpression : Feature
    {

        // Cache of from reference
        private Enumeration fromReference;

        private Enumeration.EnumerationValue valueReference;

        /// <summary>
        /// Gets or sets the operator for the expression item
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// Gets or sets the filter expression
        /// </summary>
        public Filter Filter { get; set; }

        /// <summary>
        /// Gets or sets the reference to the "From" value set
        /// </summary>
        public Enumeration FromReference
        {
            get
            {
                if (this.fromReference != null)
                    ;
                else if (this.From == null || this.MemberOf == null)
                    return null;
                else 
                    this.fromReference = this.MemberOf.Find(o => o.Name == this.From) as Enumeration;
                return this.fromReference;
            }
            set
            {
                if (value == null)
                {
                    this.MemberOf = null;
                    this.From = null;
                    this.fromReference = null;
                }
                else
                {
                    this.MemberOf = value.MemberOf;
                    //this.From = value.Name;
                    this.fromReference = value;
                }
            }
        }

        /// <summary>
        /// Gets the enumeration from value reference
        /// </summary>
        public Enumeration.EnumerationValue HeadCodeReference
        {
            get
            {
                if (this.HeadCode == null || this.FromReference == null)
                    return null;
                return this.FromReference.Literals.Find(o => o.Name == this.HeadCode);
            }
            set
            {
                this.HeadCode = value.Name;
                this.valueReference = value;
            }
        }

        /// <summary>
        /// Gets or sets the head code
        /// </summary>
        public string HeadCode { get; set; }


        /// <summary>
        /// Gets or sets the name of the expression
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// A filter that is free text rather than code system based
        /// </summary>
        public string Property { get; set; }

    }

    /// <summary>
    /// Represents a grouped filter expression
    /// </summary>
    public class GroupFilterExpression : FilterExpression
    {

        /// <summary>
        /// New expression group
        /// </summary>
        public GroupFilterExpression()
        {
            this.SubExpressions = new List<FilterExpression>();
        }

        /// <summary>
        /// Sub expressions
        /// </summary>
        public List<FilterExpression> SubExpressions { get; set; }
    }

    /// <summary>
    /// A filter expression which is only text
    /// </summary>
    public class TextFilterExpression : FilterExpression
    {
        /// <summary>
        /// Gets or sets the text of the filter
        /// </summary>
        public List<String> Text { get; set; }
    }

    /// <summary>
    /// Represents a subset of codes that are selected from a code system
    /// </summary>
    public class SubsetFilterExpression : FilterExpression
    {
        /// <summary>
        /// Gets or sets the code reference
        /// </summary>
        public List<Enumeration.EnumerationValue> CodesReference
        {
            get
            {
                if (this.Codes == null || this.FromReference == null)
                    return null;

                List<Enumeration.EnumerationValue> evs = new List<Enumeration.EnumerationValue>(this.Codes.Count);
                foreach (var cv in this.Codes)
                    evs.Add(this.FromReference.Literals.Find(o => o.Name == cv));
                return evs;
            }
            set
            {
                if (value == null)
                    this.Codes = null;
                else
                {
                    this.Codes = new List<string>(value.Count);
                    foreach (var cv in value)
                        this.Codes.Add(cv.Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the string code mnemonics
        /// </summary>
        public List<String> Codes { get; set; }
    }
}
