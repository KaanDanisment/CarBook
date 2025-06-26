using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.BrandResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.BrandQueries
{
    public class GetBrandByIdQuery: IRequest<IDataResult<GetBrandByIdQueryResult>>
    {
        public int Id { get; set; }
        public GetBrandByIdQuery(int id)
        {
            Id = id;
        }
    }
}
