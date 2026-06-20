using DeliveryOrder.Api.Features.Orders;
using DeliveryOrder.Api.Features.Orders.Contracts;
using Xunit;

namespace DeliveryOrder.Tests;

public sealed class CreateOrderRequestValidatorTests
{
    private readonly CreateOrderRequestValidator _validator = new();

    [Fact]
    public void Validate_ReturnsError_WhenWeightIsZero()
    {
        var request = CreateValidRequest() with { CargoWeightKg = 0 };

        var result = _validator.Validate(request, new DateOnly(2026, 6, 18));

        Assert.False(result.IsValid);
        Assert.Contains(nameof(request.CargoWeightKg), result.Errors.Keys);
    }

    [Fact]
    public void Validate_ReturnsError_WhenPickupDateIsInPast()
    {
        var request = CreateValidRequest() with { PickupDate = new DateOnly(2026, 6, 17) };

        var result = _validator.Validate(request, new DateOnly(2026, 6, 18));

        Assert.False(result.IsValid);
        Assert.Contains(nameof(request.PickupDate), result.Errors.Keys);
    }

    [Fact]
    public void Validate_ReturnsError_WhenSenderCityIsEmpty()
    {
        var request = CreateValidRequest() with { SenderCity = " " };

        var result = _validator.Validate(request, new DateOnly(2026, 6, 18));

        Assert.False(result.IsValid);
        Assert.Contains(nameof(request.SenderCity), result.Errors.Keys);
    }

    [Fact]
    public void Validate_ReturnsSuccess_ForValidRequest()
    {
        var request = CreateValidRequest();

        var result = _validator.Validate(request, new DateOnly(2026, 6, 18));

        Assert.True(result.IsValid);
    }

    private static CreateOrderRequest CreateValidRequest() =>
        new("Saint Petersburg", "Nevsky Prospect 10", "Moscow", "Tverskaya Street 5", 10.5m, new DateOnly(2026, 6, 19));
}
