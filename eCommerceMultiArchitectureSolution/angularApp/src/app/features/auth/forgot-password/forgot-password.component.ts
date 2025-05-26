import { Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { inject, signal, Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MyAppResponse } from '../../../core/models/common-models';
import { handleApiError } from '../../../core/services/error.utils';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-forgot-password',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  authService = inject(AuthService);
  fb = inject(FormBuilder);
  router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  private toaster = inject(ToasterService);

  fpForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
  });

  async onSubmit() {
    if (this.fpForm.invalid) return;

    this.loading.set(true);
    this.error.set(null);

    try {
      const response = await this.authService.forgotPassword(
        this.fpForm.value.email!
      );

      if (response.succeeded) {
        this.toaster.showSuccess('Password reset email sent.');
        this.router.navigate(['/auth/login']);
      } else {
        this.error.set(
          response.message ||
            response.errors?.join(', ') ||
            'Login failed. Please try again.'
        );

        this.toaster.showError(
          this.error() || 'Reset Password failed. Please try again.'
        );
      }
    } catch (err: unknown) {
      let errorMsg = handleApiError(err, this.error, 'Login');
      this.toaster.showError(errorMsg);
    } finally {
      this.loading.set(false);
    }
  }
}
