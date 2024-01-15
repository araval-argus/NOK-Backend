import { Component } from '@angular/core';
import { SidebarComponent } from '../layout/sidebar/sidebar.component';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [SidebarComponent],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {

}
