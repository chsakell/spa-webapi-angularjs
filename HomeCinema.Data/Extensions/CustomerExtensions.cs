using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Extensions
{
    public static class CustomerExtensions
    {
        public static bool UserExists(this IEntityBaseRepository<Customer> customersRepository, string email, string identityCard)
        {
            bool _userExists = false;

            _userExists = customersRepository.GetAll()
                .Any(c => c.Email.ToLower() == email ||
                c.IdentityCard.ToLower() == identityCard);

            return _userExists;
        }

        public static string GetCustomerFullName(this IEntityBaseRepository<Customer> customersRepository, int customerId)
        {
            string _customerName = string.Empty;

            var _customer = customersRepository.GetSingle(customerId);

            _customerName = _customer.FirstName + " " + _customer.LastName;

            return _customerName;
        }
    }
}
