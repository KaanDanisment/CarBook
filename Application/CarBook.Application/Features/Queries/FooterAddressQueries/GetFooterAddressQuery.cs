using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.FooterAddressResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.FooterAddressQueries
{
    public class GetFooterAddressQuery: IRequest<IDataResult<IEnumerable<GetFooterAddressQueryResult>>>
    {
    }
}
