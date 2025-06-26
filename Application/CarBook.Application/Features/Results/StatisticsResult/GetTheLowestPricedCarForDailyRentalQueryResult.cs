﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Results.StatisticsResult
{
    public class GetTheLowestPricedCarForDailyRentalQueryResult
    {
        public string BrandName { get; set; }
        public string CarModel { get; set; }
        public decimal Amount { get; set; }
    }
}
