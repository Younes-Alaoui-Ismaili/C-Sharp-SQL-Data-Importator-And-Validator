namespace GTFSValidatorAndImportTool
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSQLPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSQLUserId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSQLServerAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoadAndExtractGTFS = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAgency = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadAndExtractInterfaceDevzip = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.operationProgress = new System.Windows.Forms.ProgressBar();
            this.openFileDialogInterfaceDevzip = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogGTFSzip = new System.Windows.Forms.OpenFileDialog();
            this.btnRunValidationAndImport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSQLPassword);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSQLUserId);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSQLServerAddress);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnLoadAndExtractGTFS);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxAgency);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnLoadAndExtractInterfaceDevzip);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 238);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // txtSQLPassword
            // 
            this.txtSQLPassword.Location = new System.Drawing.Point(141, 202);
            this.txtSQLPassword.Name = "txtSQLPassword";
            this.txtSQLPassword.PasswordChar = 'x';
            this.txtSQLPassword.Size = new System.Drawing.Size(168, 20);
            this.txtSQLPassword.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "SQL Passoword: ";
            // 
            // txtSQLUserId
            // 
            this.txtSQLUserId.Location = new System.Drawing.Point(141, 176);
            this.txtSQLUserId.Name = "txtSQLUserId";
            this.txtSQLUserId.Size = new System.Drawing.Size(168, 20);
            this.txtSQLUserId.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "SQL UserId: ";
            // 
            // txtSQLServerAddress
            // 
            this.txtSQLServerAddress.Location = new System.Drawing.Point(141, 150);
            this.txtSQLServerAddress.Name = "txtSQLServerAddress";
            this.txtSQLServerAddress.Size = new System.Drawing.Size(168, 20);
            this.txtSQLServerAddress.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "SQL Server Address: ";
            // 
            // btnLoadAndExtractGTFS
            // 
            this.btnLoadAndExtractGTFS.Location = new System.Drawing.Point(141, 98);
            this.btnLoadAndExtractGTFS.Name = "btnLoadAndExtractGTFS";
            this.btnLoadAndExtractGTFS.Size = new System.Drawing.Size(168, 46);
            this.btnLoadAndExtractGTFS.TabIndex = 5;
            this.btnLoadAndExtractGTFS.Text = "Browse And Extract";
            this.btnLoadAndExtractGTFS.UseVisualStyleBackColor = true;
            this.btnLoadAndExtractGTFS.Click += new System.EventHandler(this.btnLoadAndExtractGRFS_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Upload GTFS.zip: ";
            // 
            // comboBoxAgency
            // 
            this.comboBoxAgency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAgency.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxAgency.FormattingEnabled = true;
            this.comboBoxAgency.Location = new System.Drawing.Point(141, 71);
            this.comboBoxAgency.Name = "comboBoxAgency";
            this.comboBoxAgency.Size = new System.Drawing.Size(168, 21);
            this.comboBoxAgency.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Agency:";
            // 
            // btnLoadAndExtractInterfaceDevzip
            // 
            this.btnLoadAndExtractInterfaceDevzip.Location = new System.Drawing.Point(141, 19);
            this.btnLoadAndExtractInterfaceDevzip.Name = "btnLoadAndExtractInterfaceDevzip";
            this.btnLoadAndExtractInterfaceDevzip.Size = new System.Drawing.Size(168, 46);
            this.btnLoadAndExtractInterfaceDevzip.TabIndex = 1;
            this.btnLoadAndExtractInterfaceDevzip.Text = "Browse And Extract";
            this.btnLoadAndExtractInterfaceDevzip.UseVisualStyleBackColor = true;
            this.btnLoadAndExtractInterfaceDevzip.Click += new System.EventHandler(this.btnLoadAndExtractInterfaceDevzip_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Upload InterfaceDev.zip: ";
            // 
            // operationProgress
            // 
            this.operationProgress.Location = new System.Drawing.Point(15, 308);
            this.operationProgress.Name = "operationProgress";
            this.operationProgress.Size = new System.Drawing.Size(321, 23);
            this.operationProgress.TabIndex = 1;
            // 
            // openFileDialogInterfaceDevzip
            // 
            this.openFileDialogInterfaceDevzip.FileName = "InterfaceDev";
            this.openFileDialogInterfaceDevzip.Title = "Please Select InterfaceDev.zip";
            // 
            // openFileDialogGTFSzip
            // 
            this.openFileDialogGTFSzip.FileName = "GTFS";
            // 
            // btnRunValidationAndImport
            // 
            this.btnRunValidationAndImport.Location = new System.Drawing.Point(15, 256);
            this.btnRunValidationAndImport.Name = "btnRunValidationAndImport";
            this.btnRunValidationAndImport.Size = new System.Drawing.Size(321, 46);
            this.btnRunValidationAndImport.TabIndex = 13;
            this.btnRunValidationAndImport.Text = "Run Validation and Import";
            this.btnRunValidationAndImport.UseVisualStyleBackColor = true;
            this.btnRunValidationAndImport.Click += new System.EventHandler(this.btnRunValidationAndImport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 345);
            this.Controls.Add(this.btnRunValidationAndImport);
            this.Controls.Add(this.operationProgress);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(366, 384);
            this.MinimumSize = new System.Drawing.Size(366, 384);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTFS Validator And Import Tool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSQLPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSQLUserId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSQLServerAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoadAndExtractGTFS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAgency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoadAndExtractInterfaceDevzip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar operationProgress;
        private System.Windows.Forms.OpenFileDialog openFileDialogInterfaceDevzip;
        private System.Windows.Forms.OpenFileDialog openFileDialogGTFSzip;
        private System.Windows.Forms.Button btnRunValidationAndImport;
    }
}

