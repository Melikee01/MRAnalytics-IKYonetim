using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace IKYonetim.DAL
{
    public class IzinDeposu
    {
        private readonly baglantiGetir _baglanti = new baglantiGetir();

        // 1) İZİN EKLE
        public void IzinEkle(Izin izin)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
INSERT INTO leave_requests
(personel_id, baslangic, bitis, izin_turu, aciklama, durum)
VALUES
(@pid, @bas, @bit, @tur, @acik, @durum);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", izin.PersonelId);
                    cmd.Parameters.AddWithValue("@bas", izin.BaslangicTarihi);
                    cmd.Parameters.AddWithValue("@bit", izin.BitisTarihi);
                    cmd.Parameters.AddWithValue("@tur", izin.IzinTuru);
                    cmd.Parameters.AddWithValue("@acik", (object)izin.Aciklama ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@durum", string.IsNullOrWhiteSpace(izin.Durum) ? "Beklemede" : izin.Durum);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 2) PERSONELE AİT İZİNLER
        public List<Izin> PersonelinIzinleri(int personelId)
        {
            var liste = new List<Izin>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
SELECT id, personel_id, baslangic, bitis, izin_turu, aciklama, durum
FROM leave_requests
WHERE personel_id = @pid
ORDER BY id DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", personelId);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            liste.Add(new Izin
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                PersonelId = Convert.ToInt32(dr["personel_id"]),
                                BaslangicTarihi = Convert.ToDateTime(dr["baslangic"]),
                                BitisTarihi = Convert.ToDateTime(dr["bitis"]),
                                IzinTuru = dr["izin_turu"].ToString(),
                                Aciklama = dr["aciklama"] == DBNull.Value ? null : dr["aciklama"].ToString(),
                                Durum = dr["durum"] == DBNull.Value ? "Beklemede" : dr["durum"].ToString()
                            });
                        }
                    }
                }
            }

            return liste;
        }

        // 3) TÜM İZİNLER (IK / Admin ekranı için)
        public List<Izin> TumIzinler()
        {
            var liste = new List<Izin>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
SELECT id, personel_id, baslangic, bitis, izin_turu, aciklama, durum
FROM leave_requests
ORDER BY id DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        liste.Add(new Izin
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            PersonelId = Convert.ToInt32(dr["personel_id"]),
                            BaslangicTarihi = Convert.ToDateTime(dr["baslangic"]),
                            BitisTarihi = Convert.ToDateTime(dr["bitis"]),
                            IzinTuru = dr["izin_turu"].ToString(),
                            Aciklama = dr["aciklama"] == DBNull.Value ? null : dr["aciklama"].ToString(),
                            Durum = dr["durum"] == DBNull.Value ? "Beklemede" : dr["durum"].ToString()
                        });
                    }
                }
            }

            return liste;
        }

        // 4) DURUM GÜNCELLE (Onayla / Reddet)
        public void DurumGuncelle(int izinId, string yeniDurum)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"UPDATE leave_requests SET durum=@d WHERE id=@id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@d", yeniDurum);
                    cmd.Parameters.AddWithValue("@id", izinId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 5) İZİN SİL (İstersen)
        public void IzinSil(int izinId)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"DELETE FROM leave_requests WHERE id=@id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", izinId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


