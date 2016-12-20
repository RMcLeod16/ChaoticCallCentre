using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticCallCentreV2
{
    /// <summary>
    /// Class for Form that handles editing Carriers
    /// </summary>
    public partial class frmCarrier : Form
    {
        #region vars

        /// <summary>
        /// Instantiation of data class to interface with database
        /// </summary>
        data _data = new data();

        #endregion vars

        #region init

        /// <summary>
        /// Constructor for fmrCarrier
        /// </summary>
        public frmCarrier()
        {
            InitializeComponent();     // Exactly what it says on the tin
        }

        #endregion init

        #region events

        /// <summary>
        /// Event for when frmCarrier is Activated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCarrier_Activated(object sender, EventArgs e)
        {
            GetCarriers();  // Call GetCarriers() on load to refresh list of carriers.
        }

        /// <summary>
        /// Events for when the lbxCarrier ListBox is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxCarrier_Click(object sender, EventArgs e)
        {
            LoadCarrier();  // Call LoadCarrier() to load the data for the Carrier that is selected.
        }

        /// <summary>
        /// Event for when the "Save Carrier" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCarrier_Click(object sender, EventArgs e)
        {
            if (txtCarrierName.Text != "")          // Quick check to make sure new value for existing Carrier name is not blank
            {
                // Query to update selected Carrier's existing name with new name
                string query = "UPDATE tblCarrier SET CarrierName=\"" + txtCarrierName.Text + "\" WHERE CarrierID = " + lbxCarrier.SelectedValue;
                _data.SendNonQuery(query);          // Send query to database

                GetCarriers();                      // Refresh the Carrier list to reflect the changes.
            }
        }

        /// <summary>
        /// Event for when the "Delete Carrier" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCarrier_Click(object sender, EventArgs e)
        {
            // Query to "delete" selected Carrier (client only wants it hidden, not actually destroyed)
            string query = "UPDATE tblCarrier SET IsDeleted=true WHERE CarrierID = " + lbxCarrier.SelectedValue;
            _data.SendNonQuery(query);          // Send query to database

            GetCarriers();                      // Refresh the Carrier list to reflect the changes.
        }

        /// <summary>
        /// Event for when "Add New Carrier" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewCarrier_Click(object sender, EventArgs e)
        {
            if (txtNewCarrier.Text != "")               // Quick check to make sure value for new Carrier name is not blank
            {
                // http://stackoverflow.com/questions/3581570/what-is-wrong-with-this-sql-query
                // query to check whether or not the subnitted Name exists
                string query = "SELECT (IIf(EXISTS (SELECT 1 FROM tblCarrier WHERE IsDeleted = false AND CarrierName = \"" + txtNewCarrier.Text + "\"),1,0)) as A from tblCarrier";

                _data.GetDataReader(query);                         // Query database to check whether Carrier already exists
                myDebug.WriteLine("Carrier name already exists? -> " + Convert.ToBoolean(_data.dbReader[0]));    // Debug logging stuff
                bool found = Convert.ToBoolean(_data.dbReader[0]);  // Grab result of previous query
                _data.CloseConnection();                            // Manually close DB connection

                if (found)                                          // If the Carier already exists...
                {
                    lblAdded.Text = "Already Exists!";              // Inform user that it already exists.
                }
                else                                                // Otherwise...
                {
                    // Query to insert new carrier into database
                    query = "INSERT INTO tblCarrier (CarrierName) VALUES (\"" + txtNewCarrier.Text + "\")";
                    _data.SendNonQuery(query);                      // Send query to database

                    lblAdded.Text = "Added!";                       // Inform user that the New Carrier has been added to the database.

                    GetCarriers();                                  // Refresh the Carrier list to reflect the changes.
                }
            }
        }

        #endregion events

        #region methods

        /// <summary>
        /// Method that grabs the Carriers from the database and displays them in the Form's ListBox
        /// </summary>
        private void GetCarriers()
        {
            // Query to get the Carrier data from database
            string query = "SELECT * FROM tblCarrier WHERE IsDeleted = false";
            _data.GetDataSet(query);                        // Get data from database
            lbxCarrier.DataSource = _data.ds.Tables[0];     // Bind data to ListBox
            lbxCarrier.DisplayMember = "CarrierName";
            lbxCarrier.ValueMember = "CarrierID";
            LoadCarrier();                                  // Call LoadCarrier() to load the first Carrier's data to Form
        }

        /// <summary>
        /// Method to load the currently selected Carrier's data to the Form controls
        /// </summary>
        private void LoadCarrier()
        {
            // Load the currently selected Carrier's Name into the respective TextBox
            txtCarrierName.Text = (string)_data.ds.Tables[0].Rows[lbxCarrier.SelectedIndex]["CarrierName"];
        }

        #endregion methods
    }
}
