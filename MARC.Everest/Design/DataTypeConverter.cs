/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
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
 * Date: 01-09-2009
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace MARC.Everest.Design
{
    /// <summary>
    /// Summary of DataTypeConverter
    /// </summary>
    public class DataTypeConverter : TypeConverter
    {
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            Type t = context.PropertyDescriptor.PropertyType;

            if (t.GetGenericArguments().Length > 0 && t.GetGenericArguments()[0].IsEnum)
                return true;

            return base.GetStandardValuesSupported(context);
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            else
                return base.CanConvertTo(context, destinationType);

        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            Type t = context.PropertyDescriptor.PropertyType;
            foreach (MethodInfo mi in t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                if (mi.Name == "op_Implicit" && mi.GetParameters()[0].ParameterType == sourceType && sourceType.IsEnum)
                    return true;
            
            // string is always welcomed if we have an enum
            if (sourceType == typeof(string) && t.GetGenericArguments().Length > 0 && t.GetGenericArguments()[0].IsEnum)
                return true;
            else
                return base.CanConvertFrom(context, sourceType);

        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value == null || context == null || value.ToString().Length == 0 || value.ToString() == "[No Value]") return null;

            Type t = context.PropertyDescriptor.PropertyType; // , genericType = t.GetGenericTypeDefinition();
            //t = genericType.MakeGenericType(t.GetGenericArguments());
            object instance = null;
            // Convert to the value we want!
            foreach (MethodInfo mi in t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                if (mi.Name == "op_Implicit" && mi.GetParameters()[0].ParameterType == t.GetGenericArguments()[0] && t.GetGenericArguments()[0].IsEnum)
                    instance = mi.Invoke(null, new object[] { Enum.Parse(t.GetGenericArguments()[0], value.ToString()) });
            return instance;
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || context == null || value.ToString().Length == 0 ||
                value.ToString() == "[No Value]") return null;

            return value.ToString();

            //Type t = context.PropertyDescriptor.PropertyType; // , genericType = t.GetGenericTypeDefinition();
            ////t = genericType.MakeGenericType(t.GetGenericArguments());
            //object instance = null;
            //// Convert to the value we want!
            //foreach (MethodInfo mi in t.GetMethods(BindingFlags.Static | BindingFlags.Public))
            //    if (mi.Name == "op_Implicit" && mi.GetParameters()[0].ParameterType == t.GetGenericArguments()[0] && t.GetGenericArguments()[0].IsEnum)
            //        instance = mi.Invoke(null, new object[] { Enum.Parse(t.GetGenericArguments()[0], value.ToString()) });
            //return instance;
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool  GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            Type t = context.PropertyDescriptor.PropertyType;
            if (t.IsGenericType && t.GetGenericArguments()[0].IsEnum)
                return true;
            return base.GetStandardValuesExclusive(context);

        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<object> standardValues = new List<object>();
            Type t = context.PropertyDescriptor.PropertyType;
            // Now iterate 
            if (t.IsGenericType && t.GetGenericArguments()[0].IsEnum)
            {
                foreach (System.Reflection.FieldInfo fi in t.GetGenericArguments()[0].GetFields(BindingFlags.Public | BindingFlags.Static))
                    standardValues.Add(fi.GetValue(null));
                standardValues.Insert(0,"[No Value]");
            }

            return new StandardValuesCollection(standardValues);
        }
    }
}