using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.SocialMediaResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.SocialMediaQueries
{
    public class GetSocialMediaByIdQuery: IRequest<IDataResult<GetSocialMediaByIdQueryResult>>
    {
        public int Id { get; set; }

        public GetSocialMediaByIdQuery(int id)
        {
            Id = id;
        }
    }
}
