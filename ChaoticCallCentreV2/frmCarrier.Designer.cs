namespace ChaoticCallCentreV2
{
    partial class frmCarrier
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
            this.lbxCarrier = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarrierName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteCarrier = new System.Windows.Forms.Button();
            this.btnSaveCarrier = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblAdded = new System.Windows.Forms.Label();
            this.btnNewCarrier = new System.Windows.Forms.Button();
            this.txtNewCarrier = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxCarrier
            // 
            this.lbxCarrier.FormattingEnabled = true;
            this.lbxCarrier.Location = new System.Drawing.Point(12, 12);
            this.lbxCarrier.Name = "lbxCarrier";
            this.lbxCarrier.Size = new System.Drawing.Size(120, 173);
            this.lbxCarrier.TabIndex = 1;
            this.lbxCarrier.Click += new System.EventHandler(this.lbxCarrier_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Carrier Name:";
            // 
            // txtCarrierName
            // 
            this.txtCarrierName.Location = new System.Drawing.Point(83, 13);
            this.txtCarrierName.Name = "txtCarrierName";
            this.txtCarrierName.Size = new System.Drawing.Size(100, 20);
            this.txtCarrierName.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteCarrier);
            this.groupBox1.Controls.Add(this.btnSaveCarrier);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCarrierName);
            this.groupBox1.Location = new System.Drawing.Point(138, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 74);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Carrier Edit";
            // 
            // btnDeleteCarrier
            // 
            this.btnDeleteCarrier.Location = new System.Drawing.Point(98, 39);
            this.btnDeleteCarrier.Name = "btnDeleteCarrier";
            this.btnDeleteCarrier.Size = new System.Drawing.Size(84, 23);
            this.btnDeleteCarrier.TabIndex = 5;
            this.btnDeleteCarrier.Text = "&Delete Carrier";
            this.btnDeleteCarrier.UseVisualStyleBackColor = true;
            this.btnDeleteCarrier.Click += new System.EventHandler(this.btnDeleteCarrier_Click);
            // 
            // btnSaveCarrier
            // 
            this.btnSaveCarrier.Location = new System.Drawing.Point(6, 39);
            this.btnSaveCarrier.Name = "btnSaveCarrier";
            this.btnSaveCarrier.Size = new System.Drawing.Size(84, 23);
            this.btnSaveCarrier.TabIndex = 4;
            this.btnSaveCarrier.Text = "&Save Carrier";
            this.btnSaveCarrier.UseVisualStyleBackColor = true;
            this.btnSaveCarrier.Click += new System.EventHandler(this.btnSaveCarrier_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblAdded);
            this.groupBox2.Controls.Add(this.btnNewCarrier);
            this.groupBox2.Controls.Add(this.txtNewCarrier);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(138, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New Carrier";
            // 
            // lblAdded
            // 
            this.lblAdded.AutoSize = true;
            this.lblAdded.Location = new System.Drawing.Point(114, 37);
            this.lblAdded.Name = "lblAdded";
            this.lblAdded.Size = new System.Drawing.Size(0, 13);
            this.lblAdded.TabIndex = 3;
            // 
            // btnNewCarrier
            // 
            this.btnNewCarrier.Location = new System.Drawing.Point(7, 63);
            this.btnNewCarrier.Name = "btnNewCarrier";
            this.btnNewCarrier.Size = new System.Drawing.Size(100, 23);
            this.btnNewCarrier.TabIndex = 2;
            this.btnNewCarrier.Text = "&Add New Carrier";
            this.btnNewCarrier.UseVisualStyleBackColor = true;
            this.btnNewCarrier.Click += new System.EventHandler(this.btnNewCarrier_Click);
            // 
            // txtNewCarrier
            // 
            this.txtNewCarrier.Location = new System.Drawing.Point(7, 37);
            this.txtNewCarrier.Name = "txtNewCarrier";
            this.txtNewCarrier.Size = new System.Drawing.Size(100, 20);
            this.txtNewCarrier.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Carrier Name:";
            // 
            // frmCarrier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 198);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbxCarrier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCarrier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chaotic Call Centre Carrier Edit";
            this.Activated += new System.EventHandler(this.frmCarrier_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxCarrier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCarrierName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteCarrier;
        private System.Windows.Forms.Button btnSaveCarrier;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnNewCarrier;
        private System.Windows.Forms.TextBox txtNewCarrier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAdded;


    }
}