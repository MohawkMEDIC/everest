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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.DataTypes;

namespace MARC.Everest.Interfaces
{
    /// <summary>
    /// Identifies a type as being an interaction
    /// </summary>
    /// <remarks>
    /// <para>This interface is used by GPMR to denote RMIM classes which contain
    /// all of the "core" information that an interaction would carry. Note that the
    /// presence of this interface does not mean the class is an interaction, rather
    /// it may be the base class for an interaction. For a definitive signal that 
    /// a class is an interaction, the <see cref="T:MARC.Everest.Attributes.StructureAttribute"/>
    /// or <see cref="T:MARC.Everest.Attributes.InteractionAttribute"/> attribute should be used.
    /// </para>
    /// <para>
    /// This interface is usually attached to the Message class that represents a transport wrapper
    /// which is used by an interaction to "bind" together a transport, control act and payload.
    /// </para>
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
    public interface IInteraction : IIdentifiable
    {

        /// <summary>
        /// Indicates the time this particular message instance was constructed.
        /// </summary>
        TS CreationTime { get; set; }

        /// <summary>
        /// Indicates the version of the messaging standard being referenced.
        /// </summary>
        ICodedSimple VersionCode { get; }

        /// <summary>
        /// Indicates the id of the interaction being executed
        /// </summary>
        II InteractionId { get; set; }

        /// <summary>
        /// Processing Mode
        /// </summary>
        ICodedSimple ProcessingModeCode { get; }

        /// <summary>
        /// Gets or sets the control act event
        /// </summary>
        Object ControlAct { get; set; }
    }
}