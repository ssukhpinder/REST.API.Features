using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using REST.API.Features.Extensions;
using REST.API.GlobalExceptions.Extensions;
using REST.API.GlobalExceptions.Middlewares;
using REST.API.Versioning.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Change URL names to lower case
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add versioning
builder.Services.UseAppVersioningHandler();

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
