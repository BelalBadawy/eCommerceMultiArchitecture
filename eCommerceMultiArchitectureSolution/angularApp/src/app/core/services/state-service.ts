import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StateService {
  constructor() {}

  public isActive = signal(false);

  toggle() {
    this.isActive.update((value) => !value);
  }
}
