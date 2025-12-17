using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKYonetim.BLL
{
    public static class OturumYoneticisi
    {
        public static int KullaniciId { get; private set; }
        public static int PersonelId { get; private set; }
        public static string Rol { get; private set; }

        public static void OturumBaslat(int kullaniciId, int personelId, string rol)
        {
            KullaniciId = kullaniciId;
            PersonelId = personelId;
            Rol = rol;
        }

        public static void OturumuKapat()
        {
            KullaniciId = 0;
            PersonelId = 0;
            Rol = null;
        }
    }
}

