using IKYonetim.BLL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            btnAktifeAl.Enabled = false;
            ListeyiYenile();
        }

        private void ListeyiYenile()
        {
            List<Departman> liste = chkPasifleriGoster.Checked
                ? _yonetici.TumDepartmanlar()
                : _yonetici.AktifDepartmanlariGetir();

            dgvDepartman.DataSource = null;
            dgvDepartman.DataSource = liste;

            dgvDepartman.ReadOnly = true;
            dgvDepartman.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartman.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvDepartman.Columns.Contains("Id"))
                dgvDepartman.Columns["Id"].Visible = false;

            SatirStilleriniUygula();
            SecimeGoreButonlariAyarla();
        }

        private int SeciliId()
        {
            if (dgvDepartman.CurrentRow == null) return 0;
            return Convert.ToInt32(dgvDepartman.CurrentRow.Cells["Id"].Value);
        }

        private Departman SeciliDepartman()
        {
            return dgvDepartman.CurrentRow?.DataBoundItem as Departman;
        }

        private void dgvDepartman_SelectionChanged(object sender, EventArgs e)
        {
            var d = SeciliDepartman();
            if (d == null) return;

            txtDepartmanAdi.Text = d.DepartmanAdi;
            SecimeGoreButonlariAyarla();
        }

        private void SecimeGoreButonlariAyarla()
        {
            var d = SeciliDepartman();
            if (d == null)
            {
                btnAktifeAl.Enabled = false;
                btnPasifeAl.Enabled = false;
                btnGuncelle.Enabled = false;
                return;
            }

            btnGuncelle.Enabled = true;
            btnPasifeAl.Enabled = d.Aktif;     
            btnAktifeAl.Enabled = !d.Aktif;    
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
            try
            {
                int id = SeciliId();
                if (id <= 0)
                {
                    MessageBox.Show("Lütfen bir departman seçin.");
                    return;
                }

                _yonetici.DepartmanPasifeAl(id);
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAktifeAl_Click(object sender, EventArgs e)
        {
            try
            {
                var secili = SeciliDepartman();
                if (secili == null)
                {
                    MessageBox.Show("Lütfen bir departman seçin.");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkPasifleriGoster_CheckedChanged(object sender, EventArgs e)
        {
            ListeyiYenile();
        }

        private void SatirStilleriniUygula()
        {
           
            foreach (DataGridViewRow row in dgvDepartman.Rows)
            {
                row.DefaultCellStyle.ForeColor = dgvDepartman.DefaultCellStyle.ForeColor;
                row.DefaultCellStyle.Font = dgvDepartman.Font;
            }

            
            foreach (DataGridViewRow row in dgvDepartman.Rows)
            {
                if (row.DataBoundItem is Departman departman && !departman.Aktif)
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.DefaultCellStyle.Font = new Font(dgvDepartman.Font, FontStyle.Italic);
                }
            }
        }
        
        private void dgvDepartman_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
