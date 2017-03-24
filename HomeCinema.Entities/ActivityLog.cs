using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    public class ActivityLog :IEntityBase
    {
        public int ID { get; set; }
        public int ActivityLogTypeID { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime DateUTC { get; set; }
        public string IpAddress { get; set; }
        public virtual User User { get; set; }
        public virtual ActivityLogType ActivityLogType { get; set; }

    }
}
