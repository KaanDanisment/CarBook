using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Results.StatisticsResult
{
    public class GetTheBrandWithMostCarsQueryResult
    {
        public string BrandName { get; set; }
        public int CarsCount { get; set; }
    }
}
