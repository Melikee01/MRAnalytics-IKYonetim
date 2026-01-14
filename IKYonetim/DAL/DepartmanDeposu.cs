using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IKYonetim.DAL
{
    public class DepartmanDeposu
    {
        private baglantiGetir _baglanti = new baglantiGetir();

        // 1️⃣ TÜM AKTİF DEPARTMANLAR
        public List<Departman> DepartmanlariGetir()
        {
            var liste = new List<Departman>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "SELECT id, departman_adi, aktif FROM departman";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        liste.Add(new Departman
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            DepartmanAdi = Convert.ToString(dr["departman_adi"]),
                            Aktif = Convert.ToInt32(dr["aktif"]) == 1
                        });
                    }
                }
            }
            return liste;
        }
        // SADECE AKTİF DEPARTMANLAR
        public List<Departman> AktifDepartmanlariGetir()
        {
            List<Departman> departmanlar = new List<Departman>();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "SELECT id, departman_adi, aktif FROM departman WHERE aktif = 1";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        departmanlar.Add(new Departman
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            DepartmanAdi = dr["departman_adi"].ToString(),
                            Aktif = Convert.ToInt32(dr["aktif"]) == 1
                        });
                    }
                }
            }

            return departmanlar;
        }

        public void DepartmanEkle(Departman d)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "INSERT INTO departman (departman_adi, aktif) VALUES (@ad, 1)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", d.DepartmanAdi);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DepartmanGuncelle(Departman d)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "UPDATE departman SET departman_adi=@ad WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ad", d.DepartmanAdi);
                    cmd.Parameters.AddWithValue("@id", d.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DepartmanPasifeAl(int id)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "UPDATE departman SET aktif=0 WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DepartmanAktifeAl(int id)
        {
            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            {
                string sql = "UPDATE departman SET aktif = 1 WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}



