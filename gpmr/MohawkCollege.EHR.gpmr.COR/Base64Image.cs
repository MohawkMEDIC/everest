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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace MohawkCollege.EHR.gpmr.COR
{
    /// <summary>
    /// An image that represents itself as a base64 encoded PNG file
    /// </summary>
    public class Base64Image 
    {

        private Image image;

        /// <summary>
        /// Get or set the image
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Image Image
        {
            get { return image; }
            set { image = value; }
        }
	
        /// <summary>
        /// Represent the image as a base64 encoded PNG string
        /// </summary>
        public override string ToString()
        {

            MemoryStream ms = new MemoryStream();
            this.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return Convert.ToBase64String(ms.GetBuffer());

        }

    }
}