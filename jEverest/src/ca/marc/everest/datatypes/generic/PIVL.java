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
 * User: Justin Fyfe
 * Date: 10-04-2012
 */
package ca.marc.everest.datatypes.generic;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IDistanceable;
import ca.marc.everest.datatypes.interfaces.IEncapsulatedData;
import ca.marc.everest.datatypes.interfaces.IOriginalText;
import ca.marc.everest.datatypes.*;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Represents an interval of time that recurs periodically
 */
@Structure(name = "PIVL", structureType = StructureType.DATATYPE)
public class PIVL<T extends IAny> extends SXCM<T> implements IOriginalText {

	// Backing field for phase
	private IVL<T> m_phase;
	// Backing field for period
	private PQ m_period;
	// Backing field for alignment
	private CalendarCycle m_alignment;
	// backing field for institutionspecified
	private Boolean m_institutionSpecified;
	// backing field for value
	private T m_value;
	// backing field for count
	private INT m_count;
	// backing field for frequency
	private RTO<INT, PQ> m_frequency;
	// backing field for originalText
	private ED m_originalText;
	
	/**
	 * Creates a new instance of the PIVL
	 */
	public PIVL() { super(); }
	/**
	 * Creates a new instance of the PIVL class with the specified phase and period
	 * @param phase The initial value specifying the phase of the PIVL
	 * @param period The initial value of the period (or occurrence) of the PIVL
	 */
	public PIVL(IVL<T> phase, PQ period) { this.m_phase = phase; this.m_period = period; }
	/**
	 * Creates a new instance of the PIVL class with the specified phase and frequency
	 * @param phase The initial value specifying the phase of the PIVL
	 * @param frequency A value specifying the frequency of occurrence of the PIVL
	 */
	public PIVL(IVL<T> phase, RTO<INT, PQ> frequency) { this.m_phase = phase; this.m_frequency = frequency; }
	/**
	 * Creates a new instance of the PIVL class with the specified phase and period occuring count times
	 * @param phase The initial value specifying the phase of the PIVL
	 * @param period The initial value of the period (or occurrence) of the PIVL
	 * @param count The maximum number of repetitions for the PIVL
	 */
	public PIVL(IVL<T> phase, PQ period, INT count) { this(phase, period); this.m_count = count; }
	/**
	 * Creates a new instance of the PIVL class with the specified phase and frequency occurring count times
	 * @param phase The initial value specifying the phase of the PIVL
	 * @param frequency A value specifying the frequency of occurrence of the PIVL
	 * @param count The maximum number of repetitions for the PIVL
	 */
	public PIVL(IVL<T> phase, RTO<INT,PQ> frequency, INT count) { this(phase, frequency); this.m_count = count; }
	
