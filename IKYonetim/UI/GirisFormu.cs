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


            new AnaMenu().Show();
            this.Hide();
        }
    }
}


