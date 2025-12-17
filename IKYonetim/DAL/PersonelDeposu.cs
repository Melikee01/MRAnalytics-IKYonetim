using IKYonetim.ENTITY;
using IKYonetim.ENTITY.IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace IKYonetim.DAL
{
    public class PersonelDeposu
    {
        private baglantiGetir _baglanti = new baglantiGetir();

        // 1️⃣ TÜM PERSONELLER
        public List<Personel> TumPersoneller()
        {
            List<Personel> liste = new List<Personel>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                SELECT id, ad, soyad, departman, pozisyon, aktif
                FROM personel";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        liste.Add(new Personel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Ad = dr["ad"].ToString(),
                            Soyad = dr["soyad"].ToString(),
                            Departman = dr["departman"].ToString(),
                            Pozisyon = dr["pozisyon"].ToString(),
                            Aktif = Convert.ToBoolean(dr["aktif"])
                        });
                    }
                }
            }

            return liste;
        }

        // 2️⃣ ID’YE GÖRE PERSONEL
        public Personel PersonelGetir(int personelId)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                SELECT id, ad, soyad, departman, pozisyon, aktif
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
                                Ad = dr["ad"].ToString(),
                                Soyad = dr["soyad"].ToString(),
                                Departman = dr["departman"].ToString(),
                                Pozisyon = dr["pozisyon"].ToString(),
                                Aktif = Convert.ToBoolean(dr["aktif"])
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}

