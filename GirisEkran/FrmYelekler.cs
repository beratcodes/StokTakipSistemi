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
    public partial class FrmYelekler : Form
    {
        public FrmYelekler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        DataTable dt = new DataTable();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Yelekler", bgl.baglanti());
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            CokCepliGri.Visible = false;
            CokCepliKahve.Visible = false;
            DeriYelekKahve.Visible = false;
            MühYelekSarı.Visible = false;
            MühYelekTuruncu.Visible = false;
            IsciYelekSari.Visible = false;

            LblAdet.Text = "";
            LblBeden.Text = "";
            LblFiyat.Text = "";
            LblRenk.Text = "";
            LblYelekTür.Text = "";

            txtAdet.Text = "";
            txtFiyat.Text = "";
            txtID.Text = "";
            cmbBeden.Text = "Seçiniz...";
            cmbRenk.Text = "Seçiniz...";
            cmbTür.Text = "Seçiniz...";
            txtAlisFiyat.Text = "";
        }

        

        private void FrmYelekler_Load(object sender, EventArgs e)
        {
            // Yelek Türleri'ni combobox'a ekleme.

            cmbTür.Items.Add("Seçiniz..."); // PlaaceHolder yazısı
            cmbTür.Items.Add("Çok Cepli");
            cmbTür.Items.Add("Deri");
            cmbTür.Items.Add("Mühendis");
            cmbTür.Items.Add("İşçi");

            cmbTür.SelectedIndex = 0; // Combobox'taki Başlangış Değeri varsayılan olarak seçinizin indexi olan 0'a ayarladım.

            cmbTür.SelectedIndexChanged += CmbTür_SelectedIndexChanged;

            // Yelek Bedenlerini Combobox'a ekleme.

            cmbBeden.Items.Add("Seçiniz...");
            cmbBeden.Items.Add("S");
            cmbBeden.Items.Add("M");
            cmbBeden.Items.Add("L");
            cmbBeden.Items.Add("XL");
            cmbBeden.Items.Add("2XL");
            cmbBeden.Items.Add("3XL");

            cmbBeden.SelectedIndex = 0;

            // Yelek Renklerini Combobox'a ekleme

            cmbRenk.Items.Add("Seçiniz...");
            cmbRenk.Items.Add("Sarı");
            cmbRenk.Items.Add("Turuncu");
            cmbRenk.Items.Add("Kahverengi");
            cmbRenk.Items.Add("Lacivert");
            cmbRenk.Items.Add("Gri");

            cmbRenk.SelectedIndex = 0;

            dataGridView1.ReadOnly = true;

            CokCepliGri.Visible = false;
            CokCepliKahve.Visible = false;
            DeriYelekKahve.Visible = false;
            MühYelekSarı.Visible = false;
            MühYelekTuruncu.Visible = false;
            IsciYelekSari.Visible = false;

            LblAdet.Text = "";
            LblBeden.Text = "";
            LblFiyat.Text = "";
            LblRenk.Text = "";
            LblYelekTür.Text = "";

            listele();
            
        }   

        private void CmbTür_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cmbTür.SelectedItem.ToString();

            if(selectedValue == "Seçiniz...")
            {
                dataGridView1.DataSource = dt;

                CokCepliGri.Visible = false;
                CokCepliKahve.Visible = false;
                DeriYelekKahve.Visible = false;
                MühYelekSarı.Visible = false;
                MühYelekTuruncu.Visible = false;
                IsciYelekSari.Visible = false;

                LblAdet.Text = "";
                LblBeden.Text = "";
                LblFiyat.Text = "";
                LblRenk.Text = "";
                LblYelekTür.Text = "";

                txtAdet.Text = "";
                txtFiyat.Text = "";
                txtID.Text = "";
                cmbBeden.Text = "Seçiniz...";
                cmbRenk.Text = "Seçiniz...";
                cmbTür.Text = "Seçiniz...";
            }
            else
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = $"YelekTür = '{selectedValue}'";
                dataGridView1.DataSource = dv;
            }
        }

        private void CarpiBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GeriBtn_Click(object sender, EventArgs e)
        {
            FrmStokMenu fr = new FrmStokMenu();
            fr.Show();
            this.Hide();
        }

        private void cmbRenk_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        private void cmbTür_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbBeden_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ListeleBtn_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı girdilerini kontrol et ve dönüştür
                if (string.IsNullOrWhiteSpace(cmbBeden.Text))
                {
                    MessageBox.Show("Lütfen yelek bedenini seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtAdet.Text, out int yelekAdedi))
                {
                    MessageBox.Show("Lütfen geçerli bir adet giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtFiyat.Text, out decimal yelekFiyat))
                {
                    MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbTür.Text))
                {
                    MessageBox.Show("Lütfen yelek türünü seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbRenk.Text))
                {
                    MessageBox.Show("Lütfen yelek rengini seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtAlisFiyat.Text, out decimal alisFiyat))
                {
                    MessageBox.Show("Lütfen geçerli bir alış fiyatı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Veritabanında aynı tür, beden ve renk bilgisine sahip bir yelek var mı kontrol et
                SqlCommand kontrolKomutu = new SqlCommand(
                    "SELECT COUNT(*) FROM Tbl_Yelekler WHERE YelekBeden = @p1 AND YelekTür = @p2 AND YelekRenk = @p3",
                    bgl.baglanti());
                kontrolKomutu.Parameters.AddWithValue("@p1", cmbBeden.Text);
                kontrolKomutu.Parameters.AddWithValue("@p2", cmbTür.Text);
                kontrolKomutu.Parameters.AddWithValue("@p3", cmbRenk.Text);

                int kayitSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    // Kayıt zaten varsa mevcut stoğu ve fiyatı güncelle
                    SqlCommand guncelle = new SqlCommand(
                        "UPDATE Tbl_Yelekler " +
                        "SET YelekAdedi = YelekAdedi + @p1, YelekFiyat = @p2 " +
                        "WHERE YelekBeden = @p3 AND YelekTür = @p4 AND YelekRenk = @p5 AND AlışFiyat = @p6",
                        bgl.baglanti());
                    guncelle.Parameters.AddWithValue("@p1", yelekAdedi); // Yeni eklenen adet
                    guncelle.Parameters.AddWithValue("@p2", yelekFiyat); // Güncellenen fiyat
                    guncelle.Parameters.AddWithValue("@p3", cmbBeden.Text); // Beden
                    guncelle.Parameters.AddWithValue("@p4", cmbTür.Text); // Tür
                    guncelle.Parameters.AddWithValue("@p5", cmbRenk.Text); // Renk
                    guncelle.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisFiyat.Text)); // Alış Fiyat

                    guncelle.ExecuteNonQuery();
                    MessageBox.Show("Mevcut yelek kaydının stoğu güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kayıt yoksa yeni kayıt ekle
                    SqlCommand ekle = new SqlCommand(
                        "INSERT INTO Tbl_Yelekler (YelekBeden, YelekAdedi, YelekFiyat, YelekTür, YelekRenk, AlışFiyat) " +
                        "VALUES (@p1, @p2, @p3, @p4, @p5)",
                        bgl.baglanti());
                    ekle.Parameters.AddWithValue("@p1", cmbBeden.Text);
                    ekle.Parameters.AddWithValue("@p2", yelekAdedi);
                    ekle.Parameters.AddWithValue("@p3", yelekFiyat);
                    ekle.Parameters.AddWithValue("@p4", cmbTür.Text);
                    ekle.Parameters.AddWithValue("@p5", cmbRenk.Text);
                    ekle.Parameters.AddWithValue("@p6", txtAlisFiyat.Text);

                    ekle.ExecuteNonQuery();
                    MessageBox.Show("Yeni yelek başarıyla stoğa eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Yelek Silme İşlemi
            try
            {
                SqlCommand sil = new SqlCommand("Delete From Tbl_Yelekler where YelekID=@p1", bgl.baglanti());
                sil.Parameters.AddWithValue("@p1", txtID.Text);
                sil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yelek Başarıyla Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz Parametreleri Lütfen Kontrol Ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GüncelleBtn_Click(object sender, EventArgs e)
        {
            // Yelek Güncelleme İşlemi 

            try
            {
                SqlCommand güncelle = new SqlCommand("Update Tbl_Yelekler set YelekAdedi=@p1,YelekFiyat=@p2,YelekTür=@p3,YelekRenk=@p4, YelekBeden=@p5 where YelekID=@p6", bgl.baglanti());
                güncelle.Parameters.AddWithValue("@p1", int.Parse(txtAdet.Text));
                güncelle.Parameters.AddWithValue("@p2", decimal.Parse(txtFiyat.Text));
                güncelle.Parameters.AddWithValue("@p3", cmbTür.Text);
                güncelle.Parameters.AddWithValue("@p4", cmbRenk.Text);
                güncelle.Parameters.AddWithValue("@p5", cmbBeden.Text);
                güncelle.Parameters.AddWithValue("@p6", txtID.Text);
                güncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                LblAdet.Text = "Stok Adedi: " + int.Parse(txtAdet.Text);
                LblBeden.Text = "Beden: " + cmbBeden.Text;
                LblFiyat.Text = "Fiyat: " + decimal.Parse(txtFiyat.Text);
                LblRenk.Text = "Renk: " + cmbRenk.Text;
                LblYelekTür.Text = "Yelek Türü: " + cmbTür.Text;
                MessageBox.Show("Yelek Başarıyla Güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            cmbBeden.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtFiyat.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbTür.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbRenk.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtAlisFiyat.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if(e.RowIndex >= 0)
            {
                LblAdet.Text = "Stok Adedi: " + row.Cells["YelekAdedi"].Value.ToString();
                LblBeden.Text = "Beden: " + row.Cells["YelekBeden"].Value.ToString();
                LblFiyat.Text = "Fiyat: " + row.Cells["YelekFiyat"].Value.ToString();
                LblRenk.Text = "Renk: " + row.Cells["YelekRenk"].Value.ToString();
                LblYelekTür.Text = "Yelek Türü: " + row.Cells["YelekTür"].Value.ToString();
            }

            string yelekTür = row.Cells["YelekTür"].Value.ToString().Trim();

            CokCepliGri.Visible = false;
            CokCepliKahve.Visible = false;
            DeriYelekKahve.Visible = false;
            MühYelekSarı.Visible = false;
            MühYelekTuruncu.Visible = false;
            IsciYelekSari.Visible = false;

            switch (yelekTür)
            {
                case "Çok Cepli":
                    if (row.Cells["YelekRenk"].Value.ToString() == "Kahverengi")
                    {
                        CokCepliKahve.Visible = true;
                    }
                    if (row.Cells["YelekRenk"].Value.ToString() == "Gri")
                    {
                        CokCepliGri.Visible = true;
                    }
                    break;
                case "Deri":
                    if (row.Cells["YelekRenk"].Value.ToString() == "Kahverengi")
                    {
                        DeriYelekKahve.Visible = true;
                    }
                    break;
                case "Mühendis":
                    if (row.Cells["YelekRenk"].Value.ToString() == "Turuncu")
                    {
                        MühYelekTuruncu.Visible = true;
                    }
                    if (row.Cells["YelekRenk"].Value.ToString() == "Sarı")
                    {
                        MühYelekSarı.Visible = true;
                    }
                    break;
                case "İşçi":
                    if (row.Cells["YelekRenk"].Value.ToString() == "Sarı")
                    {
                        IsciYelekSari.Visible = true;   
                    }
                    break;
                default:
                    LblAdet.Text = "";
                    LblBeden.Text = "";
                    LblFiyat.Text = "";
                    LblRenk.Text = "";
                    LblYelekTür.Text = "";

                    MessageBox.Show("İlgili markaya ait görsel bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            
        }

        private void btnSat_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıdan alınan veriler
                int yelekId = int.Parse(txtID.Text); // Yelek ID
                string yelekRenk = cmbRenk.Text; // Yelek Renk
                string yelekTür = cmbTür.Text; // Yelek Tür
                decimal satisFiyati = decimal.Parse(txtFiyat.Text); // Satış Fiyatı
                int satilanAdet = int.Parse(txtAdet.Text); // Satılan Adet
                string yelekBeden = cmbBeden.Text; // Yelek Beden
                DateTime satisTarih = SatisTarihi.Value; // Satış Tarihi

                // SQL bağlantısı
                bgl.baglanti();
                {
                    // Tbl_Yelekler tablosundan AlışFiyat ve mevcut stok bilgilerini al
                    string selectQuery = "SELECT AlışFiyat, YelekAdedi FROM Tbl_Yelekler WHERE YelekID = @id";
                    SqlCommand selectCommand = new SqlCommand(selectQuery, bgl.baglanti());
                    selectCommand.Parameters.AddWithValue("@id", yelekId);

                    SqlDataReader reader = selectCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal alisFiyati = reader["AlışFiyat"] != DBNull.Value ? Convert.ToDecimal(reader["AlışFiyat"]) : 0; // Alış Fiyatı
                        int mevcutAdet = reader["YelekAdedi"] != DBNull.Value ? Convert.ToInt32(reader["YelekAdedi"]) : 0; // Mevcut Stok
                        reader.Close();

                        // Stok kontrolü
                        if (mevcutAdet < satilanAdet)
                        {
                            MessageBox.Show("Yeterli stok yok!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Tbl_Satis tablosunda aynı ürün zaten var mı kontrol et
                        string checkQuery = "SELECT SatilanAdet FROM Tbl_Satis WHERE ÜrünID = @urunId AND ÜrünRenk = @urunRenk AND ÜrünBeden = @urunBeden";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, bgl.baglanti());
                        checkCommand.Parameters.AddWithValue("@urunId", yelekId);
                        checkCommand.Parameters.AddWithValue("@urunRenk", yelekRenk);
                        checkCommand.Parameters.AddWithValue("@urunBeden", yelekBeden);

                        SqlDataReader checkReader = checkCommand.ExecuteReader();
                        if (checkReader.Read())
                        {
                            // Ürün zaten satılmış, SatilanAdet güncellenmeli
                            int mevcutSatilanAdet = Convert.ToInt32(checkReader["SatilanAdet"]);
                            checkReader.Close();

                            string updateSalesQuery = "UPDATE Tbl_Satis SET SatilanAdet = SatilanAdet + @satilanAdet, Tarih = @tarih WHERE ÜrünID = @urunId AND ÜrünRenk = @urunRenk AND ÜrünBeden = @urunBeden";
                            SqlCommand updateSalesCommand = new SqlCommand(updateSalesQuery, bgl.baglanti());
                            updateSalesCommand.Parameters.AddWithValue("@satilanAdet", satilanAdet);
                            updateSalesCommand.Parameters.AddWithValue("@tarih", satisTarih);
                            updateSalesCommand.Parameters.AddWithValue("@urunId", yelekId);
                            updateSalesCommand.Parameters.AddWithValue("@urunRenk", yelekRenk);
                            updateSalesCommand.Parameters.AddWithValue("@urunBeden", yelekBeden);

                            updateSalesCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            checkReader.Close();

                            // Ürün ilk kez satılıyor, yeni bir satır ekle
                            string insertQuery = "INSERT INTO Tbl_Satis (ÜrünID, ÜrünTipi, ÜrünRenk, ÜrünBeden, ÜrünTürü, AlisFiyati, SatisFiyati, SatilanAdet, Tarih, ÜrünSeri) " +
                                                 "VALUES (@urunId, @urunTipi, @urunRenk, @urunBeden, @urunTürü, @alisFiyati, @satisFiyati, @satilanAdet, @tarih, @urunSeri)";
                            SqlCommand insertCommand = new SqlCommand(insertQuery, bgl.baglanti());
                            insertCommand.Parameters.AddWithValue("@urunId", yelekId);
                            insertCommand.Parameters.AddWithValue("@urunTipi", yelekTür);
                            insertCommand.Parameters.AddWithValue("@urunRenk", yelekRenk);
                            insertCommand.Parameters.AddWithValue("@urunBeden", yelekBeden);
                            insertCommand.Parameters.AddWithValue("@urunTürü", "Yelek");
                            insertCommand.Parameters.AddWithValue("@alisFiyati", alisFiyati);
                            insertCommand.Parameters.AddWithValue("@satisFiyati", satisFiyati);
                            insertCommand.Parameters.AddWithValue("@satilanAdet", satilanAdet);
                            insertCommand.Parameters.AddWithValue("@tarih", satisTarih);
                            insertCommand.Parameters.AddWithValue("@urunSeri", "Seri Yok");

                            insertCommand.ExecuteNonQuery();
                        }

                        // Tbl_Yelekler tablosunda stok adetini güncelle
                        string updateQuery = "UPDATE Tbl_Yelekler SET YelekAdedi = YelekAdedi - @adet WHERE YelekID = @id";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, bgl.baglanti());
                        updateCommand.Parameters.AddWithValue("@adet", satilanAdet);
                        updateCommand.Parameters.AddWithValue("@id", yelekId);

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Satış başarıyla gerçekleştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Girilen ID'ye ait yelek bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
