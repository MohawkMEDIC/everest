using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace MARC.Everest.Connectors.WCF.Serialization
{
    
    /// <summary>
    /// Everest serializer operation behavior
    /// </summary> 
    public class EverestSerializerOperationBehavior : DataContractSerializerOperationBehavior
    {

        // The formatter
        private IXmlStructureFormatter m_formatter;

        /// <summary>
        /// Constructs a new instance of the Everest Serializer operation behavior
        /// </summary>
        public EverestSerializerOperationBehavior(OperationDescription operation, IXmlStructureFormatter formatter) : base(operation)
        {
            // TODO: Complete member initialization
            this.m_formatter = formatter;
        }

        /// <summary>
        /// Create a serializer
        /// </summary>
        public override System.Runtime.Serialization.XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
        {
            return new XmlSerializerSurrogate(this.m_formatter);
        }

        /// <summary>
        /// Create the serializer
        /// </summary>
        public override System.Runtime.Serialization.XmlObjectSerializer CreateSerializer(Type type, System.Xml.XmlDictionaryString name, System.Xml.XmlDictionaryString ns, IList<Type> knownTypes)
        {
            return new XmlSerializerSurrogate(this.m_formatter);
        }
    }
}
