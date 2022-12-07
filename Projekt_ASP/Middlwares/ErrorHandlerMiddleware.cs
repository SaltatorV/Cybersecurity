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
            catch(System.Exception)
            {
                httpContext.Response.StatusCode=StatusCodes.Status400BadRequest;
            }
        }
    }
}
