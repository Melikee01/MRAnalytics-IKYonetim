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
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 228, 225); // MistyRose
            this.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            
        }

        private void AnaMenu_Load(object sender, EventArgs e)
        {
            lblKullaniciBilgi.Text =
                $"Hoşgeldiniz: " +
                $"{OturumYoneticisi.Email}";

            lblrol.Text =
                $"({OturumYoneticisi.Rol})";


            btnPersonel.Visible = false;
            btnDeparman.Visible = false;
            btnIzin.Visible = false;
            btnmaas.Visible = false;
            btnPerformans.Visible = false;
            btnRaporlama.Visible = false;
            btnsifredegis.Visible = true; 

            if (OturumYoneticisi.Rol == "Admin")
            {
                btnPersonel.Visible = true;
                btnDeparman.Visible = true;
                btnIzin.Visible = true;
                btnmaas.Visible = true;
                btnPerformans.Visible = true;
                btnRaporlama.Visible = true;
            }
            else if (OturumYoneticisi.Rol == "IK")
            {
                btnPersonel.Visible = true;
                btnDeparman.Visible = true;
                btnIzin.Visible = true;
                btnPerformans.Visible = true;
            }
            else if (OturumYoneticisi.Rol == "users")
            {
                btnIzin.Visible = true;
            }

        }

        private void btnPersonel_Click(object sender, EventArgs e)
        {
           
            OpenChildForm(new PersonelYonetimiFormu());
        }

        private void btnDeparman_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DepartmanYonetimi());
        }

        
        private void btnIzin_Click(object sender, EventArgs e)
        {
           
            OpenChildForm(new IzinFormu());
        }

        private void btnmaas_Click(object sender, EventArgs e)
        {
           
            OpenChildForm(new MaasHesaplamaFormu());
        }

        private void btnPerformans_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PerformansFormu());
        }

        private void btnRaporlama_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RaporFormu());
        }

        private void btnsifredegis_Click(object sender, EventArgs e)
        {
            using (var frm = new SifreDegistirFormu())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }


        private void btnCikıs_Click(object sender, EventArgs e)
        {
            OturumYoneticisi.CikisYap();
            new GirisFormu().Show();
            this.Close();
        }
        Form activeForm = null;

        void OpenChildForm(Form child)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(child);
            child.Show();
        }

        
    }

}

