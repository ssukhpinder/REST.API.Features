using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using REST.API.ModelValidation.Filters;

namespace REST.API.ModelValidation.Extensions
{
    public static class ModelValidationExtension
    {
        public static IServiceCollection UseModelValidationHandler(this IServiceCollection services)
        {

            #region Model Validation

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelHelperAttribute));
            });

            #endregion

            return services;
        }
    }
}
