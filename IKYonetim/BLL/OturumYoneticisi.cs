using System;
using IKYonetim.DAL;
using IKYonetim.ENTITY;

namespace IKYonetim.BLL
{
    public static class OturumYoneticisi
    {
        // 🔹 Oturum bilgileri (senin eski kodundan)
        public static int Id { get; private set; }
        public static int PersonelId { get; private set; }
        public static string Rol { get; private set; } = "";
        public static string Email { get; private set; } = "";

        // 🔹 İsteğe bağlı: Login olan kullanıcı nesnesini de tut (pratik olur)
        public static Users CurrentUser { get; private set; }

        // DAL
        private static readonly UsersDeposu _UsersDeposu = new UsersDeposu();

        // ✅ Giriş yapma (senin gönderdiğin kodun email'e uyarlanmış hali)
        public static bool GirisYap(string email, string parola, out string hataMesaji)
        {
            hataMesaji = "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
            {
                hataMesaji = "Email ve parola boş olamaz.";
                return false;
            }

            // DAL: GirisIcinKullaniciGetir(email, parola) -> Users döndürüyor
            var user = _UsersDeposu.GirisIcinUsersGetir(email.Trim(), parola);
            if (user == null)
            {
                hataMesaji = "Hatalı email/parola veya kullanıcı pasif.";
                return false;
            }

            // Oturum başlat (senin eski OturumBaslat'ın yaptığı işi burada yapıyoruz)
            CurrentUser = user;
            Id = user.Id;
            PersonelId = user.PersonelId ?? 0; // Admin için null olabilir
            Rol = user.Rol ?? "";
            Email = user.email ?? "";

            return true;
        }

        // ✅ Oturumu kapat
        public static void CikisYap()
        {
            CurrentUser = null;
            Id = 0;
            PersonelId = 0;
            Rol = "";
            Email = "";
        }

        // ✅ Oturum var mı? (Form açılışlarında çok işe yarar)
        public static bool GirisYapildiMi => Id > 0;

        // ✅ Rol kontrol yardımcıları (opsiyonel ama çok pratik)
        public static bool YetkiliMi(params string[] roller)
        {
            if (!GirisYapildiMi) return false;
            if (roller == null || roller.Length == 0) return true;

            foreach (var r in roller)
            {
                if (string.Equals(Rol, r, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}

