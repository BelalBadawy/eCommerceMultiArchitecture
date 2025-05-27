import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthService } from '../auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToasterService } from '../../../core/services/toaster.service';
import { handleApiError } from '../../../core/services/error.utils';

@Component({
  selector: 'app-confirm-email',
  imports: [],
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.scss',
})
export class ConfirmEmailComponent implements OnInit {
  loading = signal(true);
  confirmationStatus = signal<'processing' | 'success' | 'error'>('processing');
  error = signal<string | null>(null);
  email = signal<string>('');

  private authService = inject(AuthService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toasterService = inject(ToasterService);

  ngOnInit(): void {
    // Extract token and email from query params
    this.route.queryParams.subscribe((params) => {
      const token = params['token'];
      const email = params['email'];

      if (token && email) {
        this.email.set(email);
        this.confirmEmail(token, email);
      } else {
        this.confirmationStatus.set('error');
        this.error.set(
          'Invalid confirmation link. Please check your email for the correct link.'
        );
        this.loading.set(false);
      }
    });
  }

  async confirmEmail(token: string, email: string): Promise<void> {
    try {
      const response = await this.authService.confirmEmail(email, token);

      if (response.succeeded) {
        this.confirmationStatus.set('success');
        this.toasterService.showSuccess('Email confirmed successfully!');
        this.navigateToLogin();
      } else {
        this.confirmationStatus.set('error');
        this.error.set(
          response.message ||
            'Email confirmation failed. The link may have expired.'
        );
        this.toasterService.showError(
          response.message || 'Email confirmation failed.'
        );
      }
    } catch (err: any) {
      let errorMsg = handleApiError(err, this.error, 'Login');
      this.confirmationStatus.set('error');
      this.error.set('An unexpected error occurred. Please try again later.');
      this.toasterService.showError(err);
    } finally {
      this.loading.set(false);
    }
  }

  async resendEMailConfirmation() {
    try {
      const response = await this.authService.resendEMailConfirmation(
        this.email()
      );

      if (response.succeeded) {
        this.toasterService.showSuccess('Password reset email sent.');
        this.router.navigate(['/auth/login']);
      } else {
        this.error.set(
          response.message ||
            response.errors?.join(', ') ||
            'Login failed. Please try again.'
        );

        this.toasterService.showError(
          this.error() || 'Reset Password failed. Please try again.'
        );
      }
    } catch (err: unknown) {
      let errorMsg = handleApiError(err, this.error, 'Login');
      this.toasterService.showError(errorMsg);
    } finally {
      this.loading.set(false);
    }
  }

  navigateToLogin(): void {
    this.router.navigate(['/login'], {
      queryParams:
        this.confirmationStatus() === 'success' ? { email: this.email() } : {},
    });
  }
}
