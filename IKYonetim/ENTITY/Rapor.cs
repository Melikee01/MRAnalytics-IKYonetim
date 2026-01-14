using System;

namespace IKYonetim.ENTITY
{
    public enum RaporTipi
    {
        DepartmanBazliPersonelDagilimi = 1,
        IzinRaporu = 2,
        MaasRaporu = 3,
        PerformansRaporu = 4,
        IzinHakedisKontrolu = 5
    }

    public class RaporFiltre
    {
        // İzin raporu için
        public DateTime? Baslangic { get; set; }
        public DateTime? Bitis { get; set; }

        // Maaş / Hakediş için
        public int? Yil { get; set; }
        public int? Ay { get; set; }

        // Performans raporu için
        public int TopN { get; set; } = 5;
    }
}

