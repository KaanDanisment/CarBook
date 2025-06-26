using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.CommentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.CommentQueries
{
    public class GetCommentByIdQuery: IRequest<IDataResult<GetCommentByIdQueryResult>>
    {
        public int Id { get; set; }

        public GetCommentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
