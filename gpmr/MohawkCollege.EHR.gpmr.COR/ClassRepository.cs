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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Represents a repository of classes
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2229:ImplementSerializationConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [Serializable]
    public class ClassRepository : Dictionary<string, Feature>
    {

        public ClassRepository() : base()
        {
            Feature.Parsed += new FeatureParsedHandler(Feature_Parsed);
        }

        /// <summary>
        /// Imports any new features into the class library
        /// </summary>
        /// <param name="sender"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        void Feature_Parsed(Feature sender)
        {

            string FullName = sender is Class && (sender as Class).ContainerPackage != null ?
                    string.Format("{0}.{1}", (sender as Class).ContainerName, sender.Name) :
                    sender is Enumeration ? String.Format("{0}.{1}", (sender as Enumeration).EnumerationType, sender.Name) :
                    sender.Name;
            sender.MemberOf = this;

            Feature cf = null;
            if (!this.TryGetValue(FullName, out cf))
                this.Add(FullName, sender);
            else if (cf.GetType() != sender.GetType())
            {
                Trace.WriteLine(String.Format("Name clash '{0}' of type '{1}' and '{2}' of type '{3}' have the same name...", sender.Name,
                    sender.GetType().Name, cf.Name, cf.GetType().Name), "warn");
                this.Add(FullName, sender);
            }

        }

        /// <summary>
        /// Find a feature
        /// </summary>
        public Feature Find(Predicate<Feature> match)
        {
            foreach (KeyValuePair<String, Feature> kv in this)
                if (match(kv.Value))
                    return kv.Value;
            return null;
        }

        /// <summary>
        /// Find a feature
        /// </summary>
        public List<Feature> FindAll(Predicate<Feature> match)
        {
            List<Feature> retVal = new List<Feature>();
            foreach (KeyValuePair<String, Feature> kv in this)
                if (match(kv.Value))
                    retVal.Add(kv.Value);
            return retVal;
        }

        /// <summary>
        /// Represent this repository as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, Feature> f in this)
                sb.AppendFormat("{0}\r\n", f.Value.ToString());

            return sb.ToString();
        }

    }
}