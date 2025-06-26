using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.BlogResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.BlogQueries
{
    public class GetBlogByIdQuery: IRequest<IDataResult<GetBlogByIdQueryResult>>
    {
        public int Id { get; set; }
        public GetBlogByIdQuery(int id)
        {
            Id = id;
        }
    }
}
