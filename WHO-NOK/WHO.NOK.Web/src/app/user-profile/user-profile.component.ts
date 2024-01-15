import { Component, OnInit } from '@angular/core';
import { UserProfileService, User } from '../Services/user-profile.service';
import { CommonModule } from '@angular/common';
import 'font-awesome/css/font-awesome.min.css';
import { SidebarComponent } from '../layout/sidebar/sidebar.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [SidebarComponent,CommonModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
 
  users: User[] = [];
  filteredUsers: any[] = [];
  selectedFilter: string = 'All';
  viewMode: 'card' | 'tabular' = 'card';
  constructor(private userService: UserProfileService, private router: Router) {}
  openAddUserComponent() {
    this.router.navigate(['/add-user']);
  }
  
  ngOnInit(): void {
    this.loadUsers();
  }
  loadUsers(): void {
    this.userService.getUsers().subscribe((users) => {
      this.users = users;
    });
  }
  
  filterData(status: string) {
    this.selectedFilter = status;
    this.filteredUsers = this.users.filter(user => this.filterByStatus(user, status));
  }
  filterByStatus(user: any, status: string): boolean {
    if (status === 'All') {
      return true; // Show all users
    } else {
      return user.status === status;
    }
  }
  showRoleDropdown = false;

toggleRoleDropdown() {
  this.showRoleDropdown = !this.showRoleDropdown;
}

selectRole(role: string) {
  // Handle the selected role
  console.log('Selected Role:', role);
  this.showRoleDropdown = false;
  console.log(role);
}
setViewMode(mode: 'card' | 'tabular'): void {
  this.viewMode = mode;
}

performAction(user: User): void {
  // Add your action logic here
  console.log('Performing action for user:', user);
}
}


