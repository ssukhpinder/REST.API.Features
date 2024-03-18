using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace REST.API.Versioning.Extensions
{
    public static class VersioningExtension
    {
        /// <summary>
        /// Extension method for versioning
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseAppVersioningHandler(this IServiceCollection services)
        {
            #region Versioning 

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader());

                /* 
                 * If you want to add both options i.e. URL versioning as well as header versioning.
                 * If attribute with version is supplied on controller then URL versioning will be applicable on Swqagger
                 * Else X-Api-Version will be added as a header attribute to the remaining controller methods
                 */

                /* Uncomment this code block
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
                */
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            #endregion

            return services;
        }
    }
}
