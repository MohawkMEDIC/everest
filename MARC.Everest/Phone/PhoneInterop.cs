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
 * Date: 10-27-2009
 */
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace MARC.Everest.Phone
{
    /// <summary>
    /// Phone interoperability functions 
    /// </summary>
    public static class PhoneInterop
    {

        /// <summary>
        /// Provides the Find() method on lists
        /// </summary>
        public static T Find<T>(this List<T> me, Predicate<T> match) 
        {
            foreach (var itm in me)
                if (match(itm))
                    return itm;
            return default(T);
        }

        /// <summary>
        /// FindAll() method on lists
        /// </summary>
        public static List<T> FindAll<T>(this List<T> me, Predicate<T> match)
        {
            List<T> retVal = new List<T>();
            foreach (var itm in me)
                if (match(itm))
                    retVal.Add(itm);
            return retVal;
        }

        /// <summary>
        /// RemoveAll() method on lists
        /// </summary>
        public static void RemoveAll<T>(this List<T> me, Predicate<T> match)
        {
            for (int i = me.Count - 1; i > 0; i--)
                if (match(me[i]))
                    me.RemoveAt(i);
        }
        /// <summary>
        /// Implementation of Exists on List
        /// </summary>
        public static bool Exists<T>(this List<T> me, Predicate<T> match)
        {
            return me.Find(match) != null;
        }

        /// <summary>
        /// Replacement for Find in array
        /// </summary>
        public static T Find<T>(this T[] me, Predicate<T> match)
        {
            foreach (var itm in me)
                if (match(itm))
                    return itm;
            return default(T);
        }

        /// <summary>
        /// FindAll() method on arrays
        /// </summary>
        public static T[] FindAll<T>(this T[] me, Predicate<T> match)
        {
            List<T> retVal = new List<T>();
            foreach (var itm in me)
                if (match(itm))
                    retVal.Add(itm);
            return retVal.ToArray();
        }

        /// <summary>
        /// True if exists
        /// </summary>
        public static bool Exists<T>(this T[] array, Predicate<T> match)
        {
            foreach (var itm in array)
                if (match(itm))
                    return true;
            return false;
        }
    }
}
