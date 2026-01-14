using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225); // MistyRose
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            panel1.BackColor = System.Drawing.Color.Silver; 
        }

        private void AnaMenu_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgisi
            lblKullaniciBilgi.Text =
                $"Hoşgeldiniz: " +
                $"{OturumYoneticisi.Email} ({OturumYoneticisi.Rol})";

            // Önce hepsini kapat
            btnPersonel.Visible = false;
            btnDeparman.Visible = false;
            btnIzin.Visible = false;
            btnmaas.Visible = false;
            btnPerformans.Visible = false;
            btnRaporlama.Visible = false;
            btnsifredegis.Visible = true; // herkes şifre değiştirebilir

            // Rol bazlı yetkilendirme
            if (OturumYoneticisi.Rol == "Admin")
            {
                btnPersonel.Visible = true;
                btnDeparman.Visible = true;
                btnIzin.Visible = true;
                btnmaas.Visible = true;
                btnPerformans.Visible = true;
                btnRaporlama.Visible = true;
            }
            else if (OturumYoneticisi.Rol == "IK")
            {
                btnPersonel.Visible = true;
                btnDeparman.Visible = true;
                btnIzin.Visible = true;
                btnPerformans.Visible = true;
            }
            else if (OturumYoneticisi.Rol == "users")
            {
                btnIzin.Visible = true;
            }

        }

        private void btnPersonel_Click(object sender, EventArgs e)
        {
            new PersonelYonetimiFormu().ShowDialog();
        }

        private void btnDeparman_Click(object sender, EventArgs e)
        {
            new DepartmanYonetimi().ShowDialog();
        }

        
        private void btnIzin_Click(object sender, EventArgs e)
        {
            new IzinFormu().ShowDialog(); 
        }

        private void btnmaas_Click(object sender, EventArgs e)
        {
            new MaasHesaplamaFormu().ShowDialog();
        }

        private void btnPerformans_Click(object sender, EventArgs e)
        {
            new PerformansFormu().ShowDialog();
        }

        private void btnRaporlama_Click(object sender, EventArgs e)
        {
            new RaporFormu().ShowDialog();
        }

        private void btnsifredegis_Click(object sender, EventArgs e)
        {
            // Not: ENTITY’de de SifreDegistir sınıfı olduğu için,
            // çakışma ihtimaline karşı tam isimle çağırıyorum.
            using (var frm = new IKYonetim.UI.SifreDegistirFormu())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this); // modal açılır
            }

        }

        private void btnCikıs_Click(object sender, EventArgs e)
        {
            OturumYoneticisi.CikisYap();
            new GirisFormu().Show();
            this.Close();
        }
    }

}

