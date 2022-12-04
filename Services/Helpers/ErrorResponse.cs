using FluentValidation.Results;
using Newtonsoft.Json;

namespace Services.Helpers;

public record ErrorResponse
{
    #region Properties

    [JsonProperty(Order = 1)] public bool IsSuccess { get; init; }
    [JsonProperty(Order = 2)] public string? Message { get; init; }

    #endregion Properties
}

public record ValidationErrorResponse : ErrorResponse
{
    #region Properties

    [JsonProperty(Order = 3)] public string? TypeName { get; init; }
    [JsonProperty(Order = 4)] public List<ValidationFailure> ValidationFailures { get; init; } = new();

    #endregion Properties
}