import { LoginComponent } from './features/auth/login/login.component';
import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

export const routes: Routes = [
  // Main Layout (Authenticated Routes)
  {
    path: '',
    component: MainLayoutComponent, // Uses main layout
    children: [
      {
        path: 'home',
        loadComponent: () =>
          import('./features/home/home.component').then((m) => m.HomeComponent),
      },
      // {
      //   path: 'products',
      //   loadComponent: () =>
      //     import('./features/products/product-list.component'),
      // },
      // {
      //   path: 'cart',
      //   loadComponent: () => import('./features/cart/cart.component'),
      // },
    ],
    // canActivate: [authGuard], // Protects all child routes
  },

  // Auth Layout (Public Routes)
  {
    path: 'auth',
    component: AuthLayoutComponent, // Uses auth layout
    children: [
      {
        path: 'login',
        loadComponent: () =>
          import('./features/auth/login/login.component').then(
            (m) => m.LoginComponent
          ),
      },
      // {
      //   path: 'register',
      //   //  loadComponent: () => import('./features/auth/register.component'),
      // },
    ],
  },

  // Redirects
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products' },
];
