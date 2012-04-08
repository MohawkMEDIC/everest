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
 * Date: 06-25-2011
 */
package ca.marc.everest.datatypes;

import ca.marc.everest.annotations.*;
import ca.marc.everest.datatypes.generic.*;

import java.text.SimpleDateFormat;
import java.util.*;

/**
 * A quantity specifying a point on the axis of natural time.
 * <h3> 
 * Remarks
 * </h3>
 * <p>
 * You will notice that there are a series of constant values in this datatype.
 * These constants represent the precisions of the date object when formatted
 * as a string. We tried using enumerations however Java enumerations cannot
 * be used in switch cases (or so it appears) so yet again, we're stuck kicking 
 * it old school with a bunch of static final integers...
 * </p>
 */
@Structure(name = "TS", structureType = StructureType.DATATYPE)
public class TS extends PDV<String>
{

	/**
	 * Date is precise to the year
	 */
	public static final int YEAR = 4;
	/**
	 * Date is precise to the month
	 */
	public static final int MONTH = 6;
	/**
	 * Date is precise to the day
	 */
	public static final int DAY = 8;
	/**
	 * Date is precise to the hour without a timezone
	 */
	public static final int HOURNOTIMEZONE = 10;
	/**
	 * Date is precise to the minute without a timezone 
	 */
	public static final int MINUTENOTIMEZONE = 12;
	/**
	 * Date is precise to the second without a timezone
	 */
	public static final int SECONDNOTIMEZONE = 14;
	/**
	 * Date is a full precision without a timezone
	 */
	public static final int FULLNOTIMEZONE = 18;
	/**
	 * Date is precise to the hour with a timezeone
	 */
	public static final int HOUR = 15;
	/**
	 * Date is precise to the minute with a timezone
	 */
	public static final int  MINUTE = 17;
	/**
	 * Date is precise to the minute with a timezone
	 */
	public static final int SECOND = 19;
	/**
	 * Date is full precision with timezone.
	 */
	public static final int FULL = 23;
	/**
	 * Formats for the various flavours
	 */
	private static final HashMap<String, Integer> m_flavorPrecisions;
	/**
	 * Formats for the SimpleDateFormatter mapped to the precision of the date
	 */
	private static final HashMap<Integer, String> m_precisionFormats;
	
	/**
	 * :o No Comment 
	 */
	static {
		m_flavorPrecisions = new HashMap<String, Integer>();
		m_flavorPrecisions.put("DATETIME", TS.SECOND);
		m_flavorPrecisions.put("TS.DATETIME", TS.SECOND);
		m_flavorPrecisions.put("DATE", TS.SECOND);
		m_flavorPrecisions.put("TS.DATE", TS.SECOND);
		m_flavorPrecisions.put("", TS.SECOND);
		m_flavorPrecisions.put("FULLDATETIME", TS.SECOND);
		m_flavorPrecisions.put("TS.FULLDATETIME", TS.SECOND);
		m_flavorPrecisions.put("TS.FULLDATEWITHTIME", TS.SECOND);
		m_flavorPrecisions.put("FULLDATEWITHTIME", TS.SECOND);
		m_precisionFormats = new HashMap<Integer, String>();
		m_precisionFormats.put(TS.DAY, "yyyyMMdd");
		m_precisionFormats.put(TS.FULL, "yyyyMMddHHmmss.SSSSZ");
		m_precisionFormats.put(TS.FULLNOTIMEZONE, "yyyyMMddHHmmss.SSSS");
		m_precisionFormats.put(TS.HOUR, "yyyyMMddHHZ");
		m_precisionFormats.put(TS.HOURNOTIMEZONE, "yyyyMMddHH");
		m_precisionFormats.put(TS.MINUTE, "yyyyMMddHHmmZ");
		m_precisionFormats.put(TS.MINUTENOTIMEZONE, "yyyyMMddHHmm");
		m_precisionFormats.put(TS.MONTH, "yyyyMM");
		m_precisionFormats.put(TS.SECOND, "yyyyMMddHHmmssZ");
		m_precisionFormats.put(TS.SECONDNOTIMEZONE, "yyyyMMddHHmmss");
		m_precisionFormats.put(TS.YEAR, "yyyy");
	}
	
	/**
	 * Identifies the precision of this object
	 */
	private Integer m_dateValuePrecision = TS.FULL;
	/**
	 * The real value of the time stamp
	 */
	private Calendar m_dateValue;
	
	/**
	 * Creates a new instance of the time stamp class
	 */
	public TS() { super(); }
	/**
	 * Creates a new instance of the time stamp class with the specified value
	 * @param value The initial value of the time stamp
	 */
	public TS(Calendar value) { this.setDateValue(value); }
	/**
	 * Creates a new instance of the time stamp class with the specified value and precision
	 * @param value The initial value of the time stamp
	 * @param precision The precision of the time stamp
	 */
	public TS(Calendar value, int precision) { this(value); this.m_dateValuePrecision = precision; }
	
