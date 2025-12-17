using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKYonetim.ENTITY
{
    namespace IKYonetim.ENTITY
    {
        public class Personel
        {
            public int Id { get; set; }
            public int UserId { get; set; }   // users tablosu ile ilişki
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string Departman { get; set; }
            public string Pozisyon { get; set; }
            public bool Aktif { get; set; }
        }
    }

    
}
