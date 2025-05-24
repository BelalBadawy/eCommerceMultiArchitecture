import { Component, inject, WritableSignal, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../auth.service';
import { RegistrationDto } from '../models/auth-model';
import { handleApiError } from '../../../core/services/error.utils';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss', //
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loading = signal(false);
  error: WritableSignal<string | null> = signal(null);

  registerForm = this.fb.group(
    {
      fullName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    },
    {
      validators: this.passwordMatchValidator,
    }
  );

  passwordMatchValidator(form: any) {
    return form.get('password')?.value === form.get('confirmPassword')?.value
      ? null
      : { mismatch: true };
  }

  async onSubmit() {
    this.error.set(null);
    if (this.registerForm.invalid) return;

    this.loading.set(true);

    try {
      const credentials: RegistrationDto = {
        fullName: this.registerForm.value.fullName!,
        email: this.registerForm.value.email!,
        password: this.registerForm.value.password!,
        confirmPassword: this.registerForm.value.confirmPassword!,
      };

      const response = await this.authService.register(credentials);

      if (response.succeeded) {
        this.router.navigate(['/auth/login'], {
          queryParams: { registered: true },
        });
      } else {
        this.error.set(
          response.message ||
            response.errors?.join(', ') ||
            'Registration failed. Please try again.'
        );
      }
    } catch (err) {
      handleApiError(err, this.error, 'RegisterComponent');
    } finally {
      this.loading.set(false);
    }
  }
}
