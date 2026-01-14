using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class PersonelYonetimiFormu : Form
    {
        private bool _formIlkAcilis = true;

        private readonly PersonelYoneticisi _yonetici = new PersonelYoneticisi();
  

        public PersonelYonetimiFormu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225);
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);

            this.Load += PersonelYonetimiFormu_Load;

            // ✅ Seçenek B: chkAktif artık liste filtresi
            // Designer'da Text'i: "Sadece Aktifleri Göster" yapmanı öneririm.
            chkAktif.CheckedChanged += (s, e) => ListeyiYenile();
            if (cmbRol.Items.Count == 0)
            {
                cmbRol.Items.Add("IK");
                cmbRol.Items.Add("users");
                cmbRol.SelectedIndex = 2;
            }

        }

        private void PersonelYonetimiFormu_Load(object sender, EventArgs e)
        {
            DepartmanlariDoldur();

            // Varsayılan: sadece aktifleri göster (istersen Designer'da da Checked=true yap)
            // chkAktif.Checked = true;

            // Grid Stilleri
            dgvPersonel.BackgroundColor = System.Drawing.Color.White;
            dgvPersonel.EnableHeadersVisualStyles = false;
            dgvPersonel.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            dgvPersonel.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvPersonel.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvPersonel.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepPink;
            dgvPersonel.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            ListeyiYenile();
            YetkiyeGoreButonlar();
            _formIlkAcilis = false;
        }

        private void YetkiyeGoreButonlar()
        {
            var rol = (OturumYoneticisi.Rol ?? "").Trim();

            bool yetkili =
                rol.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                rol.Equals("IK", StringComparison.OrdinalIgnoreCase);

            btnEkle.Enabled = yetkili;
            btnGuncelle.Enabled = yetkili;
            btnSil.Enabled = yetkili;
        }

        private void DepartmanlariDoldur()
        {
            var depts = new List<string> { "İK", "Muhasebe", "Satış", "Üretim", "IT", "Kalite Kontrol", "Lojistik", "Yönetim" };
            cmbDepartman.DataSource = depts;
        }

        private void ListeyiYenile()
        {
            // 1) BLL’den tüm listeyi al (N-katmanlı mimari bozulmaz)
            var liste = _yonetici.TumPersonelleriGetir();

            // 2) chkAktif filtre: işaretliyse sadece aktifleri göster
            if (chkAktif.Checked)
                liste = liste.Where(p => p.Aktif).ToList();

            dgvPersonel.DataSource = null;
            dgvPersonel.DataSource = liste;

            dgvPersonel.ReadOnly = true;
            dgvPersonel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvPersonel.Columns.Contains("Aktif"))
                dgvPersonel.Columns["Aktif"].HeaderText = "Aktif";

            if (dgvPersonel.Columns.Contains("Id"))
                dgvPersonel.Columns["Id"].Visible = false;

            // (Opsiyonel) Pasifler görünüyorsa gri yap (chkAktif kapalıyken)
            if (!chkAktif.Checked && dgvPersonel.Columns.Contains("Aktif"))
            {
                foreach (DataGridViewRow row in dgvPersonel.Rows)
                {
                    if (row.IsNewRow) continue;
                    bool aktif = Convert.ToBoolean(row.Cells["Aktif"].Value);
                    if (!aktif)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                        row.DefaultCellStyle.BackColor = Color.Gainsboro;
                    }
                }
            }
        }
        
        private int SeciliId()
        {
            if (dgvPersonel.CurrentRow == null) return 0;

            if (dgvPersonel.Columns.Contains("Id"))
                return Convert.ToInt32(dgvPersonel.CurrentRow.Cells["Id"].Value);

            if (dgvPersonel.Columns.Contains("id"))
                return Convert.ToInt32(dgvPersonel.CurrentRow.Cells["id"].Value);

            return Convert.ToInt32(dgvPersonel.CurrentRow.Cells[0].Value);
        }

        private void dgvPersonel_CellClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex < 0) return;

    var row = dgvPersonel.Rows[e.RowIndex];

    txtAd.Text = Convert.ToString(row.Cells["Ad"].Value);
    txtSoyad.Text = Convert.ToString(row.Cells["Soyad"].Value);
    txtPozisyon.Text = Convert.ToString(row.Cells["Pozisyon"].Value);

    var dep = Convert.ToString(row.Cells["Departman"].Value);
    cmbDepartman.SelectedItem = dep;
}


        private bool SeciliPersonelAktifMi()
        {
            if (dgvPersonel.CurrentRow == null) return true;
            if (!dgvPersonel.Columns.Contains("Aktif")) return true;

            return Convert.ToBoolean(dgvPersonel.CurrentRow.Cells["Aktif"].Value);
        }
        


        private Personel FormdanOku()
        {
            // chkAktif filtre olduğu için Aktif'i buradan okumuyoruz.
            // Güncelleme yaparken seçili personelin mevcut aktifliğini koruyoruz.
            bool mevcutAktif = SeciliPersonelAktifMi();
           


            return new Personel
            {
                Ad = txtAd.Text,
                Soyad = txtSoyad.Text,
                Departman = Convert.ToString(cmbDepartman.SelectedItem),
                Pozisyon = txtPozisyon.Text,
                Aktif = mevcutAktif,
                
            };
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                Personel p = new Personel
                {
                    Ad = txtAd.Text.Trim(),
                    Soyad = txtSoyad.Text.Trim(),
                    Departman = cmbDepartman.Text.Trim(),
                    Pozisyon = txtPozisyon.Text.Trim(),
                    Aktif = true,
                    YillikIzinHakki = 14
                };

                string email = txtemail.Text.Trim();
                string rol = cmbRol.Text.Trim();

                _yonetici.PersonelVeUsersEkle(p, email, rol);

                MessageBox.Show("Personel + kullanıcı oluşturuldu.\nBaşlangıç şifre: 1234");
                ListeyiYenile();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int id = SeciliId();
                if (id <= 0) throw new Exception("Güncellenecek personeli seç.");

                var p = FormdanOku();
                p.Id = id;

                _yonetici.PersonelGuncelle(p);

                ListeyiYenile();
                MessageBox.Show("Personel güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = SeciliId();
                if (id <= 0)
                    throw new Exception("Pasife alınacak personeli seç.");

                DialogResult onay = MessageBox.Show(
                    "Bu personeli pasife almak istiyor musun?\n(Personel silinmeyecek)",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (onay != DialogResult.Yes)
                    return;

                // Seçili personelin alanlarını al, sadece Aktif=false yap
                var p = FormdanOku();
                p.Id = id;
                p.Aktif = false;

                _yonetici.PersonelAktiflikVeIzinDegistir(id, false);
                ListeyiYenile();
                MessageBox.Show("Personel pasife alındı. Yıllık izin 0landı.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int SeciliPersonelId()
        {
            if (dgvPersonel.CurrentRow == null) return 0;
            return Convert.ToInt32(dgvPersonel.CurrentRow.Cells["Id"].Value);
        }

        private void btnAktifeAl_Click(object sender, EventArgs e)
        {
            try
            {
                int id = SeciliPersonelId();
                if (id <= 0)
                    throw new Exception("Aktife alınacak personeli seç.");

                _yonetici.PersonelAktiflikVeIzinDegistir(id, true);
                ListeyiYenile();
                MessageBox.Show("Personel aktife alındı. Yıllık izin 14 oldu.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormuTemizle()
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtPozisyon.Clear();
            txtemail.Clear();

            cmbDepartman.SelectedIndex = -1;
            cmbRol.SelectedIndex = -1;
        }

    }
}

