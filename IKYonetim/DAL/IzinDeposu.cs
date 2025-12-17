using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace IKYonetim.DAL
{
    public class IzinDAL
    {
        private baglantiGetir _baglanti = new baglantiGetir();

        // 1️⃣ İZİN EKLE
        public void IzinEkle(Izin izin)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                INSERT INTO leave_requests
                (personel_id, baslangic, bitis, izin_turu, aciklama, durum)
                VALUES
                (@pid, @bas, @bit, @tur, @acik, 'Beklemede')";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", izin.PersonelId);
                    cmd.Parameters.AddWithValue("@bas", izin.BaslangicTarihi);
                    cmd.Parameters.AddWithValue("@bit", izin.BitisTarihi);
                    cmd.Parameters.AddWithValue("@tur", izin.IzinTuru);
                    cmd.Parameters.AddWithValue("@acik", izin.Aciklama);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 2️⃣ PERSONELE AİT TÜM İZİNLER
        public List<Izin> PersonelIzinleriniGetir(int personelId)
        {
            List<Izin> izinler = new List<Izin>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                SELECT *
                FROM leave_requests
                WHERE personel_id = @pid
                ORDER BY baslangic DESC";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", personelId);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            izinler.Add(new Izin
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                PersonelId = Convert.ToInt32(dr["personel_id"]),
                                BaslangicTarihi = Convert.ToDateTime(dr["baslangic"]),
                                BitisTarihi = Convert.ToDateTime(dr["bitis"]),
                                IzinTuru = dr["izin_turu"].ToString(),
                                Aciklama = dr["aciklama"].ToString(),
                                Durum = dr["durum"].ToString()
                            });
                        }
                    }
                }
            }

            return izinler;
        }
    }
}
