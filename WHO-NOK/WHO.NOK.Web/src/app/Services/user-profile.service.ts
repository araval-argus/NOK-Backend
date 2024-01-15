import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  getUsers(): Observable<User[]> {
    // Replace this with your actual API call or data source
    const dummyUsers: User[] = [
      { name: 'Steeve', status: 'Active', jobRole: 'Developer', country: 'USA', userRole: 'Contributor' },
      { name: 'Jane Doe', status: 'Inactive', jobRole: 'Designer', country: 'Canada', userRole: 'Reviewer' },
      { name: 'Richel', status: 'Inactive', jobRole: 'Designer', country: 'Australia', userRole: 'Reviewer' },
      { name: 'Mark', status: 'Ative', jobRole: 'Developer', country: 'Australia', userRole: 'User' },
    ];

    return of(dummyUsers);
  }

  constructor() { }
}
export interface User {
  name: string;
  status: string;
  jobRole: string;
  country: string;
  userRole: string;
}