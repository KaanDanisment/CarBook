using CarBook.Application.Common.Results.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Commands.TagCloudCommands
{
    public class RemoveTagCloudCommand: IRequest<IResult>
    {
        public int TagCloudId { get; set; }

        public RemoveTagCloudCommand(int tagCloudId)
        {
            TagCloudId = tagCloudId;
        }
    }
}
