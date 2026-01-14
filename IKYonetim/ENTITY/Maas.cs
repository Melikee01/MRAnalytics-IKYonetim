using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IKYonetim.ENTITY
{
    public class Maas
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }

        public int Yil { get; set; }
        public int Ay { get; set; }

        public decimal BrutMaas { get; set; }
        public decimal Prim { get; set; }
        public decimal Mesai { get; set; }
        public decimal KesintiToplam { get; set; }
        public decimal NetMaas { get; set; }

        public DateTime HesaplamaTarihi { get; set; }
        public int? HesaplayanUserId { get; set; }
        public string Aciklama { get; set; }
    }
}


