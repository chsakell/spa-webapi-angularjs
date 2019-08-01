using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    public class RolePermission : IEntityBase
    {
       public int ID { get; set; }
       public int RoleId { get; set; }
       public string PermissionKey { get; set; }
        public virtual Role Role { get; set; }
    }
}
