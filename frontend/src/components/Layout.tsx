import { Link, NavLink } from 'react-router-dom';
import type { ReactNode } from 'react';

interface LayoutProps {
  children: ReactNode;
}

export function Layout({ children }: LayoutProps) {
  return (
    <div className="app-shell">
      <header className="header">
        <Link to="/orders" className="brand">
          <span className="brand-mark">DO</span>
          <span>DeliveryOrder</span>
        </Link>
        <nav className="nav">
          <NavLink to="/orders">Orders</NavLink>
          <NavLink to="/orders/new">Create order</NavLink>
        </nav>
      </header>
      <main className="main">{children}</main>
    </div>
  );
}
