import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedIn = false;

  // Simulating a login function
  login(username: string, password: string): boolean {
    // Add your actual authentication logic here
    // For simplicity, we'll just set isLoggedIn to true if username and password are valid
    if (username === 'yourUsername' && password === 'yourPassword') {
      this.isLoggedIn = true;
      return true;
    }
    return false;
  }

  // Simulating a logout function
  logout(): void {
    // Add your actual logout logic here
    this.isLoggedIn = false;
  }

  // Check if the user is logged in
  isAuthenticated(): boolean {
    return this.isLoggedIn;
  }
}
