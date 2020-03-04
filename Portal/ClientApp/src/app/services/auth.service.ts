import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class AuthService {

  constructor(private http: HttpClient) {}

  setLoggedIn(value: boolean) {
    localStorage.setItem('loggedIn', value.toString());
  }

  setCustomerId(id: number) {
    localStorage.setItem('customerId', id.toString());
  }

  setGreeting(greeting: string) {
    localStorage.setItem('greeting', greeting);
  }

  setEmail(email: string) {
    localStorage.setItem('email', email);
  }

  setLoginSession(data: any) {
    this.setLoggedIn(true);
    this.setCustomerId(data.id);
    this.setEmail(data.email);
    this.setGreeting(`Hi, ${data.firstName}`);
  }

  get isLoggedIn(): boolean {
    return JSON.parse(localStorage.getItem('loggedIn') || 'false')
  }

  get CustomerId(): number {
    return JSON.parse(localStorage.getItem('customerId') || '0')
  }

  get getGreeting() {
    return localStorage.getItem('greeting')
  }

  get getEmail() {
    return localStorage.getItem('email')
  }
}
