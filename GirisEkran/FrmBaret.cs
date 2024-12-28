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
    public partial class FrmBaret : Form
    {
        public FrmBaret()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        DataTable dt = new DataTable();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Baretler", bgl.baglanti());
            dt.Clear(); // Global dt'de önceki listelenmiş verileri siliyorum.
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            UcMBeyazPicture.Visible = false;
            UcMMaviPicture.Visible = false;
            UcMSarıPicture.Visible = false;
            UcMTuruncuPicture.Visible = false;
            UcMYesilPicture.Visible = false;    
            CervaBeyaz.Visible = false;
            CervaMavi.Visible = false;
            CervaSarı.Visible = false;
            CervaTuruncu.Visible = false;

            LblAdet.Text = "";
            LblFiyat.Text = "";
            LblMarka.Text = "";
            LblRenk.Text = "";


        }

        private void GeriBtn_Click(object sender, EventArgs e)
        {
            FrmStokMenu fr = new FrmStokMenu();
            fr.Show();
            this.Hide();
        }

        private void CarpiBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void FrmBaret_Load(object sender, EventArgs e)
        {
            cmbRenk.Items.Add("Seçiniz..."); // PlaceHolder Yazısı
            cmbRenk.Items.Add("Beyaz");
            cmbRenk.Items.Add("Sarı");
            cmbRenk.Items.Add("Mavi");
            cmbRenk.Items.Add("Turuncu");
            cmbRenk.Items.Add("Yeşil");

            cmbRenk.SelectedIndex = 0;

            cmbMarka.Items.Add("Seçiniz...");
            cmbMarka.Items.Add("3M");
            cmbMarka.Items.Add("Cerva Alpinworker");

            cmbMarka.SelectedIndex = 0;

            cmbMarka.SelectedIndexChanged += CmbMarka_SelectedIndexChanged;

            dataGridView1.ReadOnly = false;

            listele();

            UcMBeyazPicture.Visible = false;
            UcMMaviPicture.Visible = false;
            UcMSarıPicture.Visible = false;
            UcMTuruncuPicture.Visible = false;
            UcMYesilPicture.Visible = false;
            CervaBeyaz.Visible = false;
            CervaMavi.Visible = false;
            CervaSarı.Visible = false;
            CervaTuruncu.Visible = false;

            LblAdet.Text = "";
            LblFiyat.Text = "";
            LblMarka.Text = "";
            LblRenk.Text = "";
        }

        private void CmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cmbMarka.SelectedItem.ToString();

            if (selectedValue == "Seçiniz...")
            {
                dataGridView1.DataSource = dt;

                UcMBeyazPicture.Visible = false;
                UcMMaviPicture.Visible = false;
                UcMSarıPicture.Visible = false;
                UcMTuruncuPicture.Visible = false;
                UcMYesilPicture.Visible = false;
                CervaBeyaz.Visible = false;
                CervaMavi.Visible = false;
                CervaSarı.Visible = false;
                CervaTuruncu.Visible = false;

                LblAdet.Text = "";
                LblFiyat.Text = "";
                LblMarka.Text = "";
                LblRenk.Text = "";

                txtAdet.Text = "";
                txtFiyat.Text = "";
                txtID.Text = "";
                cmbMarka.Text = "Seçiniz...";
                cmbRenk.Text = "Seçiniz...";
            }
            else
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = $"BaretMarka = '{selectedValue}'";
                dataGridView1.DataSource = dv;
            }
        }

        private void ListeleBtn_Click(object sender, EventArgs e)
        {
            listele();

            UcMBeyazPicture.Visible = false;
            UcMMaviPicture.Visible = false;
            UcMSarıPicture.Visible = false;
            UcMTuruncuPicture.Visible = false;
            UcMYesilPicture.Visible = false;
            CervaBeyaz.Visible = false;
            CervaMavi.Visible = false;
            CervaSarı.Visible = false;
            CervaTuruncu.Visible = false;

            LblAdet.Text = "";
            LblFiyat.Text = "";
            LblMarka.Text = "";
            LblRenk.Text = "";

            txtAdet.Text = "";
            txtFiyat.Text = "";
            txtID.Text = "";
            cmbMarka.Text = "Seçiniz...";
            cmbRenk.Text = "Seçiniz...";
            txtAlisFiyat.Text = "";
        }

        private void EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı girdilerini kontrol et ve dönüştür
                if (!decimal.TryParse(txtFiyat.Text, out decimal baretFiyat))
                {
                    MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtAdet.Text, out int baretAdet))
                {
                    MessageBox.Show("Lütfen geçerli bir adet giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbRenk.Text))
                {
                    MessageBox.Show("Lütfen baret rengini seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbMarka.Text))
                {
                    MessageBox.Show("Lütfen baret markasını seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mevcut kaydı kontrol et
                SqlCommand kontrolKomutu = new SqlCommand(
                    "SELECT COUNT(*) FROM Tbl_Baretler WHERE BaretRenk = @p1 AND BaretMarka = @p2",
                    bgl.baglanti());
                kontrolKomutu.Parameters.AddWithValue("@p1", cmbRenk.Text);
                kontrolKomutu.Parameters.AddWithValue("@p2", cmbMarka.Text);

                int kayitSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    // Eğer kayıt zaten varsa, stoğu güncelle
                    SqlCommand guncelle = new SqlCommand(
                        "UPDATE Tbl_Baretler " +
                        "SET BaretAdet = BaretAdet + @p1, BaretFiyat = @p2 " +
                        "WHERE BaretRenk = @p3 AND BaretMarka = @p4",
                        bgl.baglanti());
                    guncelle.Parameters.AddWithValue("@p1", baretAdet); // Yeni eklenen adet
                    guncelle.Parameters.AddWithValue("@p2", baretFiyat); // Güncel fiyat
                    guncelle.Parameters.AddWithValue("@p3", cmbRenk.Text); // Renk
                    guncelle.Parameters.AddWithValue("@p4", cmbMarka.Text); // Marka

                    guncelle.ExecuteNonQuery();
                    MessageBox.Show("Mevcut baret kaydının stoğu güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Eğer kayıt yoksa yeni kayıt ekle
                    SqlCommand ekle = new SqlCommand(
                        "INSERT INTO Tbl_Baretler (BaretFiyat, BaretAdet, BaretRenk, BaretMarka, AlışFiyati) " +
                        "VALUES (@p1, @p2, @p3, @p4, @p5)",
                        bgl.baglanti());
                    ekle.Parameters.AddWithValue("@p1", baretFiyat);
                    ekle.Parameters.AddWithValue("@p2", baretAdet);
                    ekle.Parameters.AddWithValue("@p3", cmbRenk.Text);
                    ekle.Parameters.AddWithValue("@p4", cmbMarka.Text);
                    ekle.Parameters.AddWithValue("@p5", txtAlisFiyat.Text);

                    ekle.ExecuteNonQuery();
                    MessageBox.Show("Yeni baret başarıyla stoğa eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Bağlantıyı kapat
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                // Hata mesajını göster
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SilBtn_Click(object sender, EventArgs e)
        {
            // Baret Silme İşlemi
            try
            {
                SqlCommand sil = new SqlCommand("Delete From Tbl_Baretler where BaretID=@p1", bgl.baglanti());
                sil.Parameters.AddWithValue("@p1", txtID.Text);
                sil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Baret başarıyla stoktan silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen girdiğiniz parametreleri kontrol ediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRenk_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // cmbRenk kısmına herhangi bir veri girmemek için kullanılan kod.
        }

        private void cmbMarka_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // cmbMarka kısmına herhangi bir veri girmemek için gereken kod.
        }

        private void GüncelleBtn_Click(object sender, EventArgs e)
        {
            // Güncelleme işlemi 
            try
            {
                SqlCommand güncelle = new SqlCommand("Update Tbl_Baretler set BaretFiyat=@p1,BaretAdet=@p2,BaretRenk=@p3,BaretMarka=@p4, AlışFiyatı=@p5 where BaretID=@p6", bgl.baglanti());
                güncelle.Parameters.AddWithValue("@p1", decimal.Parse(txtFiyat.Text));
                güncelle.Parameters.AddWithValue("@p2", int.Parse(txtAdet.Text));
                güncelle.Parameters.AddWithValue("@p3", cmbRenk.Text);
                güncelle.Parameters.AddWithValue("@p4", cmbMarka.Text);
                güncelle.Parameters.AddWithValue("@p5", decimal.Parse(txtAlisFiyat.Text));
                güncelle.Parameters.AddWithValue("@p6", txtID.Text);
                güncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                LblAdet.Text = "Stok Adedi: " + int.Parse(txtAdet.Text);
                LblFiyat.Text = "Fiyat: " + decimal.Parse(txtFiyat.Text);
                LblMarka.Text = "Marka: " + cmbMarka.Text;
                LblRenk.Text = "Renk: " + cmbRenk.Text;
                if (int.Parse(txtAdet.Text) < 20)
                {
                    MessageBox.Show("Bu bareti tedarik etmen gerekiyor." + "\nÜrün Adedi: " + txtAdet.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MessageBox.Show("Baret başarıyla stoktan güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen girdiğiniz parametreleri kontrol ediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // ********* Buradaki hatayı hala çözemedim hocaya soracağım.
            // DataGridView'de bir hücreye çift tıkladığımızda datagriddeki verileri ilgili boxlara atayan kod satırı.
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtFiyat.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            cmbRenk.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbMarka.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() != null) 
            {
                txtAlisFiyat.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            }

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if(e.RowIndex >= 0)
            {
                LblFiyat.Text = "Fiyatı: " + row.Cells["BaretFiyat"].Value.ToString();
                LblAdet.Text = "Stok Adedi: " + row.Cells["BaretAdet"].Value.ToString();
                LblRenk.Text = "Renk: " + row.Cells["BaretRenk"].Value.ToString();
                LblMarka.Text = "Marka: " + row.Cells["BaretMarka"].Value.ToString();
                
            }

            string baretMarka = row.Cells["BaretMarka"].Value.ToString().Trim();

            UcMBeyazPicture.Visible = false;
            UcMMaviPicture.Visible = false;
            UcMSarıPicture.Visible = false;
            UcMTuruncuPicture.Visible = false;
            UcMYesilPicture.Visible = false;
            CervaBeyaz.Visible = false;
            CervaMavi.Visible = false;
            CervaSarı.Visible = false;
            CervaTuruncu.Visible = false;

            switch(baretMarka)
            {
                case "3M":
                    if (row.Cells["BaretRenk"].Value.ToString() == "Sarı")
                    {
                        UcMSarıPicture.Visible = true;
                    }
                    if (row.Cells["BaretRenk"].Value.ToString() == "Yeşil")
                    {
                        UcMYesilPicture.Visible = true;
                    }
                    if (row.Cells["BaretRenk"].Value.ToString() == "Mavi")
                    {
                        UcMMaviPicture.Visible = true;
                    }
                    if (row.Cells["BaretRenk"].Value.ToString() == "Turuncu")
                    {
                        UcMTuruncuPicture.Visible = true;
                    }
                    if (row.Cells["BaretRenk"].Value.ToString() == "Beyaz")
                    {
                        UcMBeyazPicture.Visible = true;
                    }
                    break;
                case "Cerva Alpinworker":
                    if (row.Cells["BaretRenk"].Value.ToString() == "Beyaz")
                    {
                        CervaBeyaz.Visible = true;
                    }
                    if (row.Cells["BaretRenk"].Value.ToString() == "Mavi")
                    {
                        CervaMavi.Visible = true;
                    }
                    if (row.Cells["BaretRenk"].Value.ToString() == "Turuncu")
                    {
                        CervaTuruncu.Visible = true;
                    }
                    if (row.Cells["BaretRenk"].Value.ToString() == "Sarı")
                    {
                        CervaSarı.Visible = true;
                    }
                    break;
                default:
                    LblAdet.Text = "";
                    LblFiyat.Text = "";
                    LblMarka.Text = "";
                    LblRenk.Text = "";
                    MessageBox.Show("İlgili markaya ait resim bulunamadı!","Hata",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                
            }

            //if (int.Parse(txtAdet.Text) < 20)
            //{
            //    MessageBox.Show("Bu bareti tedarik etmen gerekiyor." + "\nÜrün Adedi: " + txtAdet.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnSat_Click(object sender, EventArgs e)
        {
            try
            {
                int baretId = int.Parse(txtID.Text); // Baret ID
                decimal satisFiyati = decimal.Parse(txtFiyat.Text); // Satış Fiyatı
                int satilanAdet = int.Parse(txtAdet.Text); // Satılan Adet
                DateTime satisTarihi = SatisTarih.Value; // Satış Tarihi
                string baretRenk = cmbRenk.Text; // Baret Renk
                string baretMarka = cmbMarka.Text; // Baret Marka

                if (bgl.baglanti().State == System.Data.ConnectionState.Open)
                {
                    bgl.baglanti().Close();
                }

                bgl.baglanti();

                // Tbl_Baretler tablosundan AlışFiyat ve mevcut stok bilgilerini al
                SqlCommand komutAlisFiyat = new SqlCommand(
                    "SELECT AlışFiyatı, BaretAdet FROM Tbl_Baretler WHERE BaretID = @p1", bgl.baglanti());
                komutAlisFiyat.Parameters.AddWithValue("@p1", baretId);

                SqlDataReader dr = komutAlisFiyat.ExecuteReader();

                if (dr.Read())
                {
                    decimal alisFiyati = dr["AlışFiyatı"] != DBNull.Value ? Convert.ToDecimal(dr["AlışFiyatı"]) : 0;
                    int mevcutAdet = dr["BaretAdet"] != DBNull.Value ? Convert.ToInt32(dr["BaretAdet"]) : 0;

                    dr.Close();

                    if (mevcutAdet < satilanAdet)
                    {
                        MessageBox.Show("Yeterli stok yok!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        bgl.baglanti().Close();
                        return;
                    }

                    // Tbl_Satis tablosunda aynı ürün daha önce satılmış mı kontrol et
                    SqlCommand komutSatisKontrol = new SqlCommand(
                        "SELECT SatilanAdet FROM Tbl_Satis WHERE ÜrünID = @p1 AND ÜrünRenk = @p2 AND ÜrünTürü = 'Baret'", bgl.baglanti());
                    komutSatisKontrol.Parameters.AddWithValue("@p1", baretId);
                    komutSatisKontrol.Parameters.AddWithValue("@p2", baretRenk);

                    SqlDataReader drSatis = komutSatisKontrol.ExecuteReader();

                    if (drSatis.Read())
                    {
                        // Aynı üründen daha önce satış yapılmış, SatilanAdet güncellenmeli
                        drSatis.Close();

                        SqlCommand komutSatisGuncelle = new SqlCommand(
                            "UPDATE Tbl_Satis SET SatilanAdet = SatilanAdet + @p1, Tarih = @p2 WHERE ÜrünID = @p3 AND ÜrünRenk = @p4 AND ÜrünTürü = 'Baret'", bgl.baglanti());
                        komutSatisGuncelle.Parameters.AddWithValue("@p1", satilanAdet);
                        komutSatisGuncelle.Parameters.AddWithValue("@p2", satisTarihi);
                        komutSatisGuncelle.Parameters.AddWithValue("@p3", baretId);
                        komutSatisGuncelle.Parameters.AddWithValue("@p4", baretRenk);

                        komutSatisGuncelle.ExecuteNonQuery();
                    }
                    else
                    {
                        drSatis.Close();

                        // Ürün ilk kez satılıyor, yeni bir satır ekle
                        SqlCommand komutSatisEkle = new SqlCommand(
                            "INSERT INTO Tbl_Satis (ÜrünID, ÜrünRenk, AlisFiyati, SatisFiyati, SatilanAdet, Tarih, ÜrünTürü, ÜrünTipi, ÜrünSeri, ÜrünBeden) " +
                            "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)", bgl.baglanti());

                        komutSatisEkle.Parameters.AddWithValue("@p1", baretId);
                        komutSatisEkle.Parameters.AddWithValue("@p2", baretRenk);
                        komutSatisEkle.Parameters.AddWithValue("@p3", alisFiyati);
                        komutSatisEkle.Parameters.AddWithValue("@p4", satisFiyati);
                        komutSatisEkle.Parameters.AddWithValue("@p5", satilanAdet);
                        komutSatisEkle.Parameters.AddWithValue("@p6", satisTarihi);
                        komutSatisEkle.Parameters.AddWithValue("@p7", "Baret");
                        komutSatisEkle.Parameters.AddWithValue("@p8", "Tip Belirtilmedi");
                        komutSatisEkle.Parameters.AddWithValue("@p9", baretMarka);
                        komutSatisEkle.Parameters.AddWithValue("@p10", "Beden Yok");

                        komutSatisEkle.ExecuteNonQuery();
                    }

                    // Tbl_Baretler tablosunda stok adetini güncelle
                    SqlCommand komutStokGuncelle = new SqlCommand(
                        "UPDATE Tbl_Baretler SET BaretAdet = BaretAdet - @p1 WHERE BaretID = @p2", bgl.baglanti());

                    komutStokGuncelle.Parameters.AddWithValue("@p1", satilanAdet);
                    komutStokGuncelle.Parameters.AddWithValue("@p2", baretId);

                    komutStokGuncelle.ExecuteNonQuery();

                    MessageBox.Show("Satış başarıyla gerçekleştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Girilen ID'ye ait baret bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (bgl.baglanti().State == System.Data.ConnectionState.Open)
                {
                    bgl.baglanti().Close();
                }
            }
        }
    }
}                 
           
            

