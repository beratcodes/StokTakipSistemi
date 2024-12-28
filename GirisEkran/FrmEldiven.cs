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
    public partial class FrmEldiven : Form
    {
        public FrmEldiven()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi(); // SqlConnection bağlantımı global olarak tanımlıyorum

        DataTable dt = new DataTable(); // datagriddeki tabloyu form içinde birden fazla yerde kullanmak için global kısıma koyuyorum.

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Eldivenler",bgl.baglanti()); // Veritabanından veriyi çekmek için SqlDataAdapter sınıfından nesne oluşturuyorum ve gerekli Sql sorgusunu yazıyorum.
            dt.Clear(); // listele butonuna tıkladığımda bir önceki verilerin buradan gitmesini sağlıyorum.
            da.Fill(dt); // Veriyi DataTable'a doldurmak için Fill metodunu kullanıyorum.
            dataGridView1.DataSource = dt; // datagridden aldığım veri kaynağını datatable'dan oluşturduğum nesneye atıyorum.

            AscıEldiveni.Visible = false;
            IscıBeybi.Visible = false;
            KaynakcıUniversal.Visible = false;
            ElektirikciBeybi.Visible = false;

            LblAdet.Text = "";
            LblFiyat.Text = "";
            LblMarka.Text = "";
            LblTür.Text = "";

            txtAdet.Text = "";
            txtFiyat.Text = "";
            txtID.Text = "";
            cmbMarka.Text = "Seçiniz...";
            cmbTür.Text = "Seçiniz...";
            txtAlisFiyat.Text = "";
        }

        private void CarpiBtn_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Uygulamadan çıkmak için gereken kod
        }

        private void GeriBtn_Click(object sender, EventArgs e)
        {
            // Geri Butonuna bastığımda Stok Menüye geçmek için o Form sınıfından bir nesne oluşturuyorum.
            FrmStokMenu fr = new FrmStokMenu();
            fr.Show(); // oluşturduğum nesne ile formun gözükmesini sağlıyorum.
            this.Hide(); // Ve var olan formun gizli kalmasını sağlıyorum.
        }

        private void FrmEldiven_Load(object sender, EventArgs e)
        {
            // Eldiven Türleri için ComboBox'a Veri Ekleme
            cmbTür.Items.Add("Seçiniz..."); // PlaceHolder Yazısı
            cmbTür.Items.Add("Kaynakçı");
            cmbTür.Items.Add("Elektirikçi");
            cmbTür.Items.Add("Aşçı");
            cmbTür.Items.Add("İşçi");

            cmbTür.SelectedIndex = 0; // Başlangıç yazısı olarak seçiniz... yazısını ayarladım.
            cmbTür.SelectedIndexChanged += CmbTür_SelectedIndexChanged;

            // Eldiven Markası için ComboBox'a Veri Ekleme
            cmbMarka.Items.Add("Seçiniz...");
            cmbMarka.Items.Add("Beybi");
            cmbMarka.Items.Add("Dolphin");
            cmbMarka.Items.Add("Universal");

            cmbMarka.SelectedIndex = 0; // Başlangıç yazısı olarak seçiniz... yazısını ayarladım.

            dataGridView1.ReadOnly = false;

            listele();
        }

        private void CmbTür_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Eldivenin Türüne göre filtreleme işlemi yapıyorum.

            string selectedValue = cmbTür.SelectedItem.ToString(); // Oluşturduğum selectedValue değişkenini cmbTür deki seçilen veriye atama işlemi yapıyorum.

            if(selectedValue == "Seçiniz...") // Eğer seçilen değer Seçiniz... ise yani placeholder yazısı ise 
            {
                dataGridView1.DataSource = dt; // DataGridView'in veri kaynağını Data tablosundan aldığım verilere atama işlemini gerçekleştiriyorum ve listeliyorum.

                AscıEldiveni.Visible = false;
                IscıBeybi.Visible = false;
                KaynakcıUniversal.Visible = false;
                ElektirikciBeybi.Visible = false;

                LblAdet.Text = "";
                LblFiyat.Text = "";
                LblMarka.Text = "";
                LblTür.Text = "";

                txtAdet.Text = "";
                txtFiyat.Text = "";
                txtID.Text = "";
                cmbMarka.Text = "Seçiniz...";
                cmbTür.Text = "Seçiniz...";

            }
            else // Eğer Değilse
            {
                DataView dv = new DataView(dt); // DataView den bir nesne oluşturup filtreleyeceğim tabloyu belirtiyorum.
                dv.RowFilter = $"EldivenTürü = '{selectedValue}'"; // EldivenTürü column'a ait seçilen değeri filtreliyorum.
                dataGridView1.DataSource = dv; // Filtrelenmiş değeri listeliyorum.
            }
        }

        private void ListeleBtn_Click(object sender, EventArgs e)
        {
            // Yukarıda oluşturduğum listele fonksiyonunu çağırdım. 

            listele(); 
        }

        private void cmbTür_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kullanıcı ComboBox'ta değişiklik yapmasını engeller.
            e.Handled = true;
        }

        private void cmbMarka_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kullanıcı ComboBox'ta değişiklik yapmasını engeller.
            e.Handled = true;
        }

        private void EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı girdilerini kontrol et ve dönüştür
                if (!decimal.TryParse(txtFiyat.Text, out decimal eldivenFiyat))
                {
                    MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtAdet.Text, out int eldivenAdedi))
                {
                    MessageBox.Show("Lütfen geçerli bir adet giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbTür.Text))
                {
                    MessageBox.Show("Lütfen eldiven türünü seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbMarka.Text))
                {
                    MessageBox.Show("Lütfen eldiven markasını seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Eldiven türü ve marka bilgilerine göre mevcut kaydı kontrol et
                SqlCommand kontrolKomutu = new SqlCommand(
                    "SELECT COUNT(*) FROM Tbl_Eldivenler WHERE EldivenTürü = @p1 AND EldivenMarka = @p2",
                    bgl.baglanti());
                kontrolKomutu.Parameters.AddWithValue("@p1", cmbTür.Text);
                kontrolKomutu.Parameters.AddWithValue("@p2", cmbMarka.Text);

                int kayitSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    // Kayıt zaten varsa, mevcut stoğu ve fiyatı güncelle
                    SqlCommand guncelle = new SqlCommand(
                        "UPDATE Tbl_Eldivenler " +
                        "SET EldivenAdedi = EldivenAdedi + @p1, EldivenFiyat = @p2 " +
                        "WHERE EldivenTürü = @p3 AND EldivenMarka = @p4",
                        bgl.baglanti());
                    guncelle.Parameters.AddWithValue("@p1", eldivenAdedi); // Yeni eklenen adet
                    guncelle.Parameters.AddWithValue("@p2", eldivenFiyat); // Güncellenen fiyat
                    guncelle.Parameters.AddWithValue("@p3", cmbTür.Text); // Tür
                    guncelle.Parameters.AddWithValue("@p4", cmbMarka.Text); // Marka

                    guncelle.ExecuteNonQuery();
                    MessageBox.Show("Mevcut eldiven kaydının stoğu güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kayıt yoksa yeni kayıt ekle
                    SqlCommand ekle = new SqlCommand(
                        "INSERT INTO Tbl_Eldivenler (EldivenFiyat, EldivenAdedi, EldivenTürü, EldivenMarka) " +
                        "VALUES (@p1, @p2, @p3, @p4)",
                        bgl.baglanti());
                    ekle.Parameters.AddWithValue("@p1", eldivenFiyat);
                    ekle.Parameters.AddWithValue("@p2", eldivenAdedi);
                    ekle.Parameters.AddWithValue("@p3", cmbTür.Text);
                    ekle.Parameters.AddWithValue("@p4", cmbMarka.Text);

                    ekle.ExecuteNonQuery();
                    MessageBox.Show("Yeni eldiven başarıyla stoğa eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Eldiven Silme İşlemi
            try
            {
                SqlCommand sil = new SqlCommand("Delete From Tbl_Eldivenler where EldivenID=@p1", bgl.baglanti());
                sil.Parameters.AddWithValue("@p1", txtID.Text);
                sil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Eldiven Başarıyla Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Girdiğiniz Parametreleri Lütfen Kontrol Ediniz." + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GüncelleBtn_Click(object sender, EventArgs e)
        {
            // Eldiven Güncelleme İşlemi

            try
            {
                SqlCommand güncelle = new SqlCommand("Update Tbl_Eldivenler set EldivenFiyat=@p1,EldivenAdedi=@p2,EldivenTürü=@p3,EldivenMarka=@p4 where EldivenID=@p5", bgl.baglanti());
                güncelle.Parameters.AddWithValue("@p1", decimal.Parse(txtFiyat.Text));
                güncelle.Parameters.AddWithValue("@p2", int.Parse(txtAdet.Text));
                güncelle.Parameters.AddWithValue("@p3", cmbTür.Text);
                güncelle.Parameters.AddWithValue("@p4", cmbMarka.Text);
                güncelle.Parameters.AddWithValue("@p5", txtID.Text);
                güncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                LblAdet.Text = "Stok Adedi: " + int.Parse(txtAdet.Text);
                LblFiyat.Text = "Fiyat: " + decimal.Parse(txtFiyat.Text);
                LblMarka.Text = "Marka: " + cmbMarka.Text;
                LblTür.Text = "Türü: " + cmbTür.Text;
                //if(int.Parse(txtAdet.Text) < 20)
                //{
                //    MessageBox.Show("Bu eldiveni tedarik etmeniz gerek." + "\nÜrün Adedi: " + txtAdet.Text,"Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //MessageBox.Show("Eldiven Başarıyla Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz Parametreleri Lütfen Kontrol Ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtFiyat.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            //txtAdet.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbTür.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbMarka.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtAlisFiyat.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if(e.RowIndex >= 0)
            {
                LblAdet.Text = "Stok Adedi: " + row.Cells["EldivenAdedi"].Value.ToString();
                LblFiyat.Text = "Fiyat: " + row.Cells["EldivenFiyat"].Value.ToString();
                LblMarka.Text = "Marka: " + row.Cells["EldivenMarka"].Value.ToString();
                LblTür.Text = "Türü: " + row.Cells["EldivenTürü"].Value.ToString();
            }

            string eldivenTürü = row.Cells["EldivenTürü"].Value.ToString().Trim();

            AscıEldiveni.Visible = false;
            IscıBeybi.Visible = false;
            KaynakcıUniversal.Visible = false;
            ElektirikciBeybi.Visible = false;

            switch (eldivenTürü)
            {
                case "Elektirikçi":
                     ElektirikciBeybi.Visible=true;
                    break;
                case "Kaynakçı":
                    KaynakcıUniversal.Visible=true; 
                    break;
                case "Aşçı":
                    AscıEldiveni.Visible=true;
                    break;
                case "İşçi":
                     IscıBeybi.Visible = true;
                    break;
                default:
                    LblAdet.Text = "";
                    LblFiyat.Text = "";
                    LblMarka.Text = "";
                    LblTür.Text = "";

                    MessageBox.Show("İlgili markaya ait görsel bulunmamaktadır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                    
            }
        }

        private void btnSat_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıdan alınan veriler
                int eldivenId = int.Parse(txtID.Text); // Eldiven ID
                string eldivenTür = cmbTür.Text; // Eldiven Türü
                string eldivenMarka = cmbMarka.Text; // Eldiven Markası
                decimal satisFiyati = decimal.Parse(txtFiyat.Text); // Satış Fiyatı
                int satilanAdet = int.Parse(txtAdet.Text); // Satılan Adet
                DateTime satisTarihi = SatisTarihi.Value; // Satış Tarihi

                SqlConnection baglanti = bgl.baglanti();

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                // Tbl_Eldivenler tablosundan AlışFiyat ve mevcut stok bilgilerini al
                SqlCommand selectCommand = new SqlCommand(
                    "SELECT AlışFiyat, EldivenAdedi FROM Tbl_Eldivenler WHERE EldivenID = @id", baglanti);
                selectCommand.Parameters.AddWithValue("@id", eldivenId);

                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    decimal alisFiyati = reader["AlışFiyat"] != DBNull.Value ? Convert.ToDecimal(reader["AlışFiyat"]) : 0; // Alış Fiyatı
                    int mevcutAdet = reader["EldivenAdedi"] != DBNull.Value ? Convert.ToInt32(reader["EldivenAdedi"]) : 0; // Mevcut Stok
                    reader.Close();

                    // Stok kontrolü
                    if (mevcutAdet < satilanAdet)
                    {
                        MessageBox.Show("Yeterli stok yok!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tbl_Satis tablosunda mevcut bir kayıt var mı kontrol et
                    SqlCommand checkCommand = new SqlCommand(
                        "SELECT SatilanAdet FROM Tbl_Satis WHERE ÜrünID = @urunId AND ÜrünTürü = @urunTürü", baglanti);
                    checkCommand.Parameters.AddWithValue("@urunId", eldivenId);
                    checkCommand.Parameters.AddWithValue("@urunTürü", "Eldiven");

                    SqlDataReader satisReader = checkCommand.ExecuteReader();
                    if (satisReader.Read())
                    {
                        // Mevcut kaydı güncelle: SatilanAdet'i artır
                        int mevcutSatilanAdet = Convert.ToInt32(satisReader["SatilanAdet"]);
                        satisReader.Close();

                        SqlCommand updateCommand = new SqlCommand(
                            "UPDATE Tbl_Satis SET SatilanAdet = SatilanAdet + @satilanAdet, SatisFiyati = @satisFiyati, Tarih = @tarih " +
                            "WHERE ÜrünID = @urunId AND ÜrünTürü = @urunTürü", baglanti);
                        updateCommand.Parameters.AddWithValue("@satilanAdet", satilanAdet);
                        updateCommand.Parameters.AddWithValue("@satisFiyati", satisFiyati);
                        updateCommand.Parameters.AddWithValue("@tarih", satisTarihi);
                        updateCommand.Parameters.AddWithValue("@urunId", eldivenId);
                        updateCommand.Parameters.AddWithValue("@urunTürü", "Eldiven");

                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        satisReader.Close();

                        // Yeni kayıt ekle
                        SqlCommand insertCommand = new SqlCommand(
                            "INSERT INTO Tbl_Satis (ÜrünID, ÜrünTipi, ÜrünSeri, ÜrünTürü, AlisFiyati, SatisFiyati, SatilanAdet, Tarih, ÜrünBeden, ÜrünRenk) " +
                            "VALUES (@urunId, @urunTipi, @urunMarka, @urunTürü, @alisFiyati, @satisFiyati, @satilanAdet, @tarih, @urunBeden, @urunRenk)", baglanti);
                        insertCommand.Parameters.AddWithValue("@urunId", eldivenId);
                        insertCommand.Parameters.AddWithValue("@urunTipi", eldivenTür);
                        insertCommand.Parameters.AddWithValue("@urunMarka", eldivenMarka);
                        insertCommand.Parameters.AddWithValue("@urunTürü", "Eldiven");
                        insertCommand.Parameters.AddWithValue("@alisFiyati", alisFiyati);
                        insertCommand.Parameters.AddWithValue("@satisFiyati", satisFiyati);
                        insertCommand.Parameters.AddWithValue("@satilanAdet", satilanAdet);
                        insertCommand.Parameters.AddWithValue("@tarih", satisTarihi);
                        insertCommand.Parameters.AddWithValue("@urunBeden", "Beden Yok");
                        insertCommand.Parameters.AddWithValue("@urunRenk", "Renk Yok");

                        insertCommand.ExecuteNonQuery();
                    }

                    // Tbl_Eldivenler tablosunda stok adetini güncelle
                    SqlCommand updateStokCommand = new SqlCommand(
                        "UPDATE Tbl_Eldivenler SET EldivenAdedi = EldivenAdedi - @adet WHERE EldivenID = @id", baglanti);
                    updateStokCommand.Parameters.AddWithValue("@adet", satilanAdet);
                    updateStokCommand.Parameters.AddWithValue("@id", eldivenId);

                    updateStokCommand.ExecuteNonQuery();

                    MessageBox.Show("Satış başarıyla güncellendi veya eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Girilen ID'ye ait eldiven bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (bgl.baglanti().State == ConnectionState.Open)
                {
                    bgl.baglanti().Close();
                }
            }

        }
    }
}



