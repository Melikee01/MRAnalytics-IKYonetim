using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace IKYonetim.DAL
{
    public class DepartmanDeposu
    {
        private baglantiGetir _baglanti = new baglantiGetir();

        // 1️⃣ TÜM AKTİF DEPARTMANLAR
        public List<Departman> DepartmanlariGetir()
        {
            List<Departman> liste = new List<Departman>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
                SELECT id, departman_adi, aktif
                FROM departman
                WHERE aktif = 1";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        liste.Add(new Departman
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            DepartmanAdi = dr["departman_adi"].ToString(),
                            Aktif = Convert.ToBoolean(dr["aktif"])
                        });
                    }
                }
            }

            return liste;
        }
    }
}

