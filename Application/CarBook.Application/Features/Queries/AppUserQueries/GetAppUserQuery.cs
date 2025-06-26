using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.AppUserResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.AppUserQueries
{
    public class GetAppUserQuery: IRequest<IDataResult<GetAppUserQueryResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
