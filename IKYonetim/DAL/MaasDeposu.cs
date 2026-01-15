using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;



namespace IKYonetim.DAL
{
    public class MaasDeposu
    {
        private baglantiGetir _baglanti = new baglantiGetir();

        public void Upsert(Maas maas)
        {

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
INSERT INTO maas
(personel_id, yil, ay, brut_maas, prim, mesai, kesinti_toplam, net_maas, hesaplama_tarihi, hesaplayan_user_id, aciklama)
VALUES
(@pid, @yil, @ay, @brut, @prim, @mesai, @kesinti, @net, @tarih, @hid, @aciklama)
ON DUPLICATE KEY UPDATE
brut_maas = VALUES(brut_maas),
prim = VALUES(prim),
mesai = VALUES(mesai),
kesinti_toplam = VALUES(kesinti_toplam),
net_maas = VALUES(net_maas),
hesaplama_tarihi = VALUES(hesaplama_tarihi),
hesaplayan_user_id = VALUES(hesaplayan_user_id),
aciklama = VALUES(aciklama);";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", maas.PersonelId);
                    cmd.Parameters.AddWithValue("@yil", maas.Yil);
                    cmd.Parameters.AddWithValue("@ay", maas.Ay);

                    cmd.Parameters.Add("@brut", MySqlDbType.Decimal).Value = maas.BrutMaas;
                    cmd.Parameters.Add("@prim", MySqlDbType.Decimal).Value = maas.Prim;
                    cmd.Parameters.Add("@mesai", MySqlDbType.Decimal).Value = maas.Mesai;
                    cmd.Parameters.Add("@kesinti", MySqlDbType.Decimal).Value = maas.KesintiToplam;
                    cmd.Parameters.Add("@net", MySqlDbType.Decimal).Value = maas.NetMaas;

                    cmd.Parameters.Add("@tarih", MySqlDbType.DateTime).Value = maas.HesaplamaTarihi;
                    cmd.Parameters.AddWithValue("@hid", (object)maas.HesaplayanUserId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@aciklama", (object)maas.Aciklama ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Maas> PersonelinMaaslari(int personelId)
        {
            var liste = new List<Maas>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
             SELECT id, personel_id, yil, ay,
             brut_maas, prim, mesai, kesinti_toplam, net_maas,
             hesaplama_tarihi, hesaplayan_user_id, aciklama
             FROM maas
             WHERE personel_id=@pid
             ORDER BY yil DESC, ay DESC, id DESC;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", personelId);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                            liste.Add(Map(dr));
                    }
                }
            }

            return liste;
        }

        public List<Maas> TumMaaslar()
        {
            var liste = new List<Maas>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = @"
            SELECT id, personel_id, yil, ay,
            brut_maas, prim, mesai, kesinti_toplam, net_maas,
            hesaplama_tarihi, hesaplayan_user_id, aciklama
            FROM maas
            ORDER BY yil DESC, ay DESC, id DESC;";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        liste.Add(Map(dr));
                }
            }

            return liste;
        }

        private Maas Map(MySqlDataReader dr)
        {
            return new Maas
            {
                Id = Convert.ToInt32(dr["id"]),
                PersonelId = Convert.ToInt32(dr["personel_id"]),
                Yil = Convert.ToInt16(dr["yil"]),
                Ay = Convert.ToByte(dr["ay"]),

                BrutMaas = Convert.ToDecimal(dr["brut_maas"]),
                Prim = Convert.ToDecimal(dr["prim"]),
                Mesai = Convert.ToDecimal(dr["mesai"]),
                KesintiToplam = Convert.ToDecimal(dr["kesinti_toplam"]),
                NetMaas = Convert.ToDecimal(dr["net_maas"]),

                HesaplamaTarihi = Convert.ToDateTime(dr["hesaplama_tarihi"]),
                HesaplayanUserId = dr["hesaplayan_user_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["hesaplayan_user_id"]),
                Aciklama = dr["aciklama"] == DBNull.Value ? "" : Convert.ToString(dr["aciklama"])

            };
        }
        public void Sil(int maasId)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "DELETE FROM maas WHERE id=@id;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", maasId);
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}

