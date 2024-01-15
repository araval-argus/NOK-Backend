import { Component } from '@angular/core';
import { SidebarComponent } from '../layout/sidebar/sidebar.component';
import { RouterModule, RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../layout/header/header.component';

@Component({
  selector: 'app-comments',
  standalone: true,
  imports: [SidebarComponent,RouterModule],
  templateUrl: './comments.component.html',
  styleUrl: './comments.component.css'
})
export class CommentsComponent {

}
