using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.BannerResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.BannerQueries
{
    public class GetBannerByIdQuery: IRequest<IDataResult<GetBannerByIdQueryResult>>
    {
        public int Id { get; set; }

        public GetBannerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
