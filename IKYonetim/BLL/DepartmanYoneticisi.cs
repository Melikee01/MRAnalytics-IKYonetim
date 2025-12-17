using IKYonetim.DAL;
using IKYonetim.ENTITY;
using System.Collections.Generic;

namespace IKYonetim.BLL
{
    public class DepartmanYoneticisi
    {
        private DepartmanDeposu _depo = new DepartmanDeposu();

        public List<Departman> AktifDepartmanlariGetir()
        {
            return _depo.DepartmanlariGetir();
        }

    }

}

