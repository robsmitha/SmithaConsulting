import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface AuthData {
  success: boolean;
  message: string;
}

@Injectable()
export class AuthService {

  private loggedInStatus = JSON.parse(localStorage.getItem('loggedIn') || 'false')
  private customerId = JSON.parse(localStorage.getItem('customerId') || 'false')

  constructor(private http: HttpClient) {}

  setLoggedIn(value: boolean) {
    this.loggedInStatus = value;
    localStorage.setItem('loggedIn', value.toString());
  }

  setCustomerId(id: number) {
    this.customerId = id;
    localStorage.setItem('customerId', id.toString());
  }

  get isLoggedIn() {
    return JSON.parse(localStorage.getItem('loggedIn') || this.loggedInStatus);
  }

  get CustomerId() {
    return JSON.parse(localStorage.getItem('customerId') || this.customerId);
  }
}
