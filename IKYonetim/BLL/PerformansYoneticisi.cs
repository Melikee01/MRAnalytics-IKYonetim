using IKYonetim.DAL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;

namespace IKYonetim.BLL
{
    public class PerformansYoneticisi
    {
        private readonly PerformansDeposu _depo = new PerformansDeposu();
        private readonly PersonelDeposu _PersonelDeposu = new PersonelDeposu();

        private static bool IsRole(string role, string expected)
            => string.Equals(role?.Trim(), expected, StringComparison.OrdinalIgnoreCase);

        private static bool AdminMi() => IsRole(OturumYoneticisi.Rol, "Admin");
        private static bool IkMi() => IsRole(OturumYoneticisi.Rol, "IK");

        public void PerformansEkle(Performans p)
        {
            if (p.PersonelId <= 0) throw new Exception("Personel seçilmedi.");
            if (p.DegerlendirenId <= 0) throw new Exception("Değerlendiren bilgisi zorunlu.");
            if (p.Puan < 1 || p.Puan > 100) throw new Exception("Puan 1-100 arasında olmalı.");

            if (p.PersonelId == p.DegerlendirenId)
                throw new Exception("Kendi kendini değerlendiremezsiniz.");

            
            if (IkMi())
            {
                var hedefRol = _PersonelDeposu.PersonelinRolunuGetir(p.PersonelId);
                if (string.IsNullOrWhiteSpace(hedefRol))
                    hedefRol = "users";

                if (!string.Equals(hedefRol, "users", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("İK yalnızca normal kullanıcıları (users) değerlendirebilir.");
            }

            _depo.PerformansEkle(p);
        }

        public void PerformansGuncelle(Performans p)
        {
            if (p.Id <= 0) throw new Exception("Geçersiz kayıt.");
            if (p.PersonelId <= 0) throw new Exception("Personel seçilmedi.");
            if (p.DegerlendirenId <= 0) throw new Exception("Değerlendiren bilgisi zorunlu.");
            if (p.Puan < 1 || p.Puan > 100) throw new Exception("Puan 1-100 arasında olmalı.");

            if (p.PersonelId == p.DegerlendirenId)
                throw new Exception("Kendi kendini değerlendiremezsiniz.");

           
            var eskiKayit = _depo.PerformansGetirById(p.Id);
            if (eskiKayit == null)
                throw new Exception("Kayıt bulunamadı.");

            
            if (!AdminMi())
            {
               
                if (!IkMi())
                    throw new Exception("Bu işlem için yetkin yok.");

                if (eskiKayit.DegerlendirenId != OturumYoneticisi.PersonelId)
                    throw new Exception("Admin tarafından girilen değerlendirmeyi İK güncelleyemez. Sadece kendi değerlendirdiğini güncelleyebilir.");
            }

            
            if (IkMi())
            {
                var hedefRol = _PersonelDeposu.PersonelinRolunuGetir(p.PersonelId);

                if (string.IsNullOrWhiteSpace(hedefRol))
                    throw new Exception("Seçilen personelin rol bilgisi bulunamadı. (kullanici.personel_id eşleşmesi yok)");

                if (!string.Equals(hedefRol, "users", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("İK yalnızca normal kullanıcıları (users) değerlendirebilir.");
            }

            _depo.PerformansGuncelle(p);
        }

        public List<Performans> TumPerformanslar()
        {
            return _depo.TumPerformanslariGetir();
        }

        public List<Performans> PersonelinPerformanslari(int personelId)
        {
            return _depo.PersonelinPerformanslariniGetir(personelId);
        }

        public void PerformansPasifeAl(int performansId)
        {
            if (performansId <= 0)
                throw new Exception("Geçersiz kayıt.");

            var kayit = _depo.PerformansGetirById(performansId);
            if (kayit == null)
                throw new Exception("Kayıt bulunamadı.");

            
            if (AdminMi())
            {
                _depo.PerformansPasifeAl(performansId);
                return;
            }

            if (!IkMi())
                throw new Exception("Bu işlem için yetkin yok.");

            if (kayit.DegerlendirenId != OturumYoneticisi.PersonelId)
                throw new Exception("Admin tarafından girilen değerlendirmeyi İK silemez. Sadece kendi değerlendirdiğini silebilir.");

            _depo.PerformansPasifeAl(performansId);
        }

        public void PerformansAktifeAl(int id)
        {
            if (id <= 0) throw new Exception("Geçersiz kayıt.");
            _depo.PerformansAktifeAl(id);
        }
    }
}
