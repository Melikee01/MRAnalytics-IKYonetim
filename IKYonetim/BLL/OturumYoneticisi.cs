using System;
using IKYonetim.DAL;
using IKYonetim.ENTITY;

namespace IKYonetim.BLL
{
    public static class OturumYoneticisi
    {
        public static int Id { get; private set; }
        public static int PersonelId { get; private set; }
        public static string Rol { get; private set; } = "";
        public static string Email { get; private set; } = "";
        public static Users CurrentUser { get; private set; }

        
        private static readonly UsersDeposu _UsersDeposu = new UsersDeposu();

        public static bool GirisYap(string email, string parola, out string hataMesaji)
        {
            hataMesaji = "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
            {
                hataMesaji = "Email ve parola boş olamaz.";
                return false;
            }

            var user = _UsersDeposu.GirisIcinUsersGetir(email.Trim(), parola);
            if (user == null)
            {
                hataMesaji = "Hatalı email/parola veya kullanıcı pasif.";
                return false;
            }

            CurrentUser = user;
            Id = user.Id;
            PersonelId = user.PersonelId ?? 0; 
            Rol = user.Rol ?? "";
            Email = user.email ?? "";

            return true;
        }

        public static void CikisYap()
        {
            CurrentUser = null;
            Id = 0;
            PersonelId = 0;
            Rol = "";
            Email = "";
        }

        public static bool GirisYapildiMi => Id > 0;

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

