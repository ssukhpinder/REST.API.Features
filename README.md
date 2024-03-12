# REST.API.Features

## 1. Common Response Model
All the API methods will adhere to the common response model structure. All features/controller methods defined in the above repository have followed the best practice.
### Example
```
/// <summary>
/// Common wrapper class for all API response models where T can be a simple or complex data type
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResponseMetaData<T>
{
    public HttpStatusCode Status { get; set; }
    public string? Message { get; set; }
    public bool IsError { get; set; }
    public string? ErrorDetails { get; set; }
    public string? CorrealtionId { get; set; }
    public T? Result { get; set; }
}
```

### 1.1 Return the model from the controller

```
public IActionResult Get()
{
    var responseMetadata = new ResponseMetaData<string>()
    {
        Status = System.Net.HttpStatusCode.OK,
        IsError = false,
        Message = CommonApiConstants.Responses.ResponseV1
    };
    return StatusCode((int)responseMetadata.Status, responseMetadata);
}
```

## 2. Exception Handling
### 2.1 Global Exception
A global middleware that gets activated whenever an exception occurs within the application.
- The middleware will also honour the common response model in case of exception
- Finally, the JSON response will be returned.

### Example
```
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            ResponseMetaData<string> responseMetadata = new()
            {
                Status = HttpStatusCode.InternalServerError,
                IsError = true,
                ErrorDetails = CommonExceptionConstants.SomeUnknownError
            };

            var serializedResponseMetadata = JsonConvert.SerializeObject(responseMetadata);
            _logger.LogError(exception, "Exception occurred: {Message}", JsonConvert.SerializeObject(serializedResponseMetadata));

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(serializedResponseMetadata);
        }
    }

}
```

### 2.2 Custom Exception
The first step is to create a CustomApiException as follows
```
public class CustomApiException : Exception
{
    private readonly HttpStatusCode _statusCode;
    private readonly string? _exceptionStrckTrace;

    public HttpStatusCode StatusCode => _statusCode;
    public string? ExceptionStackTrace => _exceptionStrckTrace;

    public CustomApiException() { }
    public CustomApiException(string message) : base(message) { }
    public CustomApiException(string message, Exception ex) : base($"{CommonExceptionConstants.CustomExceptionPrefix} {message}", ex) { }
    public CustomApiException(HttpStatusCode statusCode, string message) : base($"{CommonExceptionConstants.CustomExceptionPrefix} {message}")
    {
        _statusCode = statusCode;
    }
    public CustomApiException(HttpStatusCode statusCode, Exception ex) : base($"{CommonExceptionConstants.CustomExceptionPrefix} {ex.Message}")
    {
        _statusCode = statusCode;
        _exceptionStrckTrace = ex.StackTrace;
    }
    public CustomApiException(HttpStatusCode statusCode, string message, Exception ex) : base($"{CommonExceptionConstants.CustomExceptionPrefix} {message}")
    {
        _statusCode = statusCode;
        _exceptionStrckTrace = ex.StackTrace;
    }
}
```
The CustomApiException class is a custom exception type that inherits from the base Exception class. It includes properties and constructors to handle and propagate exceptions in a customized manner within an API application. 

A custom middleware that gets activated whenever an exception or **Custom Exception** occurs within the application. 
>Note: A custom exception middleware is beneficial in scenarios where we need to customize the custom status codes, message, stack trace, and other properties.
- The middleware will also honour the common response model in case of any exception
- Finally, the JSON response will be returned.

### Example
```
public class CustomExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandler> _logger;

    public CustomExceptionHandler(
        RequestDelegate next,
        ILogger<CustomExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {

            ResponseMetaData<string> responseMetadata = new()
            {
                Status = HttpStatusCode.InternalServerError,
                IsError = true,
                ErrorDetails = CommonExceptionConstants.SomeUnknownError
            };

            if (exception is CustomApiException)
            {
                responseMetadata.ErrorDetails = CommonExceptionConstants.CustomSomeUnknownError;
            }
            else
            {
                responseMetadata.ErrorDetails = CommonExceptionConstants.SomeUnknownError;
            }

            var serializedResponseMetadata = JsonConvert.SerializeObject(responseMetadata);
            _logger.LogError(exception, "Exception occurred: {Message}", JsonConvert.SerializeObject(serializedResponseMetadata));

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(serializedResponseMetadata);
        }
    }
}
```

## 3. Versioning
Let's start by installing two NuGet packages that we'll need to implement API versioning:

- Asp.Versioning.Http
- Asp.Versioning.Mvc.ApiExplorer

### 3.1 Configuration
Now that the API Versioning package has been installed onto the project, the next step is to configure it. The following code shows how you can configure API Versioning in ASP.NET Core:
```
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
```

### 3.2 Registration
The above extension method will be used as follows in the Program.cs class
```
// Add versioning
builder.Services.UseAppVersioningHandler();
```

### 3.3 Update Folder Structure
The Controller folder structure breakdown is as follows:
- **REST.API.Features**
  - **Controllers**
    - **v1**
      - VersionController
    - **v2**
      - VersionController

### 3.4 Add Version Attributes
Finally, the controller methods are structured with the following attributes:

```
/// <summary>
/// Controller class contains API methods to demonstrate versioning 
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]")]
```