	/**
	 * Get the value of this time stamp object. This property is backed by the DateValue and DateValuePrecision
	 * properties.
	 */
	@Property(name = "value", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	@Override
	public String getValue() { // TODO: Complete this method
		return "";
	}
	/**
	 * Set the value of this object as a string. This property is backed by the DateValue and DateValuePrecision
	 * properties. Setting this property to an arbitrary (HL7v3) date string will force a re-parse of DateValuePrecision
	 * and DateValue. 
	 * @param value The HL7v3 formatted date string to parse
	 */
	@Override
	public void setValue(String value) { }
	
	/**
	 * Gets the value of the timestamp as a Java Date object
	 * @return The timestamp as a Java Date object
	 */
	public Calendar getDateValue() { return this.m_dateValue; }
	
	/**
	 * Sets the value of the timestamp as a Java Date object
	 * @param value The new Date to represent within this TS
	 */
	public void setDateValue(Calendar value) { this.m_dateValue = value; }
	
	/**
	 * Gets the precision of the DateValue. For example, a date time of January 1, 2009 with precision
	 * of Month means that the date is precise to January 2009, the same date with precision of full
	 * is precise to January 1 2009, 00:00:00.000
	 */
	public int getDateValuePrecision() { return this.m_dateValuePrecision; }
	
	/**
	 * Sets the precision of the DateValue 
	 */
	public void setDateValuePrecision(int value) { this.m_dateValuePrecision = value; }
	
	/**
	 * Gets the flavor of this instance 
	 */
	@Property(name = "flavorId", conformance = ConformanceType.OPTIONAL, propertyType = PropertyType.STRUCTURAL)
	@Override
	public String getFlavorId() { return super.getFlavorId(); }
	
	/**
	 * Represents this timestamp (with precision) to an interval.
	 * <p>
	 * If given a TS of January 1, 2009 with precision of Month, will create
	 * an IVL&lt;TS> with a low of January 1, 2009 00:00:00 and high of 
	 * January 31, 2009 11:59:59</p>
	 */
	public IVL<TS> toIVL()
	{
		
		// Is this TS full precision? If so, then the high/low are identical
		if(this.m_dateValuePrecision == TS.FULL ||
			this.m_dateValuePrecision == TS.FULLNOTIMEZONE)
			return new IVL<TS>(this, this);
		
		IVL<TS> retVal = new IVL<TS>();
		retVal.setOperator(SetOperator.Inclusive);
		
		Calendar lowCal = Calendar.getInstance(), highCal = Calendar.getInstance();
		
		// Get calendar
		Calendar cal = new GregorianCalendar(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), 1);
		int maxMonth = cal.getActualMaximum(Calendar.DAY_OF_MONTH);
		
		// Determine the date value precision
		switch(this.m_dateValuePrecision)
		{
			case TS.YEAR:

				lowCal.set(this.m_dateValue.get(Calendar.YEAR), 0, 1, 0, 0, 0);
				highCal.set(this.m_dateValue.get(Calendar.YEAR), 0, 31, 23, 59, 59);
				break;
			case TS.MONTH:
				lowCal.set(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), 1, 0, 0, 0);
				highCal.set(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), maxMonth, 23, 59, 59);
				break;
			case TS.DAY:
				lowCal.set(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), this.m_dateValue.get(Calendar.DAY_OF_MONTH), 0, 0, 0);
				highCal.set(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), this.m_dateValue.get(Calendar.DAY_OF_MONTH), 23, 59, 59);
				break;
			case TS.HOUR:
			case TS.HOURNOTIMEZONE:
				lowCal.set(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), this.m_dateValue.get(Calendar.DAY_OF_MONTH), this.m_dateValue.get(Calendar.HOUR_OF_DAY), 0, 0);
				highCal.set(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), this.m_dateValue.get(Calendar.DAY_OF_MONTH), this.m_dateValue.get(Calendar.HOUR_OF_DAY), 59, 59);
				break;
			case TS.MINUTE:
			case TS.MINUTENOTIMEZONE:
				lowCal.set(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), this.m_dateValue.get(Calendar.DAY_OF_MONTH), this.m_dateValue.get(Calendar.HOUR_OF_DAY), this.m_dateValue.get(Calendar.MINUTE), 0);
				highCal.set(this.m_dateValue.get(Calendar.YEAR), this.m_dateValue.get(Calendar.MONTH), this.m_dateValue.get(Calendar.DAY_OF_MONTH), this.m_dateValue.get(Calendar.HOUR_OF_DAY), this.m_dateValue.get(Calendar.MINUTE), 59);
				break;
				// TODO: Handle Second precision with milliseconds
		}
		
		return new IVL<TS>(new TS(lowCal), new TS(highCal));
	}
	
	/**
	 * Sets the flavor of this instance 
	 */
	@Override
	public void setFlavorId(String value) {
		if(this.m_dateValuePrecision == null)
		{
			Integer tdprec = m_flavorPrecisions.get(value);
			if(tdprec != null)
				this.m_dateValuePrecision = tdprec;
		}
		super.setFlavorId(value);
	}
	
}
