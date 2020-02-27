import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isDropdownExpanded = false;

  constructor(private authService: AuthService) {

  }

  get isLoggedIn() {
    return this.authService.isLoggedIn;
  }

  get greeting() {
    return this.authService.getGreeting;
  }

  get email() {
    return this.authService.getEmail;
  }

  collapse() {
    this.isExpanded = false;
    this.isDropdownExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  toggleDropdown() {
    this.isDropdownExpanded = !this.isDropdownExpanded;
  }
}
