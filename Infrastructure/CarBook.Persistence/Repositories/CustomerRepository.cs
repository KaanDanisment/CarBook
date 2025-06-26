using CarBook.Application.Dtos.CustomerDtos;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Domain.Entities;
using CarBook.Persistence.Context;
using CarBook.Persistence.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly CarBookContext _context;
        public CustomerRepository(CarBookContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomer(CreateCustomerDto createCustomerDto, CancellationToken cancellationToken)
        {
            Customer customer = new Customer
            {
                Name = createCustomerDto.Name,
                Email = createCustomerDto.Email,
                PhoneNumber = createCustomerDto.PhoneNumber
            };
            await CreateAsync(cancellationToken, customer);
            return customer;
        }
    }
}
