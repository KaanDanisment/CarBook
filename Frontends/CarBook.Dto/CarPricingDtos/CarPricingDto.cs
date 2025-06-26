using CarBook.Dto.PricingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.CarPricingDtos
{
    public class CarPricingDto
    {
        public int CarPricingId { get; set; }
        public int CarId { get; set; }
        public int PricingId { get; set; }
        public PricingDto Pricing{ get; set; }
        public decimal Amount { get; set; }
    }
}
