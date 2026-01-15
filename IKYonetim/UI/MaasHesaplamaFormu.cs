using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IKYonetim.UI
{

    public partial class MaasHesaplamaFormu : Form
    {
        private readonly PersonelYoneticisi _personelYoneticisi = new PersonelYoneticisi();
        private List<Personel> _aktifPersoneller = new List<Personel>();
        private int _seciliMaasId = 0;




        public MaasHesaplamaFormu()

        {
            InitializeComponent();

            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225);
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
        }

        private void MaasHesaplamaFormu_Load(object sender, EventArgs e)
        {

            dgvMaaslar.BackgroundColor = System.Drawing.Color.White;
            dgvMaaslar.EnableHeadersVisualStyles = false;
            dgvMaaslar.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            dgvMaaslar.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvMaaslar.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvMaaslar.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepPink;
            dgvMaaslar.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);


            cmbYil.Items.Clear();
            for (int yil = 2023; yil <= 2030; yil++)
                cmbYil.Items.Add(yil);


            cmbAy.Items.Clear();
            for (int ay = 1; ay <= 12; ay++)
                cmbAy.Items.Add(ay);

            if (cmbYil.Items.Count > 0) cmbYil.SelectedIndex = 0;
            if (cmbAy.Items.Count > 0) cmbAy.SelectedIndex = 0;


            PersonelComboDoldur();

            cmbPersonel.SelectedIndexChanged += (s, e2) => { dgvMaaslar.DataSource = null; };


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
        private void CmbPersonel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPersonel.SelectedValue == null) return;

                int pid = Convert.ToInt32(cmbPersonel.SelectedValue);

                var yonetici = new MaasYoneticisi();
                var veri = yonetici.PersonelGecmisi(pid);

                MaasGridBind(veri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                var veri = yonetici.PersonelGecmisi(maas.PersonelId);
                MaasGridBind(veri);

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

                int personelId = Convert.ToInt32(cmbPersonel.SelectedValue);


                MaasYoneticisi yonetici = new MaasYoneticisi();
                var veri = yonetici.PersonelGecmisi(personelId);
                MaasGridBind(veri);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string SeciliPersonelAdSoyad()
        {
            if (cmbPersonel.SelectedValue == null) return "";
            int id = Convert.ToInt32(cmbPersonel.SelectedValue);

            var p = _aktifPersoneller.Find(x => x.Id == id);
            return p == null ? $"#{id}" : $"{p.Ad} {p.Soyad}";
        }
        private void MaasGridBind(object data)
        {
            string adSoyad = SeciliPersonelAdSoyad();


            if (data is System.Data.DataTable dt)
            {

                if (!dt.Columns.Contains("Personel"))
                    dt.Columns.Add("Personel", typeof(string));

                foreach (System.Data.DataRow r in dt.Rows)
                    r["Personel"] = adSoyad;

                dgvMaaslar.DataSource = null;
                dgvMaaslar.DataSource = dt;


                if (dgvMaaslar.Columns.Contains("PersonelId"))
                    dgvMaaslar.Columns["PersonelId"].Visible = false;


                if (dgvMaaslar.Columns.Contains("Personel"))
                    dgvMaaslar.Columns["Personel"].DisplayIndex = 0;

                return;
            }


            var list = data as System.Collections.IEnumerable;
            if (list == null)
            {
                dgvMaaslar.DataSource = data;
                return;
            }


            var view = new List<object>();
            foreach (dynamic x in list)
            {
                view.Add(new
                {
                    x.Id,
                    Personel = adSoyad,
                    x.Yil,
                    x.Ay,
                    x.BrutMaas,
                    x.Prim,
                    x.Mesai,
                    Kesinti = x.KesintiToplam, 
                    x.Aciklama
                });
            }

            dgvMaaslar.DataSource = null;
            dgvMaaslar.DataSource = view;

            if (dgvMaaslar.Columns.Contains("Id"))
                dgvMaaslar.Columns["Id"].Visible = false;
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMaaslar.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen tablodan silinecek kaydı seçiniz.");
                    return;
                }

                if (!dgvMaaslar.Columns.Contains("Id"))
                {
                    MessageBox.Show("Silme için Id kolonu bulunamadı. (Grid bind kontrol et)");
                    return;
                }

                int maasId = Convert.ToInt32(dgvMaaslar.CurrentRow.Cells["Id"].Value);

                var onay = MessageBox.Show("Seçili maaş kaydı silinsin mi?", "Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (onay != DialogResult.Yes) return;

                var yonetici = new MaasYoneticisi();
                yonetici.Sil(maasId);

                MessageBox.Show("Maaş kaydı silindi.");


                int personelId = Convert.ToInt32(cmbPersonel.SelectedValue);
                var veri = yonetici.PersonelGecmisi(personelId);
                MaasGridBind(veri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvMaaslar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvMaaslar.Rows[e.RowIndex];


            if (row.Cells["Id"]?.Value == null) return;

            _seciliMaasId = Convert.ToInt32(row.Cells["Id"].Value);
            btnSil.Enabled = true;

            try
            {
                if (e.RowIndex < 0) return;

                var seciliRow = dgvMaaslar.Rows[e.RowIndex];



                if (row.Cells["Yil"]?.Value != null)
                    cmbYil.SelectedItem = Convert.ToInt32(row.Cells["Yil"].Value);

                if (row.Cells["Ay"]?.Value != null)
                    cmbAy.SelectedItem = Convert.ToInt32(row.Cells["Ay"].Value);


                txtBrut.Text = Convert.ToDecimal(row.Cells["BrutMaas"].Value).ToString("0.##");
                txtPrim.Text = Convert.ToDecimal(row.Cells["Prim"].Value).ToString("0.##");
                txtMesai.Text = Convert.ToDecimal(row.Cells["Mesai"].Value).ToString("0.##");
                txtKesinti.Text = Convert.ToDecimal(row.Cells["Kesinti"].Value).ToString("0.##");


                btnHesapla.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}