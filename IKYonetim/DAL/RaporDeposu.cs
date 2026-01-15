using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace IKYonetim.DAL
{
    public class RaporDeposu
    {
        private readonly baglantiGetir _baglanti = new baglantiGetir();

        public DataTable DepartmanBazliPersonelDagilimiGetir()
        {
            string sql = @"
SELECT 
    TRIM(IFNULL(departman, 'Belirtilmemiş')) AS Departman,
    COUNT(*) AS CalisanSayisi
FROM personel
WHERE 1=1
  AND (aktif IS NULL OR aktif = 1)
GROUP BY TRIM(IFNULL(departman, 'Belirtilmemiş'))
ORDER BY CalisanSayisi DESC, Departman ASC;";

            return FillTable(sql);
        }

        public DataTable IzinRaporuGetir(DateTime baslangic, DateTime bitis)
        {
            
            string sql = @"
SELECT
    lr.id AS IzinId,
    p.id  AS PersonelId,
    p.ad  AS Ad,
    p.soyad AS Soyad,
    TRIM(IFNULL(p.departman, 'Belirtilmemiş')) AS Departman,
    lr.izin_turu AS IzinTuru,
    lr.baslangic AS Baslangic,
    lr.bitis AS Bitis,
    lr.durum AS Durum,
    lr.aciklama AS Aciklama
FROM leave_requests lr
JOIN personel p ON p.id = lr.personel_id
WHERE
    lr.baslangic <= @bitis
    AND lr.bitis >= @baslangic
ORDER BY lr.baslangic ASC, p.ad ASC, p.soyad ASC;";

            return FillTable(sql,
                new MySqlParameter("@baslangic", baslangic.Date),
                new MySqlParameter("@bitis", bitis.Date));
        }

        public DataTable MaasRaporuGetir(int yil, int ay)
        {
            string sql = @"
SELECT
    @yil AS Yil,
    @ay  AS Ay,
    COUNT(*) AS PersonelSayisi,
    ROUND(SUM(net_maas), 2) AS ToplamNetMaas,
    ROUND(AVG(net_maas), 2) AS OrtalamaNetMaas
FROM maas
WHERE yil=@yil AND ay=@ay;";

            return FillTable(sql,
                new MySqlParameter("@yil", yil),
                new MySqlParameter("@ay", ay));
        }

        public DataTable PerformansRaporuGetir(int topN)
        {
            string sql = @"
SELECT * FROM (
    SELECT 
        'En Yüksek' AS Grup,
        p.id AS PersonelId,
        p.ad AS Ad,
        p.soyad AS Soyad,
        pr.puan AS Puan,
        pr.degerlendirme_tarihi AS DegerlendirmeTarihi
    FROM (
        SELECT pr1.*
        FROM performans pr1
        JOIN (
            SELECT personel_id, MAX(degerlendirme_tarihi) AS max_tarih
            FROM performans
            GROUP BY personel_id
        ) x ON x.personel_id = pr1.personel_id 
           AND x.max_tarih = pr1.degerlendirme_tarihi
    ) pr
    JOIN personel p ON p.id = pr.personel_id
    ORDER BY pr.puan DESC
    LIMIT @n
) t1
UNION ALL
SELECT * FROM (
    SELECT 
        'En Düşük' AS Grup,
        p.id AS PersonelId,
        p.ad AS Ad,
        p.soyad AS Soyad,
        pr.puan AS Puan,
        pr.degerlendirme_tarihi AS DegerlendirmeTarihi
    FROM (
        SELECT pr1.*
        FROM performans pr1
        JOIN (
            SELECT personel_id, MAX(degerlendirme_tarihi) AS max_tarih
            FROM performans
            GROUP BY personel_id
        ) x ON x.personel_id = pr1.personel_id 
           AND x.max_tarih = pr1.degerlendirme_tarihi
    ) pr
    JOIN personel p ON p.id = pr.personel_id
    ORDER BY pr.puan ASC
    LIMIT @n
) t2;
";

            return FillTable(sql, new MySqlParameter("@n", topN));
        }


        public DataTable IzinHakedisKontroluGetir(int yil)
        {
            
            string sql = @"
SELECT
    p.id AS PersonelId,
    p.ad AS Ad,
    p.soyad AS Soyad,
    TRIM(IFNULL(p.departman, 'Belirtilmemiş')) AS Departman,
    p.yillik_izin_hakki AS YillikHak,
    IFNULL(u.kullanilan_gun, 0) AS KullanilanGun,
    (p.yillik_izin_hakki - IFNULL(u.kullanilan_gun, 0)) AS KalanGun
FROM personel p
LEFT JOIN (
    SELECT
        personel_id,
        SUM(DATEDIFF(bitis, baslangic) + 1) AS kullanilan_gun
    FROM leave_requests
    WHERE izin_turu = 'Yıllık İzin'
      AND durum = 'Onaylandı'
      AND YEAR(baslangic) = @yil
    GROUP BY personel_id
) u ON u.personel_id = p.id
WHERE (p.aktif IS NULL OR p.aktif = 1)
  AND (p.yillik_izin_hakki - IFNULL(u.kullanilan_gun, 0)) > 0
ORDER BY KalanGun DESC, p.ad ASC, p.soyad ASC;";

            return FillTable(sql, new MySqlParameter("@yil", yil));
        }

        private DataTable FillTable(string sql, params MySqlParameter[] prms)
        {
            var dt = new DataTable();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                if (prms != null && prms.Length > 0)
                    cmd.Parameters.AddRange(prms);

                using (var adp = new MySqlDataAdapter(cmd))
                {
                    adp.Fill(dt);
                }
            }

            return dt;
        }
    }
}
