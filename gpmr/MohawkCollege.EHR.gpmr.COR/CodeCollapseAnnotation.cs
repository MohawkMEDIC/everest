/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * Date: 08-11-2009
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Annotates a feature to notify that it represents a collapsing
    /// </summary>
    public class CodeCollapseAnnotation : Annotation
    {
        /// <summary>
        /// Gets or sets the name of the collapsed member
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Order of the annotation
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Gets or sets the original property that this collapsation was applied against
        /// </summary>
        public ClassContent Property
        {
            get
            {
                return property;
            }
            set
            {
                property = value;
                if (property is Property) 
                    originalType = (property as Property).Type;
            }
        }
        /// <summary>
        /// Gets the original type reference
        /// </summary>
        public TypeReference OriginalType { get { return originalType; } }
        /// <summary>
        /// Gets the original type that Property was set to
        /// </summary>
        private TypeReference originalType = null;
        private ClassContent property = null;

        public override string ToString()
        {
            string propString = "";
            if (originalType.Class != null)
                foreach (ClassContent cc in originalType.Class.Content)
                    if (cc is Property && (cc as Property).PropertyType == MohawkCollege.EHR.gpmr.COR.Property.PropertyTypes.Structural &&
                        !String.IsNullOrEmpty((cc as Property).FixedValue))
                        propString += String.Format("{0}='{1}',", cc.Name, (cc as Property).FixedValue);
            return string.Format("Name={0},Order={1},{2}", Name, Order, propString);
        }
    }
}
