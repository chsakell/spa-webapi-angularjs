using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class ActivityLogConfiguration : EntityBaseConfiguration<ActivityLog>
    {
        public ActivityLogConfiguration()
        {
            //Property(g => g.).IsRequired().HasMaxLength(50);
        }
    }
}
