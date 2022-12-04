using System.Net;
using Domain.Exceptions;
using Newtonsoft.Json;
using Services.Helpers;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    #region Properties

    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    #endregion Properties

    #region Constructors

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion Constructors

    #region Methods

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (UsersValidationException usersValidationException)
        {
            await HandleUsersValidationExceptionAsync(context, usersValidationException);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleUsersValidationExceptionAsync(HttpContext context,
        UsersValidationException usersValidationException)
    {
        var contentType = "application/json";
        var statusCode = (int)usersValidationException.StatusCode;

        var errorResponse = JsonConvert.SerializeObject(new ValidationErrorResponse
        {
            IsSuccess = false,
            Message = usersValidationException.Message,
            TypeName = usersValidationException.TypeName,
            ValidationFailures = usersValidationException.ValidationFailures
        });

        _logger.LogError($"Confitec.Api --- UsersValidationException Message: {errorResponse}");

        context.Response.ContentType = contentType;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(errorResponse);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var contentType = "application/json";
        var statusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = JsonConvert.SerializeObject(new ErrorResponse
        {
            IsSuccess = false,
            Message = exception.InnerException is not null ? exception?.InnerException?.Message : exception?.Message
        });

        _logger.LogError($"Confitec.Api --- Exception Message: {errorResponse}");

        context.Response.ContentType = contentType;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(errorResponse);
    }

    #endregion Methods
}