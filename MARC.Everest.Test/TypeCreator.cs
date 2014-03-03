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
using System.Reflection;
using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Connectors;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MARC.Everest.Test
{
    /// <summary>
    /// Creates instances of objects filling in properties on the object recursively.
    /// </summary>
    public class TypeCreator
    {
        private Type m_Type;

        private object m_Result;

        private Stack<Type> m_TypeStack;

        private Random m_Random;

        private DatatypeFormatter m_formatter = new DatatypeFormatter();

        private DateTime m_startTime;

        /// <summary>
        /// Initializes a new instance of the type creator for the specified type.
        /// This constructor is private because TypeCreators are cached and accessed using the static method GetCreator.
        /// </summary>
        /// <param name="t">The type that this type creator should create.</param>
        private TypeCreator(Type t)
        {
            m_Type = t;
            m_Random = new Random();
        }


        /// <summary>
        /// Get a pre-created instance
        /// </summary>
        public object GetPrecreatedInstance()
        {
            // JF - Load Pre-Created Instance
            Stream instanceStream = null;
            try
            {
                instanceStream = typeof(XMLGenerator).Assembly.GetManifestResourceStream(String.Format("MARC.Everest.Test.Created.{0}", m_Type.FullName.Replace(".", "") + ".bin"));
                if (instanceStream != null)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return bf.Deserialize(instanceStream);
                }
            }
            catch { }
            finally
            {
                if (instanceStream != null)
                    instanceStream.Close();
            }
            return null;

        }

        /// <summary>
        /// Save an instance
        /// </summary>
        public void SaveInstance()
        {
            String fName = Path.Combine(@"C:\temp", m_Type.FullName.Replace(".", "")) + ".bin";
            Stream wStream = null;
            try
            {
                wStream = File.Create(fName);
                Tracer.Trace(fName);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(wStream, m_Result);
            }
            catch { }
            finally
            {
                if (wStream != null)
                    wStream.Close();
            }
        }

        /// <summary>
        /// Returns an instance of the specified type. This might be a cached instance.
        /// </summary>
        /// <returns>An object of the specified type, or null if one could not be created.</returns>
        public object CreateInstance()
        {
            if (null != m_Result) //Check for cached copy.
                return m_Result;

            CreateInstanceInternal();

            return m_Result;
        }

        /// <summary>
        /// Does the creation of the type.
        /// </summary>
        private void CreateInstanceInternal()
        {

            //m_Result = GetPrecreatedInstance();
            //if (m_Result != null)
            //    return;

            //Check for valid cache dictionaries
            if (null == m_EmptyConstructorCache)
                m_EmptyConstructorCache = new Dictionary<Type, ConstructorInfo>();

            if (null == m_MandatoryConstructorCache)
                m_MandatoryConstructorCache = new Dictionary<Type, ConstructorInfo>();

            //Check the cache for empty constructors
            if (!m_EmptyConstructorCache.ContainsKey(m_Type))
                m_EmptyConstructorCache.Add(m_Type, m_Type.GetConstructor(Type.EmptyTypes));

            //Initialize the type stack
            m_TypeStack = new Stack<Type>();

            m_startTime = DateTime.Now;

            //Create the type
            m_Result = CreateType(m_Type, new UseContext(m_Type, (PropertyInfo)null));

            //SaveInstance();

            //Clear the type stack.
            m_TypeStack.Clear();

        }

        /// <summary>
        /// Recursive createtype method.
        /// </summary>
        /// <param name="t">The type to create</param>
        /// <param name="context">context, if it exists.</param>
        /// <returns></returns>
        private object CreateType(Type t, UseContext context)
        {
            
            if (m_TypeStack.Contains(t)) //Prevent Recursion
                return null;

            if ((context.Indent.Length > 2 * 10 &&
                DateTime.Now.Subtract(m_startTime).TotalSeconds > 10) &&
                context.PropertyAttribute.Conformance != MARC.Everest.Attributes.PropertyAttribute.AttributeConformanceType.Mandatory &&
                context.PropertyAttribute.Conformance != MARC.Everest.Attributes.PropertyAttribute.AttributeConformanceType.Populated ||
                context.PropertyAttribute != null &&
                context.PropertyAttribute.Conformance == MARC.Everest.Attributes.PropertyAttribute.AttributeConformanceType.Optional &&
                !this.GenerateOptional)
                return null;

            if(context.PropertyInfo != null && context.PropertyInfo.Name != null)
                Tracer.Trace(context.PropertyInfo.Name, context);

            if (SimpleTypeCreator.SimpleTypeCreators.ContainsKey(t)) //Simple creator exists
                return SimpleTypeCreator.SimpleTypeCreators[t].Invoke(context);

            if (IsAssignableFromGenericType(t))
                if (SimpleTypeCreator.SimpleTypeCreators.ContainsKey(GetAssignableGenericType(t)))
                    return SimpleTypeCreator.SimpleTypeCreators[GetAssignableGenericType(t)].Invoke(context);
                else if (IsEnumeration(GetAssignableGenericType(t)))
                    return AssignRandomEnumerationValue(GetAssignableGenericType(t));


            if (IsICollection(t))
                //if (SimpleTypeCreator.SimpleTypeCreators.ContainsKey(GetCollectionType(t)))
                return CreateCollection(t, GetCollectionType(t), context);

            if (IsChoice(t, context))
            {
                UseContext rootContext = context;
                while (rootContext.ParentContext != null)
                    rootContext = rootContext.ParentContext;

                if (context.PropertyAttribute.Type != null && !context.PropertyAttribute.Type.IsAbstract &&
                     (context.PropertyAttribute.InteractionOwner == rootContext.OwnerType || context.PropertyAttribute.InteractionOwner == null))
                    return CreateType(context.PropertyAttribute.Type, context);
                else
                    foreach (var prop in context.PropertyAttributes)
                        if (prop.Type != null && !prop.Type.IsAbstract &&
                                (prop.InteractionOwner == rootContext.OwnerType || prop.InteractionOwner == null))
                        {
                            context.PropertyAttribute = prop;
                            return CreateType(prop.Type, context);
                        }
            }
            m_TypeStack.Push(t);

            //Check if it is a generic type from which
            object result = CreateComplexObject(t, context);


            m_TypeStack.Pop();

            return result;

        }

        private object AssignRandomEnumerationValue(Type enumType)
        {
            Array arr = Enum.GetValues(enumType);

            if (arr.Rank > 0)
            {

                int lower = arr.GetLowerBound(0);
                int upper = arr.GetUpperBound(0);

                return arr.GetValue(arr.Length - 1);
            }
            else
            {
                return 0;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private bool IsEnumeration(Type enumType)
        {
            return enumType.IsEnum;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private bool IsChoice(Type t, UseContext context)
        {
            return null != context && null != context.PropertyInfo && null != context.PropertyAttribute && (t == typeof(object) || t.IsAbstract);
        }

        private object CreateCollection(Type collectionType, Type elementType, UseContext context)
        {
            int count = 1;

            if (null != context && null != context.PropertyAttribute)
            {
                //if (context.PropertyAttribute.MaxOccurs > context.PropertyAttribute.MinOccurs)
                //{
                //    count = m_Random.Next(context.PropertyAttribute.MinOccurs, context.PropertyAttribute.MaxOccurs + 1);
               // }
                //else
                //{
                if(context.PropertyAttribute.MinOccurs != 0)
                    count = context.PropertyAttribute.MinOccurs;
                //}
            }

            return CreateCollection(collectionType, elementType, count, context);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private object CreateCollection(Type collectionType, Type elementType, int count, UseContext context)
        {
            //Tracer.Trace(string.Format("Creating collection of type {0}", collectionType.GetCSharpGenericName()), context);

            var ctor = collectionType.GetConstructor(Type.EmptyTypes);

            if (null == ctor)
                return null;

            var addmethod = collectionType.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);

            if (null == addmethod)
                return null;

            object o = ctor.Invoke(null);

            if (null != context &&
                null != context.PropertyAttribute)
                if (context.PropertyAttribute.MaxOccurs == 0)
                    return o; //No Items permitted, ignore the previous number

            for (int i = 0; i < count; i++)
            {
                var t = Util.FromWireFormat(CreateType(elementType, context), elementType);
                if (t != null)
                    addmethod.Invoke(o, new object[]{
                    t
                });
            }
            return o;
        }

        private bool IsAssignableFromGenericType(Type t)
        {
            return GetAssignableGenericType(t) != null;
        }

        private bool IsICollection(Type t)
        {
            return GetCollectionType(t) != null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private Type GetCollectionType(Type t)
        {
            var collectioninterfacetype = typeof(ICollection<>);

            foreach (var iface in t.GetInterfaces())
            {
                if (iface.IsGenericType)
                    if (iface.GetGenericTypeDefinition() == collectioninterfacetype)
                        return iface.GetGenericArguments()[0];
            }

            return null;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private Type GetAssignableGenericType(Type t)
        {
            if (!t.IsGenericType)
                return null;

            var gargs = t.GetGenericArguments();

            for (int i = 0; i < gargs.Length; i++)
                if (t.IsAssignableFromEx(gargs[i]))
                    return gargs[i];

            return null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private object CreateComplexObject(Type t, UseContext context)
        {
            //Tracer.Trace(string.Format("CreateComplexObject of type {0}", t.GetCSharpGenericName()), context);

            if (context.PropertyAttribute != null)
                if (context.PropertyAttribute.Conformance == MARC.Everest.Attributes.PropertyAttribute.AttributeConformanceType.Optional)
                    if (context.PropertyAttributes.Count == 1) //Not a choice
                        if (null != context.PropertyInfo && context.PropertyInfo.PropertyType == t)
                            return null;

            ConstructorInfo ctor = null;

            object result = null;
              
            // Missing CMET?
            if (context.PropertyAttribute != null && context.PropertyAttribute.Type == null &&
                context.PropertyAttributes.Count == 1 && t == typeof(System.Object))
                return null;

            //if (m_MandatoryConstructorCache.ContainsKey(t))
            //    ctor = m_MandatoryConstructorCache[t];
            if (m_EmptyConstructorCache.ContainsKey(t))
                ctor = m_EmptyConstructorCache[t];

            if (null == ctor)
                ctor = t.GetConstructor(Type.EmptyTypes);

            if (null == ctor) //No Constructors available for the given type
                return null;

            var ctorparms = ctor.GetParameters();

            if (ctorparms.Length > 0)
            {
                List<object> ctorparmvalues = new List<object>();

                foreach (var ctorparm in ctorparms)
                {
                    ctorparmvalues.Add(CreateType(ctorparm.ParameterType,
                        new UseContext(t, ctorparm) { ParentContext = context }
                        ));
                }

                result = ctor.Invoke(ctorparmvalues.ToArray());
            }
            else
            {
                result = ctor.Invoke(null);
            }

            PopulateObject(result, t, context);

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private object ConvertTypeIfNecessary(object o, Type requestedType)
        {
            if (null == o) //Pass it on
                return null;

            Type t = o.GetType();

            if (t == requestedType)
                return o;

            if (requestedType.IsAssignableFrom(t))
                return o;

            if (requestedType.IsAssignableFromEx(t))
            {
                //Find the operator to convert
                List<MethodInfo> methods = new List<MethodInfo>();

                methods.AddRange(t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic));
                methods.AddRange(requestedType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic));

                foreach (var method in methods)
                {
                    if (!method.IsHideBySig || !method.IsSpecialName || !method.IsStatic)
                        continue;

                    if (method.ReturnType != requestedType)
                        continue;

                    var mparms = method.GetParameters();

                    if (mparms.Length != 1)
                        continue;

                    foreach (var mparm in mparms)
                        if (mparm.ParameterType == t)
                            return method.Invoke(null, new object[] { o });
                }

                //This is an odd case
                return null;
            }
            else
            {
                return null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void PopulateObject(object o, Type t, UseContext context)
        {

            var properties = t.GetProperties();

            // JF - Get the DT r1 formatter 
            var allowedProperties = o is HXIT ? m_formatter.GetSupportedProperties(t) : properties;

            foreach (var prop in properties)
            {
                //if (prop.Name.ToLower().StartsWith("attentionline"))
                //    System.Diagnostics.Debugger.Break();
                if (!Array.Exists<PropertyInfo>(allowedProperties, p => p.Name == prop.Name))
                    continue;

                // Don't bother with anything from infrastructure root
                if (prop.Name == "TypeId" || prop.Name == "RealmCode")
                    continue;

                if (!prop.CanWrite) //Not writable, don't even bother
                    continue;

                if (prop.GetIndexParameters().Length > 0)
                    continue;

              
                if (prop.GetValue(o, null) != null)
                    if (GetCollectionType(prop.PropertyType) == null)
                        continue;
                    else
                    {
                        prop.SetValue(o,
                            CreateCollection(prop.PropertyType,
                            GetCollectionType(prop.PropertyType),
                            new UseContext(o.GetType(), prop) { ParentContext = context }), null);
                        continue;
                    }


                if (prop.Name == "NullFlavor")
                    continue;

                if (prop.Name == "NullFlavor")
                    continue;

                try
                {
                    prop.SetValue(o,
                        ConvertTypeIfNecessary(CreateType(prop.PropertyType,
                        new UseContext(o.GetType(), prop) { ParentContext = context }), prop.PropertyType),
                        null);
                }
                catch
                {
                    //System.Diagnostics.Debugger.Break();
                }
            }
        }



        #region Static Controller, Do NOT Modify
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        private static Dictionary<Type, TypeCreator> m_TypeCreatorCache;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t")]
        public static TypeCreator GetCreator(Type t)
        {
            if (null == m_TypeCreatorCache)
                m_TypeCreatorCache = new Dictionary<Type, TypeCreator>();

            if (m_TypeCreatorCache.ContainsKey(t))
                return m_TypeCreatorCache[t];

            m_TypeCreatorCache.Add(t, new TypeCreator(t));

            return m_TypeCreatorCache[t];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        private static Dictionary<Type, ConstructorInfo> m_EmptyConstructorCache;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        private static Dictionary<Type, ConstructorInfo> m_MandatoryConstructorCache;
        #endregion

        /// <summary>
        /// When true generates optional component
        /// </summary>
        public bool GenerateOptional { get; set; }
    }
}
