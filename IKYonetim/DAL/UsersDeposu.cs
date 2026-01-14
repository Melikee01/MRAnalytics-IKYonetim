using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;

namespace IKYonetim.DAL
{
    public class UsersDeposu
    {
        public Users GirisIcinUsersGetir(string email, string parola)
        {
            using (var conn = new baglantiGetir().BaglantiGetir())
            {
                const string sql = @"
SELECT id, personel_id, email, parola, role, aktif
FROM users
WHERE email = @email AND parola = @parola AND aktif = 1
LIMIT 1;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@parola", parola);

                    using (var dr = cmd.ExecuteReader())
                    {
                        int ordPid = dr.GetOrdinal("personel_id");
                        if (!dr.Read()) return null;

                        return new Users
                        {
                            Id = dr.GetInt32("id"),
                            PersonelId = dr.IsDBNull(ordPid) ? (int?)null : dr.GetInt32(ordPid),
                            email = dr.GetString("email"),
                            Parola = dr.GetString("parola"),
                            Rol = dr.GetString("role"),
                            Aktif = dr.GetInt32("aktif") == 1
                        };
                    }
                }
            }
        }
        public bool EmailVarMi(string email, MySqlConnection conn, MySqlTransaction tx)
        {
            const string sql = @"SELECT COUNT(1) FROM users WHERE email = @email;";
            using (var cmd = new MySqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@email", email);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public void UsersEkle(Users u, MySqlConnection conn, MySqlTransaction tx)
        {
            const string sql = @"
INSERT INTO users (personel_id, email, parola, role, aktif)
VALUES (@pid, @email, @parola, @rol, @aktif);";

            using (var cmd = new MySqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@pid", u.PersonelId);   // int? kabul eder
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@parola", u.Parola);
                cmd.Parameters.AddWithValue("@rol", u.Rol);
                cmd.Parameters.AddWithValue("@aktif", u.Aktif ? 1 : 0);
                cmd.ExecuteNonQuery();
            }
        }
        public string PersonelinRolunuGetir(int personelId)
        {
            baglantiGetir b = new baglantiGetir();

            using (MySqlConnection conn = b.BaglantiGetir())
            {
                const string sql = @"
SELECT rol
FROM users
WHERE personel_id = @pid AND aktif = 1
LIMIT 1;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", personelId);

                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return null;

                    return result.ToString();
                }
            }
        }



    }
}