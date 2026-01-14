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

        public void PerformansEkle(Performans p)
        {
            if (p.PersonelId <= 0) throw new Exception("Personel seçilmedi.");
            if (p.DegerlendirenId <= 0) throw new Exception("Değerlendiren bilgisi zorunlu.");
            if (p.Puan < 1 || p.Puan > 100) throw new Exception("Puan 1-100 arasında olmalı.");

            // NOT: Bu kontrol ancak DegerlendirenId de personel_id ise anlamlıdır.
            // Senin senaryonda DegerlendirenId = users.id olma ihtimali yüksek.
            // Yine de kalsın istiyorsan kalsın; sorun çıkarırsa kaldırırız.
            if (p.PersonelId == p.DegerlendirenId)
                throw new Exception("Kendi kendini değerlendiremezsiniz.");

            // IK → Admin/IK değerlendiremesin (IK sadece users değerlendirsin)
            if (string.Equals(OturumYoneticisi.Rol, "IK", StringComparison.OrdinalIgnoreCase))
            {
                var hedefRol = _PersonelDeposu.PersonelinRolunuGetir(p.PersonelId);

                // Kullanıcı kaydı yoksa, normal personel gibi davran (users kabul et)
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

            // NOT: Yukarıdaki not burada da geçerli
            if (p.PersonelId == p.DegerlendirenId)
                throw new Exception("Kendi kendini değerlendiremezsiniz.");

            if (string.Equals(OturumYoneticisi.Rol, "IK", StringComparison.OrdinalIgnoreCase))
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

            // Admin her şeyi silebilsin istiyorsan bu blok kalsın.
            // Admin de sadece kendi kaydını silebilsin dersen bu bloğu kaldır.
            if (string.Equals(OturumYoneticisi.Rol, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                _depo.PerformansPasifeAl(performansId);
                return;
            }

            // Yetki: sadece kendi yazdığı değerlendirmeyi silebilir
            // DegerlendirenId = users.id, OturumYoneticisi.Id = users.id
            if (kayit.DegerlendirenId != OturumYoneticisi.PersonelId)
                throw new Exception("Bu performansı silemezsiniz. Sadece değerlendirmeyi yapan kişi silebilir.");


            _depo.PerformansPasifeAl(performansId);
        }

        public void PerformansAktifeAl(int id)
        {
            if (id <= 0) throw new Exception("Geçersiz kayıt.");
            _depo.PerformansAktifeAl(id);
        }
    }
}
