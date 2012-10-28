using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace MARC.Everest.DataTypes.Primitives
{
    /// <summary>
    /// Represents an Object Identifier that uniquely identifies an object
    /// </summary>
    /// <remarks>
    /// <para>OIDs represent unique but predictable identifier for objects. An
    /// OID is in the dotted form 1.3.6.x.x.x. This primitive class exists to
    /// assist developers in the assigning of an OID to an <see cref="MARC.Everest.DataTypes.II"/>
    /// </para>
    /// </remarks>
#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class OID 
    {
        /// <summary>
        /// The string value of the oid
        /// </summary>
        private string oid;

        /// <summary>
        /// Creates a new instance of the OID class
        /// </summary>
        public OID() : base() { }

        /// <summary>
        /// Creates a new instance of the OID class with the specified <paramref name="oid"/>
        /// </summary>
        /// <param name="oid">The OID to use</param>
        /// <exception cref="T:System.FormatException">When <paramref name="oid"/> is not in the proper format</exception>
        public OID(string oid)
            : base()
        {
            Regex oidRegex = new Regex("^(\\d+?\\.){1,}\\d+$");
            if (!oidRegex.IsMatch(oid))
                throw new FormatException(String.Format("The string '{0}' is not a valid OID", oid));
            this.oid = oid;
        }

        /// <summary>
        /// Casts the specified OID instance to a string
        /// </summary>
        public static implicit operator string(OID o)
        {
            return o == null ? null : o.ToString();
        }

        /// <summary>
        /// Casts the specified string to an OID instance
        /// </summary>
        public static implicit operator OID(string s)
        {
            return s == null ? null : new OID(s);
        }

        /// <summary>
        /// Compare this OID to <paramref name="obj"/>
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is OID)
                return (obj as OID).oid.Equals(this.oid);
            return base.Equals(obj);
        }

        /// <summary>
        /// Convert this OID to string representation
        /// </summary>
        public override string ToString()
        {
            return this.oid;
        }
    }
}
