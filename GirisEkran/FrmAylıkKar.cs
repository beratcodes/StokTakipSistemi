﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GirisEkran
{
    public partial class LblEldivenKar : Form
    {
        public LblEldivenKar()
        {
            InitializeComponent();
        }
        
        SqlBaglantisi bgl = new SqlBaglantisi();
        DataTable dt = new DataTable();

        private void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Satis", bgl.baglanti());
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        
        private void FrmSayim_Load(object sender, EventArgs e)
        {
            listele();
            txtKdv.Text = "18";
            txtKdv.ReadOnly = true;
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

        private void btnGunluk_Click(object sender, EventArgs e)
        {
            // Bugünün tarihini al (sadece gün, ay ve yıl)
            DateTime bugun = DateTime.Today;

            // SQL sorgusu: Tarih sütununu bugüne göre filtrele
            string sorgu = @"
            SELECT SatisID, ÜrünID, ÜrünTürü, ÜrünTipi, ÜrünSeri, ÜrünRenk, ÜrünBeden, 
            AlisFiyati, SatisFiyati, SatilanAdet, Tarih
            FROM Tbl_Satis
            WHERE CAST(Tarih AS DATE) = @BugunTarih";

        SqlCommand command = new SqlCommand(sorgu, bgl.baglanti());
        {
         // Parametreyi ekle (bugünkü tarih)
         command.Parameters.AddWithValue("@BugunTarih", bugun);

         // Veritabanını aç ve verileri al
         SqlDataAdapter da = new SqlDataAdapter(command);
         DataTable dt = new DataTable();
         da.Fill(dt);

         // DataGridView'i doldur
         dataGridView1.DataSource = dt;
        }        
            
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            // Her ürün türü için net kârları saklamak için değişkenler
            decimal ayakkabiKar = 0, pantolonKar = 0, tulumKar = 0,
                    baretKar = 0, yelekKar = 0, eldivenKar = 0;

            // KDV oranını txtKdv'den al ve %'ye çevir
            decimal kdvOrani;
            if (!decimal.TryParse(txtKdv.Text, out kdvOrani))
            {
                MessageBox.Show("Geçerli bir KDV oranı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            kdvOrani /= 100; // %18 -> 0.18 formatına dönüştür

            // DataGridView'deki her satırı döngüyle dolaş
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Satırda "ÜrünTürü" hücresinde bir veri varsa işleme devam et
                if (row.Cells["ÜrünTürü"].Value != null)
                {
                    // "ÜrünTürü" bilgisini al
                    string urunTuru = row.Cells["ÜrünTürü"].Value.ToString();

                    // Alış fiyatını, satış fiyatını ve satılan adet bilgisini al
                    decimal alisFiyati = Convert.ToDecimal(row.Cells["AlisFiyati"].Value);
                    decimal satisFiyati = Convert.ToDecimal(row.Cells["SatisFiyati"].Value);
                    int satilanAdet = Convert.ToInt32(row.Cells["SatilanAdet"].Value);

                    // KDV'yi alış ve satış fiyatlarına uygula
                    decimal alisFiyatiKdvli = alisFiyati * (1 + kdvOrani);
                    decimal satisFiyatiKdvli = satisFiyati * (1 + kdvOrani);

                    // Net kârı hesapla: (Satış Fiyatı (KDV'li) - Alış Fiyatı (KDV'li)) × Satılan Adet
                    decimal netKar = (satisFiyatiKdvli - alisFiyatiKdvli) * satilanAdet;

                    // Ürün türüne göre net kârı ilgili değişkene ekle
                    switch (urunTuru)
                    {
                        case "Ayakkabı":
                            ayakkabiKar += netKar;
                            break;
                        case "Pantolon":
                            pantolonKar += netKar;
                            break;
                        case "Tulum":
                            tulumKar += netKar;
                            break;
                        case "Baret":
                            baretKar += netKar;
                            break;
                        case "Yelek":
                            yelekKar += netKar;
                            break;
                        case "Eldiven":
                            eldivenKar += netKar;
                            break;
                    }
                }
            }

            // Hesaplanan net kârları form üzerindeki etiketlere yazdır
            LblAyakkabiKar.Text = ayakkabiKar.ToString("C2"); // Ayakkabı net kârını yaz
            LblPantolonKar.Text = pantolonKar.ToString("C2"); // Pantolon net kârını yaz
            LblTulumKar.Text = tulumKar.ToString("C2");       // Tulum net kârını yaz
            LblBaretKar.Text = baretKar.ToString("C2");       // Baret net kârını yaz
            LblYelekKar.Text = yelekKar.ToString("C2");       // Yelek net kârını yaz
            LblEldivenKarr.Text = eldivenKar.ToString("C2");   // Eldiven net kârını yaz

            decimal toplamKar = ayakkabiKar + pantolonKar + tulumKar + baretKar + yelekKar + eldivenKar;

            LblNetKar.Text = toplamKar.ToString("C2");


        }

        private void btnAylik_Click(object sender, EventArgs e)
        {
            // Bugünün tarihini al
            DateTime bugun = DateTime.Today;

            // SQL sorgusu: Tarih sütununu ay ve yıla göre filtrele
            string sorgu = @"
            SELECT SatisID, ÜrünID, ÜrünTürü, ÜrünTipi, ÜrünSeri, ÜrünRenk, ÜrünBeden, 
            AlisFiyati, SatisFiyati, SatilanAdet, Tarih
            FROM Tbl_Satis
            WHERE MONTH(Tarih) = @Ay AND YEAR(Tarih) = @Yil";

            SqlCommand command = new SqlCommand(sorgu, bgl.baglanti());

            // Parametreleri ekle (geçerli ay ve yıl)
            command.Parameters.AddWithValue("@Ay", bugun.Month);
            command.Parameters.AddWithValue("@Yil", bugun.Year);

            // Veritabanını aç ve verileri al
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // DataGridView'i doldur
            dataGridView1.DataSource = dt;
        }

        private void btnYıllık_Click(object sender, EventArgs e)
        {
            // Bugünün tarihini al
            DateTime bugun = DateTime.Today;

            // SQL sorgusu: Tarih sütununu sadece yıla göre filtrele
            string sorgu = @"
            SELECT SatisID, ÜrünID, ÜrünTürü, ÜrünTipi, ÜrünSeri, ÜrünRenk, ÜrünBeden, 
            AlisFiyati, SatisFiyati, SatilanAdet, Tarih
            FROM Tbl_Satis
            WHERE YEAR(Tarih) = @Yil";

            SqlCommand command = new SqlCommand(sorgu, bgl.baglanti());

            // Parametreyi ekle (geçerli yıl)
            command.Parameters.AddWithValue("@Yil", bugun.Year);

            // Veritabanını aç ve verileri al
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // DataGridView'i doldur
            dataGridView1.DataSource = dt;
        }

        private void kdvCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (kdvCheck.Checked)
            {
                txtKdv.ReadOnly = false;
            }
            else
            {
                txtKdv.ReadOnly = true;
            }
        }
    }
}
