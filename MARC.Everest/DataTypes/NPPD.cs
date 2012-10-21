using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.Attributes;
using System.Collections;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Interfaces;

namespace MARC.Everest.DataTypes
{
    /// <summary>
    /// Non-Parametric Probability Distribution
    /// </summary>
    /// <remarks>
    /// The NPPD data type represents a set of probabilities to form a histogram. All 
    /// of the elements in the set of <see cref="T:UVP{T}"/> are considered alternative
    /// values.
    /// <para>
    /// Typcially, NPPD is used where the sum of the probabilities of the <see cref="P:Items"/>
    /// should be less than or equal to one. This means that only one value for <typeparam name="T"/> may be true. 
    /// </para>
    /// </remarks>
    [Structure(Name = "NPPD", StructureType = StructureAttribute.StructureAttributeType.DataType)]
    [Serializable]
    public class NPPD<T> : SET<UVP<T>>, IEnumerable<UVP<T>>, IColl
        where T: IAny
    {

        /// <summary>
        /// Creates a new instance of the non-parametric probability distribution
        /// </summary>
        public NPPD() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the non-parametric probability distribution 
        /// with the specified <paramref name="items"/>
        /// </summary>
        /// <param name="items">An enumerable source of items from which to construct the inital set of <see cref="P:Items"/></param>
        public NPPD(IEnumerable items) : base(items)
        {
        }

        #region IEnumerable<T> Members

        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<UVP<T>> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Get the enumerator
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Validate this instance of NPPD
        /// </summary>
        public override bool Validate()
        {
            return (this.NullFlavor != null) ^ (this.Items.Count > 0);
        }

        /// <summary>
        /// Determines semantic equality between this instance of NPPD and <paramref name="other"/>
        /// </summary>
        public override BL SemanticEquals(MARC.Everest.DataTypes.Interfaces.IAny other)
        {
            if (other == null)
                return null;
            else if (this.IsNull && other.IsNull)
                return new BL() { NullFlavor = NullFlavorUtil.GetCommonParent(this.NullFlavor, other.NullFlavor) };
            else if (this.IsNull ^ other.IsNull)
                return new BL() { NullFlavor = DataTypes.NullFlavor.NotApplicable };
            else if (!other.GetType().IsGenericType ||
                other.GetType().GetGenericTypeDefinition().Equals(typeof(NPPD<>)))
                return false;

            // Items must be in both sets
            int otherNppdCount = 0;
            bool isEqual = true;
            foreach (object itm in (other as IEnumerable))
            {
                isEqual &= this.Items.Contains(itm as UVP<T>);
                otherNppdCount++;
            }
            return isEqual && otherNppdCount.Equals(this.Items.Count);
        }

        /// <summary>
        /// Create a set of T from Array o (shallow copy)
        /// </summary>
        /// <remarks>Usually called from the formatter</remarks>
        /// <param name="o">The array of objects to add</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
        internal static NPPD<T> Parse(LIST<IGraphable> o)
        {
            NPPD<T> retVal = new NPPD<T>(o);
            retVal.NullFlavor = o.NullFlavor;
            retVal.ControlActExt = o.ControlActExt;
            retVal.ControlActRoot = o.ControlActRoot;
            retVal.Flavor = o.Flavor;
            retVal.UpdateMode = o.UpdateMode;
            retVal.ValidTimeHigh = o.ValidTimeHigh;
            retVal.ValidTimeLow = o.ValidTimeLow;
            return retVal;
        }

    }
}
