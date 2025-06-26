using CarBook.Application.Common.Results.Abstracts;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.CommentCommands
{
    public class UpdateCommentCommand: IRequest<IResult>
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BlogId { get; set; }
    }
}
