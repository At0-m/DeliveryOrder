using DeliveryOrder.Api.Data;
using DeliveryOrder.Api.Domain.Enums;
using DeliveryOrder.Api.Features.Orders.Contracts;
using DeliveryOrderEntity = DeliveryOrder.Api.Domain.Models.DeliveryOrder;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOrder.Api.Features.Orders;

public sealed class DeliveryOrderService(DeliveryOrderDbContext dbContext, CreateOrderRequestValidator validator,
                                         IOrderNumberGenerator orderNumberGenerator, TimeProvider timeProvider)  : IDeliveryOrderService
{
    public async Task<IReadOnlyList<OrderResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var orders = await dbContext.DeliveryOrders.AsNoTracking().OrderByDescending(order => order.CreatedAtUtc)
                                                   .ThenByDescending(order => order.Id).ToListAsync(cancellationToken);

        return orders.Select(OrderResponse.FromEntity).ToArray();
    }

    public async Task<OrderResponse?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var order = await dbContext.DeliveryOrders.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        return order is null ? null : OrderResponse.FromEntity(order);
    }

    public async Task<CreateOrderResult> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var normalizedRequest = Normalize(request);
        var now = timeProvider.GetUtcNow().UtcDateTime;
        var today = DateOnly.FromDateTime(now);
        var validationResult = validator.Validate(normalizedRequest, today);

        if (!validationResult.IsValid)
        {
            return CreateOrderResult.Failure(validationResult.Errors);
        }

        var order = new DeliveryOrderEntity
        {
            OrderNumber = $"TMP-{Guid.NewGuid():N}".Substring(0, 32),
            SenderCity = normalizedRequest.SenderCity,
            SenderAddress = normalizedRequest.SenderAddress,
            RecipientCity = normalizedRequest.RecipientCity,
            RecipientAddress = normalizedRequest.RecipientAddress,
            CargoWeightKg = normalizedRequest.CargoWeightKg,
            PickupDate = normalizedRequest.PickupDate,
            Status = DeliveryOrderStatus.New,
            CreatedAtUtc = now
        };

        dbContext.DeliveryOrders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        order.OrderNumber = orderNumberGenerator.Generate(order.CreatedAtUtc, order.Id);
        await dbContext.SaveChangesAsync(cancellationToken);

        return CreateOrderResult.Success(OrderResponse.FromEntity(order));
    }

    private static CreateOrderRequest Normalize(CreateOrderRequest request) =>
        request with
        {
            SenderCity = request.SenderCity?.Trim() ?? string.Empty,
            SenderAddress = request.SenderAddress?.Trim() ?? string.Empty,
            RecipientCity = request.RecipientCity?.Trim() ?? string.Empty,
            RecipientAddress = request.RecipientAddress?.Trim() ?? string.Empty
        };
}
