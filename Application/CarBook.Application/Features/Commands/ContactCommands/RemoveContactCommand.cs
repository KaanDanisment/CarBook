using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.ContactCommands
{
    public class RemoveContactCommand: IRequest<IResult>
    {
        public int Id { get; set; }
        public RemoveContactCommand(int id)
        {
            Id = id;
        }
    }
}
