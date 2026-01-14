using IKYonetim.BLL;
using System;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class GirisFormu : Form
    {
        public GirisFormu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225); // MistyRose
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            panel1.BackColor = System.Drawing.Color.Silver;
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            string hata;

            string email = (txtemail.Text ?? "").Trim();
            string sifre = (txtsifre.Text ?? "").Trim();

            if (!OturumYoneticisi.GirisYap(email, sifre, out hata))
            {
                MessageBox.Show(hata);
                return;
            }

            // Debug için istersen:
            // MessageBox.Show("Rol: " + OturumYoneticisi.Rol + " | PersonelId: " + OturumYoneticisi.PersonelId);

            new AnaMenu().Show();
            this.Hide();
        }
    }
}


