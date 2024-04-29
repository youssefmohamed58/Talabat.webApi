using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Apis.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.Apis.Extensions
{
    public static class AddApplicationServices
    {
       public static IServiceCollection AddServices(this IServiceCollection services)
       {
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            services.AddSingleton(typeof(IResponseCacheService), typeof(ResponseCacheService));
            services.AddAutoMapper(typeof(MappingProfile));
            return services;

        }
    }
}
