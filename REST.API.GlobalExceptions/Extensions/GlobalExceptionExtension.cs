using Microsoft.AspNetCore.Builder;
using REST.API.GlobalExceptions.Middlewares;

namespace REST.API.GlobalExceptions.Extensions
{
    public static class GlobalExceptionExtension
    {
        /// <summary>
        /// Extension method to handle exceptions globally
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            #region Global Exception Middleware

            // Global middleware which will be invoked one any exception is triggered within the application
            app.UseMiddleware<GlobalExceptionHandler>();

            /* 
             * Global middleware which will be invoked one any exception is triggered or custom exception is thrown within the application
             * Use if application have requirement for custom exception 
             */
            //app.UseMiddleware<CustomExceptionHandler>();

            #endregion
            return app;
        }
    }

}
