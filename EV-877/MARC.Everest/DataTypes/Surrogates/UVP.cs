/*
 * Copyright (c) 2009, Mohawk College of Applied Arts and Technology
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that 
 * the following conditions are met:
 *
 *    * Redistributions of source code must retain the above copyright notice, this list of conditions and 
 *      the following disclaimer.
 *    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions 
 *      and the following disclaimer in the documentation and/or other materials provided with the distribution.
 *    * Neither the name of the Mohawk College of Applied Arts and Technology nor the names of its contributors 
 *      may be used to endorse or promote products derived from this software without specific prior written 
 *      permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
 * PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
 * TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 * 
 * User: computc
 * Date: 9/17/2009 12:10:38 PM
 */
using System;
using System.Collections.Generic;
using System.Text;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.Exceptions;

namespace MARC.Everest.DataTypes.Surrogates
{
    /// <summary>
    /// Used by the formatter to parse a generic instance of UVP. 
    /// </summary>
    /// <remarks>
    /// Should not be used by developers
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UVP"), Serializable]
    [Obsolete("UVP is obsolete, consider using UVP<Object>", true)]
    public class UVP : UVP<Object>
    {
    }

}