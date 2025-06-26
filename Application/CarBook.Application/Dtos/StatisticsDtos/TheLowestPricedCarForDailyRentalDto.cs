using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Dtos.StatisticsDtos
{
    public class TheLowestPricedCarForDailyRentalDto
    {
        public string BrandName { get; set; }
        public string CarModel { get; set; }
        public decimal Amount { get; set; }
    }
}
