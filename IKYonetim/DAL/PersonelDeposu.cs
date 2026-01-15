using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace IKYonetim.DAL
{
    public class PersonelDeposu
    {
        private readonly baglantiGetir _baglanti = new baglantiGetir();

        public List<Personel> TumPersoneller()
        {
            List<Personel> liste = new List<Personel>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                 SELECT id, ad, soyad, departman, pozisyon, aktif, yillik_izin_hakki
                 FROM personel";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        liste.Add(new Personel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Ad = Convert.ToString(dr["ad"]),
                            Soyad = Convert.ToString(dr["soyad"]),
                            Departman = Convert.ToString(dr["departman"]),
                            Pozisyon = Convert.ToString(dr["pozisyon"]),
                            Aktif = Convert.ToInt32(dr["aktif"]) == 1,
                            YillikIzinHakki = Convert.ToInt32(dr["yillik_izin_hakki"])
                        });
                    }
                }
            }
            return liste;
        }
        public Personel PersonelGetir(int personelId)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                SELECT id, ad, soyad, departman, pozisyon, aktif, yillik_izin_hakki
                FROM personel
                WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", personelId);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Personel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Ad = Convert.ToString(dr["ad"]),
                                Soyad = Convert.ToString(dr["soyad"]),
                                Departman = Convert.ToString(dr["departman"]),
                                Pozisyon = Convert.ToString(dr["pozisyon"]),
                                Aktif = Convert.ToInt32(dr["aktif"]) == 1,
                                YillikIzinHakki = Convert.ToInt32(dr["yillik_izin_hakki"])
                            };
                        }
                    }
                }
            }

            return null;
        }
        public void PersonelEkle(Personel p)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
             INSERT INTO personel (ad, soyad, departman, pozisyon, aktif, yillik_izin_hakki)
             VALUES (@ad, @soyad, @dep, @poz, @aktif, @izin);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", p.Ad);
                    cmd.Parameters.AddWithValue("@soyad", p.Soyad);
                    cmd.Parameters.AddWithValue("@dep", p.Departman);
                    cmd.Parameters.AddWithValue("@poz", p.Pozisyon);
                    cmd.Parameters.AddWithValue("@aktif", p.Aktif ? 1 : 0);
                    cmd.Parameters.AddWithValue("@izin", p.YillikIzinHakki);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void PersonelGuncelle(Personel p)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                 UPDATE personel
                 SET ad=@ad, soyad=@soyad, departman=@dep, pozisyon=@poz, aktif=@aktif, yillik_izin_hakki=@izin
                 WHERE id=@id;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", p.Ad);
                    cmd.Parameters.AddWithValue("@soyad", p.Soyad);
                    cmd.Parameters.AddWithValue("@dep", p.Departman);
                    cmd.Parameters.AddWithValue("@poz", p.Pozisyon);
                    cmd.Parameters.AddWithValue("@aktif", p.Aktif ? 1 : 0);
                    cmd.Parameters.AddWithValue("@izin", p.YillikIzinHakki);
                    cmd.Parameters.AddWithValue("@id", p.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void PersonelSil(int id)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "DELETE FROM personel WHERE id=@id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string PersonelinRolunuGetir(int personelId)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                SELECT role
                FROM users
                WHERE personel_id = @pid AND aktif = 1
                LIMIT 1;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", personelId);

                    object result = cmd.ExecuteScalar();
                    return (result == null || result == DBNull.Value) ? null : Convert.ToString(result);
                }
            }
        }
        public int PersonelEkleVeIdDondur(Personel p, MySqlConnection conn, MySqlTransaction tx)
        {
            const string sql = @"
            INSERT INTO personel (ad, soyad, departman, pozisyon, aktif, yillik_izin_hakki)
            VALUES (@ad, @soyad, @dep, @poz, @aktif, @izin);";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@ad", p.Ad);
                cmd.Parameters.AddWithValue("@soyad", p.Soyad);
                cmd.Parameters.AddWithValue("@dep", p.Departman);
                cmd.Parameters.AddWithValue("@poz", p.Pozisyon);
                cmd.Parameters.AddWithValue("@aktif", p.Aktif ? 1 : 0);
                cmd.Parameters.AddWithValue("@izin", p.YillikIzinHakki);

                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }
        public void PersonelAktiflikGuncelle(int personelId, bool aktif)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "UPDATE personel SET aktif=@aktif WHERE id=@id";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@aktif", aktif ? 1 : 0);
                    cmd.Parameters.AddWithValue("@id", personelId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Personel> AktifPersonelleriGetir()
        {
            var liste = new List<Personel>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                SELECT id, ad, soyad, departman
                FROM personel
                WHERE (aktif IS NULL OR aktif = 1)
                ORDER BY ad ASC, soyad ASC;";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var p = new Personel();
                        p.Id = Convert.ToInt32(dr["id"]);
                        p.Ad = Convert.ToString(dr["ad"]);
                        p.Soyad = Convert.ToString(dr["soyad"]);
                        p.Departman = Convert.ToString(dr["departman"]);
                        liste.Add(p);
                    }
                }
            }

            return liste;
        }
        public void PersonelAktiflikVeIzinGuncelle(int personelId, bool aktif)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                UPDATE personel
                SET 
                aktif = @aktif,
                yillik_izin_hakki = CASE 
                WHEN @aktif = 1 THEN 14
                ELSE 0
                END
                WHERE id = @id;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@aktif", aktif ? 1 : 0);
                    cmd.Parameters.AddWithValue("@id", personelId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
