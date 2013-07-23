using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;
using System.Globalization;

namespace MARC.Everest.DataTypes.Primitives
{
    /// <summary>
    /// A code value is used by all of the coded value types.
    /// </summary>
    /// <remarks>
    /// <para>The CodeValue class permits the usage of CWE constrained
    /// properties within instances. The CodeValue is bound to a strongly
    /// typed enumeration, however it permits the use of "alternate" codes.</para>
    /// <para>When an alternate code is specified, the IsAlternate method
    /// will return true so long as the supplied value does not fall
    /// within the range of <typeparam name="T"/></para>
    /// </remarks>
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class CodeValue<T> : IEquatable<CodeValue<T>>
        
    {

        /// <summary>
        /// Creates a new instance of the code value class
        /// </summary>
        public CodeValue() : base() { }

        /// <summary>
        /// Creates a new instance of the code value class
        /// </summary>
        public CodeValue(T value) : base() { this.valueSetValue = value;  }

        /// <summary>
        /// The value set value of this string
        /// </summary>
        private T valueSetValue;

        /// <summary>
        /// The string value of the code (if the value set value is not present)
        /// </summary>
        private string alternateCode;

        ///// <summary>
        ///// Casts this code value to a string
        ///// </summary>
        //public static implicit operator string(CodeValue<T> cv)
        //{
        //    if(typeof(T) == typeof(System.String))
        //        return cv.valueSetValue == null ? cv.valueSetValue.ToString() : cv.stringValue; // safe cast
        //    else
        //        return cv.stringValue;
        //}

        /// <summary>
        /// Cast the specified code value of T to T
        /// </summary>
        public static implicit operator T(CodeValue<T> cv)
        {
            return cv == null ? default(T) : cv.valueSetValue;
        }

        /// <summary>
        /// Cast from a concrete T value to a code value of R
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator CodeValue<T>(T value)
        {
            if (value == null)
                return null;
            return new CodeValue<T>(value);
        }

        /// <summary>
        /// Equals operation
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CodeValue<T>)
                return this.valueSetValue.Equals((obj as CodeValue<T>).valueSetValue);
            else if (obj is T)
                return this.valueSetValue.Equals(obj);
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns false if the codevalue is not strongly typed
        /// </summary>
        public bool IsAlternateCodeSpecified
        {
            get { 
                return !String.IsNullOrEmpty(alternateCode); 
            }
        }

        /// <summary>
        /// Cast a string <paramref name="value"/> into a code value of T
        /// </summary>
        //public static implicit operator CodeValue<T>(string value)
        //{
        //    return CodeValue<T>.Parse((CodeValue<String>)value);
        //}


        /// <summary>
        /// Parse attempt to parse the specified string codeValue, into a CodeValue&lt;T>
        /// </summary>
        public static CodeValue<T> Parse(CodeValue<string> codeValue)
        {
            // No code value
            if (codeValue == null) return null;

            CodeValue<T> retVal = new CodeValue<T>();
            try // to parse into the enum
            {
                Object tryVal = null;
                if(!Util.TryFromWireFormat(codeValue.valueSetValue, typeof(T), out tryVal, new List<IResultDetail>()))
                    retVal.alternateCode = codeValue.valueSetValue ?? codeValue.alternateCode;
                else
                    retVal.valueSetValue = (T)tryVal;
            }
            catch (VocabularyException)
            {
                retVal.alternateCode = codeValue.valueSetValue ?? codeValue.alternateCode;
            }
            return retVal;
        }

        /// <summary>
        /// Represent this object as a string
        /// </summary>
        public override string ToString()
        {
            return Util.ToWireFormat(alternateCode ?? String.Format(CultureInfo.InvariantCulture, "{0}", valueSetValue));
        }

        #region IEquatable<CodeValue<T>> Members

        

        /// <summary>
        /// Determine if this code value equals another code value
        /// </summary>
        public bool Equals(CodeValue<T> other)
        {
            if (other != null)
                return other.alternateCode == this.alternateCode &&
                    other.valueSetValue.Equals(this.valueSetValue);
            return false;
        }

        #endregion
    }
}
