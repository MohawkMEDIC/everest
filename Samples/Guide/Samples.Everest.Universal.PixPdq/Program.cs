/*
 * This sample illustrates the use of Everest to construct a PIX and PDQ Version 3 message. The 
 * message content is based on the samples provided by IHE as part of the ITI technical supplements.
 * 
 * Author: Justin Fyfe
 * Date: November 6, 2010
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;
using MARC.Everest.Xml;
using System.Xml;
using MARC.Everest.Interfaces;
using MARC.Everest.Formatters.XML.Datatypes.R1;

namespace Samples.Everest.Universal.PixPdq
{
    partial class Program
    {

        static MARC.Everest.Formatters.XML.ITS1.Formatter m_formatter;

        static void Main(string[] args)
        {
            
            // This is a nifty optimization we can do
            Console.WriteLine("Initializing...");
            InitializeFormatter();

            // Ask for the message to construct
            Console.WriteLine("Please Select A Message To Generate:");
            Console.WriteLine("1) PIX Version 3 Message");
            Console.WriteLine("2) PDQ Version 3 Message");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    GeneratePIX();
                    break;
                case '2':
                    GeneratePDQ();
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }   

            // Quit
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Initialize formatter
        /// </summary>
        private static void InitializeFormatter()
        {
            m_formatter = new MARC.Everest.Formatters.XML.ITS1.Formatter();
            m_formatter.ValidateConformance = false;
            m_formatter.GraphAides.Add(new DatatypeFormatter() { CompatibilityMode = DatatypeFormatterCompatibilityMode.ClinicalDocumentArchitecture });
            m_formatter.BuildCache(new Type[] { // Using Build Cache will greatly increase performance
                typeof(PRPA_IN201305UV02),
                typeof(PRPA_IN201309UV02)
            });
        }

        /// <summary>
        /// Illustrates formatting a message
        /// </summary>
        private static void FormatInstance(IGraphable message)
        {
            Console.WriteLine("Formatting...");

            // We'll indent the output
            XmlStateWriter xw = new XmlStateWriter(XmlWriter.Create(Console.OpenStandardOutput(), new XmlWriterSettings() { Indent = true }));
            m_formatter.Graph(xw, message);
            xw.Flush();
        }



    }
}
