using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Samples.Everest.Phone.Connectors.WcfClient.ClientRegistry.Everest;

namespace Samples.Everest.Phone.Connectors.WcfClient.ClientRegistry
{
    public partial class MainPage : PhoneApplicationPage
    {
        // PDC
        private PatientDemographicsConsumer m_patientDemographicsConsumer;

        /// <summary>
        /// Default ctor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            this.m_patientDemographicsConsumer = new PatientDemographicsConsumer(this.Dispatcher);
            this.m_patientDemographicsConsumer.QueryCompleted += new EventHandler<PatientDemographicsQueryCompletedEventArgs>(m_patientDemographicsConsumer_QueryCompleted);
        }

        /// <summary>
        /// Query has completed
        /// </summary>
        void m_patientDemographicsConsumer_QueryCompleted(object sender, PatientDemographicsQueryCompletedEventArgs e)
        {
            this.gridSearch.Opacity = 0;
            this.gridSearch.IsHitTestVisible = false;
            this.waitBar.IsEnabled = false;

            this.lstResult.DataContext = e.Results;
            
        }

        /// <summary>
        /// Text has been input
        /// </summary>
        private void txtPatientName_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtPatientName.Text))
                return;

            // Search
            this.gridSearch.Opacity = 0.5;
            this.gridSearch.IsHitTestVisible = true;
            this.waitBar.IsEnabled = true;
            // This hides the keyboard
            this.waitBar.Focus();

            // Now call
            this.m_patientDemographicsConsumer.Search(this.txtPatientName.Text);
        }


     
    }
}