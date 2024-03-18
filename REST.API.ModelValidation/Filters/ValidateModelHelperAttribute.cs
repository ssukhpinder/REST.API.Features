
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using REST.API.ModelValidation.Constants;
using System.Net;

namespace REST.API.ModelValidation.Filters
{
    public class ValidateModelHelperAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errors = new();
                foreach (var modelState in context.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new ObjectResult(
                    new ResponseMetaData<string>()
                    {
                        Status = System.Net.HttpStatusCode.BadRequest,
                        IsError = true,
                        ErrorDetails = CommonValidationConstants.ValidationFailed,
                        Message = $"[{string.Join(" , ", [.. errors])}]",
                        Result = null
                    }
                );
                base.OnActionExecuting(context);
            }
        }
    }
}
