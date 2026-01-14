namespace IKYonetim.UI
{
    partial class MaasHesaplamaFormu
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBrut = new System.Windows.Forms.TextBox();
            this.txtPrim = new System.Windows.Forms.TextBox();
            this.txtMesai = new System.Windows.Forms.TextBox();
            this.txtKesinti = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbYil = new System.Windows.Forms.ComboBox();
            this.cmbAy = new System.Windows.Forms.ComboBox();
            this.lblNetMaas = new System.Windows.Forms.Label();
            this.btnHesapla = new System.Windows.Forms.Button();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnListele = new System.Windows.Forms.Button();
            this.dgvMaaslar = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.cmbPersonel = new System.Windows.Forms.ComboBox();
            this.lblPersonel = new System.Windows.Forms.Label();
            this.btnSil = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaaslar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brüt Maaş:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Prim:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(487, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mesai:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Kesinti:";
            // 
            // txtBrut
            // 
            this.txtBrut.Location = new System.Drawing.Point(150, 30);
            this.txtBrut.Name = "txtBrut";
            this.txtBrut.Size = new System.Drawing.Size(100, 20);
            this.txtBrut.TabIndex = 1;
            // 
            // txtPrim
            // 
            this.txtPrim.Location = new System.Drawing.Point(346, 30);
            this.txtPrim.Name = "txtPrim";
            this.txtPrim.Size = new System.Drawing.Size(100, 20);
            this.txtPrim.TabIndex = 1;
            // 
            // txtMesai
            // 
            this.txtMesai.Location = new System.Drawing.Point(535, 33);
            this.txtMesai.Name = "txtMesai";
            this.txtMesai.Size = new System.Drawing.Size(100, 20);
            this.txtMesai.TabIndex = 1;
            // 
            // txtKesinti
            // 
            this.txtKesinti.Location = new System.Drawing.Point(150, 86);
            this.txtKesinti.Name = "txtKesinti";
            this.txtKesinti.Size = new System.Drawing.Size(100, 20);
            this.txtKesinti.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(487, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Yıl:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Ay:";
            // 
            // cmbYil
            // 
            this.cmbYil.FormattingEnabled = true;
            this.cmbYil.Location = new System.Drawing.Point(514, 93);
            this.cmbYil.Name = "cmbYil";
            this.cmbYil.Size = new System.Drawing.Size(121, 21);
            this.cmbYil.TabIndex = 4;
            // 
            // cmbAy
            // 
            this.cmbAy.FormattingEnabled = true;
            this.cmbAy.Location = new System.Drawing.Point(129, 143);
            this.cmbAy.Name = "cmbAy";
            this.cmbAy.Size = new System.Drawing.Size(121, 21);
            this.cmbAy.TabIndex = 4;
            // 
            // lblNetMaas
            // 
            this.lblNetMaas.AutoSize = true;
            this.lblNetMaas.Location = new System.Drawing.Point(317, 151);
            this.lblNetMaas.Name = "lblNetMaas";
            this.lblNetMaas.Size = new System.Drawing.Size(56, 13);
            this.lblNetMaas.TabIndex = 5;
            this.lblNetMaas.Text = "Net Maaş:";
            // 
            // btnHesapla
            // 
            this.btnHesapla.Location = new System.Drawing.Point(578, 188);
            this.btnHesapla.Name = "btnHesapla";
            this.btnHesapla.Size = new System.Drawing.Size(75, 23);
            this.btnHesapla.TabIndex = 6;
            this.btnHesapla.Text = "Hesapla";
            this.btnHesapla.UseVisualStyleBackColor = true;
            this.btnHesapla.Click += new System.EventHandler(this.btnHesapla_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(460, 188);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 23);
            this.btnKaydet.TabIndex = 6;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnListele
            // 
            this.btnListele.Location = new System.Drawing.Point(320, 188);
            this.btnListele.Name = "btnListele";
            this.btnListele.Size = new System.Drawing.Size(75, 23);
            this.btnListele.TabIndex = 6;
            this.btnListele.Text = "Listele";
            this.btnListele.UseVisualStyleBackColor = true;
            this.btnListele.Click += new System.EventHandler(this.btnListele_Click);
            // 
            // dgvMaaslar
            // 
            this.dgvMaaslar.AllowUserToAddRows = false;
            this.dgvMaaslar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaaslar.Location = new System.Drawing.Point(12, 242);
            this.dgvMaaslar.Name = "dgvMaaslar";
            this.dgvMaaslar.ReadOnly = true;
            this.dgvMaaslar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaaslar.Size = new System.Drawing.Size(852, 150);
            this.dgvMaaslar.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(696, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Açıklama:";
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(755, 33);
            this.txtAciklama.Multiline = true;
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(100, 131);
            this.txtAciklama.TabIndex = 9;
            // 
            // cmbPersonel
            // 
            this.cmbPersonel.FormattingEnabled = true;
            this.cmbPersonel.Location = new System.Drawing.Point(336, 93);
            this.cmbPersonel.Name = "cmbPersonel";
            this.cmbPersonel.Size = new System.Drawing.Size(121, 21);
            this.cmbPersonel.TabIndex = 11;
            // 
            // lblPersonel
            // 
            this.lblPersonel.AutoSize = true;
            this.lblPersonel.Location = new System.Drawing.Point(279, 93);
            this.lblPersonel.Name = "lblPersonel";
            this.lblPersonel.Size = new System.Drawing.Size(51, 13);
            this.lblPersonel.TabIndex = 12;
            this.lblPersonel.Text = "Personel:";
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(699, 188);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 23);
            this.btnSil.TabIndex = 13;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // MaasHesaplamaFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 450);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.lblPersonel);
            this.Controls.Add(this.cmbPersonel);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgvMaaslar);
            this.Controls.Add(this.btnListele);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.btnHesapla);
            this.Controls.Add(this.lblNetMaas);
            this.Controls.Add(this.cmbAy);
            this.Controls.Add(this.cmbYil);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtKesinti);
            this.Controls.Add(this.txtMesai);
            this.Controls.Add(this.txtPrim);
            this.Controls.Add(this.txtBrut);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MaasHesaplamaFormu";
            this.Text = "MaasHesaplamaFormu";
            this.Load += new System.EventHandler(this.MaasHesaplamaFormu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaaslar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBrut;
        private System.Windows.Forms.TextBox txtPrim;
        private System.Windows.Forms.TextBox txtMesai;
        private System.Windows.Forms.TextBox txtKesinti;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbYil;
        private System.Windows.Forms.ComboBox cmbAy;
        private System.Windows.Forms.Label lblNetMaas;
        private System.Windows.Forms.Button btnHesapla;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnListele;
        private System.Windows.Forms.DataGridView dgvMaaslar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.ComboBox cmbPersonel;
        private System.Windows.Forms.Label lblPersonel;
        private System.Windows.Forms.Button btnSil;
    }
}