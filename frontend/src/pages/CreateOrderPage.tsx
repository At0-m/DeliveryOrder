import { FormEvent, useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ApiError, createOrder } from '../api/ordersApi';
import { PageHeader } from '../components/PageHeader';
import { TextField } from '../components/TextField';
import type { CreateDeliveryOrderRequest, ValidationErrorBody } from '../types/order';
import { todayInputValue } from '../utils/date';

interface FormState {
  senderCity: string;
  senderAddress: string;
  recipientCity: string;
  recipientAddress: string;
  cargoWeightKg: string;
  pickupDate: string;
}

const initialFormState: FormState = {
  senderCity: '',
  senderAddress: '',
  recipientCity: '',
  recipientAddress: '',
  cargoWeightKg: '',
  pickupDate: todayInputValue()
};

export function CreateOrderPage() {
  const navigate = useNavigate();
  const [form, setForm] = useState<FormState>(initialFormState);
  const [errors, setErrors] = useState<Record<string, string[]>>({});
  const [submitError, setSubmitError] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const today = useMemo(() => todayInputValue(), []);

  function handleChange(name: string, value: string) {
    setForm(current => ({ ...current, [name]: value }));
    setErrors(current => {
      const next = { ...current };
      delete next[name[0].toUpperCase() + name.slice(1)];
      return next;
    });
  }

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setIsSubmitting(true);
    setSubmitError(null);
    setErrors({});

    const request: CreateDeliveryOrderRequest = {
      senderCity: form.senderCity,
      senderAddress: form.senderAddress,
      recipientCity: form.recipientCity,
      recipientAddress: form.recipientAddress,
      cargoWeightKg: Number(form.cargoWeightKg),
      pickupDate: form.pickupDate
    };

    try {
      const order = await createOrder(request);
      navigate(`/orders/${order.id}`);
    } catch (error) {
      if (error instanceof ApiError && error.status === 400 && isValidationErrorBody(error.body)) {
        setErrors(error.body.errors);
      } else {
        setSubmitError('Failed to create order.');
      }
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <>
      <PageHeader
        title="Create order"
        description="Fill in route, cargo weight and pickup date. The order number will be generated automatically."
      />

      <form className="surface form" onSubmit={handleSubmit}>
        {submitError && <div className="alert">{submitError}</div>}

        <div className="form-section">
          <h2>Sender</h2>
          <div className="form-grid">
            <TextField label="Sender city" name="senderCity" value={form.senderCity} error={firstError(errors.SenderCity)} onChange={handleChange} />
            <TextField label="Sender address" name="senderAddress" value={form.senderAddress} error={firstError(errors.SenderAddress)} onChange={handleChange} />
          </div>
        </div>

        <div className="form-section">
          <h2>Recipient</h2>
          <div className="form-grid">
            <TextField label="Recipient city" name="recipientCity" value={form.recipientCity} error={firstError(errors.RecipientCity)} onChange={handleChange} />
            <TextField label="Recipient address" name="recipientAddress" value={form.recipientAddress} error={firstError(errors.RecipientAddress)} onChange={handleChange} />
          </div>
        </div>

        <div className="form-section">
          <h2>Cargo</h2>
          <div className="form-grid">
            <TextField label="Weight, kg" name="cargoWeightKg" value={form.cargoWeightKg} type="number" min="0.001" step="0.001" error={firstError(errors.CargoWeightKg)} onChange={handleChange} />
            <TextField label="Pickup date" name="pickupDate" value={form.pickupDate} type="date" min={today} error={firstError(errors.PickupDate)} onChange={handleChange} />
          </div>
        </div>

        <div className="form-actions">
          <button className="button button--ghost" type="button" onClick={() => navigate('/orders')}>Cancel</button>
          <button className="button button--primary" type="submit" disabled={isSubmitting}>{isSubmitting ? 'Creating...' : 'Create order'}</button>
        </div>
      </form>
    </>
  );
}

function firstError(errors?: string[]): string | undefined {
  return errors?.[0];
}

function isValidationErrorBody(body: unknown): body is ValidationErrorBody {
  return typeof body === 'object' && body !== null && 'errors' in body;
}
