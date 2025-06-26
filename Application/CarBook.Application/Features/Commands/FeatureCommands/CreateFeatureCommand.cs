using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.FeatureCommands
{
    public class CreateFeatureCommand: IRequest<IResult>
    {
        public string Name { get; set; }
    }
}
