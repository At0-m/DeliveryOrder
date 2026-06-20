import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getOrders } from '../api/ordersApi';
import { OrderCard } from '../components/OrderCard';
import { PageHeader } from '../components/PageHeader';
import type { DeliveryOrder } from '../types/order';

export function OrdersListPage() {
  const [orders, setOrders] = useState<DeliveryOrder[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let isMounted = true;

    getOrders()
      .then(result => {
        if (isMounted) {
          setOrders(result);
          setError(null);
        }
      })
      .catch(() => {
        if (isMounted) {
          setError('Failed to load orders.');
        }
      })
      .finally(() => {
        if (isMounted) {
          setIsLoading(false);
        }
      });

    return () => {
      isMounted = false;
    };
  }, []);

  return (
    <>
      <PageHeader
        title="Delivery orders"
        description="Create and track delivery requests in one compact workspace."
        action={<Link className="button button--primary" to="/orders/new">Create order</Link>}
      />

      {isLoading && <div className="surface">Loading orders...</div>}
      {error && <div className="alert">{error}</div>}

      {!isLoading && !error && orders.length === 0 && (
        <div className="empty-state">
          <h2>No orders yet</h2>
          <p>Create the first delivery order to see it here.</p>
          <Link className="button button--primary" to="/orders/new">Create order</Link>
        </div>
      )}

      {!isLoading && !error && orders.length > 0 && (
        <section className="orders-grid">
          {orders.map(order => (
            <OrderCard key={order.id} order={order} />
          ))}
        </section>
      )}
    </>
  );
}
