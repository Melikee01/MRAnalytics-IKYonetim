using IKYonetim.DAL;
using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace IKYonetim.DAL
{
    public class PerformansDeposu
    {
        private baglantiGetir _baglanti = new baglantiGetir();

        // 1) TÜM PERFORMANSLARI GETİR
        public List<Performans> TumPerformanslariGetir()
        {
            var liste = new List<Performans>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"SELECT id, personel_id, puan, aciklama, degerlendirme_tarihi, degerlendiren_id
               FROM performans
               WHERE aktif = 1
               ORDER BY degerlendirme_tarihi DESC, id DESC
               ";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        liste.Add(MapPerformans(dr));
                    }
                }
            }

            return liste;
        }

        // 2) PERSONELE GÖRE PERFORMANSLARI GETİR
        public List<Performans> PersonelinPerformanslariniGetir(int personelId)
        {
            var liste = new List<Performans>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"SELECT id, personel_id, puan, aciklama, degerlendirme_tarihi, degerlendiren_id
                               FROM performans
                               WHERE personel_id = @pid
                               ORDER BY degerlendirme_tarihi DESC, id DESC";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", personelId);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            liste.Add(MapPerformans(dr));
                        }
                    }
                }
            }

            return liste;
        }

        // 3) PERFORMANS EKLE
        public void PerformansEkle(Performans p)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"INSERT INTO performans (personel_id, puan, aciklama, degerlendirme_tarihi, degerlendiren_id)
                               VALUES (@pid, @puan, @aciklama, @tarih, @did)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", p.PersonelId);
                    cmd.Parameters.AddWithValue("@puan", p.Puan);
                    cmd.Parameters.AddWithValue("@aciklama", (object)p.Aciklama ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tarih", p.DegerlendirmeTarihi);
                    cmd.Parameters.AddWithValue("@did", p.DegerlendirenId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4) PERFORMANS GÜNCELLE
        public void PerformansGuncelle(Performans p)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"UPDATE performans
                               SET personel_id=@pid,
                                   puan=@puan,
                                   aciklama=@aciklama,
                                   degerlendirme_tarihi=@tarih,
                                   degerlendiren_id=@did
                               WHERE id=@id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", p.Id);
                    cmd.Parameters.AddWithValue("@pid", p.PersonelId);
                    cmd.Parameters.AddWithValue("@puan", p.Puan);
                    cmd.Parameters.AddWithValue("@aciklama", (object)p.Aciklama ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tarih", p.DegerlendirmeTarihi);
                    cmd.Parameters.AddWithValue("@did", p.DegerlendirenId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 5) (İSTEĞE BAĞLI) PERFORMANS SİL
        public void PerformansSil(int id)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "DELETE FROM performans WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 6) FİLTRELİ LİSTE (Rapor ekranında çok işine yarar)
        // Hepsi nullable: göndermediğin filtre uygulanmaz.
        public List<Performans> PerformansFiltrele(int? personelId, DateTime? baslangic, DateTime? bitis, int? minPuan, int? maxPuan)
        {
            var liste = new List<Performans>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                var sb = new StringBuilder();
                sb.Append(@"SELECT id, personel_id, puan, aciklama, degerlendirme_tarihi, degerlendiren_id
                            FROM performans
                            WHERE 1=1 ");

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (personelId.HasValue && personelId.Value > 0)
                    {
                        sb.Append(" AND personel_id = @pid ");
                        cmd.Parameters.AddWithValue("@pid", personelId.Value);
                    }

                    if (baslangic.HasValue)
                    {
                        sb.Append(" AND degerlendirme_tarihi >= @bas ");
                        cmd.Parameters.AddWithValue("@bas", baslangic.Value);
                    }

                    if (bitis.HasValue)
                    {
                        sb.Append(" AND degerlendirme_tarihi <= @bit ");
                        cmd.Parameters.AddWithValue("@bit", bitis.Value);
                    }

                    if (minPuan.HasValue)
                    {
                        sb.Append(" AND puan >= @minP ");
                        cmd.Parameters.AddWithValue("@minP", minPuan.Value);
                    }

                    if (maxPuan.HasValue)
                    {
                        sb.Append(" AND puan <= @maxP ");
                        cmd.Parameters.AddWithValue("@maxP", maxPuan.Value);
                    }

                    sb.Append(" ORDER BY degerlendirme_tarihi DESC, id DESC ");

                    cmd.CommandText = sb.ToString();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            liste.Add(MapPerformans(dr));
                        }
                    }
                }
            }

            return liste;
        }

        // --- Helper: DataReader -> Entity
        private Performans MapPerformans(MySqlDataReader dr)
        {
            return new Performans
            {
                Id = Convert.ToInt32(dr["id"]),
                PersonelId = Convert.ToInt32(dr["personel_id"]),
                Puan = Convert.ToInt32(dr["puan"]),
                Aciklama = dr["aciklama"] == DBNull.Value ? "" : dr["aciklama"].ToString(),
                DegerlendirmeTarihi = Convert.ToDateTime(dr["degerlendirme_tarihi"]),
                DegerlendirenId = dr["degerlendiren_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["degerlendiren_id"])
            };
        }
        // 0) ID'YE GÖRE TEK PERFORMANS GETİR (Yetki kontrolü için lazım)
        public Performans PerformansGetirById(int id)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"SELECT id, personel_id, puan, aciklama, degerlendirme_tarihi, degerlendiren_id
                       FROM performans
                       WHERE id = @id
                       LIMIT 1";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            return MapPerformans(dr);
                    }
                }
            }

            return null;
        }

        public void PerformansPasifeAl(int id)
        {
            using (var conn = _baglanti.BaglantiGetir())
            {
                string sql = "UPDATE performans SET aktif = 0 WHERE id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void PerformansAktifeAl(int id)
        {
            using (var conn = _baglanti.BaglantiGetir())
            {
                string sql = "UPDATE performans SET aktif = 1 WHERE id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}

