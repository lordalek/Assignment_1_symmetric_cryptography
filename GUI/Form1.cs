using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assignment_1_symmetric_cryptography;
using Models;

namespace GUI
{
    public partial class Form1 : Form
    {
        private string _binaries;

        public Form1()
        {
            InitializeComponent();
            InitializeTxtXMLPath();
        }

        private void InitializeTxtXMLPath()
        {
            this.txtXMLPath.Text =
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ??
                string.Empty;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKeyField.Text.Length < 8)
                    return;
                if (txtTextField.Text.Length <= 0)
                    return;

                var block = new Block();
                var crpytionLogic = new CryptionLogic();
                this.rTxtOutput.AppendText(DateTime.Now.ToLongTimeString() + Environment.NewLine + "Encrypt: " +
                                           this.txtTextField.Text.Trim() + Environment.NewLine + "Key: " + this.txtKeyField.Text.Trim());
                this.rTxtOutput.AppendText(Environment.NewLine);
                var outPut = crpytionLogic.Encrypt(this.txtTextField.Text.Trim(), this.txtKeyField.Text.Trim());
                this.rTxtOutput.AppendText("Binary: " + Environment.NewLine + (outPut));
                this.rTxtOutput.AppendText(Environment.NewLine);
                this.rTxtOutput.AppendText(Environment.NewLine);
                _binaries = outPut;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happend. Error: " + Environment.NewLine + ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKeyField.Text.Length < 8)
                    return;
                if (txtCipherText.Text.Length <= 0)
                    return;

                var block = new Block();

                var crpytionLogic = new CryptionLogic();
                this.rTxtOutput.AppendText(DateTime.Now.ToLongTimeString() + Environment.NewLine + "Decrypt: " +
                                           this.txtCipherText.Text.Trim() + Environment.NewLine + "Key: " + this.txtKeyField.Text.Trim());
                this.rTxtOutput.AppendText(Environment.NewLine);
                var outPut = crpytionLogic.Decrypt(this.txtCipherText.Text.Trim(), this.txtKeyField.Text.Trim());
                this.rTxtOutput.AppendText("Plaintext: " + Environment.NewLine + block.ConvertBinariesToText(outPut));
                this.rTxtOutput.AppendText(Environment.NewLine); 
                this.rTxtOutput.AppendText(Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happend. Error: " + Environment.NewLine + ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCipherText.Text.Trim().Length > 0)
                lbCiptherTextCount.Text = txtCipherText.Text.Trim().Length.ToString();
        }

        private void txtTextField_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTextField.Text.Trim().Length > 0)
                lbPlaintTextCount.Text = txtTextField.Text.Trim().Length.ToString();
        }

        private void txtKeyField_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtKeyField.Text.Trim().Length > 0)
                lb_keyCounter.Text = txtKeyField.Text.Trim().Length.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtXMLPath.Text.Length <= 0)
                    return;

                var info = new CryptionInfo()
                {
                    Key = cbKey.Checked ? txtKeyField.Text : string.Empty,
                    CipherBinary = cbCiphertext.Checked ? txtCipherText.Text : string.Empty,
                    PlaintText = cbPlaintext.Checked ? txtTextField.Text : string.Empty
                };
                Helper.SaveCryptionInfo(info, txtXMLPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save data to field.. Error: " + Environment.NewLine + ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnOpenPath_Click(object sender, EventArgs e)
        {
            try
            {
                var fileDialog = new FolderBrowserDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    txtXMLPath.Text = fileDialog.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save data to field.. Error: " + Environment.NewLine + ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtXMLPath.Text.Length < 0)
                    return;

                var info = Helper.GetCryptionInfo(txtXMLPath.Text);
                txtKeyField.Text = cbKey.Checked ? info.Key ?? string.Empty : string.Empty;
                txtCipherText.Text = cbCiphertext.Checked ? info.CipherBinary ?? string.Empty : string.Empty;
                txtTextField.Text = cbPlaintext.Checked ? info.PlaintText ?? string.Empty : string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save data to field.. Error: " + Environment.NewLine + ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCopyBinary_Click(object sender, EventArgs e)
        {
            try
            {
                txtCipherText.Text = _binaries ?? string.Empty;
                lbCiptherTextCount.Text = txtCipherText.Text.Trim().Length.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save data to field.. Error: " + Environment.NewLine + ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}