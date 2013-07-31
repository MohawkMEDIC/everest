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
    /// Signifies a class that can be identified
    /// </summary>
    /// <remarks>
    /// <para>GPMR will attach this interface to any RMIM class which is identifiable
    /// by only one identifier. It will not attach this interface to 
    /// classes which permit the use of multiple identifiers
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example will iterate through any instance and will
    /// print out all identifiers contained within the message
    /// <code lang="cs" title="Get all Identified Objects">
    ///     public static void PrintIds(IGraphable g)
    ///     {
    ///         // Can't scan a null object
    ///         if(g == null) return;
    ///         
    ///         Type gType = g.GetType();
    ///         foreach(PropertyInfo pi in gType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
    ///         {
    ///             object value = pi.GetValue(g, null);
    ///             
    ///             // Don't report null objects
    ///             if(value == null)
    ///                 continue;
    ///                 
    ///             IIdentifiable identifiableObject = value as IIdentifiable;
    ///             ISetIdentifiable setIdentifiableObject = value as ISetIdentifiable;
    ///             if(identifiableObject != null)
    ///                 Console.WriteLine("{0} : {1}@{2}", pi.Name, identifiableObject.Id.Root, identifiableObject.Id.Extension);
    ///             else if(setIdentifiableObject != null)
    ///             {
    ///                 foreach(var ii in setIdentifiableObject.Id)
    ///                     Console.WriteLine("{0} : {1}@{2}", pi.Name, ii.Root, ii.Extension);
    ///             }
    ///             // Scan type
    ///             PrintIds(value as IGraphable);
    ///         }
    ///     }
    /// </code>
    /// </example>
    /// <seealso cref="T:MARC.Everest.Interfaces.ISetIdentifiable"/>
    public interface IIdentifiable : IGraphable
    {
        /// <summary>
        /// A globally unique identifier
        /// </summary>
        II Id { get; set; }
    }

    /// <summary>
    /// Signifies a class that can be identified with a set of identifiers
    /// </summary>
    /// <remarks>
    /// <para>GPMR will attach this interface to any RMIM class which is identifiable
    /// by multiple identifiers. It will not attach this interface to 
    /// classes which use only one identifier.
    /// </para>    
    /// </remarks>
    /// <example>
    /// For an example of using this interface see <see cref="T:MARC.Everest.Interfaces.IIdentifier"/>
    /// </example>
    /// <seealso cref="T:MARC.Everest.Interfaces.IIdentifiable"/>
    public interface ISetIdentifiable : IGraphable
    {
        /// <summary>
        /// Gets a set of identifiers for the object
        /// </summary>
        SET<II> Id { get; set; }
    }
}