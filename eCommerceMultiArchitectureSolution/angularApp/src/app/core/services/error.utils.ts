import { HttpErrorResponse } from '@angular/common/http';
import { MyAppResponse } from '../models/common-models';
import { WritableSignal } from '@angular/core';

// error.utils.ts
export function handleApiError(
  err: unknown,
  errorSignal?: WritableSignal<string | null>,
  context?: string
): string {
  let errorMessage = 'An unexpected error occurred';
  let logMessage = errorMessage;

  if (err instanceof HttpErrorResponse) {
    const errorResponse = err.error as MyAppResponse<unknown>;
    errorMessage =
      errorResponse?.message ||
      errorResponse?.errors?.join(', ') ||
      err.message ||
      `HTTP Error: ${err.status}`;

    logMessage = `${err.status} - ${errorMessage}`;
  } else if (err instanceof Error) {
    errorMessage = err.message;
    logMessage = err.stack || err.message;
  }

  // Add context if provided
  if (context) {
    logMessage = `[${context}] ${logMessage}`;
  }

  console.error(logMessage);

  if (errorSignal) {
    errorSignal.set(errorMessage);
  }

  return errorMessage;
}
