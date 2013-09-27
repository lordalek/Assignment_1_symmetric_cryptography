namespace GUI
{
    partial class Form1
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
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.txtTextField = new System.Windows.Forms.TextBox();
            this.rTxtOutput = new System.Windows.Forms.RichTextBox();
            this.txtKeyField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnCopyBinary = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCipherText = new System.Windows.Forms.TextBox();
            this.lbPlaintTextCount = new System.Windows.Forms.Label();
            this.lbCiptherTextCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_keyCounter = new System.Windows.Forms.Label();
            this.cbKey = new System.Windows.Forms.CheckBox();
            this.cbPlaintext = new System.Windows.Forms.CheckBox();
            this.cbCiphertext = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtXMLPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnOpenPath = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(590, 176);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // txtTextField
            // 
            this.txtTextField.Location = new System.Drawing.Point(63, 88);
            this.txtTextField.Name = "txtTextField";
            this.txtTextField.Size = new System.Drawing.Size(609, 20);
            this.txtTextField.TabIndex = 1;
            this.txtTextField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTextField_KeyUp);
            // 
            // rTxtOutput
            // 
            this.rTxtOutput.Location = new System.Drawing.Point(53, 178);
            this.rTxtOutput.Name = "rTxtOutput";
            this.rTxtOutput.Size = new System.Drawing.Size(531, 476);
            this.rTxtOutput.TabIndex = 2;
            this.rTxtOutput.Text = "";
            // 
            // txtKeyField
            // 
            this.txtKeyField.Location = new System.Drawing.Point(56, 152);
            this.txtKeyField.MaxLength = 8;
            this.txtKeyField.Name = "txtKeyField";
            this.txtKeyField.Size = new System.Drawing.Size(67, 20);
            this.txtKeyField.TabIndex = 4;
            this.txtKeyField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKeyField_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Plaintext";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Key";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(590, 205);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 7;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.txtDecrypt_Click);
            // 
            // btnCopyBinary
            // 
            this.btnCopyBinary.Location = new System.Drawing.Point(590, 234);
            this.btnCopyBinary.Name = "btnCopyBinary";
            this.btnCopyBinary.Size = new System.Drawing.Size(75, 59);
            this.btnCopyBinary.TabIndex = 8;
            this.btnCopyBinary.Text = "Copy binary to Ciphertext";
            this.btnCopyBinary.UseVisualStyleBackColor = true;
            this.btnCopyBinary.Click += new System.EventHandler(this.btnCopyBinary_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ciphertext";
            // 
            // txtCipherText
            // 
            this.txtCipherText.Location = new System.Drawing.Point(63, 114);
            this.txtCipherText.Name = "txtCipherText";
            this.txtCipherText.Size = new System.Drawing.Size(609, 20);
            this.txtCipherText.TabIndex = 10;
            this.txtCipherText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // lbPlaintTextCount
            // 
            this.lbPlaintTextCount.AutoSize = true;
            this.lbPlaintTextCount.Location = new System.Drawing.Point(678, 95);
            this.lbPlaintTextCount.Name = "lbPlaintTextCount";
            this.lbPlaintTextCount.Size = new System.Drawing.Size(0, 13);
            this.lbPlaintTextCount.TabIndex = 11;
            // 
            // lbCiptherTextCount
            // 
            this.lbCiptherTextCount.AutoSize = true;
            this.lbCiptherTextCount.Location = new System.Drawing.Point(678, 117);
            this.lbCiptherTextCount.Name = "lbCiptherTextCount";
            this.lbCiptherTextCount.Size = new System.Drawing.Size(0, 13);
            this.lbCiptherTextCount.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Insert 8 letters";
            // 
            // lb_keyCounter
            // 
            this.lb_keyCounter.AutoSize = true;
            this.lb_keyCounter.Location = new System.Drawing.Point(129, 155);
            this.lb_keyCounter.Name = "lb_keyCounter";
            this.lb_keyCounter.Size = new System.Drawing.Size(0, 13);
            this.lb_keyCounter.TabIndex = 14;
            // 
            // cbKey
            // 
            this.cbKey.AutoSize = true;
            this.cbKey.Checked = true;
            this.cbKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKey.Location = new System.Drawing.Point(6, 19);
            this.cbKey.Name = "cbKey";
            this.cbKey.Size = new System.Drawing.Size(44, 17);
            this.cbKey.TabIndex = 15;
            this.cbKey.Text = "Key";
            this.cbKey.UseVisualStyleBackColor = true;
            // 
            // cbPlaintext
            // 
            this.cbPlaintext.AutoSize = true;
            this.cbPlaintext.Checked = true;
            this.cbPlaintext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPlaintext.Location = new System.Drawing.Point(58, 19);
            this.cbPlaintext.Name = "cbPlaintext";
            this.cbPlaintext.Size = new System.Drawing.Size(66, 17);
            this.cbPlaintext.TabIndex = 16;
            this.cbPlaintext.Text = "Plaintext";
            this.cbPlaintext.UseVisualStyleBackColor = true;
            // 
            // cbCiphertext
            // 
            this.cbCiphertext.AutoSize = true;
            this.cbCiphertext.Checked = true;
            this.cbCiphertext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCiphertext.Location = new System.Drawing.Point(130, 19);
            this.cbCiphertext.Name = "cbCiphertext";
            this.cbCiphertext.Size = new System.Drawing.Size(73, 17);
            this.cbCiphertext.TabIndex = 17;
            this.cbCiphertext.Text = "Ciphertext";
            this.cbCiphertext.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbKey);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.cbCiphertext);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.cbPlaintext);
            this.groupBox1.Location = new System.Drawing.Point(63, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 47);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data fields to save/load";
            // 
            // txtXMLPath
            // 
            this.txtXMLPath.Location = new System.Drawing.Point(63, 62);
            this.txtXMLPath.Name = "txtXMLPath";
            this.txtXMLPath.Size = new System.Drawing.Size(609, 20);
            this.txtXMLPath.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Path:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(209, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(290, 18);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 22;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnOpenPath
            // 
            this.btnOpenPath.Location = new System.Drawing.Point(678, 60);
            this.btnOpenPath.Name = "btnOpenPath";
            this.btnOpenPath.Size = new System.Drawing.Size(28, 23);
            this.btnOpenPath.TabIndex = 23;
            this.btnOpenPath.Text = "...";
            this.btnOpenPath.UseVisualStyleBackColor = true;
            this.btnOpenPath.Click += new System.EventHandler(this.btnOpenPath_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 666);
            this.Controls.Add(this.btnOpenPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtXMLPath);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lb_keyCounter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbCiptherTextCount);
            this.Controls.Add(this.lbPlaintTextCount);
            this.Controls.Add(this.txtCipherText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCopyBinary);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKeyField);
            this.Controls.Add(this.rTxtOutput);
            this.Controls.Add(this.txtTextField);
            this.Controls.Add(this.btnEncrypt);
            this.MaximumSize = new System.Drawing.Size(741, 704);
            this.MinimumSize = new System.Drawing.Size(741, 704);
            this.Name = "Form1";
            this.Text = "Assigment 1 - symmetric cryptography";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.TextBox txtTextField;
        private System.Windows.Forms.RichTextBox rTxtOutput;
        private System.Windows.Forms.TextBox txtKeyField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnCopyBinary;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCipherText;
        private System.Windows.Forms.Label lbPlaintTextCount;
        private System.Windows.Forms.Label lbCiptherTextCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_keyCounter;
        private System.Windows.Forms.CheckBox cbKey;
        private System.Windows.Forms.CheckBox cbPlaintext;
        private System.Windows.Forms.CheckBox cbCiphertext;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtXMLPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnOpenPath;
    }
}

