using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SantaShop.API
{
    [Table("SantaShop.Users")]

    public class User
    {
        public string UserName { get; set; }
        public string FullName { get; set; }

        [JsonIgnore] //paragarantirquenuncaenviamosapasswordnumaresposta
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
