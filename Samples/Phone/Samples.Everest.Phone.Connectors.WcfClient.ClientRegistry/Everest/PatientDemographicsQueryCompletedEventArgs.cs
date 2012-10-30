using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using Samples.Everest.Phone.Connectors.WcfClient.ClientRegistry.Model;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.RMIM.UV.NE2008.Vocabulary;

namespace Samples.Everest.Phone.Connectors.WcfClient.ClientRegistry.Everest
{
    /// <summary>
    /// An event argument signaling that the patient demographics query is complete
    /// </summary>
    public class PatientDemographicsQueryCompletedEventArgs : EventArgs
    {
        
        /// <summary>
        /// Creates a new instance of the completed args
        /// </summary>
        public PatientDemographicsQueryCompletedEventArgs(System.Collections.Generic.List<MARC.Everest.RMIM.UV.NE2008.MFMI_MT700711UV01.Subject1<MARC.Everest.RMIM.UV.NE2008.PRPA_MT201310UV02.Patient, object>> results)
        {
            this.Results = new List<Patient>(results.Count);
            foreach (var item in results)
            {
                var regRole = item.RegistrationEvent.Subject1.registeredRole;
                var role = regRole.PatientEntityChoiceSubject as MARC.Everest.RMIM.UV.NE2008.PRPA_MT201310UV02.Person;
                
                Patient p = new Patient();

                // Identifiers
                foreach (var ii in regRole.Id)
                    p.Id.Add(String.Format("{0} - {1}", ii.Extension, ii.AssigningAuthorityName ?? ii.Root));

                // Names
                foreach (var nam in role.Name)
                {
                    p.Name.Add(String.Format("{0} ({1})", nam.ToString("{FAM}, {GIV}"), Util.ToWireFormat(nam.Use)));
                    if (nam.Use != null && !nam.Use.IsEmpty && nam.Use.Find(o => o == EntityNameUse.Legal) != null) ;
                    p.DisplayName = nam.ToString("{FAM}, {GIV}").Trim() ;
                }

                // Gender
                if (role.AdministrativeGenderCode != null &&
                    !role.AdministrativeGenderCode.IsNull)
                    p.Gender = ((AdministrativeGender)role.AdministrativeGenderCode).ToString();

                // DOB
                if (role.BirthTime != null &&
                    !role.BirthTime.IsNull)
                    p.DateOfBirth = role.BirthTime.DateValue;

                if (regRole.SubjectOf1.Count > 0 && regRole.SubjectOf1[0].QueryMatchObservation != null &&
                    regRole.SubjectOf1[0].QueryMatchObservation.NullFlavor == null)
                    p.Confidence = (int)(INT)regRole.SubjectOf1[0].QueryMatchObservation.Value;
                else
                    p.Confidence = 100;

                this.Results.Add(p);
            }
        }

        /// <summary>
        /// No results
        /// </summary>
        public PatientDemographicsQueryCompletedEventArgs()
        {
            this.Results = new List<Patient>();
        }

        /// <summary>
        /// Results
        /// </summary>
        public List<Patient> Results { get; private set; }
    }
}
