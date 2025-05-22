// src/app/core/services/auth.service.ts
import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';

// Types
interface User {
  id: string;
  userName: string;
  email: string;
  roles?: string[];
}

interface AuthState {
  user: User | null;
  token: string | null;
  refreshToken: string | null;
  refreshTokenExpiration: Date | null;
  isAuthenticated: boolean;
}

export interface AuthenticationResponse {
  id: string;
  userName: string;
  email: string;
  token: string;
  refreshToken: string;
  refreshTokenExpiration: Date;
}

export interface MyAppResponse<T> {
  statusCode: number;
  succeeded: boolean;
  message?: string;
  errors?: string[];
  data?: T;
  redirectTo?: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  // State management with signals
  private _state = signal<AuthState>({
    user: null,
    token: null,
    refreshToken: null,
    refreshTokenExpiration: null,
    isAuthenticated: false,
  });

  // Computed signals
  user = computed(() => this._state().user);
  token = computed(() => this._state().token);
  refreshToken = computed(() => this._state().refreshToken);
  refreshTokenExpiration = computed(() => this._state().refreshTokenExpiration);
  isAuthenticated = computed(() => this._state().isAuthenticated);

  constructor() {
    this.initializeFromStorage();
  }

  private initializeFromStorage(): void {
    const token = localStorage.getItem('access_token');
    const refreshToken = localStorage.getItem('refresh_token');
    const refreshTokenExpiration = localStorage.getItem(
      'refresh_token_expiration'
    );
    const user = localStorage.getItem('user');

    if (token && user) {
      this._state.update((state) => ({
        ...state,
        token,
        refreshToken: refreshToken || null,
        refreshTokenExpiration: refreshTokenExpiration
          ? new Date(refreshTokenExpiration)
          : null,
        user: JSON.parse(user),
        isAuthenticated: true,
      }));
    }
  }

  private updateState(newState: Partial<AuthState>): void {
    this._state.update((state) => ({ ...state, ...newState }));

    if (newState.token) localStorage.setItem('access_token', newState.token);
    if (newState.refreshToken)
      localStorage.setItem('refresh_token', newState.refreshToken);
    if (newState.refreshTokenExpiration)
      localStorage.setItem(
        'refresh_token_expiration',
        newState.refreshTokenExpiration.toISOString()
      );
    if (newState.user)
      localStorage.setItem('user', JSON.stringify(newState.user));

    if (newState.token === null) localStorage.removeItem('access_token');
    if (newState.refreshToken === null)
      localStorage.removeItem('refresh_token');
    if (newState.refreshTokenExpiration === null)
      localStorage.removeItem('refresh_token_expiration');
    if (newState.user === null) localStorage.removeItem('user');
  }

  async login(credentials: {
    email: string;
    password: string;
    rememberMe: boolean;
  }): Promise<MyAppResponse<AuthenticationResponse>> {
    const login$ = this.http.post<MyAppResponse<AuthenticationResponse>>(
      `${environment.apiRoot}/v1/Account/Login`,
      credentials
    );

    const response = await firstValueFrom(login$);

    if (response.succeeded && response.data) {
      const {
        id,
        userName,
        email,
        token,
        refreshToken,
        refreshTokenExpiration,
      } = response.data;

      this.updateState({
        user: { id, userName, email },
        token,
        refreshToken,
        refreshTokenExpiration: new Date(refreshTokenExpiration),
        isAuthenticated: true,
      });
    }

    return response;
  }

  async refreshTokenApi(): Promise<MyAppResponse<AuthenticationResponse>> {
    const refreshToken = this._state().refreshToken;
    if (!refreshToken) {
      throw new Error('No refresh token available');
    }

    const refreshToken$ = this.http.get<MyAppResponse<AuthenticationResponse>>(
      `${environment.apiRoot}/v1/Account/RefreshToken?refreshToken=${refreshToken}`
    );

    const response = await firstValueFrom(refreshToken$);

    if (response.succeeded && response.data) {
      const {
        id,
        userName,
        email,
        token,
        refreshToken: newRefreshToken,
        refreshTokenExpiration,
      } = response.data;

      this.updateState({
        user: { id, userName, email },
        token,
        refreshToken: newRefreshToken,
        refreshTokenExpiration: new Date(refreshTokenExpiration),
        isAuthenticated: true,
      });
    }

    return response;
  }

  // Other methods remain the same as previous implementation
  // (register, confirmEmail, forgotPassword, etc.)
  // ...

  logout(): void {
    const token = this._state().token;
    if (token) {
      this.revokeToken(token).catch(console.error);
    }

    this.updateState({
      user: null,
      token: null,
      refreshToken: null,
      refreshTokenExpiration: null,
      isAuthenticated: false,
    });

    this.router.navigate(['/login']);
  }

  private async revokeToken(token: string): Promise<MyAppResponse<void>> {
    const revokeToken$ = this.http.post<MyAppResponse<void>>(
      `${environment.apiRoot}/v1/Account/RevokeToken`,
      { token }
    );
    return firstValueFrom(revokeToken$);
  }

  // Role checks
  hasRole(role: string): boolean {
    return this._state().user?.roles?.includes(role) || false;
  }

  hasAnyRole(roles: string[]): boolean {
    return roles.some((role) => this.hasRole(role));
  }
}
