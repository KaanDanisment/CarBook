using CarBook.Application.Common.Behaviors;
using CarBook.Application.Common.Security;
using CarBook.Application.Services.Abstract;
using CarBook.Application.Services.Concrete;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // MeidatR configuration
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            // Services
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<ITokenHelper, JwtHelper>();
        }
    }
}
