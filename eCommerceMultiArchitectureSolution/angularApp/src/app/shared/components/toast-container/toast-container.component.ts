import { Component, inject } from '@angular/core';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-toast-container',
  standalone: true,
  imports: [NgbToastModule],
  template: `
    @for (toast of toasterService.toasts(); track toast.id) {
      <ngb-toast
        [class]="toast.classname"
        [autohide]="toast.autohide ?? true"
        [delay]="toast.delay ?? 5000"
        (hidden)="toasterService.remove(toast)"
      >
        <ng-template ngbToastHeader>
          <strong class="me-auto">{{ toast.headertext }}</strong>
        </ng-template>
        {{ toast.textOrTpl }}
      </ngb-toast>
    } @empty {
      <!-- No toasts to display -->
    }
  `,
  styles: [
    `
      :host {
        position: fixed;
        top: 20px;
        right: 20px;
        z-index: 1200;
        max-width: 350px;
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
      }
    `
  ]
})
export class ToastContainerComponent {
  toasterService = inject(ToasterService);
}