namespace IKYonetim.UI
{
    partial class RaporFormu
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
            this.cmbRapor = new System.Windows.Forms.ComboBox();
            this.btnGetir = new System.Windows.Forms.Button();
            this.dgvRapor = new System.Windows.Forms.DataGridView();
            this.grpFiltre = new System.Windows.Forms.GroupBox();
            this.nudAy = new System.Windows.Forms.NumericUpDown();
            this.nudYil = new System.Windows.Forms.NumericUpDown();
            this.dtBas = new System.Windows.Forms.DateTimePicker();
            this.dtBit = new System.Windows.Forms.DateTimePicker();
            this.pnlTarih = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.PnlMaas = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRapor)).BeginInit();
            this.grpFiltre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYil)).BeginInit();
            this.pnlTarih.SuspendLayout();
            this.PnlMaas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbRapor
            // 
            this.cmbRapor.FormattingEnabled = true;
            this.cmbRapor.Location = new System.Drawing.Point(6, 19);
            this.cmbRapor.Name = "cmbRapor";
            this.cmbRapor.Size = new System.Drawing.Size(121, 21);
            this.cmbRapor.TabIndex = 0;
            // 
            // btnGetir
            // 
            this.btnGetir.Location = new System.Drawing.Point(6, 61);
            this.btnGetir.Name = "btnGetir";
            this.btnGetir.Size = new System.Drawing.Size(75, 23);
            this.btnGetir.TabIndex = 1;
            this.btnGetir.Text = "Raporu Getir";
            this.btnGetir.UseVisualStyleBackColor = true;
            this.btnGetir.Click += new System.EventHandler(this.btnGetir_Click);
            // 
            // dgvRapor
            // 
            this.dgvRapor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRapor.Location = new System.Drawing.Point(23, 148);
            this.dgvRapor.Name = "dgvRapor";
            this.dgvRapor.Size = new System.Drawing.Size(736, 150);
            this.dgvRapor.TabIndex = 2;
            // 
            // grpFiltre
            // 
            this.grpFiltre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grpFiltre.Controls.Add(this.cmbRapor);
            this.grpFiltre.Controls.Add(this.btnGetir);
            this.grpFiltre.Location = new System.Drawing.Point(241, 12);
            this.grpFiltre.Name = "grpFiltre";
            this.grpFiltre.Size = new System.Drawing.Size(251, 130);
            this.grpFiltre.TabIndex = 3;
            this.grpFiltre.TabStop = false;
            this.grpFiltre.Text = "Filtreler";
            // 
            // nudAy
            // 
            this.nudAy.Location = new System.Drawing.Point(73, 68);
            this.nudAy.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudAy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAy.Name = "nudAy";
            this.nudAy.Size = new System.Drawing.Size(120, 20);
            this.nudAy.TabIndex = 0;
            this.nudAy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudYil
            // 
            this.nudYil.Location = new System.Drawing.Point(73, 34);
            this.nudYil.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.nudYil.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudYil.Name = "nudYil";
            this.nudYil.Size = new System.Drawing.Size(120, 20);
            this.nudYil.TabIndex = 0;
            this.nudYil.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // dtBas
            // 
            this.dtBas.Location = new System.Drawing.Point(21, 34);
            this.dtBas.Name = "dtBas";
            this.dtBas.Size = new System.Drawing.Size(200, 20);
            this.dtBas.TabIndex = 0;
            // 
            // dtBit
            // 
            this.dtBit.Location = new System.Drawing.Point(21, 67);
            this.dtBit.Name = "dtBit";
            this.dtBit.Size = new System.Drawing.Size(200, 20);
            this.dtBit.TabIndex = 0;
            // 
            // pnlTarih
            // 
            this.pnlTarih.BackColor = System.Drawing.Color.Silver;
            this.pnlTarih.Controls.Add(this.label2);
            this.pnlTarih.Controls.Add(this.dtBit);
            this.pnlTarih.Controls.Add(this.dtBas);
            this.pnlTarih.Location = new System.Drawing.Point(409, 332);
            this.pnlTarih.Name = "pnlTarih";
            this.pnlTarih.Size = new System.Drawing.Size(238, 100);
            this.pnlTarih.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "izin Tarihi Filtreleme";
            // 
            // PnlMaas
            // 
            this.PnlMaas.BackColor = System.Drawing.Color.Silver;
            this.PnlMaas.Controls.Add(this.label4);
            this.PnlMaas.Controls.Add(this.label3);
            this.PnlMaas.Controls.Add(this.label1);
            this.PnlMaas.Controls.Add(this.nudAy);
            this.PnlMaas.Controls.Add(this.nudYil);
            this.PnlMaas.Location = new System.Drawing.Point(140, 332);
            this.PnlMaas.Name = "PnlMaas";
            this.PnlMaas.Size = new System.Drawing.Size(215, 100);
            this.PnlMaas.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Ay:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Yıl:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Maaş Filtrele:";
            // 
            // RaporFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PnlMaas);
            this.Controls.Add(this.pnlTarih);
            this.Controls.Add(this.grpFiltre);
            this.Controls.Add(this.dgvRapor);
            this.Name = "RaporFormu";
            this.Text = "RaporFormu";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRapor)).EndInit();
            this.grpFiltre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYil)).EndInit();
            this.pnlTarih.ResumeLayout(false);
            this.pnlTarih.PerformLayout();
            this.PnlMaas.ResumeLayout(false);
            this.PnlMaas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRapor;
        private System.Windows.Forms.Button btnGetir;
        private System.Windows.Forms.DataGridView dgvRapor;
        private System.Windows.Forms.GroupBox grpFiltre;
        private System.Windows.Forms.NumericUpDown nudAy;
        private System.Windows.Forms.NumericUpDown nudYil;
        private System.Windows.Forms.DateTimePicker dtBas;
        private System.Windows.Forms.DateTimePicker dtBit;
        private System.Windows.Forms.Panel pnlTarih;
        private System.Windows.Forms.Panel PnlMaas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}