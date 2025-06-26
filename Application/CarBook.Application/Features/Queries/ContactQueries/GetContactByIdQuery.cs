using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.ContactResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.ContactQueries
{
    public class GetContactByIdQuery: IRequest<IDataResult<GetContactByIdQueryResult>>
    {
        public int Id { get; set; }
        public GetContactByIdQuery(int id)
        {
            Id = id;
        }
    }
}
