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
    /// Represents a state within a state machine
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class State
    {
        // Drawing constants
        private const int FONT_SIZE = 12; // Font size
        private const int CD_PADDING = 4; // Padding X
        private const int ARC_SIZE = 8;

        private Documentation documentation;
        private string name;
        private Dictionary<String, State> child;
        private string parentStateName;
        private Image stateImage;

        /// <summary>
        /// Get an image representation of the state
        /// </summary>
        public Image StateImage
        {
            get
            {
                if (stateImage != null) return stateImage;
                stateImage = new Bitmap(this.name.Length * (FONT_SIZE - 4) + 2 * CD_PADDING, CD_PADDING * 2 + FONT_SIZE + 1);
                Graphics g = Graphics.FromImage(stateImage);
                Pen blk = new Pen(Color.Black, 0.1f);

                // Draw -> A state is represented by a rectangle with rounded edges
                g.DrawRectangle(blk, new Rectangle(0, 0, stateImage.Width - 1, stateImage.Height - 1));

                // Rectangles to clear
                Rectangle[] cornerRectangles = new Rectangle[] {
                    new Rectangle(stateImage.Width - ARC_SIZE, stateImage.Height - ARC_SIZE, ARC_SIZE, ARC_SIZE),
                    new Rectangle(0, stateImage.Height - ARC_SIZE, ARC_SIZE, ARC_SIZE),
                    new Rectangle(0, 0, ARC_SIZE, ARC_SIZE),
                    new Rectangle(stateImage.Width - ARC_SIZE, 0, ARC_SIZE, ARC_SIZE)
                };

                // Quadrant
                int q = 0;
                // Draw corners
                foreach(Rectangle r in cornerRectangles)
                {
                    g.FillRectangle(Brushes.White, r);
                    Rectangle dr = new Rectangle(q == 0 || q == 3 ? r.X - r.Width - 1 : r.X, q == 0 || q == 1 ? r.Y - r.Height - 1 : r.Y, r.Width * 2, r.Height * 2);
                    g.DrawArc(blk, dr, q * 90.0f, 90.0f);
                    q++;
                }

                g.DrawString(Name, new Font(FontFamily.GenericMonospace, FONT_SIZE, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, new PointF(CD_PADDING, CD_PADDING - 1));

                return stateImage;

            }
        }

        /// <summary>
        /// Get or set the parent state's name
        /// </summary>
        public string ParentStateName
        {
            get { return parentStateName; }
            set { parentStateName = value; }
        }
	
        /// <summary>
        /// Get or set all child states
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Dictionary<String, State> ChildStates
        {
            get { return child; }
            set { child = value; }
        }

        /// <summary>
        /// Get or sets the name of the state
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
	
        /// <summary>
        /// Get or set documentation about the state
        /// </summary>
        public Documentation Documentation
        {
            get { return documentation; }
            set { documentation = value; }
        }

        
    }
}