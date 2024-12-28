using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GirisEkran
{
    public partial class FrmIsPantolonu : Form
    {
        public FrmIsPantolonu()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        DataTable dt = new DataTable();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_IsPantolonlari", bgl.baglanti());
            dt.Clear(); // her listele metodunu çağırdığımda önceki verilerin üstüne birdaha veri listelememek için önceki verileri siliyorum.
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            LblAdet.Text = " ";
            LblBeden.Text = " ";
            LblFiyat.Text = " ";
            LblKumas.Text = " ";

            GabardinPicture.Visible = false;
            HarmanPicture.Visible = false;
            ParcaPicture.Visible = false;

            txtId.Text = "";
            txtPanAdet.Text = "";
            txtPanFiyat.Text = "";
            cmbBeden.Text = "Seçiniz...";
            cmbKumas.Text = "Seçiniz...";
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagridview'de olan verilerin satırdaki bir hücrenin üstüne çift tıkladığımzıda datagridde olan verileri ilgili boxlara taşımasını sağlayan komut.
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            if (dataGridView1.Rows[secilen].Cells[0].Value != null) // sürekli dizin aralık dışındaydı diyordu bende seçilen hücrenin önce null olup olmadığını kontrol ettim.
            {
                txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            }
            else
            {
                txtId.Text = " "; // Eğer null ise boş bir değer atamak için boş string kullandım.
            }
            if (dataGridView1.Rows[secilen].Cells[1].Value.ToString() != null)
            {
                txtPanFiyat.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            }
            else
            {
                txtPanFiyat.Text = " ";
            }
            //if (dataGridView1.Rows[secilen].Cells[2].Value.ToString() != null)
            //{
            //    txtPanAdet.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            //}
            //else
            //{
            //    txtPanAdet.Text = " ";
            //}
            if (dataGridView1.Rows[secilen].Cells[3].Value.ToString() != null)
            {
                cmbBeden.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            }
            else
            {
                cmbBeden.Text = " ";    
            }
            if (dataGridView1.Rows[secilen].Cells[4].Value.ToString() != null) // Burayı hocaya soracağım bir türlü çözemedim.
            {
                cmbKumas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            }
            else
            {
                cmbKumas.Text = " ";
            }
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() != null) // Burayı hocaya soracağım bir türlü çözemedim.
            {
                txtAlisFiyat.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            }
            else
            {
                txtAlisFiyat.Text = " ";
            }

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if(e.RowIndex >= 0)
            {
                LblKumas.Text = "Kumaş Türü: " + row.Cells["KumaşTürü"].Value.ToString();
                LblBeden.Text = "Beden: " + row.Cells["PantolonBeden"].Value.ToString();
                LblFiyat.Text = "Fiyat: " + row.Cells["PantolonFiyati"].Value.ToString();
                LblAdet.Text = "Stok Adedi: " + row.Cells["PantolonAdedi"].Value.ToString();
            }

            string kumastür = row.Cells["KumaşTürü"].Value.ToString().Trim();
            GabardinPicture.Visible = false;
            HarmanPicture.Visible = false;
            ParcaPicture.Visible = false;

            switch (kumastür)
            {
                case "Gabardin":
                    GabardinPicture.Visible = true;
                    break;
                case "Harman":
                    HarmanPicture.Visible = true;
                    break;
                case "Parça":
                    ParcaPicture.Visible = true;
                    break;
                default:
                    LblAdet.Text = " ";
                    LblBeden.Text = " ";
                    LblFiyat.Text = " ";
                    LblKumas.Text = " ";
                    MessageBox.Show("İlgili resim bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            //if (int.Parse(txtPanAdet.Text) < 20)
            //{
            //    MessageBox.Show("Bu pantolonu tedarik etmeniz gerekiyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void FrmIsPantolonu_Load(object sender, EventArgs e)
        {
            // ComboBox'a öğeler ekleme
            
            cmbBeden.Items.Add("Seçiniz..."); // Placeholder yazısı
            cmbBeden.Items.Add("S");
            cmbBeden.Items.Add("M");
            cmbBeden.Items.Add("L");
            cmbBeden.Items.Add("XL");
            cmbBeden.Items.Add("XXL");
            cmbBeden.Items.Add("3XL");

            // Başlangıç öğesini seç
            cmbBeden.SelectedIndex = 0; // Yani Combobox'ta Form açıldığında seçiniz yazısı çıkacak.

            cmbBeden.SelectedIndexChanged += ComboBox1_SelectedIndexChanged; // Kullanıcı seçim yaptığında SelectedIndexChanged tetiklenicek ve ComboBox1_SelectedIndexChanged eventi çalışacak.

            cmbKumas.Items.Add("Seçiniz...");
            cmbKumas.Items.Add("Gabardin");
            cmbKumas.Items.Add("Harman");
            cmbKumas.Items.Add("Parça");

            cmbKumas.SelectedIndex = 0;

            GabardinPicture.Visible = false;
            HarmanPicture.Visible = false;
            ParcaPicture.Visible = false;

            LblAdet.Text = " ";
            LblBeden.Text = " ";
            LblFiyat.Text = " ";
            LblKumas.Text = " ";

            dataGridView1.ReadOnly = false;

            listele();
        }

        // ComboBox seçim değiştiğinde yapılacak işlemler
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçili öğeyi alıyoruz
            string selectedValue = cmbBeden.SelectedItem.ToString(); // Seçili öğeyi alıyorum ve string'e dönüştürüyorum.


            if (selectedValue == "Seçiniz...") // Eğer "Seçiniz..." öğesi seçildiyse tüm verileri gösteriyorum
            {

                dataGridView1.DataSource = dt; // DataGridView'de tüm verileri göstermek için DataTable'dan oluşturduğum nesneyi datagridview'in veri kaynağı olarak atıyorum.

                txtId.Text = " ";
                txtPanAdet.Text = " ";
                txtPanFiyat.Text = " ";
                cmbBeden.Text = "Seçiniz...";
                cmbKumas.Text = "Seçiniz...";
            }
            else
            {
                // Seçilen değere göre filtre uygulama işlemi
                DataView dv = new DataView(dt); // Filtelenecek değerleri DataTable'ın dt nesnesinden al.
                dv.RowFilter = $"PantolonBeden = '{selectedValue}'"; // PantolonBeden sütununda cmbBeden'de seçilen satırdaki beden ile eşleştiyse filtre uygulanıyor.

                // DataGridView'e filtrelenmiş veriyi göster
                dataGridView1.DataSource = dv;
            }
        }

        private void ListeleBtn_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            //txtId.Enabled = false;
            txtId.ReadOnly = true; // txtId kısmına yazı yazmayı engellemek için ReadOnly Kullanıyorum.
        }

        private void EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı girdilerini kontrol et ve dönüştür
                if (!decimal.TryParse(txtPanFiyat.Text, out decimal pantolonFiyati))
                {
                    MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtPanAdet.Text, out int pantolonAdedi))
                {
                    MessageBox.Show("Lütfen geçerli bir adet giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbBeden.Text))
                {
                    MessageBox.Show("Lütfen pantolon bedenini seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbKumas.Text))
                {
                    MessageBox.Show("Lütfen kumaş türünü seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtAlisFiyat.Text, out decimal alisFiyati))
                {
                    MessageBox.Show("Lütfen geçerli bir alış fiyatı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Veritabanında kaydın olup olmadığını kontrol et
                SqlCommand kontrolKomutu = new SqlCommand(
                    "SELECT COUNT(*) FROM Tbl_IsPantolonlari WHERE PantolonBeden = @p1 AND KumaşTürü = @p2",
                    bgl.baglanti());
                kontrolKomutu.Parameters.AddWithValue("@p1", cmbBeden.Text);
                kontrolKomutu.Parameters.AddWithValue("@p2", cmbKumas.Text);

                int kayitSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    // Kayıt varsa mevcut stoğu güncelle
                    SqlCommand guncelle = new SqlCommand(
                        "UPDATE Tbl_IsPantolonlari " +
                        "SET PantolonAdedi = PantolonAdedi + @p1, PantolonFiyati = @p2, AlışFiyat = @p3 " +
                        "WHERE PantolonBeden = @p4 AND KumaşTürü = @p5",
                        bgl.baglanti());
                    guncelle.Parameters.AddWithValue("@p1", pantolonAdedi); // Yeni eklenen adet
                    guncelle.Parameters.AddWithValue("@p2", pantolonFiyati); // Güncel fiyat
                    guncelle.Parameters.AddWithValue("@p3", alisFiyati); // Güncel alış fiyatı
                    guncelle.Parameters.AddWithValue("@p4", cmbBeden.Text); // Beden
                    guncelle.Parameters.AddWithValue("@p5", cmbKumas.Text); // Kumaş türü

                    guncelle.ExecuteNonQuery();
                    MessageBox.Show("Mevcut pantolon kaydının stoğu güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kayıt yoksa yeni kayıt ekle
                    SqlCommand ekle = new SqlCommand(
                        "INSERT INTO Tbl_IsPantolonlari (PantolonFiyati, PantolonAdedi, PantolonBeden, KumaşTürü, AlışFiyat) " +
                        "VALUES (@p1, @p2, @p3, @p4, @p5)",
                        bgl.baglanti());
                    ekle.Parameters.AddWithValue("@p1", pantolonFiyati);
                    ekle.Parameters.AddWithValue("@p2", pantolonAdedi);
                    ekle.Parameters.AddWithValue("@p3", cmbBeden.Text);
                    ekle.Parameters.AddWithValue("@p4", cmbKumas.Text);
                    ekle.Parameters.AddWithValue("@p5", alisFiyati);

                    ekle.ExecuteNonQuery();
                    MessageBox.Show("Yeni pantolon başarıyla stoğunuza eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Pantolon silme işlemi
            try
            {
                SqlCommand sil = new SqlCommand("Delete From Tbl_IsPantolonlari where PantolonID=@p1", bgl.baglanti());
                sil.Parameters.AddWithValue("@p1", txtId.Text); // Burada seçtiğim pantolonun ID numarasına göre silmesini istiyorum başka bir column adı koysaydım örneğin KumaşTürüne göre silme işlemi gerçekleştirseydim o kumaş türüne ait tüm pantolonlar silinebilirdi.
                sil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Pantolon Başarıyla Stoğunuzdan Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen girdiğiniz parametreleri kontrol ediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
            
        }

        private void GüncelleBtn_Click(object sender, EventArgs e)
        {
            // Güncelleme işlemi
            try
            {
                SqlCommand güncelle = new SqlCommand("Update Tbl_IsPantolonlari set PantolonFiyati=@p1, PantolonAdedi=@p2, PantolonBeden=@p3, KumaşTürü=@p4, AlışFiyat=@p5 where PantolonID=@p6", bgl.baglanti());
                güncelle.Parameters.AddWithValue("@p1", decimal.Parse(txtPanFiyat.Text));
                güncelle.Parameters.AddWithValue("@p2", int.Parse(txtPanAdet.Text));
                güncelle.Parameters.AddWithValue("@p3", cmbBeden.Text);
                güncelle.Parameters.AddWithValue("@p4", cmbKumas.Text);
                güncelle.Parameters.AddWithValue("@p5", txtAlisFiyat.Text);
                güncelle.Parameters.AddWithValue("@p6", txtId);
                güncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                LblAdet.Text = "Stok Adedi: " + int.Parse(txtPanAdet.Text);
                LblBeden.Text = "Beden: " + cmbBeden.Text;
                LblFiyat.Text = "Fiyat: " + decimal.Parse(txtPanFiyat.Text);
                LblKumas.Text = "Kumaş: " + cmbKumas.Text;
                
                if(int.Parse(txtPanAdet.Text) < 20)
                {
                    MessageBox.Show("Bu pantolonu tedarik etmeniz gerekiyor.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MessageBox.Show("Pantolon Başarıyla Güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen girdiğiniz parametreleri kontrol ediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBeden_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void cmbKumas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSat_Click(object sender, EventArgs e)
        {
            try
            {
                int pantolonId = int.Parse(txtId.Text); // Pantolon ID
                decimal satisFiyati = decimal.Parse(txtPanFiyat.Text); // Satış Fiyatı
                int satilanAdet = int.Parse(txtPanAdet.Text); // Satılan Adet
                DateTime satisTarihi = SatisTarihi.Value; // Satış Tarihi
                string pantolonBeden = cmbBeden.Text; // Pantolon Beden
                string kumasTür = cmbKumas.Text;

                if (bgl.baglanti().State == System.Data.ConnectionState.Open)
                {
                    bgl.baglanti().Close();
                }

                bgl.baglanti();

                // Tbl_IsPantolonları tablosundan AlışFiyat ve mevcut stok bilgilerini al
                SqlCommand komutAlisFiyat = new SqlCommand(
                    "SELECT AlışFiyat, PantolonAdedi FROM Tbl_IsPantolonlari WHERE PantolonID = @p1", bgl.baglanti());
                komutAlisFiyat.Parameters.AddWithValue("@p1", pantolonId);

                SqlDataReader dr = komutAlisFiyat.ExecuteReader();

                if (dr.Read())
                {
                    decimal alisFiyati = dr["AlışFiyat"] != DBNull.Value ? Convert.ToDecimal(dr["AlışFiyat"]) : 0;
                    int mevcutAdet = dr["PantolonAdedi"] != DBNull.Value ? Convert.ToInt32(dr["PantolonAdedi"]) : 0;

                    dr.Close();

                    if (mevcutAdet < satilanAdet)
                    {
                        MessageBox.Show("Yeterli stok yok!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        bgl.baglanti().Close();
                        return;
                    }

                    // Tbl_Satis tablosunda aynı ürün daha önce satılmış mı kontrol et
                    SqlCommand komutSatisKontrol = new SqlCommand(
                        "SELECT SatilanAdet FROM Tbl_Satis WHERE ÜrünID = @p1 AND ÜrünBeden = @p2 AND ÜrünTürü = 'Pantolon'", bgl.baglanti());
                    komutSatisKontrol.Parameters.AddWithValue("@p1", pantolonId);
                    komutSatisKontrol.Parameters.AddWithValue("@p2", pantolonBeden);

                    SqlDataReader drSatis = komutSatisKontrol.ExecuteReader();

                    if (drSatis.Read())
                    {
                        // Aynı üründen daha önce satış yapılmış, SatilanAdet güncellenmeli
                        int mevcutSatilanAdet = Convert.ToInt32(drSatis["SatilanAdet"]);
                        drSatis.Close();

                        SqlCommand komutSatisGuncelle = new SqlCommand(
                            "UPDATE Tbl_Satis SET SatilanAdet = SatilanAdet + @p1, Tarih = @p2 WHERE ÜrünID = @p3 AND ÜrünBeden = @p4 AND ÜrünTürü = 'Pantolon'", bgl.baglanti());
                        komutSatisGuncelle.Parameters.AddWithValue("@p1", satilanAdet);
                        komutSatisGuncelle.Parameters.AddWithValue("@p2", satisTarihi);
                        komutSatisGuncelle.Parameters.AddWithValue("@p3", pantolonId);
                        komutSatisGuncelle.Parameters.AddWithValue("@p4", pantolonBeden);

                        komutSatisGuncelle.ExecuteNonQuery();
                    }
                    else
                    {
                        drSatis.Close();

                        // Ürün ilk kez satılıyor, yeni bir satır ekle
                        SqlCommand komutSatisEkle = new SqlCommand(
                            "INSERT INTO Tbl_Satis (ÜrünID, ÜrünBeden, AlisFiyati, SatisFiyati, SatilanAdet, Tarih, ÜrünTürü, ÜrünTipi, ÜrünSeri, ÜrünRenk) " +
                            "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)", bgl.baglanti());

                        komutSatisEkle.Parameters.AddWithValue("@p1", pantolonId);
                        komutSatisEkle.Parameters.AddWithValue("@p2", pantolonBeden);
                        komutSatisEkle.Parameters.AddWithValue("@p3", alisFiyati);
                        komutSatisEkle.Parameters.AddWithValue("@p4", satisFiyati);
                        komutSatisEkle.Parameters.AddWithValue("@p5", satilanAdet);
                        komutSatisEkle.Parameters.AddWithValue("@p6", satisTarihi);
                        komutSatisEkle.Parameters.AddWithValue("@p7", "Pantolon");
                        komutSatisEkle.Parameters.AddWithValue("@p8", kumasTür);
                        komutSatisEkle.Parameters.AddWithValue("@p9", "Seri Yok");
                        komutSatisEkle.Parameters.AddWithValue("@p10", "Renk Yok");

                        komutSatisEkle.ExecuteNonQuery();
                    }

                    // Tbl_IsPantolonlari tablosunda stok adetini güncelle
                    SqlCommand komutStokGuncelle = new SqlCommand(
                        "UPDATE Tbl_IsPantolonlari SET PantolonAdedi = PantolonAdedi - @p1 WHERE PantolonID = @p2", bgl.baglanti());

                    komutStokGuncelle.Parameters.AddWithValue("@p1", satilanAdet);
                    komutStokGuncelle.Parameters.AddWithValue("@p2", pantolonId);

                    komutStokGuncelle.ExecuteNonQuery();

                    MessageBox.Show("Satış başarıyla gerçekleştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Girilen ID'ye ait pantolon bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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