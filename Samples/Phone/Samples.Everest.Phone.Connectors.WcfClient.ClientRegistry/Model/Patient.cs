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

namespace Samples.Everest.Phone.Connectors.WcfClient.ClientRegistry.Model
{
    /// <summary>
    /// Patient class for our local model
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Patient
        /// </summary>
        public Patient()
        {
            this.Id = new List<string>();
            this.Name = new List<string>();
            this.Address = new List<string>();
            this.Telecoms = new List<string>();
        }

        /// <summary>
        /// Gets the patient's identifier
        /// </summary>
        public List<string> Id { get; set; }
        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Confidence in the match
        /// </summary>
        public int Confidence { get; set; }
        /// <summary>
        /// Gets or sets the patient name
        /// </summary>
        public List<String> Name { get; set; }
        /// <summary>
        /// Gets or sets the patient gender
        /// </summary>
        public String Gender { get; set; }
        /// <summary>
        /// Gets or sets the dob
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Gets or sets the address
        /// </summary>
        public List<String> Address { get; set; }
        /// <summary>
        /// Gets or sets the telecoms
        /// </summary>
        public List<String> Telecoms { get; set; }

    }
}
