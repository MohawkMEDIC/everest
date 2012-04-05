using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Interfaces
{
    /// <summary>
    /// This namespace contains interfaces that are implemented by RMIM classes
    /// </summary>
    /// <remarks>
    /// <para>These interfaces are implemented by RMIM classes and assigned by GPMR. The 
    /// purpose of the interfaces are to facilitate general formatting and parsing of 
    /// instances.</para>
    /// <para>Perhaps the most useful application of these interfaces are on the processing
    /// of message results</para>
    /// </remarks>
    /// <example>
    /// <code lang="cs" title="Parsing an Interaction">
    ///    <![CDATA[
    /// IGraphable instance = formatter.ParseObject(File.OpenRead(@"C:\temp.xml"));
    /// 
    /// // We can get the identifier
    /// if(instance is IIdentifiable)
    ///     Console.WriteLine((instance as IIdentifiable).Id);
    /// 
    /// // Or we can use the IInteraction interface to get interaction details
    /// IInteraction interaction = instance as IInteraction;
    /// if(interaction != null)
    ///     Console.WriteLine(interaction.InteractionId.Extension);
    ///    ]]>
    /// </code>
    /// </example>
    internal class NamespaceDoc
    {
    }
}
