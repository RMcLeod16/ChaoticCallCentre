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
    /// Class for Form that handles Admin Password input,
    /// for use with some extra features in application
    /// </summary>
    public partial class frmAdmin : Form
    {
        /// <summary>
        /// Constructor for frmAdmin.
        /// </summary>
        public frmAdmin()
        {
            InitializeComponent();  // Exactly what it says on the tin.
        }

        // http://stackoverflow.com/questions/299086/c-sharp-how-do-i-click-a-button-by-hitting-enter-whilst-textbox-has-focus
        // http://csharphelper.com/blog/2014/12/display-a-simple-password-dialog-before-a-program-starts-in-c/
        // http://stackoverflow.com/questions/2109441/how-to-show-error-warning-message-box-in-net-how-to-customize-messagebox
        /// <summary>
        /// Event for when "Submit" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "2n00b2beg0d")              // If password matches...
            {
                this.DialogResult = DialogResult.OK;            // Send OK.
            }
            else                                                // Otherwise...
            {                                                   // Imform user of their mistake
                MessageBox.Show("ERROR: Invalid Password.", "Admin Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();                            // Return focus to Password field for retry
            }
        }
    }
}
