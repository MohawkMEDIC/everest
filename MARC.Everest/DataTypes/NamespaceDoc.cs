using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// The namespace in which data types exist
    /// </summary>
    /// <remarks>
    /// <para>Data Types are a major portion of any HL7v3 system. Data types
    /// represent the foundational classes from which more complex classes can 
    /// be constructed.</para>
    /// <para>The MARC-HI Everest Framework contains a series of data types classes
    /// that are a hand written combination of R1 and R2 HL7 data types structures. They
    /// contain methods that aim to assist developers in the creation of their instances
    /// and all attempts have been made to sheild developers from the complexities of 
    /// the data types.</para>
    /// <example>
    /// <code lang="cs" title="Sheilded Data Types">
    /// // Note how a developer is sheilded from using data types directly
    /// REPC_IN000076CA instance = new REPC_IN000076CA();
    /// instance.Id = Guid.NewGuid();
    /// instance.CreationTime = DateTime.Now;
    /// instance.ProcessingMode = ProcessingID.Immediate;
    /// </code>
    /// </example>
    /// <para>The data types themselves may be formatted using one or more
    /// data type formatters. This makes it possible to use the same data types
    /// classes in instances that use R1 or R2 data types.</para>
    /// </remarks>
    internal class NamespaceDoc
    {
    }
}
