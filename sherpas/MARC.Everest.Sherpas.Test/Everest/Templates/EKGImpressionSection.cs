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
 * Date: 26-4-2014
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.Attributes;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using MARC.Everest.DataTypes;
using MARC.Everest.Sherpas.Interface;
using MARC.Everest.Connectors;
using MARC.Everest.Sherpas.ResultDetail;
using System.ComponentModel;
using MARC.Everest.Sherpas.Formatter.XML.ITS1;

// A hand written template based on the sandbox-decor sample xml file ... illustrates how the generator should generate template classes
namespace MARC.Everest.Test.Sherpas.Templates
{

    [Template("2.16.840.1.113883.3.1937.99.61.3.10.3001")]
    [Structure(Name = "Section", StructureType = StructureAttribute.StructureAttributeType.MessageType, IsEntryPoint = false, Model = "POCD_MT000040UV", Publisher = "Copyright (C)2011, Health Level Seven")]
    public sealed class EKGImpressionSection : Section, IMessageTypeTemplate
    {

        private bool m_wasInitialized = false;

        /// <summary>
        /// Class code (which is fixed)
        /// </summary>
        [Property(Name = "classCode", PropertyType = PropertyAttribute.AttributeAttributeType.Structural, Conformance = PropertyAttribute.AttributeConformanceType.Optional, SortKey = 0, DefaultUpdateMode = UpdateMode.Unknown, FixedValue = "DOCSECT", SupplierDomain = "2.16.840.1.113883.5.6")]
        public override CS<ActClassDocumentSection> ClassCode { get; set; }
        
        /// <summary>
        /// The code documentation ... blah
        /// </summary>
        [Property(Name = "code", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, MinOccurs = 1, MaxOccurs = 1, Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, SortKey = 3, DefaultUpdateMode = UpdateMode.Unknown, SupplierDomain = "2.16.840.1.113883.6.1")]
        public new CD<System.String> Code { get; set; }

        /// <summary>
        /// Title of the document
        /// </summary>
        [Property(Name = "title", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, SortKey = 4, DefaultUpdateMode = UpdateMode.Unknown)]
        public override ST Title { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        [Property(Name = "text", PropertyType = PropertyAttribute.AttributeAttributeType.NonStructural, Conformance = PropertyAttribute.AttributeConformanceType.Mandatory, SortKey = 5, DefaultUpdateMode = UpdateMode.Unknown)]
        public override SD Text { get; set; }

        /// <summary>
        /// Initialize the tempalte instance with defaults, no need to call this if you're using the factory
        /// </summary>
        public void InitializeInstance()
        {

            if (this.m_wasInitialized) return; // initialize is only allowed once
            this.m_wasInitialized = true;

            if (this.ClassCode == null)
            {
                this.ClassCode = new CS<ActClassDocumentSection>();
                this.ClassCode.Code = ActClassDocumentSection.DOCSECT;
            }

            if (this.TemplateId == null)
            {
                // When a list
                II temp_GUID_ = new II();
                temp_GUID_.Root = "2.16.840.1.113883.3.1937.99.61.3.10.3001";
                this.TemplateId = new LIST<II>();
                this.TemplateId.Add(temp_GUID_);
            }

            if (this.Code == null)
            {
                this.Code = new CD<string>();
                this.Code.Code = "18844-1";
                this.Code.CodeSystem = "2.16.840.1.113883.6.1";
            }
        }

        /// <summary>
        /// Validate extended
        /// </summary>
        public override IEnumerable<Connectors.IResultDetail> ValidateEx()
        {
            // Additional rules could be added
            var retVal = new List<IResultDetail>(base.ValidateEx());

            if (this.Code != null)
            {
                if (this.Code.Code != "18844-1")
                    retVal.Add(new FixedValueMisMatchedResultDetail(this.Code.Code, "18844-1", true, null));
                if (this.Code.CodeSystem != "2.16.840.1.113883.6.1")
                    retVal.Add(new FixedValueMisMatchedResultDetail(this.Code.CodeSystem, "2.16.840.1.113883.6.1", true, null));
            }

            // ETC
            return retVal;
        }

    }

    /// <summary>
    /// Section factory for use by deelopers
    /// </summary>
    public static partial class SectionFactory
    {

        /// <summary>
        /// Factory method
        /// </summary>
        public static EKGImpressionSection CreateEKGImpressionSection(ST title, SD text)
        {
            var retVal = new EKGImpressionSection();
            retVal.Title = title;
            retVal.Text = text;
            retVal.InitializeInstance();
            return retVal;
        }
    }

    /// <summary>
    /// Extension methods
    /// </summary>
    public static partial class ExtensionMethods
    {


        /// <summary>
        /// Extension methods should be generated from the "component" element in the same
        /// </summary>
        public static void AddEKGImpressionSection(this StructuredBody me, EKGImpressionSection section)
        {
            me.Component.Add(new Component3(ActRelationshipHasComponent.HasComponent, false, section));
        }

        /// <summary>
        /// Generated to register templates
        /// </summary>
        /// <param name="me"></param>
        public static void RegisterSimpleCDADocumentTemplates(this ClinicalDocumentFormatter me)
        {
            me.RegisterTemplate(typeof(EKGImpressionSection));
        }
    }

}
