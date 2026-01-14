using IKYonetim.DAL;
using System;
using System.Data;

namespace IKYonetim.BLL
{
    public class RaporlamaYoneticisi
    {
        private readonly RaporDeposu _depo = new RaporDeposu();

        public DataTable DepartmanBazliPersonelDagilimi()
        {
            return _depo.DepartmanBazliPersonelDagilimiGetir();
        }

        public DataTable IzinRaporu(DateTime baslangic, DateTime bitis)
        {
            if (bitis.Date < baslangic.Date)
                throw new ArgumentException("Bitiş tarihi, başlangıç tarihinden küçük olamaz.");

            return _depo.IzinRaporuGetir(baslangic, bitis);
        }

        public DataTable MaasRaporu(int yil, int ay)
        {
            if (yil < 2000 || yil > 2100) throw new ArgumentException("Yıl aralığı hatalı.");
            if (ay < 1 || ay > 12) throw new ArgumentException("Ay 1-12 olmalı.");

            return _depo.MaasRaporuGetir(yil, ay);
        }

        public DataTable PerformansRaporu(int topN)
        {
            if (topN < 1) topN = 1;
            if (topN > 100) topN = 100;

            return _depo.PerformansRaporuGetir(topN);
        }

        public DataTable IzinHakedisKontrolu(int yil)
        {
            if (yil < 2000 || yil > 2100) throw new ArgumentException("Yıl aralığı hatalı.");
            return _depo.IzinHakedisKontroluGetir(yil);
        }
    }
}
