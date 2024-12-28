using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GirisEkran
{
    public partial class FrmKayıtOl : Form
    {
        public FrmKayıtOl()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl =new SqlBaglantisi();

        private void CarpiBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GeriBtn_Click(object sender, EventArgs e)
        {
            FrmGiris fr = new FrmGiris();
            fr.Show();
            this.Hide();
        }

        private void kayıtolBtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlCommand kayıt = new SqlCommand("insert into Kullanıcı_Tbl (KullaniciAd,Sifre) values (@p1,@p2)", bgl.baglanti());
                kayıt.Parameters.AddWithValue("@p1", txtKullaniciAd.Text);
                kayıt.Parameters.AddWithValue("@p2", txtSifre.Text);
                kayıt.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kaydınız Başarıyla Oluşturulmuştur.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen girdiğiniz parametreleri kontrol ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
