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
 * Date: 09-02-2011
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.datatypes.generic.*;
import ca.marc.everest.datatypes.interfaces.IQuantity;
import ca.marc.everest.annotations.*;
import java.math.*;

/**
 * An extension of coded value data type that can represent a
 * quantity using any unit of measure from any code system
 */
@Structure(name = "PQR", structureType = StructureType.DATATYPE)
public class PQR extends CV<String> implements IQuantity {

	// backing field for value
	private BigDecimal m_value;
	
	/**
	 * Creates a new instance of the PQR type
	 */
	public PQR() {}
	/**
	 * Creates a new instance of the PQR type with the specified values
	 * @param value The value of the PQR
	 * @param code The code that describes the unit of measure
	 * @param codeSystem The code system from which the code was selected
	 */
	public PQR(BigDecimal value, String code, String codeSystem) {
		super(code, codeSystem);
		this.m_value = value;
	}
	
	/**
	 * Gets the value of the PQR instance
	 */
	@Property(name = "value", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.MANDATORY)
	public BigDecimal getValue() { return this.m_value; }
	/**
	 * Sets the value of the PQR instance
	 */
	public void setValue(BigDecimal value) { this.m_value = value; }
	
	/**
	 * Gets the precision of the BigDecimal that is the primary value of this PQ
	 */
	public int getPrecision() { 
		String bigdRep = this.getValue().toPlainString(); // wth?
		int precision = bigdRep.length();
		precision = precision - bigdRep.lastIndexOf('.');
		return precision;
	}
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = super.hashCode();
		result = prime * result + ((m_value == null) ? 0 : m_value.hashCode());
		return result;
	}
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (!super.equals(obj))
			return false;
		if (getClass() != obj.getClass())
			return false;
		PQR other = (PQR) obj;
		if (m_value == null) {
			if (other.m_value != null)
				return false;
		} else if (!m_value.equals(other.m_value))
			return false;
		return true;
	}
	/**
	 * Represent this PQR as an integer
	 */
	@Override
	public Integer toInteger() {
		return this.getValue().intValue();
	}
	/**
	 * Represent this PQR as a double
	 */
	@Override
	public Double toDouble() {
		return this.getValue().doubleValue();
	}
}
