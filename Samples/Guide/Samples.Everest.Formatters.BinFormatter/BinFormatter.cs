using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.Connectors;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MARC.Everest.Interfaces;

namespace Samples.Everest.Formatters.BinFormatter
{
    /// <summary>
    /// Formats structures to binary
    /// </summary>
    public class BinFormatter : IStructureFormatter
    {
        /// <summary>
        /// Create a new instance of BinFormatter
        /// </summary>
        public BinFormatter() { this.GraphAides = new List<IStructureFormatter>(); }

        #region IStructureFormatter Members

        /// <summary>
        /// Clone this formatter
        /// </summary>
        public Object Clone()
        {
            BinFormatter bf = (BinFormatter)this.MemberwiseClone(); // Clone the object
            bf.GraphAides.Clear(); // don't share graph aide instances
            bf.Details = null; // clear details
            // Now instantiate a new set of graph aides
            foreach (var f in bf.GraphAides)
                bf.GraphAides.Add(f.Clone() as IStructureFormatter);
            return bf;
        }

        /// <summary>
        /// Aides to this formatter
        /// </summary>
        public List<IStructureFormatter> GraphAides
        {
            get;
            set;
        }

        /// <summary>
        /// Tell any caller this formatter handles any structure
        /// </summary>
        public List<string> HandleStructure
        {
            get { return new List<string>() { "*" }; }
        }

        /// <summary>
        /// Details of the operation
        /// </summary>
        public IResultDetail[] Details
        {
            get;
            private set;
        }

        /// <summary>
        /// Get or set the host formatter
        /// </summary>
        public IStructureFormatter Host
        {
            get;
            set;
        }

        /// <summary>
        /// Graph the object
        /// </summary>
        /// <param name="s">The stream to graph to</param>
        /// <param name="o">The IGraphable object to graph</param>
        /// <returns>The result of the operation</returns>
        public ResultCode GraphObject(Stream s, IGraphable o)
        {
            // Everest 1.0 changed the way formatters work so this method really only
            // exists to be backwards compatible
            var result = Graph(s, o);
            this.Details = result.Details;
            return result.Code;
        }

        /// <summary>
        /// Graph object <paramref name="o"/> onto stream <paramref name="s"/>
        /// </summary>
        public IFormatterGraphResult Graph(Stream s, IGraphable o)
        {
            try
            {
                BinaryFormatter fmt = new BinaryFormatter();
                fmt.Serialize(s, o);
                return new BinaryFormatterGraphResult()
                {
                    Code = ResultCode.Accepted,
                    Details = new IResultDetail[0]
                };
            }
            catch (Exception e)
            {
                return new BinaryFormatterGraphResult() // Invalid result
                {
                    Details = new IResultDetail[] {
                            new ResultDetail(ResultDetailType.Error, e.Message, e)
                            },
                    Code = ResultCode.Error
                };
            }
        }

        /// <summary>
        /// Parse object from <paramref name="s"/>
        /// </summary>
        /// <param name="s">The stream to parse the object from</param>
        /// <returns>The parsed object</returns>
        public IGraphable ParseObject(Stream s)
        {
            var result = Parse(s);
            this.Details = result.Details;
            return result.Structure;
        }


        /// <summary>
        /// Parse an object from <paramref name="s"/>
        /// </summary>
        public IFormatterParseResult Parse(Stream s)
        {
            try
            {
                BinaryFormatter fmt = new BinaryFormatter();
                return new BinaryFormatterParseResult() // Create valid result
                {
                    Structure = (IGraphable)fmt.Deserialize(s),
                    Code = ResultCode.Accepted
                };
            }
            catch (Exception e)
            {
                return new BinaryFormatterParseResult() // Create invalid result
                {
                    Details = new IResultDetail[] {
                        new ResultDetail(ResultDetailType.Error, e.Message, e)
                    },
                    Code = ResultCode.Error
                };
            }
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            return;
        }

        #endregion
    }
}
