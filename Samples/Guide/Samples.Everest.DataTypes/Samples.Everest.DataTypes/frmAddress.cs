using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MARC.Everest.DataTypes;

namespace DataTypeExplorer
{
    public partial class frmAddress : Form
    {
        public frmAddress()
        {
            InitializeComponent();
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            //Create a new list of ADXP(Address Parts)
            List<ADXP> adxpList = new List<ADXP>();

            //Populate the adxpList
            if (!String.IsNullOrEmpty(txtBuildingNumber.Text))
                adxpList.Add(new ADXP(txtBuildingNumber.Text, AddressPartType.BuildingNumber));
            if (!String.IsNullOrEmpty(txtStreetNameBase.Text))
                adxpList.Add(new ADXP(txtStreetNameBase.Text, AddressPartType.StreetNameBase));
            if (!String.IsNullOrEmpty(txtStreetType.Text))
                adxpList.Add(new ADXP(txtStreetType.Text, AddressPartType.StreetType));
            if (!String.IsNullOrEmpty(txtDirection.Text))
                adxpList.Add(new ADXP(txtDirection.Text, AddressPartType.Direction));
            if (!String.IsNullOrEmpty(txtCity.Text))
                adxpList.Add(new ADXP(txtCity.Text, AddressPartType.City));
            if (!String.IsNullOrEmpty(txtState.Text))
                adxpList.Add(new ADXP(txtState.Text, AddressPartType.State));
            if (!String.IsNullOrEmpty(txtCountry.Text))
                adxpList.Add(new ADXP(txtCountry.Text, AddressPartType.Country));
            if (!String.IsNullOrEmpty(txtPostalCode.Text))
                adxpList.Add(new ADXP(txtPostalCode.Text, AddressPartType.PostalCode));
            
            //Create a address out of the Address Parts.
            //NOTE: The PostalAddressUse can be changed to format the address in a variety of ways.
            AD ad = new AD(PostalAddressUse.HomeAddress, adxpList);

            // Get the AD in XML format using the FormatterHelper class.
            string adXmlStr = FormatterHelper.FormatDataType(ad, "AD");

            // Display the generated xml.
            txtXml.Text = adXmlStr;
        }
    }
}
