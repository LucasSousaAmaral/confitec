namespace Api.Middlewares;

public static class MiddlewareExtensions
{
    #region Methods

    public static IApplicationBuilder UseErrorHandlingMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }

    #endregion Methods
}