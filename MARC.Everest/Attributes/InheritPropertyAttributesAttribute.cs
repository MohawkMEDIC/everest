using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARC.Everest.Attributes
{
    /// <summary>
    /// Indicates the formatter should propogate property attributes from the base class
    /// </summary>
    /// <remarks>
    /// The normal behavior of the formatter is to treat any overridden property in a class as a 
    /// new property. This means that the base property attributes are ignored. For example:
    /// <code lang="cs">
    /// <![CDATA[
    /// public class Foo
    /// {
    ///    [Property(Name = "value")]
    ///    public virtual ANY Value { get; set; }
    /// }
    /// 
    /// public class Bar : Foo
    /// {
    ///    [Property(Name = "value", type = typeof(II))]
    ///    public override II Value { get; set; }
    /// }
    /// ]]>
    /// </code>
    /// Would result in only "value" being represented if "II" is populated, thus breaking compatibility with the base. To
    /// overcome this, this attribute should be added to a property where the implementer wishes to have the base <see cref="T:MARC.Everest.Attributes.PropertyAttribute"/>s should be
    /// propogated.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class InheritPropertyAttributesAttribute : Attribute
    {
    }
}
