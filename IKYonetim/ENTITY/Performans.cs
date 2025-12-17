using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IKYonetim.ENTITY
{
    public class Performans
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }

        public int Puan { get; set; }          // 1–100
        public string Aciklama { get; set; }

        public DateTime DegerlendirmeTarihi { get; set; }
        public int DegerlendirenId { get; set; } // IK / Admin
    }
}
