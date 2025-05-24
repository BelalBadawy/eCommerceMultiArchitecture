// src/app/core/services/auth.service.ts
import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';
import { MyAppResponse } from '../../core/models/common-models';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { RegistrationDto } from './models/auth-model';

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
  isAuthenticated = computed(() => {
    const state = this._state();
    return state.isAuthenticated && this.isTokenValid(state.token);
  });

  constructor() {
    this.initializeFromStorage();
  }

  /* ========== STATE MANAGEMENT ========== */
  private initializeFromStorage(): void {
    const token = localStorage.getItem('access_token');
    const refreshToken = localStorage.getItem('refresh_token');
    const refreshTokenExpiration = localStorage.getItem(
      'refresh_token_expiration'
    );
    const user = localStorage.getItem('user');

    if (token && user && this.isTokenValid(token)) {
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
    } else {
      this.clearAuthStorage();
    }
  }

  private updateState(newState: Partial<AuthState>): void {
    this._state.update((state) => ({ ...state, ...newState }));

    if (newState.token) localStorage.setItem('access_token', newState.token);
    if (newState.refreshToken)
      localStorage.setItem('refresh_token', newState.refreshToken);
    if (newState.refreshTokenExpiration) {
      localStorage.setItem(
        'refresh_token_expiration',
        newState.refreshTokenExpiration.toISOString()
      );
    }
    if (newState.user)
      localStorage.setItem('user', JSON.stringify(newState.user));

    if (newState.token === null) localStorage.removeItem('access_token');
    if (newState.refreshToken === null)
      localStorage.removeItem('refresh_token');
    if (newState.refreshTokenExpiration === null)
      localStorage.removeItem('refresh_token_expiration');
    if (newState.user === null) localStorage.removeItem('user');
  }

  private clearAuthStorage(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('refresh_token_expiration');
    localStorage.removeItem('user');
  }

  /* ========== TOKEN VALIDATION ========== */
  private isTokenValid(token: string | null): boolean {
    if (!token) return false;
    try {
      const decoded = jwtDecode<JwtPayload>(token);
      return !!decoded.exp && decoded.exp * 1000 > Date.now();
    } catch {
      return false;
    }
  }

  isTokenAboutToExpire(bufferSeconds: number = 300): boolean {
    const token = this._state().token;
    if (!token) return true;

    try {
      const decoded = jwtDecode<JwtPayload>(token);
      return (
        !decoded.exp || decoded.exp * 1000 - Date.now() < bufferSeconds * 1000
      );
    } catch {
      return true;
    }
  }

  getTokenExpiration(): Date | null {
    const token = this._state().token;
    if (!token) return null;

    try {
      const decoded = jwtDecode<JwtPayload>(token);
      return decoded.exp ? new Date(decoded.exp * 1000) : null;
    } catch {
      return null;
    }
  }

  getTokenRoles(): string[] {
    const token = this._state().token;
    if (!token) return [];

    try {
      const decoded = jwtDecode<any>(token);
      if (Array.isArray(decoded.roles)) return decoded.roles;
      if (decoded.roles) return [decoded.roles];
      if (
        decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
      ) {
        const roles =
          decoded[
            'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
          ];
        return Array.isArray(roles) ? roles : [roles];
      }
      return [];
    } catch {
      return [];
    }
  }

  /* ========== AUTH METHODS ========== */
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

  async register(credentials: RegistrationDto): Promise<MyAppResponse<string>> {
    const register$ = this.http.post<MyAppResponse<string>>(
      `${environment.apiRoot}/v1/Account/Register`,
      credentials
    );

    return await firstValueFrom(register$);
  }

  async forgotPassword(email: string): Promise<MyAppResponse<boolean>> {
    const forgotPassword$ = this.http.post<MyAppResponse<boolean>>(
      `${environment.apiRoot}/v1/Account/ForgotPassword?email={email}`,
      {}
    );

    return await firstValueFrom(forgotPassword$);
  }

  async refreshTokenApi(): Promise<boolean> {
    const refreshToken = this._state().refreshToken;
    if (!refreshToken || !this.isTokenValid(refreshToken)) {
      this.logout();
      return false;
    }

    try {
      const response = await firstValueFrom(
        this.http.get<MyAppResponse<AuthenticationResponse>>(
          `${environment.apiRoot}/v1/Account/RefreshToken?refreshToken=${refreshToken}`
        )
      );

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
        return true;
      }
      return false;
    } catch {
      this.logout();
      return false;
    }
  }

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

    this.clearAuthStorage();
    this.router.navigate(['/auth/login']);
  }

  private async revokeToken(token: string): Promise<MyAppResponse<void>> {
    const revokeToken$ = this.http.post<MyAppResponse<void>>(
      `${environment.apiRoot}/v1/Account/RevokeToken`,
      { token }
    );
    return firstValueFrom(revokeToken$);
  }

  /* ========== ROLE CHECKS ========== */
  hasRole(role: string): boolean {
    return this.getTokenRoles().includes(role);
  }

  hasAnyRole(roles: string[]): boolean {
    return this.getTokenRoles().some((role) => roles.includes(role));
  }
}
