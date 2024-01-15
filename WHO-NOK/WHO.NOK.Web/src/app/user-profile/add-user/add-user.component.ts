import { Component } from '@angular/core';
import { UserProfileComponent } from '../user-profile.component';
import { FormsModule ,} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [UserProfileComponent,FormsModule,CommonModule],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})


export class AddUserComponent {

  user = {
    firstName: '',
    lastName: '',
    email: '',
    jobTitle: '',
    country: '',
    role: '',
    institution: '',
    affiliation: '',
    reasonForAccess: '',
    status: 'INACTIVE'
  };
  constructor(private router: Router) {}

  onSubmit() {
    // Add your form submission logic here
    console.log('Form submitted:', this.user);
    this.goBackToUserComponent();
    // You can call a service to send the data to the server or perform any other actions
  }
  goBackToUserComponent() {
    this.router.navigate(['/userprofile']);
  }

}

