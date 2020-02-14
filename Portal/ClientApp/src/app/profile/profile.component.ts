import { Component } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { Customer } from '../classes/customer';
import { Router } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
})

export class ProfileComponent {
  customer: Customer
  private id: number
  constructor(private customerService: CustomerService,
    private router: Router,
    private authService: AuthService) {
    this.id = authService.CustomerId
  }
  ngOnInit() {
    this.customerService.loadCustomer(this.id).
      subscribe(data => {
        this.customer = data
      });
  }

}
