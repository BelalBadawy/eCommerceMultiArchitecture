import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../auth.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ValidatorFn,
  AbstractControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { ToasterService } from '../../../core/services/toaster.service';
import { ResetPasswordDto } from '../models/auth-model';
import { handleApiError } from '../../../core/services/error.utils';

@Component({
  selector: 'app-reset-password',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent {
  loading = signal(false);
  errorMessage = signal<string | null>(null);
  email = signal<string>('');
  token = signal<string>('');
  error = signal<string | null>(null);

  private authService = inject(AuthService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toasterService = inject(ToasterService);
  resetPasswordForm: FormGroup;
  constructor(private fb: FormBuilder) {
    this.route.queryParams.subscribe((params) => {
      const _token = params['token'];
      const _email = params['email'];

      if (_token && _email) {
        this.email.set(_email);
        this.token.set(_token);
      } else {
        this.errorMessage.set(
          'Invalid reset password link. Please check your email for the correct link.'
        );
        this.loading.set(false);
      }
    });

    this.resetPasswordForm = this.fb.group(
      {
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator: ValidatorFn = (control: AbstractControl) => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    if (
      password &&
      confirmPassword &&
      password.value !== confirmPassword.value
    ) {
      return { passwordMismatch: true };
    }
    return null;
  };

  navigateToForgotPassword(): void {
    this.router.navigate(['/auth/forgot-password']);
  }

  async onSubmit() {
    if (this.resetPasswordForm.valid) {
      try {
        this.loading.set(true);
        this.errorMessage.set(null);

        const formData = {
          token: this.token(),
          email: this.email(),
          password: this.resetPasswordForm.get('password')?.value,
          confirmPassword: this.resetPasswordForm.get('confirmPassword')?.value,
        };

        const response = await this.authService.resetPassword(
          formData as ResetPasswordDto
        );

        if (response.succeeded) {
          this.toasterService.showSuccess(
            response.message || 'Reset password successful'
          );
        } else {
          this.error.set(
            response.message ||
              response.errors?.join(', ') ||
              'Reset Password failed. Please try again.'
          );

          this.toasterService.showError(
            this.error() || 'Reset Password failed. Please try again.'
          );
        }
      } catch (err: any) {
        let errorMsg = handleApiError(err, this.error, 'Reset Password');
        this.toasterService.showError(errorMsg);
      } finally {
        this.loading.set(false);
      }
    }
  }
}
