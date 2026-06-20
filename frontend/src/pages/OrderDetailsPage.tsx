import { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import { getOrder } from '../api/ordersApi';
import { PageHeader } from '../components/PageHeader';
import type { DeliveryOrder } from '../types/order';
import { formatDate, formatDateTime } from '../utils/date';

export function OrderDetailsPage() {
  const { id } = useParams();
  const [order, setOrder] = useState<DeliveryOrder | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [copied, setCopied] = useState(false);

  useEffect(() => {
    let isMounted = true;

    if (!id) {
      setError('Order id is missing.');
      setIsLoading(false);
      return;
    }

    getOrder(id)
      .then(result => {
        if (isMounted) {
          setOrder(result);
          setError(null);
        }
      })
      .catch(() => {
        if (isMounted) {
          setError('Order was not found.');
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
  }, [id]);

  async function copyOrderNumber() {
    if (!order) {
      return;
    }

    await navigator.clipboard.writeText(order.orderNumber);
    setCopied(true);
    setTimeout(() => setCopied(false), 1600);
  }

  if (isLoading) {
    return <div className="surface">Loading order...</div>;
  }

  if (error || !order) {
    return (
      <div className="empty-state">
        <h1>{error ?? 'Order was not found.'}</h1>
        <Link className="button button--primary" to="/orders">Back to orders</Link>
      </div>
    );
  }

  return (
    <>
      <PageHeader
        title={order.orderNumber}
        description="Read-only delivery order card with route and cargo information."
        action={<button className="button button--ghost" type="button" onClick={copyOrderNumber}>{copied ? 'Copied' : 'Copy number'}</button>}
      />

      <section className="details-layout">
        <div className="surface details-card details-card--wide">
          <h2>Route</h2>
          <div className="route route--large">
            <div>
              <span>From</span>
              <strong>{order.senderCity}</strong>
              <p>{order.senderAddress}</p>
            </div>
            <div className="route-line">to</div>
            <div>
              <span>To</span>
              <strong>{order.recipientCity}</strong>
              <p>{order.recipientAddress}</p>
            </div>
          </div>
        </div>

        <div className="surface details-card">
          <h2>Cargo</h2>
          <dl>
            <div>
              <dt>Weight</dt>
              <dd>{order.cargoWeightKg} kg</dd>
            </div>
            <div>
              <dt>Pickup date</dt>
              <dd>{formatDate(order.pickupDate)}</dd>
            </div>
          </dl>
        </div>

        <div className="surface details-card">
          <h2>System</h2>
          <dl>
            <div>
              <dt>Status</dt>
              <dd><span className="status-pill">{order.status}</span></dd>
            </div>
            <div>
              <dt>Created</dt>
              <dd>{formatDateTime(order.createdAtUtc)}</dd>
            </div>
          </dl>
        </div>
      </section>

      <div className="page-actions">
        <Link className="button button--ghost" to="/orders">Back to orders</Link>
      </div>
    </>
  );
}
