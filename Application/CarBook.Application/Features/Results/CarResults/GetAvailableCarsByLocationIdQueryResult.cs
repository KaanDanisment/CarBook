using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Results.CarResults
{
    public class GetAvailableCarsByLocationIdQueryResult
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Transmission { get; set; }
        public string CoverImageUrl { get; set; }
        public string BigImageUrl { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal DailyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
    }
}
