import { Component, inject } from '@angular/core';
import { StateService } from '../../core/services/state-service';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  stateService = inject(StateService);
  toggleSidebar() {
    this.stateService.toggle();
  }
}
