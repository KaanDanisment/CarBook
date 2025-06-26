using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.CommentCommands
{
    public class CreateCommentCommand: IRequest<IResult>
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public int BlogId { get; set; }
    }
}
