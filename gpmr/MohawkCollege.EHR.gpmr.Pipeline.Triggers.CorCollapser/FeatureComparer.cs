using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorCollapser
{
    /// <summary>
    /// Responsible for determining if a feature is identical
    /// </summary>
    internal class FeatureComparer : IComparer<Feature>
    {

        /// <summary>
        /// Current comparation happening
        /// </summary>
        Stack<Feature> currentComparation = new Stack<Feature>();

        #region IComparer<Feature> Members

        /// <summary>
        /// Comparison 
        /// </summary>
        public int Compare(Feature x, Feature y)
        {

            // Validate that the comparation can occur
            if (x == null && y == null) // Both null = same
                return 0;
            else if (x == null || y == null) // One is null, can't compare, inequal
                return -1;

            // Current comparation is already happening
            if (currentComparation.Contains(x))
                return 0;

            try
            {
                // Register this comparation
                currentComparation.Push(x);

                if (!x.GetType().Equals(y.GetType())) // Not even the same type of feature
                    return -1;
                else if (!x.Name.Equals(y.Name))
                    return x.Name.CompareTo(y.Name);
                else if (x == y) // The objects are identical memory address so they are identical
                    return 0;
                else // A little more advanced analysis is needed
                {
                    if (x is Class) // Already established both are same type
                        return CompareClass(x as Class, y as Class);
                    else if (x is Interaction)
                        return CompareInteraction(x as Interaction, y as Interaction);
                    else if (x is Enumeration)
                        return CompareEnumeration(x as Enumeration, y as Enumeration);
                    else if (x is SubSystem)
                        return CompareSubSystem(x as SubSystem, y as SubSystem);
                    else
                        return -1;
                }
            }
            finally
            {
                // Pop from the stack
                if(currentComparation.Pop() != x) throw new Exception("Stack Mis-Match!");
            }
        }

        /// <summary>
        /// Compare two classes
        /// </summary>
        private int CompareClass(Class x, Class y)
        {
            // First, are the base classes the same
            if ((x.BaseClass == null) ^ (y.BaseClass == null))
                return -1;
            else if (x.BaseClass != null && y.BaseClass != null &&
                x.BaseClass.ToString().CompareTo(y.BaseClass.ToString()) != 0)
                return x.BaseClass.ToString().CompareTo(y.BaseClass.ToString());
            else if (x.TypeParameters != null && y.TypeParameters != null &&
                x.TypeParameters.Count.CompareTo(y.TypeParameters.Count) != 0) // Ensure the type parameters are equal
                return x.TypeParameters.Count.CompareTo(y.TypeParameters.Count);
            else if (!x.Content.Count.Equals(y.Content.Count))
                return x.Content.Count.CompareTo(y.Content.Count);
            else
            {
                // Now compare the members (x in y)
                foreach (ClassContent cc in x.Content)
                    if (y.Content.Find(a => CompareContent(cc, a) == 0) == null)
                        return -1;
                return 0;
            }
        }

        /// <summary>
        /// Class content comparer
        /// </summary>
        private int CompareContent(ClassContent x, ClassContent y)
        {
            // Name must match
            if (!x.Name.Equals(y.Name))
                return x.Name.CompareTo(y.Name);
            else if (!x.GetType().Equals(y.GetType())) // must be the same type of content
                return -1;
            else if (x is Property) // We have already asserted that x and y must be the same type
                return CompareProperty(x as Property, y as Property);
            else if (x is Choice) // Compare choice
                return CompareChoice(x as Choice, y as Choice);
            return 0;
        }

        /// <summary>
        /// Compare two choices
        /// </summary>
        private int CompareChoice(Choice x, Choice y)
        {
            // The choice count must be the same
            if (!x.Content.Count.Equals(y.Content.Count))
                return x.Content.Count.CompareTo(y.Content.Count);
            // Each of the choices must be the same
            // Now compare the members (x in y)
            foreach (ClassContent cc in x.Content)
                if (y.Content.Find(a => CompareContent(cc, a) == 0) == null)
                    return -1;

            int annotCompare = CompareAnnotations(x.Annotations, y.Annotations);
            if (annotCompare != 0) return annotCompare;

            return 0;
        }

        /// <summary>
        /// Compare two properties
        /// </summary>
        private int CompareProperty(Property x, Property y)
        {
         
            // The property Conformance must be the same
            if (!x.Conformance.Equals(y.Conformance))
                return x.Conformance.CompareTo(y.Conformance);
            else if ((x.FixedValue == null) ^ (y.FixedValue == null))
                return -1;
            else if (x.FixedValue != null && y.FixedValue != null &&
                !x.FixedValue.Equals(y.FixedValue)) // Fixed value must be equal
                return x.FixedValue.CompareTo(y.FixedValue);
            else if (!x.MaxOccurs.Equals(y.MaxOccurs)) // Max occurs must match
                return x.MaxOccurs.CompareTo(y.MaxOccurs);
            else if (!x.MinOccurs.Equals(y.MinOccurs)) // Min occurs must match
                return x.MinOccurs.CompareTo(y.MinOccurs);
            else if (!x.PropertyType.Equals(y.PropertyType)) // Property type must match
                return x.PropertyType.CompareTo(y.PropertyType);
            else if ((x.SupplierDomain == null) ^ (y.SupplierDomain == null))
                return -1;
            else if (x.SupplierDomain != null && y.SupplierDomain != null &&
                !x.SupplierDomain.Equals(y.SupplierDomain)) // Supplier domains must match
                return CompareEnumeration(x.SupplierDomain, y.SupplierDomain);
            else if ((x.AlternateTraversalNames != null) ^ (y.AlternateTraversalNames != null))
                return -1;
            else if (x.AlternateTraversalNames != null && y.AlternateTraversalNames != null &&
                !x.AlternateTraversalNames.Count.Equals(y.AlternateTraversalNames.Count))
                return x.AlternateTraversalNames.Count.CompareTo(y.AlternateTraversalNames.Count);

            int trCompare = CompareTypeReference(x.Type, y.Type);

            if (trCompare != 0) return trCompare;

            // Compare annotations
            int annotCompare = CompareAnnotations(x.Annotations, y.Annotations);
            if (annotCompare != 0) return annotCompare;

            return 0;

        }

        /// <summary>
        /// Compare annotations, compares types of annotations rather than annotations
        /// </summary>
        private int CompareAnnotations(List<Annotation> x, List<Annotation> y)
        {
            foreach (var annotation in x ?? new List<Annotation>())
                if (!y.Exists(o => o.GetType() == annotation.GetType())) // must have the same type of annotation
                    return 1;
            foreach (var annotation in y ?? new List<Annotation>())
                if (!x.Exists(o => o.GetType() == annotation.GetType()))
                    return -1;
            return 0;
        }

        /// <summary>
        /// Compare two type references
        /// </summary>
        internal int CompareTypeReference(TypeReference x, TypeReference y)
        {

            int typeCompare = Compare(x.Class, y.Class);
            if (typeCompare != 0) return typeCompare; // Types don't match

            // Core data type reference
            if (x.CoreDatatypeName != null && y.CoreDatatypeName != null &&
                !x.CoreDatatypeName.Equals(y.CoreDatatypeName))
                return x.CoreDatatypeName.CompareTo(y.CoreDatatypeName);
            else if ((x.CoreDatatypeName != null) ^ (y.CoreDatatypeName != null))
                return -1;

            // Generic supplier
            if (x.GenericSupplier == null && y.GenericSupplier == null)
                return 0;
            else if (x.GenericSupplier != null && y.GenericSupplier != null &&
                !x.GenericSupplier.Count.Equals(y.GenericSupplier.Count)) // Count of generic suppliers must match
                return x.GenericSupplier.Count.CompareTo(y.GenericSupplier.Count);
            else if ((x.GenericSupplier == null) ^ (y.GenericSupplier == null)) // One generic supplier is null the other is not
                return -1; 

            // Binding must match
            for (int i = 0; i < x.GenericSupplier.Count; i++)
            {
                typeCompare = CompareTypeReference(x.GenericSupplier[i], y.GenericSupplier[i]);
                if (typeCompare != null) return typeCompare;
            }

            return 0;
        }
        /// <summary>
        /// Compare two interactions
        /// </summary>
        private int CompareInteraction(Interaction x, Interaction y)
        {
            if (x.TriggerEvent.CompareTo(y.TriggerEvent) != 0) // Compare trigger events
                return x.TriggerEvent.CompareTo(y.TriggerEvent);
            else if (x.Responses.Count.CompareTo(y.Responses.Count) != null) // not the same number of responses
                return x.Responses.Count.CompareTo(y.Responses.Count);
            else if (x.MessageType.ToString().CompareTo(y.MessageType.ToString()) != 0)
                return x.MessageType.ToString().CompareTo(y.MessageType.ToString());
            return 0;
        }

        /// <summary>
        /// Compare enumerations
        /// </summary>
        private int CompareEnumeration(Enumeration x, Enumeration y)
        {
            return -1;
        }

        /// <summary>
        /// Compare sub-systems
        /// </summary>
        private int CompareSubSystem(SubSystem x, SubSystem y)
        {
            if (x.OwnedClasses.Count.CompareTo(y.OwnedClasses.Count) != 0)
                return x.OwnedClasses.Count.CompareTo(y.OwnedClasses.Count);
            else
            {
                foreach (Class c in x.OwnedClasses) // search the owned classes x in y
                    if (y.OwnedClasses.Find(a => CompareClass(c, a) == 0) == null)
                        return -1;
                foreach (Class c in y.OwnedClasses) // Search the owned classes y in x
                    if (x.OwnedClasses.Find(a => CompareClass(c, a) == 0) == null)
                        return 1;
                return 0;
            }
        }

        #endregion
    }
}
