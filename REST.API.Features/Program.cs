using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using REST.API.Features.Extensions;
using REST.API.GlobalExceptions.Extensions;
using REST.API.ModelValidation.Extensions;
using REST.API.GlobalExceptions.Middlewares;
using REST.API.Versioning.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    string xmlFIle = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFIle));
});

//Change URL names to lower case
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add versioning
builder.Services.UseAppVersioningHandler();

// Add model validation
builder.Services.UseModelValidationHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Add swagger UI via extension handler
    app.UseSwaggerHandler();
}

// Global Exception Middleware
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
