import type { CreateDeliveryOrderRequest, DeliveryOrder } from '../types/order';

const apiBaseUrl = import.meta.env.VITE_API_URL ?? '/api';

export async function getOrders(): Promise<DeliveryOrder[]> {
  const response = await fetch(`${apiBaseUrl}/orders`);
  return handleResponse<DeliveryOrder[]>(response);
}

export async function getOrder(id: string): Promise<DeliveryOrder> {
  const response = await fetch(`${apiBaseUrl}/orders/${id}`);
  return handleResponse<DeliveryOrder>(response);
}

export async function createOrder(request: CreateDeliveryOrderRequest): Promise<DeliveryOrder> {
  const response = await fetch(`${apiBaseUrl}/orders`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(request)
  });

  return handleResponse<DeliveryOrder>(response);
}

async function handleResponse<T>(response: Response): Promise<T> {
  if (response.ok) {
    return response.json() as Promise<T>;
  }

  const body = await readBody(response);
  throw new ApiError(response.status, body);
}

async function readBody(response: Response): Promise<unknown> {
  const text = await response.text();

  if (!text) {
    return null;
  }

  try {
    return JSON.parse(text) as unknown;
  } catch {
    return text;
  }
}

export class ApiError extends Error {
  constructor(
    public readonly status: number,
    public readonly body: unknown
  ) {
    super('Request failed');
  }
}
