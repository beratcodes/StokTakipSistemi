using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GirisEkran
{
    public partial class FrmIsAyakkabı : Form
    {
        public FrmIsAyakkabı()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        
        DataTable dt = new DataTable();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_IsAyakkabıları", bgl.baglanti());
            dt.Clear(); // her listele metodunu çağırdığımda önceki verilerin üstüne birdaha veri listelememek için önceki verileri siliyorum.
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            LblFiyat.Text = " ";
            LblMarka.Text = " ";
            LblNumara.Text = " ";
            LblStokSayi.Text = " ";

            ydsPicture.Visible = false;
            BasePicture.Visible = false;
            TowolkforPicture.Visible = false;
            BmesPicture.Visible = false;
            SwolxPicture.Visible = false;

            txtAdet.Text = "";
            txtFiyat.Text = "";
            txtıd.Text = "";
            cmbMarka.Text = "Seçiniz...";
            mskNo.Text = "";
            txtAlisFiyat.Text = "";

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

        private void FrmIsAyakkabı_Load(object sender, EventArgs e)
        {
            // ComboBox'a öğeler ekleme
            cmbMarka.Items.Add("Seçiniz..."); // Placeholder yazısı
            cmbMarka.Items.Add("YDS");
            cmbMarka.Items.Add("Base B1006A");
            cmbMarka.Items.Add("Toworkfor X");
            cmbMarka.Items.Add("Swolx Combo X");
            cmbMarka.Items.Add("BMES 1453");

            // Başlangıç öğesini seç
            cmbMarka.SelectedIndex = 0; // Yani Combobox'ta Form açıldığında seçiniz yazısı çıkacak.

            cmbMarka.SelectedIndexChanged += ComboBox1_SelectedIndexChanged; // Kullanıcı seçim yaptığında SelectedIndexChanged tetiklenicek ve
                                                                             // ComboBox1_SelectedIndexChanged eventi çalışacak.
            listele();

            ydsPicture.Visible = false;
            BasePicture.Visible = false;
            TowolkforPicture.Visible = false;
            BmesPicture.Visible = false;
            SwolxPicture.Visible = false;

            dataGridView1.ReadOnly = true;
        }


        // ComboBox seçim değiştiğinde yapılacak işlemler
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçili öğeyi alıyoruz
            string selectedValue = cmbMarka.SelectedItem.ToString(); // Seçili öğeyi alıyorum ve string'e dönüştürüyorum.


            if (selectedValue == "Seçiniz...") // Eğer "Seçiniz..." öğesi seçildiyse tüm verileri gösteriyorum
            {
                
                dataGridView1.DataSource = dt; // Tüm verileri göstermek için DataGridView'in veri kaynağını yeniden ayarla

                // Eğer Seçiniz yazısına gelindiyse resimler tekrardan görünmez hale gelir.
                ydsPicture.Visible = false;
                BasePicture.Visible = false;
                TowolkforPicture.Visible = false;
                BmesPicture.Visible = false;
                SwolxPicture.Visible = false;

                LblFiyat.Text = "";
                LblMarka.Text = "";
                LblNumara.Text = "";
                LblStokSayi.Text = "";
            }
            else
            {
                // Seçilen değere göre filtre uygulama işlemi
                DataView dv = new DataView(dt); // Filtelenecek değerleri DataTable'ın dt nesnesinden al.
                dv.RowFilter = $"AyakkabiMarkasi = '{selectedValue}'"; // Ayakkabı Markası sütununda cmbMarka'da seçilen satırdaki marka ile eşleştiyse filtre uygulanıyor.

                // DataGridView'e filtrelenmiş veriyi göster
                dataGridView1.DataSource = dv;
            }
        }

        private void ListeleBtn_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void txtıd_TextChanged(object sender, EventArgs e)
        {
            txtıd.Enabled = true; // ID kısmı olduğu için veri girmeyi engeller.
            txtıd.ReadOnly = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Burada yaptığım işlem CellDoubleClick eventinde datagridview'deki herhangi bir satırdaki hücreye tıkladığımda
            // o satırla alakalı bilgileri otomatik olarak textbox'lara aktaran kod bloğu
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtıd.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            cmbMarka.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            mskNo.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtFiyat.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            //txtAdet.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtAlisFiyat.Text = dataGridView1.Rows[secilen].Cells["AlışFiyat"].Value.ToString();

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if (e.RowIndex >= 0)
            {
                // Seçilen satırdaki veriyi almak için kullandım.

                LblMarka.Text = "Marka: " + row.Cells["AyakkabiMarkasi"].Value.ToString();
                LblNumara.Text = "Numara: " + row.Cells["AyakkabiNumarasi"].Value.ToString();
                LblFiyat.Text = "Fiyat: " + row.Cells["AyakkabiFiyati"].Value.ToString();
                LblStokSayi.Text = "Stok Adedi: " + row.Cells["AyakkabiAdedi"].Value.ToString();
            }

            string marka = row.Cells["AyakkabiMarkasi"].Value.ToString().Trim(); // 1. columnda marka var
            ydsPicture.Visible = false; // Tüm resimleri gizliyeceğim sonrasında switch case açarak seçtiğim markaya göre visible true olacak.
            BmesPicture.Visible = false;
            SwolxPicture.Visible = false;
            TowolkforPicture.Visible = false;
            BasePicture.Visible = false;

            switch (marka)
            {
                case "YDS":
                    ydsPicture.Visible = true; 
                    break;
                case "Base B1006A":
                    BasePicture.Visible = true; 
                    break;
                case "Toworkfor X":
                    TowolkforPicture.Visible = true; 
                    break;
                case "BMES 1453":
                    BmesPicture.Visible = true; 
                    break;
                case "Swolx Combo X":
                    SwolxPicture.Visible= true; 
                    break;
                default:
                    LblFiyat.Text = " ";
                    LblMarka.Text = " ";
                    LblNumara.Text = " ";
                    LblStokSayi.Text = " ";
                    MessageBox.Show("İlgili markaya ait resim bulunamadı.", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
      
        private void ListeleBtn_Click_1(object sender, EventArgs e)
        {
            listele(); // Butona tıkladığımızda en yukarıda yazdığım listele metodu çalışacak
        }

        private void EkleBtn_Click(object sender, EventArgs e)
        {
            // Ekleme İşlemi
            // Programın patlamaması için try - catch bloğu koyuyorum.
            try
            {
                // Kullanıcıdan alınan verileri kontrol et ve dönüştür
                if (string.IsNullOrWhiteSpace(cmbMarka.Text))
                {
                    MessageBox.Show("Lütfen marka bilgisini giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(mskNo.Text))
                {
                    MessageBox.Show("Lütfen ayakkabı numarasını giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtFiyat.Text, out decimal fiyat))
                {
                    MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtAdet.Text, out int adet))
                {
                    MessageBox.Show("Lütfen geçerli bir adet giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtAlisFiyat.Text, out decimal alisFiyat))
                {
                    MessageBox.Show("Lütfen geçerli bir alış fiyatı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Veritabanında kaydın olup olmadığını kontrol et
                SqlCommand kontrolKomutu = new SqlCommand(
                    "SELECT COUNT(*) FROM Tbl_IsAyakkabıları WHERE AyakkabiMarkasi = @p1 AND AyakkabiNumarasi = @p2", bgl.baglanti());
                kontrolKomutu.Parameters.AddWithValue("@p1", cmbMarka.Text);
                kontrolKomutu.Parameters.AddWithValue("@p2", mskNo.Text);

                int kayitSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    // Kayıt zaten varsa, adet ve diğer bilgileri güncelle
                    SqlCommand guncelle = new SqlCommand(
                        "UPDATE Tbl_IsAyakkabıları " +
                        "SET AyakkabiAdedi = AyakkabiAdedi + @p1, AyakkabiFiyati = @p2, AlışFiyat = @p3 " +
                        "WHERE AyakkabiMarkasi = @p4 AND AyakkabiNumarasi = @p5", bgl.baglanti());
                    guncelle.Parameters.AddWithValue("@p1", adet);
                    guncelle.Parameters.AddWithValue("@p2", fiyat);
                    guncelle.Parameters.AddWithValue("@p3", alisFiyat);
                    guncelle.Parameters.AddWithValue("@p4", cmbMarka.Text);
                    guncelle.Parameters.AddWithValue("@p5", mskNo.Text);

                    guncelle.ExecuteNonQuery();
                    MessageBox.Show("Mevcut kaydın stoğu güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kayıt yoksa yeni kayıt ekle
                    SqlCommand ekle = new SqlCommand(
                        "INSERT INTO Tbl_IsAyakkabıları (AyakkabiMarkasi, AyakkabiNumarasi, AyakkabiFiyati, AyakkabiAdedi, AlışFiyat) " +
                        "VALUES (@p1, @p2, @p3, @p4, @p5)", bgl.baglanti());
                    ekle.Parameters.AddWithValue("@p1", cmbMarka.Text);
                    ekle.Parameters.AddWithValue("@p2", mskNo.Text);
                    ekle.Parameters.AddWithValue("@p3", fiyat);
                    ekle.Parameters.AddWithValue("@p4", adet);
                    ekle.Parameters.AddWithValue("@p5", alisFiyat);

                    ekle.ExecuteNonQuery();
                    MessageBox.Show("Yeni ayakkabı başarıyla stoğa eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void GüncelleBtn_Click(object sender, EventArgs e)
        {
            // Güncelleme İşlemi
            try
            {
                SqlCommand güncelle = new SqlCommand("Update Tbl_IsAyakkabıları set " +
                    "AyakkabiMarkasi=@p1, AyakkabiNumarasi=@p2, AyakkabiFiyati=@p3, AyakkabiAdedi=@p4, AlışFiyat=@p5 where AyakkabiID=@p6", bgl.baglanti());
                güncelle.Parameters.AddWithValue("@p1", cmbMarka.Text);
                güncelle.Parameters.AddWithValue("@p2", int.Parse(mskNo.Text));
                güncelle.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                güncelle.Parameters.AddWithValue("@p4", int.Parse(txtAdet.Text));
                güncelle.Parameters.AddWithValue("@p5", txtAlisFiyat.Text);
                güncelle.Parameters.AddWithValue("@p6", int.Parse(txtıd.Text));
                güncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                LblMarka.Text = "Marka: " + cmbMarka.Text;
                LblNumara.Text = "Numara: " + int.Parse(mskNo.Text);
                LblFiyat.Text = "Fiyat: " + decimal.Parse(txtFiyat.Text);
                LblStokSayi.Text = "Stok Adedi: " + int.Parse(txtAdet.Text);
                MessageBox.Show("Ayakkabı Stoğunuz Başarıyla Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz Parametreleri Kontrol Edip Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }  
        private void SilBtn_Click(object sender, EventArgs e)
        {
            // Silme İşlemi
            try
            {
                SqlCommand sil = new SqlCommand("Delete From Tbl_IsAyakkabıları where AyakkabiID=@p1", bgl.baglanti());
                sil.Parameters.AddWithValue("@p1", txtıd.Text);
                sil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Seçilen Ayakkabı Başarıyla Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Seçilen Ayakkabı Başarıyla Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }    
        }

        private void cmbMarka_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            try
            {
                int ayakkabiId = int.Parse(txtıd.Text);
                decimal satisFiyati = decimal.Parse(txtFiyat.Text);
                int satilanAdet = int.Parse(txtAdet.Text);
                DateTime satisTarihi = SatisTarih.Value;
                string ayakkabiMarkasi = cmbMarka.Text;
                string ayakkabiNumarasi = mskNo.Text;

                if (bgl.baglanti().State == System.Data.ConnectionState.Open)
                {
                    bgl.baglanti().Close();
                }

                bgl.baglanti();

                // Tbl_IsAyakkabıları tablosundan AlışFiyat ve mevcut stok bilgilerini al
                SqlCommand komutAlisFiyat = new SqlCommand(
                    "SELECT AlışFiyat, AyakkabiAdedi FROM Tbl_IsAyakkabıları WHERE AyakkabiID = @p1", bgl.baglanti());
                komutAlisFiyat.Parameters.AddWithValue("@p1", ayakkabiId);

                SqlDataReader dr = komutAlisFiyat.ExecuteReader();

                if (dr.Read())
                {
                    // DBNull kontrolü ile değerleri al
                    decimal alisFiyati = dr["AlışFiyat"] != DBNull.Value ? Convert.ToDecimal(dr["AlışFiyat"]) : 0;
                    int mevcutAdet = dr["AyakkabiAdedi"] != DBNull.Value ? Convert.ToInt32(dr["AyakkabiAdedi"]) : 0;

                    dr.Close();

                    // Stok kontrolü
                    if (mevcutAdet < satilanAdet)
                    {
                        MessageBox.Show("Yeterli stok yok!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        bgl.baglanti().Close();
                        return;
                    }

                    // Tbl_Satis tablosunda aynı ürün zaten var mı kontrol et
                    SqlCommand komutSatisKontrol = new SqlCommand(
                        "SELECT SatilanAdet FROM Tbl_Satis WHERE ÜrünID = @p1 AND " +
                        "ÜrünSeri = @p2 AND ÜrünBeden = @p3", bgl.baglanti());
                    komutSatisKontrol.Parameters.AddWithValue("@p1", ayakkabiId);
                    komutSatisKontrol.Parameters.AddWithValue("@p2", ayakkabiMarkasi);
                    komutSatisKontrol.Parameters.AddWithValue("@p3", ayakkabiNumarasi);

                    SqlDataReader drSatis = komutSatisKontrol.ExecuteReader();

                    if (drSatis.Read())
                    {
                        // Ürün zaten satılmış, SatilanAdet güncellenmeli
                        int mevcutSatilanAdet = Convert.ToInt32(drSatis["SatilanAdet"]);
                        drSatis.Close();

                        SqlCommand komutSatisGuncelle = new SqlCommand(
                            "UPDATE Tbl_Satis SET SatilanAdet = SatilanAdet + @p1, Tarih = @p2 WHERE " +
                            "ÜrünID = @p3 AND ÜrünSeri = @p4 AND ÜrünBeden = @p5", bgl.baglanti());
                        komutSatisGuncelle.Parameters.AddWithValue("@p1", satilanAdet);
                        komutSatisGuncelle.Parameters.AddWithValue("@p2", satisTarihi);
                        komutSatisGuncelle.Parameters.AddWithValue("@p3", ayakkabiId);
                        komutSatisGuncelle.Parameters.AddWithValue("@p4", ayakkabiMarkasi);
                        komutSatisGuncelle.Parameters.AddWithValue("@p5", ayakkabiNumarasi);

                        komutSatisGuncelle.ExecuteNonQuery();
                    }
                    else
                    {
                        drSatis.Close();

                        // Ürün ilk kez satılıyor, yeni bir satır ekle
                        SqlCommand komutSatisEkle = new SqlCommand(
                            "INSERT INTO Tbl_Satis (ÜrünID, ÜrünSeri, ÜrünBeden, AlisFiyati, SatisFiyati, SatilanAdet, Tarih, ÜrünTürü, ÜrünTipi, ÜrünRenk) " +
                            "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)", bgl.baglanti());

                        komutSatisEkle.Parameters.AddWithValue("@p1", ayakkabiId);
                        komutSatisEkle.Parameters.AddWithValue("@p2", ayakkabiMarkasi);
                        komutSatisEkle.Parameters.AddWithValue("@p3", ayakkabiNumarasi);
                        komutSatisEkle.Parameters.AddWithValue("@p4", alisFiyati);
                        komutSatisEkle.Parameters.AddWithValue("@p5", satisFiyati);
                        komutSatisEkle.Parameters.AddWithValue("@p6", satilanAdet);
                        komutSatisEkle.Parameters.AddWithValue("@p7", satisTarihi);
                        komutSatisEkle.Parameters.AddWithValue("@p8", "Ayakkabı");
                        komutSatisEkle.Parameters.AddWithValue("@p9", "Ürün Tipi Yok");
                        komutSatisEkle.Parameters.AddWithValue("@p10", "Renk Yok");

                        komutSatisEkle.ExecuteNonQuery();
                    }

                    // Tbl_IsAyakkabıları tablosunda stok adetini güncelle
                    SqlCommand komutStokGuncelle = new SqlCommand(
                        "UPDATE Tbl_IsAyakkabıları SET AyakkabiAdedi = AyakkabiAdedi - @p1 WHERE AyakkabiID = @p2", bgl.baglanti());

                    komutStokGuncelle.Parameters.AddWithValue("@p1", satilanAdet);
                    komutStokGuncelle.Parameters.AddWithValue("@p2", ayakkabiId);

                    komutStokGuncelle.ExecuteNonQuery();

                    MessageBox.Show("Satış başarıyla gerçekleştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Girilen ID'ye ait ayakkabı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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