import { Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { inject, signal, Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MyAppResponse } from '../../../core/models/common-models';
import { handleApiError } from '../../../core/services/error.utils';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule, // Only needed for form directives
    // No CommonModule needed!
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  authService = inject(AuthService);
  fb = inject(FormBuilder);
  router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    rememberMe: [false],
  });

  async onSubmit() {
    if (this.loginForm.invalid) return;

    this.loading.set(true);
    this.error.set(null);

    try {
      const response = await this.authService.login({
        email: this.loginForm.value.email!,
        password: this.loginForm.value.password!,
        rememberMe: this.loginForm.value.rememberMe!,
      });

      if (response.succeeded) {
        this.router.navigate(['/home']);
      } else {
        this.error.set(
          response.message ||
            response.errors?.join(', ') ||
            'Login failed. Please try again.'
        );
      }
    } catch (err: unknown) {
      handleApiError(err, this.error, 'Login');
    } finally {
      this.loading.set(false);
    }
  }
}
