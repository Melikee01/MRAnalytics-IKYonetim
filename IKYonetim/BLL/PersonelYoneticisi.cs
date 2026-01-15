using IKYonetim.DAL;
using IKYonetim.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace IKYonetim.BLL
{
    public class PersonelYoneticisi
    {
        private readonly PersonelDeposu _personelDeposu = new PersonelDeposu();
        private readonly UsersDeposu _usersDeposu = new UsersDeposu();
        private readonly baglantiGetir _baglanti = new baglantiGetir();

        public List<Personel> TumPersonelleriGetir()
        {
            return _personelDeposu.TumPersoneller();
        }

        public Personel PersonelGetir(int personelId)
        {
            if (personelId <= 0)
                return null;

            return _personelDeposu.PersonelGetir(personelId);
        }
        public void PersonelEkle(Personel p)
        {
            Validate(p);
            _personelDeposu.PersonelEkle(p);
        }

        public void PersonelGuncelle(Personel p)
        {
            if (p.Id <= 0)
                throw new Exception("Güncellenecek personeli seç.");

            Validate(p);
            _personelDeposu.PersonelGuncelle(p);
        }

        public void PersonelSil(int id)
        {
            if (id <= 0)
                throw new Exception("Silinecek personeli seç.");

            _personelDeposu.PersonelSil(id);
        }
        private void Validate(Personel p)
        {
            if (string.IsNullOrWhiteSpace(p.Ad))
                throw new Exception("Ad boş olamaz.");

            if (string.IsNullOrWhiteSpace(p.Soyad))
                throw new Exception("Soyad boş olamaz.");

            if (string.IsNullOrWhiteSpace(p.Departman))
                throw new Exception("Departman boş olamaz.");

            if (string.IsNullOrWhiteSpace(p.Pozisyon))
                throw new Exception("Pozisyon boş olamaz.");
        }
        public void PersonelVeUsersEkle(Personel p, string email, string rol)
        {
            if (p == null) throw new Exception("Personel bilgisi boş olamaz.");
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("E-mail boş olamaz.");
            if (string.IsNullOrWhiteSpace(rol)) throw new Exception("Rol seçilmelidir.");

            email = email.Trim();
            rol = rol.Trim();

            using (MySqlConnection conn = _baglanti.BaglantiGetir())
            using (MySqlTransaction tx = conn.BeginTransaction())
            {
                try
                {
                    
                    if (_usersDeposu.EmailVarMi(email, conn, tx))
                        throw new Exception("Bu e-mail ile kayıtlı kullanıcı zaten var.");

                   
                    int personelId = _personelDeposu.PersonelEkleVeIdDondur(p, conn, tx);

                    
                    Users u = new Users
                    {
                        PersonelId = personelId,
                        email = email,
                        Parola = "1234",
                        Rol = rol,        
                        Aktif = true
                    };

                    _usersDeposu.UsersEkle(u, conn, tx);

                    tx.Commit();
                }
                catch
                {
                    try { tx.Rollback(); } catch { }
                    throw;
                }
            }
        }
        public void PersonelAktiflikDegistir(int personelId, bool aktif)
        {
            if (personelId <= 0)
                throw new Exception("Geçerli bir personel seçilmedi.");

            _personelDeposu.PersonelAktiflikGuncelle(personelId, aktif);
        }
        public List<Personel> AktifPersonelleriGetir()
        {
            return _personelDeposu.AktifPersonelleriGetir();
        }
        public void PersonelAktiflikVeIzinDegistir(int personelId, bool aktif)
        {
            if (personelId <= 0)
                throw new Exception("Geçerli bir personel seçilmedi.");

            _personelDeposu.PersonelAktiflikVeIzinGuncelle(personelId, aktif);
        }



    }
}

