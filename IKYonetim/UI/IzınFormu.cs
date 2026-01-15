using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class IzinFormu : Form
    {
        private readonly IzinYoneticisi _izinYoneticisi = new IzinYoneticisi();

        public IzinFormu()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(255, 228, 225);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        private void IzinFormu_Load(object sender, EventArgs e)
        {
            dgvIzinler.RowPrePaint -= dgvIzinler_RowPrePaint;
            dgvIzinler.RowPrePaint += dgvIzinler_RowPrePaint;

            dgvIzinler.BackgroundColor = Color.White;
            dgvIzinler.EnableHeadersVisualStyles = false;
            dgvIzinler.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dgvIzinler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvIzinler.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvIzinler.DefaultCellStyle.SelectionBackColor = Color.DeepPink;
            dgvIzinler.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

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

            // ✅ Load biter bitmez listeyi çek (async çağıracağız)
            // WinForms Load event'i async yapılamadığı için Shown'da çağırıyoruz:
            this.Shown -= IzinFormu_Shown;
            this.Shown += IzinFormu_Shown;
        }

        private async void IzinFormu_Shown(object sender, EventArgs e)
        {
            try
            {
                await ListeyiYenileAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static bool IsRole(string role, string expected)
            => string.Equals(role?.Trim(), expected, StringComparison.OrdinalIgnoreCase);

        private void YetkilereGoreEkran()
        {
            var role = OturumYoneticisi.Rol;
            lblMod.Text = $"Mod: {role}";

            grpTalep.Visible = false;
            grpAdminOnay.Visible = false;

            chkTumIzinler.Visible = false;
            chkTumIzinler.Checked = false;

            chkBekleyenler.Visible = false;
            chkBekleyenler.Checked = false;

            if (IsRole(role, "users") || IsRole(role, "personel"))
            {
                grpTalep.Visible = true;
            }
            else if (IsRole(role, "ik"))
            {
                grpTalep.Visible = true;
                chkTumIzinler.Visible = true;
                chkTumIzinler.Checked = true;
            }
            else if (IsRole(role, "admin"))
            {
                grpAdminOnay.Visible = true;
                chkBekleyenler.Visible = true;
                chkBekleyenler.Checked = true;
            }

            grpTalep.BringToFront();
            dgvIzinler.BringToFront();
            chkTumIzinler.BringToFront();
            this.Refresh();
        }

        // ✅ UI donmasın diye async
        private async Task ListeyiYenileAsync()
        {
            var rol = OturumYoneticisi.Rol;
            dgvIzinler.DataSource = null;

            if (IsRole(rol, "admin"))
            {
                string durumFiltre = chkBekleyenler.Checked ? "Beklemede" : null;



                // Data fetching directly
                var data = await _izinYoneticisi.ListeleAsync(
                    personelId: null,
                    tumu: true,
                    durum: durumFiltre
                );

                dgvIzinler.DataSource = null; // Reset first
                dgvIzinler.DataSource = data;
            }
            else if (IsRole(rol, "ik"))
            {
                if (chkTumIzinler.Checked)
                {
                    var data = await _izinYoneticisi.ListeleAsync(personelId: null, tumu: true);
                    dgvIzinler.DataSource = null;
                    dgvIzinler.DataSource = data;
                }
                else
                {
                    if (OturumYoneticisi.PersonelId <= 0)
                        throw new Exception("Oturum PersonelId bulunamadı.");

                    var data = await _izinYoneticisi.ListeleAsync(
                        personelId: OturumYoneticisi.PersonelId,
                        tumu: false
                    );
                    dgvIzinler.DataSource = null;
                    dgvIzinler.DataSource = data;
                }
            }
            else
            {
                if (OturumYoneticisi.PersonelId <= 0)
                    throw new Exception("Oturum PersonelId bulunamadı.");

                // Data fetching directly
                var data = await _izinYoneticisi.ListeleAsync(
                    personelId: OturumYoneticisi.PersonelId,
                    tumu: false
                );

                dgvIzinler.DataSource = null;
                dgvIzinler.DataSource = data;
            }

            dgvIzinler.ReadOnly = true;
            dgvIzinler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIzinler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvIzinler.Refresh();
        }

        private void SetBusy(bool busy)
        {
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;

            // Disable entire groups to ensure all controls are managed
            grpAdminOnay.Enabled = !busy;
            grpTalep.Enabled = !busy;

            // filtreler
            chkTumIzinler.Enabled = !busy;
            chkBekleyenler.Enabled = !busy;

            // grid tıklanabilir kalsın ama isterse kapat
            dgvIzinler.Enabled = !busy;
            dtpBaslangic.Enabled = !busy;
            dtpBitis.Enabled = !busy;
        }

        private async void btnTalepOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                // UI Validation
                if (dtpBitis.Value.Date < dtpBaslangic.Value.Date)
                    throw new Exception("Bitiş tarihi başlangıç tarihinden küçük olamaz.");

                if (OturumYoneticisi.PersonelId <= 0)
                    throw new Exception("Oturum PersonelId bulunamadı.");

                var izin = new Izin
                {
                    PersonelId = OturumYoneticisi.PersonelId,
                    BaslangicTarihi = dtpBaslangic.Value.Date,
                    BitisTarihi = dtpBitis.Value.Date,
                    IzinTuru = cmbIzinTuru.Text,
                    Aciklama = txtAciklama.Text
                };

                // 1. Capture context variables needed for background thread
                int pid = OturumYoneticisi.PersonelId;

                // 2. Run EVERYTHING in background (Fire and Forget style for UI responsiveness)
                await Task.Run(async () => 
                {
                    // A. Insert
                    await _izinYoneticisi.IzinTalepEtAsync(izin);

                    // B. Fetch Updated Data (Background)
                    var data = await _izinYoneticisi.ListeleAsync(
                        personelId: pid,
                        tumu: false
                    );
                    
                    // C. Update UI (Marshal back to UI Thread)
                    this.Invoke((Action)(async () => 
                    {
                        dgvIzinler.DataSource = null;
                        dgvIzinler.DataSource = data;
                        dgvIzinler.Refresh();
                        
                        txtAciklama.Clear();
                        
                        // Non-blocking Feedback: Change button text briefly
                        var oldText = btnTalepOlustur.Text;
                        var oldColor = btnTalepOlustur.BackColor;
                        
                        btnTalepOlustur.Text = "✅ Oluşturuldu!";
                        btnTalepOlustur.BackColor = Color.LightGreen;
                        
                        // Small delay to show success, then revert
                        await Task.Delay(2000);
                        
                        // Verify control still exists (form might be closed)
                        if (!btnTalepOlustur.IsDisposed)
                        {
                            btnTalepOlustur.Text = oldText;
                            btnTalepOlustur.BackColor = oldColor;
                        }
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int SeciliIzinId()
        {
            if (dgvIzinler.CurrentRow == null)
                throw new Exception("Lütfen listeden bir izin seç.");

            object val;

            if (dgvIzinler.Columns.Contains("id"))
                val = dgvIzinler.CurrentRow.Cells["id"].Value;
            else if (dgvIzinler.Columns.Contains("Id"))
                val = dgvIzinler.CurrentRow.Cells["Id"].Value;
            else
                val = dgvIzinler.CurrentRow.Cells[0].Value;

            return Convert.ToInt32(val);
        }

        private async void btnOnayla_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRole(OturumYoneticisi.Rol, "admin"))
                    throw new Exception("Bu işlem için yetkin yok.");

                int id = SeciliIzinId();

                // 1. Capture context
                string durumFiltre = chkBekleyenler.Checked ? "Beklemede" : null;
                int currentPid = OturumYoneticisi.PersonelId;

                // 2. Background Worker
                await Task.Run(async () => 
                {
                    // A. Update DB
                    await _izinYoneticisi.DurumGuncelleAsync(id, "Onaylandı");

                    // B. Fetch Data
                    var data = await _izinYoneticisi.ListeleAsync(
                        personelId: null,
                        tumu: true,
                        durum: durumFiltre
                    );

                    // C. UI Update
                    this.Invoke((Action)(async () => 
                    {
                        dgvIzinler.DataSource = null;
                        dgvIzinler.DataSource = data;
                        dgvIzinler.Refresh();

                        // Non-blocking Feedback
                        var oldText = btnOnayla.Text;
                        var oldColor = btnOnayla.BackColor;
                        
                        btnOnayla.Text = "✅ Onaylandı!";
                        btnOnayla.BackColor = Color.LightGreen;
                        
                        await Task.Delay(2000);
                        
                        if (!btnOnayla.IsDisposed)
                        {
                            btnOnayla.Text = oldText;
                            btnOnayla.BackColor = oldColor;
                        }
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReddet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRole(OturumYoneticisi.Rol, "admin"))
                    throw new Exception("Bu işlem için yetkin yok.");

                int id = SeciliIzinId();

                // 1. Capture context
                string durumFiltre = chkBekleyenler.Checked ? "Beklemede" : null;

                // 2. Background Worker
                await Task.Run(async () => 
                {
                    // A. Update DB
                    await _izinYoneticisi.DurumGuncelleAsync(id, "Reddedildi");

                    // B. Fetch Data
                    var data = await _izinYoneticisi.ListeleAsync(
                        personelId: null,
                        tumu: true,
                        durum: durumFiltre
                    );

                    // C. UI Update
                    this.Invoke((Action)(async () => 
                    {
                        dgvIzinler.DataSource = null;
                        dgvIzinler.DataSource = data;
                        dgvIzinler.Refresh();

                        // Non-blocking Feedback
                        var oldText = btnReddet.Text;
                        var oldColor = btnReddet.BackColor;
                        
                        btnReddet.Text = "❌ Reddedildi!";
                        btnReddet.BackColor = Color.LightCoral;
                        
                        await Task.Delay(2000);
                        
                        if (!btnReddet.IsDisposed)
                        {
                            btnReddet.Text = oldText;
                            btnReddet.BackColor = oldColor;
                        }
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvIzinler_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dgvIzinler.Rows[e.RowIndex];

            row.DefaultCellStyle.BackColor = dgvIzinler.DefaultCellStyle.BackColor;

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

        private async void chkTumIzinler_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetBusy(true);
                await ListeyiYenileAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void chkBekleyenler_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetBusy(true);
                await ListeyiYenileAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }
    }
}
