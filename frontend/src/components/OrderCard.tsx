import { Link } from 'react-router-dom';
import type { DeliveryOrder } from '../types/order';
import { formatDate } from '../utils/date';

interface OrderCardProps {
  order: DeliveryOrder;
}

export function OrderCard({ order }: OrderCardProps) {
  return (
    <Link className="order-card" to={`/orders/${order.id}`}>
      <div className="order-card__top">
        <span className="order-card__number">{order.orderNumber}</span>
        <span className="status-pill">{order.status}</span>
      </div>
      <div className="route">
        <span>{order.senderCity}</span>
        <span className="route__arrow">to</span>
        <span>{order.recipientCity}</span>
      </div>
      <div className="order-card__meta">
        <span>{order.cargoWeightKg} kg</span>
        <span>Pickup {formatDate(order.pickupDate)}</span>
      </div>
    </Link>
  );
}
