using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// Represents an enumeration which is defined as a concept domain
    /// </summary>
    public class ConceptDomain : Enumeration
    {

        /// <summary>
        /// Get the type of enumeration structure this class represents
        /// </summary>
        public override string EnumerationType
        {
            get { return "Concept Domain"; }
        }

        private string classDiagram;

        /// <summary>
        /// Get referenced nodes
        /// </summary>
        private void GetReferencedNodes(List<Enumeration> nodes)
        {
            
            // Specializations
            foreach (var spec in this.Specializes ?? new List<ConceptDomain>())
            {
                if (!nodes.Exists(o=>o is ConceptDomain && o.Name == spec.Name))
                {
                    nodes.Add(spec);
                    spec.GetReferencedNodes(nodes);
                }
            }
            // Specialized by
            foreach (var spec in this.SpecializedBy ?? new List<ConceptDomain>())
            {
                if (!nodes.Exists(o => o is ConceptDomain && o.Name == spec.Name))
                {
                    nodes.Add(spec);
                    spec.GetReferencedNodes(nodes);
                }
            }

            // Context bindings
            List<Enumeration> contexts = new List<Enumeration>();
            foreach (var nd in nodes)
            {
                if (nd is ConceptDomain)
                {
                    foreach (var ctx in (nd as ConceptDomain).ContextBinding ?? new List<Enumeration>())
                        if (!contexts.Contains(ctx))
                            contexts.Add(ctx);
                }
            }

            nodes.AddRange(contexts);
            
        }

        /// <summary>
        /// Represent this image as a base64 encoded PNG of a class diagram representing the class structure
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public string GraphicalRepresentation
        {
            get
            {
                if (classDiagram != null) return classDiagram;

                List<Enumeration> nodes = new List<Enumeration>();
                nodes.Add(this);
                this.GetReferencedNodes(nodes);

                // Now layout
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("digraph class_{0} {{ ratio=compress rankdir=BT fontname=\"Arial\" fontsize=10 ", Name);

                // Draw Contents
                foreach (var nd in nodes)
                {
                    // Declare this as a node
                    sb.AppendFormat("node [fontname=\"Arial\" fontsize=10 shape=record color={3}] {2}_{0} [label=\"{{{0}|({1})}}\"] ", nd.Name, nd.EnumerationType, nd.GetType().Name, nd == this ? "red" : "black");

                    // Draw inheritence
                    // Get the base representation
                    if (nd is ConceptDomain)
                    {
                        var ndc = nd as ConceptDomain;
                        // Draw specializes
                        foreach (var spec in ndc.Specializes ?? new List<ConceptDomain>())
                        {
                            if(nodes.Contains(spec))
                                sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 arrowhead=empty color=black style=\"solid\" headlabel=\"\" label=\"\"] node[color=black] {2}_{0}->{3}_{1} ", ndc.Name, spec.Name, nd.GetType().Name, spec.GetType().Name);
                        }
                        // Draw specialized by
                        //foreach (var spec in ndc.SpecializedBy ?? new List<ConceptDomain>())
                        //    sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 arrowhead=empty color=black style=\"solid\" headlabel=\"\" label=\"\"] node[color=black] {2}_{0}->{3}_{1} ", spec.Name, ndc.Name, spec.GetType().Name, nd.GetType().Name);
                        // Draw context binding
                        foreach (var spec in ndc.ContextBinding ?? new List<Enumeration>())
                            sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 arrowhead=solid color=black style=\"dashed\" headlabel=\"\" label=\"binding\"] node[color=black] {2}_{0}->{3}_{1} ", ndc.Name, spec.Name, nd.GetType().Name, spec.GetType().Name);

                    }
                    
                }

                sb.Append("}");

                classDiagram = sb.ToString();

                return classDiagram;
            }
        }

        /// <summary>
        /// Get only the class representation
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        internal string GraphicalRepresentationConceptDomainOnly()
        {
            StringBuilder sb = new StringBuilder();

            // Declare this as a node
            sb.AppendFormat("node [fontname=\"Arial\" fontsize=10 shape=rectangle color=red] {0} [label=\"{{{0}}}\"] ", Name);

            // Draw inheritence
            // Get the base representation
            foreach(var spec in this.Specializes ?? new List<ConceptDomain>())
                sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 arrowhead=empty color=black style=\"solid\" headlabel=\"\" label=\"\"] node[color=black] {0}->{1} ", Name, spec.Name);
            // Draw specialized by
            foreach (var spec in this.Specializes ?? new List<ConceptDomain>())
                sb.AppendFormat("edge [fontname=\"Arial\" fontsize=10 arrowhead=empty color=black style=\"solid\" headlabel=\"\" label=\"\"] node[color=black] {0}->{1} ", Name, spec.Name);

            return sb.ToString();
        }

        /// <summary>
        /// Gets or sets the bound code systems or value sets
        /// </summary>
        public List<Enumeration> ContextBinding { get; set; }

        /// <summary>
        /// Identifies the concept domain(s) that this concept domain specializes
        /// </summary>
        public List<ConceptDomain> Specializes { get; set; }

        /// <summary>
        /// Get the list of concept domains that this concept domain is specialized by
        /// </summary>
        public List<ConceptDomain> SpecializedBy
        {
            get
            {
                // Scan the member of to get a list of domains that specialize this domain
                if (this.MemberOf != null)
                {
                    var specializedBy = from cd in this.MemberOf
                                        where cd.Value is ConceptDomain &&
                                        (cd.Value as ConceptDomain).Specializes != null &&
                                        (cd.Value as ConceptDomain).Specializes.Contains(this)
                                        select cd.Value;
                    List<ConceptDomain> retVal = new List<ConceptDomain>();
                    foreach (var cd in specializedBy)
                        retVal.Add(cd as ConceptDomain);
                    return retVal.Count == 0 ? null : retVal;
                }
                return null;
            }
        }
    }
}
