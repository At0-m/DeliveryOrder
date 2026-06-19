namespace DeliveryOrder.Api.Shared.Validation;

public sealed record ValidationResult(IReadOnlyDictionary<string, string[]> Errors)
{
    public bool IsValid => Errors.Count == 0;
}
