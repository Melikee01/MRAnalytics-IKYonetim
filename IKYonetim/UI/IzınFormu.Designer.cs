namespace IKYonetim.UI
{
    partial class IzinFormu
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
            this.grpTalep = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBaslangic = new System.Windows.Forms.DateTimePicker();
            this.dtpBitis = new System.Windows.Forms.DateTimePicker();
            this.cmbIzinTuru = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTalepOlustur = new System.Windows.Forms.Button();
            this.dgvIzinler = new System.Windows.Forms.DataGridView();
            this.grpAdminOnay = new System.Windows.Forms.GroupBox();
            this.btnOnayla = new System.Windows.Forms.Button();
            this.btnReddet = new System.Windows.Forms.Button();
            this.lblMod = new System.Windows.Forms.Label();
            this.chkTumIzinler = new System.Windows.Forms.CheckBox();
            this.chkBekleyenler = new System.Windows.Forms.CheckBox();
            this.grpTalep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIzinler)).BeginInit();
            this.grpAdminOnay.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTalep
            // 
            this.grpTalep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grpTalep.Controls.Add(this.dtpBaslangic);
            this.grpTalep.Controls.Add(this.label1);
            this.grpTalep.Controls.Add(this.dtpBitis);
            this.grpTalep.Controls.Add(this.btnTalepOlustur);
            this.grpTalep.Controls.Add(this.label2);
            this.grpTalep.Controls.Add(this.label3);
            this.grpTalep.Controls.Add(this.label4);
            this.grpTalep.Controls.Add(this.cmbIzinTuru);
            this.grpTalep.Controls.Add(this.txtAciklama);
            this.grpTalep.Location = new System.Drawing.Point(26, 29);
            this.grpTalep.Name = "grpTalep";
            this.grpTalep.Size = new System.Drawing.Size(458, 180);
            this.grpTalep.TabIndex = 0;
            this.grpTalep.TabStop = false;
            this.grpTalep.Text = "İzin Talebi";
            this.grpTalep.Enter += new System.EventHandler(this.grpTalep_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 12);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Başlangıç Tarihi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bitiş Tarihi";
            // 
            // dtpBaslangic
            // 
            this.dtpBaslangic.Location = new System.Drawing.Point(238, 28);
            this.dtpBaslangic.Name = "dtpBaslangic";
            this.dtpBaslangic.Size = new System.Drawing.Size(200, 20);
            this.dtpBaslangic.TabIndex = 3;
            // 
            // dtpBitis
            // 
            this.dtpBitis.Location = new System.Drawing.Point(238, 81);
            this.dtpBitis.Name = "dtpBitis";
            this.dtpBitis.Size = new System.Drawing.Size(200, 20);
            this.dtpBitis.TabIndex = 3;
            // 
            // cmbIzinTuru
            // 
            this.cmbIzinTuru.FormattingEnabled = true;
            this.cmbIzinTuru.Location = new System.Drawing.Point(18, 36);
            this.cmbIzinTuru.Name = "cmbIzinTuru";
            this.cmbIzinTuru.Size = new System.Drawing.Size(121, 21);
            this.cmbIzinTuru.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "İzin Türü";
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(18, 84);
            this.txtAciklama.Multiline = true;
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(100, 20);
            this.txtAciklama.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Açıklama";
            // 
            // btnTalepOlustur
            // 
            this.btnTalepOlustur.Location = new System.Drawing.Point(26, 137);
            this.btnTalepOlustur.Name = "btnTalepOlustur";
            this.btnTalepOlustur.Size = new System.Drawing.Size(92, 23);
            this.btnTalepOlustur.TabIndex = 6;
            this.btnTalepOlustur.Text = "Talep Oluştur";
            this.btnTalepOlustur.UseVisualStyleBackColor = true;
            this.btnTalepOlustur.Click += new System.EventHandler(this.btnTalepOlustur_Click);
            // 
            // dgvIzinler
            // 
            this.dgvIzinler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIzinler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIzinler.Location = new System.Drawing.Point(30, 265);
            this.dgvIzinler.Name = "dgvIzinler";
            this.dgvIzinler.ReadOnly = true;
            this.dgvIzinler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIzinler.Size = new System.Drawing.Size(691, 192);
            this.dgvIzinler.TabIndex = 7;
            // 
            // grpAdminOnay
            // 
            this.grpAdminOnay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grpAdminOnay.Controls.Add(this.btnOnayla);
            this.grpAdminOnay.Controls.Add(this.btnReddet);
            this.grpAdminOnay.Location = new System.Drawing.Point(490, 29);
            this.grpAdminOnay.Name = "grpAdminOnay";
            this.grpAdminOnay.Size = new System.Drawing.Size(231, 180);
            this.grpAdminOnay.TabIndex = 8;
            this.grpAdminOnay.TabStop = false;
            this.grpAdminOnay.Text = "Admin Onay / Red";
            // 
            // btnOnayla
            // 
            this.btnOnayla.Location = new System.Drawing.Point(25, 81);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(75, 67);
            this.btnOnayla.TabIndex = 9;
            this.btnOnayla.Text = "Onayla";
            this.btnOnayla.UseVisualStyleBackColor = true;
            this.btnOnayla.Enter += new System.EventHandler(this.btnOnayla_Click);
            // 
            // btnReddet
            // 
            this.btnReddet.Location = new System.Drawing.Point(131, 81);
            this.btnReddet.Name = "btnReddet";
            this.btnReddet.Size = new System.Drawing.Size(75, 67);
            this.btnReddet.TabIndex = 9;
            this.btnReddet.Text = "Reddet";
            this.btnReddet.UseVisualStyleBackColor = true;
            this.btnReddet.Enter += new System.EventHandler(this.btnReddet_Click);
            // 
            // lblMod
            // 
            this.lblMod.AutoSize = true;
            this.lblMod.Location = new System.Drawing.Point(372, 13);
            this.lblMod.Name = "lblMod";
            this.lblMod.Size = new System.Drawing.Size(35, 13);
            this.lblMod.TabIndex = 10;
            this.lblMod.Text = "label5";
            // 
            // chkTumIzinler
            // 
            this.chkTumIzinler.AutoSize = true;
            this.chkTumIzinler.Location = new System.Drawing.Point(30, 215);
            this.chkTumIzinler.Name = "chkTumIzinler";
            this.chkTumIzinler.Size = new System.Drawing.Size(110, 17);
            this.chkTumIzinler.TabIndex = 11;
            this.chkTumIzinler.Text = "Tüm izinleri göster";
            this.chkTumIzinler.UseVisualStyleBackColor = true;
            this.chkTumIzinler.Visible = false;
            this.chkTumIzinler.CheckedChanged += new System.EventHandler(this.chkTumIzinler_CheckedChanged);
            // 
            // chkBekleyenler
            // 
            this.chkBekleyenler.AutoSize = true;
            this.chkBekleyenler.Location = new System.Drawing.Point(30, 238);
            this.chkBekleyenler.Name = "chkBekleyenler";
            this.chkBekleyenler.Size = new System.Drawing.Size(137, 17);
            this.chkBekleyenler.TabIndex = 12;
            this.chkBekleyenler.Text = "Sadece beklemedekiler";
            this.chkBekleyenler.UseVisualStyleBackColor = true;
            this.chkBekleyenler.Visible = false;
            this.chkBekleyenler.CheckedChanged += new System.EventHandler(this.chkBekleyenler_CheckedChanged);
            // 
            // IzinFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 469);
            this.Controls.Add(this.chkBekleyenler);
            this.Controls.Add(this.chkTumIzinler);
            this.Controls.Add(this.lblMod);
            this.Controls.Add(this.grpAdminOnay);
            this.Controls.Add(this.dgvIzinler);
            this.Controls.Add(this.grpTalep);
            this.Name = "IzinFormu";
            this.Text = "IzınFormu";
            this.Load += new System.EventHandler(this.IzinFormu_Load);
            this.grpTalep.ResumeLayout(false);
            this.grpTalep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIzinler)).EndInit();
            this.grpAdminOnay.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTalep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpBaslangic;
        private System.Windows.Forms.DateTimePicker dtpBitis;
        private System.Windows.Forms.ComboBox cmbIzinTuru;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTalepOlustur;
        private System.Windows.Forms.DataGridView dgvIzinler;
        private System.Windows.Forms.GroupBox grpAdminOnay;
        private System.Windows.Forms.Button btnOnayla;
        private System.Windows.Forms.Button btnReddet;
        private System.Windows.Forms.Label lblMod;
        private System.Windows.Forms.CheckBox chkTumIzinler;
        private System.Windows.Forms.CheckBox chkBekleyenler;
    }
}