using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GirisEkran
{
    public partial class FrmStokMenu : Form
    {
        public FrmStokMenu()
        {
            InitializeComponent();
        }

        // Bu kısımda her kategoriye ait bir form olacak ve ilgili kategoriye tıkladığımızda o sayfa açılacak diğer sayfa gizlenecek.

        private void GeriBtn_Click(object sender, EventArgs e)
        {
            FrmGiris fr = new FrmGiris();
            fr.Show();
            this.Hide();
        }

        private void CarpiBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void İsAyakkabısıPctr_Click(object sender, EventArgs e)
        {
            FrmIsAyakkabı fr = new FrmIsAyakkabı();
            fr.Show();
            this.Hide();
        }

        private void İsPantolonuPctr_Click(object sender, EventArgs e)
        {
            FrmIsPantolonu fr = new FrmIsPantolonu();
            fr.Show();
            this.Hide();
        }

        private void BaretlerPctr_Click(object sender, EventArgs e)
        {
            FrmBaret fr = new FrmBaret();
            fr.Show();
            this.Hide();
        }

        private void TulumlarPctr_Click(object sender, EventArgs e)
        {
            FrmTulumlar fr = new FrmTulumlar();
            fr.Show();
            this.Hide();
        }

        private void YeleklerPctr_Click(object sender, EventArgs e)
        {
            FrmYelekler fr = new FrmYelekler(); 
            fr.Show();
            this.Hide();
        }

        private void AsciOnluguPctr_Click(object sender, EventArgs e)
        {
            FrmEldiven fr = new FrmEldiven();
            fr.Show();
            this.Hide();
        }

        private void GelirGiderPctr_Click(object sender, EventArgs e)
        {
            LblEldivenKar fr = new LblEldivenKar();
            fr.Show();
            this.Hide();
        }
    }
}
