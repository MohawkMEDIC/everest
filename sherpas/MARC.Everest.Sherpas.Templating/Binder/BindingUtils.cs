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
using MARC.Everest.Sherpas.Templating.Format;

namespace MARC.Everest.Sherpas.Templating.Binder
{
    /// <summary>
    /// Binding utilities
    /// </summary>
    static class BindingUtils
    {
        /// <summary>
        /// Creates the choice conditions for the specified properties in the classTemplate that have contains relationships
        /// </summary>
        internal static void CreateContainsChoice(Format.PropertyTemplateContainer container)
        {

            foreach (PropertyTemplateContainer containerTemplate in container.Templates)
            {
                int minOccurs = 0,
                    maxOccurs = 0;

                foreach (PropertyTemplateDefinition prop in containerTemplate.Templates.OfType<PropertyTemplateDefinition>())
                {
                    // Contains relationships
                    List<PropertyChoiceTemplateDefinition> choices = new List<PropertyChoiceTemplateDefinition>();

                    foreach (var childPropsContains in prop.Templates.OfType<PropertyTemplateDefinition>().Where(pa => !String.IsNullOrEmpty(pa.Contains)))
                    {
                        // Get min/max occurs
                        if (childPropsContains.MinOccurs != null)
                            minOccurs += int.Parse(childPropsContains.MinOccurs);
                        if (childPropsContains.MaxOccurs != null)
                            maxOccurs += int.Parse(childPropsContains.MaxOccurs);

                        // Find the choices via property
                        var choice = choices.Find(c => c.Tag == prop.Name);
                        if (choice == null)
                        {
                            choice = new PropertyChoiceTemplateDefinition();
                            choice.Tag = prop.Name;
                            choices.Add(choice);
                        }

                        // Now to add this to the option of choices
                        choice.Templates.Add(childPropsContains);
                    }

                    // Clean up choices
                    foreach (var chc in choices)
                    {
                        // ADd to the container template
                        prop.Templates.Add(chc);
                        // Remove the other choices
                        foreach (PropertyTemplateDefinition itm in chc.Templates)
                        {
                            itm.TemplateReference = itm.Contains;
                            itm.Contains = null;
                            prop.Templates.Remove(itm);
                        }
                    }


                    // Adjust min/max occurs
                    if (prop.MinOccurs != null && minOccurs > int.Parse(prop.MinOccurs))
                        prop.MinOccurs = minOccurs.ToString();
                    if (prop.MaxOccurs != null && prop.MaxOccurs != "*" && maxOccurs > int.Parse(prop.MaxOccurs))
                        prop.MaxOccurs = maxOccurs.ToString();
                }
              
            }
        }
    }
}
