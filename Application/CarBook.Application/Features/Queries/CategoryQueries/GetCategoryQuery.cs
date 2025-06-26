using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.CategoryResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.CategoryQueries
{
    public class GetCategoryQuery: IRequest<IDataResult<IEnumerable<GetCategoryQueryResult>>>
    {
    }
}
