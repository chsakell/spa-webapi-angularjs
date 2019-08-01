using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
   public class Setting :IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int AppScope { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Description { get; set; }
    }
}
