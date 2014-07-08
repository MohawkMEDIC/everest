/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
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
 * User: fyfej
 * Date: 3-6-2013
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.Interfaces;
using MARC.Everest.Connectors;
using System.Reflection;
using System.Diagnostics;

namespace MARC.Everest.Test
{
    internal static class XMLGenerator
    {

        private const string instanceDir = "Instances";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal static bool GenerateInstance(Type instanceType, Stream outputStream)
        {
            IResultDetail[] results = null;
            return GenerateInstance(instanceType, outputStream, out results);
        }

        internal static bool GenerateInstance(Type instanceType, Stream outputStream, out IResultDetail[] details)
        {

            string resourceName = String.Format(instanceType.FullName).Replace(".", "");

            var formatter = new MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter();
            formatter.ValidateConformance = false;
            //formatter.Settings = Formatters.XML.ITS1.SettingsType.DefaultLegacy;
            // Testing pregen
            formatter.GraphAides.Add(new MARC.Everest.Formatters.XML.Datatypes.R1.DatatypeFormatter());
            //formatter.BuildCache(Assembly.Load("MARC.Everest.RMIM.CA.R020402").GetTypes());

            IGraphable result = TypeCreator.GetCreator(instanceType).CreateInstance() as IGraphable;

            Trace.WriteLine("Starting Serialization");
            DateTime start = DateTime.Now;
            var gresult = formatter.Graph(outputStream, result);
            Trace.WriteLine(String.Format("            ->{0}", DateTime.Now.Subtract(start).TotalMilliseconds));
            Trace.WriteLine(String.Format("            ->{0} bytes", outputStream.Length));
            List<IResultDetail> dlist = new List<IResultDetail>();
            dlist.AddRange(gresult.Details);
            details = dlist.ToArray();

            return gresult.Code == MARC.Everest.Connectors.ResultCode.Accepted;
        }

    }
}
