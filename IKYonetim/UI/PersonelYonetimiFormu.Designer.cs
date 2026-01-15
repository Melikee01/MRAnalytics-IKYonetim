namespace IKYonetim.UI
{
    partial class PersonelYonetimiFormu
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
            this.dgvPersonel = new System.Windows.Forms.DataGridView();
            this.txtAd = new System.Windows.Forms.TextBox();
            this.txtSoyad = new System.Windows.Forms.TextBox();
            this.txtPozisyon = new System.Windows.Forms.TextBox();
            this.cmbDepartman = new System.Windows.Forms.ComboBox();
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.chkAktif = new System.Windows.Forms.CheckBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAktifeAl = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersonel)).BeginInit();
           
            this.SuspendLayout();
            // 
            // dgvPersonel
            // 
            this.dgvPersonel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPersonel.Location = new System.Drawing.Point(29, 227);
            this.dgvPersonel.Name = "dgvPersonel";
            this.dgvPersonel.ReadOnly = true;
            this.dgvPersonel.Size = new System.Drawing.Size(603, 194);
            this.dgvPersonel.TabIndex = 0;
            this.dgvPersonel.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPersonel_CellClick);
            // 
            // txtAd
            // 
            this.txtAd.Location = new System.Drawing.Point(29, 101);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(100, 20);
            this.txtAd.TabIndex = 1;
            // 
            // txtSoyad
            // 
            this.txtSoyad.Location = new System.Drawing.Point(29, 147);
            this.txtSoyad.Name = "txtSoyad";
            this.txtSoyad.Size = new System.Drawing.Size(100, 20);
            this.txtSoyad.TabIndex = 1;
            // 
            // txtPozisyon
            // 
            this.txtPozisyon.Location = new System.Drawing.Point(175, 101);
            this.txtPozisyon.Name = "txtPozisyon";
            this.txtPozisyon.Size = new System.Drawing.Size(100, 20);
            this.txtPozisyon.TabIndex = 1;
            // 
            // cmbDepartman
            // 
            this.cmbDepartman.FormattingEnabled = true;
            this.cmbDepartman.Location = new System.Drawing.Point(313, 101);
            this.cmbDepartman.Name = "cmbDepartman";
            this.cmbDepartman.Size = new System.Drawing.Size(121, 21);
            this.cmbDepartman.TabIndex = 2;
            // 
            // chkAktif
            // 
            this.chkAktif.AutoSize = true;
            this.chkAktif.Location = new System.Drawing.Point(29, 195);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Size = new System.Drawing.Size(134, 17);
            this.chkAktif.TabIndex = 3;
            this.chkAktif.Text = "Sadece Aktifleri Göster";
            this.chkAktif.UseVisualStyleBackColor = true;
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(152)))), ((int)(((byte)(238)))));
            this.btnEkle.Location = new System.Drawing.Point(472, 153);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(75, 23);
            this.btnEkle.TabIndex = 4;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = false;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(152)))), ((int)(((byte)(238)))));
            this.btnGuncelle.Location = new System.Drawing.Point(472, 124);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(75, 23);
            this.btnGuncelle.TabIndex = 4;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = false;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(152)))), ((int)(((byte)(238)))));
            this.btnSil.Location = new System.Drawing.Point(472, 85);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 23);
            this.btnSil.TabIndex = 4;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Soyad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(310, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Departman:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Pozisyon:";
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(175, 147);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(100, 20);
            this.txtemail.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(172, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "E-mail:";
            // 
            // cmbRol
            // 
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Items.AddRange(new object[] {
            "IK",
            "users"});
            this.cmbRol.Location = new System.Drawing.Point(313, 146);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(121, 21);
            this.cmbRol.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(321, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Rol:";
            // 
            // btnAktifeAl
            // 
            this.btnAktifeAl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(152)))), ((int)(((byte)(238)))));
            this.btnAktifeAl.Location = new System.Drawing.Point(472, 189);
            this.btnAktifeAl.Name = "btnAktifeAl";
            this.btnAktifeAl.Size = new System.Drawing.Size(75, 23);
            this.btnAktifeAl.TabIndex = 12;
            this.btnAktifeAl.Text = "Geri Yükle";
            this.btnAktifeAl.UseVisualStyleBackColor = false;
            this.btnAktifeAl.Click += new System.EventHandler(this.btnAktifeAl_Click);
            // 
            // PersonelYonetimiFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IKYonetim.Properties.Resources.arkaPlan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAktifeAl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtemail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.chkAktif);
            this.Controls.Add(this.cmbDepartman);
            this.Controls.Add(this.txtPozisyon);
            this.Controls.Add(this.txtSoyad);
            this.Controls.Add(this.txtAd);
            this.Controls.Add(this.dgvPersonel);
            this.Name = "PersonelYonetimiFormu";
            this.Text = "PersonelYonetimiFormu";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersonel)).EndInit();
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPersonel;
        private System.Windows.Forms.TextBox txtAd;
        private System.Windows.Forms.TextBox txtSoyad;
        private System.Windows.Forms.TextBox txtPozisyon;
        private System.Windows.Forms.ComboBox cmbDepartman;
        private System.Diagnostics.PerformanceCounter performanceCounter1;
        private System.Windows.Forms.CheckBox chkAktif;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAktifeAl;
    }
}