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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        // Global kısıma SqlBaglantisi sınıfından bgl adında bir nesne oluşturuyorum.

        SqlBaglantisi bgl = new SqlBaglantisi(); 

        private void GirisYapButton_Click(object sender, EventArgs e)
        {
            if (txtKullanici.Text == null)
            {
                MessageBox.Show("Lütfen kullanıcı adınızı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtSifre.Text == null)
            {
                MessageBox.Show("Lütfen şifrenizi giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SqlCommand giris = new SqlCommand("Select * From Kullanıcı_Tbl where KullaniciAd=@p1 and Sifre=@p2",bgl.baglanti());
            giris.Parameters.AddWithValue("@p1", txtKullanici.Text); // Girilen parametrelerin hangi kutucuktan geleceğini buradan tanımlıyorum.
            giris.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = giris.ExecuteReader(); // SqlDataReader sınıfının dr nesnesini girilen parametrelere atıyıp execute ediyorum.
            if (dr.Read()) // Eğer girilen parametreler doğruysa if'in altındaki scope çalışıyor 
            {
                FrmStokMenu fr = new FrmStokMenu(); // Burada FrmStokMenu sınıfından fr adında bir nesne oluşturuyorum.
                fr.Show(); // diğer formun gözükmesi için Show() fonksiyonunu çağırıyorum.
                this.Hide(); // Var olan formumun kapanması(gizlenmesi) için this.Hide() yapıyorum
            }
            else
            {
                // Eğer Kullanıcı adı ya da şifre hatalıysa bunu kullanıcıya bildirecek bir Hata mesajı yazıyorum.
                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error); 
            }
            
        }

        private void CarpiBtn_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Formun köşesinde bulunan çarpıya bastığımda programın sonlanmasını istiyorum.
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmKayıtOl fr = new FrmKayıtOl();
            fr.Show();
            this.Hide();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
        



