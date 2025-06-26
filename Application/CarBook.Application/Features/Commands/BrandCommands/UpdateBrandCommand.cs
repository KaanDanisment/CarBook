using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.BrandCommands
{
    public class UpdateBrandCommand: IRequest<IResult>
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
    }
}
