namespace IKYonetim.UI
{
    partial class GirisFormu
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
            this.lblemail = new System.Windows.Forms.Label();
            this.lblsifre = new System.Windows.Forms.Label();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.txtsifre = new System.Windows.Forms.TextBox();
            this.btngiris = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GİRİŞ";
            // 
            // lblemail
            // 
            this.lblemail.AutoSize = true;
            this.lblemail.Location = new System.Drawing.Point(70, 49);
            this.lblemail.Name = "lblemail";
            this.lblemail.Size = new System.Drawing.Size(35, 13);
            this.lblemail.TabIndex = 0;
            this.lblemail.Text = "E-mail";
            // 
            // lblsifre
            // 
            this.lblsifre.AutoSize = true;
            this.lblsifre.Location = new System.Drawing.Point(70, 117);
            this.lblsifre.Name = "lblsifre";
            this.lblsifre.Size = new System.Drawing.Size(28, 13);
            this.lblsifre.TabIndex = 0;
            this.lblsifre.Text = "Şifre";
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(73, 76);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(100, 20);
            this.txtemail.TabIndex = 1;
            // 
            // txtsifre
            // 
            this.txtsifre.Location = new System.Drawing.Point(73, 133);
            this.txtsifre.Name = "txtsifre";
            this.txtsifre.PasswordChar = '*';
            this.txtsifre.Size = new System.Drawing.Size(100, 20);
            this.txtsifre.TabIndex = 1;
            this.txtsifre.UseSystemPasswordChar = true;
            // 
            // btngiris
            // 
            this.btngiris.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(152)))), ((int)(((byte)(238)))));
            this.btngiris.Location = new System.Drawing.Point(84, 180);
            this.btngiris.Name = "btngiris";
            this.btngiris.Size = new System.Drawing.Size(75, 23);
            this.btngiris.TabIndex = 2;
            this.btngiris.Text = "Giriş";
            this.btngiris.UseVisualStyleBackColor = false;
            this.btngiris.Click += new System.EventHandler(this.btngiris_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(195)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btngiris);
            this.panel1.Controls.Add(this.txtemail);
            this.panel1.Controls.Add(this.lblsifre);
            this.panel1.Controls.Add(this.txtsifre);
            this.panel1.Controls.Add(this.lblemail);
            this.panel1.Location = new System.Drawing.Point(244, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 225);
            this.panel1.TabIndex = 3;
            // 
            // GirisFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IKYonetim.Properties.Resources.arkaPlan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "GirisFormu";
            this.Text = "GirisFormu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblemail;
        private System.Windows.Forms.Label lblsifre;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.TextBox txtsifre;
        private System.Windows.Forms.Button btngiris;
        private System.Windows.Forms.Panel panel1;
    }
}