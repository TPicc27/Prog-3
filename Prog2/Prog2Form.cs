// Program 3
// CIS 200
// Fall 2016
// Due: 11/15/2016
// By: C9022

// File: Prog2Form.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About and Exit items, an Insert menu with Address and
// Letter items, and a Report menu with List Addresses and List Parcels
// items.
// Additionally being able to save the address list into file and store it using
// serialzation in binary format. Also being able to open serializied file by deserialzing
// and displaying it. Already existing addresses can be edited and to the current 
// list of addresses using the edit address in the menu strip.  


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class Prog2Form : Form
    {
        private UserParcelView upv; // The UserParcelView
        private BinaryFormatter formatter = new BinaryFormatter(); // object for serializing address list file in binary format.
        private BinaryFormatter reader = new BinaryFormatter();  // object for deserializing address list file in binary format.
        private FileStream input;  // stream for writing file
        private FileStream output; // stream for opening to a file
        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display. A few test addresses are
        //                added to the list of addresses
        public Prog2Form()
        {
            InitializeComponent();

            upv = new UserParcelView();

            
        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NL = Environment.NewLine; // Newline shorthand

            MessageBox.Show($"Program 2{NL}By: Andrew L. Wright{NL}CIS 200{NL}Fall 2016",
                "About Program 2");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();    // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    upv.AddAddress(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        int.Parse(addressForm.ZipText)); // Use form's properties to create address
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Addresses:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Address a in upv.AddressList)
            {
                result.Append(a.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
            }

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog

            if (upv.AddressCount < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return;
            }

            letterForm = new LetterForm(upv.AddressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as upv's AddressList
                    upv.AddLetter(upv.AddressAt(letterForm.OriginAddressIndex),
                        upv.AddressAt(letterForm.DestinationAddressIndex),
                        decimal.Parse(letterForm.FixedCostText)); // Letter to be inserted
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Parcels:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Parcel p in upv.ParcelList)
            {
                result.Append(p.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
                totalCost += p.CalcCost();
            }

            result.Append(NL);
            result.Append($"Total Cost: {totalCost:C}");

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // handler for opening address list file button
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create and show dialog box enabling user to open file
            DialogResult result; // result of OpenFileDialog
            string fileName; // name 


            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified name
            } // end using

            // ensure that user clicked OK
            if (result == DialogResult.OK)
            {
                // show error if user specified invalid address list file
                if (fileName == string.Empty)
                {
                    MessageBox.Show("Invalid File Name", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); // Message Box saying error
                }

                else
                {
                    // deserialize address list file and store data in the TextBox
                    try
                    {
                        output = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                        upv = (UserParcelView)reader.Deserialize(output);

                    }
                    // handle exception if there is a problem opening the address list file
                    catch (IOException)
                    {
                        MessageBox.Show("Error opening file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error); // Message Box saying error.
                    }
                    // handle exception when there are no Serializable addreses in the address list file
                    catch (SerializationException)
                    {
                        MessageBox.Show("Error opening file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error); // Message Box saying error.
                    }
                }
            }
        }

        // handler for the Save as button
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; //create and show dialog box enabling user to save file
            string saveFile; // name of file to save data

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; // let user create file

                result = fileChooser.ShowDialog(); // retrive the result of the dialog box
                saveFile = fileChooser.FileName; // get specified file name
            } // end using

            // ensure that user clicked "OK"
            if (result == DialogResult.OK)
            {
                // show error if user specified invalid address list file.
                if (saveFile == string.Empty)
                {
                    MessageBox.Show("Invalid File Name", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); // Message Box saying error.
                }
                else
                {
                    // save file via FileStream if the file is valid.
                    try
                    {
                        // open file with right access
                        input = new FileStream(saveFile, FileMode.OpenOrCreate, FileAccess.Write);

                        formatter.Serialize(input, upv); // write Record to FilStream by serializing object.
                    }
                    // handle exception if address list file cannot be written
                    catch (IOException)
                    {
                        MessageBox.Show("Error Saving File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error); // Messabe Box saying error
                    }
                    // handle exception if address list file cannot be written
                    catch (SerializationException)
                    {
                        MessageBox.Show("Error Saving File1", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error); // Message Box saying error
                    }
                    // handle exception to see address list file exists.
                    finally
                    {
                        if (input != null) // see if file exists
                            input.Close(); // close file
                    }
                }
            }
        }

        private void addressToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditAddressForm editForm = new EditAddressForm(upv.AddressList); // Edit Address Form
            DialogResult result = editForm.ShowDialog(); // Show Edit Address Form

            if (result == DialogResult.OK) // If the Edit is clicked
            {
                int index; // index variable

                index = editForm.EditAddress; // address index put into index variable

                if (index >= 0) // index less than or equal to 0
                {

                    Address updateAddress = upv.AddressAt(index); // index of addresses goes into variable 

                    AddressForm addressForm = new AddressForm(); // Address Form 

                    addressForm.AddressName = updateAddress.Name; // Show address selected address name
                    addressForm.Address1 = updateAddress.Address1; // show address selected address 1
                    addressForm.Address2 = updateAddress.Address2; // show address selected address 2
                    addressForm.City = updateAddress.City; // show address selected address city 
                    addressForm.State = updateAddress.State; // show address selected address state
                    addressForm.ZipText = Convert.ToString(updateAddress.Zip); // show address selected address zip code

                    DialogResult updateResult = addressForm.ShowDialog(); // show selected address information in address form dialog box

                    if (updateResult == DialogResult.OK) // if OK is clicked
                    {
                        updateAddress.Name = addressForm.AddressName; // update person name
                        updateAddress.Address1 = addressForm.Address1; // update address 1
                        updateAddress.Address2 = addressForm.Address2; // update address 2
                        updateAddress.City = addressForm.City; // update city
                        updateAddress.State = addressForm.State; // update state
                        updateAddress.Zip = int.Parse(addressForm.ZipText); // update zip code
                    }

                }
            }
        }
    }
}