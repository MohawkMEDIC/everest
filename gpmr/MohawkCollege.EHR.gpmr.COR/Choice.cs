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
    /// Data used must be one of the properties or choice within this class content model
    /// </summary>
    public class Choice : ClassContent
    {

        /// <summary>
        /// Create a new instance of choice. Assigns a guid name to the choice
        /// </summary>
        public Choice()
            : base()
        {
            base.Name = Guid.NewGuid().ToString();
        }

        private List<ClassContent> content;

        /// <summary>
        /// The content that must be chosen in this model
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<ClassContent> Content
        {
            get { return content; }
            set { content = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} choice [{1}..{2}] (",
                Conformance, MinOccurs, MaxOccurs);

            //if (Content == null)
            //    System.Diagnostics.Debugger.Break();

            foreach (ClassContent cc in Content)
                sb.AppendFormat("\t{0}\r\n", cc.ToString());

            sb.Append(");");
            return sb.ToString();
        }
    }
}