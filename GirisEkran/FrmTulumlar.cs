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
    public partial class FrmTulumlar : Form
    {
        public FrmTulumlar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        DataTable dt = new DataTable();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Tulumlar", bgl.baglanti());
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            LacivertBahçıvanPctr.Visible = false;
            TuruncuBahcıvanPctr.Visible = false;
            MaviBahcıvanPctr.Visible = false;
            İşTulumuLacivertPctr.Visible = false;
            IsTuruncuPctr.Visible = false;
            TyvekTulumPctr.Visible = false;
            ReflektörLacivertPctr.Visible = false;
            ReflektörTuruncuPctr.Visible = false;
            IsMaviTulumPicture.Visible = false;

            LblAdet.Text = " ";
            LblBeden.Text = " ";
            LblFiyat.Text = " ";
            LblRenk.Text = " ";
            LblTür.Text = " ";

            txtAdet.Text = "";
            txtFiyat.Text = "";
            txtID.Text = "";
            cmbBeden.Text = "Seçiniz...";
            cmbRenk.Text = "Seçiniz...";
            cmbTür.Text = "Seçiniz...";
            txtAlisFiyat.Text = "";
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

        private void FrmTulumlar_Load(object sender, EventArgs e)
        {
            cmbRenk.Items.Add("Seçiniz..."); // PlaceHolder Yazısı
            cmbRenk.Items.Add("Beyaz");
            cmbRenk.Items.Add("Lacivert");
            cmbRenk.Items.Add("Mavi");
            cmbRenk.Items.Add("Turuncu");

            cmbRenk.SelectedIndex = 0;
            //cmbRenk.SelectedIndexChanged += CmbRenk_SelectedIndexChanged;

            cmbTür.Items.Add("Seçiniz..."); // PlaceHolder Yazısı
            cmbTür.Items.Add("Bahçıvan");
            cmbTür.Items.Add("Tyvek");
            cmbTür.Items.Add("Reflektör");
            cmbTür.Items.Add("İş");

            cmbTür .SelectedIndex = 0;
            cmbTür.SelectedIndexChanged += CmbTür_SelectedIndexChanged;

            cmbBeden.Items.Add("Seçiniz..."); // PlaceHolder Yazısı
            cmbBeden.Items.Add("S");
            cmbBeden.Items.Add("M");
            cmbBeden.Items.Add("L");
            cmbBeden.Items.Add("XL");
            cmbBeden.Items.Add("2XL");
            cmbBeden.Items.Add("3XL");
            cmbBeden.SelectedIndex = 0;

            LacivertBahçıvanPctr.Visible = false;
            TuruncuBahcıvanPctr.Visible = false;
            MaviBahcıvanPctr.Visible = false;
            İşTulumuLacivertPctr.Visible = false;
            IsTuruncuPctr.Visible = false;
            TyvekTulumPctr.Visible = false;
            ReflektörLacivertPctr.Visible = false;
            ReflektörTuruncuPctr.Visible = false;
            IsMaviTulumPicture.Visible = false;

            LblAdet.Text = " ";
            LblBeden.Text = " ";
            LblFiyat.Text = " ";
            LblRenk.Text = " ";
            LblTür.Text = " ";

            dataGridView1.ReadOnly = true;


            listele();

        }

        private void CmbTür_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cmbTür.SelectedItem.ToString();

            if(selectedValue == "Seçiniz...")
            {
                dataGridView1.DataSource = dt;

                LacivertBahçıvanPctr.Visible = false;
                TuruncuBahcıvanPctr.Visible = false;
                MaviBahcıvanPctr.Visible = false;
                İşTulumuLacivertPctr.Visible = false;
                IsTuruncuPctr.Visible = false;
                TyvekTulumPctr.Visible = false;
                ReflektörLacivertPctr.Visible = false;
                ReflektörTuruncuPctr.Visible = false;
                IsMaviTulumPicture.Visible = false; 

                LblAdet.Text = " ";
                LblBeden.Text = " ";
                LblFiyat.Text = " ";
                LblRenk.Text = " ";
                LblTür.Text = " ";
            }
            else
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = $"TulumTürü = '{selectedValue}'";
                dataGridView1.DataSource = dv;
            }
        }

        private void cmbRenk_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbTür_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void EkleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı girdilerini kontrol et ve dönüştür
                if (!decimal.TryParse(txtFiyat.Text, out decimal tulumFiyati))
                {
                    MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtAdet.Text, out int tulumAdedi))
                {
                    MessageBox.Show("Lütfen geçerli bir adet giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbTür.Text))
                {
                    MessageBox.Show("Lütfen tulum türünü seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbRenk.Text))
                {
                    MessageBox.Show("Lütfen tulum rengini seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbBeden.Text))
                {
                    MessageBox.Show("Lütfen tulum bedenini seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mevcut kaydı kontrol et
                SqlCommand kontrolKomutu = new SqlCommand(
                    "SELECT COUNT(*) FROM Tbl_Tulumlar WHERE TulumTürü = @p1 AND TulumRenk = @p2 AND TulumBeden = @p3",
                    bgl.baglanti());
                kontrolKomutu.Parameters.AddWithValue("@p1", cmbTür.Text);
                kontrolKomutu.Parameters.AddWithValue("@p2", cmbRenk.Text);
                kontrolKomutu.Parameters.AddWithValue("@p3", cmbBeden.Text);

                int kayitSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    // Eğer kayıt zaten varsa, stoğu güncelle
                    SqlCommand guncelle = new SqlCommand(
                        "UPDATE Tbl_Tulumlar " +
                        "SET TulumAdedi = TulumAdedi + @p1, TulumFiyati = @p2 " +
                        "WHERE TulumTürü = @p3 AND TulumRenk = @p4 AND TulumBeden = @p5",
                        bgl.baglanti());
                    guncelle.Parameters.AddWithValue("@p1", tulumAdedi); // Yeni eklenen adet
                    guncelle.Parameters.AddWithValue("@p2", tulumFiyati); // Güncel fiyat
                    guncelle.Parameters.AddWithValue("@p3", cmbTür.Text); // Tür
                    guncelle.Parameters.AddWithValue("@p4", cmbRenk.Text); // Renk
                    guncelle.Parameters.AddWithValue("@p5", cmbBeden.Text); // Beden
                    guncelle.ExecuteNonQuery();
                    MessageBox.Show("Mevcut tulum kaydının stoğu güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Eğer kayıt yoksa yeni kayıt ekle
                    SqlCommand ekle = new SqlCommand(
                        "INSERT INTO Tbl_Tulumlar (TulumFiyati, TulumAdedi, TulumTürü, TulumRenk, TulumBeden) " +
                        "VALUES (@p1, @p2, @p3, @p4, @p5)",
                        bgl.baglanti());
                    ekle.Parameters.AddWithValue("@p1", tulumFiyati);
                    ekle.Parameters.AddWithValue("@p2", tulumAdedi);
                    ekle.Parameters.AddWithValue("@p3", cmbTür.Text);
                    ekle.Parameters.AddWithValue("@p4", cmbRenk.Text);
                    ekle.Parameters.AddWithValue("@p5", cmbBeden.Text);

                    ekle.ExecuteNonQuery();
                    MessageBox.Show("Yeni tulum başarıyla stoğa eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ListeleBtn_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void SilBtn_Click(object sender, EventArgs e)
        {
            // Tulum Silme İşlemi
            try
            {
                SqlCommand sil = new SqlCommand("Delete From Tbl_Tulumlar where TulumID=@p1", bgl.baglanti());
                sil.Parameters.AddWithValue("@p1", txtID.Text);
                sil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Tulum Başarıyla Stoktan Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz Parametreleri Lütfen Kontrol Ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GüncelleBtn_Click(object sender, EventArgs e)
        {
            //Tulum Güncelleme İşlemi
            try
            {
                SqlCommand güncelle = new SqlCommand("Update Tbl_Tulumlar set TulumFiyati=@p1, TulumAdedi=@p2, TulumTürü=@p3, TulumRenk=@p4, TulumBeden=@p5 where TulumID=@p6", bgl.baglanti());
                güncelle.Parameters.AddWithValue("@p1", decimal.Parse(txtFiyat.Text));
                güncelle.Parameters.AddWithValue("@p2", int.Parse(txtAdet.Text));
                güncelle.Parameters.AddWithValue("@p3", cmbTür.Text);
                güncelle.Parameters.AddWithValue("@p4", cmbRenk.Text);
                güncelle.Parameters.AddWithValue("@p5", cmbBeden.Text);
                güncelle.Parameters.AddWithValue("@p6", txtID.Text);
                güncelle.ExecuteNonQuery();
                bgl.baglanti().Close();
                LblAdet.Text = "Stok Adedi: " + int.Parse(txtAdet.Text);
                LblBeden.Text = "Beden: " + cmbBeden.Text;
                LblFiyat.Text = "Fiyat: " + decimal.Parse(txtFiyat.Text);
                LblTür.Text = "Türü: " + cmbTür.Text;
                LblRenk.Text = "Renk: " + cmbRenk.Text; 
                MessageBox.Show("Tulum Başarıyla Güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz Parametreleri Lütfen Kontrol Ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}



        private void cmbBeden_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Burayı hocaya soracağım sürekli bir hata alıyorum sütunları satırları sütunları kontrol etmeme rağmen index hatası alıyorum.
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            cmbBeden.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            //txtAdet.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtFiyat.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            cmbTür.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbRenk.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtAlisFiyat.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if(e.RowIndex >= 0)
            {
                LblTür.Text = "Türü: " + row.Cells["TulumTürü"].Value.ToString();
                LblBeden.Text = "Beden: " + row.Cells["TulumBeden"].Value.ToString();
                LblRenk.Text = "Renk: " + row.Cells["TulumRenk"].Value.ToString();
                LblFiyat.Text = "Fiyat: " + row.Cells["TulumFiyati"].Value.ToString();
                LblAdet.Text = "Stok Adedi: " + row.Cells["TulumAdedi"].Value.ToString();
            }

            string tulumtür = row.Cells["TulumTürü"].Value.ToString().Trim();
            
            LacivertBahçıvanPctr.Visible = false;
            TuruncuBahcıvanPctr.Visible = false;
            MaviBahcıvanPctr.Visible = false;
            İşTulumuLacivertPctr.Visible = false;
            IsTuruncuPctr.Visible = false;
            TyvekTulumPctr.Visible = false;
            ReflektörLacivertPctr.Visible = false;
            ReflektörTuruncuPctr.Visible = false;
            IsMaviTulumPicture.Visible = false;

            switch (tulumtür)
            {
                case "Bahçıvan":
                    if (row.Cells["TulumRenk"].Value.ToString() == "Lacivert")
                    {
                        LacivertBahçıvanPctr.Visible = true;
                    }
                    if (row.Cells["TulumRenk"].Value.ToString() == "Mavi")
                    {
                        MaviBahcıvanPctr.Visible = true;
                    }
                    if (row.Cells["TulumRenk"].Value.ToString() == "Turuncu")
                    {
                        TuruncuBahcıvanPctr.Visible = true;
                    }
                    break;
                case "Tyvek":
                    if (row.Cells["TulumRenk"].Value.ToString() == "Beyaz")
                    {
                        TyvekTulumPctr.Visible = true;
                    }
                    break;
                case "İş":
                    if (row.Cells["TulumRenk"].Value.ToString() == "Turuncu")
                    {
                        IsTuruncuPctr.Visible = true;
                    }
                    if (row.Cells["TulumRenk"].Value.ToString() == "Lacivert")
                    {
                        İşTulumuLacivertPctr.Visible = true;
                    }
                    if (row.Cells["TulumRenk"].Value.ToString() == "Mavi")
                    {
                        IsMaviTulumPicture.Visible = true;
                    }
                    break;
                case "Reflektör":
                    if (row.Cells["TulumRenk"].Value.ToString() == "Lacivert")
                    {
                        ReflektörLacivertPctr.Visible = true;
                    }
                    if (row.Cells["TulumRenk"].Value.ToString() == "Turuncu")
                    {
                        ReflektörTuruncuPctr.Visible = true;
                    }
                    break;
                default:
                    LblAdet.Text = " ";
                    LblBeden.Text = " ";
                    LblFiyat.Text = " ";
                    LblRenk.Text = " ";
                    LblTür.Text = " ";
                    
                    MessageBox.Show("İlgili markaya ait görsel bulunamadı!","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void btnSat_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıdan alınan veriler
                int tulumId = int.Parse(txtID.Text); // Tulum ID
                string tulumRenk = cmbRenk.Text; // Tulum Renk
                decimal satisFiyati = decimal.Parse(txtFiyat.Text); // Satış Fiyatı
                int satilanAdet = int.Parse(txtAdet.Text); // Satılan Adet
                string tulumBeden = cmbBeden.Text; // Tulum Beden
                string tulumTür = cmbTür.Text; // Tulum Tür
                DateTime satisTarih = SatisTarih.Value; // Satış Tarihi

                // SQL bağlantısı
                bgl.baglanti();
                {
                    // Tbl_Tulumlar tablosundan AlışFiyat ve mevcut stok bilgilerini al
                    string selectQuery = "SELECT AlışFiyat, TulumAdedi FROM Tbl_Tulumlar WHERE TulumID = @id";
                    SqlCommand selectCommand = new SqlCommand(selectQuery, bgl.baglanti());
                    selectCommand.Parameters.AddWithValue("@id", tulumId);

                    SqlDataReader reader = selectCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal alisFiyati = reader["AlışFiyat"] != DBNull.Value ? Convert.ToDecimal(reader["AlışFiyat"]) : 0; // Alış Fiyatı
                        int mevcutAdet = reader["TulumAdedi"] != DBNull.Value ? Convert.ToInt32(reader["TulumAdedi"]) : 0; // Mevcut Stok
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
                        checkCommand.Parameters.AddWithValue("@urunId", tulumId);
                        checkCommand.Parameters.AddWithValue("@urunRenk", tulumRenk);
                        checkCommand.Parameters.AddWithValue("@urunBeden", tulumBeden);

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
                            updateSalesCommand.Parameters.AddWithValue("@urunId", tulumId);
                            updateSalesCommand.Parameters.AddWithValue("@urunRenk", tulumRenk);
                            updateSalesCommand.Parameters.AddWithValue("@urunBeden", tulumBeden);

                            updateSalesCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            checkReader.Close();

                            // Ürün ilk kez satılıyor, yeni bir satır ekle
                            string insertQuery = "INSERT INTO Tbl_Satis (ÜrünID, ÜrünRenk, ÜrünBeden, ÜrünTipi, ÜrünTürü, AlisFiyati, SatisFiyati, SatilanAdet, Tarih, ÜrünSeri) " +
                                                 "VALUES (@urunId, @urunRenk, @urunBeden, @urunTipi, @urunTürü, @alisFiyati, @satisFiyati, @satilanAdet, @tarih ,@urunSeri)";
                            SqlCommand insertCommand = new SqlCommand(insertQuery, bgl.baglanti());
                            insertCommand.Parameters.AddWithValue("@urunId", tulumId);
                            insertCommand.Parameters.AddWithValue("@urunRenk", tulumRenk);
                            insertCommand.Parameters.AddWithValue("@urunBeden", tulumBeden);
                            insertCommand.Parameters.AddWithValue("@urunTipi", tulumTür);
                            insertCommand.Parameters.AddWithValue("@urunTürü", "Tulum");
                            insertCommand.Parameters.AddWithValue("@alisFiyati", alisFiyati);
                            insertCommand.Parameters.AddWithValue("@satisFiyati", satisFiyati);
                            insertCommand.Parameters.AddWithValue("@satilanAdet", satilanAdet);
                            insertCommand.Parameters.AddWithValue("@tarih", satisTarih);
                            insertCommand.Parameters.AddWithValue("@urunSeri", "Seri Yok");

                            insertCommand.ExecuteNonQuery();
                        }

                        // Tbl_Tulumlar tablosunda stok adetini güncelle
                        string updateQuery = "UPDATE Tbl_Tulumlar SET TulumAdedi = TulumAdedi - @adet WHERE TulumID = @id";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, bgl.baglanti());
                        updateCommand.Parameters.AddWithValue("@adet", satilanAdet);
                        updateCommand.Parameters.AddWithValue("@id", tulumId);

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Satış başarıyla gerçekleştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Girilen ID'ye ait tulum bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

       

