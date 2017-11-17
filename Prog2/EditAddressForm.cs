// Program 3
// CIS 200
// Fall 2016
// 11/15/2016
// Grading ID: C9022

// EditAddressForm.cs is a form that uses a combo box to find all the 
// address in the already created address list.  The user should be able 
// to select a specific address and they can click edit to take them to the
// the address form for that specified address selected.  The user can 
// also click cancel if they do not want to proceed.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class EditAddressForm : Form
    {
        private List<Address> addressList; // list of addresses used to fill combo box.

        // Precondition: List of address is populated with available addresses.
        // Postcondition: The form GUI is prepared for display.
        public EditAddressForm(List<Address> addresses)
        {
            InitializeComponent();

            addressList = addresses;
        }

        // Precondition: None
        // Postconditon: The list of address is populated to the 
        //               edit address combo box.
        private void EditAddressForm_Load(object sender, EventArgs e)
        {
            foreach (Address a in addressList) // loop through address list
                editAddressComboBox.Items.Add(a.Name); // add to combo box.
        }

        internal int EditAddress
        {
            // Precondition: User has selected the Edit Address combo box.
            // Postcondition: The index of the selected address is returned.
            get
            {

                return editAddressComboBox.SelectedIndex; // return combo box.

            }
            // Precondition: -1 <= value < addressList.Count
            // Postcondition: The specified index is selected in edit address combo box.
            set
            {
                if ((value >= -1) && (value < addressList.Count))
                    editAddressComboBox.SelectedIndex = value;
                else
                    throw new ArgumentOutOfRangeException("EditAddressIndex", value,
                                                         "Index must be invalid");

            }

        }

        // Precondition: Focus shifted to Edit Address Combo Box.
        // Postcondition: If the index is invalid, then focus remains on the the Combo box.
        private void editAddressComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (editAddressComboBox.SelectedIndex == -1) // if index is -1
            {
                e.Cancel = true; // keep focus on combo box

                errorProvider1.SetError(editAddressComboBox, "Please select an address!"); // Error Message

            }
        }

        // Precondition: Edit Address Combo Box has succeeded.
        // Postcondition: The error message is cleared and the focus has changed.
        private void editAddressComboBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(editAddressComboBox, ""); //Error Message
        }

        // Precondition: User clicks the edit button.
        // Postcondition: The form is validated, and the edit Address is dismissed.
        private void editButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
                this.DialogResult = DialogResult.OK;  // ok result.
        }

        // Precondition: User clicks the cancel button
        // Postcondition: The edit form is dismissed.
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // cancel result
            this.Close();
        }
    }
}
