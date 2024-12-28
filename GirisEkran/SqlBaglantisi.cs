using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GirisEkran
{
    internal class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=beratxhunter\\SQLEXPRESS;Initial Catalog=Proje;Integrated Security=True;Encrypt=False;");
            baglan.Open();
            return baglan;
        }
    }
}
