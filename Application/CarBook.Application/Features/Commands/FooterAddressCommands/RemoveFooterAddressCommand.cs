using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.FooterAddressCommands
{
    public class RemoveFooterAddressCommand: IRequest<IResult>
    {
        public int Id { get; set; }

        public RemoveFooterAddressCommand(int id)
        {
            Id = id;
        }
    }
}
