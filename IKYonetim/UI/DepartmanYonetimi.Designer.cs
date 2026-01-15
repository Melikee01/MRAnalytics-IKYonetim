namespace IKYonetim.UI
{
    partial class DepartmanYonetimi
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
            this.dgvDepartman = new System.Windows.Forms.DataGridView();
            this.txtDepartmanAdi = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.btnPasifeAl = new System.Windows.Forms.Button();
            this.chkPasifleriGoster = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAktifeAl = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartman)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDepartman
            // 
            this.dgvDepartman.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepartman.Location = new System.Drawing.Point(30, 165);
            this.dgvDepartman.Name = "dgvDepartman";
            this.dgvDepartman.Size = new System.Drawing.Size(483, 169);
            this.dgvDepartman.TabIndex = 0;
            this.dgvDepartman.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartman_CellContentClick);
            this.dgvDepartman.SelectionChanged += new System.EventHandler(this.dgvDepartman_SelectionChanged);
            // 
            // txtDepartmanAdi
            // 
            this.txtDepartmanAdi.Location = new System.Drawing.Point(30, 106);
            this.txtDepartmanAdi.Name = "txtDepartmanAdi";
            this.txtDepartmanAdi.Size = new System.Drawing.Size(100, 20);
            this.txtDepartmanAdi.TabIndex = 1;
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(217, 97);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(75, 36);
            this.btnEkle.TabIndex = 2;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Location = new System.Drawing.Point(352, 97);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(75, 36);
            this.btnGuncelle.TabIndex = 2;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // btnPasifeAl
            // 
            this.btnPasifeAl.Location = new System.Drawing.Point(80, 351);
            this.btnPasifeAl.Name = "btnPasifeAl";
            this.btnPasifeAl.Size = new System.Drawing.Size(75, 58);
            this.btnPasifeAl.TabIndex = 2;
            this.btnPasifeAl.Text = "Pasife Al";
            this.btnPasifeAl.UseVisualStyleBackColor = true;
            this.btnPasifeAl.Click += new System.EventHandler(this.btnPasifeAl_Click);
            // 
            // chkPasifleriGoster
            // 
            this.chkPasifleriGoster.AutoSize = true;
            this.chkPasifleriGoster.Location = new System.Drawing.Point(30, 142);
            this.chkPasifleriGoster.Name = "chkPasifleriGoster";
            this.chkPasifleriGoster.Size = new System.Drawing.Size(96, 17);
            this.chkPasifleriGoster.TabIndex = 3;
            this.chkPasifleriGoster.Text = "Pasifleri Göster";
            this.chkPasifleriGoster.UseVisualStyleBackColor = true;
            this.chkPasifleriGoster.CheckedChanged += new System.EventHandler(this.chkPasifleriGoster_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Departman Adı:";
            // 
            // btnAktifeAl
            // 
            this.btnAktifeAl.Location = new System.Drawing.Point(330, 351);
            this.btnAktifeAl.Name = "btnAktifeAl";
            this.btnAktifeAl.Size = new System.Drawing.Size(75, 58);
            this.btnAktifeAl.TabIndex = 5;
            this.btnAktifeAl.Text = "Aktife Al";
            this.btnAktifeAl.UseVisualStyleBackColor = true;
            this.btnAktifeAl.Click += new System.EventHandler(this.btnAktifeAl_Click);
            // 
            // DepartmanYonetimi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IKYonetim.Properties.Resources.arkaPlan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAktifeAl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkPasifleriGoster);
            this.Controls.Add(this.btnPasifeAl);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.txtDepartmanAdi);
            this.Controls.Add(this.dgvDepartman);
            this.Name = "DepartmanYonetimi";
            this.Text = "DepartmanYonetimi";
            this.Load += new System.EventHandler(this.DepartmanYonetimi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartman)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDepartman;
        private System.Windows.Forms.TextBox txtDepartmanAdi;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.Button btnPasifeAl;
        private System.Windows.Forms.CheckBox chkPasifleriGoster;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAktifeAl;
    }
}