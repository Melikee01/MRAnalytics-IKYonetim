using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace IKYonetim.ENTITY
{
    public class Izin
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }

        public DateTime BaslangicTarihi { get; set; }   // DB: baslangic (datetime)
        public DateTime BitisTarihi { get; set; }       // DB: bitis (datetime)

        public string IzinTuru { get; set; }            // DB: izin_turu (varchar(50))
        public string Aciklama { get; set; }            // DB: aciklama (text, NULL olabilir)
        public string Durum { get; set; }               // DB: durum (varchar(20), default Beklemede)
    }
}
