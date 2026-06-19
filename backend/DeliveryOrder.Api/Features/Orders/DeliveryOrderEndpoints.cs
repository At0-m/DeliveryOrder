using DeliveryOrder.Api.Features.Orders.Contracts;
using DeliveryOrder.Api.Shared.Api;

namespace DeliveryOrder.Api.Features.Orders;

public static class DeliveryOrderEndpoints
{
    public static IEndpointRouteBuilder MapDeliveryOrderEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/orders").WithTags("Orders");

        group.MapGet("", async (IDeliveryOrderService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetAllAsync(cancellationToken))).WithName("GetOrders");

        group.MapGet("/{id:long}", async (long id, IDeliveryOrderService service, CancellationToken cancellationToken) =>
        {
            var order = await service.GetByIdAsync(id, cancellationToken);

            return order is null ? Results.NotFound(new ApiErrorResponse("OrderNotFound", "Order was not found."))
                                 : Results.Ok(order);
        }).WithName("GetOrderById");

        group.MapPost("", async (CreateOrderRequest request, IDeliveryOrderService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateAsync(request, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(new ValidationErrorResponse(result.Errors));
            }

            return Results.Created($"/api/orders/{result.Order!.Id}", result.Order);
        }).WithName("CreateOrder");

        return endpoints;
    }
}
