/* 
 * Copyright 2008-2011 Mohawk College of Applied Arts and Technology
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
 * Date: 06-29-2011
 */
package ca.marc.everest.datatypes.generic;

import java.util.Collection;
import java.util.List;

import ca.marc.everest.datatypes.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IEncapsulatedData;
import ca.marc.everest.datatypes.interfaces.IInterval;
import ca.marc.everest.datatypes.interfaces.IOrderedDataType;
import ca.marc.everest.datatypes.interfaces.IOriginalText;
import ca.marc.everest.datatypes.interfaces.IPqTranslatable;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;
import ca.marc.everest.annotations.*;

/**
 * A set of consecutive values of an ordered base datatype. Any ordered type can be the basis of an IVL
 */
@Structure(name = "IVL", structureType = StructureType.DATATYPE)
public class IVL<T extends IAny> extends SXCM<T> implements IInterval<T>, IPqTranslatable<IVL<T>>, IOriginalText  {

	// backing field for low inclusive attribute
	private Boolean m_lowInclusive = null;
	// backing field for high inclusive
	private Boolean m_highInclusive = null;
	// backing field for the width
	private PQ m_width;
	// backing field for the lower bound of the set
	private T m_low;
	// backing field for the upper bound of the set
	private T m_high;
	// backing field for original text
	private ED m_originalText;
	
	/**
	 * Creates a new instance of the interval type
	 */
	public IVL() { super(); }
	/**
	 * Creates a new instance of the interval type with the specified value
	 * @param value The value of the interval type
	 */
	public IVL(T value) { super(value); }
	/**
	 * Creates a new instance of the interval type with the specified upper and lower bounds
	 * @param low The initial value of the lower bound of the interval
	 * @param high The initial value of the upper bound of the interval
	 */
	public IVL(T low, T high) { this.m_low = low; this.m_high = high; }
	/**
	 * Creates a new instance of the interval type with the specified upper and lower bounds with
	 * the specified set operator applied 
	 * @param low The initial value of the lower bound of the interval
	 * @param high The value of the upper bound of the interval
	 * @param operator The manner in which this IVL instance participates in a containing SET expression
	 */
	public IVL(T low, T high, SetOperator operator) { this(low, high); this.setOperator(operator); }
	
