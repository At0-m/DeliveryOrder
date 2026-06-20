import { Link } from 'react-router-dom';

export function NotFoundPage() {
  return (
    <div className="empty-state">
      <h1>Page not found</h1>
      <p>The page you are looking for does not exist.</p>
      <Link className="button button--primary" to="/orders">Back to orders</Link>
    </div>
  );
}
