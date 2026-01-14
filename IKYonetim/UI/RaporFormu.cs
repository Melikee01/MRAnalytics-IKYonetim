using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Data;
using System.Windows.Forms;

namespace IKYonetim.UI
{
    public partial class RaporFormu : Form
    {
        private readonly RaporlamaYoneticisi _yonetici = new RaporlamaYoneticisi();

        public RaporFormu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225);
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.Load += RaporFormu_Load;
        }

        private void RaporFormu_Load(object sender, EventArgs e)
        {
            // Combo doldur
            cmbRapor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRapor.Items.Clear();
            cmbRapor.Items.Add(new ComboItem("Departman Bazlı Personel Dağılımı", RaporTipi.DepartmanBazliPersonelDagilimi));
            cmbRapor.Items.Add(new ComboItem("İzin Raporu", RaporTipi.IzinRaporu));
            cmbRapor.Items.Add(new ComboItem("Maaş Raporu", RaporTipi.MaasRaporu));
            cmbRapor.Items.Add(new ComboItem("Performans Raporu", RaporTipi.PerformansRaporu));
            cmbRapor.Items.Add(new ComboItem("İzin Hakediş Kontrolü", RaporTipi.IzinHakedisKontrolu));

            cmbRapor.SelectedIndexChanged += CmbRapor_SelectedIndexChanged;
            cmbRapor.SelectedIndex = 0;

            // Default filtre değerleri
            dtBas.Value = DateTime.Today.AddDays(-30);
            dtBit.Value = DateTime.Today;

            nudYil.Value = DateTime.Today.Year;
            nudAy.Value = DateTime.Today.Month;



            // Grid ayarı
            dgvRapor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRapor.ReadOnly = true;
            dgvRapor.AllowUserToAddRows = false;
            dgvRapor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Grid Stilleri - Vivid
            dgvRapor.BackgroundColor = System.Drawing.Color.White;
            dgvRapor.EnableHeadersVisualStyles = false;
            dgvRapor.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            dgvRapor.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvRapor.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvRapor.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepPink;
            dgvRapor.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            FiltrePanelleriniAyarla();
        }

        private void CmbRapor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrePanelleriniAyarla();
        }

        private void FiltrePanelleriniAyarla()
        {
            var tip = SeciliRaporTipi();

            // Hepsini kapat
            pnlTarih.Visible = false;
            PnlMaas.Visible = false;


            // Gerekene göre aç
            if (tip == RaporTipi.IzinRaporu)
                pnlTarih.Visible = true;

            if (tip == RaporTipi.MaasRaporu)
                PnlMaas.Visible = true;

            if (tip == RaporTipi.IzinHakedisKontrolu)
                PnlMaas.Visible = true; // yıl lazım (ay şart değil ama panel aynı)
        }

        private RaporTipi SeciliRaporTipi()
        {
            var ci = cmbRapor.SelectedItem as ComboItem;
            return ci == null ? RaporTipi.DepartmanBazliPersonelDagilimi : ci.Tip;
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;

                var tip = SeciliRaporTipi();

                if (tip == RaporTipi.DepartmanBazliPersonelDagilimi)
                {
                    dt = _yonetici.DepartmanBazliPersonelDagilimi();
                }
                else if (tip == RaporTipi.IzinRaporu)
                {
                    dt = _yonetici.IzinRaporu(dtBas.Value, dtBit.Value);
                }
                else if (tip == RaporTipi.MaasRaporu)
                {
                    dt = _yonetici.MaasRaporu((int)nudYil.Value, (int)nudAy.Value);
                }
                else if (tip == RaporTipi.PerformansRaporu)
                {
                    // nudTopN formda yoksa sabit de verebilirsin: dt = _yonetici.PerformansRaporu(5);
                    dt = _yonetici.PerformansRaporu(2);
                }

                else // IzinHakedisKontrolu
                {
                    dt = _yonetici.IzinHakedisKontrolu((int)nudYil.Value);
                }

                dgvRapor.DataSource = null;
                dgvRapor.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Rapor Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ComboBox için küçük yardımcı sınıf
        private class ComboItem
        {
            public string Text { get; private set; }
            public RaporTipi Tip { get; private set; }

            public ComboItem(string text, RaporTipi tip)
            {
                Text = text;
                Tip = tip;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void nudYil_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

