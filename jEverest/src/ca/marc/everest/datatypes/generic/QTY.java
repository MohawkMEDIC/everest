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
 * Date: 08-16-2011
 */
package ca.marc.everest.datatypes.generic;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.*;

/**
 * The quantity data type is an abstract generalization for all data types whose value set has an
 * order relation and where difference is defined in all of the data type's totally ordered value.
 */
@Structure(name = "QTY", structureType = StructureType.DATATYPE)
public class QTY<T> extends PDV<T> {

	// backing field for expression
	private ED m_expression;
	// Backing field for expression language
	private String m_expressionLanguage;
	// backing field for original text
	private ED m_originalText;
	// backing field for uncertainty
	private QTY<T> m_uncertainty;
	// backing field for uncertainty type
	private QuantityUncertaintyType m_uncertaintyType;
	// backing field for uncertainty range
	private IVL<QTY<T>> m_uncertaintyRange;
	
	/**
	 * Creates a new instance of QTY
	 */
	public QTY() {	}

	/**
	 * Creates a new instance of QTY with the specified value
	 */
	public QTY(T value) {
		super(value);
	}

	/**
	 * Gets an expression that represents the value of the quantity
	 */
	public ED getExpression() { return this.m_expression; }
	/**
	 * Sets an expression that represents the value of the quantity
	 */
	public void setExpression(ED value) { this.m_expression = value; }
	/**
	 * Gets the language used for the expression
	 */
	public String getExpressionLanguage() { return this.m_expressionLanguage; }
	/**
	 * Sets the language used for the expression
	 */
	public void setExpressionLanguage(String value) { this.m_expressionLanguage = value; }
	/**
	 * Gets a value that represents the original text that was used to derive the quantity 
	 */
	public ED getOriginalText() { return this.m_originalText; }
	/**
	 * Sets the original text that was used to derive the quantity
	 */
	public void setOriginalText(ED value) { this.m_originalText = value; }
	/**
	 * Gets a value that represents the uncertainty of the quantity using a distribution function
	 * and its parameters
	 */
	public QTY<T> getUncertainty() { return this.m_uncertainty; }
	/**
	 * Sets a value that represents the uncertainty of the quantity using a distribution function
	 * and its parameters
	 */
	public void setUncertainty(QTY<T> value) { this.m_uncertainty = value; }
	/**
	 * Gets a code specifying the type of probability distribution in uncertainty.
	 */
	public QuantityUncertaintyType getUncertaintyType() { return this.m_uncertaintyType; }
	/**
	 * Sets a code specifying the type of probability distribution in uncertainty.
	 */
	public void setUncertaintyType(QuantityUncertaintyType value) { this.m_uncertaintyType = value; }
	/**
	 * Gets 
	 * @return
	 */
	
	public IVL<QTY<T>> getUncertaintyRange() { return this.m_uncertaintyRange; }
	public void setUncertaintyRange(IVL<QTY<T>> value) { this.m_uncertaintyRange = value; }
	
	
	/**
	 * Validate the structure
	 */
	@Override
	public boolean validate()
	{
		return ((this.m_expression != null && this.m_expressionLanguage != null) || this.m_expression == null) &&
		(((this.m_uncertaintyRange != null) ^ (this.m_uncertainty != null)) || this.m_uncertaintyRange == null && this.m_uncertainty == null) &&
		super.validate();
	}
	
}
