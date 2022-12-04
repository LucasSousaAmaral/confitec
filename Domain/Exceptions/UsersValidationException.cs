using System.Net;
using System.Runtime.Serialization;
using FluentValidation.Results;

namespace Domain.Exceptions;

[Serializable]
public class UsersValidationException : Exception, ISerializable
{
    #region Properties

    public List<ValidationFailure> ValidationFailures { get; init; } = new();
    public string? TypeName { get; init; }
    public HttpStatusCode StatusCode { get; init; }

    #endregion Properties

    #region Constructors

    public UsersValidationException(List<ValidationFailure> validationFailures, string? typeName = null,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = "Validation Failure.")
        : base(message)
    {
        ValidationFailures = validationFailures;
        TypeName = typeName;
        StatusCode = statusCode;
    }

    public UsersValidationException(List<ValidationFailure> validationFailures, Exception inner,
        string? typeName = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        string message = "Validation Failure.")
        : base(message, inner)
    {
        ValidationFailures = validationFailures;
        TypeName = typeName;
        StatusCode = statusCode;
    }

    protected UsersValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    #endregion Constructors

    #region Methods

    public override void GetObjectData(SerializationInfo info,
        StreamingContext context)
    {
        base.GetObjectData(info, context);
    }

    #endregion Methods
}