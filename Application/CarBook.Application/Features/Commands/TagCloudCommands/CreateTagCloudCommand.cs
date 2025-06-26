using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.TagCloudCommands
{
    public class CreateTagCloudCommand: IRequest<IResult>
    {
        public string Title { get; set; }
        public int BlogId { get; set; }
    }
}
