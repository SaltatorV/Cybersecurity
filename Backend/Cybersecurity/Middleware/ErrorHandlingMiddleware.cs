﻿using Cybersecurity.Exceptions;

namespace Cybersecurity.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                _logger.LogError(badRequestException, badRequestException.Message);

                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (ForbiddenException forbiddenException)
            {
                _logger.LogError(forbiddenException, forbiddenException.Message);

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbiddenException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, notFoundException.Message);

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