	/**
	 * Gets the original text indicating where the interval was derived
	 */
	@Property(name = "originalText", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public ED getOriginalText() { return this.m_originalText; }
	/**
	 * Sets the original text indicating where the interval was derived
	 */
	public void setOriginalText(ED value) { this.m_originalText = value; }
	/**
	 * Sets the original text indicating where the interval was derived
	 */
	@Override
	public void setOriginalText(IEncapsulatedData value) { this.m_originalText = (ED)value; }
	/**
	 * Gets the lower bound of the interval
	 */
	@Property(name = "low", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public T getLow() { return this.m_low; }
	/**
	 * Sets the lower bound of the interval
	 */
	@Override
	public void setLow(T value) { this.m_low = value; }
	/**
	 * Gets a flag indicating if the lower bound of the interval is inclusive
	 */
	@Property(name = "lowClosed", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public Boolean getLowInclusive() {  return this.m_lowInclusive; }
	/**
	 * Sets a flag indicating if the lower bound of the interval is inclusive
	 */
	@Override
	public void setLowInclusive(Boolean value) { this.m_lowInclusive = value; }
	/**
	 * Get the upper bound of the interval
	 */
	@Property(name = "high", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public T getHigh() { return this.m_high; }
	/**
	 * Set the upper bound of the interval
	 */
	@Override
	public void setHigh(T value) { this.m_high = value;  }
	/**
	 * Gets a flag indicating if the upper bound of the interval is inclusive
	 */
	@Property(name = "highClosed", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public Boolean getHighInclusive() { return this.m_highInclusive; }
	/**
	 * Sets a flag indicating if the upper bound of the interval is inclusive
	 */
	@Override
	public void setHighInclusive(Boolean value) { this.m_highInclusive = value; }

	/**
	 * Gets the width of the interval
	 */
	@Property(name = "width", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public PQ getWidth() { return this.m_width; }
	/**
	 * Sets the width of the interval
	 */
	public void setWidth(PQ value) { this.m_width = value; }
	
	/**
	 * Determines if the interval is a valid width
	 */
	@Flavor(name = "IVL.WIDTH")
	public static <T extends IAny> boolean isValidWidthFlavor(IVL<T> ivl)
	{
		// IVL must have a Width and either a High / Low
        return (ivl.getLow() == null && ivl.getHigh() == null && ivl.getLowInclusive() == null && ivl.getHighInclusive() == null && ivl.getWidth() != null) ^ (ivl.getNullFlavor() != null);
	}

	/**
	 * Determines if the interval is a high
	 */
	@Flavor(name = "IVL.HIGH")
	public static <T extends IAny> boolean isValidHighFlavor(IVL<T> ivl)
	{
		 return (ivl.getLow() == null && ivl.getHigh() != null && ivl.getLowInclusive() == null && BL.TRUE.equals(ivl.getHighInclusive()) && ivl.getWidth() == null) ^ (ivl.getNullFlavor() != null);
	}
	
	/**
	 * Determine if the interval is a low flavor
	 */
	@Flavor(name = "IVL.LOW")
	public static <T extends IAny> boolean isValidLowFlavor(IVL<T> ivl)
	{
		return (ivl.getLow() != null && ivl.getHigh() == null && BL.TRUE.equals(ivl.getLowInclusive()) && ivl.getHighInclusive() == null && ivl.getWidth() == null) ^ (ivl.getNullFlavor() != null);
	}
	
	/**
	 * Identifies if this instance of an IVL is valid
	 * <p>
	 * Either the IVL is assigned a null flavor, or one of value, low, high or width is set and
     * when lowIncluded is set, low is set and
     * when highIncluded is set, high is set
     * </p>
	 */
	@Override
	public boolean validate()
	{
		return (this.getNullFlavor() != null) ^ (this.m_low != null || this.m_width != null || this.m_high != null || this.getValue() != null) &&
			(this.m_lowInclusive == null && this.m_low != null || this.m_lowInclusive != null) &&
			(this.m_highInclusive == null && this.m_high != null || this.m_highInclusive != null) &&
			super.validate();
		
	}
	/**
	 * Translate the interval by the specified translation
	 */
	@SuppressWarnings("unchecked")
	@Override
	public IVL<T> translate(PQ translation) {
		if (this.isNull())
		{
            IVL<T> r = new IVL<T>();
            r.setNullFlavor(this.getNullFlavor());
		}
		
        // Low and high references
        T low = this.getLow(),
            high = this.getHigh(),
            value = this.getValue();

        // Is a width specified? If so, then we need a 
        // valid low/high to translate
        if (low == null || high == null)
        {
            // Determine if we can calculate bounds
            if (this.getWidth() != null && (low instanceof IPqTranslatable<?> || high instanceof IPqTranslatable<?>))
            {
                if (low != null)
                    high = ((IPqTranslatable<T>)low).translate(this.getWidth());
                else if (high != null)
                    low = ((IPqTranslatable<T>)high).translate(this.getWidth().negate());
            }
            else if(value == null)
                throw new UnsupportedOperationException("Cannot determine set bounds");
        }

        // Translate
        IPqTranslatable<T> pqLow = (IPqTranslatable<T>)low,
            pqHigh = (IPqTranslatable<T>)high,
            pqValue = (IPqTranslatable<T>)value;
        low = low == null ? null : pqLow.translate(translation);
        high = pqHigh == null ? null : pqHigh.translate(translation);
        value = pqValue == null ? null : pqValue.translate(translation);

        IVL<T> retVal = new IVL<T>(low, high);
        retVal.setValue(value);
        retVal.setLowInclusive(this.getLowInclusive());
        retVal.setHighInclusive(this.getHighInclusive());
        return retVal;
    }
	/**
	 * Validate this instance of IVL using the validation rules. 
	 * <p>An IVL is valid if:</p>
	 * <ul>
	 * 	<li>When NullFlavor is specified Low, Width, High and Value are not and, </li>
	 *  <li>When LowInclusive is specified, Low is also specified and,</li>
	 *  <li>When HighInclusive is specified, High is also specified</li>
	 * </ul>
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		Collection<IResultDetail> retVal = (List<IResultDetail>)super.validateEx();

        // Validation
        if (this.isNull() && (this.getLow() != null || this.getHigh() != null || this.getWidth() != null || this.getValue() != null))
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "IVL", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
        if (this.getLowInclusive() != null && this.getLow() == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "IVL", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "LowClosed", "Low"), null));
        if (this.getHighInclusive() != null && this.getHigh() == null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "IVL", String.format(EverestValidationMessages.MSG_DEPENDENT_VALUE_MISSING, "HighClosed", "High"), null));

        return retVal;
	}
	
	/**
	 * To bound interval
	 */
	@SuppressWarnings("unchecked")
	public IVL<T> toBoundIvl()
	{
		if (this.getLow() != null && this.getHigh() != null)
		{
            IVL<T> retVal = new IVL<T>((T)this.getLow().shallowCopy(), (T)this.getHigh().shallowCopy());
            retVal.setLowInclusive(this.getLowInclusive());
            retVal.setHighInclusive(this.getHighInclusive());
            return retVal;
		}
        else if (this.getLow() != null && this.getLow() instanceof IPqTranslatable<?>)
        {
            IVL<T> retVal = new IVL<T>((T)this.getLow().shallowCopy(), ((IPqTranslatable<T>)this.getLow()).translate(this.getWidth()));
            retVal.setLowInclusive(this.getLowInclusive());
            return retVal;
        }
        else if (this.getHigh() != null && this.getHigh() instanceof IPqTranslatable<?>)
        {
            IVL<T> retVal = new IVL<T>(((IPqTranslatable<T>)this.getHigh()).translate(this.getWidth().negate()), (T)this.getHigh().shallowCopy());
            retVal.setHighInclusive(this.getHighInclusive());
            return retVal;
        }
        else
            throw new UnsupportedOperationException("Low or High is calculable from given data");
		
	}
	
	/**
	 * @see ca.marc.everest.datatypes.ANY#semanticEquals(ca.marc.everest.datatypes.interfaces.IAny)
	 */
	@SuppressWarnings("unchecked")
	@Override
	public BL semanticEquals(IAny other) {
		BL semanticEquals = BL.FALSE;

        // Based on set, first, is the other a DSET? 
        if (other instanceof SET<?>)
            return semanticEqualsInternal((SET<T>)other);
        else if (other instanceof IVL<?>)
        {
            IVL<T> ivlOther = (IVL<T>)other;
            // Parameters to semantic equality
            boolean otherHighInfinite = (ivlOther.getHigh() == null || NullFlavor.PositiveInfinity.equals(ivlOther.getHigh().getNullFlavor().getCode())),
                thisHighInfinite = (this.getHigh() == null || NullFlavor.PositiveInfinity.equals(this.getHigh().getNullFlavor().getCode())),
                otherLowInifinite = (ivlOther.getLow() == null || NullFlavor.NegativeInfinity.equals(ivlOther.getLow().getNullFlavor().getCode())),
                thisLowInfinite = (this.getLow() == null || NullFlavor.NegativeInfinity.equals(this.getLow().getNullFlavor().getCode())),
                isOtherUnbound = (ivlOther.getHigh() == null || ivlOther.getHigh().isNull()) && !otherHighInfinite ||
                    (ivlOther.getLow() == null || ivlOther.getLow().isNull()) && !otherLowInifinite,
                isThisUnbound = (this.getHigh() == null || this.getHigh().isNull()) && !thisHighInfinite ||
                    (this.getLow() == null || this.getLow().isNull()) && !thisLowInfinite;

            // Case 1 : Both are bound
            if(!isOtherUnbound && !isThisUnbound)
                return BL.fromBoolean(((otherHighInfinite && thisHighInfinite) || this.getHigh().semanticEquals(ivlOther.getHigh()).toBoolean()) &&
                    ((otherLowInifinite && thisLowInfinite) || this.getLow().semanticEquals(ivlOther.getLow()).toBoolean()));
            return BL.FALSE; // all others are not equal
            
        }
        return BL.FALSE;
	}

	/**
	 * Creates a set from the IVL
	 * @return
	 */
	public SET<T> toSet()
	{
		IVL<T> lh = this.toBoundIvl();
        if (lh.getLow() instanceof IOrderedDataType<?> && lh.getLow() instanceof Comparable<?>)
        {
            SET<T> retVal = new SET<T>();
            IOrderedDataType<T> current = (IOrderedDataType<T>)lh.getLow();
            while (((Comparable<T>)current).compareTo(lh.getHigh()) <= (lh.getHighInclusive().equals(true) ? 0 : -1))
            {
                retVal.add((T)current);
                current = (IOrderedDataType<T>)current.nextValue();
            }
            return retVal;
        }
        else
            throw new UnsupportedOperationException("Cannot enumerate generic type to construct the resultant set");
	}
	
	/**
	 * Determine if this interval, represented as a set will semantically equalivent to the set
	 */
	private BL semanticEqualsInternal(SET<T> set)
	{
		return this.toSet().semanticEquals(set);
	}
	
	/**
	 * Determine if the interval described by this IVL contains the specified member
	 */
	@SuppressWarnings("unchecked")
	public boolean contains(T member)
	{
        if (!(member instanceof Comparable<?>))
            throw new IllegalArgumentException("member must implement IComparable");

        // Low and high references
        T low = this.getLow(),
            high = this.getHigh();
        
        // Is a width specified?
        if (low == null || high == null)
        {
            // Determine if we can calculate bounds
            if (this.getWidth() != null && member instanceof IPqTranslatable<?>)
            {
                if (low != null)
                    high = ((IPqTranslatable<T>)low).translate(this.getWidth());
                else if (high != null)
                    low = ((IPqTranslatable<T>)high).translate(this.getWidth().negate());
            }
            else
                throw new UnsupportedOperationException("Cannot determine set bounds");
        }

        // Comparable instance
        Comparable<T> membComp = (Comparable<T>)member;

        // Determine if the member is within the bounds
        int lb = this.getLowInclusive() == true ? 0 : 1,
            hb = this.getHighInclusive() == true ? 0 : -1;

        return membComp.compareTo(low) >= lb && membComp.compareTo(high) <= hb;

	}
	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		if (this.getValue() != null)
            return String.format("%1", this.getValue());
        else if (this.getLow() != null && this.getHigh() != null)
            return String.format("{%1 .. %2}", this.getLow(), this.getHigh());
        else if (this.getLow() != null)
            return String.format("{%1 ..}", this.getLow());
        else if (this.getHigh() != null)
            return String.format("{.. %1}", this.getHigh());
        else return super.toString();
	}
	/* (non-Javadoc)
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_high == null) ? 0 : m_high.hashCode());
		result = prime * result
				+ ((m_highInclusive == null) ? 0 : m_highInclusive.hashCode());
		result = prime * result + ((m_low == null) ? 0 : m_low.hashCode());
		result = prime * result
				+ ((m_lowInclusive == null) ? 0 : m_lowInclusive.hashCode());
		result = prime * result
				+ ((m_originalText == null) ? 0 : m_originalText.hashCode());
		result = prime * result + ((m_width == null) ? 0 : m_width.hashCode());
		return result;
	}
	/* (non-Javadoc)
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		IVL other = (IVL) obj;
		if (m_high == null) {
			if (other.m_high != null)
				return false;
		} else if (!m_high.equals(other.m_high))
			return false;
		if (m_highInclusive == null) {
			if (other.m_highInclusive != null)
				return false;
		} else if (!m_highInclusive.equals(other.m_highInclusive))
			return false;
		if (m_low == null) {
			if (other.m_low != null)
				return false;
		} else if (!m_low.equals(other.m_low))
			return false;
		if (m_lowInclusive == null) {
			if (other.m_lowInclusive != null)
				return false;
		} else if (!m_lowInclusive.equals(other.m_lowInclusive))
			return false;
		if (m_originalText == null) {
			if (other.m_originalText != null)
				return false;
		} else if (!m_originalText.equals(other.m_originalText))
			return false;
		if (m_width == null) {
			if (other.m_width != null)
				return false;
		} else if (!m_width.equals(other.m_width))
			return false;
		return true;
	}

	

	
}
