using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Dtos.CustomerDtos;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Services.Abstract;
using CarBook.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<IDataResult<CustomerDto>> CreateCustomer(CreateCustomerDto createCustomerDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                Customer customer = await _customerRepository.CreateCustomer(createCustomerDto, cancellationToken);

                return new SuccessDataResult<CustomerDto>(new CustomerDto
                {
                    Id = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer");
                return new ErrorDataResult<CustomerDto>(ex.Message, "SystemError");
            }
        }

        public async Task<IDataResult<CustomerDto>> GetCustomerByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Customer customer = await _customerRepository.GetAsync(cancellationToken, customer => customer.Email == email);
                if (customer == null)
                {
                    return new SuccessDataResult<CustomerDto>(null, "Customer not found");
                }
                return new SuccessDataResult<CustomerDto>(new CustomerDto
                {
                    Id = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer by email");
                return new ErrorDataResult<CustomerDto>(ex.Message, "SystemError");
            }
        }
    }
}
