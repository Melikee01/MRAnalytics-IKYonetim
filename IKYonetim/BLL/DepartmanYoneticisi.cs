using IKYonetim.DAL;
using IKYonetim.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKYonetim.BLL
{
    public class DepartmanYoneticisi
    {
        private DepartmanDeposu _depo = new DepartmanDeposu();




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
            ad = (ad ?? "").Trim();
            if (ad.Length == 0)
                throw new Exception("Departman adı boş olamaz.");

            _depo.DepartmanEkle(new Departman { DepartmanAdi = ad });
        }

        public void DepartmanGuncelle(int id, string ad)
        {
            ad = (ad ?? "").Trim();
            if (id <= 0)
                throw new Exception("Geçersiz departman seçimi.");
            if (ad.Length == 0)
                throw new Exception("Departman adı boş olamaz.");

            _depo.DepartmanGuncelle(
                new Departman { Id = id, DepartmanAdi = ad }
            );
        }

        public void DepartmanPasifeAl(int id)
        {
            if (id <= 0)
                throw new Exception("Geçersiz departman seçimi.");

            _depo.DepartmanPasifeAl(id);
        }
        public void DepartmanAktifeAl(int id)
        {
            if (id <= 0)
                throw new Exception("Geçersiz departman seçimi.");

            _depo.DepartmanAktifeAl(id);
        }
    }
}



