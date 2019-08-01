using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    public class UserPermission : IEntityBase
    {
       public int ID { get; set; }
       public int UserId { get; set; }
       public string PermissionKey { get; set; }
        public bool Granted { get; set; }
        public virtual User User { get; set; }
    }
}
