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
