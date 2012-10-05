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
 * Date: 10-05-2012
 */
package ca.marc.everest.datatypes.generic;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.List;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.EverestValidationMessages;
import ca.marc.everest.datatypes.SetOperator;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.IListContainer;
import ca.marc.everest.datatypes.interfaces.ISetComponent;
import ca.marc.everest.interfaces.IGraphable;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Represents a QSET{T} that has been specifialized as a union of its components 
 * 
 * @see QSI QSI (Intersection)
 * @see QSD QSD (Difference)
 * @see QSP QSP (Periodic Hull)
 * @see QSU QSU (Union)
 * @see SXPR SXPR (Set Expression)
 * @see SXCM SXCM (Set Components)
 * @see GTS GTS (General Timing Specification)
 */
@Structure(name = "QSU", structureType = StructureType.DATATYPE)
public class QSU<T extends IAny> extends QSC<T> implements Collection<ISetComponent<T>>, IListContainer {

	
	/**
	 * Creates a new instance of the QSU class
	 */
	public QSU() { super(); }
	/**
	 * Creates a new instance of the QSU class with the specified items
	 * @param terms
	 */
	public QSU(Collection<ISetComponent<T>> terms)
	{
		this.p_terms = new ArrayList<ISetComponent<T>>(terms);
	}
	/**
	 * Creates a new QSU from the specified terms
	 */
	public static <T extends IAny> QSU<T> createQSU(ISetComponent<T>...terms)
	{
		return new QSU<T>(Arrays.asList(terms));
	}
	
	/**
	 * Gets the equivalent set operator if this QSI items are represented in a SXPR
	 */
	@Override
	protected SetOperator getEquivalentSetOperator() {
		return SetOperator.Inclusive;
	}

	/**
	 * Normalizes this expression so that all items only use the QS* components
	 */
	@SuppressWarnings("unchecked")
	@Override
	public IGraphable normalize() {
		QSU<T> retVal = (QSU<T>)this.shallowCopy();
		retVal.setTerms(new ArrayList<ISetComponent<T>>(this.getTerms())); // re-reference array
		for(int i = 0; i < retVal.getTerms().size(); i++)
			if(retVal.getTerms().get(i) instanceof SXPR<?>)
				retVal.p_terms.set(i, ((SXPR<T>)retVal.getTerms().get(i)).translateToQSET());
		return retVal;
	}
	
	/**
	 * Extended validation routine returning detected issues
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
		 List<IResultDetail> retVal = new ArrayList<IResultDetail>();

         if (!(this.isNull() ^ !this.isEmpty()))
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSU", EverestValidationMessages.MSG_NULLFLAVOR_WITH_VALUE, null));
         else if(this.p_terms.size() < 2)
             retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSU", "QSU must contain at least two items for intersection", null));
         for (ISetComponent<T> qs : this.p_terms)
             if (qs == null || qs.isNull())
                 retVal.add(new DatatypeValidationResultDetail(ResultDetailType.ERROR, "QSU", EverestValidationMessages.MSG_NULL_COLLECTION_VALUE, null));
         return retVal;
	}



}
