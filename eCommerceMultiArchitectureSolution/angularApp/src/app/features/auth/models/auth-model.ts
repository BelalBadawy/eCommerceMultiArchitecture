// src/app/core/types/api.types.ts
export interface RegistrationDto {
  fullName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface LoginDto {
  email: string;
  password: string;
  rememberMe: boolean;
}

export interface ConfirmEmailDto {
  token: string;
  email: string;
}

export interface ResetPasswordDto {
  token: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface ProblemDetails {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  instance?: string;
}

export interface AuthenticationResponse {
  id: string;
  userName: string;
  email: string;
  token: string;
  refreshToken: string;
  refreshTokenExpiration: Date;
}

// Add other DTO interfaces as needed
