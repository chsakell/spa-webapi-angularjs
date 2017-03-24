using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Abstract
{
    public interface ICustomerRepository : IEntityBaseRepository<Customer> { }

    //public interface IUserRepository : IEntityBaseRepository<User> { }

    //public interface IAttendeeRepository : IEntityBaseRepository<Attendee> { }
}
