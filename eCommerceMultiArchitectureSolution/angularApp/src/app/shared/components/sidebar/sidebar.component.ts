import { Component, inject } from '@angular/core';
import { StateService } from '../../../core/services/state-service';

@Component({
  selector: 'app-sidebar',
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
})
export class SidebarComponent {
  stateService = inject(StateService);
}
