using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.RepositoryInterfaces.GenericRepository;
using CarBook.Application.Services.Abstract;
using CarBook.Persistence.Context;
using CarBook.Persistence.Repositories;
using CarBook.Persistence.Repositories.GenericRepository;
using CarBook.Persistence.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddScoped<CarBookContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICarDescriptionRepository, CarDescriptionRepository>();
            services.AddScoped<ICarFeatureRepository, CarFeatureRepository>();
            services.AddScoped<ICarPricingRepository, CarPricingRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IFooterAddressRepository, FooterAddressRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IPricingRepository, PricingRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddScoped<ITagCloudRepository, TagCloudRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAppRoleRepository, AppRoleRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
        }
    }
}
