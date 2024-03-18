using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using REST.API.EFCore.Context;
using REST.API.EFCore.Interface;
using REST.API.EFCore.Services;

namespace REST.API.EFCore.Extensions
{
    public static class EfCoreDbExtension
    {
        public static IServiceCollection UseEfCoreDbHandler(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EFCoreDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ICommonService, CommonService>();

            return services;
        }
    }
}
