namespace DeliveryOrder.Api.Features.Orders;

public interface IOrderNumberGenerator
{
    string Generate(DateTime createdAtUtc, long id);
}
