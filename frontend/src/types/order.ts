export interface DeliveryOrder {
  id: number;
  orderNumber: string;
  senderCity: string;
  senderAddress: string;
  recipientCity: string;
  recipientAddress: string;
  cargoWeightKg: number;
  pickupDate: string;
  status: string;
  createdAtUtc: string;
}

export interface CreateDeliveryOrderRequest {
  senderCity: string;
  senderAddress: string;
  recipientCity: string;
  recipientAddress: string;
  cargoWeightKg: number;
  pickupDate: string;
}

export interface ValidationErrorBody {
  errors: Record<string, string[]>;
}
