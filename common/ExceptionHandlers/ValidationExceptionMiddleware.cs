using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.ExceptionHandlers;

public class ValidationExceptionMiddleware: IMiddleware
{
  
    private readonly IProblemDetailsService _problemDetailsService;

    public ValidationExceptionMiddleware(IProblemDetailsService problemDetailsService)
    {
       
        _problemDetailsService = problemDetailsService;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            var problemContext = new ProblemDetailsContext
            {
                HttpContext = context,
                Exception = validationException,
                ProblemDetails = new ProblemDetails
                {
                    Detail = "one or more validation errors occurred",
                    Status = StatusCodes.Status400BadRequest,
                }
            };
            
            await _problemDetailsService.TryWriteAsync(problemContext);
            return;
        }
        
    }
}