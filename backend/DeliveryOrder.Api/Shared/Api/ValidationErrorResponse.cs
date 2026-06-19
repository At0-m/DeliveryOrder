namespace DeliveryOrder.Api.Shared.Api;

public sealed record ValidationErrorResponse(IReadOnlyDictionary<string, string[]> Errors);
