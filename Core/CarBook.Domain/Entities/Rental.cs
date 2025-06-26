using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer  { get; set; }
        public DateOnly? RentDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public TimeOnly? RentTime { get; set; }
        public TimeOnly? ReturnTime { get; set; }
        public int PickupLocationId { get; set; }
        public Location PickupLocation { get; set; }
        public int DropoffLocationId { get; set; }
        public Location DropoffLocation { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
