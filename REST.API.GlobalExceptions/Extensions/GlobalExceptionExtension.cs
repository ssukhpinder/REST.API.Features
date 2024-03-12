using Microsoft.AspNetCore.Builder;
using REST.API.GlobalExceptions.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST.API.GlobalExceptions.Extensions
{
    public static class GlobalExceptionExtension
    {
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
