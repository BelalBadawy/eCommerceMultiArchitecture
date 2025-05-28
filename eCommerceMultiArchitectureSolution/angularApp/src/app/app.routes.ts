import { CategoryListComponent } from './features/categories/category-list/category-list.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
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
      {
        path: 'categories',
        loadComponent: () =>
          import(
            './features/categories/category-list/category-list.component'
          ).then((m) => m.CategoryListComponent),
      },
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
      {
        path: 'register',
        loadComponent: () =>
          import('./features/auth/register/register.component').then(
            (m) => m.RegisterComponent
          ),
      },
      {
        path: 'forgot-password',
        loadComponent: () =>
          import(
            './features/auth/forgot-password/forgot-password.component'
          ).then((m) => m.ForgotPasswordComponent),
      },
      {
        path: 'confirm-email',
        loadComponent: () =>
          import('./features/auth/confirm-email/confirm-email.component').then(
            (c) => c.ConfirmEmailComponent
          ),
      },

      {
        path: 'reset-password',
        loadComponent: () =>
          import(
            './features/auth/reset-password/reset-password.component'
          ).then((c) => c.ResetPasswordComponent),
      },
    ],
  },

  // Redirects
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products' },
];
