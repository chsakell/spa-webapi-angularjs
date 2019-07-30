using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Data.Abstract;
using HomeCinema.Data;
using HomeCinema.Data.Infrastructure;

namespace HomeCinema.Data.Repositories
{
    public class CustomerRepository : EntityBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbFactory context)
            : base(context)
        { }
    }
}
