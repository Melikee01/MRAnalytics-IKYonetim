using IKYonetim.DAL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IKYonetim.BLL
{
    public class IzinYoneticisi
    {
        private readonly IzinDeposu _izinDal;

        public IzinYoneticisi(IzinDeposu izinDal = null)
        {
            _izinDal = izinDal ?? new IzinDeposu();
        }

        private static bool IsRole(string role, string expected)
            => string.Equals(role?.Trim(), expected, StringComparison.OrdinalIgnoreCase);

        // ---- ASYNC ----
        public async Task<List<Izin>> ListeleAsync(int? personelId = null, bool tumu = false, string durum = null)
        {
            List<Izin> liste = tumu
                ? await TumIzinlerAsync()
                : await KendiIzinlerimAsync(personelId);

            if (!string.IsNullOrWhiteSpace(durum))
            {
                liste = liste.FindAll(x =>
                    !string.IsNullOrWhiteSpace(x.Durum) &&
                    x.Durum.Equals(durum, StringComparison.OrdinalIgnoreCase));
            }

            return liste;
        }

        public async Task DurumGuncelleAsync(int izinId, string yeniDurum)
        {
            await IzinDurumuGuncelleAsync(izinId, yeniDurum);
        }

        public async Task IzinTalepEtAsync(Izin izin, int personelId = 0)
        {
            if (izin == null) throw new ArgumentNullException(nameof(izin));

            if (personelId > 0)
                izin.PersonelId = personelId;

            if (izin.PersonelId <= 0)
                izin.PersonelId = OturumYoneticisi.PersonelId;

            if (izin.PersonelId <= 0)
                throw new InvalidOperationException("Personel bilgisi bulunamadı (PersonelId boş).");

            if (izin.BaslangicTarihi == default || izin.BitisTarihi == default)
                throw new ArgumentException("Başlangıç ve bitiş tarihi boş olamaz.");

            if (izin.BitisTarihi.Date < izin.BaslangicTarihi.Date)
                throw new ArgumentException("Bitiş tarihi, başlangıç tarihinden küçük olamaz.");

            izin.IzinTuru = (izin.IzinTuru ?? string.Empty).Trim();
            if (izin.IzinTuru.Length == 0)
                throw new ArgumentException("İzin türü boş olamaz.");

            if (string.IsNullOrWhiteSpace(izin.Durum))
                izin.Durum = "Beklemede";

            await _izinDal.IzinEkleAsync(izin);
        }

        public async Task<List<Izin>> KendiIzinlerimAsync(int? personelId = null)
        {
            int pid = personelId ?? OturumYoneticisi.PersonelId;

            if (pid <= 0)
                throw new InvalidOperationException("PersonelId bulunamadı.");

            return await _izinDal.PersonelinIzinleriAsync(pid);
        }

        public async Task<List<Izin>> TumIzinlerAsync()
        {
            string rol = OturumYoneticisi.Rol;

            // IK/admin görebilir
            if (!IsRole(rol, "admin") && !IsRole(rol, "ik"))
                throw new UnauthorizedAccessException("Bu işlem için yetkin yok.");

            return await _izinDal.TumIzinlerAsync();
        }

        public async Task IzinDurumuGuncelleAsync(int izinId, string yeniDurum)
        {
            if (izinId <= 0)
                throw new ArgumentException("Geçersiz izinId.");

            yeniDurum = (yeniDurum ?? string.Empty).Trim();
            if (yeniDurum.Length == 0)
                throw new ArgumentException("Yeni durum boş olamaz.");

            string rol = OturumYoneticisi.Rol;

            // sadece admin
            if (!IsRole(rol, "admin"))
                throw new UnauthorizedAccessException("Bu işlem için yetkin yok. (Sadece Admin)");

            if (yeniDurum != "Beklemede" && yeniDurum != "Onaylandı" && yeniDurum != "Reddedildi")
                throw new ArgumentException("Geçersiz durum. (Beklemede/Onaylandı/Reddedildi)");

            await _izinDal.DurumGuncelleAsync(izinId, yeniDurum);
        }

        public async Task IzinSilAsync(int izinId)
        {
            if (izinId <= 0)
                throw new ArgumentException("Geçersiz izinId.");

            string rol = OturumYoneticisi.Rol;

            // sadece admin
            if (!IsRole(rol, "admin"))
                throw new UnauthorizedAccessException("Silme işlemi sadece Admin için.");

            await _izinDal.IzinSilAsync(izinId);
        }

        // ---- İstersen eski SYNC metotlar kalsın (mevcut kullanan yer varsa bozulmasın) ----
        public List<Izin> Listele(int? personelId = null, bool tumu = false, string durum = null)
            => ListeleAsync(personelId, tumu, durum).GetAwaiter().GetResult();

        public void DurumGuncelle(int izinId, string yeniDurum)
            => DurumGuncelleAsync(izinId, yeniDurum).GetAwaiter().GetResult();

        public void IzinTalepEt(Izin izin, int personelId = 0)
            => IzinTalepEtAsync(izin, personelId).GetAwaiter().GetResult();
    }
}
