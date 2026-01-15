using IKYonetim.DAL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;

namespace IKYonetim.BLL
{
    public class DepartmanYoneticisi
    {
        private readonly DepartmanDeposu _depo;

        public DepartmanYoneticisi(DepartmanDeposu depo = null)
        {
            _depo = depo ?? new DepartmanDeposu();
        }

        public List<Departman> TumDepartmanlar()
        {
            return _depo.DepartmanlariGetir();
        }

        public List<Departman> AktifDepartmanlariGetir()
        {
            return _depo.AktifDepartmanlariGetir();
        }

        public void DepartmanEkle(string ad)
        {
            ad = (ad ?? string.Empty).Trim();

            if (ad.Length == 0)
                throw new ArgumentException("Departman adı boş olamaz.");

            _depo.DepartmanEkle(new Departman { DepartmanAdi = ad });
        }

        public void DepartmanGuncelle(int id, string ad)
        {
            ad = (ad ?? string.Empty).Trim();

            if (id <= 0)
                throw new ArgumentException("Geçersiz departman seçimi.");

            if (ad.Length == 0)
                throw new ArgumentException("Departman adı boş olamaz.");

            _depo.DepartmanGuncelle(new Departman
            {
                Id = id,
                DepartmanAdi = ad
            });
        }

        public void DepartmanPasifeAl(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Geçersiz departman seçimi.");

            _depo.DepartmanPasifeAl(id);
        }

        public void DepartmanAktifeAl(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Geçersiz departman seçimi.");

            _depo.DepartmanAktifeAl(id);
        }
    }
}
