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
using MARC.Everest.Attributes;
using MARC.Everest.Interfaces;

namespace MARC.Everest.DataTypes.Interfaces
{

    /// <summary>
    /// Identifies type of uncertainty distributions
    /// </summary>
    [Structure(Name = "UncertaintyType", CodeSystem = "2.16.840.1.113883.5.1020", StructureType = StructureAttribute.StructureAttributeType.ConceptDomain)]
    public enum QuantityUncertaintyType
    {
        /// <summary>
        /// The The uniform distribution assigns a constant probability over the entire interval 
        /// of possible outcomes
        /// </summary>
        [Enumeration(Value = "U")]
        Uniform,
        /// <summary>
        /// This is the well-known bell-shaped normal distribution
        /// </summary>
        [Enumeration(Value = "N")]
        Normal,
        /// <summary>
        /// The logarithmic normal distribution is used to transform skewed random variable X into a normally
        /// distributed random variable
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LogNormal")]
        [Enumeration(Value = "LN")]
        LogNormal,
        /// <summary>
        /// The gamma-distribution used for data that is skewed and bounded to the right
        /// </summary>
        [Enumeration(Value = "G")]
        Gamma,
        /// <summary>
        /// Used for data that describes extinction
        /// </summary>
        [Enumeration(Value = "E")]
        Exponential,
        /// <summary>
        /// Used to describe the sum of squares of random variables that occurs when a varience is estimated
        /// </summary>
        [Enumeration(Value = "X2")]
        X2,
        /// <summary>
        /// Used to describe the quotient of a normal random variable and the square root of a X^2 random variable
        /// </summary>
        [Enumeration(Value = "T")]
        TDistribution,
        /// <summary>
        /// Used to describe the quotient of two X^2 random variables
        /// </summary>
        [Enumeration(Value = "F")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "F")]
        F,
        /// <summary>
        /// The beta distribution is used for data that is bounded on both sides and may or may not be skewed.
        /// </summary>
        [Enumeration(Value = "B")]
        Beta
    }

    /// <summary>
    /// Non-genericized interface that dictates a class as a quantity
    /// </summary>
    [TypeMap(Name = "QTY")]
    public interface IQuantity : IAny, IPrimitiveDataValue
    {

        /// <summary>
        /// The expression that represents the quantity
        /// </summary>
        IEncapsulatedData Expression { get; set;  }

        /// <summary>
        /// Get or set the original text from which the text was derived
        /// </summary>
        IEncapsulatedData OriginalText { get; set;  }

        /// <summary>
        /// Get or set the uncertaint of the quantity using a distribution function
        /// </summary>
        IQuantity Uncertainty { get; set;  }

        /// <summary>
        /// Get or set the uncertainty type
        /// </summary>
        QuantityUncertaintyType? UncertaintyType { get; set; }

        /// <summary>
        /// Uncertainty range
        /// </summary>
        IVL<IQuantity> UncertainRange { get; set; }

        /// <summary>
        /// Convert the fractional number to a double
        /// </summary>
        double ToDouble();
    }
}