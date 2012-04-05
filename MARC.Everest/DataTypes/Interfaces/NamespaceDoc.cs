using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes.Interfaces
{
    /// <summary>
    /// The namespace in which data type interfaces reside 
    /// </summary>
    /// <remarks>
    /// <para>The interfaces within the MARC.Everest.Interfaces namespace
    /// are used by the data types to establish common functionality between
    /// data types. These interfaces can then be used by formatters to 
    /// and populate instances of classes easily.</para>
    /// <para>Also, these interfaces can be used by developers to use
    /// common functionality between like classes. For example, the 
    /// <see cref="T:MARC.Everest.DataTypes.LIST{T}"/> and <see cref="T:MARC.Everest.DataTypes.BAG{T}"/>
    /// are both collections, and thus, both implement the <see cref="T:MARC.Everest.Interfaces.IColl{T}"/></para>
    /// </remarks>
    internal class NamespaceDoc
    {
    }
}
