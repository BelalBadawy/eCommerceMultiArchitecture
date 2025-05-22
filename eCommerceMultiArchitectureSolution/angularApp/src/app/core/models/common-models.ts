export interface MyAppResponse<T> {
  statusCode: number;
  succeeded: boolean;
  message?: string;
  errors?: string[];
  data?: T;
  redirectTo?: string;
}
