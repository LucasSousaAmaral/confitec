using Domain.Exceptions;
using FluentValidation;
using MediatR;
using System.Net;

namespace Services.Behaviors;

public sealed class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    #region Properties

    private readonly IValidator<TRequest>[] _validators;

    #endregion Properties

    #region Constructors

    public ValidatorBehavior(IValidator<TRequest>[] validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    #endregion Constructors

    #region Methods
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = _validators.Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();
        if (failures.Any())
            throw new UsersValidationException(failures, request.GetType().Name, HttpStatusCode.BadRequest);
        return await next();
    }

    #endregion Methods
}