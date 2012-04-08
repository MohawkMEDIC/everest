using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Attributes;

namespace MARC.Everest.DataTypes.Interfaces
{

    /// <summary>
    /// Represents a set component that can be used as part of
    /// a DataTypes R1 SXPR or DataTypes R2 QSET
    /// </summary>
    [TypeMap(Name = "SXCM")]
    [TypeMap(Name = "QSET")]
    public interface ISetComponent<T> : IAny
    {
    }
}
