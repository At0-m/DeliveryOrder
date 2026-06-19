using DeliveryOrder.Api.Features.Orders.Contracts;
using DeliveryOrder.Api.Shared.Validation;

namespace DeliveryOrder.Api.Features.Orders;

public sealed class CreateOrderRequestValidator
{
    public ValidationResult Validate(CreateOrderRequest request, DateOnly today)
    {
        var errors = new Dictionary<string, List<string>>();

        AddRequiredString(errors, nameof(request.SenderCity), request.SenderCity, 100);
        AddRequiredString(errors, nameof(request.SenderAddress), request.SenderAddress, 300);
        AddRequiredString(errors, nameof(request.RecipientCity), request.RecipientCity, 100);
        AddRequiredString(errors, nameof(request.RecipientAddress), request.RecipientAddress, 300);

        if (request.CargoWeightKg <= 0)
        {
            AddError(errors, nameof(request.CargoWeightKg), "Cargo weight must be greater than zero.");
        }

        if (request.PickupDate < today)
        {
            AddError(errors, nameof(request.PickupDate), "Pickup date cannot be in the past.");
        }

        return new ValidationResult(errors.ToDictionary(pair => pair.Key, pair => pair.Value.ToArray()));
    }

    private static void AddRequiredString(Dictionary<string, List<string>> errors, string fieldName,
                                          string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            AddError(errors, fieldName, "Field is required.");
            return;
        }

        if (value.Trim().Length > maxLength)
        {
            AddError(errors, fieldName, $"Field length must be less than or equal to {maxLength}.");
        }
    }

    private static void AddError(Dictionary<string, List<string>> errors, string fieldName, string message)
    {
        if (!errors.TryGetValue(fieldName, out var messages))
        {
            messages = [];
            errors[fieldName] = messages;
        }

        messages.Add(message);
    }
}
