using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.AppUserCommands
{
    public class RegisterAppUserCommand: IRequest<IResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid AppRoleId { get; set; }
    }
}
