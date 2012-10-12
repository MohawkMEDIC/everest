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

import java.util.ArrayList;
import java.util.Collection;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.*;
import ca.marc.everest.datatypes.interfaces.IAny;
import ca.marc.everest.datatypes.interfaces.ISetComponent;
import ca.marc.everest.interfaces.IResultDetail;
import ca.marc.everest.interfaces.ResultDetailType;
import ca.marc.everest.resultdetails.DatatypeValidationResultDetail;

/**
 * Set component: An individual component belonging to a set
 * <p>See the IVL&lt;T> datatype for an example of a set</p>
 * <p>Note that the value property is maintained for schema compatibility and should not be set directly</p>
 */
@Structure(name = "SXCM", structureType = StructureType.DATATYPE)
public class SXCM<T extends IAny> extends PDV<T> implements ISetComponent<T> {

	// backing field for the set operator
	private SetOperator m_setOperator;
	
	/**
	 * Creates a new instance of the SXCM class
	 */
	public SXCM() { super(); }
	/**
	 * Creates a new instance of the SXCM class with the specified value
	 * @param value The initial value of the SXCM
	 */
	public SXCM(T value) { super(value); }
	/**
	 * Copies the values in copy to this instance of SXCM 
	 * @throws CloneNotSupportedException When SXCM is bound to a class which cannot be cloned
	 */
	@SuppressWarnings("unchecked")
	public SXCM(SXCM<T> copy) throws CloneNotSupportedException {
		super((T)copy.getValue().shallowCopy());
		this.m_setOperator = copy.getOperator();
	}
	/**
	 * Gets the operator that dictates how the component is included as part of the set
	 */
	public SetOperator getOperator() { return this.m_setOperator; }
	/**
	 * Sets the operator that dictates how the component is included as part of the set
	 * @param value The new operator for the set component
	 */
	public void setOperator(SetOperator value) { this.m_setOperator = value; }
	
	/* (non-Javadoc)
	 * @see java.lang.Object#hashCode()
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result
				+ ((m_setOperator == null) ? 0 : m_setOperator.hashCode());
		return result;
	}
	/* (non-Javadoc)
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj) {
			return true;
		}
		if (!super.equals(obj)) {
			return false;
		}
		if (!(obj instanceof SXCM)) {
			return false;
		}
		SXCM<?> other = (SXCM<?>) obj;
		if (m_setOperator != other.m_setOperator) {
			return false;
		}
		return true;
	}
	/** (non-Javadoc)
	 * @see ca.marc.everest.datatypes.ANY#validateEx()
	 */
	@Override
	public Collection<IResultDetail> validateEx() {
        Collection<IResultDetail >retVal = new ArrayList<IResultDetail>();
        if (this.getValue() != null)
            retVal.add(new DatatypeValidationResultDetail(ResultDetailType.WARNING, "SXCM", String.format(EverestValidationMessages.MSG_PROPERTY_SCHEMA_ONLY, "Value"), null));
        return retVal;
	}
	
	/**
	 * Translate to a QSET equivalent
	 */
	@SuppressWarnings("unchecked")
	public ISetComponent<T> translateToQSETComponent()
	{
		if (this instanceof IVL<?> || this instanceof PIVL<?> || this instanceof EIVL<?>)
            return this;
        else if (this instanceof SXPR<?>)
            return ((SXPR<T>)this).translateToQSET();
        else if (this instanceof SXCM<?>) // This is a value that will appear in a QSS
            return QSS.createQSS(this.getValue());
        return null;	}
	
}
