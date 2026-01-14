
using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class IzinFormu : Form
    {
        private readonly IzinYoneticisi _izinYoneticisi = new IzinYoneticisi();

        public IzinFormu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225);
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
        }

        private void grpTalep_Enter(object sender, EventArgs e)
        {
            // Şimdilik boş bırakabilirsin
        }

        private void IzinFormu_Load(object sender, EventArgs e)
        {
            dgvIzinler.RowPrePaint -= dgvIzinler_RowPrePaint; // çift bağlanmayı önler
            dgvIzinler.RowPrePaint += dgvIzinler_RowPrePaint;

            // Grid Stilleri
            dgvIzinler.BackgroundColor = System.Drawing.Color.White;
            dgvIzinler.EnableHeadersVisualStyles = false;
            dgvIzinler.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            dgvIzinler.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvIzinler.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvIzinler.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepPink;
            dgvIzinler.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // ComboBox örnek değerler (istersen DB'den çekersin)
            if (cmbIzinTuru.Items.Count == 0)
            {
                cmbIzinTuru.Items.AddRange(new object[]
                {
                    "Yıllık İzin",
                    "Raporlu",
                    "Mazeret İzni"
                });
                cmbIzinTuru.SelectedIndex = 0;
            }

            YetkilereGoreEkran();
            ListeyiYenile();
        }

        private void YetkilereGoreEkran()
        {
            var rol = OturumYoneticisi.Rol;
            lblMod.Text = $"Mod: {rol}";

            // Varsayılan
            grpTalep.Visible = false;
            grpAdminOnay.Visible = false;

            // Varsayılan checkbox durumları (güvenli başlangıç)
            chkTumIzinler.Visible = false;
            chkTumIzinler.Checked = false;

            chkBekleyenler.Visible = false;
            chkBekleyenler.Checked = false;

            if (rol == "users" || rol == "Personel")
            {
                // User: talep var, onay yok
                grpTalep.Visible = true;
                grpAdminOnay.Visible = false;

                // User: filtre checkbox’ları yok
                chkTumIzinler.Visible = false;
                chkBekleyenler.Visible = false;
            }
            else if (rol == "IK")
            {
                // IK: talep var, onay yok
                grpTalep.Visible = true;
                grpAdminOnay.Visible = false;

                // IK: tüm izinler checkbox'ı açık (senin mevcut tasarımın)
                chkTumIzinler.Visible = true;
                chkTumIzinler.Checked = true;

                // IK: bekleyen filtresi yok
                chkBekleyenler.Visible = false;
                chkBekleyenler.Checked = false;
            }
            else if (rol == "Admin")
            {
                // Admin: talep yok, onay var
                grpTalep.Visible = false;
                grpAdminOnay.Visible = true;

                // Admin: tüm izinler checkbox'ı yok
                chkTumIzinler.Visible = false;
                chkTumIzinler.Checked = false;

                // ✅ Admin: bekleyen filtresi görünsün
                chkBekleyenler.Visible = true;
                chkBekleyenler.Checked = true;   // default: sadece bekleyenleri göster
            }
        }


        private void ListeyiYenile()
        {
            var rol = OturumYoneticisi.Rol;
            dgvIzinler.DataSource = null;

            if (rol == "Admin")
            {
                // ✅ Admin: bekleyenler checkbox'ına göre
                string durumFiltre = null;

                if (chkBekleyenler.Checked)
                    durumFiltre = "Beklemede";

                dgvIzinler.DataSource =
                    _izinYoneticisi.Listele(
                        personelId: null,
                        tumu: true,
                        durum: durumFiltre   // 👈 sadece admin kullanır
                    );
            }
            else if (rol == "IK")
            {
                // IK: checkbox'a göre
                if (chkTumIzinler.Checked)
                {
                    dgvIzinler.DataSource =
                        _izinYoneticisi.Listele(personelId: null, tumu: true);
                }
                else
                {
                    if (OturumYoneticisi.PersonelId <= 0)
                        throw new Exception("Oturum PersonelId bulunamadı.");

                    dgvIzinler.DataSource =
                        _izinYoneticisi.Listele(
                            personelId: OturumYoneticisi.PersonelId,
                            tumu: false
                        );
                }
            }
            else
            {
                // User / Personel
                if (OturumYoneticisi.PersonelId <= 0)
                    throw new Exception("Oturum PersonelId bulunamadı.");

                dgvIzinler.DataSource =
                    _izinYoneticisi.Listele(
                        personelId: OturumYoneticisi.PersonelId,
                        tumu: false
                    );
            }

            dgvIzinler.ReadOnly = true;
            dgvIzinler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIzinler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void btnTalepOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpBitis.Value.Date < dtpBaslangic.Value.Date)
                    throw new Exception("Bitiş tarihi başlangıç tarihinden küçük olamaz.");

                if (OturumYoneticisi.PersonelId <= 0)
                    throw new Exception("Oturum PersonelId bulunamadı. Lütfen yeniden giriş yap.");

                var izin = new Izin
                {
                    PersonelId = OturumYoneticisi.PersonelId,
                    BaslangicTarihi = dtpBaslangic.Value.Date,
                    BitisTarihi = dtpBitis.Value.Date,
                    IzinTuru = cmbIzinTuru.Text,
                    Aciklama = txtAciklama.Text
                };

                _izinYoneticisi.IzinTalepEt(izin);

                MessageBox.Show("İzin talebi oluşturuldu.");
                txtAciklama.Clear();
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int SeciliIzinId()
        {
            if (dgvIzinler.CurrentRow == null)
                throw new Exception("Lütfen listeden bir izin seç.");

            object val = null;

            if (dgvIzinler.Columns.Contains("id"))
                val = dgvIzinler.CurrentRow.Cells["id"].Value;
            else if (dgvIzinler.Columns.Contains("Id"))
                val = dgvIzinler.CurrentRow.Cells["Id"].Value;
            else
                val = dgvIzinler.CurrentRow.Cells[0].Value;

            return Convert.ToInt32(val);
        }

        // ✅ EKLENDİ: Seçili iznin durumunu getirir
        private string SeciliIzinDurum()
        {
            if (dgvIzinler.CurrentRow == null)
                throw new Exception("Lütfen bir izin seçiniz.");

            // Kolon adı DB'den "durum" veya "Durum" gelebilir
            if (dgvIzinler.Columns.Contains("durum"))
                return dgvIzinler.CurrentRow.Cells["durum"].Value?.ToString();

            if (dgvIzinler.Columns.Contains("Durum"))
                return dgvIzinler.CurrentRow.Cells["Durum"].Value?.ToString();

            throw new Exception("Durum bilgisi bulunamadı. Grid'de 'durum' kolonu yok.");
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            try
            {
                if (OturumYoneticisi.Rol != "Admin")
                    throw new Exception("Bu işlem için yetkin yok.");

                int id = SeciliIzinId();
                string durum = SeciliIzinDurum();

                _izinYoneticisi.DurumGuncelle(id, "Onaylandı");

                MessageBox.Show("İzin ONAYLANDI.");
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {
            try
            {
                if (OturumYoneticisi.Rol != "Admin")
                    throw new Exception("Bu işlem için yetkin yok.");

                int id = SeciliIzinId();
                string durum = SeciliIzinDurum();

                _izinYoneticisi.DurumGuncelle(id, "Reddedildi");

                MessageBox.Show("İzin REDDEDİLDİ.");
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
            // ... diğer kodlar
            private void dgvIzinler_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
            {
                var row = dgvIzinler.Rows[e.RowIndex];

                // kolon adı durum / Durum olabilir
                string durum = null;

                if (dgvIzinler.Columns.Contains("durum"))
                    durum = row.Cells["durum"].Value?.ToString();
                else if (dgvIzinler.Columns.Contains("Durum"))
                    durum = row.Cells["Durum"].Value?.ToString();

                if (durum == "Beklemede")
                    row.DefaultCellStyle.BackColor = Color.Khaki;
                else if (durum == "Onaylandı")
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                else if (durum == "Reddedildi")
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
            }

        private void chkTumIzinler_CheckedChanged(object sender, EventArgs e)
        {
            ListeyiYenile();
        }
        private void chkBekleyenler_CheckedChanged(object sender, EventArgs e)
        {
            ListeyiYenile();
        }

    }
}
