using IKYonetim.BLL;
using ikYonetimNYPProjesi.BLL;
using System;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class SifreDegistirFormu : Form
    {
        public SifreDegistirFormu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225);
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);

            // Password char ayarları
            txtEskiSifre.UseSystemPasswordChar = true;
            txtYeniSifre.UseSystemPasswordChar = true;
            txtYeniSifreTekrar.UseSystemPasswordChar = true;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                int personelId = OturumYoneticisi.PersonelId;

                SifreYoneticisi yonetici = new SifreYoneticisi();

                yonetici.SifreDegistir(
                    personelId,
                    txtEskiSifre.Text,
                    txtYeniSifre.Text,
                    txtYeniSifreTekrar.Text
                );

                MessageBox.Show(
                    "Şifre başarıyla değiştirildi.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtEskiSifre.Clear();
                txtYeniSifre.Clear();
                txtYeniSifreTekrar.Clear();
                txtEskiSifre.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}