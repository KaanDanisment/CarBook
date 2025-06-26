using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Dtos.CustomerDtos;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Services.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<CustomerDto>> CreateCustomer(CreateCustomerDto createCustomerDto, CancellationToken cancellationToken);
        Task<IDataResult<CustomerDto>> GetCustomerByEmail(string email, CancellationToken cancellationToken); 
    }
}
