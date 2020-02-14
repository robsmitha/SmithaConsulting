import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { Customer } from '../classes/customer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
})
export class SignUpComponent {
  customer: Customer
  signUpForm;

  constructor(private customerService: CustomerService, private formBuilder: FormBuilder, private router: Router, private authService: AuthService) {
    this.signUpForm = this.formBuilder.group({
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      confirmPassword: ''
    });
  }

  ngOnInit() {

  }

  onSubmit(signUpData) {
    this.customerService.signUpCustomer(signUpData)
      .subscribe(data => {
        if (data.id) {
          this.authService.setLoggedIn(true);
          this.authService.setCustomerId(data.id);
          this.router.navigateByUrl('profile');
        } else {
          alert('Not found');
        }
      });
  }
}
