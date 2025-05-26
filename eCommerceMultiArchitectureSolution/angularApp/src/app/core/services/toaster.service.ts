import { Injectable, TemplateRef, signal, computed } from '@angular/core';

export interface Toast {
  id: string;
  textOrTpl: string | TemplateRef<any>;
  classname?: string;
  delay?: number;
  autohide?: boolean;
  headertext?: string;
}

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  // Using signals for reactive state management
  private _toasts = signal<Toast[]>([]);

  // Computed signal for public access
  toasts = computed(() => this._toasts());

  // Computed signal for toast count
  toastCount = computed(() => this._toasts().length);

  // Show a success toast
  showSuccess(message: string, options?: Partial<Toast>): void {
    this.show(message, {
      classname: 'bg-success text-light',
      delay: 5000,
      autohide: true,
      headertext: 'Success',
      ...options,
    });
  }

  // Show an error toast
  showError(message: string, options?: Partial<Toast>): void {
    this.show(message, {
      classname: 'bg-danger text-light',
      delay: 8000,
      autohide: true,
      headertext: 'Error',
      ...options,
    });
  }

  // Show a warning toast
  showWarning(message: string, options?: Partial<Toast>): void {
    this.show(message, {
      classname: 'bg-warning text-dark',
      delay: 6000,
      autohide: true,
      headertext: 'Warning',
      ...options,
    });
  }

  // Show an info toast
  showInfo(message: string, options?: Partial<Toast>): void {
    this.show(message, {
      classname: 'bg-info text-light',
      delay: 5000,
      autohide: true,
      headertext: 'Info',
      ...options,
    });
  }

  // Generic show method
  show(
    textOrTpl: string | TemplateRef<any>,
    options: Partial<Toast> = {}
  ): void {
    const toast: Toast = {
      id: crypto.randomUUID(), // Using modern crypto API
      textOrTpl,
      classname: options.classname || 'bg-light',
      delay: options.delay || 5000,
      autohide: options.autohide !== false,
      headertext: options.headertext || 'Notification',
    };

    this._toasts.update((toasts) => [...toasts, toast]);
  }

  // Remove a specific toast
  remove(toast: Toast): void {
    this._toasts.update((toasts) => toasts.filter((t) => t.id !== toast.id));
  }

  // Clear all toasts
  clear(): void {
    this._toasts.set([]);
  }
}
