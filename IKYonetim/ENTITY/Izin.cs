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

        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        public string IzinTuru { get; set; }
        public string Aciklama { get; set; }

        public string Durum { get; set; } // Beklemede / Onaylandı / Reddedildi
    }
}
