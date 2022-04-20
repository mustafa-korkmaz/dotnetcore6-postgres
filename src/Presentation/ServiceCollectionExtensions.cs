using Application;
using Application.Services.Account;
using Application.Services.Order;
using Application.Services.Product;

namespace Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigSections(
           this IServiceCollection services, IConfiguration config)
        {
            //services.Configure<SomeConfig>(
            //    config.GetSection("SomeConfig"));

            return services;
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            //ViewModels to DTOs mappings
            services.AddAutoMapper(typeof(MappingProfile));

            //DTOs to Domain entities mappings
            services.AddAutoMapper(typeof(Application.MappingProfile));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}


