using IKYonetim.DAL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;

namespace IKYonetim.BLL
{
    public class IzinYoneticisi
    {
        private IzinDAL _izinDal = new IzinDAL();

        public void IzinTalepEt(Izin izin)
        {
            if (izin.PersonelId <= 0)
                throw new Exception("Personel bilgisi yok.");

            if (izin.BaslangicTarihi > izin.BitisTarihi)
                throw new Exception("Tarih aralığı hatalı.");

            _izinDal.IzinEkle(izin);
        }

        public List<Izin> KendiIzinlerim()
        {
            return _izinDal.PersonelIzinleriniGetir(
                OturumYoneticisi.PersonelId
            );
        }
    }
}
