import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { Customer } from '../classes/customer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
})
export class SignInComponent {
  customer: Customer
  signInForm;

  constructor(private customerService: CustomerService, private formBuilder: FormBuilder, private router: Router, private authService: AuthService) {
    this.signInForm = this.formBuilder.group({
      email: '',
      password: ''
    });
  }

  ngOnInit() {

  }

  onSubmit(signInData) {
    this.customerService.signInCustomer(signInData)
      .subscribe(data => {
        if (data.id > 0) {
          this.authService.setLoggedIn(true);
          this.authService.setCustomerId(data.id);
          this.router.navigateByUrl('profile');
        } else {
          alert('Not found');
        }
      });
  }
}
