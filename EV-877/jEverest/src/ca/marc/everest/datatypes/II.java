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
 * Date: 09-09-2011
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.interfaces.*;
import java.util.*;
import java.util.regex.*;

/**
 * Represents a globally unique reference number that identifies an object
 */
@Structure(name = "II", structureType = StructureType.DATATYPE)
public class II extends ANY {

	// Backing field for root
	private String m_root;
	// Backing field for extension
	private String m_extension;
	// Backing field for scope
	private IdentifierScope m_scope;
	// Backing field for reliability
	private IdentifierReliability m_reliability;
	// Backing field for name
	private String m_identifierName;
	// Backing field for displayable property
	private boolean m_displayable;
	// Backing field for assigning authority name
	private String m_assigningAuthorityName;
	
	/**
	 * Creates a new instance of the II type
	 */
	public II() { super(); }
	/**
	 * Creates a new instance of the II type with the specified
	 * UUID as its root
	 * @param root A UUID instance representing a globally unique identifier (GUID)
	 */
	public II(UUID root) 
	{ 
		this.m_root = root.toString();
	}
	/**
	 * Creates a new instance of the II type with the specified 
	 * string as its root. The string should be an OID in the form
	 * x.y.z.cc.....
	 * @param root A string value (typically an OID) that qualifies the extension
	 */
	public II(String root) 
	{ 
		this.m_root = root;
	}
	/**
	 * Creates a new instance of the II type with the specified
	 * string as its root and string as its extension. Typically
	 * the root will be an OID in dotted notation and the extension
	 * will be a unique identifier within the OID domain.
	 * 
	 * @param root A string value (typically an OID) that qualifies the extension
	 * @param extension A string value that represents the value of the unique identifier within the domain
	 */
	public II(String root, String extension)
	{
		this(root);
		this.m_extension = extension;
	}
	
	/**
	 * Gets a value that guarantees the uniqueness of the extension of this instance identifier
	 */
	@Property(name = "root", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public String getRoot() { return this.m_root; }
	/**
	 * Sets a value that guarantees the uniqueness of the extension of this instance identifier
	 */
	public void setRoot(String value) { this.m_root = value; }
	/**
	 * Gets a character string that uniquely identifies the object
	 */
	@Property(name = "extension", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public String getExtension() { return this.m_extension; }
	/**
	 * Sets a character string that uniquely identifies the object
	 */
	public void setExtension(String value) { this.m_extension = value; }
	/**
	 * Gets a value that identifies the scope under which this identifier applies to the object
	 */
	@Property(name = "scope", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public IdentifierScope getScope() { return this.m_scope; }
	/**
	 * Sets a value that identifies the scope under which this identifier applies to the object
	 */
	public void setScope(IdentifierScope value) { this.m_scope = value; }
	/**
	 * Gets a value that specifies the reliability of the instance identifier
	 */
	@Property(name = "reliability", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public IdentifierReliability getReliability() { return this.m_reliability; }
	/**
	 * Sets a value that specifies the reliability of the instance identifier
	 */
	public void setReliability(IdentifierReliability value) { this.m_reliability = value; }
	/**
	 * Gets a value that specifies if the identifier is intended to be displayed on a user screen
	 */
	@Property(name = "displayable", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.REQUIRED)
	public boolean getDisplayable() { return this.m_displayable; }
	/**
	 * Sets a value that specifies if the identifier is intended to be displayed on a user screen
	 * @param value
	 */
	public void setDisplayable(boolean value) { this.m_displayable = value; }
	/**
	 * Gets a human readable name for the identifier
	 */
	@Property(name = "identifierName", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public String getIdentifierName() { return this.m_identifierName; }
	/**
	 * Sets a human readable name for the identifier
	 */
	public void setIdentifierName(String value) { this.m_identifierName = value; }
	/**
	 * Gets the authority responsible for the assignment of the identifier
	 */
	@Property(name = "assigningAuthorityName", propertyType = PropertyType.STRUCTURAL, conformance = ConformanceType.OPTIONAL)
	public String getAssigningAuthorityName() { return this.m_assigningAuthorityName; }
	/**
	 * Sets the authority that is responsible for the assignment of the identifier.
	 */
	public void setAssigningAuthorityName(String value) { this.m_assigningAuthorityName = value; }
	
	/**
	 * Determines if the root of the specified II is a UUID
	 */
	private static boolean isRootUuid(II ii)
	{
		Pattern rp = Pattern.compile("[{]?[A-F0-9]{8}-?([A-F0-9]{4}-?){3}[A-F0-9]{12}");
		// Does this next line create a matcher? I dunno... Apparently the Java SL can't pick
		// one pattern and stick to it. Why do I call a static method on Pattern, but 
		// then call a completely different method that creates something that doesn't 
		// have the word create, then for other patterns I use newInstance, or createInstance,
		// or getInstance, or a constructor, or some other flavor du jour... This is one of my 
		// biggest complaints about Java.. Lack of consistency even in the standard library? Why? 
		// There is no excuse for this, Sun developed and was in control of the SL so why couldn't 
		// they just pick one pattern and stick to it or at least be consistent about how they chose 
		// to use patterns. These same Sun biggots are the ones who will argue that operator overloading
		// can be abused therefore it is not included in the Language spec? What? So I can't have a +
		// operator on BigDecimal but I call matcher() which is really a create method?!?!?!?!?!?!
		//
		// The SL appears to be a mish-mash of patterns that are neither compatible
		// nor consistent with each other. This leads to the Java coding cycle, let me 
		// explain the regular coding cycle (C#, VB, Delphi, most modern languages):
		//  	Code X
		//		Code Y
		//		Code Z
		//		Compile
		//		Run
		//		Repeat
		//
		// The Java coding cycle:
		//		Code ... 
		//		Hmm.. well maybe to do X, I use this... no.. ok..
		//		Google...
		//		Ok, now I have to Y, is it the same as X? ... let's try...
		//		Hmm... nope.. ok... 
		//		Google...
		//		Ok, now I want to do Z, is it the same as X? ... try
		//		Hmm... nope.. same as Y?
		//		Hmm... nope.. ok...
		//		Google...
		//		Compile
		//		Run
		//		Google...
		// 
		// Java is tedious...
		// </rant>
		Matcher matcher = rp.matcher(ii.getRoot()); 
 
		return matcher.find();
	}
}
