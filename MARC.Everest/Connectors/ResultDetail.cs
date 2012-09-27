/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * Author: Justin Fyfe
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Connectors;

namespace MARC.Everest.Connectors
{
    /// <summary>
    /// Represents diagnostic details about a validation, formatting or connection
    /// operation.
    /// </summary>
    [Serializable]
    public class ResultDetail : IResultDetail
    {
        // location
        private string m_location = null;

        #region IResultDetail Members

        /// <summary>
        /// Exception
        /// </summary>
        [NonSerialized]
        private Exception exception;

        /// <summary>
        /// Type of result detail
        /// </summary>
        public ResultDetailType Type { get; protected set; }

        /// <summary>
        /// Message of the detail
        /// </summary>
        public virtual string Message { get; private set; }

        /// <summary>
        /// Gets or sets the location of the details
        /// </summary>
        public string Location
        {
            get
            {
                return m_location ;// ?? (exception == null ? null : exception.StackTrace);
            }
            set
            {
                m_location = value;
            }
        }

        /// <summary>
        /// Exception
        /// </summary>
        public Exception Exception { get { return exception; } private set { exception = value; } }

        /// <summary>
        /// Creates a new instance of hte datatype result detail
        /// </summary>
        public ResultDetail(ResultDetailType type, string message, string location, Exception exception) :
            this(type, message, exception)
        {
            this.Location = location;
        }

        /// <summary>
        /// Creates a new instance of the result detail class
        /// </summary>
        public ResultDetail(ResultDetailType type, string message, string location) :
            this(type, message, location, null)
        { }

        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public ResultDetail(ResultDetailType type, string message, Exception exception)
        {
            this.Type = type;
            this.Message = message;
            this.Exception = exception;
        }

        /// <summary>
        /// Result detail
        /// </summary>
        public ResultDetail(string message)
        {
            this.Type = ResultDetailType.Error;
            this.Message = message;
        }

        #endregion
    }

    /// <summary>
    /// Represents result details related to the validation of a message instance
    /// </summary>
    public class ValidationResultDetail : FormalConstraintViolationResultDetail
    {
        /// <summary>
        /// Creates a new instance of hte datatype result detail
        /// </summary>
        public ValidationResultDetail(ResultDetailType type, string message, string location, Exception exception) :
            base(type, message, location, exception) {}

        /// <summary>
        /// Creates a new instance of the result detail class
        /// </summary>
        public ValidationResultDetail(ResultDetailType type, string message, string location) :
            base(type, message, location, null)
        { }

        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public ValidationResultDetail(ResultDetailType type, string message, Exception exception) :
            base(type, message, exception)
        { }

