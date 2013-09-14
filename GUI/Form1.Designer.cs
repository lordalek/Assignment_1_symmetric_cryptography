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
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeyField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDecrypt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCipherText = new System.Windows.Forms.TextBox();
            this.lbPlaintTextCount = new System.Windows.Forms.Label();
            this.lbCiptherTextCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(47, 149);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // txtTextField
            // 
            this.txtTextField.Location = new System.Drawing.Point(73, 25);
            this.txtTextField.Name = "txtTextField";
            this.txtTextField.Size = new System.Drawing.Size(244, 20);
            this.txtTextField.TabIndex = 1;
            this.txtTextField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTextField_KeyUp);
            // 
            // rTxtOutput
            // 
            this.rTxtOutput.Location = new System.Drawing.Point(47, 178);
            this.rTxtOutput.Name = "rTxtOutput";
            this.rTxtOutput.Size = new System.Drawing.Size(414, 176);
            this.rTxtOutput.TabIndex = 2;
            this.rTxtOutput.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Insert 8 letters";
            // 
            // txtKeyField
            // 
            this.txtKeyField.Location = new System.Drawing.Point(50, 123);
            this.txtKeyField.Name = "txtKeyField";
            this.txtKeyField.Size = new System.Drawing.Size(244, 20);
            this.txtKeyField.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Plaintext";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Key";
            // 
            // txtDecrypt
            // 
            this.txtDecrypt.Location = new System.Drawing.Point(219, 149);
            this.txtDecrypt.Name = "txtDecrypt";
            this.txtDecrypt.Size = new System.Drawing.Size(75, 23);
            this.txtDecrypt.TabIndex = 7;
            this.txtDecrypt.Text = "Decrypt";
            this.txtDecrypt.UseVisualStyleBackColor = true;
            this.txtDecrypt.Click += new System.EventHandler(this.txtDecrypt_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ciphertext";
            // 
            // txtCipherText
            // 
            this.txtCipherText.Location = new System.Drawing.Point(73, 52);
            this.txtCipherText.Name = "txtCipherText";
            this.txtCipherText.Size = new System.Drawing.Size(363, 20);
            this.txtCipherText.TabIndex = 10;
            this.txtCipherText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // lbPlaintTextCount
            // 
            this.lbPlaintTextCount.AutoSize = true;
            this.lbPlaintTextCount.Location = new System.Drawing.Point(324, 31);
            this.lbPlaintTextCount.Name = "lbPlaintTextCount";
            this.lbPlaintTextCount.Size = new System.Drawing.Size(0, 13);
            this.lbPlaintTextCount.TabIndex = 11;
            // 
            // lbCiptherTextCount
            // 
            this.lbCiptherTextCount.AutoSize = true;
            this.lbCiptherTextCount.Location = new System.Drawing.Point(443, 55);
            this.lbCiptherTextCount.Name = "lbCiptherTextCount";
            this.lbCiptherTextCount.Size = new System.Drawing.Size(0, 13);
            this.lbCiptherTextCount.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Insert 8 letters";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 532);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbCiptherTextCount);
            this.Controls.Add(this.lbPlaintTextCount);
            this.Controls.Add(this.txtCipherText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDecrypt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKeyField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rTxtOutput);
            this.Controls.Add(this.txtTextField);
            this.Controls.Add(this.btnEncrypt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.TextBox txtTextField;
        private System.Windows.Forms.RichTextBox rTxtOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKeyField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button txtDecrypt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCipherText;
        private System.Windows.Forms.Label lbPlaintTextCount;
        private System.Windows.Forms.Label lbCiptherTextCount;
        private System.Windows.Forms.Label label5;
    }
}

