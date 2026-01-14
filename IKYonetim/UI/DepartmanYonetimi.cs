using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IKYonetim.UI

{
    public partial class DepartmanYonetimi : Form
    {
        private readonly DepartmanYoneticisi _yonetici = new DepartmanYoneticisi();

        public DepartmanYonetimi()
        {
            InitializeComponent();
        }

        private void DepartmanYonetimi_Load(object sender, EventArgs e)
        {
            ListeyiYenile();
        }
        private void ListeyiYenile()
        {
            List<Departman> liste;

            // Pasifler dahil tüm departmanlar
            if (chkPasifleriGoster.Checked)
                liste = _yonetici.TumDepartmanlar();
            else
                // Yalnızca aktif departmanlar
                liste = _yonetici.AktifDepartmanlariGetir();

            dgvDepartman.DataSource = null;
            dgvDepartman.DataSource = liste;

            dgvDepartman.ReadOnly = true;
            dgvDepartman.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartman.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvDepartman.Columns.Contains("Id"))
                dgvDepartman.Columns["Id"].Visible = false;
            PasifSatirlariGriYap();
        }


        private int SeciliId()
        {
            if (dgvDepartman.CurrentRow == null) return 0;
            return Convert.ToInt32(dgvDepartman.CurrentRow.Cells["Id"].Value);
        }

        private void dgvDepartman_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDepartman.CurrentRow == null) return;
            txtDepartmanAdi.Text = Convert.ToString(dgvDepartman.CurrentRow.Cells["DepartmanAdi"].Value);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                _yonetici.DepartmanEkle(txtDepartmanAdi.Text);
                txtDepartmanAdi.Clear();
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
                _yonetici.DepartmanGuncelle(id, txtDepartmanAdi.Text);
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPasifeAl_Click(object sender, EventArgs e)
        {
            int id = SeciliId();
            _yonetici.DepartmanPasifeAl(id);
            ListeyiYenile();
        }

        private void chkPasifleriGoster_CheckedChanged(object sender, EventArgs e)
        {
            ListeyiYenile();
        }


        private void dgvDepartman_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDepartman.CurrentRow?.DataBoundItem is Departman d)
                btnAktifeAl.Enabled = !d.Aktif;
            else
                btnAktifeAl.Enabled = false;
        }
        private void PasifSatirlariGriYap()
        {
            foreach (DataGridViewRow row in dgvDepartman.Rows)
            {
                if (row.DataBoundItem is Departman departman && !departman.Aktif)
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.DefaultCellStyle.Font = new Font(
                        dgvDepartman.Font,
                        FontStyle.Italic
                    );
                }
            }
        }

        private void btnAktifeAl_Click(object sender, EventArgs e)
        {
            if (dgvDepartman.CurrentRow == null)
            {
                MessageBox.Show("Lütfen bir departman seçin.");
                return;
            }

            var secili = dgvDepartman.CurrentRow.DataBoundItem as Departman;

            if (secili == null)
            {
                MessageBox.Show("Seçim okunamadı.");
                return;
            }

            if (secili.Aktif)
            {
                MessageBox.Show("Bu departman zaten aktif.");
                return;
            }

            _yonetici.DepartmanAktifeAl(secili.Id);
            ListeyiYenile();
            MessageBox.Show("Departman tekrar aktife alındı.");
        }

    }
}