using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.FeatureResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.FeatureQueries
{
    public class GetFeatureByIdQuery: IRequest<IDataResult<GetFeatureByIdQueryResult>>
    {
        public int Id { get; set; }

        public GetFeatureByIdQuery(int id)
        {
            Id = id;
        }
    }
}
