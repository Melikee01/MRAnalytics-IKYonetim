using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace IKYonetim.DAL
{
    public class IzinDeposu
    {
        private readonly baglantiGetir _baglanti = new baglantiGetir();

        // ---- SYNC (istersen kalsın) ----
        public void IzinEkle(Izin izin) => IzinEkleAsync(izin).GetAwaiter().GetResult();
        public List<Izin> PersonelinIzinleri(int personelId) => PersonelinIzinleriAsync(personelId).GetAwaiter().GetResult();
        public List<Izin> TumIzinler() => TumIzinlerAsync().GetAwaiter().GetResult();
        public void DurumGuncelle(int izinId, string yeniDurum) => DurumGuncelleAsync(izinId, yeniDurum).GetAwaiter().GetResult();
        public void IzinSil(int izinId) => IzinSilAsync(izinId).GetAwaiter().GetResult();

        // ---- ASYNC (UI bununla donmaz) ----
        public async Task IzinEkleAsync(Izin izin)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir(false))
            {
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                const string sql = @"
INSERT INTO leave_requests
(personel_id, baslangic, bitis, izin_turu, aciklama, durum)
VALUES
(@pid, @bas, @bit, @tur, @acik, @durum);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 30;

                    cmd.Parameters.AddWithValue("@pid", izin.PersonelId);
                    cmd.Parameters.AddWithValue("@bas", izin.BaslangicTarihi);
                    cmd.Parameters.AddWithValue("@bit", izin.BitisTarihi);
                    cmd.Parameters.AddWithValue("@tur", izin.IzinTuru);
                    cmd.Parameters.AddWithValue("@acik", (object)izin.Aciklama ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@durum", string.IsNullOrWhiteSpace(izin.Durum) ? "Beklemede" : izin.Durum);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Izin>> PersonelinIzinleriAsync(int personelId)
        {
            var liste = new List<Izin>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir(false))
            {
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                const string sql = @"
SELECT id, personel_id, baslangic, bitis, izin_turu, aciklama, durum
FROM leave_requests
WHERE personel_id = @pid
ORDER BY id DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 30;
                    cmd.Parameters.AddWithValue("@pid", personelId);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            liste.Add(new Izin
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                PersonelId = Convert.ToInt32(dr["personel_id"]),
                                BaslangicTarihi = Convert.ToDateTime(dr["baslangic"]),
                                BitisTarihi = Convert.ToDateTime(dr["bitis"]),
                                IzinTuru = Convert.ToString(dr["izin_turu"]) ?? "",
                                Aciklama = dr["aciklama"] == DBNull.Value ? null : Convert.ToString(dr["aciklama"]),
                                Durum = dr["durum"] == DBNull.Value ? "Beklemede" : (Convert.ToString(dr["durum"]) ?? "Beklemede")
                            });
                        }
                    }
                }
            }

            return liste;
        }

        public async Task<List<Izin>> TumIzinlerAsync()
        {
            var liste = new List<Izin>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir(false))
            {
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                const string sql = @"
SELECT id, personel_id, baslangic, bitis, izin_turu, aciklama, durum
FROM leave_requests
ORDER BY id DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 30;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            liste.Add(new Izin
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                PersonelId = Convert.ToInt32(dr["personel_id"]),
                                BaslangicTarihi = Convert.ToDateTime(dr["baslangic"]),
                                BitisTarihi = Convert.ToDateTime(dr["bitis"]),
                                IzinTuru = Convert.ToString(dr["izin_turu"]) ?? "",
                                Aciklama = dr["aciklama"] == DBNull.Value ? null : Convert.ToString(dr["aciklama"]),
                                Durum = dr["durum"] == DBNull.Value ? "Beklemede" : (Convert.ToString(dr["durum"]) ?? "Beklemede")
                            });
                        }
                    }
                }
            }

            return liste;
        }

        public async Task DurumGuncelleAsync(int izinId, string yeniDurum)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir(false))
            {
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                const string sql = @"UPDATE leave_requests SET durum=@d WHERE id=@id;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 30;
                    cmd.Parameters.AddWithValue("@d", yeniDurum);
                    cmd.Parameters.AddWithValue("@id", izinId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task IzinSilAsync(int izinId)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir(false))
            {
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                const string sql = @"DELETE FROM leave_requests WHERE id=@id;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 30;
                    cmd.Parameters.AddWithValue("@id", izinId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
