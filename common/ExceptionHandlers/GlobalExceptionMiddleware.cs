using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.ExceptionHandlers;

public class GlobalExceptionMiddleware: IMiddleware
{

    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionMiddleware(IProblemDetailsService problemDetailsService)
    {

        _problemDetailsService = problemDetailsService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                }
            });
            
            return;
        }
    }
}