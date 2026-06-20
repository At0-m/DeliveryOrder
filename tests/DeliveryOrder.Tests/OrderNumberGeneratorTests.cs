using DeliveryOrder.Api.Features.Orders;
using Xunit;

namespace DeliveryOrder.Tests;

public sealed class OrderNumberGeneratorTests
{
    [Fact]
    public void Generate_ReturnsStableReadableNumber()
    {
        var generator = new OrderNumberGenerator();

        var result = generator.Generate(new DateTime(2026, 6, 18, 12, 30, 0, DateTimeKind.Utc), 42);

        Assert.Equal("DO-20260618-00042", result);
    }
}
