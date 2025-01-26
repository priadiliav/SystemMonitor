import { Component } from '@angular/core';
import { ComputerTableComponent } from './computer-table/computer-table.component';

@Component({
  selector: 'app-root',
  imports: [ComputerTableComponent],
  standalone: true,
  template: `<app-computer-table></app-computer-table>`,
})
export class AppComponent {}
