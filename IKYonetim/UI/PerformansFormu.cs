using IKYonetim.BLL;
using IKYonetim.DAL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class PerformansFormu : Form
    {
        // ===== BLL / DAL =====
        private readonly PerformansYoneticisi _performansYoneticisi = new PerformansYoneticisi();
        private readonly PersonelYoneticisi _personelYoneticisi = new PersonelYoneticisi();
        private readonly UsersDeposu _kullaniciDeposu = new UsersDeposu();

        // ===== STATE =====
        private List<Personel> _tumPersoneller = new List<Personel>();
        private List<Personel> _aktifPersoneller = new List<Personel>();
        private int _seciliPerformansId = 0;

        public PerformansFormu()
        {
            InitializeComponent();
            Load += PerformansFormu_Load;
        }

        // =========================================================
        // FORM LOAD
        // =========================================================
        private void PerformansFormu_Load(object sender, EventArgs e)
        {
            nudPuan.Minimum = 1;
            nudPuan.Maximum = 100;
            nudPuan.Value = 1;

            dtpTarih.MaxDate = DateTime.Today;
            dtpTarih.Value = DateTime.Today;

            dgvPerformans.ReadOnly = true;
            dgvPerformans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPerformans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPerformans.MultiSelect = false;

            PersonelleriDoldur();
            ListeyiYenile();

            btnGuncelle.Enabled = false;
            btnTemizle.Enabled = false;
        }

        // =========================================================
        // PERSONEL COMBO
        // =========================================================
        private void PersonelleriDoldur()
        {
            try
            {
                _tumPersoneller = _personelYoneticisi.TumPersonelleriGetir();
                _aktifPersoneller = _tumPersoneller.Where(p => p.Aktif).ToList();

                var kaynak = _aktifPersoneller.Select(p => new
                {
                    p.Id,
                    AdSoyad = p.Ad + " " + p.Soyad + " (" + p.Departman + ")"
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

        // =========================================================
        // LİSTELEME
        // =========================================================
        private void ListeyiYenile()
        {
            var liste = _performansYoneticisi.TumPerformanslar();

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

            if (dgvPerformans.Columns["Id"] != null)
                dgvPerformans.Columns["Id"].Visible = false;
        }

        private string PersonelAdSoyadGetir(int personelId)
        {
            Personel p = _tumPersoneller.FirstOrDefault(x => x.Id == personelId);
            return p == null ? "#" + personelId : p.Ad + " " + p.Soyad;
        }

        // =========================================================
        // KAYDET
        // =========================================================
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (cmbPersonel.SelectedValue == null)
            {
                MessageBox.Show("Lütfen personel seçin.");
                return;
            }

            Performans kayit = new Performans
            {
                PersonelId = Convert.ToInt32(cmbPersonel.SelectedValue),
                Puan = Convert.ToInt32(nudPuan.Value),
                Aciklama = txtAciklama.Text == null ? "" : txtAciklama.Text.Trim(),
                DegerlendirmeTarihi = dtpTarih.Value.Date,
                DegerlendirenId = OturumYoneticisi.PersonelId
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

        // =========================================================
        // GÜNCELLE
        // =========================================================
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seciliPerformansId <= 0)
                {
                    MessageBox.Show("Güncellemek için kayıt seçin.");
                    return;
                }

                // --- Rol Kontrolü ---
                if (string.Equals(OturumYoneticisi.Rol, "IK", StringComparison.OrdinalIgnoreCase))
                {
                    int hedefPersonelId = Convert.ToInt32(cmbPersonel.SelectedValue);
                    string hedefRol = _kullaniciDeposu.PersonelinRolunuGetir(hedefPersonelId);

                    if (!string.Equals(hedefRol, "users", StringComparison.OrdinalIgnoreCase))
                        throw new Exception("İK, Admin/İK personelin performans kaydını güncelleyemez.");
                }

                Performans guncel = new Performans
                {
                    Id = _seciliPerformansId,
                    PersonelId = Convert.ToInt32(cmbPersonel.SelectedValue),
                    Puan = Convert.ToInt32(nudPuan.Value),
                    Aciklama = txtAciklama.Text == null ? "" : txtAciklama.Text.Trim(),
                    DegerlendirmeTarihi = dtpTarih.Value.Date,
                    DegerlendirenId = OturumYoneticisi.PersonelId
                };

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

        // =========================================================
        // GRID CLICK
        // =========================================================
        private void dgvPerformans_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvPerformans.Rows[e.RowIndex];
            _seciliPerformansId = Convert.ToInt32(row.Cells["Id"].Value);

            string personelAd = row.Cells["Personel"].Value.ToString();
            Personel match = _aktifPersoneller.FirstOrDefault(p => (p.Ad + " " + p.Soyad) == personelAd);

            if (match != null)
                cmbPersonel.SelectedValue = match.Id;

            nudPuan.Value = Convert.ToDecimal(row.Cells["Puan"].Value);
            txtAciklama.Text = row.Cells["Aciklama"].Value == null ? "" : row.Cells["Aciklama"].Value.ToString();
            dtpTarih.Value = Convert.ToDateTime(row.Cells["Tarih"].Value);

            // --- Rol Kontrolü ---
            if (string.Equals(OturumYoneticisi.Rol, "IK", StringComparison.OrdinalIgnoreCase))
            {
                int hedefPersonelId = Convert.ToInt32(cmbPersonel.SelectedValue);
                string hedefRol = _kullaniciDeposu.PersonelinRolunuGetir(hedefPersonelId);

                if (string.IsNullOrEmpty(hedefRol) ||
                    !string.Equals(hedefRol, "users", StringComparison.OrdinalIgnoreCase))
                {
                    btnGuncelle.Enabled = false;
                    btnTemizle.Enabled = true;
                    MessageBox.Show("İK, Admin/İK personelin performans kaydını güncelleyemez.");
                    return;
                }
            }

            btnGuncelle.Enabled = true;
            btnTemizle.Enabled = true;
        }

        // =========================================================
        // PASİFE AL
        // =========================================================
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            if (_seciliPerformansId <= 0)
            {
                MessageBox.Show("Silmek için kayıt seçin.");
                return;
            }

            DialogResult sonuc = MessageBox.Show(
                "Kayıt silinecek. Emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

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

        // =========================================================
        // FORM RESET
        // =========================================================
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
    }
}
