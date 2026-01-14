using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKYonetim.ENTITY
{
    public class Users
    {
        public int Id { get; set; }
        public string email { get; set; } = "";
        public string Parola { get; set; } = "";
        public string Rol { get; set; } = "";      // "Admin" | "IK" | "User"
        public bool Aktif { get; set; }
        public int? PersonelId { get; set; }        // Admin için null olabilir
    }

}
