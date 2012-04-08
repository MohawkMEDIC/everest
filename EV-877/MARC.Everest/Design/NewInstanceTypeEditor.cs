/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
using System.Drawing.Design;
using System.Reflection;
using MARC.Everest.Attributes;
using System.Security.Permissions;

namespace MARC.Everest.Design
{
    /// <summary>
    /// Allows developers to use a designer to create new instances of this type
    /// </summary>
    public class NewInstanceTypeEditor : UITypeEditor
    {
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        /// <summary>
        /// Allows us to keep a reference to types in the drop down that is displayed for this editor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class CreateTypeReference
        {
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
            public Type Type { get; set; }
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            /// <param name="s"></param>
            /// <param name="t"></param>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
            public CreateTypeReference(string s, Type t) { this.Name = s; this.Type = t; }
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Name;
            }
        }

        /// <summary>
        /// Create a listbox. Since we don't know under what conditions the API source will be loading, 
        /// we can't include a direct reference to System.Windows.Forms.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private object CreateControl(Type t, object[] propertyAttributes, object value)
        {

            object retVal = null;
            // Are we creating from a type that has a cast operator to/from string?
            foreach (MethodInfo mi in t.GetMethods())
            {
                if (mi.Name == "op_Implicit" && mi.GetParameters()[0].ParameterType == typeof(System.String) && retVal == null) // Use a text box!
                {
                    retVal = AppDomain.CurrentDomain.CreateInstanceAndUnwrap("System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.TextBox");
                    retVal.GetType().GetProperty("WordWrap").SetValue(retVal, true, null);
                    retVal.GetType().GetProperty("Text").SetValue(retVal, value == null ? "" : value.ToString(), null);
                }
            }

            //object[] propertyAttributes = t.GetCustomAttributes(typeof(PropertyAttribute), true);
            if (retVal == null && (t == typeof(object) || t.IsAbstract))
            { // Handle the "Choice" case
                retVal = AppDomain.CurrentDomain.CreateInstanceAndUnwrap("System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListBox");
                object itemsHandle = retVal.GetType().GetProperty("Items").GetValue(retVal, null);
                foreach(PropertyAttribute pa in propertyAttributes)
                    itemsHandle.GetType().GetMethod("Add").Invoke(itemsHandle, new object[] { new CreateTypeReference(pa.Name, pa.Type) });
                itemsHandle.GetType().GetMethod("Add").Invoke(itemsHandle, new object[] { "Set to null" });
            }
            else if (retVal == null) // Just create or nullify
            {
                retVal = AppDomain.CurrentDomain.CreateInstanceAndUnwrap("System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListBox");
                object itemsHandle = retVal.GetType().GetProperty("Items").GetValue(retVal, null);
                itemsHandle.GetType().GetMethod("Add").Invoke(itemsHandle, new object[] { "Create new Instance" });
                itemsHandle.GetType().GetMethod("Add").Invoke(itemsHandle, new object[] { "Set to null" });
            }

            retVal.GetType().GetProperty("BorderStyle").SetValue(retVal, 0, null);
            return retVal;
        }

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            if (provider == null || provider.GetType().FullName != "System.Windows.Forms.PropertyGridInternal.PropertyDescriptorGridEntry")
                return null;

            // First off, does this point to an enum?
            //bool isEnum = 
            // What type are we creating
            //Type creatingType = provider.GetType().GetProperty("PropertyType").GetValue(provider, null) as Type;
            Type creatingType = context.PropertyDescriptor.PropertyType;
            object[] propertyAttributes = context.Instance.GetType().GetProperty(context.PropertyDescriptor.Name).GetCustomAttributes(typeof(PropertyAttribute), true);

            // Get the ListBox control
            object lb = CreateControl(creatingType, propertyAttributes, value);

            // Create a service
            Type serviceType = lb.GetType().Assembly.GetType("System.Windows.Forms.Design.IWindowsFormsEditorService");
            object service = provider.GetService(serviceType);
            

            // Drop down the control
            serviceType.GetMethod("DropDownControl").Invoke(service, new object[] { lb });

            if (lb.GetType().GetProperty("SelectedItem") != null && lb.GetType().GetProperty("SelectedItem").GetValue(lb, null) == null)
                value = null;
            else if (lb.GetType().GetProperty("SelectedItem") != null && lb.GetType().GetProperty("SelectedItem").GetValue(lb, null).ToString() == "Create new Instance")
                value = creatingType.Assembly.CreateInstance(creatingType.FullName);
            else if (lb.GetType().GetProperty("SelectedItem") != null) // We don't know what type they want.. yet
            {
                object selectedItem = lb.GetType().GetProperty("SelectedItem").GetValue(lb, null);
                if (selectedItem is string)
                    value = null;
                else
                    value = (selectedItem as CreateTypeReference).Type.Assembly.CreateInstance((selectedItem as CreateTypeReference).Type.FullName);
            }
            else if (lb.GetType().GetProperty("Text") != null && lb.GetType().GetProperty("Text").GetValue(lb, null).ToString().Length > 0) // Textual
            {
                object tmp = lb.GetType().GetProperty("Text").GetValue(lb, null);
                // Look for the ctor
                foreach (MethodInfo mi in creatingType.GetMethods(BindingFlags.Public | BindingFlags.Static))
                    if (mi.Name == "op_Implicit" && mi.GetParameters()[0].ParameterType == typeof(System.String)) // Use a text box!
                    {
                        value = mi.Invoke(null, new object[] { tmp });
                        break;
                    }
            }

            serviceType.GetMethod("CloseDropDown").Invoke(service, null);

            return value;
        }
    }
}