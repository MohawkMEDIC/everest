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
 * Date: 08-31-2011
 */
package ca.marc.everest.formatters.xml.its1;

import java.io.InputStream;
import java.io.OutputStream;
import java.security.NoSuchAlgorithmException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

import javax.xml.stream.XMLStreamReader;
import javax.xml.stream.XMLStreamWriter;

import ca.marc.everest.interfaces.*;
import ca.marc.everest.datatypes.*;
import ca.marc.everest.datatypes.generic.*;

import ca.marc.everest.exceptions.ObjectDisposedException;
import ca.marc.everest.formatters.interfaces.*;
import java.io.*;
import java.math.BigDecimal;
import java.nio.MappedByteBuffer;
import java.nio.channels.FileChannel;
import java.nio.channels.FileChannel.MapMode;

/**
 * A formatter instance that has the capability to graph
 * and parse instances to/from the HL7v3 XML ITS 1.0 
 * specification
 */
public class XmlIts1Formatter implements IStructureFormatter, IXmlStructureFormatter {

	// set to true when the object's dispose method has been called
	private boolean m_disposed = false;
	// A list of graph aides that are set for this instance of the structure formatter
	private ArrayList<IStructureFormatter> m_graphAides = new ArrayList<IStructureFormatter>();
	// backing field for the host property
	private IStructureFormatter m_host;
	
	/**
	 * Tear down the XmlIts1Formatter instance and 
	 * mark the object as disposed.
	 */
	@Override
	public void dispose() {
		// TODO Add tear down here
		this.m_disposed = true;
	}

	/**
	 * Helper method that will throw an exception if the object has been disposed
	 */
	private void throwIfDisposed() throws ObjectDisposedException
	{
		if(this.m_disposed)
			throw new ObjectDisposedException("XmlIts1Formatter");
	}

	/**
	 * Performs provisioning steps on all assigned graph aides
	 */
	private void provisionGraphAides() throws ObjectDisposedException
	{
		throwIfDisposed();
		for(IStructureFormatter aide : this.m_graphAides)
			aide.setHost(this);
	}
	
	/**
	 * Parse an instance from the specified XmlStreamReader
	 * @param xr The XMLStreamReader to parse data from
	 * @return The formatter result of the parse operation
	 * @throws ParseException 
	 */
	@Override
	public IFormatterParseResult Parse(XMLStreamReader xr) throws ObjectDisposedException {
		throwIfDisposed();
		provisionGraphAides();
		// TODO Auto-generated method stub
	
		return null;
	}

	/**
	 * Graphs object o to the XmlStreamWriter xw
	 * @param xw The XmlStreamWriter to graph data to
	 * @param o The object that is to be graphed
	 * @return The result of the graph operation
	 */
	@Override
	public IFormatterGraphResult Graph(XMLStreamWriter xw, IGraphable o) throws ObjectDisposedException {
		throwIfDisposed();
		provisionGraphAides();
		
		// TODO Auto-generated method stub
		return null;
	}

	/**
	 * Gets the list of graph aides that are currently assigned
	 * to this instance of the formatter
	 */
	@Override
	public ArrayList<IStructureFormatter> getGraphAides() throws ObjectDisposedException {
		throwIfDisposed();
		return this.m_graphAides;
	}

	/**
	 * Gets the host of this formatter instance
	 */
	@Override
	public IStructureFormatter getHost() throws ObjectDisposedException {
		throwIfDisposed();
		return this.m_host;
	}

	/**
	 * Sets the host of this formatter instance. This is not expected to be
	 * called by external callers, rather it is set when the host is about
	 * to perform a parse/graph operation
	 */
	@Override
	public void setHost(IStructureFormatter value) throws ObjectDisposedException {
		throwIfDisposed();
		this.m_host = value;
	}

	/**
	 * Gets a list of structures that this 
	 * @return
	 */
	@Override
	public ArrayList<String> getHandledStructures() {
		// TODO Auto-generated method stub
		return null;
	}

	/**
	 * Graphs object o onto stream s
	 * @param s The stream to which the instance o is to be graphed
	 * @param o The object instance to be graphed
	 * @return An IFormatterGraphResult containing the result of the format operation
	 */
	@Override
	public IFormatterGraphResult Graph(OutputStream s, IGraphable o) throws ObjectDisposedException {
		throwIfDisposed();
		provisionGraphAides();
		
		// TODO Auto-generated method stub
		return null;
	}

	/**
	 * Parses an object from stream s
	 * @param s The stream from which the object instance is to be parsed
	 * @return An IFormatterParseResult that contains the results of the parse operation
	 */
	@Override
	public IFormatterParseResult Parse(InputStream s) throws ObjectDisposedException {
		// TODO Auto-generated method stub
		throwIfDisposed();
		provisionGraphAides();
		return null;
	}

	

}
