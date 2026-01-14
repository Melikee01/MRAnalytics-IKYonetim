using IKYonetim.DAL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;

namespace IKYonetim.BLL
{
    public class IzinYoneticisi
    {
        private readonly IzinDeposu _izinDal = new IzinDeposu();

        // =========================================================
        // 0) UI UYUMLULUK (KÖPRÜ METOTLAR)
        // =========================================================

        /// <summary>
        /// UI tarafında çağırılan "Listele" metodu.
        /// tumu=true  => tüm izinler (IK/Admin)
        /// tumu=false => sadece personelin izinleri
        /// durum doluysa => sadece o durumdaki izinler (örn: "Beklemede")
        /// </summary>
        public List<Izin> Listele(int? personelId = null, bool tumu = false, string durum = null)
        {
            List<Izin> liste;

            if (tumu)
                liste = TumIzinler();              // rol kontrolü TumIzinler içinde
            else
                liste = KendiIzinlerim(personelId);

            // ✅ Durum filtresi (DAL'e dokunmadan BLL'de filtre)
            if (!string.IsNullOrWhiteSpace(durum))
            {
                liste = liste.FindAll(x =>
                    !string.IsNullOrWhiteSpace(x.Durum) &&
                    x.Durum.Equals(durum, StringComparison.OrdinalIgnoreCase));
            }

            return liste;
        }

        /// <summary>
        /// UI tarafında çağırılan "DurumGuncelle" metodu için uyumluluk sağlar.
        /// </summary>
        public void DurumGuncelle(int izinId, string yeniDurum)
        {
            IzinDurumuGuncelle(izinId, yeniDurum);
        }

        // =========================
        // 1) İZİN TALEBİ OLUŞTUR
        // =========================
        public void IzinTalepEt(Izin izin, int personelId = 0)
        {
            if (izin == null) throw new ArgumentNullException(nameof(izin));

            // PersonelId doldurma
            if (personelId > 0)
                izin.PersonelId = personelId;

            if (izin.PersonelId <= 0)
                izin.PersonelId = OturumYoneticisi.PersonelId;

            if (izin.PersonelId <= 0)
                throw new Exception("Personel bilgisi bulunamadı (PersonelId boş).");

            // Tarih kontrol
            if (izin.BaslangicTarihi == default || izin.BitisTarihi == default)
                throw new Exception("Başlangıç ve bitiş tarihi boş olamaz.");

            if (izin.BitisTarihi < izin.BaslangicTarihi)
                throw new Exception("Bitiş tarihi, başlangıç tarihinden küçük olamaz.");

            // İzin türü kontrol
            if (string.IsNullOrWhiteSpace(izin.IzinTuru))
                throw new Exception("İzin türü boş olamaz.");

            // Durum default
            if (string.IsNullOrWhiteSpace(izin.Durum))
                izin.Durum = "Beklemede";

            _izinDal.IzinEkle(izin);
        }

        // =========================
        // 2) KENDİ İZİNLERİM
        // =========================
        public List<Izin> KendiIzinlerim(int? personelId = null)
        {
            int pid = personelId ?? OturumYoneticisi.PersonelId;

            if (pid <= 0)
                throw new Exception("PersonelId bulunamadı.");

            return _izinDal.PersonelinIzinleri(pid);
        }

        // =========================
        // 3) TÜM İZİNLER (IK / Admin)
        // =========================
        public List<Izin> TumIzinler()
        {
            string rol = OturumYoneticisi.Rol;

            if (rol != "Admin" && rol != "IK")
                throw new Exception("Bu işlem için yetkin yok.");

            return _izinDal.TumIzinler();
        }

        // =========================
        // 4) ONAYLA / REDDET
        // =========================
        public void IzinDurumuGuncelle(int izinId, string yeniDurum)
        {
            if (izinId <= 0)
                throw new Exception("Geçersiz izinId.");

            if (string.IsNullOrWhiteSpace(yeniDurum))
                throw new Exception("Yeni durum boş olamaz.");

            // ✅ Sadece Admin onay/red yapabilsin (önceki kuralın buydu)
            string rol = OturumYoneticisi.Rol;
            if (rol != "Admin")
                throw new Exception("Bu işlem için yetkin yok. (Sadece Admin)");

            // Sadece izin verilen durumlar
            if (yeniDurum != "Beklemede" && yeniDurum != "Onaylandı" && yeniDurum != "Reddedildi")
                throw new Exception("Geçersiz durum. (Beklemede/Onaylandı/Reddedildi)");

            _izinDal.DurumGuncelle(izinId, yeniDurum);
        }

        // =========================
        // 5) İZİN SİL (Opsiyonel)
        // =========================
        public void IzinSil(int izinId)
        {
            if (izinId <= 0)
                throw new Exception("Geçersiz izinId.");

            string rol = OturumYoneticisi.Rol;
            if (rol != "Admin")
                throw new Exception("Silme işlemi sadece Admin için.");

            _izinDal.IzinSil(izinId);
        }
    }
}





