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
    public class GetCommentsByBlogIdQuery: IRequest<IDataResult<IEnumerable<GetCommentsByBlogIdQueryResult>>>
    {
        public int Id { get; set; }

        public GetCommentsByBlogIdQuery(int id)
        {
            Id = id;
        }
    }
}
