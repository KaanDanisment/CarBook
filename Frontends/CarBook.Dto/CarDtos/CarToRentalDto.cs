using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.CarDtos
{
    public class CarToRentalDto
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Transmission { get; set; }
        public string CoverImageUrl { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal DailyPrice { get; set; }
    }
}
