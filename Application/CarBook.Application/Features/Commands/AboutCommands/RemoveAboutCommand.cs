using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.AboutCommands
{
    public class RemoveAboutCommand: IRequest<IResult>
    {
        public int Id { get; set; }
        public RemoveAboutCommand(int id)
        {
            Id = id;
        }
    }
}
