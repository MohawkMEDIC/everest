/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 20-4-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.CodeDom;
using MARC.Everest.Sherpas.Templating.Renderer;

namespace MARC.Everest.Sherpas.Templating.Format
{
    /// <summary>
    /// Represents a base class which defines a series of instructions for use in a method (initialization or validation)
    /// </summary>
    [XmlType("MethodDefinitionBase", Namespace = "urn:marc-hi:everest/sherpas/template")]
    public class MethodDefinitionBase : ArtifactTemplateBase, IMethodInstruction
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MethodDefinitionBase()
        {
            this.Instruction = new List<Object>();
        }


        /// <summary>
        /// Identifies the steps to take in the method
        /// </summary>
        [XmlElement("set", typeof(AssignmentStatementDefinition))]
        [XmlElement("call", typeof(MethodInvokationStatementDefinition))]
        [XmlElement("where", typeof(ConditionalStatementDefinition))]
        [XmlElement("construct", typeof(ConstructorInvokationStatementDefinition))]
        [XmlElement("foreach", typeof(IterationStatementDefinition))]
        [XmlElement("block", typeof(MethodDefinitionBase))]
        public virtual List<Object> Instruction { get; set; }

        /// <summary>
        /// To code dom statemnet
        /// </summary>
        public virtual CodeStatementCollection ToCodeDomStatement(RenderContext scope)
        {
            var retVal = new CodeStatementCollection();
            foreach (IMethodInstruction ins in this.Instruction)
            {
                retVal.AddRange(ins.ToCodeDomStatement(scope));
            }
            return retVal;
        }
    }
}
