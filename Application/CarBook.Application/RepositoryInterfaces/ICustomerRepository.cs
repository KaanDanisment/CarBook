using CarBook.Application.Dtos.CustomerDtos;
using CarBook.Application.RepositoryInterfaces.GenericRepository;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.RepositoryInterfaces
{
    public interface ICustomerRepository: IGenericRepository<Customer>
    {
        Task<Customer> CreateCustomer(CreateCustomerDto createCustomerDto, CancellationToken cancellationToken);
    }
}
