using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.BlogCommands
{
    public class RemoveBlogCommand: IRequest<IResult>
    {
        public int Id { get; set; }
        public RemoveBlogCommand(int id)
        {
            Id = id;
        }
    }
}
