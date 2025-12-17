using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace IKYonetim.DAL
{
    public class baglantiGetir
    {
        public MySqlConnection BaglantiGetir()
        {
            MySqlConnection baglanti = new MySqlConnection("Server=172.21.54.253;Port=3306;Database=26_132430031;Uid=26_132430031;Pwd=İnif123.;");
            baglanti.Open();
            return baglanti;
        }
       
    }
}
