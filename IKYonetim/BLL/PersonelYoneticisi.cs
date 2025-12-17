using IKYonetim.DAL;
using IKYonetim.ENTITY;
using IKYonetim.ENTITY.IKYonetim.ENTITY;
using System.Collections.Generic;

namespace IKYonetim.BLL
{
    public class PersonelYoneticisi
    {
        private PersonelDeposu _depo = new PersonelDeposu();

        public List<Personel> TumPersonelleriGetir()
        {
            return _depo.TumPersoneller();
        }

        public Personel PersonelGetir(int personelId)
        {
            if (personelId <= 0)
                return null;

            return _depo.PersonelGetir(personelId);
        }
    }
}

