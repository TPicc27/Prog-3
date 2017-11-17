namespace UPVApp
{
    partial class EditAddressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.editAddressComboBox = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.editAddressLabel = new System.Windows.Forms.Label();
            this.editButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // editAddressComboBox
            // 
            this.editAddressComboBox.FormattingEnabled = true;
            this.editAddressComboBox.Location = new System.Drawing.Point(94, 34);
            this.editAddressComboBox.Name = "editAddressComboBox";
            this.editAddressComboBox.Size = new System.Drawing.Size(163, 21);
            this.editAddressComboBox.TabIndex = 0;
            this.editAddressComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.editAddressComboBox_Validating);
            this.editAddressComboBox.Validated += new System.EventHandler(this.editAddressComboBox_Validated);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // editAddressLabel
            // 
            this.editAddressLabel.AutoSize = true;
            this.editAddressLabel.Location = new System.Drawing.Point(19, 37);
            this.editAddressLabel.Name = "editAddressLabel";
            this.editAddressLabel.Size = new System.Drawing.Size(69, 13);
            this.editAddressLabel.TabIndex = 1;
            this.editAddressLabel.Text = "Edit Address:";
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(48, 70);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(170, 70);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 116);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.editAddressLabel);
            this.Controls.Add(this.editAddressComboBox);
            this.Name = "EditAddressForm";
            this.Text = "EditAddressForm";
            this.Load += new System.EventHandler(this.EditAddressForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox editAddressComboBox;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Label editAddressLabel;
    }
}