	/**
	 * Gets a value representing the interval
	 */
	@Property(name = "phase", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public IVL<T> getPhase() {
		return this.m_phase;
	}

	/**
	 * Sets a value representing the interval
	 */
	public void setM_phase(IVL<T> value) {
		this.m_phase = value;
	}

	/**
	 * Gets a time duration specifying the reciprocal measure of the frequency at which the phase repeats
	 */
	@Property(name = "period", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	public PQ getPeriod() {
		return this.m_period;
	}

	/**
	 * Sets a time duration specifying the reciprocal measure of the frequency at which the phase repeats
	 */
	public void setPeriod(PQ value) {
		this.m_period = value;
	}

	/**
	 * Gets a value which specifies if an how the repetitions are aligned to the cycles of the underlying calendar
	 */
	@Property(name = "alignment", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	public CalendarCycle getAlignment() {
		return this.m_alignment;
	}

	/**
	 * Sets a value which specifies if and how the repetitions are aligned to the cycles of the underlying calendar
	 */
	public void setAlignment(CalendarCycle value) {
		this.m_alignment = value;
	}

	/**
	 * Gets a value which indicates whether the exact timing is up to the party executing the schedule
	 */
	@Property(name = "institutionSpecified", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	public Boolean getInstitutionSpecified() {
		return this.m_institutionSpecified;
	}

	/**
	 * Sets a value which indicates whether the exact timing is up to the party executing the schedule
	 */
	public void setInstitutionSpecified(Boolean value) {
		this.m_institutionSpecified = value;
	}

	/**
	 * Gets a value indicating the maximum number of repetitions the period can repeat
	 */
	@Property(name = "count", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	public INT getCount() {
		return this.m_count;
	}

	/**
	 * Sets a value indicating the maximum number of repetitions the period can repeat
	 */
	public void setCount(INT value) {
		this.m_count = value;
	}

	/**
	 * Gets the value which indicates the frequency at which the interval repeats
	 */
	@Property(name = "frequency", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	public RTO<INT, PQ> getM_frequency() {
		return m_frequency;
	}

	/**
	 * Sets the value which indicates the frequency at which the interval repeats
	 */
	public void setM_frequency(RTO<INT, PQ> value) {
		this.m_frequency = value;
	}

	/**
	 * Gets a value representing the reason why the specified interval was supplied
	 */
	@Property(name = "originalText", propertyType = PropertyType.NONSTRUCTURAL, conformance = ConformanceType.REQUIRED)
	@Override
	public IEncapsulatedData getOriginalText() {
		return this.m_originalText;
	}

	/**
	 * Sets a value representing the reason why the specified interval was supplied
	 */
	@Override
	public void setOriginalText(IEncapsulatedData value) {
		this.m_originalText = (ED)value;
	}

	/**
	 * Sets a value representing the reason why the specified interval was specified
	 */
	public void setOriginalText(ED value)
	{
		this.m_originalText = value;
	}
	/**
	 * @see ca.marc.everest.datatypes.generic.PDV#validate()
	 */
	@Override
	public boolean validate() {
		  return ((this.isNull() && this.m_period == null && this.m_phase == null && this.m_frequency == null) || (!this.isNull())) &&
	                ((!this.isNull() && (this.m_period != null) ^ (this.m_frequency != null)) &&
	                 ((this.m_phase != null && this.m_phase.getWidth() != null && this.m_period != null && this.m_period.compareTo(this.m_phase.getWidth()) >= 0) || (this.m_phase == null || this.m_period == null || this.m_phase.getWidth() == null)) ||
	                 (this.isNull()));
	}
	
	/**
	 * @see ca.marc.everest.datatypes.ANY#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		 List<IResultDetail> retVal = new ArrayList<IResultDetail>(super.validateEx());

         if(this.isNull() && (this.m_period != null || this.m_frequency != null || this.m_phase != null)
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "PIVL", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
         else if(!this.isNull() && this.m_period == null && this.m_period == null)
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "PIVL", EverestValidationMessages.MSG_NULLFLAVOR_MISSING, null));
         if((this.m_frequency != null) ^ (this.m_period != null))
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "PIVL", String.format(EverestValidationMessages.MSG_INDEPENDENT_VALUE, "Frequency", "Period"), null));
         if(this.m_phase != null && (this.m_phase.getWidth() == null || this.m_phase.getWidth().isNull() || this.m_phase.getWidth().compareTo(this.m_period) > 0))
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "PIVL", "Width property of Phase must be less than the Period property", null));
         return retVal;
	}
	
	/**
	 * @see ca.marc.everest.datatypes.ANY#semanticEquals(ca.marc.everest.datatypes.interfaces.IAny)
	 */
	@SuppressWarnings("unchecked")
	@Override
	public BL semanticEquals(IAny other) {
		BL baseEq = super.semanticEquals(other);
        if (!baseEq.toBoolean())
            return baseEq;

        // Null-flavored
        if (this.isNull() && other.isNull())
            return BL.TRUE;
        else if (this.isNull() ^ other.isNull())
            return BL.FALSE;

        PIVL<T> eivlOther = (PIVL<T>)other;
        if (eivlOther == null)
            return BL.FALSE;
        else
            return
                (eivlOther.getPeriod() == null ? this.getPeriod() == null : eivlOther.getPeriod().semanticEquals(this.getPeriod()).toBoolean())
                &&
                (eivlOther.getFrequency() == null ? this.getFrequency() == null : eivlOther.getFrequency().semanticEquals(this.getFrequency()).toBoolean()) 
                &&
                (eivlOther.getPhase() == null ? this.getPhase() == null : eivlOther.getPhase().semanticEquals(this.getPhase()).toBoolean());
	}
	
	
	/**
	 * Determines if member is contained (or described by) the periodic interval
	 */
	public boolean Contains(T member)
	{
		// Algorithm:
        //  phase.translate((int)((member - low) / period) * period)
        // 
        if(!(member instanceof IDistanceable<?>))
            throw new IllegalArgumentException("Unable to calculate the distance between objects of specified type");
        if (this.m_phase == null)
            throw new UnsupportedOperationException("Cannot determine containment of a PIVL that is not bound with a Phase");

        IDistanceable<T> distMemb = (IDistanceable<T>)member;
        PQ period = this.m_period;

        if (period == null && this.m_frequency != null) // If frequency is specified, then make period the reciprocal
            period = this.m_frequency.getDenominator().divide(this.m_frequency.getNumerator());

        // Get the distance between the target iteration and the phase
        PQ desiredTranslation = distMemb.distance(this.m_phase.getLow());

        // Get the desired translation in repeats
        desiredTranslation = desiredTranslation.divide(period);
        desiredTranslation.setValue(BigDecimal.valueOf(Math.round(desiredTranslation.toDouble())));

        if (this.m_count != null && this.m_count.compareTo(new INT(desiredTranslation.toInteger())) > 0) // number of iterations exceeds limit
            return false;
        else if (desiredTranslation.getValue().intValue() < 0) // Happens before offset, meaning first 
            return false;

        // Correct to periods we need to translate by
        desiredTranslation = desiredTranslation.multiply(period);

        // Translate phase
        IVL<T> translatedPhase = this.m_phase.translate(desiredTranslation);
        return translatedPhase.contains(member);		
	}
	
	
	
}
