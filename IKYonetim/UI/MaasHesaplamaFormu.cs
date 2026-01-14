using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class MaasHesaplamaFormu : Form
    {

        private readonly PersonelYoneticisi _personelYoneticisi = new PersonelYoneticisi();
        private List<Personel> _aktifPersoneller = new List<Personel>();

        public MaasHesaplamaFormu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225);
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            txtBrut.TextChanged += txtPara_TextChanged;
            txtPrim.TextChanged += txtPara_TextChanged;
            txtMesai.TextChanged += txtPara_TextChanged;
            txtKesinti.TextChanged += txtPara_TextChanged;
        }

        private void MaasHesaplamaFormu_Load(object sender, EventArgs e)
        {
            // Grid Stilleri
            dgvMaaslar.BackgroundColor = System.Drawing.Color.White;
            dgvMaaslar.EnableHeadersVisualStyles = false;
            dgvMaaslar.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            dgvMaaslar.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvMaaslar.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvMaaslar.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepPink;
            dgvMaaslar.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // Yıl
            cmbYil.Items.Clear();
            for (int yil = 2020; yil <= 2030; yil++)
                cmbYil.Items.Add(yil);

            // Ay
            cmbAy.Items.Clear();
            for (int ay = 1; ay <= 12; ay++)
                cmbAy.Items.Add(ay);

            if (cmbYil.Items.Count > 0) cmbYil.SelectedIndex = 0;
            if (cmbAy.Items.Count > 0) cmbAy.SelectedIndex = 0;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");

            // Personel combobox doldur
            PersonelComboDoldur();
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                decimal brut = decimal.Parse(txtBrut.Text);
                decimal prim = decimal.Parse(txtPrim.Text);
                decimal mesai = decimal.Parse(txtMesai.Text);
                decimal kesinti = decimal.Parse(txtKesinti.Text);

                decimal net = brut + prim + mesai - kesinti;
                lblNetMaas.Text = net.ToString("N2") + " ₺";
            }
            catch
            {
                MessageBox.Show("Lütfen maaş alanlarını doğru giriniz.");
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!OturumYoneticisi.GirisYapildiMi)
            {
                MessageBox.Show("Oturum bulunamadı. Lütfen tekrar giriş yapın.");
                return;
            }

            try
            {
                if (cmbPersonel.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen personel seçiniz.");
                    return;
                }

                if (cmbYil.SelectedItem == null || cmbAy.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen yıl ve ay seçiniz.");
                    return;
                }

                Maas maas = new Maas
                {
                    // Artık txtPersonelId yok: seçili personelden alıyoruz
                    PersonelId = Convert.ToInt32(cmbPersonel.SelectedValue),
                    Yil = Convert.ToInt32(cmbYil.SelectedItem),
                    Ay = Convert.ToInt32(cmbAy.SelectedItem),

                    BrutMaas = decimal.Parse(txtBrut.Text),
                    Prim = decimal.Parse(txtPrim.Text),
                    Mesai = decimal.Parse(txtMesai.Text),
                    KesintiToplam = decimal.Parse(txtKesinti.Text),

                    HesaplamaTarihi = DateTime.Now,
                    Aciklama = (txtAciklama.Text ?? "").Trim()
                };

                MaasYoneticisi yonetici = new MaasYoneticisi();
                yonetici.Kaydet(maas);

                MessageBox.Show("Maaş kaydedildi.");

                // Kaydettikten sonra seçili personelin geçmişini yenile
                dgvMaaslar.DataSource = yonetici.PersonelGecmisi(maas.PersonelId);
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen Brüt/Prim/Mesai/Kesinti alanlarına sayısal değer giriniz.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPersonel.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen personel seçiniz.");
                    return;
                }

                MaaslariListeleSeciliPersonel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PersonelComboDoldur()
        {
            _aktifPersoneller = _personelYoneticisi.AktifPersonelleriGetir();

            var kaynak = new List<PersonelComboItem>();
            foreach (var p in _aktifPersoneller)
            {
                kaynak.Add(new PersonelComboItem
                {
                    Id = p.Id,
                    Gosterim = (p.Ad + " " + p.Soyad + " | " + p.Departman)
                });
            }

            cmbPersonel.DataSource = null;
            cmbPersonel.DisplayMember = "Gosterim";
            cmbPersonel.ValueMember = "Id";
            cmbPersonel.DataSource = kaynak;

            if (cmbPersonel.Items.Count > 0)
                cmbPersonel.SelectedIndex = 0;
        }

        private class PersonelComboItem
        {
            public int Id { get; set; }
            public string Gosterim { get; set; }
        }
        private class MaasGridRow
        {
            public int Id { get; set; }              // DB silme için lazım (gizleyeceğiz)
            public int PersonelId { get; set; }
            public string AdSoyad { get; set; }      // Id yerine göstereceğiz
            public int Yil { get; set; }
            public int Ay { get; set; }
            public decimal BrutMaas { get; set; }
            public decimal Prim { get; set; }
            public decimal Mesai { get; set; }
            public decimal KesintiToplam { get; set; }
            public decimal NetMaas { get; set; }
            public DateTime HesaplamaTarihi { get; set; }
            public string Aciklama { get; set; }
        }
        private List<MaasGridRow> GridVerisiHazirla(List<Maas> maaslar)
        {
            var rows = new List<MaasGridRow>();

            foreach (var m in maaslar)
            {
                // _aktifPersoneller zaten combobox doldururken çekiliyor
                var p = _aktifPersoneller.Find(x => x.Id == m.PersonelId);
                string adSoyad = p == null ? ("#" + m.PersonelId) : (p.Ad + " " + p.Soyad);

                rows.Add(new MaasGridRow
                {
                    Id = m.Id,
                    PersonelId = m.PersonelId,
                    AdSoyad = adSoyad,
                    Yil = m.Yil,
                    Ay = m.Ay,
                    BrutMaas = m.BrutMaas,
                    Prim = m.Prim,
                    Mesai = m.Mesai,
                    KesintiToplam = m.KesintiToplam,
                    NetMaas = m.NetMaas,
                    HesaplamaTarihi = m.HesaplamaTarihi,
                    Aciklama = m.Aciklama
                });
            }

            return rows;
        }
        private void MaaslariListeleSeciliPersonel()
        {
            if (cmbPersonel.SelectedValue == null) return;

            int personelId = Convert.ToInt32(cmbPersonel.SelectedValue);

            MaasYoneticisi yonetici = new MaasYoneticisi();
            var maaslar = yonetici.PersonelGecmisi(personelId);

            dgvMaaslar.AutoGenerateColumns = true;
            dgvMaaslar.DataSource = GridVerisiHazirla(maaslar);

            // Kolon düzeni:
            if (dgvMaaslar.Columns["Id"] != null)
                dgvMaaslar.Columns["Id"].Visible = false; // Id gizli

            if (dgvMaaslar.Columns["AdSoyad"] != null)
                dgvMaaslar.Columns["AdSoyad"].HeaderText = "Ad Soyad";
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMaaslar.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen silmek için bir satır seçiniz.");
                    return;
                }

                // GridVerisiHazirla ile DataSource bağladık: satır MaasGridRow
                var rowObj = dgvMaaslar.CurrentRow.DataBoundItem as MaasGridRow;
                if (rowObj == null || rowObj.Id <= 0)
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı.");
                    return;
                }

                var onay = MessageBox.Show("Seçili maaş kaydı silinsin mi?", "Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (onay != DialogResult.Yes) return;

                MaasYoneticisi yonetici = new MaasYoneticisi();
                yonetici.Sil(rowObj.Id);

                MessageBox.Show("Kayıt silindi.");

                MaaslariListeleSeciliPersonel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool _formatlaniyor = false;

        private void txtPara_TextChanged(object sender, EventArgs e)
        {
            if (_formatlaniyor) return;

            var tb = sender as TextBox;
            if (tb == null) return;

            string text = tb.Text;

            // Sadece rakamları al
            string digits = "";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsDigit(c)) digits += c;
            }

            if (digits.Length == 0)
            {
                _formatlaniyor = true;
                tb.Text = "";
                _formatlaniyor = false;
                return;
            }

            // Çok büyümesin diye (isteğe bağlı)
            if (digits.Length > 12) digits = digits.Substring(0, 12);

            decimal value = decimal.Parse(digits); // 25000

            _formatlaniyor = true;
            tb.Text = value.ToString("N0", new CultureInfo("tr-TR")); // 25.000
            tb.SelectionStart = tb.Text.Length; // imleç sona
            _formatlaniyor = false;
        }


    }

}
