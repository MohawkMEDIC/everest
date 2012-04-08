using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta.Annotations;
using MohawkCollege.EHR.gpmr.COR;

namespace MohawkCollege.EHR.gpmr.Pipeline.Triggers.CorDelta
{
    /// <summary>
    /// Render functoids for deki
    /// </summary>
    public class DekiRenderFunctoids
    {

        /// <summary>
        /// Find annotations
        /// </summary>
        public static List<T> FindAnnotations<T>(Feature f) where T : ConstraintAnnotation, new()
        {
            List<Annotation> sca = f.Annotations.FindAll(o => o is T);
            List<T> retVal = new List<T>(sca.Count);
            foreach (T sc in sca)
                retVal.Add(sc);
            return retVal;
        }

        /// <summary>
        /// Supported in annotation
        /// </summary>
        public static object SupportedInAnnotation(object parm)
        {
            Feature f = parm as Feature;
            var retVal = new List<SupportedConstraintAnnotation>();
            if (f == null)
                return null;
            else
                retVal.Add(new SupportedConstraintAnnotation()
                {
                    RealmCode = GetRealmCode(f)
                });
                    

            retVal.AddRange(FindAnnotations<SupportedConstraintAnnotation>(parm as Feature));
            return retVal;
        }

        /// <summary>
        /// Constraint annotations
        /// </summary>
        public static object ConformanceConstraintAnnotation(object parm)
        {
            Property f = parm as Property;
            var retVal = new List<ConformanceConstraintAnnotation>();

            if (f == null)
            {
                Choice c = parm as Choice;
                retVal.Add(new ConformanceConstraintAnnotation()
                {
                    NewValue = c.Conformance,
                    RealmCode = GetRealmCode(c)
                });

            }
            else
            {
                retVal.Add(new ConformanceConstraintAnnotation()
                {
                    NewValue = f.Conformance,
                    RealmCode = GetRealmCode(f)
                });
            }
            retVal.AddRange(FindAnnotations<ConformanceConstraintAnnotation>(parm as Feature));
            return retVal;
        }

        /// <summary>
        /// Constraint annotations
        /// </summary>
        public static object DataTypeConstraintAnnotation(object parm)
        {
            Property f = parm as Property;
            var retVal = new List<DatatypeChangeConstraintAnnotation>();

            if (f == null)
            {
                return null;
            }
            else
            {
                retVal.Add(new DatatypeChangeConstraintAnnotation()
                {
                    NewValue = f.Type,
                    RealmCode = GetRealmCode(f)
                });
            }
            retVal.AddRange(FindAnnotations<DatatypeChangeConstraintAnnotation>(parm as Feature));
            return retVal;
        }
        /// <summary>
        /// Cardinality constraint 
        /// </summary>
        public static object CardinalityConstraintAnnotation(object parm)
        {
            Property f = parm as Property;
            var retVal = new List<CardinalityConstraintAnnotation>();

            if (f == null)
            {
                Choice c = parm as Choice;
                retVal.Add(new CardinalityConstraintAnnotation()
                {
                    MaxOccurs = c.MaxOccurs,
                    MinOccurs = c.MinOccurs,
                    RealmCode = GetRealmCode(c)
                });
                
            }
            else
            {
                retVal.Add(new CardinalityConstraintAnnotation()
                {
                    MaxOccurs = f.MaxOccurs,
                    MinOccurs = f.MinOccurs,
                    RealmCode = GetRealmCode(f)
                });
            }
            retVal.AddRange(FindAnnotations<CardinalityConstraintAnnotation>(parm as Feature));
            return retVal;
        }

        /// <summary>
        /// Get realm code
        /// </summary>
        private static string GetRealmCode(Feature f)
        {
            
            Property p = f as Property;
            Class cc = f as Class;
            if (p == null && cc == null)
            {
                Choice c = f as Choice;
                cc = c.Container as Class;
            }
            else if(cc == null)
            {
                // Container data
                cc = p.Container as Class;
                if (cc == null)
                    cc = (p.Container as Choice).Container as Class;
            }

            if (cc == null)
                return String.Empty;
            return cc.ContainerName.Substring(cc.ContainerName.Length - 2);
        }

        /// <summary>
        /// Cardinality constraint grouped by values
        /// </summary>
        public static object CardinalityConstraintAnnotationGroupedByValue(object parm)
        {
            Feature f = parm as Feature;
            var cca = FindAnnotations<CardinalityConstraintAnnotation>(f);
            var ccg = from CardinalityConstraintAnnotation annot in cca
                      group annot by new { annot.MinOccurs, annot.MaxOccurs } into g
                      select new { MinMax = g.Key, Annotations = g };

