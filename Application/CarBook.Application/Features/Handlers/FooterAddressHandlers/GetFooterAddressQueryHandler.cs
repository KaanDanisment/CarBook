using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.FooterAddressQueries;
using CarBook.Application.Features.Results.FooterAddressResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.FooterAddressHandlers
{
    public class GetFooterAddressQueryHandler : IRequestHandler<GetFooterAddressQuery, IDataResult<IEnumerable<GetFooterAddressQueryResult>>>
    {
        private readonly IFooterAddressRepository _repository;

        public GetFooterAddressQueryHandler(IFooterAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetFooterAddressQueryResult>>> Handle(GetFooterAddressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<FooterAddress> footerAddresses = await _repository.GetAllAsync(cancellationToken);
                
                IEnumerable<GetFooterAddressQueryResult> getFooterAddressQueryResults = footerAddresses.Select(x => new GetFooterAddressQueryResult
                {
                    FooterAddressId = x.FooterAddressId,
                    Description = x.Description,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetFooterAddressQueryResult>>(getFooterAddressQueryResults, "Footer Addresses found successfully");
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetFooterAddressQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
