using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.TagCloudResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.TagCloudQueries
{
    public class GetTagCloudByIdQuery: IRequest<IDataResult<GetTagCloudByIdQueryResult>>
    {
        public int Id { get; set; }

        public GetTagCloudByIdQuery(int id)
        {
            Id = id;
        }
    }
}
