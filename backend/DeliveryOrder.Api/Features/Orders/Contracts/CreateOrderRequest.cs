namespace DeliveryOrder.Api.Features.Orders.Contracts;

public sealed record CreateOrderRequest(string SenderCity, string SenderAddress, string RecipientCity,
                                        string RecipientAddress, decimal CargoWeightKg, DateOnly PickupDate);
