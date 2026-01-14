using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKYonetim.ENTITY
{


    public class Personel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Departman { get; set; }
        public string Pozisyon { get; set; }
        public bool Aktif { get; set; }
        public int YillikIzinHakki { get; set; }
    }
    

   
}
