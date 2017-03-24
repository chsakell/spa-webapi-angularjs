using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
   public class UserPreference:IEntityBase
    {
        public int ID { get; set; }

        public int UserId { get; set; }

        public string PreferenceType { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
        public virtual User User { get; set; }

    }
}