            List<CardinalityConstraintAnnotation> retVal = new List<CardinalityConstraintAnnotation>();
            foreach (var kv in ccg)
            {
                StringBuilder realmString = new StringBuilder();
                foreach (var realm in kv.Annotations)
                    realmString.AppendFormat("{0}, ", realm.RealmCode);
                realmString.Remove(realmString.Length - 2, 2);
                retVal.Add(new CardinalityConstraintAnnotation()
                {
                    MinOccurs = kv.MinMax.MinOccurs,
                    MaxOccurs = kv.MinMax.MaxOccurs,
                    RealmCode = realmString.ToString()
                });
            }
            return retVal;

        }

        /// <summary>
        /// Constraint annotation
        /// </summary>
        public static object ConformanceConstraintAnnotationGroupedByConformance(object parm)
        {
            Feature f = parm as Feature;
            var cca = FindAnnotations<ConformanceConstraintAnnotation>(parm as Feature);
            var ccg = from ConformanceConstraintAnnotation annot in cca
                      group annot by annot.NewValue into g
                      select new { Conformance = g.Key, Annotations = g };

            List<ConformanceConstraintAnnotation> retVal = new List<ConformanceConstraintAnnotation>();
            foreach (var kv in ccg)
            {
                StringBuilder realmString = new StringBuilder();
                foreach (var realm in kv.Annotations)
                    realmString.AppendFormat("{0}, ", realm.RealmCode);
                realmString.Remove(realmString.Length - 2, 2);
                retVal.Add(new ConformanceConstraintAnnotation()
                {
                    NewValue = kv.Conformance,
                    RealmCode = realmString.ToString()
                });
            }
            return retVal;
        }

        /// <summary>
        /// Documentation constraint annotation
        /// </summary>
        public static object DocumentationConstraintAnnotation(object parm)
        {
            return FindAnnotations<AnnotationConstraintAnnotation>(parm as Feature);
        }
        /// <summary>
        /// Get the fixed value constraints
        /// </summary>
        public static object FixedValueConstraintAnnotation(object parm)
        {
            Property f = parm as Property;
            var retVal = new List<FixedValueConstraintAnnotation>();

            if (f != null && (!String.IsNullOrEmpty(f.FixedValue)))
            {
                retVal.Add(new FixedValueConstraintAnnotation()
                {
                    NewValue = f.FixedValue,
                    RealmCode = GetRealmCode(f)
                });
            }

            retVal.AddRange(FindAnnotations<FixedValueConstraintAnnotation>(parm as Feature));

            if (retVal.Count == 0)
                return null;

            return retVal;
        }
        /// <summary>
        /// Get the default value constraints
        /// </summary>
        public static object DefaultValueConstraintAnnotation(object parm)
        {
            Property f = parm as Property;
            var retVal = new List<DefaultValueConstraintAnnotation>();

            if (f != null && (!String.IsNullOrEmpty(f.DefaultValue)))
            {
                retVal.Insert(0, new DefaultValueConstraintAnnotation()
                {
                    NewValue = f.DefaultValue,
                    RealmCode = GetRealmCode(f)
                });
            }

            retVal.AddRange(FindAnnotations<DefaultValueConstraintAnnotation>(parm as Feature));

            
            if (retVal.Count == 0)
                return null;

            return retVal;
        }

        /// <summary>
        /// Get the supplier strength constraints
        /// </summary>
        public static object SupplierStrengthConstraintAnnotation(object parm)
        {
            Property f = parm as Property;
            var retVal = new List<SupplierStrengthConstraintAnnotation>();

            if (f != null && f.SupplierStrength.HasValue)
            {
                retVal.Add(new SupplierStrengthConstraintAnnotation()
                {
                    NewValue = f.SupplierStrength.Value,
                    RealmCode = GetRealmCode(f)
                });
            }

            retVal.AddRange(FindAnnotations<SupplierStrengthConstraintAnnotation>(parm as Feature));
            
            if (retVal.Count == 0)
                return null;

            return retVal;
        }

        /// <summary>
        /// Gets the supplier domain constraints
        /// </summary>
        public static object SupplierDomainConstraintAnnotation(object parm)
        {
            Property f = parm as Property;
            var retVal = new List<SupplierDomainConstraintAnnotation>();

            if (f != null && f.SupplierDomain != null)
            {
                retVal.Add(new SupplierDomainConstraintAnnotation()
                {
                    NewValue = (object)f.SupplierDomain ?? (object)f.SupplierDomain.Name,
                    RealmCode = GetRealmCode(f)
                });
            }

            retVal.AddRange(FindAnnotations<SupplierDomainConstraintAnnotation>(parm as Feature));

            if (retVal.Count == 0)
                return null;

            return retVal;
        }
    }
}
