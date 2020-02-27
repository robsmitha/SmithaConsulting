import { Component } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { Customer } from '../classes/customer';
import { Router } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
})

export class ProfileComponent {
  customer: Customer
  private id: number
  fullName: string
  constructor(private customerService: CustomerService,
    private router: Router,
    private authService: AuthService,
    private datePipe: DatePipe) {
    this.id = authService.CustomerId
  }
  ngOnInit() {
    this.customerService.loadCustomer(this.id).
      subscribe(data => {
        this.customer = data
        this.customer.createdAt = this.datePipe.transform(this.customer.createdAt, 'MM-dd-yyyy')
        this.fullName = data.firstName + " " + data.middleName + " " + data.lastName
      });
  }
  editProfile(): void {
    this.router.navigateByUrl('edit-profile');
  }
}
