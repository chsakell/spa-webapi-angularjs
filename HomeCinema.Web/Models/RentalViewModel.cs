using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Models
{
    public class RentalViewModel
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public int StockId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public string Status { get; set; }
    }
}