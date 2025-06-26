using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.StatisticsResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.StatisticsQueries
{
    public class GetAvgWeeklyRentalPriceQuery: IRequest<IDataResult<GetAvgWeeklyRentalPriceQueryResult>>
    {
    }
}
