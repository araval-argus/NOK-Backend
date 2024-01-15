import { Component } from '@angular/core';
import 'font-awesome/css/font-awesome.min.css';
import { HomeComponent } from '../../home/home.component';
import { NudgetsComponent } from '../../nudgets/nudgets.component';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { SettingsComponent } from '../../settings/settings.component';
import { CommentsComponent } from '../../comments/comments.component';
import { routes } from '../../app.routes';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    SidebarComponent,
  RouterLink
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  logout() {
    // Implement logout logic
    console.log('Logging out...');
  }

}
