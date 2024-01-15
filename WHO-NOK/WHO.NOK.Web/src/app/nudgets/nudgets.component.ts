import { Component } from '@angular/core';
import { SidebarComponent } from '../layout/sidebar/sidebar.component';

@Component({
  selector: 'app-nudgets',
  standalone: true,
  imports: [SidebarComponent],
  templateUrl: './nudgets.component.html',
  styleUrl: './nudgets.component.css'
})
export class NudgetsComponent {

}