        /// <summary>
        /// Result detail
        /// </summary>
        public ValidationResultDetail(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// This class is used to group validation result details which represent formal constraint violations in the message
    /// </summary>
    public class FormalConstraintViolationResultDetail : ResultDetail
    {
        /// <summary>
        /// Creates a new instance of hte datatype result detail
        /// </summary>
        public FormalConstraintViolationResultDetail(ResultDetailType type, string message, string location, Exception exception) :
            base(type, message, location, exception) { }

        /// <summary>
        /// Creates a new instance of the result detail class
        /// </summary>
        public FormalConstraintViolationResultDetail(ResultDetailType type, string message, string location) :
            base(type, message, location, null)
        { }

        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public FormalConstraintViolationResultDetail(ResultDetailType type, string message, Exception exception) :
            base(type, message, exception)
        { }

        /// <summary>
        /// Result detail
        /// </summary>
        public FormalConstraintViolationResultDetail(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Required element is missing
    /// </summary>
    /// <remarks>
    /// This formal constraint violation indicates that an instance is missing a required element
    /// where the minimum occurs is set to 1 (the conformance of Populated). At minimum a NullFlavor
    /// should be popualted
    /// </remarks>
    [Serializable]
    public class RequiredElementMissingResultDetail : FormalConstraintViolationResultDetail
    {
        /// <summary>
        /// Create a new instance of the required element missing result detail
        /// </summary>
        public RequiredElementMissingResultDetail(ResultDetailType type, string message, string location) :
            base(type, message, location, null) { }

        /// <summary>
        /// Result detail
        /// </summary>
        public RequiredElementMissingResultDetail(string message) : base(message) { }
    }

    /// <summary>
    /// Insufficient repetitions
    /// </summary>
    /// <remarks>
    /// This formal constraint violation indicates that an instnace has not supplied sufficient
    /// repetitions to fulfill the min occurs constraint on the property.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Repitions"), Serializable]
    public class InsufficientRepetionsResultDetail : FormalConstraintViolationResultDetail
    {
        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public InsufficientRepetionsResultDetail(ResultDetailType type, string message, string location) :
            base(type, message, location, null) { }

        /// <summary>
        /// Result detail
        /// </summary>
        public InsufficientRepetionsResultDetail(string message) : base(message) { }
    }

    /// <summary>
    /// Mandatory element is missing
    /// </summary>
    /// <remarks>
    /// This formal constraint violation indicates that an instance has a property set to null (or whose null flavor is set ) when
    /// the conformance for the property is Mandatory
    /// </remarks>
    [Serializable]
    public class MandatoryElementMissingResultDetail : FormalConstraintViolationResultDetail
    {
        
        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public MandatoryElementMissingResultDetail(ResultDetailType type, string message, string location) :
            base(type, message, location) { }

        /// <summary>
        /// Result detail
        /// </summary>
        public MandatoryElementMissingResultDetail(string message) : base(message) {}

    }

    /// <summary>
    /// The requested feature has not been implemented in the Everest Framework
    /// </summary>
    /// <remarks>
    /// This result detail is usually triggered when functionality has been implemented that 
    /// has not been implemented, or is impossible to implement in the Everest Framework
    /// </remarks>
    public class NotImplementedResultDetail : ResultDetail
    {
        /// <summary>
        /// Creates a new instance of the not implemented result detail with the specified <paramref name="message"/>
        /// </summary>
        /// <param name="message">A human readable message the describes the result detail</param>
        public NotImplementedResultDetail(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of the not implemented result detail with the specified <paramref name="type"/>,
        /// <paramref name="message"/> which occurred at <paramref name="location"/> and was triggered by <paramref name="exception"/>
        /// being thrown
        /// </summary>
        /// <param name="type">Indicates the type of result detail (error, warning, informational)</param>
        /// <param name="message">A human readable message that describes the result detail</param>
        /// <param name="location">The location in the stream where the result detail was triggered</param>
        /// <param name="exception">An exception that caused the result detail to be thrown</param>
        public NotImplementedResultDetail(ResultDetailType type, string message, string location, Exception exception)
            : base(type, message, location, exception) { }
        /// <summary>
        /// Creates a new instance of the not implemented result detail with the specified <paramref name="type"/>,
        /// <paramref name="message"/> which occurred at <paramref name="location"/>
        /// </summary>
        /// <param name="type">Indicates the type of result detail (error, warning, informational)</param>
        /// <param name="message">A human readable message that describes the result detail</param>
        /// <param name="location">The locaiton within the stream where the result detail was triggered.</param>
        public NotImplementedResultDetail(ResultDetailType type, string message, string location)
            : base(type, message, location) { }
    }

    /// <summary>
    /// Encountered an element that was not understood
    /// </summary>
    /// <remarks>
    /// This result details occurs during the parsing of an instance whereby the formatter encountered
    /// an element that has no "home" within the object. 
    /// </remarks>
    [Serializable]
    public class NotImplementedElementResultDetail : NotImplementedResultDetail
    {

        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public NotImplementedElementResultDetail(ResultDetailType type, string elementName, string elementNamespace, Exception exception) :
            this(type, elementName, elementNamespace, null, exception) { }

        /// <summary>
        /// Create a new instance of the not implemented element result detail
        /// </summary>
        public NotImplementedElementResultDetail(ResultDetailType type, string elementName, string elementNamespace, string location, Exception exception) :
            base(type, String.Format("Element '{0}#{1}' is not supported and was not interpreted", elementName, elementNamespace), location, exception) { }

        /// <summary>
        /// Result detail
        /// </summary>
        public NotImplementedElementResultDetail(string elementName, string elementNamespace) : this(ResultDetailType.Warning, elementName, elementNamespace, null, null) { }

    }

    /// <summary>
    /// A choice element was used that is not supported
    /// </summary>
    /// <remarks>
    /// This result detail is usually used whenever a property is populated with a valid value (from .NET perspective) however
    /// the RMIM model does not support the choice. This is commonly raised for referneces to System.Object</remarks>
    [Serializable]
    public class NotSupportedChoiceResultDetail : FormalConstraintViolationResultDetail
    {

        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public NotSupportedChoiceResultDetail(ResultDetailType type, string message, Exception exception) :
            base(type, message, exception) { }

        /// <summary>
        /// Create a new instance of the not implemented element result detail
        /// </summary>
        public NotSupportedChoiceResultDetail(ResultDetailType type, string message, string location, Exception exception) :
            base(type, message, location, exception) { }

        /// <summary>
        /// Result detail
        /// </summary>
        public NotSupportedChoiceResultDetail(string message) : base(message) { }

    }

    /// <summary>
    /// The fixed value in the message definition does not match the value supplied
    /// </summary>
    /// <remarks>
    /// This violation occurs during the parsing of an instance whereby a value is supplied in the instance that does not
    /// match the fixed value defined in the .NET type. The formatter may override the supplied value with the fixed value, 
    /// or may use the fixed value
    /// </remarks>
    [Serializable]
    public class FixedValueMisMatchedResultDetail : ValidationResultDetail
    {

        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public FixedValueMisMatchedResultDetail(string suppliedValue, string fixedValue, string location) :
            base(ResultDetailType.Error, String.Format("The supplied value of '{0}' doesn't match the fixed value of '{1}'", suppliedValue, fixedValue), location) 
        {
            this.SuppliedValue = suppliedValue;
        }

        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public FixedValueMisMatchedResultDetail(string suppliedValue, string fixedValue, bool isIgnored, string location) :
            base(isIgnored ? ResultDetailType.Error : ResultDetailType.Warning, String.Format("The supplied value of '{0}' doesn't match the fixed value of '{1}', {2}", suppliedValue, fixedValue, isIgnored ? "the supplied value will be ignored" : "the supplied value has been used in place of the fixed value"), location)
        {
            this.SuppliedValue = suppliedValue;
            this.Overwritten = !isIgnored;
        }

        /// <summary>
        /// When true, indicates that the supplied value was used to overwrite the fixed value
        /// </summary>
        public bool Overwritten { get; private set; }

        /// <summary>
        /// Gets or sets the supplied value on the wire
        /// </summary>
        public string SuppliedValue { get; set; }

    }

    /// <summary>
    /// An issue was detected with the codified concepts within the message
    /// </summary>
    /// <remarks>
    /// This issue is raised whenever codified data is encountered whereby the supplied value 
    /// cannot be used in the context, or if the value is unknown / invalid.
    /// </remarks>
    [Serializable]
    public class VocabularyIssueResultDetail : ValidationResultDetail
    {

        /// <summary>
        /// Create a new instance of the datatype result detail
        /// </summary>
        public VocabularyIssueResultDetail(ResultDetailType type, string message, Exception exception) :
            base(type, message, exception) { }

        /// <summary>
        /// Create a new instance of the vocabulary issue result detail
        /// </summary>
        public VocabularyIssueResultDetail(ResultDetailType type, string message, string location, Exception exception) :
            base(type, message, location, exception) { }

        /// <summary>
        /// Result detail
        /// </summary>
        public VocabularyIssueResultDetail(string message) : base(message) { }

    }
    
    /// <summary>
    /// Identifies that basic validation of a datatype instance has failed
    /// </summary>
    [Serializable]
    public class DatatypeValidationResultDetail : ValidationResultDetail
    {

        /// <summary>
        /// Gets the name of the datatype where validation failed
        /// </summary>
        public string DatatypeName { get; private set; }

        /// <summary>
        /// Gets the error message
        /// </summary>
        public override string Message
        {
            get
            {
                if (String.IsNullOrEmpty(base.Message))
                    return string.Format("Data type '{0}' failed basic validation, please refer to the developer's guide for more detail", this.DatatypeName);
                else
                    return string.Format("Data type '{0}' failed basic validation, the violation was : {1}", this.DatatypeName, base.Message);
            }
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public DatatypeValidationResultDetail(string datatype) : base(null) {
            this.DatatypeName = datatype;
        }
        /// <summary>
        /// Creates a new instance of the datatype validation result detail class with the specified parameters
        /// </summary>
        /// <param name="type">The type of result detail</param>
        /// <param name="datatypeName">The name of the datatype that is not supported</param>
        /// <param name="location">The location within the instance that that is not supported</param>
        public DatatypeValidationResultDetail(ResultDetailType type, string datatypeName, string location) : 
            base(type, null, location)
        {
            this.DatatypeName = datatypeName;
        }
        /// <summary>
        /// Creates a new instance of the datatype validation result detail class with the specified parameters
        /// </summary>
        /// <param name="type">The type of result detail</param>
        /// <param name="datatypeName">The name of the datatype that is not supported</param>
        /// <param name="location">The location within the instance that that is not supported</param>
        public DatatypeValidationResultDetail(ResultDetailType type, string datatypeName, string message, string location) :
            base(type, message, location)
        {
            this.DatatypeName = datatypeName;
        }
        
    }

    /// <summary>
    /// Identifies that flavor validation of a datatype instance has failed
    /// </summary>
    [Serializable]
    public class DatatypeFlavorValidationResultDetail : DatatypeValidationResultDetail
    {

        /// <summary>
        /// Gets the name of the datatype where validation failed
        /// </summary>
        public string FlavorName { get; private set; }

        /// <summary>
        /// Gets the message
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format("Datatype '{0}' failed validation criteria for flavor '{1}'. Please refer to development guide for validation rules", this.DatatypeName, this.FlavorName);
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DatatypeFlavorValidationResultDetail(string datatypeName, string flavorName)
            : base(datatypeName)
        {
            this.FlavorName = flavorName;
        }
        /// <summary>
        /// Creates a new instance of the datatype validation result detail class with the specified parameters
        /// </summary>
        /// <param name="type">The type of result detail</param>
        /// <param name="datatypeName">The name of the datatype that is not supported</param>
        /// <param name="location">The location within the instance that that is not supported</param>
        public DatatypeFlavorValidationResultDetail(ResultDetailType type, string datatypeName, string flavorName, string location) :
            base(type, datatypeName, location)
        {
            this.FlavorName = flavorName;
        }

    }

    /// <summary>
    /// Identifies that the value in a data type has been propagated as the traversal
    /// on which the original value was set is not rendered
    /// </summary>
    /// <remarks>
    /// <para>This result detail is added whenever a value (such as NullFlavor, Flavor, etc...)
    /// is set on a data type that is "transparent" or not rendered (such as GTS.Hull). The result detail
    /// is used to record the original and destination and value of propagation</para>
    /// </remarks>
    [Serializable]
    public class PropertyValuePropagatedResultDetail : ResultDetail
    {
        /// <summary>
        /// Gets the value that was propagated
        /// </summary>
        public object ValuePropagated { get; private set; }
        /// <summary>
        /// Gets the original path that the value was originally set on
        /// </summary>
        public string OriginalPath { get; private set;  }
        /// <summary>
        /// Gets the destination path that the value has been propagated to
        /// </summary>
        public string DestinationPath { get; set; }

        #region IResultDetail Members

        /// <summary>
        /// Gets the message of the result detail
        /// </summary>
        public override string Message
        {
            get
            {
                return String.Format("Value '{0}' set on '{1}' has been propagated to '{2}'",
                    Util.ToWireFormat(this.ValuePropagated), OriginalPath, DestinationPath);
            }
        }

        #endregion

        /// <summary>
        /// Creates a new instance of the property propagated result detail
        /// </summary>
        public PropertyValuePropagatedResultDetail(ResultDetailType type, String originalPath, String destinationPath, Object valuePropagated, string location) : base(type, null, location, null) {
            this.DestinationPath = destinationPath;
            this.OriginalPath = originalPath;
            this.ValuePropagated = valuePropagated;
        }


    }

    /// <summary>
    /// Identifies that a property populated within the datatype can't rendered
    /// to the output stream by the current datatype formatter
    /// </summary>
    /// <remarks>
    /// <para>
    /// Because the Everest data type library is a combination of R1 and R2 concepts 
    /// (to support write once render both) some concepts cannot be rendered within 
    /// either instance of a message. 
    /// </para>
    /// <para>This result detail signals that a value populated in memory may
    /// not have been rendered on the wire</para>
    /// <para>This abstract class must be extended by datatype formatters</para>
    /// </remarks>
    [Serializable]
    public abstract class UnsupportedDatatypePropertyResultDetail : NotImplementedElementResultDetail
    {
        
        #region IResultDetail Members

        /// <summary>
        /// Gets the message for the detail
        /// </summary>
        public override string Message
        {
            get { return String.Format("Property '{0}' in '{1}' is not supported by this datatype formatter", PropertyName, DatatypeName); }
        }

        #endregion

        /// <summary>
        /// Gets or sets the name of the property that isn't supported
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets or sets the name of the datatype that contains the unsupported property
        /// </summary>
        public string DatatypeName { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        protected UnsupportedDatatypePropertyResultDetail() : base(null, null) { }
        /// <summary>
        /// Creates a new instance of the unsupported data type result detail
        /// </summary>
        /// <param name="type">The type of result detail</param>
        /// <param name="propertyName">The name of the property that is not supported</param>
        /// <param name="datatypeName">The name of the datatype that is not supported</param>
        /// <param name="location">The location within the instance that that is not supported</param>
        protected UnsupportedDatatypePropertyResultDetail(ResultDetailType type, string propertyName, string datatypeName, string location) :
            base(type, null, null, null)
        {
            this.DatatypeName = datatypeName;
            this.PropertyName = propertyName;
        }
    }
    
}