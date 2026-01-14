using IKYonetim.DAL;
using System;

namespace ikYonetimNYPProjesi.BLL
{
    public class SifreYoneticisi
    {
        private readonly SifreDeposu _depo;

        // ✅ Parametresiz constructor (UI artık new SifreYoneticisi() diyebilir)
        public SifreYoneticisi()
        {
            _depo = new SifreDeposu();
        }

        public void SifreDegistir(int personelId, string eskiSifre, string yeniSifre, string yeniSifreTekrar)
        {
            if (personelId <= 0)
                throw new Exception("Oturum personel bilgisi bulunamadı.");

            if (string.IsNullOrWhiteSpace(eskiSifre) ||
                string.IsNullOrWhiteSpace(yeniSifre) ||
                string.IsNullOrWhiteSpace(yeniSifreTekrar))
                throw new Exception("Lütfen tüm alanları doldurun.");

            if (yeniSifre != yeniSifreTekrar)
                throw new Exception("Yeni şifre ve tekrar şifre aynı olmalıdır.");

            var mevcut = _depo.PersonelinSifresiniGetir(personelId);

            if (mevcut == null)
                throw new Exception("Kullanıcı kaydı bulunamadı. (users tablosunda eşleşme yok)");

            if (mevcut != eskiSifre)
                throw new Exception("Mevcut şifre yanlış.");

            _depo.PersonelinSifresiniGuncelle(personelId, yeniSifre);
        }
    }
}