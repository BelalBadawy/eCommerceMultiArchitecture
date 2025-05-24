import { Component, inject } from '@angular/core';
import { StateService } from '../../../core/services/state-service';
import { AuthService } from '../../../features/auth/auth.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  stateService = inject(StateService);
  authService = inject(AuthService);
  toggleSidebar() {
    this.stateService.toggle();
  }
}
