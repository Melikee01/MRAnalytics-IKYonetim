using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class PerformansFormu : Form
    {
        private readonly PerformansYoneticisi _performansYoneticisi = new PerformansYoneticisi();
        private readonly PersonelYoneticisi _personelYoneticisi = new PersonelYoneticisi();

        private List<Personel> _personeller = new List<Personel>();
        private int _seciliPerformansId = 0;
        private List<Personel> _tumPersoneller = new List<Personel>();


        public PerformansFormu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225);
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.Load += PerformansFormu_Load;
        }

        private void PerformansFormu_Load(object sender, EventArgs e)
        {
            nudPuan.Minimum = 1;
            nudPuan.Maximum = 100;
            nudPuan.Value = 1;

            dtpTarih.Value = DateTime.Today;

            dgvPerformans.ReadOnly = true;
            dgvPerformans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPerformans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPerformans.MultiSelect = false;

            // Grid Stilleri
            dgvPerformans.BackgroundColor = System.Drawing.Color.White;
            dgvPerformans.EnableHeadersVisualStyles = false;
            dgvPerformans.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            dgvPerformans.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvPerformans.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvPerformans.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepPink;
            dgvPerformans.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            PersonelleriDoldur();
            ListeyiYenile();

            btnGuncelle.Enabled = false;
            btnTemizle.Enabled = false;
        }

        private void PersonelleriDoldur()
        {
            try
            {
                _tumPersoneller = _personelYoneticisi.TumPersonelleriGetir();

                // ComboBox için SADECE aktifler
                _personeller = _tumPersoneller.Where(p => p.Aktif).ToList();

                var kaynak = _personeller.Select(p => new
                {
                    p.Id,
                    AdSoyad = $"{p.Ad} {p.Soyad} ({p.Departman})"
                }).ToList();

                cmbPersonel.DataSource = null;
                cmbPersonel.DisplayMember = "AdSoyad";
                cmbPersonel.ValueMember = "Id";
                cmbPersonel.DataSource = kaynak;

                if (cmbPersonel.Items.Count > 0)
                    cmbPersonel.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel doldurma hatası: " + ex.Message);
            }
        }


        private void ListeyiYenile()
        {
            var liste = _performansYoneticisi.TumPerformanslar();

            // Grid'de ID yerine ad soyad göstermek için view-model oluşturuyoruz
            var view = liste.Select(x => new
            {
                x.Id,
                Personel = PersonelAdSoyadGetir(x.PersonelId),
                x.Puan,
                x.Aciklama,
                Tarih = x.DegerlendirmeTarihi,
                Degerlendiren = PersonelAdSoyadGetir(x.DegerlendirenId)
            }).ToList();

            dgvPerformans.DataSource = null;
            dgvPerformans.DataSource = view;

            if (dgvPerformans.Columns.Contains("Id"))
                dgvPerformans.Columns["Id"].Visible = false;
        }

        private string PersonelAdSoyadGetir(int personelId)
        {
            var p = _tumPersoneller.FirstOrDefault(x => x.Id == personelId);
            return p == null ? $"#{personelId}" : $"{p.Ad} {p.Soyad}";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (cmbPersonel.SelectedValue == null)
            {
                MessageBox.Show("Lütfen personel seçin.");
                return;
            }

            int personelId = Convert.ToInt32(cmbPersonel.SelectedValue);

            var kayit = new Performans
            {
                PersonelId = personelId,
                Puan = Convert.ToInt32(nudPuan.Value),
                Aciklama = (txtAciklama.Text ?? "").Trim(),
                DegerlendirmeTarihi = dtpTarih.Value.Date,
                DegerlendirenId = OturumYoneticisi.PersonelId // NOT NULL için şart
            };

            try
            {
                _performansYoneticisi.PerformansEkle(kayit);
                MessageBox.Show("Performans kaydedildi.");

                Temizle();
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (_seciliPerformansId <= 0)
            {
                MessageBox.Show("Güncellemek için listeden bir kayıt seçin.");
                return;
            }

            if (cmbPersonel.SelectedValue == null)
            {
                MessageBox.Show("Lütfen personel seçin.");
                return;
            }

            int personelId = Convert.ToInt32(cmbPersonel.SelectedValue);

            var guncel = new Performans
            {
                Id = _seciliPerformansId,
                PersonelId = personelId,
                Puan = Convert.ToInt32(nudPuan.Value),
                Aciklama = (txtAciklama.Text ?? "").Trim(),
                DegerlendirmeTarihi = dtpTarih.Value.Date,
                DegerlendirenId = OturumYoneticisi.PersonelId // güncelleyen kim? pratik: oturumdaki kişi
            };

            try
            {
                _performansYoneticisi.PerformansGuncelle(guncel);
                MessageBox.Show("Performans güncellendi.");

                Temizle();
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (_seciliPerformansId <= 0)
            {
                MessageBox.Show("Silmek için listeden bir kayıt seçin.");
                return;
            }

            var sonuc = MessageBox.Show("Seçili performans kaydı silinsin mi?", "Onay", MessageBoxButtons.YesNo);
            if (sonuc != DialogResult.Yes) return;

            try
            {
                // BLL’de yoksa doğrudan DAL çağırma. En doğrusu BLL’ye de eklemek.
                // Eğer PerformansYoneticisi içinde PerformansSil(int id) yazdıysan:
                _performansYoneticisi.PerformansPasifeAl(_seciliPerformansId);

                MessageBox.Show("Kayıt silindi.");
                Temizle();
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Grid’den satır seçince formu doldur
        private void dgvPerformans_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPerformans.Rows[e.RowIndex];
            _seciliPerformansId = Convert.ToInt32(row.Cells["Id"].Value);

            // View'da personel adını gösteriyoruz; tekrar ID'ye dönmek için seçili ID'yi DB'den çekmeye gerek kalmasın diye
            // daha pratik: grid'e Id gizli ama duruyor, personel seçimini ad üzerinden eşleyelim.
            // Personel sütunu "Ad Soyad" döndürüyor; ComboBox'ı eşlemek için personel ID lazım.
            // Bu yüzden: seçili kaydı DB’den çekmek yerine listeyi tekrar query etmek yerine,
            // burada basit yaklaşım: grid’de Personel ve Tarih üzerinden formu doldururuz.
            // En sağlamı: DAL/BLL’ye "Id ile getir" metodu eklemek. Şimdilik pratik yapalım:

            // Personel adı (örn "Ali Veli") → combobox'ta aynı başlangıçla bulmaya çalış
            string personelAd = row.Cells["Personel"].Value?.ToString() ?? "";

            var match = _personeller.FirstOrDefault(p => ($"{p.Ad} {p.Soyad}") == personelAd);
            if (match != null)
                cmbPersonel.SelectedValue = match.Id;

            nudPuan.Value = Convert.ToDecimal(row.Cells["Puan"].Value);
            txtAciklama.Text = row.Cells["Aciklama"].Value?.ToString() ?? "";
            dtpTarih.Value = Convert.ToDateTime(row.Cells["Tarih"].Value);

            btnGuncelle.Enabled = true;
            btnTemizle.Enabled = true;
        }

        private void Temizle()
        {
            _seciliPerformansId = 0;

            nudPuan.Value = 1;
            dtpTarih.Value = DateTime.Today;
            txtAciklama.Clear();

            if (cmbPersonel.Items.Count > 0)
                cmbPersonel.SelectedIndex = 0;

            btnGuncelle.Enabled = false;
            btnTemizle.Enabled = false;
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            if (_seciliPerformansId <= 0)
            {
                MessageBox.Show("Silmek için kayıt seçin.");
                return;
            }

            // İstersen onay sor:
            var sonuc = MessageBox.Show("Kayıt silinecek. Emin misiniz?",
                                        "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc != DialogResult.Yes) return;

            try
            {
                _performansYoneticisi.PerformansPasifeAl(_seciliPerformansId);
                MessageBox.Show("Kayıt silindi.");

                Temizle();
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

    }
}
