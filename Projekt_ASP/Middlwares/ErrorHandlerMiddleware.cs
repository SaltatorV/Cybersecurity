using Projekt_ASP.Extensions;
using System.Globalization;

namespace Projekt_ASP.Middlwares
{
    public class ErrorHandlerMiddleware 
    {
        public readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

       public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ProductsAlreadyExistsException e)
            {
                httpContext.Response.StatusCode = StatusCodes.Status402PaymentRequired;
            }
            catch(System.Exception)
            {
                httpContext.Response.StatusCode=StatusCodes.Status400BadRequest;
            }
        }
    }
}
