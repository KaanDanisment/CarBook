using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.ServiceResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.ServiceQueries
{
    public class GetServiceByIdQuery: IRequest<IDataResult<GetServiceByIdQueryResult>>
    {
        public int Id { get; set; }

        public GetServiceByIdQuery(int id)
        {
            Id = id;
        }
    }
}
