using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Extensions
{
    public static class RentalExtensions
    {
        //public static IEnumerable<Rental> GetStockRentals(this IEntityBaseRepository<Rental> rentalsRepository, IEnumerable<Stock> stocks)
        //{
        //    IEnumerable<Rental> _rentals = null;
        //    IEnumerable<int> _stockIds = stocks.Select(s => s.ID).Distinct();

        //    _rentals = rentalsRepository.GetAll().Where(r => _stockIds.Contains(r.StockId));

        //    return _rentals;
        //}
    }
}
