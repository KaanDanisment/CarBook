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
    public class GetFooterAddressByIdQueryHandler : IRequestHandler<GetFooterAddressByIdQuery, IDataResult<GetFooterAddressByIdQueryResult>>
    {
        private readonly IFooterAddressRepository _repository;

        public GetFooterAddressByIdQueryHandler(IFooterAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetFooterAddressByIdQueryResult>> Handle(GetFooterAddressByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                FooterAddress footerAddress = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (footerAddress == null)
                {
                    return new ErrorDataResult<GetFooterAddressByIdQueryResult>("Footer Address not found", "BadRequest");
                }
                GetFooterAddressByIdQueryResult getFooterAddressByIdQueryResult = new GetFooterAddressByIdQueryResult
                {
                    FooterAddressId = footerAddress.FooterAddressId,
                    Description = footerAddress.Description,
                    Address = footerAddress.Address,
                    Phone = footerAddress.Phone,
                    Email = footerAddress.Email
                };
                return new SuccessDataResult<GetFooterAddressByIdQueryResult>(getFooterAddressByIdQueryResult, "Footer Address found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetFooterAddressByIdQueryResult>(ex.Message, "SystemError");
            }
           
        }
    }
}
