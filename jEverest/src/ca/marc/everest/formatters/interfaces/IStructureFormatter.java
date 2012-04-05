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
 * Date: 07-21-2011
 */
package ca.marc.everest.formatters.interfaces;

import java.io.*;
import ca.marc.everest.interfaces.*;
import java.util.*;

/**
 * Represents a class that has the ability to render an IGraphible object
 * into another form, effectively serializing it
 */
public interface IStructureFormatter extends Cloneable, IDisposable {

	
	/**
	 * Gets a list of graph aides that are to be used
	 * to assist this formatter in the graphing of 
	 * instances
	 */
	ArrayList<IStructureFormatter> getGraphAides();

	/**
	 * Gets the host of this formatter. When the 
	 * formatter is added as a graph aide, the HOST
	 * is usually the container structure formatter
	 */
	IStructureFormatter getHost();
	/**
	 * Sets the host of this formatter. Should not be
	 * set by callers and exists in this interface to support
	 * graph aides adding themselves as the host
	 */
	void setHost(IStructureFormatter value);

	/**
	 * Gets a list of structures that this graph 
	 * aide can "handle". An asterisk (*) usually
	 * means that the formatter is flexible and
	 * can handle any type of structure.
	 */
	ArrayList<String> getHandledStructures();

	/**
	 * Graphs object o onto output stream s
	 * @param s The stream to graph the instance to
	 * @param o The object that is to be graphed
	 * @return An IFormatterGraphResult that contains the result of the graph
	 */
	IFormatterGraphResult Graph(OutputStream s, IGraphable o);
	
	/**
	 * Parses an object instance from input stream s
	 * @param s The stream that is to be parsed
	 * @return An IFormatterParseResult that contains the result of the parse operation
	 */
	IFormatterParseResult Parse(InputStream s);

}
