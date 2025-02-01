using CleanArch.Application.Exceptions;
using CleanArch.Application.Wrappers;

namespace CleanArch.API.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public CustomExceptionMiddleware(RequestDelegate requestDelegate)
        {
            next = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                var response = new Response();
                switch (e)
                {
                    case ValidationException ex:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        response.Errors = ex.Errors;
                        break;                   
                    default:
                        Console.WriteLine(e.InnerException);
                        Console.WriteLine(e.Message);

                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        response.Message = "Xeta bas verdi";
                        break;
                }
                await context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
