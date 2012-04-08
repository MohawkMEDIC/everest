using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using MARC.Everest.Interfaces;

namespace MARC.Everest.Connectors.WCF.Serialization
{
    /// <summary>
    /// Contract behavior
    /// </summary>
    public class EverestSerializerFormatAttribute : Attribute, IContractBehavior
    {

        /// <summary>
        /// Formatter for the WCF service
        /// </summary>
        public Type Formatter { get; set; }

        /// <summary>
        /// Graph aide for the WCF service
        /// </summary>
        public Type GraphAide { get; set; }

        /// <summary>
        /// True if conformance should be validated
        /// </summary>
        public bool ValidateConformance { get; set; }


        /// <summary>
        /// Create a formatter
        /// </summary>
        public IXmlStructureFormatter CreateFormatter()
        {
            var ci = this.Formatter.GetConstructor(Type.EmptyTypes);
            if (ci == null)
                throw new InvalidOperationException("Formatter must have a parameterless constructor");
            var retVal = ci.Invoke(null) as IXmlStructureFormatter;
            if (retVal == null)
                throw new InvalidOperationException("Formatter must implement IXmlStructureFormatter interface");
            if (retVal is IValidatingStructureFormatter)
                (retVal as IValidatingStructureFormatter).ValidateConformance = this.ValidateConformance;

            // Graph aide
            if (this.GraphAide != null)
            {
                ci = GraphAide.GetConstructor(Type.EmptyTypes);
                if (ci == null)
                    throw new InvalidOperationException("GraphAide must have a parmaeterless constructor");
                var ga = ci.Invoke(null) as IXmlStructureFormatter;
                if (ga == null || !(ga is IDatatypeStructureFormatter))
                    throw new InvalidOperationException("GraphAide must implement IXmlStructureFormatter and IDatatypeStructureFormatter");
                if (ga is IValidatingStructureFormatter)
                    (ga as IValidatingStructureFormatter).ValidateConformance = this.ValidateConformance;
                retVal.GraphAides.Add(ga);
            }

            return retVal;
        }

        #region IContractBehavior Members

        /// <summary>
        /// Add binding parameters
        /// </summary>
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Apply a client behavior
        /// </summary>
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            this.ReplaceSerializerOperationBehavior(contractDescription);
        }


        /// <summary>
        /// Apply dispatcher behavior
        /// </summary>
        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.DispatchRuntime dispatchRuntime)
        {
            this.ReplaceSerializerOperationBehavior(contractDescription);
        }

        /// <summary>
        /// Validate
        /// </summary>
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            foreach(var od in contractDescription.Operations)
                foreach (var md in od.Messages)
                {
                    this.ValidateMessagePartDescription(md.Body.ReturnValue);
                    foreach (var pd in md.Body.Parts)
                        this.ValidateMessagePartDescription(pd);
                    if (md.Headers.Count > 0)
                        throw new NotImplementedException("Custom message headers are not implemented as part of this behavior");
                }
        }

        #endregion

        /// <summary>
        /// Replace serializer
        /// </summary>
        private void ReplaceSerializerOperationBehavior(ContractDescription contractDescription)
        {
            foreach(var od in contractDescription.Operations)
                for (int i = 0; i < od.Behaviors.Count; i++)
                {
                    DataContractSerializerOperationBehavior dcsob = od.Behaviors[i] as DataContractSerializerOperationBehavior;
                    if (dcsob != null)
                        od.Behaviors[i] = new EverestSerializerOperationBehavior(od, this.CreateFormatter());
                }
        }

        /// <summary>
        /// Validate message part description
        /// </summary>
        /// <param name="pd"></param>
        private void ValidateMessagePartDescription(MessagePartDescription pd)
        {
            if (pd != null)
                this.ValidateIsIGraphable(pd.Type);
        }

        /// <summary>
        /// Validate is IGraphable and has parameterless constructor
        /// </summary>
        private void ValidateIsIGraphable(Type type)
        {
            var ci = type.GetConstructor(Type.EmptyTypes);
            if (ci == null)
                throw new InvalidOperationException("Type must have a parameterless constructor");
            if (!type.IsPublic)
                throw new InvalidOperationException("Type must be public");
            if (type.GetInterface(typeof(IGraphable).FullName) == null)
                throw new InvalidOperationException("Type must implement IGraphable");
        }
    }
}
