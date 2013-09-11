using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assignment_1_symmetric_cryptography;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTextField.Text.Trim().Length != 8 || this.txtKeyField.Text.Trim().Length != 8)
                    return;

                var crpytionLogic = new CryptionLogic();
                this.rTxtOutput.AppendText(DateTime.Now.ToLongTimeString() + " Encrypt: " + this.txtTextField.Text.Trim() + " Key: " + this.txtKeyField.Text.Trim());
                this.rTxtOutput.AppendText(Environment.NewLine);
                this.rTxtOutput.AppendText("Output: " + crpytionLogic.Encrypt(this.txtTextField.Text.Trim(), this.txtKeyField.Text.Trim()));
                this.rTxtOutput.AppendText(Environment.NewLine);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happend. Error: " + Environment.NewLine, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTextField.Text.Trim().Length != 8 || this.txtKeyField.Text.Trim().Length != 8)
                    return;

                var crpytionLogic = new CryptionLogic();
                this.rTxtOutput.AppendText(DateTime.Now.ToLongTimeString() + " Decrypt: " + this.txtTextField.Text.Trim() + " Key: " + this.txtKeyField.Text.Trim());
                this.rTxtOutput.AppendText(Environment.NewLine);
                this.rTxtOutput.AppendText("Output: " + crpytionLogic.Decrypt(this.txtTextField.Text.Trim(), this.txtKeyField.Text.Trim()));
                this.rTxtOutput.AppendText(Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happend. Error: " + Environment.NewLine, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
