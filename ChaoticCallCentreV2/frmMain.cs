using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ChaoticCallCentreV2
{
    /// <summary>
    /// Class for the main Form for ChaoticCallCentreV2
    /// </summary>
    public partial class frmMain : Form
    {
        #region vars

        /// <summary>
        /// Used to check if a new record is being dealt with or not
        /// </summary>
        bool isNewRecord = false;

        /// <summary>
        /// Integer array holding the PhoneRecordID of each record in tblPhoneRecords
        /// </summary>
        int[] RecordIDs = null;

        /// <summary>
        /// Used to track which record is currently being worked with.
        /// </summary>
        int currentRecord = 0;      //start at first record

        /// <summary>
        /// An Integer used to track the total amount of records being dealt with.
        /// </summary>
        int totalRecords;           //placeholder for total amount of records

        /// <summary>
        /// Instantiation of data class to interface with database
        /// </summary>
        data _data = new data();

        #endregion vars

        #region init

        public frmMain()
        {
            InitializeComponent();     // Exactly what it says on the tin

            myDebug.WriteLine("Program ChaoticCallCentreV2 Started!");

            List<string> cboOptions = new List<string>();       //list of options for Sort and Search Option Combobox
            cboOptions.Add("First Name");
            cboOptions.Add("Last Name");
            cboOptions.Add("Date of Birth");
            cboOptions.Add("Description");
            cboOptions.Add("Carrier");

            cboSort.DataSource = cboOptions;                    //Set DataSources for the comboboxes
            cboSearch.BindingContext = new BindingContext();
            cboSearch.DataSource = cboOptions;

            GetRecordsIDs();
            GetCarriers();
            LoadRecord();
        }

        #endregion init

        #region events

        /// <summary>
        /// Event for when the "File -> New Record" Tool Strip Menu Item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewRecord();        // Call NewRecord() to setup the Form for input of a new Record
        }

        /// <summary>
        /// Event for when the "File -> Save Record" Tool Strip Menu Item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveRecord();       // Call SaveRecord() to save the currently displayed Record to the database
        }

        /// <summary>
        /// Event for when the "File -> Exit Program" Tool Strip Menu Item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();      // Call ExitProgram() to exit the program
        }

        // http://csharphelper.com/blog/2014/12/display-a-simple-password-dialog-before-a-program-starts-in-c/
        /// <summary>
        /// Event for when the "Other -> Admin Features" Tool Strip Menu Item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void activateAdminFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activateAdminFeaturesToolStripMenuItem.Checked)             // If the "Admin Features" is already checked...
            {
                activateAdminFeaturesToolStripMenuItem.Checked = false;     // Uncheck it
                btnCarrierEdit.Visible = false;                             // Make admin-only controls invisible and inaccessible
            }
            else                                                            // Otherwise...
            {
                frmAdmin passform = new frmAdmin();                         // Instantiate new frmAdmin for Admin password input

                if (passform.ShowDialog() != DialogResult.OK)               // Display the form, and if the above form does not return OK...
                {
                    myDebug.WriteLine("User cancelled admin password input.");  // It means the user cancelled password input
                }
                else                                                        // If it does return OK...
                {
                    activateAdminFeaturesToolStripMenuItem.Checked = true;  // "Admin Features" is checked.
                    btnCarrierEdit.Visible = true;                          // Make admin-only controls visible and usable
                }
            }
        }

        /// <summary>
        /// Event for when the "New Record" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewRecord();        // Call NewRecord() to setup the Form for input of a new Record
        }

        /// <summary>
        /// Event for when the "Save Record" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecord();       // Call SaveRecord() to save the currently displayed Record to the database
        }

        /// <summary>
        /// Event for when the "Exit Program" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitProgram();      // Call ExitProgram() to exit the program
        }

        /// <summary>
        /// Event for when the "First" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentRecord = 0;      //set current record to first one
            LoadRecord();           //and load it up on screen
        }

        /// <summary>
        /// Event for when the "Previous" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentRecord > 0)  //if not at the first record
            {
                currentRecord--;    //set current Record to the previous one
                LoadRecord();       //and load it up on screen
            }
        }

        /// <summary>
        /// Event for when the "Next" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentRecord < totalRecords - 1)   //if current record is not the alst one
            {
                currentRecord++;    //set current record to the next one
                LoadRecord();       //and load it up on screen
            }
        }

        /// <summary>
        /// Event for when the "Last" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            currentRecord = totalRecords - 1;   //set current record to last record
            LoadRecord();                       //and load it up on screen
        }

        /// <summary>
        /// Event for when the "Search" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSort_Click(object sender, EventArgs e)
        {
            SortRecords();          // Call SortRecords() to sort the records by a certain field specified by the user
            currentRecord = 0;      // Set current record to first one
            LoadRecord();           // and load it up on screen
        }

        /// <summary>
        /// Event for "Reset Sort" Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetSort_Click(object sender, EventArgs e)
        {
            GetRecordsIDs();        // Call record array refreshing method, resetting custom sorting in the process
            currentRecord = 0;      // Set current record to first one
            LoadRecord();           // and load it up on screen
        }

        /// <summary>
        /// Event for when the "Search" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchRecords();        // Call SearchRecords() to find the first record matching the criteria specified by the user
            LoadRecord();           // Load the record  on screen
        }

        // http://stackoverflow.com/questions/1944909/c-sharp-winforms-position-a-form-relative-to-another-form
        /// <summary>
        /// Event for when the "Carrier Edit" buton is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCarrierEdit_Click(object sender, EventArgs e)
        {
            frmCarrier carrier = new frmCarrier();                  // Instantiate new frmCarrier to edit the Carriers

            if (carrier.ShowDialog() != DialogResult.OK)            // Display the Form, and if not return OK...
            {
                myDebug.WriteLine("User exited frmCarrier.");       // The user has exited the frmCarrier form.
            }
        }

        /// <summary>
        /// Event for when the "Delete Record" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Query to "delete" selected Record (client only wants it hidden, not actually destroyed)
            string query = "UPDATE tblPhoneRecord SET IsDeleted=true WHERE PhoneRecordID = " + RecordIDs[currentRecord];
            _data.SendNonQuery(query);      // Send query to database

            currentRecord--;                // Decrement currenntRecord by one
            GetRecordsIDs();                // Refresh the RecordIDs after removing one
            LoadRecord();                   // Load selected record
        }

        #endregion events

        #region methods

        /// <summary>
        /// Method to get the PhoneRecordIDs of all the Records in the database,
        /// which are used when cycling through records
        /// </summary>
        private void GetRecordsIDs()
        {
            // Query to get all PhoneRecordIDs from database
            string query = "SELECT PhoneRecordID FROM tblPhoneRecord WHERE IsDeleted = false";
            _data.GetDataSet(query);                                                // Get data from database
            totalRecords = _data.ds.Tables[0].Rows.Count;                           // Get total amount of records for totalRecords

            myDebug.WriteLine("Total Records Counted: " + totalRecords);

            RecordIDs = new int[totalRecords];                                      // Create new int array for RecordIDs
            for (int i = 0; i < totalRecords; i++)                                  // For each element in the array...
            {
                RecordIDs[i] = (int)_data.ds.Tables[0].Rows[i]["PhoneRecordID"];    // Add the corresponding RecordID to the array
                myDebug.WriteLine("Record at Index " + i + " has PhoneRecordID of " + (int)_data.ds.Tables[0].Rows[i]["PhoneRecordID"]);
            }
        }

        /// <summary>
        /// Method to get the list of Carriers from the database and bind them to the respective form controls
        /// </summary>
        private void GetCarriers()
        {
            // Query to get the Carrier datafrom the database
            string query = "SELECT * FROM tblCarrier WHERE IsDeleted = false";
            _data.GetDataSet(query);                                    // Get data from database

            cboCarrier.DataSource = _data.ds.Tables[0];                 // Bind the data to the Carrier selection ComboBox
            cboCarrier.DisplayMember = "CarrierName";
            cboCarrier.ValueMember = "CarrierID";

            // http://stackoverflow.com/questions/4344366/multiple-combo-boxes-with-the-same-data-source-c
            cboCarrierSearch.BindingContext = new BindingContext();     // Bind the data to the Carrier Search ComboBox too
            cboCarrierSearch.DataSource = _data.ds.Tables[0];
            cboCarrierSearch.DisplayMember = "CarrierName";
            cboCarrierSearch.ValueMember = "CarrierID";

        }

        /// <summary>
        /// Method to setup the Form for a New Record
        /// </summary>
        private void NewRecord()
        {
            txtFirstName.Text = "";             // Set on screen controls' data to default
            txtLastName.Text = "";
            dtpDOB.Value = DateTime.Now.Date;
            txtDesc.Text = "";
            cboCarrier.SelectedIndex = 0;

            currentRecord = totalRecords;       // Set current record to a new one, one after all the other records
            isNewRecord = true;                 // Is a new record, so a new entry will be needed to store it, so set this to true
        }

        /// <summary>
        /// Method to load a Record corresponding the currently selected Record
        /// </summary>
        private void LoadRecord()
        {
            try
            {
                // Query to get the PhoneRecordID that corresponds to the currently selected Record
                string query = "SELECT * FROM tblPhoneRecord WHERE PhoneRecordID = " + RecordIDs[currentRecord];

                _data.GetDataReader(query);                                     // Read data from database

                txtFirstName.Text = _data.dbReader["FirstName"].ToString();     // Load data to form controls
                txtLastName.Text = _data.dbReader["LastName"].ToString();
                dtpDOB.Value = (DateTime)_data.dbReader["RecordDate"];
                txtDesc.Text = _data.dbReader["Description"].ToString();
                cboCarrier.SelectedValue = (int)_data.dbReader["CarrierID"];

                _data.CloseConnection();                                        // Manually close connection left open by data.GetDataReader()
            }
            catch (IndexOutOfRangeException ex)
            {
                myDebug.WriteLine(ex.Message + " This should be OK, means no records, so creating new one.");
                NewRecord();                                                    // Call NewRecord() if there are none
            }
        }

        /// <summary>
        /// Method to save the Record data currently displayed in the form to the database
        /// </summary>
        private void SaveRecord()
        {
            if (isNewRecord)                                                // If it is a New Record...
            {
                // Query to insert new record into the database
                string query = "INSERT INTO tblPhoneRecord (FirstName, LastName, RecordDate, Description, CarrierID) VALUES (\"" + txtFirstName.Text + "\", \"" + txtLastName.Text + "\", \"" + dtpDOB.Value.Date + "\", \"" + txtDesc.Text + "\", " + cboCarrier.SelectedValue + " )";

                _data.SendNonQuery(query);                          // Send data to database
                GetRecordsIDs();                                    // Refresh the RecordIDs after adding the new one
            }
            else                                                    // If it is an existing Record...
            {
                // Query to update existing record with latest data
                string query = "UPDATE tblPhoneRecord " +
                    "SET FirstName=\"" + txtFirstName.Text + "\", LastName=\"" + txtLastName.Text + "\", RecordDate=\"" + dtpDOB.Value.Date + "\", Description=\"" + txtDesc.Text + "\", CarrierID=" + cboCarrier.SelectedValue +
                    " WHERE PhoneRecordID = " + RecordIDs[currentRecord];

                _data.SendNonQuery(query);                          // Send data to database
            }
            myDebug.WriteLine("cboCarrier Selection is: " + cboCarrier.SelectedValue);
            LoadRecord();                                           // Load the currently selected Record
        }

        /// <summary>
        /// A generic method to create some temporary arrays containing all records' values of a certain property,
        /// for use when soting and searching.
        /// </summary>
        /// <param name="cboIndex">The selected index of the combobox that was used for property selection.</param>
        /// <returns>An Object array containing a specified property's data from every record</returns>
        private Object[] MakeTempArray(int cboIndex)
        {
            Object[] temp = new Object[totalRecords];       // Create new temporary Object array

            myDebug.WriteLine("cboIndex = " + cboIndex);

            // Build temp array of just the property we need
            for (int i = 0; i < totalRecords; i++)          // For each Record...
            {
                // oshit, can sort and search through killed records...
                // string query = "SELECT * FROM tblPhoneRecord WHERE PhoneRecordID = " + RecordIDs[i];
                // ...but not anymore!
                // Query to get the Record corresponding to the for loop's current iteration
                string query = "SELECT * FROM tblPhoneRecord WHERE IsDeleted = false AND PhoneRecordID = " + RecordIDs[i];
                _data.GetDataReader(query);     // Read data from database

                switch (cboIndex)               // Check the index of the specified ComboBox
                {
                    case 0:                                                             // If First Name option is selected...
                        temp[i] = _data.dbReader["FirstName"].ToString().ToLower();     // add all FirstNames to temp array
                        break;

                    case 1:                                                             // If Last Name option is selected...
                        temp[i] = _data.dbReader["LastName"].ToString().ToLower();      // add all LastNames to temp array
                        break;

                    case 2:                                                             // If Date of Birth option is selected...
                        temp[i] = (DateTime)_data.dbReader["RecordDate"];               // add all RecordDates to temp array
                        break;

                    case 3:                                                             // If Description option is selected...
                        temp[i] = _data.dbReader["Description"].ToString().ToLower();   // add all Descriptions to temp array
                        break;

                    case 4:                                                             // If Carrier option is selected...
                        temp[i] = _data.dbReader["CarrierID"];                          // add all CarrierIDs to temp array
                        break;
                }

                _data.CloseConnection();                                // Manually close connection left open by data.GetDataReader()

                myDebug.WriteLine("temp[" + i + "] = " + temp[i]);      // Formatted debug printing for easy logging of variables during for loop
            }
            return temp;                                                // Return the generated Object array
        }

        /// <summary>
        /// Method to sort the records in the natural order of the property specified by the user.
        /// </summary>
        private void SortRecords()
        {
            Object[] temp = MakeTempArray(cboSort.SelectedIndex);               // Make Temp Sort Arrays based on Sort ComboBox selection

            Array.Sort(temp, RecordIDs);                                        // Sort the main RecordIDs array using the Temp array as a key
            myDebug.WriteLine("Records sorted by " + cboSort.SelectedValue);
        }

        /// <summary>
        /// Method to search for the first record that matches the data specified by the user.
        /// </summary>
        private void SearchRecords()
        {
            Object[] temp = MakeTempArray(cboSearch.SelectedIndex);                     // Make Temp Search Arrays based on Search ComboBox selection
            int resultIndex;                                                            // Index of first resulting record from search

            switch (cboSearch.SelectedIndex)                                            // Check which field user wants to search by
            {
                case 2:                                                                 // If Date of Birth option is selected...
                    resultIndex = Array.IndexOf(temp, dtpDOBSearch.Value.Date);         // Get the index of the first array value matching the specified Date of Birth
                    break;

                case 4:                                                                 // If Carrier option is selected...
                    myDebug.WriteLine("Selected CarrierID --> " + cboCarrierSearch.SelectedValue);
                    resultIndex = Array.IndexOf(temp, cboCarrierSearch.SelectedValue);  // Get the index of the first array value matching the value of the specified Carrier    
                    break;

                default:                                                                // Otherwise...
                    resultIndex = Array.IndexOf(temp, txtSearch.Text.ToLower());        // Get the index of the first array value matching the text string specified
                    break;
            }

            if (resultIndex < 0)                            // If search result is not a valid index...
            {                                               // Inform user that the search item was not found
                MessageBox.Show("Search item not found.", "Search in " + cboSearch.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else                                            // Otherwise...
            {
                currentRecord = resultIndex;                // Set current record to the record containing the found search value
            }
        }

        /// <summary>
        /// Method that handles Exiting the program, can make sure certain things get done before the program is terminated.
        /// </summary>
        private void ExitProgram()
        {
            myDebug.WriteLine("Exiting Program...");    // Just a debug log message toinform that the program is about to be exited
            Application.Exit();                         // Exit the program
        }

        #endregion methods
    }
}
