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
 * Date: 07-08-2011
 */
package ca.marc.everest.interfaces;

/**
 * Since Java doesn't have the capability to append meta-data to JAR files
 * easily (like the AssemblyInfo file in .NET) all auto-generated RMIM 
 * JAR files will have a class in their root package called JarInfo which 
 * contain the name, date, license, etc... of the generated file
 */
public interface IGeneratedJarInfo {

	/**
	 * Gets the title (ie:friendly name) of the JAR file
	 */
	String getTitle();

	/**
	 * Get the version of GPMR that was used to generate the file
	 */
	String getGpmrVersion();

	/**
	 * Get the name of the company that generated the JAR file 
	 */
	String getCompany();

	/**
	 * Get copyright information about the JAR file
	 */
	String getCopyright();

	/**
	 * Get the version in major.minor.release.build of the JAR
	 */
	String getVersion();

	/**
	 * Get the informational version of the JAR file (example: R02.04.02, NE2008, etc...)
	 */
	String getInformationalVersion();
	
}
