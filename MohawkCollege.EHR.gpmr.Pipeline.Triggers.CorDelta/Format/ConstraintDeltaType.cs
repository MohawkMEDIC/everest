using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Format
{
    /// <summary>
    /// Identifies the type of constraints
    /// </summary>
    [XmlType("ConstraintDeltaType", Namespace = "urn:infoway.ca/deltaSet")]
    public enum ConstraintDeltaType
    {
        /// <summary>
        /// Constraint is on an annotation
        /// </summary>
        [XmlEnum("ANNOTATION")]
        Annotation,
        /// <summary>
        /// Constraint changes the business name
        /// </summary>
        [XmlEnum("CHANGE_BUSINESS_NAME")]
        BusinessName,
        /// <summary>
        /// Constraint changes cardinality
        /// </summary>
        [XmlEnum("CHANGE_CARDINALITY")]
        Cardinality,
        /// <summary>
        /// Constraint changes conformance
        /// </summary>
        [XmlEnum("CHANGE_CONFORMANCE")]
        Conformance,
        /// <summary>
        /// Constraint changes the default value
        /// </summary>
        [XmlEnum("CHANGE_DEFAULT_VALUE")]
        DefaultValue,
        /// <summary>
        /// Constraint changes the data type
        /// </summary>
        [XmlEnum("CHANGE_DATATYPE")]
        Datatype,
        /// <summary>
        /// Constraint changes the fixed value
        /// </summary>
        [XmlEnum("CHANGE_FIXED")]
        Fixed,
        /// <summary>
        /// Constraint changes the maximum length
        /// </summary>
        [XmlEnum("CHANGE_LENGTH")]
        Length,
        /// <summary>
        /// Constraint changes the update mode
        /// </summary>
        [XmlEnum("CHANGE_UPDATEMODE_VALUES")]
        UpdateModeValue,
        /// <summary>
        /// Constraint changes the default update mode
        /// </summary>
        [XmlEnum("CHANGE_UPDATEMODE_DEFAULT")]
        UpdateModeDefault,
        /// <summary>
        /// Constraint changes the vocabulary
        /// </summary>
        [XmlEnum("CHANGE_VOCABULARY_BINDING")]
        VocabularyBinding,
        /// <summary>
        /// Constraint changes the vocabulary strength
        /// </summary>
        [XmlEnum("CHANGE_VOCABULARY_STRENGTH")]
        VocabularyStrength,
        /// <summary>
        /// Constraint removes the the item
        /// </summary>
        [XmlEnum("REMOVE")]
        Remove,
        /// <summary>
        /// Not used
        /// </summary>
        [XmlEnum("REMOVE_ANNOTATION")]
        RemoveAnnotation,
        /// <summary>
        /// Constraint substitutes a different CMET
        /// </summary>
        [XmlEnum("SUBSTITUTE_CMET")]
        SubstituteCmet
    }
}
